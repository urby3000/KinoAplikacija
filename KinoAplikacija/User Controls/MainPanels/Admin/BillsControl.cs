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
    public partial class BillsControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private IList<Discount> _discounts;
        private BindingSource _bs;
        private User CurrentUser;
        private List<Bill> bills;
        public BillsControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void BillsControl_Load(object sender, EventArgs e)
        {

            _discounts = m_session.CreateCriteria(typeof(Discount)).List<Discount>();
            _bs = new BindingSource();
            _bs.DataSource = _discounts;
            DiscountGridView.DataSource = _bs;
            DiscountGridView.Columns["Bills"].Visible = false;
            DiscountGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("OrderDate", "OrderDate");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("FullPrice", "FullPrice");
            dataGridView1.Columns.Add("PayDate", "PayDate");
            dataGridView1.Columns.Add("Paid", "Paid");
            dataGridView1.Columns.Add("DiscountId", "DiscountId");
            resetGrid();
        }
        private void resetGrid()
        {

            m_session.Clear();

            bills = m_session.CreateCriteria(typeof(Bill)).List<Bill>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Bill b in bills)
            {
                if (b.Discount == null)
                {
                    dataGridView1.Rows.Add(new object[] { b.Id, b.OrderDate, b.Price, b.FullPrice, b.PayDate, b.Paid, "" });
                }
                else
                {
                    dataGridView1.Rows.Add(new object[] { b.Id, b.OrderDate, b.Price, b.FullPrice, b.PayDate, b.Paid, b.Discount.Id });
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void DiscountGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = DiscountGridView.Rows[rowIndex];
            DiscountIdTextbox.Text = row.Cells[1].Value.ToString();
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
            OrderDatePicker.Value = DateTime.Parse(row.Cells[1].Value.ToString());
            PriceTextbox.Text = row.Cells[2].Value.ToString();
            FullPriceTextbox.Text = row.Cells[3].Value.ToString();
            if (row.Cells[4].Value != null)
            {
                PayDatePicker.Value = DateTime.Parse(row.Cells[4].Value.ToString());
            }
            PaidCheckbox.Checked = (bool)row.Cells[5].Value;
            DiscountIdTextbox.Text = row.Cells[6].Value.ToString();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Bill b = new Bill();
            b.OrderDate = OrderDatePicker.Value;
            b.Paid = PaidCheckbox.Checked;
            if (PaidCheckbox.Checked)
            {
                b.PayDate = PayDatePicker.Value;
            }
            if (!string.IsNullOrEmpty(DiscountIdTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Discount d where d.Id=?");
                    Discount res = query.SetString(0, DiscountIdTextbox.Text).UniqueResult<Discount>();
                    if (res != null)
                    {
                        b.Discount = res;
                    }
                    else
                    {
                        MessageBox.Show("Discount with that ID does NOT exist.");
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

                        m_Session.Save(b);
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

            Bill b = new Bill();


            if (!string.IsNullOrEmpty(DiscountIdTextbox.Text))
            {
                try
                {

                    IQuery query = m_session.CreateQuery("from Discount d where d.Id=?");
                    Discount res = query.SetString(0, DiscountIdTextbox.Text).UniqueResult<Discount>();
                    if (res != null)
                    {
                        b.Discount = res;
                    }
                    else
                    {
                        MessageBox.Show("Discount with that ID does NOT exist.");
                        return;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            //izračunamo  ceno
            try
            {

                IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                List<Reservation> res = query.SetString(0, IdTextbox.Text).List<Reservation>().ToList();

                if (res.Count > 0)
                {
                    foreach (Reservation r in res)
                    {
                        if (r.Bill.Id == Convert.ToInt32(IdTextbox.Text.ToString()))
                        {
                            b.Price += r.Event.Price;
                        }
                    }

                }
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            if (b.Price > 0)
            {
                if (b.Discount != null)
                {
                    b.FullPrice = b.Price - b.Price * (b.Discount.Percent / 100);
                }
                else
                {

                    b.FullPrice = b.Price;
                }
            }
            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill res = query.SetString(0, IdTextbox.Text).UniqueResult<Bill>();
                        res.OrderDate = OrderDatePicker.Value;
                        res.Paid = PaidCheckbox.Checked;
                        res.Discount = b.Discount;
                        res.Price = b.Price;
                        res.FullPrice = b.FullPrice;
                        if (PaidCheckbox.Checked)
                        {
                            res.PayDate = PayDatePicker.Value;
                        }
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
            resetGrid();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=?");
                List<Reservation> res = query.SetString(0, IdTextbox.Text).List<Reservation>().ToList();
                if (res.Count > 0)
                {
                    MessageBox.Show("Can't delete Bill because it is still connected with Reservation.");
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
                        IQuery query = m_Session.CreateQuery("from Bill b where b.Id=?");
                        Bill res = query.SetString(0, IdTextbox.Text).UniqueResult<Bill>();
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

        private void DiscountGridView_Enter(object sender, EventArgs e)
        {


            _discounts = m_session.CreateCriteria(typeof(Discount)).List<Discount>();
            _bs = new BindingSource();
            _bs.DataSource = _discounts;
            DiscountGridView.DataSource = _bs;
            DiscountGridView.Columns["Bills"].Visible = false;
            DiscountGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void DataGridView1_Enter(object sender, EventArgs e)
        {
            resetGrid();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                BillPDFExportForm pdfexportform = new BillPDFExportForm(folderBrowserDialog1.SelectedPath);
                pdfexportform.SetNhib(m_sessionfactory,m_session);
                pdfexportform.Show();
            }
        }
    }
}
