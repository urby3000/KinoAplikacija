namespace KinoAplikacija.User_Controls.MainPanels.Normal.Checkout
{
    partial class CheckoutForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PriceAfterDiscountLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.DiscountLabel = new System.Windows.Forms.Label();
            this.ItemsLabel = new System.Windows.Forms.Label();
            this.BillDateLabel = new System.Windows.Forms.Label();
            this.orderDateLabel = new System.Windows.Forms.Label();
            this.DiscountTextbox = new System.Windows.Forms.TextBox();
            this.FullPriceTextbox = new System.Windows.Forms.TextBox();
            this.PriceTextbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(944, 378);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(863, 378);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 1;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1027, 197);
            this.dataGridView1.TabIndex = 31;
            // 
            // PriceAfterDiscountLabel
            // 
            this.PriceAfterDiscountLabel.AutoSize = true;
            this.PriceAfterDiscountLabel.Location = new System.Drawing.Point(905, 325);
            this.PriceAfterDiscountLabel.Name = "PriceAfterDiscountLabel";
            this.PriceAfterDiscountLabel.Size = new System.Drawing.Size(130, 18);
            this.PriceAfterDiscountLabel.TabIndex = 30;
            this.PriceAfterDiscountLabel.Text = "Price After Discount";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(905, 271);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(39, 18);
            this.PriceLabel.TabIndex = 29;
            this.PriceLabel.Text = "Price";
            // 
            // DiscountLabel
            // 
            this.DiscountLabel.AutoSize = true;
            this.DiscountLabel.Location = new System.Drawing.Point(661, 271);
            this.DiscountLabel.Name = "DiscountLabel";
            this.DiscountLabel.Size = new System.Drawing.Size(62, 18);
            this.DiscountLabel.TabIndex = 28;
            this.DiscountLabel.Text = "Discount";
            // 
            // ItemsLabel
            // 
            this.ItemsLabel.AutoSize = true;
            this.ItemsLabel.Location = new System.Drawing.Point(11, 50);
            this.ItemsLabel.Name = "ItemsLabel";
            this.ItemsLabel.Size = new System.Drawing.Size(46, 18);
            this.ItemsLabel.TabIndex = 27;
            this.ItemsLabel.Text = "Items ";
            // 
            // BillDateLabel
            // 
            this.BillDateLabel.AutoSize = true;
            this.BillDateLabel.Location = new System.Drawing.Point(9, 9);
            this.BillDateLabel.Name = "BillDateLabel";
            this.BillDateLabel.Size = new System.Drawing.Size(60, 18);
            this.BillDateLabel.TabIndex = 24;
            this.BillDateLabel.Text = "Bill Date";
            // 
            // orderDateLabel
            // 
            this.orderDateLabel.AutoSize = true;
            this.orderDateLabel.Location = new System.Drawing.Point(29, 29);
            this.orderDateLabel.Name = "orderDateLabel";
            this.orderDateLabel.Size = new System.Drawing.Size(101, 18);
            this.orderDateLabel.TabIndex = 23;
            this.orderDateLabel.Text = "info order date";
            // 
            // DiscountTextbox
            // 
            this.DiscountTextbox.Location = new System.Drawing.Point(675, 292);
            this.DiscountTextbox.Name = "DiscountTextbox";
            this.DiscountTextbox.ReadOnly = true;
            this.DiscountTextbox.Size = new System.Drawing.Size(238, 26);
            this.DiscountTextbox.TabIndex = 21;
            // 
            // FullPriceTextbox
            // 
            this.FullPriceTextbox.Location = new System.Drawing.Point(919, 346);
            this.FullPriceTextbox.Name = "FullPriceTextbox";
            this.FullPriceTextbox.ReadOnly = true;
            this.FullPriceTextbox.Size = new System.Drawing.Size(100, 26);
            this.FullPriceTextbox.TabIndex = 19;
            // 
            // PriceTextbox
            // 
            this.PriceTextbox.Location = new System.Drawing.Point(919, 292);
            this.PriceTextbox.Name = "PriceTextbox";
            this.PriceTextbox.ReadOnly = true;
            this.PriceTextbox.Size = new System.Drawing.Size(100, 26);
            this.PriceTextbox.TabIndex = 20;
            // 
            // CheckoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1048, 412);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.PriceAfterDiscountLabel);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.DiscountLabel);
            this.Controls.Add(this.ItemsLabel);
            this.Controls.Add(this.BillDateLabel);
            this.Controls.Add(this.orderDateLabel);
            this.Controls.Add(this.DiscountTextbox);
            this.Controls.Add(this.FullPriceTextbox);
            this.Controls.Add(this.PriceTextbox);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.ConfirmButton);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CheckoutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkout";
            this.Load += new System.EventHandler(this.CheckoutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label PriceAfterDiscountLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label DiscountLabel;
        private System.Windows.Forms.Label ItemsLabel;
        private System.Windows.Forms.Label BillDateLabel;
        private System.Windows.Forms.Label orderDateLabel;
        private System.Windows.Forms.TextBox DiscountTextbox;
        private System.Windows.Forms.TextBox FullPriceTextbox;
        private System.Windows.Forms.TextBox PriceTextbox;
    }
}