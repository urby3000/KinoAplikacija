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
    public partial class CountriesControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private IList<Country> _countries;
        private BindingSource _bs;
        private User CurrentUser;
        public CountriesControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void CountriesControl_Load(object sender, EventArgs e)
        {

            _countries = m_session.CreateCriteria(typeof(Country)).List<Country>();
            _bs = new BindingSource();
            _bs.DataSource = _countries;
            _bs.AllowNew = true;
            dataGridView1.DataSource = _bs;
            _bs.ListChanged += new System.ComponentModel.ListChangedEventHandler(bs_ListChanged);
            dataGridView1.Columns["Places"].Visible = false;
        }
        void bs_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        using (ITransaction tx = m_session.BeginTransaction())
                        {
                            try
                            {
                                Country newCountry = (Country)(_bs.List[e.NewIndex]);
                                if (newCountry.Name == null)
                                {
                                    newCountry.Name = "";
                                }
                                if (newCountry.Abbreviation == null)
                                {
                                    newCountry.Abbreviation = "";
                                }
                                m_session.SaveOrUpdate(newCountry);
                                tx.Commit();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                        break;
                    }
            }
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Country newCountry = (Country)(e.Row.DataBoundItem);
                //uporabljaj imena classov in spremenljivk v classu
                IQuery query = m_session.CreateQuery("from Place p where p.Country=" + newCountry.Id);
                List<Place> places = query.List<Place>().ToList();
                if (places.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("If you delete this Country "
                                            + "you are also deleting the Places that are in this Country." +
                                            " Do you want to continue?", "Deleting Country", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Information);

                    if (dr != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            using (ISession m_session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        Country newCountry = (Country)(e.Row.DataBoundItem);
                        m_session.Delete(newCountry);
                        tx.Commit();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
        }
    }
}
