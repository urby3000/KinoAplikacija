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
    public partial class PlacesControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Place> places;
        User CurrentUser;
        public PlacesControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void PlacesControl_Load(object sender, EventArgs e)
        {

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("PostalCode", "PostalCode");
            dataGridView1.Columns.Add("CountryId", "CountryId");
            resetGrid();

        }
        private void resetGrid()
        {


            m_session.Clear();
            places = m_session.CreateCriteria(typeof(Place)).List<Place>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Place p in places)
            {
                dataGridView1.Rows.Add(new object[] { p.Id, p.Name, p.PostalCode, p.Country.Id });
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
            PlaceTextbox.Text = row.Cells[1].Value.ToString();
            PostalCodeTextbox.Text = row.Cells[2].Value.ToString();
            CountryTextbox.Text = row.Cells[3].Value.ToString();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Place p where p.Id=?");
                        Place res = query.SetString(0, IdTextbox.Text).UniqueResult<Place>();
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
            Place p = new Place();

            p.Name = PlaceTextbox.Text;
            p.PostalCode = PostalCodeTextbox.Text;
            try
            {

                IQuery query = m_session.CreateQuery("from Country c where c.Id=?");
                Country res = query.SetString(0, CountryTextbox.Text).UniqueResult<Country>();
                if (res != null)
                {
                    p.Country = res;
                }
                else
                {
                    MessageBox.Show("Country with that ID does NOT exist.");
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

                        m_Session.Save(p);
                        tx.Commit();
                        IdTextbox.Text = p.Id.ToString();
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
            p.Name = PlaceTextbox.Text;
            p.PostalCode = PostalCodeTextbox.Text;


            try
            {
                IQuery query = m_session.CreateQuery("from Country c where c.Id=?");
                Country res = query.SetString(0, CountryTextbox.Text).UniqueResult<Country>();
                if (res != null)
                {
                    p.Country = res;
                }
                else
                {
                    MessageBox.Show("Country with that ID does NOT exist.");
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
                        IQuery query = m_Session.CreateQuery("from Place p where p.Id=?");
                        Place res = query.SetString(0, IdTextbox.Text).UniqueResult<Place>();
                        res.Name = p.Name;
                        res.PostalCode = p.PostalCode;
                        res.Country = p.Country;
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
