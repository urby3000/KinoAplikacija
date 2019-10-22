namespace KinoAplikacija.User_Controls.MainPanels.Normal
{
    partial class UserEventsControl
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
            this.EventsLabel = new System.Windows.Forms.Label();
            this.EventFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ShoppingCartLabel = new System.Windows.Forms.Label();
            this.ShoppingCartFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckoutButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // EventsLabel
            // 
            this.EventsLabel.AutoSize = true;
            this.EventsLabel.Location = new System.Drawing.Point(4, 4);
            this.EventsLabel.Name = "EventsLabel";
            this.EventsLabel.Size = new System.Drawing.Size(49, 18);
            this.EventsLabel.TabIndex = 0;
            this.EventsLabel.Text = "Events";
            // 
            // EventFlowPanel
            // 
            this.EventFlowPanel.AutoScroll = true;
            this.EventFlowPanel.Location = new System.Drawing.Point(7, 25);
            this.EventFlowPanel.Name = "EventFlowPanel";
            this.EventFlowPanel.Size = new System.Drawing.Size(643, 437);
            this.EventFlowPanel.TabIndex = 1;
            this.EventFlowPanel.WrapContents = false;
            // 
            // ShoppingCartLabel
            // 
            this.ShoppingCartLabel.AutoSize = true;
            this.ShoppingCartLabel.Location = new System.Drawing.Point(656, 4);
            this.ShoppingCartLabel.Name = "ShoppingCartLabel";
            this.ShoppingCartLabel.Size = new System.Drawing.Size(94, 18);
            this.ShoppingCartLabel.TabIndex = 2;
            this.ShoppingCartLabel.Text = "Shopping Cart";
            // 
            // ShoppingCartFlowPanel
            // 
            this.ShoppingCartFlowPanel.AutoScroll = true;
            this.ShoppingCartFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ShoppingCartFlowPanel.Location = new System.Drawing.Point(659, 25);
            this.ShoppingCartFlowPanel.Name = "ShoppingCartFlowPanel";
            this.ShoppingCartFlowPanel.Size = new System.Drawing.Size(249, 367);
            this.ShoppingCartFlowPanel.TabIndex = 3;
            // 
            // CheckoutButton
            // 
            this.CheckoutButton.Location = new System.Drawing.Point(720, 430);
            this.CheckoutButton.Name = "CheckoutButton";
            this.CheckoutButton.Size = new System.Drawing.Size(132, 29);
            this.CheckoutButton.TabIndex = 4;
            this.CheckoutButton.Text = "Go To Checkout";
            this.CheckoutButton.UseVisualStyleBackColor = true;
            this.CheckoutButton.Click += new System.EventHandler(this.CheckoutButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(720, 398);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(132, 26);
            this.textBox1.TabIndex = 5;
            // 
            // UserEventsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CheckoutButton);
            this.Controls.Add(this.ShoppingCartFlowPanel);
            this.Controls.Add(this.ShoppingCartLabel);
            this.Controls.Add(this.EventFlowPanel);
            this.Controls.Add(this.EventsLabel);
            this.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserEventsControl";
            this.Size = new System.Drawing.Size(916, 465);
            this.Load += new System.EventHandler(this.UserEventsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EventsLabel;
        private System.Windows.Forms.FlowLayoutPanel EventFlowPanel;
        private System.Windows.Forms.Label ShoppingCartLabel;
        private System.Windows.Forms.FlowLayoutPanel ShoppingCartFlowPanel;
        private System.Windows.Forms.Button CheckoutButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}
