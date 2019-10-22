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
    public partial class TheatersControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Theater> theaters;
        User CurrentUser;
        private IList<Place> _places;
        private BindingSource _placesBindingSource;
        public TheatersControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void TheatersControl_Load(object sender, EventArgs e)
        {

            m_session.Clear();
            _places = m_session.CreateCriteria(typeof(Place)).List<Place>();
            _placesBindingSource = new BindingSource();
            _placesBindingSource.DataSource = _places;
            dataGridView2.DataSource = _placesBindingSource;
            dataGridView2.Columns["Users"].Visible = false;
            dataGridView2.Columns["Theaters"].Visible = false;
            dataGridView2.Columns["PostalCode"].Visible = false;
            dataGridView2.Columns["Country"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Address", "Address");
            dataGridView1.Columns.Add("PlaceId", "PlaceId");
            resetGrid();
        }
        private void resetGrid()
        {
            m_session.Clear();
            theaters = m_session.CreateCriteria(typeof(Theater)).List<Theater>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Theater t in theaters)
            {
                dataGridView1.Rows.Add(new object[] { t.Id, t.Name, t.Address, t.Place.Id });
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
            AddressTextbox.Text = row.Cells[2].Value.ToString();
            PlaceIdTextbox.Text = row.Cells[3].Value.ToString();
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            PlaceIdTextbox.Text = row.Cells[2].Value.ToString();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            Theater t = new Theater();
            t.Name = NameTextbox.Text;
            t.Address = AddressTextbox.Text;
            try
            {

                IQuery query = m_session.CreateQuery("from Place p where p.Id=?");
                Place res = query.SetString(0, PlaceIdTextbox.Text).UniqueResult<Place>();
                if (res != null)
                {
                    t.Place = res;
                }
                else
                {
                    MessageBox.Show("Place with that ID does NOT exist.");
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

                        m_Session.Save(t);
                        tx.Commit();
                        IdTextbox.Text = t.Id.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            resetGrid();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Theater t where t.Id=?");
                        Theater res = query.SetString(0, IdTextbox.Text).UniqueResult<Theater>();
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
            Place p = new Place();
            try
            {

                IQuery query = m_session.CreateQuery("from Place p where p.Id=?");
                Place res = query.SetString(0, PlaceIdTextbox.Text).UniqueResult<Place>();
                if (res != null)
                {
                    p = res;
                }
                else
                {
                    MessageBox.Show("Place with that ID does NOT exist.");
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
                        IQuery query = m_Session.CreateQuery("from Theater t where t.Id=?");
                        Theater res = query.SetString(0, IdTextbox.Text).UniqueResult<Theater>();
                        res.Name = NameTextbox.Text;
                        res.Address = AddressTextbox.Text;
                        res.Place = p;
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
