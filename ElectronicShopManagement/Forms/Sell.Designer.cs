namespace ElectronicShopManagement.Forms
{
    partial class Sell
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
            this.txtsellqty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtsellprice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboboxsell = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboboxsellproname = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnpayment = new System.Windows.Forms.Button();
            this.tblshowproductsell = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtamount = new System.Windows.Forms.Label();
            this.combocashier = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblshowproductsell)).BeginInit();
            this.SuspendLayout();
            // 
            // txtsellqty
            // 
            this.txtsellqty.Location = new System.Drawing.Point(779, 106);
            this.txtsellqty.Name = "txtsellqty";
            this.txtsellqty.Size = new System.Drawing.Size(129, 20);
            this.txtsellqty.TabIndex = 29;
            this.txtsellqty.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(642, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 19);
            this.label6.TabIndex = 28;
            this.label6.Text = "Product Quantity";
            // 
            // txtsellprice
            // 
            this.txtsellprice.Location = new System.Drawing.Point(779, 46);
            this.txtsellprice.Name = "txtsellprice";
            this.txtsellprice.Size = new System.Drawing.Size(129, 20);
            this.txtsellprice.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(642, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 19);
            this.label5.TabIndex = 26;
            this.label5.Text = "Product Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(278, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "Product Name";
            // 
            // comboboxsell
            // 
            this.comboboxsell.FormattingEnabled = true;
            this.comboboxsell.Location = new System.Drawing.Point(415, 46);
            this.comboboxsell.Name = "comboboxsell";
            this.comboboxsell.Size = new System.Drawing.Size(171, 21);
            this.comboboxsell.TabIndex = 23;
            this.comboboxsell.SelectedIndexChanged += new System.EventHandler(this.comboboxsell_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(278, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "Category";
            // 
            // comboboxsellproname
            // 
            this.comboboxsellproname.FormattingEnabled = true;
            this.comboboxsellproname.Location = new System.Drawing.Point(415, 105);
            this.comboboxsellproname.Name = "comboboxsellproname";
            this.comboboxsellproname.Size = new System.Drawing.Size(171, 21);
            this.comboboxsellproname.TabIndex = 30;
            this.comboboxsellproname.SelectedIndexChanged += new System.EventHandler(this.comboboxsellproname_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(622, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 29);
            this.button1.TabIndex = 31;
            this.button1.Text = "Add To Card";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(779, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 29);
            this.button2.TabIndex = 32;
            this.button2.Text = "Cancel Product";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnpayment
            // 
            this.btnpayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpayment.Location = new System.Drawing.Point(1000, 490);
            this.btnpayment.Name = "btnpayment";
            this.btnpayment.Size = new System.Drawing.Size(88, 29);
            this.btnpayment.TabIndex = 33;
            this.btnpayment.Text = "Pay Now!";
            this.btnpayment.UseVisualStyleBackColor = true;
            this.btnpayment.Click += new System.EventHandler(this.btnpayment_Click);
            // 
            // tblshowproductsell
            // 
            this.tblshowproductsell.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tblshowproductsell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblshowproductsell.Location = new System.Drawing.Point(12, 216);
            this.tblshowproductsell.Name = "tblshowproductsell";
            this.tblshowproductsell.Size = new System.Drawing.Size(1140, 242);
            this.tblshowproductsell.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(845, 490);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 26);
            this.label1.TabIndex = 35;
            this.label1.Text = "Total:";
            // 
            // txtamount
            // 
            this.txtamount.AutoSize = true;
            this.txtamount.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtamount.Location = new System.Drawing.Point(902, 490);
            this.txtamount.Name = "txtamount";
            this.txtamount.Size = new System.Drawing.Size(59, 26);
            this.txtamount.TabIndex = 36;
            this.txtamount.Text = "0.00 ";
            this.txtamount.Click += new System.EventHandler(this.txtamount_Click);
            // 
            // combocashier
            // 
            this.combocashier.FormattingEnabled = true;
            this.combocashier.Items.AddRange(new object[] {
            "Theany",
            "Vuth",
            "Tak",
            "Tra"});
            this.combocashier.Location = new System.Drawing.Point(415, 170);
            this.combocashier.Name = "combocashier";
            this.combocashier.Size = new System.Drawing.Size(171, 21);
            this.combocashier.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(278, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "Cashier Name";
            // 
            // Sell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 639);
            this.Controls.Add(this.combocashier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtamount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tblshowproductsell);
            this.Controls.Add(this.btnpayment);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboboxsellproname);
            this.Controls.Add(this.txtsellqty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtsellprice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboboxsell);
            this.Controls.Add(this.label3);
            this.Name = "Sell";
            this.Text = "Sell";
            this.Load += new System.EventHandler(this.Sell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblshowproductsell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtsellqty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtsellprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboboxsell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboboxsellproname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnpayment;
        private System.Windows.Forms.DataGridView tblshowproductsell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtamount;
        private System.Windows.Forms.ComboBox combocashier;
        private System.Windows.Forms.Label label2;
    }
}