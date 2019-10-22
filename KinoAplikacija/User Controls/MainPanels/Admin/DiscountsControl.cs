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
    public partial class DiscountsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private IList<Discount> _discounts;
        private BindingSource _bs;
        private User CurrentUser;
        public DiscountsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void DiscountsControl_Load(object sender, EventArgs e)
        {

            _discounts = m_session.CreateCriteria(typeof(Discount)).List<Discount>();
            _bs = new BindingSource();
            _bs.DataSource = _discounts;
            _bs.AllowNew = true;
            dataGridView1.DataSource = _bs;
            _bs.ListChanged += new ListChangedEventHandler(bs_ListChanged);
            dataGridView1.Columns["Bills"].Visible = false;

        }
        void bs_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        using (ITransaction tx = m_session.BeginTransaction())
                        {
                            try
                            {
                                Discount newDiscount = (Discount)(_bs.List[e.NewIndex]);
                                if (newDiscount.Name == null)
                                {
                                    newDiscount.Name = "";
                                }
                                m_session.SaveOrUpdate(newDiscount);
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
            using (ISession m_session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    try
                    {
                        Discount newDiscount = (Discount)(e.Row.DataBoundItem);
                        m_session.Delete(newDiscount);
                        tx.Commit();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
        }
    }
}
