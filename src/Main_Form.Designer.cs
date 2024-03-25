namespace nsForm
{
	partial class Main_Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
			this.btAdaman = new System.Windows.Forms.Button();
			this.lbCredit = new System.Windows.Forms.Label();
			this.btMyrmex = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btAdaman
			// 
			this.btAdaman.BackColor = System.Drawing.Color.Gold;
			this.btAdaman.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btAdaman.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btAdaman.Location = new System.Drawing.Point(74, 42);
			this.btAdaman.Name = "btAdaman";
			this.btAdaman.Size = new System.Drawing.Size(150, 30);
			this.btAdaman.TabIndex = 3;
			this.btAdaman.Text = "Adaman";
			this.btAdaman.UseVisualStyleBackColor = false;
			this.btAdaman.Click += new System.EventHandler(this.btAdaman_Click);
			// 
			// lbCredit
			// 
			this.lbCredit.AutoSize = true;
			this.lbCredit.BackColor = System.Drawing.Color.Transparent;
			this.lbCredit.Location = new System.Drawing.Point(12, 155);
			this.lbCredit.Name = "lbCredit";
			this.lbCredit.Size = new System.Drawing.Size(302, 102);
			this.lbCredit.TabIndex = 4;
			this.lbCredit.Text = resources.GetString("lbCredit.Text");
			// 
			// btMyrmex
			// 
			this.btMyrmex.BackColor = System.Drawing.Color.Gold;
			this.btMyrmex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btMyrmex.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btMyrmex.Location = new System.Drawing.Point(74, 78);
			this.btMyrmex.Name = "btMyrmex";
			this.btMyrmex.Size = new System.Drawing.Size(150, 30);
			this.btMyrmex.TabIndex = 3;
			this.btMyrmex.Text = "Myrmex";
			this.btMyrmex.UseVisualStyleBackColor = false;
			this.btMyrmex.Click += new System.EventHandler(this.btMyrmex_Click);
			// 
			// Main_Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.BackgroundImage = global::nsForm.Properties.Resources.table;
			this.ClientSize = new System.Drawing.Size(309, 251);
			this.Controls.Add(this.lbCredit);
			this.Controls.Add(this.btMyrmex);
			this.Controls.Add(this.btAdaman);
			this.KeyPreview = true;
			this.Name = "Main_Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "A Few Decktet Games";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btAdaman;
		private System.Windows.Forms.Label lbCredit;
		private System.Windows.Forms.Button btMyrmex;
	}
}