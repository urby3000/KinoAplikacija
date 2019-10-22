using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KinoAplikacija.User_Controls.MainPanels.Admin;
using NHibernate;
using KinoAplikacija.Entity;

namespace KinoAplikacija
{
    public partial class AdminSidePanel : UserControl
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        FlowLayoutPanel MainPanel;
        User CurrentUser;
        public AdminSidePanel(FlowLayoutPanel mp,User user)
        {
            InitializeComponent();
            MainPanel = mp;
            CurrentUser = user;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_SessionFactory = isf;
            m_Session = iss;
        }

        private void UsersButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            UsersControl usc = new UsersControl(CurrentUser);
            usc.SetNhib(m_SessionFactory,m_Session);
            MainPanel.Controls.Add(usc);

        }

        private void CountriesPlacesButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            CountriesControl cc = new CountriesControl(CurrentUser);
            cc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(cc);
            PlacesControl pc = new PlacesControl(CurrentUser);
            pc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(pc);
        }

        private void MoviesGenresButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            GenresControl gc = new GenresControl(CurrentUser);
            gc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(gc);
            MoviesControl mc = new MoviesControl(CurrentUser);
            mc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(mc);
        }

        private void TheatersRoomsButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            TheatersControl tc = new TheatersControl(CurrentUser);
            tc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(tc);
            RoomsControl rc = new RoomsControl(CurrentUser);
            rc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(rc);
        }

        private void EventsButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            EventsControl ec = new EventsControl(CurrentUser);
            ec.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(ec);
        }

        private void RBDButton_Click(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            ReservationsControl rc = new ReservationsControl(CurrentUser);
            rc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(rc);
            BillsControl bc = new BillsControl(CurrentUser);
            bc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(bc);
            DiscountsControl dc = new DiscountsControl(CurrentUser);
            dc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(dc);
        }

        private void AdminSidePanel_Load(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count > 0)
            {
                MainPanel.Controls.Clear();
            }
            UsersControl usc = new UsersControl(CurrentUser);
            usc.SetNhib(m_SessionFactory, m_Session);
            MainPanel.Controls.Add(usc);
        }
    }
}
