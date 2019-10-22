using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KinoAplikacija.Entity;
using NHibernate;
using KinoAplikacija.User_Controls.MainPanels.Normal.Bills;

namespace KinoAplikacija.User_Controls.MainPanels.Normal
{
    public partial class UserBillsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private User CurrentUser;
        private List<Bill> bills;
        public UserBillsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void UserBills_Load(object sender, EventArgs e)
        {
            refresh();
            setTextLanguage();
        }
        public void setTextLanguage() {
            BillsReservationsLabel.Text = Properties.Resources.BillsReservationsLabel;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c.GetType() == typeof(BillForUser))
                {
                    BillForUser bfu = (BillForUser)c;
                    bfu.setTextLanguage();
                }
            }
        }
        public void refresh()
        {
            m_session.Clear();
            bills = m_session.CreateCriteria(typeof(Bill)).List<Bill>().ToList();
            flowLayoutPanel1.Controls.Clear();
            foreach (Bill b in bills)
            {
                bool usersBill = false;
                foreach (Reservation r in b.Reservations)
                {
                    if (r.User.Id == CurrentUser.Id)
                    {
                        usersBill = true;
                        break;

                    }
                }
                if (usersBill)
                {
                    BillForUser bfu = new BillForUser(b, CurrentUser, this);
                    bfu.SetNhib(m_sessionfactory, m_session);
                    flowLayoutPanel1.Controls.Add(bfu);
                }
            }
        }
    }
}
