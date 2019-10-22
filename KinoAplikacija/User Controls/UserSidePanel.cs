using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using KinoAplikacija.Entity;
using KinoAplikacija.User_Controls.MainPanels.Normal;

namespace KinoAplikacija.User_Controls
{
    public partial class UserSidePanel : UserControl
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        FlowLayoutPanel MainPanel;
        public User CurrentUser;
        HomeForm homeform;
        public UserSidePanel(FlowLayoutPanel mp,User user,HomeForm h)
        {
            InitializeComponent();
            MainPanel = mp;
            CurrentUser = user;
            homeform = h;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_SessionFactory = isf;
            m_Session = iss;
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            ProfileControl pc = new ProfileControl(CurrentUser,this,homeform);
            pc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(pc);
        }

        private void EventsButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            UserEventsControl uec = new UserEventsControl(CurrentUser);
            uec.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(uec);
        }

        private void ReservationsBillsButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            UserBillsControl ubc = new UserBillsControl(CurrentUser);
            ubc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(ubc);
        }
        public void setTextLanguage()
        {
            ProfileButton.Text = Properties.Resources.ProfileButton;
            EventsButton.Text = Properties.Resources.EventsButton;
            ReservationsBillsButton.Text = Properties.Resources.ReservationsBillsButton;
        }
        private void UserSidePanel_Load(object sender, EventArgs e)
        {
            setTextLanguage();
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            ProfileControl pc = new ProfileControl(CurrentUser, this, homeform);
            pc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(pc);
        }
    }
}
