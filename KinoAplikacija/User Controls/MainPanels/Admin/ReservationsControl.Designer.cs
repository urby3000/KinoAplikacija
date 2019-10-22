namespace KinoAplikacija.User_Controls.MainPanels.Admin
{
    partial class ReservationsControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.MovieId = new System.Windows.Forms.Label();
            this.EventIdTextbox = new System.Windows.Forms.TextBox();
            this.UserIdTextbox = new System.Windows.Forms.TextBox();
            this.UserGridView = new System.Windows.Forms.DataGridView();
            this.EventGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.BillIdTextbox = new System.Windows.Forms.TextBox();
            this.BillGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.IdTextbox = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(352, 147);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 48;
            this.label3.Text = "UserId";
            // 
            // MovieId
            // 
            this.MovieId.AutoSize = true;
            this.MovieId.Location = new System.Drawing.Point(367, 228);
            this.MovieId.Name = "MovieId";
            this.MovieId.Size = new System.Drawing.Size(55, 18);
            this.MovieId.TabIndex = 47;
            this.MovieId.Text = "EventId";
            // 
            // EventIdTextbox
            // 
            this.EventIdTextbox.Location = new System.Drawing.Point(382, 249);
            this.EventIdTextbox.Name = "EventIdTextbox";
            this.EventIdTextbox.Size = new System.Drawing.Size(27, 26);
            this.EventIdTextbox.TabIndex = 46;
            // 
            // UserIdTextbox
            // 
            this.UserIdTextbox.Location = new System.Drawing.Point(375, 26);
            this.UserIdTextbox.Name = "UserIdTextbox";
            this.UserIdTextbox.Size = new System.Drawing.Size(27, 26);
            this.UserIdTextbox.TabIndex = 45;
            // 
            // UserGridView
            // 
            this.UserGridView.AllowUserToAddRows = false;
            this.UserGridView.AllowUserToDeleteRows = false;
            this.UserGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserGridView.Location = new System.Drawing.Point(375, 58);
            this.UserGridView.Name = "UserGridView";
            this.UserGridView.ReadOnly = true;
            this.UserGridView.Size = new System.Drawing.Size(316, 165);
            this.UserGridView.TabIndex = 44;
            this.UserGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserGridView_CellClick);
            // 
            // EventGridView
            // 
            this.EventGridView.AllowUserToAddRows = false;
            this.EventGridView.AllowUserToDeleteRows = false;
            this.EventGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventGridView.Location = new System.Drawing.Point(375, 281);
            this.EventGridView.Name = "EventGridView";
            this.EventGridView.ReadOnly = true;
            this.EventGridView.Size = new System.Drawing.Size(316, 135);
            this.EventGridView.TabIndex = 42;
            this.EventGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EventGridView_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 18);
            this.label1.TabIndex = 51;
            this.label1.Text = "BillId";
            // 
            // BillIdTextbox
            // 
            this.BillIdTextbox.Location = new System.Drawing.Point(117, 198);
            this.BillIdTextbox.Name = "BillIdTextbox";
            this.BillIdTextbox.Size = new System.Drawing.Size(27, 26);
            this.BillIdTextbox.TabIndex = 50;
            // 
            // BillGridView
            // 
            this.BillGridView.AllowUserToAddRows = false;
            this.BillGridView.AllowUserToDeleteRows = false;
            this.BillGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BillGridView.Location = new System.Drawing.Point(117, 230);
            this.BillGridView.Name = "BillGridView";
            this.BillGridView.ReadOnly = true;
            this.BillGridView.Size = new System.Drawing.Size(238, 147);
            this.BillGridView.TabIndex = 49;
            this.BillGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BillGridView_CellClick);
            this.BillGridView.Enter += new System.EventHandler(this.BillGridView_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 18);
            this.label2.TabIndex = 53;
            this.label2.Text = "Id";
            // 
            // IdTextbox
            // 
            this.IdTextbox.Enabled = false;
            this.IdTextbox.Location = new System.Drawing.Point(15, 197);
            this.IdTextbox.Name = "IdTextbox";
            this.IdTextbox.Size = new System.Drawing.Size(27, 26);
            this.IdTextbox.TabIndex = 52;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(15, 261);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 26);
            this.numericUpDown1.TabIndex = 54;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 18);
            this.label4.TabIndex = 55;
            this.label4.Text = "SeatNumber";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.DeleteButton.Location = new System.Drawing.Point(205, 384);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(104, 32);
            this.DeleteButton.TabIndex = 58;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.EditButton.Location = new System.Drawing.Point(112, 384);
            this.EditButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(85, 32);
            this.EditButton.TabIndex = 57;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.AddButton.Location = new System.Drawing.Point(4, 384);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(100, 32);
            this.AddButton.TabIndex = 56;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 18);
            this.label7.TabIndex = 59;
            this.label7.Text = "Reservations";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.button1.Location = new System.Drawing.Point(3, 294);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 53);
            this.button1.TabIndex = 60;
            this.button1.Text = "Empty Seat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ReservationsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IdTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BillIdTextbox);
            this.Controls.Add(this.BillGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MovieId);
            this.Controls.Add(this.EventIdTextbox);
            this.Controls.Add(this.UserIdTextbox);
            this.Controls.Add(this.UserGridView);
            this.Controls.Add(this.EventGridView);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReservationsControl";
            this.Size = new System.Drawing.Size(694, 420);
            this.Load += new System.EventHandler(this.ReservationsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MovieId;
        private System.Windows.Forms.TextBox EventIdTextbox;
        private System.Windows.Forms.TextBox UserIdTextbox;
        private System.Windows.Forms.DataGridView UserGridView;
        private System.Windows.Forms.DataGridView EventGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BillIdTextbox;
        private System.Windows.Forms.DataGridView BillGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IdTextbox;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}
