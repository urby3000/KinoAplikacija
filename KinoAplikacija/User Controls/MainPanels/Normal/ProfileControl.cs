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
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace KinoAplikacija.User_Controls.MainPanels.Normal
{
    public partial class ProfileControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private IList<Country> _countries;
        private BindingSource _countriesBindingSource;
        private IList<Place> _places;
        private BindingSource _placesBindingSource;
        User CurrentUser;
        Place selectedPlace;
        HomeForm home;
        UserSidePanel usersidep;
        public ProfileControl(User u, UserSidePanel usp, HomeForm h)
        {
            InitializeComponent();

            CurrentUser = u;
            home = h;
            usersidep = usp;

        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private bool checkEmail(string s)
        {
            Regex rEMail = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (s.Length > 0)

            {

                if (!rEMail.IsMatch(s))

                {
                    return false;
                }

            }
            return true;
        }
        private bool checkIfNumberOrString(string s)//true number, false string
        {
            return int.TryParse(s, out int n);
        }

        private void TelephoneText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            e.Handled = true;
        }
        private void StringOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private bool IsTextBoxEmpty(TextBox textbox)
        {
            if (textbox.Text != "")
            {
                return false;
            }
            ToolTip tt = new ToolTip();
            tt.ToolTipIcon = ToolTipIcon.Error;
            tt.Show("Please fill out.", textbox, 0, 0, 1000);
            return true;
        }
        public void setTextLanguage()
        {
            ProfileLabel.Text = Properties.Resources.ProfileLabel;
            NameLabel.Text = Properties.Resources.NameLabel;
            SurnameLabel.Text = Properties.Resources.SurnameLabel;
            AddressLabel.Text = Properties.Resources.AddressLabel;
            EmailLabel.Text = Properties.Resources.EmailLabel;
            TelephoneNumberLabel.Text = Properties.Resources.TelephoneNumberLabel;
            BirthdayLabel.Text = Properties.Resources.BirthdayLabel;
            NewPasswordLabel.Text = Properties.Resources.NewPasswordLabel;
            SaveButton.Text = Properties.Resources.SaveButton;
            PasswordChangeButton.Text = Properties.Resources.PasswordChangeButton;


        }

        private void ProfileControl_Load(object sender, EventArgs e)
        {
            _countries = m_session.CreateCriteria(typeof(Country)).List<Country>();
            _countriesBindingSource = new BindingSource();
            _countriesBindingSource.DataSource = _countries;
            CountriesComboBox.DataSource = _countriesBindingSource;
            CountriesComboBox.DisplayMember = "Name";
            CountriesComboBox.ValueMember = "Name";



            _places = m_session.CreateCriteria(typeof(Place)).List<Place>();
            _placesBindingSource = new BindingSource();
            _placesBindingSource.DataSource = _places;
            PlacesGridView.DataSource = _placesBindingSource;
            PlacesGridView.Columns["Users"].Visible = false;
            PlacesGridView.Columns["Theaters"].Visible = false;
            PlacesGridView.Columns["PostalCode"].Visible = false;
            PlacesGridView.Columns["Id"].Visible = false;
            PlacesGridView.Columns["Country"].Visible = false;
            PlacesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            PlacesGridView.ColumnHeadersVisible = false;

            if (_countries.Count > 0)
            {
                CountriesComboBox.SelectedIndex = CountriesComboBox.FindStringExact(_countries[0].Name);
                Country country = _countries[0];
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[PlacesGridView.DataSource];
                currencyManager1.SuspendBinding();
                foreach (DataGridViewRow row in PlacesGridView.Rows)
                {
                    if (((Place)row.DataBoundItem).Country.Id == country.Id)
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



            setTextLanguage();




            NameText.Text = CurrentUser.Name;
            LastNameText.Text = CurrentUser.Surname;
            EmailText.Text = CurrentUser.Email;
            TelephoneText.Text = CurrentUser.TelephoneNumber;
            if (CurrentUser.Birthday == null)
            {
                BirthdayPicker.Enabled = false;
                birthdayCheckbox.Checked = false;
            }
            else
            {
                BirthdayPicker.Value = (DateTime)CurrentUser.Birthday;
                birthdayCheckbox.Checked = true;
            }
            AddressTextbox.Text = CurrentUser.Address;
            if (CurrentUser.Place == null)
            {
                PlaceCheckbox.Checked = false;

            }
            else
            {
                PlaceCheckbox.Checked = true;
                PlaceTextbox.Text = CurrentUser.Place.PostalCode + " " + CurrentUser.Place.Name;
                selectedPlace = CurrentUser.Place;
            }


        }

        private void CountriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            Country country = (Country)CountriesComboBox.SelectedItem;
            _places = m_session.CreateCriteria(typeof(Place)).List<Place>();
            _placesBindingSource = new BindingSource();
            _placesBindingSource.DataSource = _places;
            PlacesGridView.DataSource = _placesBindingSource;
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[PlacesGridView.DataSource];
            currencyManager1.SuspendBinding();
            foreach (DataGridViewRow row in PlacesGridView.Rows)
            {
                if (((Place)row.DataBoundItem).Country.Id == country.Id)
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

        private void PlacesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PlaceCheckbox.Checked)
            {
                if (PlacesGridView.CurrentRow == null)
                {
                    return;
                }
                Place p = ((Place)PlacesGridView.CurrentRow.DataBoundItem);
                selectedPlace = p;
                PlaceTextbox.Text = p.PostalCode + " " + p.Name;
            }
            else
            {
                PlaceTextbox.Text = "";
                selectedPlace = null;
            }
        }

        private void BirthdayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            BirthdayPicker.Enabled = birthdayCheckbox.Checked;
        }

        private void PlaceCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (PlaceCheckbox.Checked)
            {
                if (PlacesGridView.CurrentRow == null)
                {
                    return;
                }
                Place p = ((Place)PlacesGridView.CurrentRow.DataBoundItem);
                selectedPlace = p;
                PlaceTextbox.Text = p.PostalCode + " " + p.Name;
            }
            else
            {
                PlaceTextbox.Text = "";
                selectedPlace = null;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool signup = true;
            if ((IsTextBoxEmpty(NameText) || IsTextBoxEmpty(LastNameText) || IsTextBoxEmpty(EmailText) || IsTextBoxEmpty(TelephoneText)))
            {
                signup = false;
            }
            if (!checkEmail(EmailText.Text))
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.Show(Properties.Resources.ErrorEmailFormat, EmailText, 0, 0, 1000);
                signup = false;
            }
            if (!checkIfNumberOrString(TelephoneText.Text))
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.Show(Properties.Resources.ErrorTelephoneNumberFormat, TelephoneText, 0, 0, 1000);
                signup = false;
            }
            if (!signup)
            {
                return;
            }

            try
            {
                IQuery query = m_session.CreateQuery("from User u where u.Id<>" + CurrentUser.Id.ToString() + " AND (u.Email='" + EmailText.Text + "' OR u.TelephoneNumber=" + TelephoneText.Text + ")");
                IList<User> res = query.List<User>();

                if (res.Count > 0)
                {
                    MessageBox.Show(Properties.Resources.ErrorEmailTelephoneTaken);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Place p = new Place();
            if (!string.IsNullOrEmpty(PlaceTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Place p where p.Id=?");
                    Place res = query.SetString(0, selectedPlace.Id.ToString()).UniqueResult<Place>();
                    if (res != null)
                    {
                        p = res;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ErrorPlace);
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
                        User res = query.SetString(0, CurrentUser.Id.ToString()).UniqueResult<User>();
                        res.Name = NameText.Text;
                        res.Surname = LastNameText.Text;
                        res.Email = EmailText.Text;
                        res.Address = AddressTextbox.Text;
                        if (string.IsNullOrEmpty(PlaceTextbox.Text))
                        {
                            res.Place = null;
                        }
                        else
                        {
                            res.Place = p;
                        }
                        res.TelephoneNumber = TelephoneText.Text;
                        if (birthdayCheckbox.Checked)
                        {
                            res.Birthday = BirthdayPicker.Value;
                        }
                        else
                        {
                            res.Birthday = null;
                        }
                        m_Session.Update(res);
                        tx.Commit();
                        home.CurrentUser = res;
                        usersidep.CurrentUser = res;

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void PasswordChangeButton_Click(object sender, EventArgs e)
        {
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from User u where u.Id=?");
                        User res = query.SetString(0, CurrentUser.Id.ToString()).UniqueResult<User>();
                        res.PasswordHash = GetMd5Hash(PasswordTextbox.Text.ToString());
                        m_Session.Update(res);
                        tx.Commit();
                        home.CurrentUser = res;
                        usersidep.CurrentUser = res;

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
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
    }
}
