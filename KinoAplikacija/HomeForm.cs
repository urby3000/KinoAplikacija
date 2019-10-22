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
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using KinoAplikacija.User_Controls;
using KinoAplikacija.User_Controls.MainPanels.Language;

namespace KinoAplikacija
{
    public partial class HomeForm : Form
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        public User CurrentUser;
        public StartForm startform;
        public HomeForm(StartForm sf)
        {
            InitializeComponent();
            ConfigureLog4Net();
            ConfigureNHibernate(false);
            startform = sf;

        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_SessionFactory = isf;
            m_Session = iss;
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
        public void setTextLanguage()
        {
            this.Text = Properties.Resources.AppName;
            LogoutButton.Text = Properties.Resources.LogOutButton;
        }
        private void HomeForm_Load(object sender, EventArgs e)
        {

            //test query
            //123-123 admin
            //1234-123 navadn user
            /*IQuery query = m_Session.CreateQuery("from User u where u.TelephoneNumber=1234");
            User res = query.UniqueResult<User>();
            CurrentUser = res;*/
            if (CurrentUser != null)
            {
                if (CurrentUser.RightsLevel == "1")
                {
                    AdminSidePanel asp = new AdminSidePanel(MainPanel, CurrentUser);
                    asp.SetNhib(m_SessionFactory,m_Session);
                    SidePanel.Controls.Add(asp);
                    
                }
                if (CurrentUser.RightsLevel == "5")
                {
                    UserSidePanel usp = new UserSidePanel(MainPanel, CurrentUser,this);
                    usp.SetNhib(m_SessionFactory, m_Session);
                    SidePanel.Controls.Add(usp);
                    SidePanel.Controls.Add(new LanguageControl(this));
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            startform.Visible = true;
            this.Close();
        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            startform.Visible = true;
        }
    }
}
