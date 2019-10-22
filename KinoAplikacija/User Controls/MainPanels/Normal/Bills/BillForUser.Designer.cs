namespace KinoAplikacija.User_Controls.MainPanels.Normal.Bills
{
    partial class BillForUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.idLabel = new System.Windows.Forms.Label();
            this.orderDateLabel = new System.Windows.Forms.Label();
            this.paidDateLabel = new System.Windows.Forms.Label();
            this.EventsTextbox = new System.Windows.Forms.RichTextBox();
            this.priceLabel = new System.Windows.Forms.Label();
            this.FullPriceLabel = new System.Windows.Forms.Label();
            this.discountLabel = new System.Windows.Forms.Label();
            this.BillPDFPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BillPDFPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(13, 12);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(20, 18);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "id";
            this.idLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.idLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.idLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // orderDateLabel
            // 
            this.orderDateLabel.AutoSize = true;
            this.orderDateLabel.Location = new System.Drawing.Point(52, 12);
            this.orderDateLabel.Name = "orderDateLabel";
            this.orderDateLabel.Size = new System.Drawing.Size(73, 18);
            this.orderDateLabel.TabIndex = 1;
            this.orderDateLabel.Text = "order date";
            this.orderDateLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.orderDateLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.orderDateLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // paidDateLabel
            // 
            this.paidDateLabel.AutoSize = true;
            this.paidDateLabel.Location = new System.Drawing.Point(155, 12);
            this.paidDateLabel.Name = "paidDateLabel";
            this.paidDateLabel.Size = new System.Drawing.Size(66, 18);
            this.paidDateLabel.TabIndex = 2;
            this.paidDateLabel.Text = "paid date";
            this.paidDateLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.paidDateLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.paidDateLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // EventsTextbox
            // 
            this.EventsTextbox.Location = new System.Drawing.Point(315, 3);
            this.EventsTextbox.Name = "EventsTextbox";
            this.EventsTextbox.ReadOnly = true;
            this.EventsTextbox.Size = new System.Drawing.Size(190, 37);
            this.EventsTextbox.TabIndex = 3;
            this.EventsTextbox.Text = "";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(511, 12);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(39, 18);
            this.priceLabel.TabIndex = 4;
            this.priceLabel.Text = "Price";
            this.priceLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.priceLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.priceLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // FullPriceLabel
            // 
            this.FullPriceLabel.AutoSize = true;
            this.FullPriceLabel.Location = new System.Drawing.Point(742, 12);
            this.FullPriceLabel.Name = "FullPriceLabel";
            this.FullPriceLabel.Size = new System.Drawing.Size(65, 18);
            this.FullPriceLabel.TabIndex = 5;
            this.FullPriceLabel.Text = "Full Price";
            this.FullPriceLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.FullPriceLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.FullPriceLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // discountLabel
            // 
            this.discountLabel.AutoSize = true;
            this.discountLabel.Location = new System.Drawing.Point(591, 12);
            this.discountLabel.Name = "discountLabel";
            this.discountLabel.Size = new System.Drawing.Size(122, 18);
            this.discountLabel.TabIndex = 6;
            this.discountLabel.Text = "Discount name - %";
            this.discountLabel.Click += new System.EventHandler(this.BillForUser_Click);
            this.discountLabel.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.discountLabel.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            // 
            // BillPDFPictureBox
            // 
            this.BillPDFPictureBox.Image = global::KinoAplikacija.Properties.Resources.icons8_export_pdf_filled_80;
            this.BillPDFPictureBox.Location = new System.Drawing.Point(835, 3);
            this.BillPDFPictureBox.Name = "BillPDFPictureBox";
            this.BillPDFPictureBox.Size = new System.Drawing.Size(33, 37);
            this.BillPDFPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BillPDFPictureBox.TabIndex = 7;
            this.BillPDFPictureBox.TabStop = false;
            this.BillPDFPictureBox.Click += new System.EventHandler(this.BillPDFPictureBox_Click);
            this.BillPDFPictureBox.MouseEnter += new System.EventHandler(this.BillPDFPictureBox_MouseEnter);
            this.BillPDFPictureBox.MouseLeave += new System.EventHandler(this.BillPDFPictureBox_MouseLeave);
            // 
            // BillForUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.BillPDFPictureBox);
            this.Controls.Add(this.discountLabel);
            this.Controls.Add(this.FullPriceLabel);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.EventsTextbox);
            this.Controls.Add(this.paidDateLabel);
            this.Controls.Add(this.orderDateLabel);
            this.Controls.Add(this.idLabel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BillForUser";
            this.Size = new System.Drawing.Size(935, 43);
            this.Load += new System.EventHandler(this.BillForUser_Load);
            this.Click += new System.EventHandler(this.BillForUser_Click);
            this.MouseEnter += new System.EventHandler(this.Div_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Div_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.BillPDFPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label orderDateLabel;
        private System.Windows.Forms.Label paidDateLabel;
        private System.Windows.Forms.RichTextBox EventsTextbox;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label FullPriceLabel;
        private System.Windows.Forms.Label discountLabel;
        private System.Windows.Forms.PictureBox BillPDFPictureBox;
    }
}
