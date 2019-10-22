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
    public partial class RoomsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Room> rooms;
        User CurrentUser;
        private IList<Theater> _theaters;
        private BindingSource _theaterBindingSource;
        public RoomsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }

        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }
        private void RoomsControl_Load(object sender, EventArgs e)
        {

            m_session.Clear();
            _theaters = m_session.CreateCriteria(typeof(Theater)).List<Theater>();
            _theaterBindingSource = new BindingSource();
            _theaterBindingSource.DataSource = _theaters;
            dataGridView2.DataSource = _theaterBindingSource;
            dataGridView2.Columns["Rooms"].Visible = false;
            dataGridView2.Columns["Address"].Visible = false;
            dataGridView2.Columns["Place"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("NumberOfSeats", "NumberOfSeats");
            dataGridView1.Columns.Add("TheaterId", "TheaterId");
            resetGrid();
        }

        private void resetGrid()
        {
            m_session.Clear();
            rooms = m_session.CreateCriteria(typeof(Room)).List<Room>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Room r in rooms)
            {
                dataGridView1.Rows.Add(new object[] { r.Id, r.Name, r.NumberOfSeats, r.Theater.Id });
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
            NameTextbox.Text = row.Cells[1].Value.ToString();
            NumOfSeatsTextbox.Text = row.Cells[2].Value.ToString();
            TheaterIdTextbox.Text = row.Cells[3].Value.ToString();
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            TheaterIdTextbox.Text = row.Cells[0].Value.ToString();
        }

        private void DataGridView2_Enter(object sender, EventArgs e)
        {

            _theaters = m_session.CreateCriteria(typeof(Theater)).List<Theater>();
            _theaterBindingSource = new BindingSource();
            _theaterBindingSource.DataSource = _theaters;
            dataGridView2.DataSource = _theaterBindingSource;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            Room r = new Room();
            r.Name = NameTextbox.Text;
            r.NumberOfSeats = int.Parse(NumOfSeatsTextbox.Text);
            try
            {

                IQuery query = m_session.CreateQuery("from Theater t where t.Id=?");
                Theater res = query.SetString(0, TheaterIdTextbox.Text).UniqueResult<Theater>();
                if (res != null)
                {
                    r.Theater = res;
                }
                else
                {
                    MessageBox.Show("Theater with that ID does NOT exist.");
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
            resetGrid();

        }

        private void NumOfSeatsTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
                        IQuery query = m_Session.CreateQuery("from Room r where r.Id=?");
                        Room res = query.SetString(0, IdTextbox.Text).UniqueResult<Room>();
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

        private void EditButton_Click(object sender, EventArgs e)
        {

            Theater t = new Theater();
                    try
                    {

                        IQuery query = m_session.CreateQuery("from Theater t where t.Id=?");
                        Theater res = query.SetString(0, TheaterIdTextbox.Text).UniqueResult<Theater>();
                        if (res != null)
                        {
                            t = res;
                        }
                        else
                        {
                            MessageBox.Show("Theater with that ID does NOT exist.");
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
                        IQuery query = m_Session.CreateQuery("from Room r where r.Id=?");
                        Room res = query.SetString(0, IdTextbox.Text).UniqueResult<Room>();
                        res.Name = NameTextbox.Text;
                        res.NumberOfSeats = int.Parse(NumOfSeatsTextbox.Text);
                        res.Theater = t;
                        m_Session.Update(res);
                        tx.Commit();
                        IdTextbox.Text = res.Id.ToString();
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
