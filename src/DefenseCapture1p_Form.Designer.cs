namespace nsForm
{
	partial class DefenseCapture1p_Form
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
			this.btUndo = new System.Windows.Forms.Button();
			this.btReset = new System.Windows.Forms.Button();
			this.btNewGame = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btConfirm1 = new System.Windows.Forms.Button();
			this.lbActionPoint = new System.Windows.Forms.Label();
			this.btDiscardHand = new System.Windows.Forms.Button();
			this.btConfirm2 = new System.Windows.Forms.Button();
			this.lbAreaHand = new System.Windows.Forms.Label();
			this.lbAreaMain = new System.Windows.Forms.Label();
			this.lbAreaDiscard = new System.Windows.Forms.Label();
			this.lbAreaDamaged = new System.Windows.Forms.Label();
			this.lbAreaCaptured = new System.Windows.Forms.Label();
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
			// btConfirm1
			// 
			this.btConfirm1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btConfirm1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btConfirm1.Location = new System.Drawing.Point(1120, 499);
			this.btConfirm1.Margin = new System.Windows.Forms.Padding(0);
			this.btConfirm1.Name = "btConfirm1";
			this.btConfirm1.Size = new System.Drawing.Size(100, 60);
			this.btConfirm1.TabIndex = 5;
			this.btConfirm1.Text = "Take Action";
			this.btConfirm1.UseVisualStyleBackColor = true;
			this.btConfirm1.Click += new System.EventHandler(this.btConfirm_Click);
			// 
			// lbActionPoint
			// 
			this.lbActionPoint.BackColor = System.Drawing.Color.Transparent;
			this.lbActionPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbActionPoint.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbActionPoint.ForeColor = System.Drawing.Color.Black;
			this.lbActionPoint.Location = new System.Drawing.Point(860, 570);
			this.lbActionPoint.Name = "lbActionPoint";
			this.lbActionPoint.Size = new System.Drawing.Size(124, 52);
			this.lbActionPoint.TabIndex = 2;
			this.lbActionPoint.Text = " AP:";
			this.lbActionPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btDiscardHand
			// 
			this.btDiscardHand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDiscardHand.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btDiscardHand.Location = new System.Drawing.Point(1120, 569);
			this.btDiscardHand.Margin = new System.Windows.Forms.Padding(0);
			this.btDiscardHand.Name = "btDiscardHand";
			this.btDiscardHand.Size = new System.Drawing.Size(100, 60);
			this.btDiscardHand.TabIndex = 5;
			this.btDiscardHand.Text = "Discard\r\nHand";
			this.btDiscardHand.UseVisualStyleBackColor = true;
			this.btDiscardHand.Click += new System.EventHandler(this.btDiscardHand_Click);
			// 
			// btConfirm2
			// 
			this.btConfirm2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btConfirm2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btConfirm2.Location = new System.Drawing.Point(1120, 430);
			this.btConfirm2.Margin = new System.Windows.Forms.Padding(0);
			this.btConfirm2.Name = "btConfirm2";
			this.btConfirm2.Size = new System.Drawing.Size(100, 60);
			this.btConfirm2.TabIndex = 5;
			this.btConfirm2.Text = "Withdraw";
			this.btConfirm2.UseVisualStyleBackColor = true;
			this.btConfirm2.Click += new System.EventHandler(this.btConfirm_Click);
			// 
			// lbAreaHand
			// 
			this.lbAreaHand.BackColor = System.Drawing.Color.Transparent;
			this.lbAreaHand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbAreaHand.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAreaHand.ForeColor = System.Drawing.Color.Black;
			this.lbAreaHand.Location = new System.Drawing.Point(270, 531);
			this.lbAreaHand.Name = "lbAreaHand";
			this.lbAreaHand.Size = new System.Drawing.Size(150, 30);
			this.lbAreaHand.TabIndex = 2;
			this.lbAreaHand.Text = "Your Hand";
			this.lbAreaHand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbAreaMain
			// 
			this.lbAreaMain.BackColor = System.Drawing.Color.Transparent;
			this.lbAreaMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbAreaMain.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAreaMain.ForeColor = System.Drawing.Color.Black;
			this.lbAreaMain.Location = new System.Drawing.Point(270, 561);
			this.lbAreaMain.Name = "lbAreaMain";
			this.lbAreaMain.Size = new System.Drawing.Size(150, 30);
			this.lbAreaMain.TabIndex = 2;
			this.lbAreaMain.Text = "Main Deck";
			this.lbAreaMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbAreaDiscard
			// 
			this.lbAreaDiscard.BackColor = System.Drawing.Color.Transparent;
			this.lbAreaDiscard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbAreaDiscard.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAreaDiscard.ForeColor = System.Drawing.Color.Black;
			this.lbAreaDiscard.Location = new System.Drawing.Point(270, 591);
			this.lbAreaDiscard.Name = "lbAreaDiscard";
			this.lbAreaDiscard.Size = new System.Drawing.Size(150, 30);
			this.lbAreaDiscard.TabIndex = 2;
			this.lbAreaDiscard.Text = "Discard Pile";
			this.lbAreaDiscard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbAreaDamaged
			// 
			this.lbAreaDamaged.BackColor = System.Drawing.Color.Transparent;
			this.lbAreaDamaged.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbAreaDamaged.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAreaDamaged.ForeColor = System.Drawing.Color.Black;
			this.lbAreaDamaged.Location = new System.Drawing.Point(426, 531);
			this.lbAreaDamaged.Name = "lbAreaDamaged";
			this.lbAreaDamaged.Size = new System.Drawing.Size(150, 30);
			this.lbAreaDamaged.TabIndex = 2;
			this.lbAreaDamaged.Text = "Damaged Area";
			this.lbAreaDamaged.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbAreaCaptured
			// 
			this.lbAreaCaptured.BackColor = System.Drawing.Color.Transparent;
			this.lbAreaCaptured.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lbAreaCaptured.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAreaCaptured.ForeColor = System.Drawing.Color.Black;
			this.lbAreaCaptured.Location = new System.Drawing.Point(426, 561);
			this.lbAreaCaptured.Name = "lbAreaCaptured";
			this.lbAreaCaptured.Size = new System.Drawing.Size(150, 30);
			this.lbAreaCaptured.TabIndex = 2;
			this.lbAreaCaptured.Text = "Captured Area";
			this.lbAreaCaptured.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DefenseCapture1p_Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(123)))));
			this.ClientSize = new System.Drawing.Size(1262, 653);
			this.Controls.Add(this.btDiscardHand);
			this.Controls.Add(this.btConfirm2);
			this.Controls.Add(this.btConfirm1);
			this.Controls.Add(this.btSettings);
			this.Controls.Add(this.btNewGame);
			this.Controls.Add(this.btReset);
			this.Controls.Add(this.btUndo);
			this.Controls.Add(this.lbAreaCaptured);
			this.Controls.Add(this.lbAreaDamaged);
			this.Controls.Add(this.lbAreaDiscard);
			this.Controls.Add(this.lbAreaMain);
			this.Controls.Add(this.lbAreaHand);
			this.Controls.Add(this.lbActionPoint);
			this.Controls.Add(this.lbStatusWin);
			this.Controls.Add(this.pictureBox1);
			this.KeyPreview = true;
			this.Name = "DefenseCapture1p_Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Defend and Capture";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefenseCapture1p_Form_FormClosing);
			this.Load += new System.EventHandler(this.DefenseCapture1p_Form_Load);
			this.SizeChanged += new System.EventHandler(this.DefenseCapture1p_Form_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DefenseCapture1p_Form_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer tiUI;
		private System.Windows.Forms.Label lbStatusWin;
		private System.Windows.Forms.Button btSettings;
		private System.Windows.Forms.Button btUndo;
		private System.Windows.Forms.Button btReset;
		private System.Windows.Forms.Button btNewGame;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btConfirm1;
		private System.Windows.Forms.Label lbActionPoint;
		private System.Windows.Forms.Button btDiscardHand;
		private System.Windows.Forms.Button btConfirm2;
		private System.Windows.Forms.Label lbAreaHand;
		private System.Windows.Forms.Label lbAreaMain;
		private System.Windows.Forms.Label lbAreaDiscard;
		private System.Windows.Forms.Label lbAreaDamaged;
		private System.Windows.Forms.Label lbAreaCaptured;
	}
}