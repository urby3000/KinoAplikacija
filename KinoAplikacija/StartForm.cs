using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using KinoAplikacija.Entity;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using KinoAplikacija.User_Controls.MainPanels.Language;
using KinoAplikacija.User_Controls.PasswordResetForm;

namespace KinoAplikacija
{   
    public partial class StartForm : Form
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        public StartForm()
        {
            InitializeComponent();
            ConfigureLog4Net();
            ConfigureNHibernate(false);
            //ConfigureNHibernate(true);
        }
        private void ResetSession()
        {
            m_Session.Close();
            m_Session.Dispose();
            m_Session = m_SessionFactory.OpenSession();
        }
        private void ConfigureLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        private void ConfigureNHibernate(bool bDrop)
        {
            Configuration config = new Configuration();
            config.Configure();
            HbmSerializer.Default.Validate = true;
            config.AddInputStream(HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
            //ustvarjanje nove tabele, brisanje tabele
            /*config.AddAssembly(typeof(User).Assembly);
            new SchemaExport(config).Execute(true, true, bDrop);*/
            m_SessionFactory = config.BuildSessionFactory();
            m_Session = m_SessionFactory.OpenSession();
        }
        private bool checkEmail(string s) {
            Regex rEMail = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (s.Length > 0)

            {

                if (!rEMail.IsMatch(s))

                {
                    return false;
                }

            }
            return true;
        }
        private bool checkIfNumberOrString(string s)//true number, false string
        {
            return int.TryParse(s, out int n);
        }

        private void TelephoneText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            e.Handled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool login = false,check=true;
            User u=new User();

            if (IsTextBoxEmpty(LoginText1) && IsTextBoxEmpty(LoginText2))
            {
                check = false;
            }
            if (!check) {
                return;
            }
            if (checkIfNumberOrString(LoginText1.Text))//true - samo številke
            {//primerjaja telefonsko številko za login
                try
                {
                    IQuery query = m_Session.CreateQuery("from User u where u.TelephoneNumber=" + LoginText1.Text);
                    User res = query.UniqueResult<User>();
                    if (res != null && res.PasswordHash.Equals(GetMd5Hash(LoginText2.Text)))
                    {
                        login = true;
                        u = res;
                    }
                    else {

                        ToolTip tt = new ToolTip();
                        tt.ToolTipIcon = ToolTipIcon.Error;
                        tt.Show(Properties.Resources.ErrorWrongTelephonePassword, LoginText1, 0, 0, 1000);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ResetSession();
                }
            }
            else
            {//false - string
                if (checkEmail(LoginText1.Text))
                {//primerjaj za Email login
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from User u where u.Email='" + LoginText1.Text + "'");
                        User res = query.UniqueResult<User>();
                        if (res != null && res.PasswordHash.Equals(GetMd5Hash(LoginText2.Text)))
                        {

                            login = true;
                            u = res;

                        }
                        else
                        {

                            ToolTip tt = new ToolTip();
                            tt.ToolTipIcon = ToolTipIcon.Error;
                            tt.Show(Properties.Resources.ErrorWrongEmailPassword, LoginText1, 0, 0, 1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        ResetSession();
                    }
                }
                else
                {
                    ToolTip tt = new ToolTip();
                    tt.ToolTipIcon = ToolTipIcon.Error;
                    tt.Show(Properties.Resources.ErrorEmailFormat, LoginText1, 0, 0, 1000);
                }
            }
            if (login) {
                HomeForm h = new HomeForm(this);
                h.CurrentUser = u;
                h.SetNhib(m_SessionFactory, m_Session);
                this.Hide();
                h.ShowDialog();
            }
        }

        private void StringOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private bool IsTextBoxEmpty(TextBox textbox)
        {
            if (textbox.Text != "") {
                return false;
            }
            ToolTip tt = new ToolTip();
            tt.ToolTipIcon = ToolTipIcon.Error;
            tt.Show(Properties.Resources.ErrorFillOut, textbox, 0, 0, 1000);
            return true;
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            bool signup = true;
            if ((IsTextBoxEmpty(NameText) || IsTextBoxEmpty(LastNameText) || IsTextBoxEmpty(EmailText) || IsTextBoxEmpty(TelephoneText) || IsTextBoxEmpty(PasswordText))) {
                signup = false;
            }
            if (!checkEmail(EmailText.Text))
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.Show(Properties.Resources.ErrorEmailFormat, EmailText, 0, 0, 1000);
                signup = false;
            }
            if (!checkIfNumberOrString(TelephoneText.Text))
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.Show(Properties.Resources.ErrorTelephoneNumberFormat, TelephoneText, 0, 0, 1000);
                signup = false;
            }
            if (!signup) {
                return;
            }
            //check if email or telephone number is in use
            try
            {
                IQuery query = m_Session.CreateQuery("from User u where u.Email='" + EmailText.Text + "' OR u.TelephoneNumber="+TelephoneText.Text);
                User res = query.UniqueResult<User>();
                if (res != null)
                {
                    MessageBox.Show(Properties.Resources.ErrorEmailTelephoneTaken);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ResetSession();
            }

            using (ISession m_Session = m_SessionFactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        User u = new User
                        {
                            Email = EmailText.Text,
                            TelephoneNumber = TelephoneText.Text,
                            Name = NameText.Text,
                            Surname = LastNameText.Text,
                            RightsLevel = "5",
                            JoinDate = DateTime.Now,
                            PasswordHash = GetMd5Hash(PasswordText.Text)
                        };
                        m_Session.Save(u);
                        tx.Commit();
                        MessageBox.Show(Properties.Resources.SuccessSignUp);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        ResetSession();
                    }
                }
            }
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

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            m_Session.Close();
            m_Session.Dispose();
            m_SessionFactory.Close();
            m_SessionFactory.Dispose();
        }
        public void setTextLanguage() {
            this.Text = Properties.Resources.AppName;
            LoginTitleLabel.Text = Properties.Resources.LoginTitleLabel;
            TelephoneOrEmailLabel.Text = Properties.Resources.TelephoneOrEmailLabel;
            PasswordLabel.Text = Properties.Resources.PasswordLabel;
            ForgetPasswordLinkLabel.Text = Properties.Resources.ForgetPasswordLinkLabel;
            LoginButton.Text = Properties.Resources.LoginButton;

            SignUpTitleLabel.Text = Properties.Resources.SignUpTitleLabel;
            NameLabel.Text = Properties.Resources.NameLabel;
            LastNameLabel.Text = Properties.Resources.SurnameLabel;
            EmailLabel.Text = Properties.Resources.EmailLabel;
            TelephoneNumberLabel.Text = Properties.Resources.TelephoneNumberLabel;
            PasswordLabel1.Text = Properties.Resources.PasswordLabel;
            SignUpButton.Text = Properties.Resources.SignUpButton;

        }
        private void StartForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new LanguageControl((Form)this));

            setTextLanguage();
        }

        private void ForgetPasswordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordResetForm prf= new PasswordResetForm();
            prf.SetNhib(m_SessionFactory,m_Session);
            prf.ShowDialog();


        }
    }
}
