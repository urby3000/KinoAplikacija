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

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Bills
{
    public partial class BillInfoControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private User CurrentUser;
        private Bill bill;
        private BillInfoForm billinfoform;
        public BillInfoControl(User u, Bill b, BillInfoForm bif)
        {
            InitializeComponent();
            CurrentUser = u;
            bill = b;
            billinfoform = bif;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        public void setTextLanguage()
        {
            this.Text = Properties.Resources.BillInfoFormText;
            BillNumberLabel.Text = Properties.Resources.BillNumberLabel;
            BillDateLabel.Text = Properties.Resources.BillDateLabel;
            PayDateInfoLabel.Text = Properties.Resources.PayDateInfoLabel;
            ItemsLabel.Text = Properties.Resources.ItemsLabel;
            DiscountLabel.Text = Properties.Resources.DiscountLabel;
            PriceLabel.Text = Properties.Resources.PriceLabel;
            PriceAfterDiscountLabel.Text = Properties.Resources.PriceAfterDiscountLabel;
            SimulateButton.Text = Properties.Resources.SimulatePaymentButton;
            dataGridView1.Columns[0].HeaderText = Properties.Resources.ColumnEventId;
            dataGridView1.Columns[1].HeaderText = Properties.Resources.ColumnMovieTitle;
            dataGridView1.Columns[2].HeaderText = Properties.Resources.ColumnDate;
            dataGridView1.Columns[3].HeaderText = Properties.Resources.ColumnTime;
            dataGridView1.Columns[4].HeaderText = Properties.Resources.ColumnTheaterAddress;
            dataGridView1.Columns[5].HeaderText = Properties.Resources.ColumnTheaterRoom;
            dataGridView1.Columns[6].HeaderText = Properties.Resources.ColumnNumberOfSeats;
            dataGridView1.Columns[7].HeaderText = Properties.Resources.ColumnSeatNumbers;
            dataGridView1.Columns[8].HeaderText = Properties.Resources.ColumnPriceForOne;
            dataGridView1.Columns[9].HeaderText = Properties.Resources.ColumnTotalPrice;
        }
        private void BillInfoControl_Load(object sender, EventArgs e)
        {

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
            setTextLanguage();

            BillIdLabel.Text = "#" + bill.Id.ToString();
            BillIdLabel.BackColor = Color.AliceBlue;
            orderDateLabel.Text = bill.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            orderDateLabel.BackColor = Color.AliceBlue;
            if (bill.Paid)
            {
                SimulateButton.Visible = false;
                PayDateLabel.Text = ((DateTime)bill.PayDate).ToString("dddd, dd MMMM yyyy HH:mm:ss");
                PayDateLabel.BackColor = Color.Honeydew;
            }
            else
            {

                PayDateLabel.Text = Properties.Resources.paidDateLabelNotPaid;
                PayDateLabel.BackColor = Color.LightCoral;
            }
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in bill.Reservations)
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
                foreach (Reservation r in bill.Reservations)
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
            PriceTextbox.Text = String.Format("{0:0.##}", bill.Price) + " €";
            if (bill.Discount == null)
            {
                DiscountTextbox.Text = "Discount: 0%";
            }
            else
            {
                DiscountTextbox.Text = bill.Discount.Name + " - " + String.Format("{0:0.##}", bill.Discount.Percent) + "%";
            }
            FullPriceTextbox.Text = String.Format("{0:0.##}", bill.FullPrice) + " €";
        }

        private void SimulateButton_Click(object sender, EventArgs e)
        {
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill res = query.SetString(0, bill.Id.ToString()).UniqueResult<Bill>();
                        res.Paid = true;
                        res.PayDate = DateTime.Now;
                        m_Session.Update(res);
                        tx.Commit();
                        PriceTextbox.Text = res.Price.ToString();
                        FullPriceTextbox.Text = res.FullPrice.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            billinfoform.billforuser.ubcontrol.refresh();
            billinfoform.Close();
        }
    }
}
