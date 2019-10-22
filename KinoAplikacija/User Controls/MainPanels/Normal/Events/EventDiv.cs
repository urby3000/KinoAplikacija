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
using KinoAplikacija.User_Controls.MainPanels.Normal.Events;

namespace KinoAplikacija.User_Controls.MainPanels.Normal
{
    public partial class EventDiv : UserControl
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        Event ev;
        UserEventsControl uecontrol;
        public EventDiv(Event e, UserEventsControl uec)
        {
            InitializeComponent();
            ev = e;
            uecontrol = uec;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_SessionFactory = isf;
            m_Session = iss;
        }

        private void EventDiv_Load(object sender, EventArgs e)
        {
            titleLabel.Text = ev.Movie.Title;
            pictureBox1.Load(ev.Movie.ImageSource);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            richTextBox1.Text = ev.Date.ToString("dddd, dd MMMM yyyy");
            richTextBox1.Text +="\n"+ ev.Date.ToString("HH:mm");
            richTextBox1.Text += "\n" + String.Format("{0:0.##}", ev.Price)+" €";
            richTextBox1.Text += "\n" + ev.Room.Theater.Place.Country.Name + ", " + ev.Room.Theater.Place.Name 
                               + "\n" + ev.Room.Theater.Address;
            richTextBox1.Text += "\n" + ev.Room.Theater.Name + ", " + ev.Room.Name;

        }

        public void setTextLanguage()//zaradi datuma...
        {
            richTextBox1.Text = ev.Date.ToString("dddd, dd MMMM yyyy");
            richTextBox1.Text += "\n" + ev.Date.ToString("HH:mm");
            richTextBox1.Text += "\n" + String.Format("{0:0.##}", ev.Price) + " €";
            richTextBox1.Text += "\n" + ev.Room.Theater.Place.Country.Name + ", " + ev.Room.Theater.Place.Name
                               + "\n" + ev.Room.Theater.Address;
            richTextBox1.Text += "\n" + ev.Room.Theater.Name + ", " + ev.Room.Name;
        }
        private void EventDiv_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void EventDiv_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
        }

        private void EventDiv_Click(object sender, EventArgs e)
        {
            ChooseSeats cs = new ChooseSeats(ev,uecontrol);
            cs.SetNhib(m_SessionFactory, m_Session);
            DialogResult dr= cs.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //izbrišemo vse rezervacije od tega userja za ta event 
                uecontrol.shoppingCart.RemoveAll(item => item.Event == ev);
                foreach (Button b in cs.seats.selectedSeats)
                {//dobimo izbrane številke sedežev in naredimo nove rezervacije
                    Reservation r = new Reservation();
                    r.Event = ev;
                    r.SeatNumber = Convert.ToInt32(b.Text);
                    r.User = uecontrol.CurrentUser;
                    uecontrol.shoppingCart.Add(r);
                }
                uecontrol.refreshShoppingCart();
            }
        }
    }
}
