namespace KinoAplikacija.User_Controls
{
    partial class UserSidePanel
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ProfileButton = new System.Windows.Forms.Button();
            this.EventsButton = new System.Windows.Forms.Button();
            this.ReservationsBillsButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ProfileButton);
            this.flowLayoutPanel1.Controls.Add(this.EventsButton);
            this.flowLayoutPanel1.Controls.Add(this.ReservationsBillsButton);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(249, 121);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // ProfileButton
            // 
            this.ProfileButton.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ProfileButton.Location = new System.Drawing.Point(4, 4);
            this.ProfileButton.Margin = new System.Windows.Forms.Padding(4);
            this.ProfileButton.Name = "ProfileButton";
            this.ProfileButton.Size = new System.Drawing.Size(245, 32);
            this.ProfileButton.TabIndex = 7;
            this.ProfileButton.Text = "Profile";
            this.ProfileButton.UseVisualStyleBackColor = true;
            this.ProfileButton.Click += new System.EventHandler(this.ProfileButton_Click);
            // 
            // EventsButton
            // 
            this.EventsButton.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.EventsButton.Location = new System.Drawing.Point(4, 44);
            this.EventsButton.Margin = new System.Windows.Forms.Padding(4);
            this.EventsButton.Name = "EventsButton";
            this.EventsButton.Size = new System.Drawing.Size(245, 32);
            this.EventsButton.TabIndex = 8;
            this.EventsButton.Text = "Events";
            this.EventsButton.UseVisualStyleBackColor = true;
            this.EventsButton.Click += new System.EventHandler(this.EventsButton_Click);
            // 
            // ReservationsBillsButton
            // 
            this.ReservationsBillsButton.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ReservationsBillsButton.Location = new System.Drawing.Point(4, 84);
            this.ReservationsBillsButton.Margin = new System.Windows.Forms.Padding(4);
            this.ReservationsBillsButton.Name = "ReservationsBillsButton";
            this.ReservationsBillsButton.Size = new System.Drawing.Size(245, 32);
            this.ReservationsBillsButton.TabIndex = 9;
            this.ReservationsBillsButton.Text = "Bills, Reservations";
            this.ReservationsBillsButton.UseVisualStyleBackColor = true;
            this.ReservationsBillsButton.Click += new System.EventHandler(this.ReservationsBillsButton_Click);
            // 
            // UserSidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserSidePanel";
            this.Size = new System.Drawing.Size(257, 129);
            this.Load += new System.EventHandler(this.UserSidePanel_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ProfileButton;
        private System.Windows.Forms.Button EventsButton;
        private System.Windows.Forms.Button ReservationsBillsButton;
    }
}
