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

namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    public partial class ReservationsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Reservation> reservations;
        private IList<User> _users;
        private BindingSource _usersBindingSource;
        private List<Event> events;
        private List<Bill> bills;
        User CurrentUser;
        public ReservationsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void ReservationsControl_Load(object sender, EventArgs e)
        {

            _users = m_session.CreateCriteria(typeof(User)).List<User>();
            _usersBindingSource = new BindingSource();
            _usersBindingSource.DataSource = _users;
            UserGridView.DataSource = _usersBindingSource;
            UserGridView.Columns["PasswordHash"].Visible = false;
            UserGridView.Columns["Birthday"].Visible = false;
            UserGridView.Columns["JoinDate"].Visible = false;
            UserGridView.Columns["Place"].Visible = false;
            UserGridView.Columns["RightsLevel"].Visible = false;
            UserGridView.Columns["Reservations"].Visible = false;
            UserGridView.Columns["PasswordResets"].Visible = false;
            UserGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            EventGridView.Columns.Add("Id", "Id");
            EventGridView.Columns.Add("Price", "Price");
            EventGridView.Columns.Add("Date", "Date");
            EventGridView.Columns.Add("MovieName", "MovieName");
            EventGridView.Columns.Add("TheaterName", "TheaterName");
            EventGridView.Columns.Add("RoomName", "RoomName");
            EventGridViewReset();

            BillGridView.Columns.Add("Id", "Id");
            BillGridView.Columns.Add("OrderDate", "OrderDate");
            BillGridView.Columns.Add("PayDate", "PayDate");
            BillGridView.Columns.Add("Paid", "Paid");
            BillGridView.Columns.Add("Price", "Price");
            BillGridView.Columns.Add("FullPrice", "FullPrice");
            BillGridView.Columns.Add("DiscountId", "DiscountId");
            BillGridViewReset();

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("UserId", "UserId");
            dataGridView1.Columns.Add("EventId", "EventId");
            dataGridView1.Columns.Add("SeatNumber", "SeatNumber");
            dataGridView1.Columns.Add("BillId", "BillId");
            DataGridViewReset();

        }
        private void EventGridViewReset()
        {
            m_session.Clear();
            events = m_session.CreateCriteria(typeof(Event)).List<Event>().ToList();
            EventGridView.Rows.Clear();
            EventGridView.Refresh();
            foreach (Event e in events)
            {
                EventGridView.Rows.Add(new object[] { e.Id, e.Price, e.Date, e.Movie.Title, e.Room.Theater.Name, e.Room.Name });
            }
            EventGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void DataGridViewReset()
        {
            m_session.Clear();
            reservations = m_session.CreateCriteria(typeof(Reservation)).List<Reservation>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Reservation r in reservations)
            {
                dataGridView1.Rows.Add(new object[] { r.Id, r.User.Id, r.Event.Id, r.SeatNumber, r.Bill.Id });
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void BillGridViewReset()
        {
            //bills pokaže samo račune, ki niso povezani z nobeno rezervacijo in z rezervacijo, ki je trenutno povezana
            m_session.Clear();
            bills = m_session.CreateCriteria(typeof(Bill)).List<Bill>().ToList();
            BillGridView.Rows.Clear();
            BillGridView.Refresh();
            foreach (Bill b in bills)
            {
                bool showRow = false;

                if (b.Reservations.Count <= 0)
                {
                    showRow = true;
                }
                foreach (Reservation r in b.Reservations)
                {
                    if (r.Bill == b)
                    {
                        showRow = true;
                        break;
                    }
                    if (r.Bill == null)
                    {
                        showRow = true;
                        break;
                    }
                }
                if (showRow)
                {
                    if (b.Discount == null)
                    {
                        BillGridView.Rows.Add(new object[] { b.Id, b.OrderDate, b.PayDate, b.Paid, b.Price, b.FullPrice, "" });
                    }
                    else
                    {
                        BillGridView.Rows.Add(new object[] { b.Id, b.OrderDate, b.PayDate, b.Paid, b.Price, b.FullPrice, b.Discount.Id });
                    }

                }
            }
            BillGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void UserGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = UserGridView.Rows[rowIndex];
            UserIdTextbox.Text = row.Cells[0].Value.ToString();
        }

        private void EventGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = EventGridView.Rows[rowIndex];
            EventIdTextbox.Text = row.Cells[0].Value.ToString();
            //seat number max je nastavljen reservation->event->room->NumberOfSeats
            foreach (Event ev in events)
            {
                if (ev.Id == (int)row.Cells[0].Value)
                {
                    numericUpDown1.Maximum = ev.Room.NumberOfSeats;
                }
            }
        }

        private void BillGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = BillGridView.Rows[rowIndex];
            BillIdTextbox.Text = row.Cells[0].Value.ToString();

        }

        private void BillGridView_Enter(object sender, EventArgs e)
        {
            BillGridViewReset();
        }

        private void Button1_Click(object sender, EventArgs e)//find empty seat for event
        {
            int seat = 0;
            if (reservations.Count <= 0)
            {
                numericUpDown1.Value = 1;
                return;
            }
            if (string.IsNullOrEmpty(EventIdTextbox.Text))
            {
                MessageBox.Show("Select Event.");
                return;
            }
            List<int> takenSeats = new List<int>();

            IQuery query = m_session.CreateQuery("from Reservation r where r.Event=" + EventIdTextbox.Text);
            List<Reservation> res = query.List<Reservation>().ToList();
            if (res.Count == 0)
            {

            }
            foreach (Reservation reserv in res)
            {
                takenSeats.Add(reserv.SeatNumber);
            }
            if (takenSeats.Count != 0)
            {
                List<int> allSeatNumbers = Enumerable.Range(1, res[0].Event.Room.NumberOfSeats).ToList();
                List<int> freeSeats = allSeatNumbers.Except(takenSeats).ToList();
                if (freeSeats.Count != 0)
                {
                    seat = freeSeats.First();
                }
            }
            else
            {
                seat = 1;
            }
            if (seat == 0)
            {
                MessageBox.Show("No available seats.");
            }
            else
            {
                numericUpDown1.Value = seat;
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(UserIdTextbox.Text) || string.IsNullOrEmpty(BillIdTextbox.Text) || string.IsNullOrEmpty(EventIdTextbox.Text))
            {
                MessageBox.Show("Fill UserId, BillId, EventId");
            }
            //preveri eventid
            Event ev = new Event();
            try
            {
                IQuery event_query = m_session.CreateQuery("from Event e where e.Id=" + EventIdTextbox.Text);
                Event event_res = event_query.UniqueResult<Event>();
                ev = event_res;
                if (event_res == null)
                {
                    MessageBox.Show("Event ne obstaja");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //preveri userid
            User u = new User();
            try
            {
                IQuery user_query = m_session.CreateQuery("from User u where u.Id=" + UserIdTextbox.Text);
                User user_res = user_query.UniqueResult<User>();
                u = user_res;
                if (user_res == null)
                {
                    MessageBox.Show("User ne obstaja");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Bill b = new Bill();
            try//preveri če obstaja bill id, potem pa preveri če je bill že zaseden - ali še nima rezervacije bill, rezervacija isti uporabnik... 
            {
                IQuery bill_query = m_session.CreateQuery("from Bill b where b.Id=" + BillIdTextbox.Text);
                Bill bill_res = bill_query.UniqueResult<Bill>();
                b = bill_res;
                if (bill_res != null)
                {
                    if (bill_res.Reservations.Count > 0)//je zaseden, preveri če je isti uporabnik, 1 uporabnik lahko ima isti račun
                    {
                        foreach (Reservation rese in bill_res.Reservations)
                        {
                            if (rese.User.Id != Convert.ToInt32(UserIdTextbox.Text.ToString()))
                            {

                                MessageBox.Show("UserId mora zaradi računa biti enak.");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("UserId ne obstaja");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //preveri SeatNumber
            List<int> takenSeats = new List<int>();
            IQuery seatnum_query = m_session.CreateQuery("from Reservation r where r.Event=" + EventIdTextbox.Text);
            List<Reservation> seatnum_res = seatnum_query.List<Reservation>().ToList();
            foreach (Reservation reserv in seatnum_res)
            {
                takenSeats.Add(reserv.SeatNumber);
            }
            IQuery event_seats = m_session.CreateQuery("from Event e where e.Id=" + EventIdTextbox.Text);
            Event getEvent = event_seats.UniqueResult<Event>();
            List<int> allSeatNumbers = Enumerable.Range(1, getEvent.Room.NumberOfSeats).ToList();
            List<int> freeSeats = allSeatNumbers.Except(takenSeats).ToList();
            if (!freeSeats.Contains((int)numericUpDown1.Value))
            {
                MessageBox.Show("Seat Number taken.");
                return;
            }
            if (getEvent.Room.NumberOfSeats < (int)numericUpDown1.Value || (int)numericUpDown1.Value <= 0)
            {

                MessageBox.Show("Invalid Seat Number");
            }



            Reservation r = new Reservation();
            r.User = u;
            r.Event = ev;
            r.Bill = b;
            r.SeatNumber = (int)numericUpDown1.Value;
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        m_Session.Save(r);
                        tx.Commit();
                        IdTextbox.Text = r.Id.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //izračunano novo ceno računa
            decimal price = 0;
            decimal fullprice = 0;
            try
            {

                IQuery reservs_query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                List<Reservation> reservs_res = reservs_query.SetString(0, BillIdTextbox.Text).List<Reservation>().ToList();

                if (reservs_res.Count > 0)
                {
                    foreach (Reservation reserv in reservs_res)
                    {
                        price += reserv.Event.Price;
                    }

                }
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            if (price > 0)
            {
                if (b.Discount != null)
                {
                    fullprice = price - price * (b.Discount.Percent / 100);
                }
                else
                {

                    fullprice = price;
                }
            }

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery b_query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill b_res = b_query.SetString(0, BillIdTextbox.Text).UniqueResult<Bill>();
                        b_res.Price = price;
                        b_res.FullPrice = fullprice;
                        m_Session.Update(b_res);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            DataGridViewReset();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            IdTextbox.Text = row.Cells[0].Value.ToString();
            UserIdTextbox.Text = row.Cells[1].Value.ToString();
            EventIdTextbox.Text = row.Cells[2].Value.ToString();
            foreach (Event ev in events)
            {
                if (ev.Id == Convert.ToInt32(EventIdTextbox.Text.ToString()))
                {
                    numericUpDown1.Maximum = ev.Room.NumberOfSeats;
                }
            }
            numericUpDown1.Value = (int)row.Cells[3].Value;
            BillIdTextbox.Text = row.Cells[4].Value.ToString();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Reservation r where r.Id=?");
                        Reservation res = query.SetString(0, IdTextbox.Text).UniqueResult<Reservation>();
                        m_Session.Delete(res);
                        tx.Commit();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            //izračunano novo ceno računa
            decimal price = 0;
            decimal fullprice = 0;
            Bill b = new Bill();
            try
            {
                IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                List<Reservation> res = query.SetString(0, BillIdTextbox.Text).List<Reservation>().ToList();
                b = res[0].Bill;
                if (res.Count > 0)
                {
                    foreach (Reservation reserv in res)
                    {
                        price += reserv.Event.Price;
                    }

                    if (price > 0)
                    {
                        if (b.Discount != null)
                        {
                            fullprice = price - price * (b.Discount.Percent / 100);
                        }
                        else
                        {

                            fullprice = price;
                        }
                    }
                }
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill res = query.SetString(0, BillIdTextbox.Text).UniqueResult<Bill>();
                        res.Price = price;
                        res.FullPrice = fullprice;
                        m_Session.Update(res);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }


            DataGridViewReset();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            //preveri eventid
            Event ev = new Event();
            try
            {
                IQuery query = m_session.CreateQuery("from Event e where e.Id=" + EventIdTextbox.Text);
                Event res = query.UniqueResult<Event>();
                ev = res;
                if (res == null)
                {
                    MessageBox.Show("Event ne obstaja");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //preveri userid
            User u = new User();
            try
            {
                IQuery query = m_session.CreateQuery("from User u where u.Id=" + UserIdTextbox.Text);
                User res = query.UniqueResult<User>();
                u = res;
                if (res == null)
                {
                    MessageBox.Show("User ne obstaja");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Bill b = new Bill();
            try//preveri če obstaja bill id, potem pa preveri če je bill že zaseden - ali še nima rezervacije bill, rezervacija isti uporabnik... 
            {
                IQuery query = m_session.CreateQuery("from Bill b where b.Id=" + BillIdTextbox.Text);
                Bill res = query.UniqueResult<Bill>();
                b = res;
                if (res != null)
                {
                    if (res.Reservations.Count > 0)//je zaseden, preveri če je isti uporabnik, 1 uporabnik lahko ima isti račun
                    {
                        foreach (Reservation rese in res.Reservations)
                        {
                            if (rese.User.Id != Convert.ToInt32(UserIdTextbox.Text.ToString()))
                            {

                                MessageBox.Show("UserId mora zaradi računa biti enak.");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("UserId ne obstaja");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //preveri SeatNumber
            List<int> takenSeats = new List<int>();
            //če je event isti, je ista tudi soba
            try
            {
                IQuery query = m_session.CreateQuery("from Reservation r where r.Event=" + EventIdTextbox.Text);
                List<Reservation> res = query.List<Reservation>().ToList();
                foreach (Reservation reserv in res)
                {
                    takenSeats.Add(reserv.SeatNumber);
                }
                if (takenSeats.Count != 0)
                {
                    List<int> allSeatNumbers = Enumerable.Range(1, res[0].Event.Room.NumberOfSeats).ToList();
                    List<int> freeSeats = allSeatNumbers.Except(takenSeats).ToList();
                    //ko urejamo rezervacijo moramo paziti da lahko uporabimo tudi njen SeatNumber
                    foreach (Reservation reserv in res)
                    {
                        if (reserv.Id == Convert.ToInt32(IdTextbox.Text.ToString()))
                        {
                            freeSeats.Add(reserv.SeatNumber);
                        }
                    }
                    if (res[0].Event.Room.NumberOfSeats < (int)numericUpDown1.Value || (int)numericUpDown1.Value <= 0)
                    {
                        MessageBox.Show("Invalid Seat Number");
                        return;
                    }
                    if (!freeSeats.Contains((int)numericUpDown1.Value))
                    {
                        MessageBox.Show("Seat Number taken.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            Bill oldBill = new Bill();//uporabil bom spodaj
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Reservation r where r.Id=?");
                        Reservation res = query.SetString(0, IdTextbox.Text).UniqueResult<Reservation>();
                        oldBill = res.Bill;
                        res.User = u;
                        res.Event = ev;
                        res.Bill = b;
                        res.SeatNumber = (int)numericUpDown1.Value;
                        m_Session.Update(res);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //izračunamo novo ceno računa
            decimal price = 0;
            decimal fullprice = 0;
            try
            {

                IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                List<Reservation> res = query.SetString(0, BillIdTextbox.Text).List<Reservation>().ToList();

                if (res.Count > 0)
                {
                    foreach (Reservation reserv in res)
                    {
                        price += reserv.Event.Price;
                    }

                }
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            if (price > 0)
            {
                if (b.Discount != null)
                {
                    fullprice = price - price * (b.Discount.Percent / 100);
                }
                else
                {

                    fullprice = price;
                }
            }

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill res = query.SetString(0, BillIdTextbox.Text).UniqueResult<Bill>();
                        res.Price = price;
                        res.FullPrice = fullprice;
                        m_Session.Update(res);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //preverimo če je spremenil bill id, kar pomeni,da moramo spremeniti tudi prejšnji račun
            if (oldBill.Id != Convert.ToInt32(BillIdTextbox.Text.ToString()))
            {
                price = 0;
                fullprice = 0;
                try
                {

                    IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                    List<Reservation> res = query.SetString(0, oldBill.Id.ToString()).List<Reservation>().ToList();

                    if (res.Count > 0)
                    {
                        foreach (Reservation reserv in res)
                        {
                            price += reserv.Event.Price;
                        }

                    }
                }


                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                //oldbill discount

                try
                {

                    IQuery query = m_session.CreateQuery("from Bill b where b.Id=?");
                    Bill res = query.SetString(0, oldBill.Id.ToString()).UniqueResult<Bill>();
                    if (price > 0)
                    {
                        if (res.Discount != null)
                        {
                            fullprice = price - price * (res.Discount.Percent / 100);
                        }
                        else
                        {

                            fullprice = price;
                        }
                    }
                }


                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                using (ISession m_Session = m_sessionfactory.OpenSession())
                {
                    using (ITransaction tx = m_Session.BeginTransaction())
                    {
                        try
                        {
                            IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                            Bill res = query.SetString(0, oldBill.Id.ToString()).UniqueResult<Bill>();
                            res.Price = price;
                            res.FullPrice = fullprice;
                            m_Session.Update(res);
                            tx.Commit();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            DataGridViewReset();
        }
    }
}
