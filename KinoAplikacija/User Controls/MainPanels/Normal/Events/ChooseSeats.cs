using KinoAplikacija.Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Events
{
    public partial class ChooseSeats : Form
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        public UserEventsControl uecontrol;
        Event ev;
        public Seats seats;
        public ChooseSeats(Event e,UserEventsControl ue)
        {
            InitializeComponent();
            uecontrol = ue;
            ev = e;
        }

        private void ChooseSeats_Load(object sender, EventArgs e)
        {
            seats = new Seats(ev, uecontrol);
            seats.SetNhib(m_sessionfactory, m_session);
            flowLayoutPanel1.Controls.Add(seats);
            setTextLanguage();
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }
        public void setTextLanguage() {
            this.Text = Properties.Resources.ChooseSeatsWindowName;
            CloseButton.Text = Properties.Resources.CloseButton;
            ReserveButton.Text = Properties.Resources.ReserveButton;
        }
        private void ReserveButton_Click(object sender, EventArgs e)
        {
            if (seats.selectedSeats.Count <= 0)
            {
                MessageBox.Show(Properties.Resources.ErrorSelectSeat);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
