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

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Events
{
    public partial class Seats : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        Event eve;
        UserEventsControl uecontrol;
        public List<Button> selectedSeats = new List<Button>();
        public Seats(Event ev, UserEventsControl uec)
        {
            InitializeComponent();
            eve = ev;
            uecontrol = uec;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void Seats_Load(object sender, EventArgs e)
        {
            //sedaj moram dobiti vse rezervacije za ta Event, torej rezervacije v bazi, 
            List<int> takenSeats= new List<int>();
            List<Reservation> all_reservations = m_session.CreateCriteria(typeof(Reservation)).List<Reservation>().ToList();
            foreach (Reservation r in all_reservations)
            {
                if (r.Event == eve)
                {
                    takenSeats.Add(r.SeatNumber);
                }
            }
            //ko dobimo rezervacije lahko pokažemo sedeže, ki so na voljo
            //prej še dobimo max število sedežev Event->Room.NumberOfSeats
            
            int numberOfSeats = eve.Room.NumberOfSeats;
            for (int i = 1; i < numberOfSeats + 1; i++) {
                if (takenSeats.Contains(i))
                {
                    flowLayoutPanel1.Controls.Add(getButton(false,i.ToString()));
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(getButton(true,i.ToString()));
                }
            }
        }
        Button getButton(bool enabled,string value)
        {
            Button newButton = new Button();
            newButton.Text = value.ToString();
            newButton.Enabled = enabled;
            newButton.Width=50;
            newButton.FlatStyle = FlatStyle.Standard;
            newButton.Click += new EventHandler(ButtonClick);
            newButton.Tag = false;
            newButton.BackColor = Color.White;
            //sedaj še preverimo če je sedež v ShoppingCartu
            foreach (Reservation r in uecontrol.shoppingCart)
            {
                if (r.Event == eve)
                {
                    if (r.SeatNumber.ToString() == value.ToString())
                    {
                        newButton.Tag = true;
                        newButton.BackColor = Color.Gold;
                        selectedSeats.Add(newButton);

                    }
                }
            }

            if (!enabled) {
                newButton.BackColor = Color.Firebrick;
            }
            return newButton;
        }
        protected void ButtonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ((bool)b.Tag)
            {
                selectedSeats.Remove(b);
                b.BackColor = Color.White;
                b.Tag = false;
            }
            else {
                selectedSeats.Add(b);
                b.BackColor = Color.Gold;
                b.Tag = true;
            }
        }
    }
}
