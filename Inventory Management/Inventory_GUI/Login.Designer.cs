namespace Inventory_Management
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Username = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Password = new System.Windows.Forms.TextBox();
            this.Login_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Title.Font = new System.Drawing.Font("Righteous", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(440, 40);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(583, 63);
            this.Title.TabIndex = 0;
            this.Title.Text = "Inventory Management";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(24, 184);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 8);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 25.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(296, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 51);
            this.label1.TabIndex = 3;
            this.label1.Text = "Login";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(432, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 8);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.Username);
            this.panel3.ForeColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(200, 240);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(320, 88);
            this.panel3.TabIndex = 4;
            // 
            // Username
            // 
            this.Username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Username.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.Username.Location = new System.Drawing.Point(16, 24);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(288, 40);
            this.Username.TabIndex = 6;
            this.Username.TabStop = false;
            this.Username.Text = "Username";
            this.Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.Password);
            this.panel4.ForeColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(200, 360);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(320, 88);
            this.panel4.TabIndex = 5;
            // 
            // Password
            // 
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Password.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.Password.Location = new System.Drawing.Point(16, 24);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(288, 40);
            this.Password.TabIndex = 7;
            this.Password.TabStop = false;
            this.Password.Text = "Password";
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Login_Button
            // 
            this.Login_Button.Font = new System.Drawing.Font("Roboto", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_Button.ForeColor = System.Drawing.Color.Black;
            this.Login_Button.Image = ((System.Drawing.Image)(resources.GetObject("Login_Button.Image")));
            this.Login_Button.Location = new System.Drawing.Point(296, 480);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(128, 64);
            this.Login_Button.TabIndex = 7;
            this.Login_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(728, 128);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(672, 440);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(160)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1432, 653);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrackerX";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

