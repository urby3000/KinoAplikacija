namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    partial class BillsControl
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OrderDatePicker = new System.Windows.Forms.DateTimePicker();
            this.PriceTextbox = new System.Windows.Forms.TextBox();
            this.FullPriceTextbox = new System.Windows.Forms.TextBox();
            this.DiscountGridView = new System.Windows.Forms.DataGridView();
            this.PayDatePicker = new System.Windows.Forms.DateTimePicker();
            this.PaidCheckbox = new System.Windows.Forms.CheckBox();
            this.DiscountIdTextbox = new System.Windows.Forms.TextBox();
            this.IdTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(635, 178);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.dataGridView1.Enter += new System.EventHandler(this.DataGridView1_Enter);
            // 
            // OrderDatePicker
            // 
            this.OrderDatePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.OrderDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.OrderDatePicker.Location = new System.Drawing.Point(12, 280);
            this.OrderDatePicker.Name = "OrderDatePicker";
            this.OrderDatePicker.Size = new System.Drawing.Size(160, 26);
            this.OrderDatePicker.TabIndex = 36;
            this.OrderDatePicker.Value = new System.DateTime(2019, 5, 6, 0, 0, 0, 0);
            // 
            // PriceTextbox
            // 
            this.PriceTextbox.Enabled = false;
            this.PriceTextbox.Location = new System.Drawing.Point(86, 230);
            this.PriceTextbox.Name = "PriceTextbox";
            this.PriceTextbox.Size = new System.Drawing.Size(70, 26);
            this.PriceTextbox.TabIndex = 37;
            // 
            // FullPriceTextbox
            // 
            this.FullPriceTextbox.Enabled = false;
            this.FullPriceTextbox.Location = new System.Drawing.Point(192, 230);
            this.FullPriceTextbox.Name = "FullPriceTextbox";
            this.FullPriceTextbox.Size = new System.Drawing.Size(70, 26);
            this.FullPriceTextbox.TabIndex = 38;
            // 
            // DiscountGridView
            // 
            this.DiscountGridView.AllowUserToAddRows = false;
            this.DiscountGridView.AllowUserToDeleteRows = false;
            this.DiscountGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DiscountGridView.Location = new System.Drawing.Point(376, 212);
            this.DiscountGridView.Name = "DiscountGridView";
            this.DiscountGridView.ReadOnly = true;
            this.DiscountGridView.Size = new System.Drawing.Size(262, 159);
            this.DiscountGridView.TabIndex = 39;
            this.DiscountGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DiscountGridView_CellClick);
            this.DiscountGridView.Enter += new System.EventHandler(this.DiscountGridView_Enter);
            // 
            // PayDatePicker
            // 
            this.PayDatePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.PayDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PayDatePicker.Location = new System.Drawing.Point(187, 280);
            this.PayDatePicker.Name = "PayDatePicker";
            this.PayDatePicker.Size = new System.Drawing.Size(160, 26);
            this.PayDatePicker.TabIndex = 40;
            this.PayDatePicker.Value = new System.DateTime(2019, 5, 6, 0, 0, 0, 0);
            // 
            // PaidCheckbox
            // 
            this.PaidCheckbox.AutoSize = true;
            this.PaidCheckbox.Location = new System.Drawing.Point(187, 312);
            this.PaidCheckbox.Name = "PaidCheckbox";
            this.PaidCheckbox.Size = new System.Drawing.Size(54, 22);
            this.PaidCheckbox.TabIndex = 41;
            this.PaidCheckbox.Text = "Paid";
            this.PaidCheckbox.UseVisualStyleBackColor = true;
            // 
            // DiscountIdTextbox
            // 
            this.DiscountIdTextbox.Location = new System.Drawing.Point(316, 230);
            this.DiscountIdTextbox.Name = "DiscountIdTextbox";
            this.DiscountIdTextbox.Size = new System.Drawing.Size(31, 26);
            this.DiscountIdTextbox.TabIndex = 42;
            // 
            // IdTextbox
            // 
            this.IdTextbox.Enabled = false;
            this.IdTextbox.Location = new System.Drawing.Point(12, 230);
            this.IdTextbox.Name = "IdTextbox";
            this.IdTextbox.Size = new System.Drawing.Size(31, 26);
            this.IdTextbox.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 18);
            this.label1.TabIndex = 44;
            this.label1.Text = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 45;
            this.label2.Text = "OrderDate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "PayDate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 47;
            this.label4.Text = "DiscountId";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 18);
            this.label5.TabIndex = 48;
            this.label5.Text = "Price";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 49;
            this.label6.Text = "FullPrice";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.DeleteButton.Location = new System.Drawing.Point(217, 339);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(104, 32);
            this.DeleteButton.TabIndex = 52;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.EditButton.Location = new System.Drawing.Point(124, 339);
            this.EditButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(85, 32);
            this.EditButton.TabIndex = 51;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.AddButton.Location = new System.Drawing.Point(12, 339);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(104, 32);
            this.AddButton.TabIndex = 50;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 18);
            this.label7.TabIndex = 53;
            this.label7.Text = "Bills";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.button1.Location = new System.Drawing.Point(506, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 22);
            this.button1.TabIndex = 54;
            this.button1.Text = "Export Bill PDFs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BillsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IdTextbox);
            this.Controls.Add(this.DiscountIdTextbox);
            this.Controls.Add(this.PaidCheckbox);
            this.Controls.Add(this.PayDatePicker);
            this.Controls.Add(this.DiscountGridView);
            this.Controls.Add(this.FullPriceTextbox);
            this.Controls.Add(this.PriceTextbox);
            this.Controls.Add(this.OrderDatePicker);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BillsControl";
            this.Size = new System.Drawing.Size(641, 374);
            this.Load += new System.EventHandler(this.BillsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker OrderDatePicker;
        private System.Windows.Forms.TextBox PriceTextbox;
        private System.Windows.Forms.TextBox FullPriceTextbox;
        private System.Windows.Forms.DataGridView DiscountGridView;
        private System.Windows.Forms.DateTimePicker PayDatePicker;
        private System.Windows.Forms.CheckBox PaidCheckbox;
        private System.Windows.Forms.TextBox DiscountIdTextbox;
        private System.Windows.Forms.TextBox IdTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}
