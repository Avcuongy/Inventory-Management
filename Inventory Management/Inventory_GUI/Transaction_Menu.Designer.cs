namespace Inventory_Management
{
    partial class Transaction_Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transaction_Menu));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Supplier_Transaction = new System.Windows.Forms.Button();
            this.button_Return_Order = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.tBx_Search = new System.Windows.Forms.TextBox();
            this.dGV_Transaction = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.return_Profile = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Transaction)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.return_Profile)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panel1.Controls.Add(this.button_Supplier_Transaction);
            this.panel1.Controls.Add(this.button_Return_Order);
            this.panel1.Controls.Add(this.button_Search);
            this.panel1.Controls.Add(this.tBx_Search);
            this.panel1.Controls.Add(this.dGV_Transaction);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(80, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1336, 624);
            this.panel1.TabIndex = 25;
            // 
            // button_Supplier_Transaction
            // 
            this.button_Supplier_Transaction.BackColor = System.Drawing.Color.White;
            this.button_Supplier_Transaction.Font = new System.Drawing.Font("Roboto", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Supplier_Transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.button_Supplier_Transaction.Location = new System.Drawing.Point(826, 82);
            this.button_Supplier_Transaction.Name = "button_Supplier_Transaction";
            this.button_Supplier_Transaction.Size = new System.Drawing.Size(240, 48);
            this.button_Supplier_Transaction.TabIndex = 12;
            this.button_Supplier_Transaction.TabStop = false;
            this.button_Supplier_Transaction.Text = "Supplier Order";
            this.button_Supplier_Transaction.UseVisualStyleBackColor = false;
            this.button_Supplier_Transaction.Click += new System.EventHandler(this.button_Supplier_Transaction_Click);
            // 
            // button_Return_Order
            // 
            this.button_Return_Order.BackColor = System.Drawing.Color.White;
            this.button_Return_Order.Font = new System.Drawing.Font("Roboto", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Return_Order.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.button_Return_Order.Location = new System.Drawing.Point(1072, 82);
            this.button_Return_Order.Name = "button_Return_Order";
            this.button_Return_Order.Size = new System.Drawing.Size(240, 48);
            this.button_Return_Order.TabIndex = 11;
            this.button_Return_Order.TabStop = false;
            this.button_Return_Order.Text = "Return Order";
            this.button_Return_Order.UseVisualStyleBackColor = false;
            this.button_Return_Order.Click += new System.EventHandler(this.Return_Order_Click);
            // 
            // button_Search
            // 
            this.button_Search.BackColor = System.Drawing.Color.Transparent;
            this.button_Search.Image = ((System.Drawing.Image)(resources.GetObject("button_Search.Image")));
            this.button_Search.Location = new System.Drawing.Point(312, 80);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(64, 48);
            this.button_Search.TabIndex = 10;
            this.button_Search.UseVisualStyleBackColor = false;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // tBx_Search
            // 
            this.tBx_Search.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBx_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.tBx_Search.Location = new System.Drawing.Point(24, 80);
            this.tBx_Search.Name = "tBx_Search";
            this.tBx_Search.Size = new System.Drawing.Size(280, 47);
            this.tBx_Search.TabIndex = 9;
            this.tBx_Search.Text = "Search";
            this.tBx_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dGV_Transaction
            // 
            this.dGV_Transaction.AllowUserToAddRows = false;
            this.dGV_Transaction.AllowUserToDeleteRows = false;
            this.dGV_Transaction.AllowUserToResizeColumns = false;
            this.dGV_Transaction.AllowUserToResizeRows = false;
            this.dGV_Transaction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGV_Transaction.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGV_Transaction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGV_Transaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGV_Transaction.DefaultCellStyle = dataGridViewCellStyle2;
            this.dGV_Transaction.Location = new System.Drawing.Point(24, 136);
            this.dGV_Transaction.Name = "dGV_Transaction";
            this.dGV_Transaction.RowHeadersVisible = false;
            this.dGV_Transaction.RowHeadersWidth = 51;
            this.dGV_Transaction.RowTemplate.Height = 24;
            this.dGV_Transaction.Size = new System.Drawing.Size(1288, 472);
            this.dGV_Transaction.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(19)))), ((int)(((byte)(1)))));
            this.panel3.Location = new System.Drawing.Point(24, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1288, 8);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(19)))), ((int)(((byte)(1)))));
            this.panel2.Location = new System.Drawing.Point(0, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1288, 10);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(101)))));
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 656);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // return_Profile
            // 
            this.return_Profile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(160)))), ((int)(((byte)(224)))));
            this.return_Profile.Image = ((System.Drawing.Image)(resources.GetObject("return_Profile.Image")));
            this.return_Profile.Location = new System.Drawing.Point(0, 300);
            this.return_Profile.Name = "return_Profile";
            this.return_Profile.Size = new System.Drawing.Size(64, 50);
            this.return_Profile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.return_Profile.TabIndex = 27;
            this.return_Profile.TabStop = false;
            this.return_Profile.Click += new System.EventHandler(this.return_Profile_Click);
            // 
            // Transaction_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1432, 653);
            this.Controls.Add(this.return_Profile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Transaction_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction_Menu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Transaction)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.return_Profile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dGV_Transaction;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox return_Profile;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox tBx_Search;
        private System.Windows.Forms.Button button_Return_Order;
        private System.Windows.Forms.Button button_Supplier_Transaction;
    }
}