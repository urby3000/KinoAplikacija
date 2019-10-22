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

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Events
{
    public partial class ShoppingItem : UserControl
    {
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        public Event eve;
        public UserEventsControl uecontrol;
        List<Reservation> rs = new List<Reservation>();
        public ShoppingItem(UserEventsControl uec,Event ev)
        {
            InitializeComponent();
            eve = ev;
            uecontrol = uec;
        }

        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_SessionFactory = isf;
            m_Session = iss;
        }
        private void ShoppingItem_Load(object sender, EventArgs e)
        {
            decimal price = new decimal();
            foreach (Reservation r in uecontrol.shoppingCart) {
                if (r.Event == eve) {
                    rs.Add(r);
                    price += eve.Price;
                }
            }
            label1.Text = rs.Count.ToString() + "x " + eve.Movie.Title;
            label2.Text=    String.Format("{0:0.##}", price) + " €";
            setTextLanguage();
        }
        public void setTextLanguage() {
            RemoveButton.Text = Properties.Resources.RemoveButton;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            uecontrol.shoppingCart.RemoveAll(item => item.Event == eve);
            uecontrol.refreshShoppingCart();
        }
    }
}
