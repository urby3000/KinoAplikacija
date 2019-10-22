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
using System.IO;
using IronPdf;

namespace KinoAplikacija.User_Controls.MainPanels.Normal.Bills
{
    public partial class BillForUser : UserControl
    {
        string billHtmlTemplate = Environment.CurrentDirectory + @"\Templates\Bill Html Template\";
        string billHtmlExported = Environment.CurrentDirectory + @"\BillHtmls\";
        private ISessionFactory m_sessionfactory = null;
        private ISession m_session = null;
        private User CurrentUser;
        public List<BillInfoForm> bifList = new List<BillInfoForm>();
        Bill bill;
        public UserBillsControl ubcontrol;
        public BillForUser(Bill b, User u, UserBillsControl ubc)
        {
            InitializeComponent();
            bill = b;
            CurrentUser = u;
            ubcontrol = ubc;
        }
        public void SetNhib(ISessionFactory isf, ISession iss)
        {
            m_sessionfactory = isf;
            m_session = iss;
        }
        public void setTextLanguage()
        {
            foreach (BillInfoForm bif in bifList)
            {
                bif.setTextLanguage();
            }

            idLabel.Text = "#" + bill.Id.ToString();
            orderDateLabel.Text = bill.OrderDate.ToString("dd MMMM yyyy");
            if (bill.Paid)
            {
                paidDateLabel.Text = Properties.Resources.paidDateLabelPaid + " " + ((DateTime)bill.PayDate).ToString("dd MMMM yyyy");
                this.BackColor = Color.Honeydew;
            }
            else
            {
                this.BackColor = Color.LightCoral;
                paidDateLabel.Text = Properties.Resources.paidDateLabelNotPaid;
            }
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in bill.Reservations)
            {
                if (!uniqueEvents.Contains(r.Event))
                {
                    uniqueEvents.Add(r.Event);
                }
            }
            EventsTextbox.Text = "";
            foreach (Event even in uniqueEvents)
            {
                int counter = 0;
                foreach (Reservation r in bill.Reservations)
                {
                    if (r.Event == even)
                    {
                        counter++;
                    }
                }
                EventsTextbox.Text += counter.ToString() + "x " + even.Movie.Title + " ";
                counter = 0;
            }
            priceLabel.Text = String.Format("{0:0.##}", bill.Price) + " €";
            if (bill.Discount == null)
            {
                discountLabel.Text = Properties.Resources.DiscountTranslate + ": 0%";
            }
            else
            {
                discountLabel.Text = bill.Discount.Name + " - " + String.Format("{0:0.##}", bill.Discount.Percent) + "%";
            }
            FullPriceLabel.Text = String.Format("{0:0.##}", bill.FullPrice) + " €";

        }
        private void BillForUser_Load(object sender, EventArgs e)
        {
            setTextLanguage();
        }

        private void Div_MouseLeave(object sender, EventArgs e)
        {
            if (bill.Paid)
            {
                this.BackColor = Color.Honeydew;
            }
            else
            {
                this.BackColor = Color.LightCoral;
            }
        }

        private void Div_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
        }

        private void BillForUser_Click(object sender, EventArgs e)
        {
            BillInfoForm bif = new BillInfoForm(CurrentUser, bill, this);
            bif.SetNhib(m_sessionfactory, m_session);
            bif.Show();
            bifList.Add(bif);
        }

        private void BillPDFPictureBox_MouseEnter(object sender, EventArgs e)
        {
            BillPDFPictureBox.BackColor = Color.AliceBlue;
            ToolTip tt = new ToolTip();
            tt.Show(Properties.Resources.BillTranslate+" PDF", (PictureBox)sender, 20, 0, 500);
        }

        private void BillPDFPictureBox_MouseLeave(object sender, EventArgs e)
        {
            BillPDFPictureBox.BackColor = Color.Transparent;
        }

        private void BillPDFPictureBox_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "pdf "+Properties.Resources.FileTranslate+" (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = bill.Id.ToString()+ "-"+Properties.Resources.BillTranslate + "-" + bill.OrderDate.ToString("ddMMMMyyyy");
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Task a = Task.Run(() => {
                    CreateHtml();
                });
                a.Wait();
                // Create a PDF from an existing HTML using C#
                var Renderer = new IronPdf.HtmlToPdf();
                var PDF = Renderer.RenderHTMLFileAsPdf(billHtmlExported + "index.html");
                var OutputPath = saveFileDialog1.FileName;
                try
                {
                    PDF.SaveAs(OutputPath);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                System.Diagnostics.Process.Start(OutputPath);
            }

        }
        private string CreateHtml()
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






            body = body.Replace("{Name}", CurrentUser.Name);
            body = body.Replace("{Lastname}", CurrentUser.Surname);
            string address = "";
            if (CurrentUser.Address != "")
            {
                address += CurrentUser.Address;
            }
            if (CurrentUser.Place != null)
            {
                address += ", " + CurrentUser.Place.PostalCode + " " + CurrentUser.Place.Name
                                        + ", " + CurrentUser.Place.Country.Name;
            }
            body = body.Replace("{UserAddress}", address);
            body = body.Replace("{UserEmail}", CurrentUser.Email);
            body = body.Replace("{UserTelephone}", CurrentUser.TelephoneNumber);

            body = body.Replace("{BillId}", bill.Id.ToString());
            body = body.Replace("{OrderDate}", bill.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            if (bill.Paid)
            {
                body = body.Replace("{PayDate}", ((DateTime)bill.PayDate).ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            }
            else
            {
                body = body.Replace("{PayDate}", "<span style=\"background-color:#F08080; color:black\">"+Properties.Resources.paidDateLabelNotPaid+"</span>");
            }
            List<Event> uniqueEvents = new List<Event>();
            foreach (Reservation r in bill.Reservations)
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
                foreach (Reservation r in bill.Reservations)
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

                String result = body.Substring(pFrom, pTo - pFrom);
                result = result.Replace("{EventId}", even.Id.ToString());
                result = result.Replace("{MovieTitle}", even.Movie.Title);
                result = result.Replace("{Date}", even.Date.ToString("dd MMMM yyyy"));
                result = result.Replace("{Time}", even.Date.ToString("HH:mm:ss"));
                result = result.Replace("{TheaterAddress}", even.Room.Theater.Address + ", " + even.Room.Theater.Place.Name
                                                        + ", " + even.Room.Theater.Place.Country.Name);
                result = result.Replace("{TheaterRoom}", even.Room.Theater.Name + ", " + even.Room.Name);
                result = result.Replace("{SeatNumbers}", s);
                result = result.Replace("{Quantity}", counter.ToString());
                result = result.Replace("{UnitPrice}", String.Format("{0:0.##}", even.Price) + " €");
                result = result.Replace("{Total}", String.Format("{0:0.##}", (counter * even.Price)) + " €");


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
                body = body.Replace("{Discount}", bill.Discount.Name + "  " + String.Format("{0:0.##}", bill.Discount.Percent) + " %");

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

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
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
                if (File.Exists(targetDir + "/" + Path.GetFileName(file)))
                {
                    File.Delete(targetDir + "/" + Path.GetFileName(file));
                }
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

    }
}
