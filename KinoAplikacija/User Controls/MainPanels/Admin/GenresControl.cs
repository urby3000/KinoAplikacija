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
using System.Xml.Serialization;
using System.IO;

namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    public partial class GenresControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private IList<Genre> _genres;
        public BindingSource _bs;
        User CurrentUser;
        public GenresControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void GenresControl_Load(object sender, EventArgs e)
        {
            resetGrid();
        }
        void resetGrid()
        {

            _genres = m_session.CreateCriteria(typeof(Genre)).List<Genre>();
            _bs = new BindingSource();
            _bs.DataSource = _genres;
            _bs.AllowNew = true;
            dataGridView1.DataSource = _bs;
            _bs.ListChanged += new ListChangedEventHandler(bs_ListChanged);
            dataGridView1.Columns["MoviesGenres"].Visible = false;
        }
        void bs_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        using (ITransaction tx = m_session.BeginTransaction())
                        {
                            Genre newGenre = (Genre)(_bs.List[e.NewIndex]);
                            if (newGenre.Name == null)
                            {
                                newGenre.Name = "";
                            }
                            m_session.Save(newGenre);
                            tx.Commit();
                        }
                        break;
                    }
            }
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            try
            {
                List<MovieGenre> MoviesGenres;
                IQuery query = m_session.CreateQuery("from MovieGenre mg where mg.Genre=" + ((Genre)(e.Row.DataBoundItem)).Id);
                MoviesGenres = query.List<MovieGenre>().ToList();

                foreach (MovieGenre moviegenre in MoviesGenres)//prvo moramo zbrisati iz vmesne tabele
                {
                    using (ISession m_Session = m_sessionfactory.OpenSession())
                    {
                        using (ITransaction tx = m_Session.BeginTransaction())
                        {
                            m_Session.Delete(moviegenre);
                            tx.Commit();
                        }
                    }
                }
                using (ITransaction tx = m_session.BeginTransaction())
                {
                    Genre newGenre = (Genre)(e.Row.DataBoundItem);
                    m_session.Delete(newGenre);
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message);
            }

        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<Genre> genres = null;
                XmlSerializer serializer = new XmlSerializer(typeof(List<Genre>));

                StreamReader reader = new StreamReader(openFileDialog.FileName);
                genres = (List<Genre>)serializer.Deserialize(reader);
                reader.Close();
                List<string> missing = new List<string>();
                //če genre še ne obstaja ga dodam
                foreach (Genre gser in genres)
                {
                    bool exists = false;
                    foreach (Genre g in _genres)
                    {
                        if (gser.Name == g.Name)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        missing.Add(gser.Name);
                    }
                }
                if (missing.Count > 0)
                {
                    foreach (string genreName in missing)
                    {

                        using (ISession m_Session = m_sessionfactory.OpenSession())
                        {
                            using (ITransaction tx = m_Session.BeginTransaction())
                            {
                                try
                                {
                                    Genre g = new Genre();
                                    g.Name = genreName;
                                    m_Session.Save(g);
                                    tx.Commit();
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }

                    resetGrid();
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FileName = "genres";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<Genre>));

                using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                {
                    ser.Serialize(fs, _genres);
                }
            }
        }
    }
}
