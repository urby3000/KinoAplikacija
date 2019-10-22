using KinoAplikacija.Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    public partial class BillPDFExportForm : Form
    {
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        List<Bill> bills = new List<Bill>();
        string filepath;
        string billHtmlTemplate = Environment.CurrentDirectory + @"\Templates\Bill Html Template\";
        string billHtmlExported = Environment.CurrentDirectory + @"\BillHtmls\";
        public BillPDFExportForm(string fp)
        {
            InitializeComponent();
            filepath = fp;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }

        private void BillPDFExportForm_Load(object sender, EventArgs e)
        {
            bills = m_session.CreateCriteria(typeof(Bill)).List<Bill>().ToList();
            progressBar1.Maximum = bills.Count;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 1;
            foreach (Bill b in bills)
            {
                string filename = b.Id.ToString() + "-" + Properties.Resources.BillTranslate + "-" + b.OrderDate.ToString("ddMMMMyyyy") + ".pdf";
                /*
                Task a = Task.Run(() =>
                {

                });
                a.Wait();*/

                CreateHtml(b);
                var Renderer = new IronPdf.HtmlToPdf();
                // Create a PDF from an existing HTML using C#
                while (!File.Exists(billHtmlExported + "index.html")) { }
                var PDF = Renderer.RenderHTMLFileAsPdf(billHtmlExported + "index.html");
                PDF.SaveAs(filepath + @"/" + filename);
                backgroundWorker1.ReportProgress(counter, b);
                counter++;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Bill ID: " + ((Bill)e.UserState).Id;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // Cancel the asynchronous operation.
            this.backgroundWorker1.CancelAsync();
            this.Close();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
        private string CreateHtml(Bill bill)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(billHtmlTemplate + "index.html"))
            {
                body = reader.ReadToEnd();
            }
            //translated
            body = body.Replace("{BillTo}", Properties.Resources.BillToHtml);
            body = body.Replace("{BillText}", Properties.Resources.BillHtml);
            body = body.Replace("{DateOfBill}", Properties.Resources.DateOfBillHtml);
            body = body.Replace("{DateOfPayment}", Properties.Resources.DateOfPaymentHtml);


            body = body.Replace("{ColumnEvent}", Properties.Resources.ColumnEventHtml);
            body = body.Replace("{ColumnMovie}", Properties.Resources.ColumnMovieHtml);
            body = body.Replace("{ColumnDate}", Properties.Resources.ColumnDateHtml);
            body = body.Replace("{ColumnTime}", Properties.Resources.ColumnTimeHtml);
            body = body.Replace("{ColumnTheaterAddress}", Properties.Resources.ColumnTheaterAddressHtml);
            body = body.Replace("{ColumnTheaterRoom}", Properties.Resources.ColumnTheaterRoomHtml);
            body = body.Replace("{ColumnSeatNumbers}", Properties.Resources.ColumnSeatNumbersHtml);
            body = body.Replace("{ColumnQuantity}", Properties.Resources.ColumnQuantityHtml);
            body = body.Replace("{ColumnUnitPrice}", Properties.Resources.ColumnUnitPriceHtml);
            body = body.Replace("{ColumnTotal}", Properties.Resources.ColumnTotalHtml);

            body = body.Replace("{TotalPriceHtml}", Properties.Resources.TotalPriceHtml);
            body = body.Replace("{DiscountHtml}", Properties.Resources.DiscountHtml);
            body = body.Replace("{PriceAfterDiscountHtml}", Properties.Resources.PriceAfterDiscountHtml);
            body = body.Replace("{FooterHtml}", Properties.Resources.FooterHtml);

            IList<Reservation> Reservations = null;

            User user = new User();
            try
            {
                IQuery query = m_session.CreateQuery("from Reservation r where r.Bill=" + bill.Id);
                IList<Reservation> res = query.List<Reservation>();
                Reservations = res;
                if (Reservations.Count > 0)
                {
                    user = Reservations[0].User;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Reservations==null)
            {
                try
                {
                    IQuery query = m_session.CreateQuery("from User u where u.Id=" + user.Id);
                    User res = query.UniqueResult<User>();
                    user = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                body = body.Replace("{Name}", user.Name);
                body = body.Replace("{Lastname}", user.Surname);
                string address = "";
                if (user.Address != "")
                {
                    address += user.Address;
                }
                Place place = new Place();
                Country country = new Country();
                if (user.Place != null)
                {
                    try
                    {
                        IQuery query = m_session.CreateQuery("from Place p where p.Id=" + user.Place.Id);
                        Place res = query.UniqueResult<Place>();
                        place = res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    try
                    {
                        IQuery query = m_session.CreateQuery("from Country c where c.Id=" + place.Country.Id);
                        Country res = query.UniqueResult<Country>();
                        country = res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    address += ", " + place.PostalCode + " " + place.Name
                                            + ", " + country.Name;
                }
                body = body.Replace("{UserAddress}", address);
                body = body.Replace("{UserEmail}", user.Email);
                body = body.Replace("{UserTelephone}", user.TelephoneNumber);
            }


            body = body.Replace("{BillId}", bill.Id.ToString());
            body = body.Replace("{OrderDate}", bill.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            if (bill.Paid)
            {
                body = body.Replace("{PayDate}", ((DateTime)bill.PayDate).ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            }
            else
            {
                body = body.Replace("{PayDate}", "<span style=\"background-color:#F08080; color:black\">" + Properties.Resources.paidDateLabelNotPaid + "</span>");
            }
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in Reservations)
            {
                if (!uniqueEvents.Contains(r.Event))
                {
                    uniqueEvents.Add(r.Event);
                }
            }
            foreach (Event even in uniqueEvents)
            {
                int counter = 0;
                List<int> seatNumbers = new List<int>();
                foreach (Reservation r in Reservations)
                {
                    if (r.Event == even)
                    {
                        seatNumbers.Add(r.SeatNumber);
                        counter++;
                    }
                }
                string s = "";
                foreach (int j in seatNumbers)
                {
                    s += j + " ";
                }

                int pFrom = body.IndexOf("<!--") + "<!--".Length;
                int pTo = body.LastIndexOf("-->");
                //movie
                Movie movie = new Movie();
                try
                {
                    IQuery query = m_session.CreateQuery("from Movie m where m.Id=" + even.Movie.Id);
                    Movie res = query.UniqueResult<Movie>();
                    movie = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //room
                Room room = new Room();
                try
                {
                    IQuery query = m_session.CreateQuery("from Room r where r.Id=" + even.Room.Id);
                    Room res = query.UniqueResult<Room>();
                    room = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //theater
                Theater theater = new Theater();
                try
                {
                    IQuery query = m_session.CreateQuery("from Theater t where t.Id=" + room.Theater.Id);
                    Theater res = query.UniqueResult<Theater>();
                    theater = res;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //t place
                Place place = new Place();
                try
                {
                    IQuery query = m_session.CreateQuery("from Place p where p.Id=" + theater.Place.Id);
                    Place res = query.UniqueResult<Place>();
                    place = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //t country
                Country country = new Country();
                try
                {
                    IQuery query = m_session.CreateQuery("from Country c where c.Id=" + place.Country.Id);
                    Country res = query.UniqueResult<Country>();
                    country = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //event
                Event eve = new Event();
                try
                {
                    IQuery query = m_session.CreateQuery("from Event e where e.Id=" + even.Id);
                    Event res = query.UniqueResult<Event>();
                    eve = res;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                String result = body.Substring(pFrom, pTo - pFrom);
                result = result.Replace("{EventId}", eve.Id.ToString());
                result = result.Replace("{MovieTitle}", movie.Title);
                result = result.Replace("{Date}", eve.Date.ToString("dd MMMM yyyy"));
                result = result.Replace("{Time}", eve.Date.ToString("HH:mm:ss"));
                result = result.Replace("{TheaterAddress}", theater.Address + ", " + place.Name
                                                        + ", " + country.Name);
                result = result.Replace("{TheaterRoom}", theater.Name + ", " + room.Name);
                result = result.Replace("{SeatNumbers}", s);
                result = result.Replace("{Quantity}", counter.ToString());
                result = result.Replace("{UnitPrice}", String.Format("{0:0.##}", eve.Price) + " €");
                result = result.Replace("{Total}", String.Format("{0:0.##}", (counter * eve.Price)) + " €");


                int index = body.IndexOf("<tbody>");

                if (index >= 0)
                {
                    body = body.Insert(index + "<tbody>".Length, result);
                }
                counter = 0;
            }
            body = body.Replace("{BillPrice}", String.Format("{0:0.##}", bill.Price) + " €");
            if (bill.Discount == null)
            {
                body = body.Replace("{Discount}", "No Discount  0 %");
            }
            else
            {
                Discount discount = new Discount();
                try
                {
                    IQuery query = m_session.CreateQuery("from Discount d where d.Id=" + bill.Discount.Id);
                    discount = query.UniqueResult<Discount>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                body = body.Replace("{Discount}", discount.Name + "  " + String.Format("{0:0.##}", discount.Percent) + " %");

            }
            body = body.Replace("{FullPrice}", String.Format("{0:0.##}", bill.FullPrice) + " €");

            Copy(billHtmlTemplate, billHtmlExported);
            string path = billHtmlExported + "index.html";
            try
            {

                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(body);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return body;
        }
        void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                try
                {
                    if (File.Exists(targetDir + "/" + Path.GetFileName(file)))
                    {
                        File.Delete(targetDir + "/" + Path.GetFileName(file));
                    }
                    while (!IsFileReady(file)) { }
                    // while (IsFileLocked(new FileInfo(file))) { }
                    File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
