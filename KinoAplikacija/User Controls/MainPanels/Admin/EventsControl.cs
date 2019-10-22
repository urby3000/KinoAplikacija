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

namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    public partial class EventsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Event> events;
        private IList<Room> _rooms;
        private BindingSource _roomsBindingSource;
        private IList<Movie> _movies;
        private BindingSource _moviesBindingSource;
        private IList<Theater> _theaters;
        private BindingSource _theatersBindingSource;
        User CurrentUser;
        public EventsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void EventsControl_Load(object sender, EventArgs e)
        {
            _theaters = m_session.CreateCriteria(typeof(Theater)).List<Theater>();
            _theatersBindingSource = new BindingSource();
            _theatersBindingSource.DataSource = _theaters;
            TheatersComboBox.DataSource = _theatersBindingSource;
            TheatersComboBox.DisplayMember = "Name";
            TheatersComboBox.ValueMember = "Name";

            _rooms = m_session.CreateCriteria(typeof(Room)).List<Room>();
            _roomsBindingSource = new BindingSource();
            _roomsBindingSource.DataSource = _rooms;
            RoomGridView.DataSource = _roomsBindingSource;
            RoomGridView.Columns["Events"].Visible = false;
            RoomGridView.Columns["NumberOfSeats"].Visible = false;
            RoomGridView.Columns["Theater"].Visible = false;
            RoomGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            _movies = m_session.CreateCriteria(typeof(Movie)).List<Movie>();
            _moviesBindingSource = new BindingSource();
            _moviesBindingSource.DataSource = _movies;
            MovieGridView.DataSource = _moviesBindingSource;
            MovieGridView.Columns["MoviesGenres"].Visible = false;
            MovieGridView.Columns["Events"].Visible = false;
            MovieGridView.Columns["Description"].Visible = false;
            MovieGridView.Columns["ImageSource"].Visible = false;
            MovieGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Date", "Date");
            dataGridView1.Columns.Add("MovieId", "MovieId");
            dataGridView1.Columns.Add("RoomId", "RoomId");
            resetGrid();

        }
        private void resetGrid()
        {

            m_session.Clear();

            events = m_session.CreateCriteria(typeof(Event)).List<Event>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Event e in events)
            {
                dataGridView1.Rows.Add(new object[] { e.Id, e.Price, e.Date, e.Movie.Id, e.Room.Id });
            }
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
            PriceTextbox.Text = row.Cells[1].Value.ToString();
            DatePicker.Value = DateTime.Parse(row.Cells[2].Value.ToString());
            MovieIdTextbox.Text = row.Cells[3].Value.ToString();
            RoomIdTextbox.Text = row.Cells[4].Value.ToString();
        }
        private void MovieGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = MovieGridView.Rows[rowIndex];
            MovieIdTextbox.Text = row.Cells[2].Value.ToString();
        }

        private void RoomGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = RoomGridView.Rows[rowIndex];
            RoomIdTextbox.Text = row.Cells[1].Value.ToString();
        }

        private void TheatersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Theater theater = (Theater)TheatersComboBox.SelectedItem;
            _rooms = m_session.CreateCriteria(typeof(Room)).List<Room>();
            _roomsBindingSource = new BindingSource();
            _roomsBindingSource.DataSource = _rooms;
            RoomGridView.DataSource = _roomsBindingSource;
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[RoomGridView.DataSource];
            currencyManager1.SuspendBinding();
            foreach (DataGridViewRow row in RoomGridView.Rows)
            {
                if (((Room)row.DataBoundItem).Theater.Id == theater.Id)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
            currencyManager1.ResumeBinding();
        }

        private void PriceTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44 && PriceTextbox.Text.IndexOf(',') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Event e where e.Id=?");
                        Event res = query.SetString(0, IdTextbox.Text).UniqueResult<Event>();
                        m_Session.Delete(res);
                        tx.Commit();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            resetGrid();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Event ev = new Event();
            ev.Date = DatePicker.Value;
            ev.Price = Convert.ToDecimal(PriceTextbox.Text);
            if (!string.IsNullOrEmpty(MovieIdTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Movie m where m.Id=?");
                    Movie res = query.SetString(0, MovieIdTextbox.Text).UniqueResult<Movie>();
                    if (res != null)
                    {
                        ev.Movie = res;
                    }
                    else
                    {
                        MessageBox.Show("Movie with that ID does NOT exist.");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            if (!string.IsNullOrEmpty(RoomIdTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Room r where r.Id=?");
                    Room res = query.SetString(0, RoomIdTextbox.Text).UniqueResult<Room>();
                    if (res != null)
                    {
                        ev.Room = res;
                    }
                    else
                    {
                        MessageBox.Show("Room with that ID does NOT exist.");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {

                        m_Session.Save(ev);
                        tx.Commit();
                        IdTextbox.Text = ev.Id.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            resetGrid();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MovieIdTextbox.Text) || string.IsNullOrEmpty(RoomIdTextbox.Text))
            {
                return;
            }
            Movie m = new Movie();
            try
            {

                IQuery query = m_session.CreateQuery("from Movie m where m.Id=?");
                Movie res = query.SetString(0, MovieIdTextbox.Text).UniqueResult<Movie>();
                if (res != null)
                {
                    m = res;
                }
                else
                {
                    MessageBox.Show("Movie with that ID does NOT exist.");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            Room r = new Room();

            try
            {

                IQuery query = m_session.CreateQuery("from Room r where r.Id=?");
                Room res = query.SetString(0, RoomIdTextbox.Text).UniqueResult<Room>();
                if (res != null)
                {
                    r = res;
                }
                else
                {
                    MessageBox.Show("Room with that ID does NOT exist.");
                    return;
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
                        IQuery query = m_Session.CreateQuery("from Event e where e.Id=?");
                        Event res = query.SetString(0, IdTextbox.Text).UniqueResult<Event>();
                        res.Date = DatePicker.Value;
                        res.Price = Convert.ToDecimal(PriceTextbox.Text);
                        res.Movie = m;
                        res.Room = r;
                        m_Session.Update(res);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            resetGrid();
        }
    }
}
