namespace SistemKasir
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtHargaBeli = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtHargaJual = new System.Windows.Forms.TextBox();
            this.Harga = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStok = new System.Windows.Forms.TextBox();
            this.txtSatuan = new System.Windows.Forms.TextBox();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTotalRecord = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kode Barang :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtKode
            // 
            this.txtKode.Location = new System.Drawing.Point(156, 39);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(100, 26);
            this.txtKode.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "btnSimpan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // txtHargaBeli
            // 
            this.txtHargaBeli.Location = new System.Drawing.Point(156, 93);
            this.txtHargaBeli.Name = "txtHargaBeli";
            this.txtHargaBeli.Size = new System.Drawing.Size(100, 26);
            this.txtHargaBeli.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nama Barang :";
            // 
            // dgvBarang
            // 
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarang.Location = new System.Drawing.Point(350, 48);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.RowHeadersWidth = 62;
            this.dgvBarang.RowTemplate.Height = 28;
            this.dgvBarang.Size = new System.Drawing.Size(1045, 347);
            this.dgvBarang.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Harga Beli :";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(156, 65);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(100, 26);
            this.txtNama.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(-1, 270);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 29);
            this.button2.TabIndex = 8;
            this.button2.Text = "btnUpdate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(108, 235);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 27);
            this.button3.TabIndex = 9;
            this.button3.Text = "btnHapus";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(108, 274);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 25);
            this.button4.TabIndex = 10;
            this.button4.Text = "btnRefresh";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtHargaJual
            // 
            this.txtHargaJual.Location = new System.Drawing.Point(156, 120);
            this.txtHargaJual.Name = "txtHargaJual";
            this.txtHargaJual.Size = new System.Drawing.Size(100, 26);
            this.txtHargaJual.TabIndex = 11;
            // 
            // Harga
            // 
            this.Harga.AutoSize = true;
            this.Harga.Location = new System.Drawing.Point(47, 123);
            this.Harga.Name = "Harga";
            this.Harga.Size = new System.Drawing.Size(94, 20);
            this.Harga.TabIndex = 12;
            this.Harga.Text = "Harga Jual :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Stok :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Satuan :";
            // 
            // txtStok
            // 
            this.txtStok.Location = new System.Drawing.Point(156, 147);
            this.txtStok.Name = "txtStok";
            this.txtStok.Size = new System.Drawing.Size(100, 26);
            this.txtStok.TabIndex = 15;
            // 
            // txtSatuan
            // 
            this.txtSatuan.Location = new System.Drawing.Point(156, 179);
            this.txtSatuan.Name = "txtSatuan";
            this.txtSatuan.Size = new System.Drawing.Size(100, 26);
            this.txtSatuan.TabIndex = 16;
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(531, 16);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(119, 26);
            this.txtCari.TabIndex = 17;
            this.txtCari.Click += new System.EventHandler(this.Form1_Load);
            this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(378, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Cari Nama Barang :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(517, 408);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(71, 20);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblTotalRecord
            // 
            this.lblTotalRecord.AutoSize = true;
            this.lblTotalRecord.Location = new System.Drawing.Point(604, 408);
            this.lblTotalRecord.Name = "lblTotalRecord";
            this.lblTotalRecord.Size = new System.Drawing.Size(111, 20);
            this.lblTotalRecord.TabIndex = 20;
            this.lblTotalRecord.Text = "lblTotalRecord";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTotalRecord);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.txtSatuan);
            this.Controls.Add(this.txtStok);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Harga);
            this.Controls.Add(this.txtHargaJual);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHargaBeli);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtKode);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtHargaBeli;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtHargaJual;
        private System.Windows.Forms.Label Harga;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStok;
        private System.Windows.Forms.TextBox txtSatuan;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTotalRecord;
    }
}

