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
using KinoAplikacija.User_Controls.MainPanels.Normal.Events;
using KinoAplikacija.User_Controls.MainPanels.Normal.Checkout;

namespace KinoAplikacija.User_Controls.MainPanels.Normal
{
    public partial class UserEventsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        public User CurrentUser;
        public List<Reservation> shoppingCart;
        public Bill userBill;
        public UserEventsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
            shoppingCart = new List<Reservation>();
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }
        public void refreshShoppingCart()
        {
            ShoppingCartFlowPanel.Controls.Clear();
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in shoppingCart)
            {
                if (!uniqueEvents.Contains(r.Event)) {
                    uniqueEvents.Add(r.Event);
                }
                
            }
            foreach (Event even in uniqueEvents)
            {
                foreach (Reservation r in shoppingCart)
                {
                    if (r.Event == even)
                    {
                        ShoppingItem si = new ShoppingItem(this, r.Event);
                        si.SetNhib(m_sessionfactory, m_session);
                        ShoppingCartFlowPanel.Controls.Add(si);
                        break;
                    }
                }

            }
            decimal price = new decimal();
            foreach (Reservation r in shoppingCart) {
                price += r.Event.Price;
            }
            textBox1.Text= String.Format("{0:0.##}", price) + " €"; 

        }
        private void UserEventsControl_Load(object sender, EventArgs e)
        {
            List<Event> events = m_session.CreateCriteria(typeof(Event)).List<Event>().ToList();
            foreach (Event ev in events)
            {
                EventDiv ed = new EventDiv(ev, this);
                ed.SetNhib(m_sessionfactory, m_session);
                EventFlowPanel.Controls.Add(ed);
            }
            setTextLanguage();
        }
        public void setTextLanguage() {
            EventsLabel.Text = Properties.Resources.EventsLabel;
            ShoppingCartLabel.Text = Properties.Resources.ShoppingCartLabel;
            CheckoutButton.Text = Properties.Resources.CheckoutButton;
            foreach (Control c in EventFlowPanel.Controls)
            {

                if (c.GetType() == typeof(EventDiv))
                {
                    EventDiv ed = (EventDiv)c;
                    ed.setTextLanguage();
                }
            }
            foreach (Control c in ShoppingCartFlowPanel.Controls)
            {

                if (c.GetType() == typeof(ShoppingItem))
                {
                    ShoppingItem si = (ShoppingItem)c;
                    si.setTextLanguage();
                }
            }
        }
        private void CheckoutButton_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count <= 0) {
                return;
            }
            CheckoutForm cf = new CheckoutForm(CurrentUser,shoppingCart);
            cf.SetNhib(m_sessionfactory, m_session);
            DialogResult dr = cf.ShowDialog();
            if (dr == DialogResult.OK)
            {

                shoppingCart.Clear();
                refreshShoppingCart();
            }
            
        }
    }
}
