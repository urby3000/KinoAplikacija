using KinoAplikacija.Entity;
using NHibernate;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Windows.Forms;
//api key --

namespace KinoAplikacija.User_Controls.PasswordResetForm
{
    public partial class PasswordResetForm : Form
    {
        string emailHtmlTemplate = Environment.CurrentDirectory + @"\Templates\email template\";
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        public int resetStage = 1;
        User user;
        PasswordReset pr = new PasswordReset();
        public PasswordResetForm()
        {
            InitializeComponent();
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void PasswordResetForm_Load(object sender, EventArgs e)
        {
            setTextLanguage();
            stage1Text();
        }
        public void setTextLanguage()
        {
            this.Text = Properties.Resources.PasswordResetFormText;
        }
        private string CreateHtml()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(emailHtmlTemplate + "index.html"))
            {
                body = reader.ReadToEnd();
            }
            //translated
            body = body.Replace("{Name}", user.Name);
            body = body.Replace("{HelloEmail}", Properties.Resources.HelloEmail);
            body = body.Replace("{BodyTextEmail}", Properties.Resources.BodyTextEmail);
            body = body.Replace("{PleaseCopyEmail}", Properties.Resources.PleaseCopyEmail);
            body = body.Replace("{SecurityCodeEmail}", Properties.Resources.SecurityCodeEmail);
            body = body.Replace("{UseItEmail}", Properties.Resources.UseItEmail);
            body = body.Replace("{SecurityCode}", pr.SecurityCode);
            return body;
        }
        public string getRandomSecurityCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (resetStage == 1)
            {//pošlji email z kodo in vstavi v bazo
             //preveri email
                try
                {
                    m_session.Clear();
                    IQuery query = m_session.CreateQuery("from User u where u.Email='" + textbox1.Text + "'");
                    User res = query.UniqueResult<User>();
                    if (res == null)
                    {
                        MessageBox.Show(Properties.Resources.ErrorUserExist);
                        return;
                    }
                    user = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                //preveri če je že kakšen password reset v teku
                try
                {
                    IQuery query = m_session.CreateQuery("from PasswordReset pr where pr.User=" + user.Id
                                                            + " AND (pr.ResetDate IS NULL OR pr.ResetDate>'" + DateTime.Now + "')"
                                                            + " AND pr.Reset=" + false);
                    PasswordReset res = query.UniqueResult<PasswordReset>();
                    if (res == null)
                    {
                        pr.User = user;
                        pr.SecurityCode = getRandomSecurityCode(5);
                        pr.ResetDate = null;
                        pr.Reset = false;

                    }
                    else
                    {
                        pr = res;
                        pr.ResetDate = null;
                        pr.Reset = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        m_session.SaveOrUpdate(pr);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                var apiKey = "--";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("support@kinoaplikacija.com", "Kino Aplikacija Support");
                var subject = Properties.Resources.EmailSubject + pr.SecurityCode;
                var to = new EmailAddress(user.Email, user.Name + " " + user.Surname);
                var plainTextContent = pr.SecurityCode;
                var htmlContent = CreateHtml();
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);
                resetStage++;
                stage2Text();
            }
            else if (resetStage == 2)
            {//uporabnik je dobil email in želi vpisati kodo
             //updatamo ResetDate - uporabnik ima 5 minut da vpiše kodo
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        pr.ResetDate = DateTime.Now.AddMinutes(5);
                        m_session.Update(pr);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                stage3Text();
                resetStage++;
            }
            else if (resetStage == 3)
            {//preveri kodo in čas
                try
                {
                    IQuery query = m_session.CreateQuery("from PasswordReset pr where pr.User=" + user.Id
                                                            + " AND pr.ResetDate>'" + DateTime.Now + "'"
                                                            + " AND pr.Reset=" + false
                                                            + " AND pr.SecurityCode='" + textbox1.Text.ToString() + "'");
                    PasswordReset res = query.UniqueResult<PasswordReset>();
                    if (res == null)
                    {
                        MessageBox.Show(Properties.Resources.ErrorSecurityCodeTime);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        pr.Reset = true;
                        m_session.Update(pr);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                stage4Text();
                resetStage++;//če je vse vredu lahko vpiše novo geslo
            }
            else if (resetStage == 4)
            {//vnesemo novo geslo uporabniku
                if (string.IsNullOrEmpty(textbox1.Text))
                {
                    MessageBox.Show(Properties.Resources.ErrorInputPassword);
                    return;
                }
                user.PasswordHash = GetMd5Hash(textbox1.Text);
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        m_session.Update(user);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                MessageBox.Show(Properties.Resources.SuccessPasswordReset);
                this.Close();
            }
        }

        public void stage1Text()
        {

            TextboxLabel.Text = Properties.Resources.EmailLabel;
            textbox1.Visible = true;
            textbox1.Text = "";
            HelpLabel.Text = Properties.Resources.Stage1HelpLabel;
            button1.Text = Properties.Resources.Stage1Button;
        }

        public void stage2Text()
        {
            TextboxLabel.Text = "";
            textbox1.Visible = false;
            textbox1.Text = "";
            HelpLabel.Text = Properties.Resources.Stage2HelpLabel;
            button1.Text = Properties.Resources.Stage2Button;

        }
        public void stage3Text()
        {
            TextboxLabel.Text = Properties.Resources.Stage3TextboxLabel;
            textbox1.Visible = true;
            textbox1.Text = "";
            HelpLabel.Text = Properties.Resources.Stage3HelpLabel;
            button1.Text = Properties.Resources.Stage3Button;
        }
        public void stage4Text()
        {
            TextboxLabel.Text = "";
            textbox1.Visible = true;
            textbox1.UseSystemPasswordChar = true;
            textbox1.Text = "";
            HelpLabel.Text = Properties.Resources.Stage4HelpLabel;
            button1.Text = Properties.Resources.Stage4Button;
        }
        static string GetMd5Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
