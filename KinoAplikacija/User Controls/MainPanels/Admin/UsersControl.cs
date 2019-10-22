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
using System.Security.Cryptography;

namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    public partial class UsersControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<User> users;
        User CurrentUser;
        private IList<Place> _places;
        private BindingSource _placesBindingSource;
        public UsersControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void UsersControl_Load(object sender, EventArgs e)
        {
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
            dataGridView1.Columns.Add("Surname", "Surname");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("TelephoneNumber", "TelephoneNumber");
            dataGridView1.Columns.Add("JoinDate", "JoinDate");
            dataGridView1.Columns.Add("Birthday", "Birthday");
            dataGridView1.Columns.Add("Address", "Address");
            dataGridView1.Columns.Add("PlaceId", "PlaceId");
            dataGridView1.Columns.Add("PasswordHash", "PasswordHash");
            dataGridView1.Columns.Add("RightsLevel", "RightsLevel");
            resetGrid();
        }
        private void resetGrid()
        {

            m_session.Clear();

            users = m_session.CreateCriteria(typeof(User)).List<User>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (User u in users)
            {
                if (u.Place == null)
                {
                    dataGridView1.Rows.Add(new object[] { u.Id,u.Name,u.Surname,u.Email,u.TelephoneNumber,
                                                u.JoinDate,u.Birthday,u.Address,"",u.PasswordHash,u.RightsLevel});
                }
                else
                {
                    dataGridView1.Rows.Add(new object[] { u.Id,u.Name,u.Surname,u.Email,u.TelephoneNumber,
                                                u.JoinDate,u.Birthday,u.Address,u.Place.Id,u.PasswordHash,u.RightsLevel});
                }
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
            NameTexbox.Text = row.Cells[1].Value.ToString();
            SurnameTextbox.Text = row.Cells[2].Value.ToString();
            EmailTextbox.Text = row.Cells[3].Value.ToString();
            TelephoneTextbox.Text = row.Cells[4].Value.ToString();
            joinDatePicker.Value = DateTime.Parse(row.Cells[5].Value.ToString());
            if (row.Cells[6].Value != null) {
                BirthdayPicker.Value = DateTime.Parse(row.Cells[6].Value.ToString());
            }
            if (row.Cells[7].Value != null)
            {
                AddressTextbox.Text = row.Cells[7].Value.ToString();
            }
            PlaceIdTextbox.Text = row.Cells[8].Value.ToString();
            PasswordTextbox.Text = row.Cells[9].Value.ToString();
            RightsTextbox.Text = row.Cells[10].Value.ToString();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                IQuery query = m_session.CreateQuery("from User u where (u.Email='" + EmailTextbox.Text + "' OR u.TelephoneNumber=" + TelephoneTextbox.Text + ")");
                IList<User> res = query.List<User>();
                if (res.Count > 0)
                {
                    MessageBox.Show("Email or Telephone number already exists.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            User u = new User();
            u.Name = NameTexbox.Text;
            u.Surname = SurnameTextbox.Text;
            u.Email = EmailTextbox.Text;
            u.Address = AddressTextbox.Text;
            u.TelephoneNumber = TelephoneTextbox.Text;
            u.JoinDate = joinDatePicker.Value;
            u.Birthday = BirthdayPicker.Value;
            u.PasswordHash = PasswordTextbox.Text;
            u.RightsLevel = RightsTextbox.Text;
            if (!string.IsNullOrEmpty(PlaceIdTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Place p where p.Id=?");
                    Place res = query.SetString(0, PlaceIdTextbox.Text).UniqueResult<Place>();
                    if (res != null)
                    {
                        u.Place = res;
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
            }


            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {

                        m_Session.Save(u);
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
                IQuery query = m_session.CreateQuery("from User u where u.Id<>" + IdTextbox.Text + " AND (u.Email='" + EmailTextbox.Text + "' OR u.TelephoneNumber=" + TelephoneTextbox.Text + ")");
                IList<User> res = query.List<User>();
                if (res.Count > 0)
                {
                    MessageBox.Show("Email or Telephone number already exists.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (!string.IsNullOrEmpty(PlaceIdTextbox.Text))
            {
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
            }
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from User u where u.Id=?");
                        User res = query.SetString(0, IdTextbox.Text).UniqueResult<User>();
                        res.Name = NameTexbox.Text;
                        res.Surname = SurnameTextbox.Text;
                        res.Email = EmailTextbox.Text;
                        res.Address = AddressTextbox.Text;
                        if (string.IsNullOrEmpty(PlaceIdTextbox.Text))
                        {
                            res.Place = null;
                        }
                        else
                        {
                            res.Place = p;
                        }
                        res.TelephoneNumber = TelephoneTextbox.Text;
                        res.JoinDate = joinDatePicker.Value;
                        res.Birthday = BirthdayPicker.Value;
                        res.PasswordHash = PasswordTextbox.Text;
                        res.RightsLevel = RightsTextbox.Text;
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        if (CurrentUser.Id == Int32.Parse(IdTextbox.Text))
                        {
                            MessageBox.Show("Don't be silly.");
                            return;
                        }
                        IQuery query = m_Session.CreateQuery("from User u where u.Id=?");
                        User res = query.SetString(0, IdTextbox.Text).UniqueResult<User>();
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
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            PasswordTextbox.Text = GetMd5Hash(PasswordTextbox.Text);
        }
        static string GetMd5Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
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
    }
}
