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
    public partial class MoviesControl : UserControl
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private List<Movie> movies;
        private User CurrentUser;
        private BindingSource GenresBindingSource;
        private IList<Genre> _genres;
        public MoviesControl(User u)
        {
            InitializeComponent();
            CurrentUser = u;
        }

        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }
        private void MoviesControl_Load(object sender, EventArgs e)
        {

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("ImageSource", "ImageSource");
            resetGrid();
            resetCheckedList();
        }
        private void resetCheckedList()
        {

            _genres = m_session.CreateCriteria(typeof(Genre)).List<Genre>();
            GenresBindingSource = new BindingSource();
            GenresBindingSource.DataSource = _genres;

            ((CheckedListBox)GenresCheckedListbox).DataSource = GenresBindingSource;

            ((CheckedListBox)GenresCheckedListbox).DisplayMember = "Name";
            ((CheckedListBox)GenresCheckedListbox).ValueMember = "Id";
        }
        private void resetGrid()
        {

            movies = m_session.CreateCriteria(typeof(Movie)).List<Movie>().ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (Movie m in movies)
            {
                dataGridView1.Rows.Add(new object[] { m.Id, m.Title, m.Description, m.ImageSource });
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
            TitleTextbox.Text = row.Cells[1].Value.ToString();
            DescriptionTextbox.Text = row.Cells[2].Value.ToString();
            ImageUrlTextbox.Text = row.Cells[3].Value.ToString();

            try
            {
                //uporabljaj imena classov in spremenljivk v classu
                IQuery query = m_session.CreateQuery("from MovieGenre mg where mg.Movie=" + IdTextbox.Text);
                List<MovieGenre> MoviesGenres = query.List<MovieGenre>().ToList();
                //reset checkboxov
                for (int i = 0; i < GenresCheckedListbox.Items.Count; i++)
                {
                    GenresCheckedListbox.SetItemChecked(i, false);
                }
                foreach (MovieGenre femg in MoviesGenres)
                {
                    for (int i = 0; i < GenresCheckedListbox.Items.Count; i++)
                    {
                        if (femg.Genre.Name == ((Genre)GenresCheckedListbox.Items[i]).Name)
                        {
                            GenresCheckedListbox.SetItemChecked(i, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Movie m = new Movie();
            m.Title = TitleTextbox.Text;
            m.Description = DescriptionTextbox.Text;
            m.ImageSource = ImageUrlTextbox.Text;
            using (ISession m_Session = m_sessionfactory.OpenSession())//vstavi film
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {

                        m_Session.Save(m);
                        tx.Commit();
                        IdTextbox.Text = m.Id.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }

            foreach (object itemChecked in GenresCheckedListbox.CheckedItems)
            {
                Genre g = (Genre)itemChecked;
                using (ISession m_Session = m_sessionfactory.OpenSession())
                {
                    using (ITransaction tx = m_Session.BeginTransaction())
                    {
                        try
                        {
                            MovieGenre mg = new MovieGenre();//vstavi v moviegenre vmesno tabelo
                            mg.Movie = m;
                            mg.Genre = g;
                            m_Session.Save(mg);
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
            resetCheckedList();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            using (ISession m_Session = m_sessionfactory.OpenSession())
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Movie m where m.Id=?");
                        Movie res = query.SetString(0, IdTextbox.Text).UniqueResult<Movie>();
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
            Movie m = new Movie();
            using (ISession m_Session = m_sessionfactory.OpenSession())//updatam stvari iz textboxov
            {
                using (ITransaction tx = m_Session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = m_Session.CreateQuery("from Movie m where m.Id=?");
                        Movie res = query.SetString(0, IdTextbox.Text).UniqueResult<Movie>();
                        res.Title = TitleTextbox.Text;
                        res.Description = DescriptionTextbox.Text;
                        res.ImageSource = ImageUrlTextbox.Text;
                        m_Session.Update(res);
                        tx.Commit();
                        m = res;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            List<MovieGenre> MoviesGenres = new List<MovieGenre>();
            //brisanje iz VMESNE TABELE - POVEZAVE
            //dobim seznam žanrov filma
            try
            {
                IQuery query = m_session.CreateQuery("from MovieGenre mg where mg.Movie=" + IdTextbox.Text);
                MoviesGenres = query.List<MovieGenre>().ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            foreach (object itemChecked in GenresCheckedListbox.CheckedItems)//primerjam seznam iz baze ter obkljukane žanre
            {
                Genre g = (Genre)itemChecked;
                bool exists = false;
                foreach (MovieGenre moviegenre in MoviesGenres)
                {
                    if (g.Id == moviegenre.Genre.Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)//če žanr, ki je obkljukan ne obstaja v bazi ga moramo dodati
                {//dodaj
                    using (ISession m_Session = m_sessionfactory.OpenSession())
                    {
                        using (ITransaction tx = m_Session.BeginTransaction())
                        {
                            MovieGenre mg = new MovieGenre();//vstavi v moviegenre vmesno tabelo
                            mg.Movie = m;
                            mg.Genre = g;
                            m_Session.Save(mg);
                            tx.Commit();
                        }
                    }
                }
            }
            //sedaj moramo preveriti če je kateri žanr, ki NI obkljukan shranjen v bazi. nato ga zbrišemo

            foreach (MovieGenre moviegenre in MoviesGenres)
            {
                foreach (object item in GenresCheckedListbox.Items)
                {
                    if (!GenresCheckedListbox.CheckedItems.Contains(item))//unchecked
                    {
                        Genre g = (Genre)item;
                        if (moviegenre.Genre.Id == g.Id)
                        {
                            //odstrani
                            using (ISession m_Session = m_sessionfactory.OpenSession())
                            {
                                using (ITransaction tx = m_Session.BeginTransaction())
                                {
                                    m_Session.Delete(moviegenre);
                                    tx.Commit();
                                }
                            }
                        }
                    }
                }
            }
            resetGrid();
            resetCheckedList();
        }

        private void GenresCheckedListbox_Enter(object sender, EventArgs e)
        {
            resetCheckedList();
            if (string.IsNullOrEmpty(IdTextbox.Text))
            {
                return;
            }
            try
            {
                //uporabljaj imena classov in spremenljivk v classu
                IQuery query = m_session.CreateQuery("from MovieGenre mg where mg.Movie=" + IdTextbox.Text);
                List<MovieGenre> MoviesGenres = query.List<MovieGenre>().ToList();
                //reset checkboxov
                for (int i = 0; i < GenresCheckedListbox.Items.Count; i++)
                {
                    GenresCheckedListbox.SetItemChecked(i, false);
                }
                foreach (MovieGenre femg in MoviesGenres)
                {
                    for (int i = 0; i < GenresCheckedListbox.Items.Count; i++)
                    {
                        if (femg.Genre.Name == ((Genre)GenresCheckedListbox.Items[i]).Name)
                        {
                            GenresCheckedListbox.SetItemChecked(i, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
