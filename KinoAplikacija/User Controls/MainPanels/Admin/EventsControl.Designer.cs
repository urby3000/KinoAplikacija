namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    partial class EventsControl
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
            this.MovieGridView = new System.Windows.Forms.DataGridView();
            this.TheatersComboBox = new System.Windows.Forms.ComboBox();
            this.RoomGridView = new System.Windows.Forms.DataGridView();
            this.IdTextbox = new System.Windows.Forms.TextBox();
            this.PriceTextbox = new System.Windows.Forms.TextBox();
            this.RoomIdTextbox = new System.Windows.Forms.TextBox();
            this.MovieIdTextbox = new System.Windows.Forms.TextBox();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MovieId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MovieGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(551, 147);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // MovieGridView
            // 
            this.MovieGridView.AllowUserToAddRows = false;
            this.MovieGridView.AllowUserToDeleteRows = false;
            this.MovieGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MovieGridView.Location = new System.Drawing.Point(178, 230);
            this.MovieGridView.Name = "MovieGridView";
            this.MovieGridView.ReadOnly = true;
            this.MovieGridView.Size = new System.Drawing.Size(192, 147);
            this.MovieGridView.TabIndex = 1;
            this.MovieGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MovieGridView_CellClick);
            // 
            // TheatersComboBox
            // 
            this.TheatersComboBox.FormattingEnabled = true;
            this.TheatersComboBox.Location = new System.Drawing.Point(412, 198);
            this.TheatersComboBox.Name = "TheatersComboBox";
            this.TheatersComboBox.Size = new System.Drawing.Size(142, 26);
            this.TheatersComboBox.TabIndex = 2;
            this.TheatersComboBox.SelectedIndexChanged += new System.EventHandler(this.TheatersComboBox_SelectedIndexChanged);
            // 
            // RoomGridView
            // 
            this.RoomGridView.AllowUserToAddRows = false;
            this.RoomGridView.AllowUserToDeleteRows = false;
            this.RoomGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoomGridView.Location = new System.Drawing.Point(376, 230);
            this.RoomGridView.Name = "RoomGridView";
            this.RoomGridView.ReadOnly = true;
            this.RoomGridView.Size = new System.Drawing.Size(178, 147);
            this.RoomGridView.TabIndex = 3;
            this.RoomGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoomGridView_CellClick);
            // 
            // IdTextbox
            // 
            this.IdTextbox.Enabled = false;
            this.IdTextbox.Location = new System.Drawing.Point(12, 198);
            this.IdTextbox.Name = "IdTextbox";
            this.IdTextbox.Size = new System.Drawing.Size(27, 26);
            this.IdTextbox.TabIndex = 4;
            // 
            // PriceTextbox
            // 
            this.PriceTextbox.Location = new System.Drawing.Point(12, 320);
            this.PriceTextbox.Name = "PriceTextbox";
            this.PriceTextbox.Size = new System.Drawing.Size(70, 26);
            this.PriceTextbox.TabIndex = 5;
            this.PriceTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PriceTextbox_KeyPress);
            // 
            // RoomIdTextbox
            // 
            this.RoomIdTextbox.Location = new System.Drawing.Point(376, 198);
            this.RoomIdTextbox.Name = "RoomIdTextbox";
            this.RoomIdTextbox.Size = new System.Drawing.Size(27, 26);
            this.RoomIdTextbox.TabIndex = 6;
            // 
            // MovieIdTextbox
            // 
            this.MovieIdTextbox.Location = new System.Drawing.Point(178, 198);
            this.MovieIdTextbox.Name = "MovieIdTextbox";
            this.MovieIdTextbox.Size = new System.Drawing.Size(27, 26);
            this.MovieIdTextbox.TabIndex = 7;
            // 
            // DatePicker
            // 
            this.DatePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker.Location = new System.Drawing.Point(12, 258);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(160, 26);
            this.DatePicker.TabIndex = 35;
            this.DatePicker.Value = new System.DateTime(2019, 5, 6, 0, 0, 0, 0);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.DeleteButton.Location = new System.Drawing.Point(226, 384);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(104, 32);
            this.DeleteButton.TabIndex = 38;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.EditButton.Location = new System.Drawing.Point(133, 384);
            this.EditButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(85, 32);
            this.EditButton.TabIndex = 37;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.AddButton.Location = new System.Drawing.Point(21, 384);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(104, 32);
            this.AddButton.TabIndex = 36;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 18);
            this.label1.TabIndex = 39;
            this.label1.Text = "Id";
            // 
            // MovieId
            // 
            this.MovieId.AutoSize = true;
            this.MovieId.Location = new System.Drawing.Point(163, 177);
            this.MovieId.Name = "MovieId";
            this.MovieId.Size = new System.Drawing.Size(59, 18);
            this.MovieId.TabIndex = 40;
            this.MovieId.Text = "MovieId";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 41;
            this.label3.Text = "RoomId";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 18);
            this.label4.TabIndex = 42;
            this.label4.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 299);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 18);
            this.label5.TabIndex = 43;
            this.label5.Text = "Price";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 18);
            this.label7.TabIndex = 55;
            this.label7.Text = "Events";
            // 
            // EventsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MovieId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.MovieIdTextbox);
            this.Controls.Add(this.RoomIdTextbox);
            this.Controls.Add(this.PriceTextbox);
            this.Controls.Add(this.IdTextbox);
            this.Controls.Add(this.RoomGridView);
            this.Controls.Add(this.TheatersComboBox);
            this.Controls.Add(this.MovieGridView);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EventsControl";
            this.Size = new System.Drawing.Size(557, 419);
            this.Load += new System.EventHandler(this.EventsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MovieGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView MovieGridView;
        private System.Windows.Forms.ComboBox TheatersComboBox;
        private System.Windows.Forms.DataGridView RoomGridView;
        private System.Windows.Forms.TextBox IdTextbox;
        private System.Windows.Forms.TextBox PriceTextbox;
        private System.Windows.Forms.TextBox RoomIdTextbox;
        private System.Windows.Forms.TextBox MovieIdTextbox;
        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MovieId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}
