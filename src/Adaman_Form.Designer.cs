namespace nsForm
{
	partial class Adaman_Form
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
			this.btConfirm = new System.Windows.Forms.Button();
			this.btTest = new System.Windows.Forms.Button();
			this.lbStatusCardsLeft = new System.Windows.Forms.Label();
			this.btSuggest = new System.Windows.Forms.Button();
			this.btNewGame = new System.Windows.Forms.Button();
			this.btUndo = new System.Windows.Forms.Button();
			this.btReset = new System.Windows.Forms.Button();
			this.lbStatusWin = new System.Windows.Forms.Label();
			this.lbStatusPersonLeft = new System.Windows.Forms.Label();
			this.btSettings = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// tiUI
			// 
			this.tiUI.Interval = 10;
			this.tiUI.Tick += new System.EventHandler(this.tiUI_Tick);
			// 
			// btConfirm
			// 
			this.btConfirm.Enabled = false;
			this.btConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btConfirm.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btConfirm.Location = new System.Drawing.Point(815, 566);
			this.btConfirm.Margin = new System.Windows.Forms.Padding(5);
			this.btConfirm.Name = "btConfirm";
			this.btConfirm.Size = new System.Drawing.Size(100, 60);
			this.btConfirm.TabIndex = 0;
			this.btConfirm.Text = "Confirm";
			this.btConfirm.UseVisualStyleBackColor = true;
			this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
			// 
			// btTest
			// 
			this.btTest.Location = new System.Drawing.Point(1231, 305);
			this.btTest.Margin = new System.Windows.Forms.Padding(5);
			this.btTest.Name = "btTest";
			this.btTest.Size = new System.Drawing.Size(101, 41);
			this.btTest.TabIndex = 1;
			this.btTest.Text = "test";
			this.btTest.UseVisualStyleBackColor = true;
			this.btTest.Click += new System.EventHandler(this.btTest_Click);
			// 
			// lbStatusCardsLeft
			// 
			this.lbStatusCardsLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.lbStatusCardsLeft.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStatusCardsLeft.Location = new System.Drawing.Point(18, 250);
			this.lbStatusCardsLeft.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbStatusCardsLeft.Name = "lbStatusCardsLeft";
			this.lbStatusCardsLeft.Size = new System.Drawing.Size(150, 30);
			this.lbStatusCardsLeft.TabIndex = 2;
			this.lbStatusCardsLeft.Text = " ";
			this.lbStatusCardsLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btSuggest
			// 
			this.btSuggest.BackColor = System.Drawing.Color.Gold;
			this.btSuggest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSuggest.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSuggest.Location = new System.Drawing.Point(815, 478);
			this.btSuggest.Margin = new System.Windows.Forms.Padding(5);
			this.btSuggest.Name = "btSuggest";
			this.btSuggest.Size = new System.Drawing.Size(100, 30);
			this.btSuggest.TabIndex = 3;
			this.btSuggest.Text = "Hint";
			this.btSuggest.UseVisualStyleBackColor = false;
			this.btSuggest.Click += new System.EventHandler(this.btSuggest_Click);
			// 
			// btNewGame
			// 
			this.btNewGame.BackColor = System.Drawing.Color.Gold;
			this.btNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btNewGame.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btNewGame.Location = new System.Drawing.Point(815, 265);
			this.btNewGame.Margin = new System.Windows.Forms.Padding(5);
			this.btNewGame.Name = "btNewGame";
			this.btNewGame.Size = new System.Drawing.Size(100, 30);
			this.btNewGame.TabIndex = 3;
			this.btNewGame.Text = "New Game";
			this.btNewGame.UseVisualStyleBackColor = false;
			this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
			// 
			// btUndo
			// 
			this.btUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.btUndo.Enabled = false;
			this.btUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btUndo.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btUndo.Location = new System.Drawing.Point(815, 438);
			this.btUndo.Margin = new System.Windows.Forms.Padding(5);
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
			this.btReset.Location = new System.Drawing.Point(815, 305);
			this.btReset.Margin = new System.Windows.Forms.Padding(5);
			this.btReset.Name = "btReset";
			this.btReset.Size = new System.Drawing.Size(100, 30);
			this.btReset.TabIndex = 3;
			this.btReset.Text = "Replay";
			this.btReset.UseVisualStyleBackColor = false;
			this.btReset.Click += new System.EventHandler(this.btReset_Click);
			// 
			// lbStatusWin
			// 
			this.lbStatusWin.BackColor = System.Drawing.Color.White;
			this.lbStatusWin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbStatusWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbStatusWin.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStatusWin.ForeColor = System.Drawing.Color.Black;
			this.lbStatusWin.Location = new System.Drawing.Point(270, 109);
			this.lbStatusWin.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbStatusWin.Name = "lbStatusWin";
			this.lbStatusWin.Size = new System.Drawing.Size(580, 78);
			this.lbStatusWin.TabIndex = 2;
			this.lbStatusWin.Text = " ";
			this.lbStatusWin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lbStatusWin.Visible = false;
			// 
			// lbStatusPersonLeft
			// 
			this.lbStatusPersonLeft.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStatusPersonLeft.Location = new System.Drawing.Point(18, 530);
			this.lbStatusPersonLeft.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbStatusPersonLeft.Name = "lbStatusPersonLeft";
			this.lbStatusPersonLeft.Size = new System.Drawing.Size(150, 30);
			this.lbStatusPersonLeft.TabIndex = 2;
			this.lbStatusPersonLeft.Text = " ";
			this.lbStatusPersonLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btSettings
			// 
			this.btSettings.BackColor = System.Drawing.Color.Gold;
			this.btSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSettings.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSettings.Location = new System.Drawing.Point(815, 225);
			this.btSettings.Margin = new System.Windows.Forms.Padding(5);
			this.btSettings.Name = "btSettings";
			this.btSettings.Size = new System.Drawing.Size(100, 30);
			this.btSettings.TabIndex = 3;
			this.btSettings.Text = "Settings";
			this.btSettings.UseVisualStyleBackColor = false;
			this.btSettings.Click += new System.EventHandler(this.btSettings_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::nsForm.Properties.Resources.table;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(796, 589);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// Adaman_Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.ClientSize = new System.Drawing.Size(1132, 653);
			this.Controls.Add(this.btSettings);
			this.Controls.Add(this.btNewGame);
			this.Controls.Add(this.btReset);
			this.Controls.Add(this.btUndo);
			this.Controls.Add(this.btSuggest);
			this.Controls.Add(this.lbStatusWin);
			this.Controls.Add(this.lbStatusPersonLeft);
			this.Controls.Add(this.lbStatusCardsLeft);
			this.Controls.Add(this.btTest);
			this.Controls.Add(this.btConfirm);
			this.Controls.Add(this.pictureBox1);
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "Adaman_Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Adaman";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer tiUI;
		private System.Windows.Forms.Button btConfirm;
		private System.Windows.Forms.Button btTest;
		private System.Windows.Forms.Label lbStatusCardsLeft;
		private System.Windows.Forms.Button btSuggest;
		private System.Windows.Forms.Button btNewGame;
		private System.Windows.Forms.Button btUndo;
		private System.Windows.Forms.Button btReset;
		private System.Windows.Forms.Label lbStatusWin;
		private System.Windows.Forms.Label lbStatusPersonLeft;
		private System.Windows.Forms.Button btSettings;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}