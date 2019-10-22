using KinoAplikacija.Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Bills
{
    public partial class BillInfoForm : Form
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private User CurrentUser;
        private Bill bill;
        public BillForUser billforuser;
        public BillInfoForm(User u, Bill b, BillForUser bfu)
        {
            InitializeComponent();
            CurrentUser = u;
            bill = b;
            billforuser = bfu;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        public void setTextLanguage() {
            this.Text = Properties.Resources.BillInfoFormText;
            foreach (Control c in flowLayoutPanel1.Controls) {
                if (c.GetType() == typeof(BillInfoControl))
                {
                    BillInfoControl bif = (BillInfoControl)c;
                    bif.setTextLanguage();
                }
            }
        }
        private void BillInfoForm_Load(object sender, EventArgs e)
        {
            BillInfoControl bic = new BillInfoControl(CurrentUser, bill,this);
            bic.SetNhib(m_sessionfactory,m_session);
            flowLayoutPanel1.Controls.Add(bic);
            setTextLanguage();
        }

        private void BillInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            billforuser.bifList.Remove(this);
        }
    }
}
