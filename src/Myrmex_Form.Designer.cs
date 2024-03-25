namespace nsForm
{
	partial class Myrmex_Form
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
			this.components = new System.ComponentModel.Container();
			this.tiUI = new System.Windows.Forms.Timer(this.components);
			this.lbStatusWin = new System.Windows.Forms.Label();
			this.btSettings = new System.Windows.Forms.Button();
			this.btSuggest = new System.Windows.Forms.Button();
			this.btUndo = new System.Windows.Forms.Button();
			this.btReset = new System.Windows.Forms.Button();
			this.btNewGame = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// tiUI
			// 
			this.tiUI.Interval = 10;
			this.tiUI.Tick += new System.EventHandler(this.tiUI_Tick);
			// 
			// lbStatusWin
			// 
			this.lbStatusWin.BackColor = System.Drawing.Color.Transparent;
			this.lbStatusWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbStatusWin.Font = new System.Drawing.Font("Times New Roman", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStatusWin.ForeColor = System.Drawing.Color.Gold;
			this.lbStatusWin.Location = new System.Drawing.Point(12, 430);
			this.lbStatusWin.Name = "lbStatusWin";
			this.lbStatusWin.Size = new System.Drawing.Size(642, 52);
			this.lbStatusWin.TabIndex = 2;
			this.lbStatusWin.Text = " ";
			this.lbStatusWin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbStatusWin.Visible = false;
			// 
			// btSettings
			// 
			this.btSettings.BackColor = System.Drawing.Color.Gold;
			this.btSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSettings.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSettings.Location = new System.Drawing.Point(13, 12);
			this.btSettings.Name = "btSettings";
			this.btSettings.Size = new System.Drawing.Size(100, 30);
			this.btSettings.TabIndex = 3;
			this.btSettings.Text = "Settings";
			this.btSettings.UseVisualStyleBackColor = false;
			this.btSettings.Click += new System.EventHandler(this.btSettings_Click);
			// 
			// btSuggest
			// 
			this.btSuggest.BackColor = System.Drawing.Color.Gold;
			this.btSuggest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSuggest.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSuggest.Location = new System.Drawing.Point(253, 600);
			this.btSuggest.Name = "btSuggest";
			this.btSuggest.Size = new System.Drawing.Size(100, 30);
			this.btSuggest.TabIndex = 3;
			this.btSuggest.Text = "Hint";
			this.btSuggest.UseVisualStyleBackColor = false;
			this.btSuggest.Visible = false;
			this.btSuggest.Click += new System.EventHandler(this.btSuggest_Click);
			// 
			// btUndo
			// 
			this.btUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.btUndo.Enabled = false;
			this.btUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btUndo.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btUndo.Location = new System.Drawing.Point(135, 600);
			this.btUndo.Name = "btUndo";
			this.btUndo.Size = new System.Drawing.Size(100, 30);
			this.btUndo.TabIndex = 3;
			this.btUndo.Text = "Undo";
			this.btUndo.UseVisualStyleBackColor = false;
			this.btUndo.Click += new System.EventHandler(this.btUndo_Click);
			// 
			// btReset
			// 
			this.btReset.BackColor = System.Drawing.Color.Gold;
			this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReset.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btReset.Location = new System.Drawing.Point(13, 84);
			this.btReset.Name = "btReset";
			this.btReset.Size = new System.Drawing.Size(100, 30);
			this.btReset.TabIndex = 3;
			this.btReset.Text = "Replay";
			this.btReset.UseVisualStyleBackColor = false;
			this.btReset.Click += new System.EventHandler(this.btReset_Click);
			// 
			// btNewGame
			// 
			this.btNewGame.BackColor = System.Drawing.Color.Gold;
			this.btNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btNewGame.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btNewGame.Location = new System.Drawing.Point(13, 48);
			this.btNewGame.Name = "btNewGame";
			this.btNewGame.Size = new System.Drawing.Size(100, 30);
			this.btNewGame.TabIndex = 3;
			this.btNewGame.Text = "New Game";
			this.btNewGame.UseVisualStyleBackColor = false;
			this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::nsForm.Properties.Resources.table;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(796, 499);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// Myrmex_Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.ClientSize = new System.Drawing.Size(1262, 653);
			this.Controls.Add(this.btSettings);
			this.Controls.Add(this.btNewGame);
			this.Controls.Add(this.btReset);
			this.Controls.Add(this.btUndo);
			this.Controls.Add(this.btSuggest);
			this.Controls.Add(this.lbStatusWin);
			this.Controls.Add(this.pictureBox1);
			this.KeyPreview = true;
			this.Name = "Myrmex_Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Myrmex";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Myrmex_Form_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Myrmex_Form_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer tiUI;
		private System.Windows.Forms.Label lbStatusWin;
		private System.Windows.Forms.Button btSettings;
		private System.Windows.Forms.Button btSuggest;
		private System.Windows.Forms.Button btUndo;
		private System.Windows.Forms.Button btReset;
		private System.Windows.Forms.Button btNewGame;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}