using KinoAplikacija.Entity;
using KinoAplikacija.User_Controls.MainPanels.Normal.Bills;
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

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Checkout
{
    public partial class CheckoutForm : Form
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        public User CurrentUser;
        public List<Reservation> shoppingCart;
        Bill b = new Bill();
        public CheckoutForm(User u, List<Reservation> sc)
        {
            InitializeComponent();
            CurrentUser = u;
            shoppingCart = sc;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void CheckoutForm_Load(object sender, EventArgs e)
        {


            b.OrderDate = DateTime.Now;
            b.Paid = false;
            //gremo čez vse discounte in izberemo največjega
            IList<Discount> all_discounts = m_session.CreateCriteria(typeof(Discount)).List<Discount>();
            b.Discount = null;
            if (CurrentUser.TelephoneNumber == "123")//admin ma zastonj
            {
                b.Discount = all_discounts.First(o => o.Name == "Free Discount");
            }
            if (CurrentUser.Birthday != null)//birthday
            {
                if (b.OrderDate.Date == ((DateTime)CurrentUser.Birthday).Date)
                {
                    b.Discount = all_discounts.First(o => o.Name == "Birthday Discount");
                }
            }
            foreach (Discount d in all_discounts)
            {
                if (b.OrderDate >= d.FromDate && d.ToDate >= b.OrderDate)
                {
                    if (b.Discount != null)
                    {
                        if (b.Discount.Percent < d.Percent)
                        {
                            b.Discount = d;
                        }
                    }
                    else
                    {
                        b.Discount = d;
                    }
                }
            }
            foreach (Reservation r in shoppingCart)
            {//izračunamo ceno
                b.Price += r.Event.Price;
            }
            b.FullPrice = b.Price - b.Price * (b.Discount.Percent / 100);
            foreach (Reservation r in shoppingCart) {
                r.Bill = b;
            }
            setTextLanguage();

            dataGridView1.Columns.Add("EventId", Properties.Resources.ColumnEventId);
            dataGridView1.Columns.Add("MovieTitle", Properties.Resources.ColumnMovieTitle);
            dataGridView1.Columns.Add("Date", Properties.Resources.ColumnDate);
            dataGridView1.Columns.Add("Time", Properties.Resources.ColumnTime);
            dataGridView1.Columns.Add("TheaterAddress", Properties.Resources.ColumnTheaterAddress);
            dataGridView1.Columns.Add("TheaterRoom", Properties.Resources.ColumnTheaterRoom);
            dataGridView1.Columns.Add("NumberOfSeats", Properties.Resources.ColumnNumberOfSeats);
            dataGridView1.Columns.Add("SeatNumbers", Properties.Resources.ColumnSeatNumbers);
            dataGridView1.Columns.Add("PriceForOne", Properties.Resources.ColumnPriceForOne);
            dataGridView1.Columns.Add("TotalPrice", Properties.Resources.ColumnTotalPrice);

            orderDateLabel.Text = b.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            orderDateLabel.BackColor = Color.AliceBlue;
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in shoppingCart)
            {
                if (!uniqueEvents.Contains(r.Event))
                {
                    uniqueEvents.Add(r.Event);
                }
            }
            foreach (Event even in uniqueEvents)
            {
                int counter = 0;
                List<int> seatNumbers = new List<int>();
                foreach (Reservation r in shoppingCart)
                {
                    if (r.Event == even)
                    {
                        seatNumbers.Add(r.SeatNumber);
                        counter++;
                    }
                }
                string s = "";
                foreach (int j in seatNumbers)
                {
                    s += j + " ";
                }

                dataGridView1.Rows.Add(new object[] { even.Id.ToString(), even.Movie.Title, even.Date.ToString("dd MMMM yyyy"), even.Date.ToString("HH:mm:ss")
                                            , even.Room.Theater.Place.Country.Name+", "+  even.Room.Theater.Place.Name+", "+
                                               even.Room.Theater.Address, even.Room.Theater.Name+", "+ even.Room.Name,
                                                counter,s,
                                                 String.Format("{0:0.##}", even.Price)+" €" ,  String.Format("{0:0.##}", (counter*even.Price))+" €"  });

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                counter = 0;
            }
            PriceTextbox.Text = String.Format("{0:0.##}", b.Price) + " €";
            if (b.Discount == null)
            {
                DiscountTextbox.Text = Properties.Resources.DiscountTranslate+": 0%";
            }
            else
            {
                DiscountTextbox.Text = b.Discount.Name + " - " + String.Format("{0:0.##}", b.Discount.Percent) + "%";
            }
            FullPriceTextbox.Text = String.Format("{0:0.##}", b.FullPrice) + " €";
        }
        public void setTextLanguage()
        {
            this.Text = Properties.Resources.CheckoutFormText;
            ConfirmButton.Text = Properties.Resources.ConfirmButton;
            BackButton.Text = Properties.Resources.BackButton;
            PriceAfterDiscountLabel.Text = Properties.Resources.PriceAfterDiscountLabel;
            PriceLabel.Text = Properties.Resources.PriceLabel;
            DiscountLabel.Text = Properties.Resources.DiscountLabel;
            ItemsLabel.Text = Properties.Resources.ItemsLabel;
            BillDateLabel.Text = Properties.Resources.BillDateLabel;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //sedaj ustvarimo račun
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {

                        m_Session.Save(b);

                        tx.Commit();
                        foreach (Reservation r in shoppingCart)
                        {
                            r.Bill = b;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            foreach (Reservation r in shoppingCart) {
                using (ISession m_Session = m_sessionfactory.OpenSession())
                {
                    using (ITransaction tx = m_Session.BeginTransaction())
                    {
                        try
                        {
                            r.Bill = b;
                            m_Session.Save(r);

                            tx.Commit();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }
    }
}
