using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using nsDecktet;

namespace nsForm
{
	public partial class Myrmex_Form : Form
	{
		Myrmex Myrmex;
		List<Myrmex> History = new List<Myrmex>();

		List<UIButton> BackSlot = new List<UIButton>();
		private void NewGame(bool seeded = false)
		{
			Myrmex = new Myrmex();
			Myrmex.Setup(seeded);
		}

		public Myrmex_Form()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(Myrmex_Config));
				FileStream fs = new FileStream("./myrmex_config.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				Myrmex_Config mc = (Myrmex_Config)xmls.Deserialize(sr);
				sr.Close();
				fs.Close();
			}
			catch
			{
			}

			for (int c = 0; c < 12; c++)
			{
				UIButton bt = new UIButton();
				bt.Button = new RoundedButton();
				bt.Button.Size = new Size(95, 133);
				bt.Button.Location = new Point(176 + c * 109 - bt.Button.Width / 2, 80 - bt.Button.Height / 2);
				bt.Button.FlatStyle = FlatStyle.Flat;
				bt.Button.FlatAppearance.BorderSize = 1;
				bt.Button.FlatAppearance.BorderColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.BackColor = Color.FromArgb(255, 178, 206, 170);
				this.Controls.Add(bt.Button);
				bt.Button.SendToBack();
				BackSlot.Add(bt);
			}

			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(Myrmex));
				FileStream fs = new FileStream("./myrmex_save.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				Myrmex = (Myrmex)xmls.Deserialize(sr);
				foreach (string gamename in Myrmex.AllComponentsForUI)
				{
					Myrmex.GetComponent(gamename).UIFlyInstantlyOnce = true;
				}
				sr.Close();
				fs.Close();
			}
			catch
			{
				btNewGame_Click(sender, e);
			}
			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(List<Myrmex>));
				FileStream fs = new FileStream("./myrmex_undohistory.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				History = (List<Myrmex>)xmls.Deserialize(sr);
				sr.Close();
				fs.Close();
			}
			catch
			{
			}

			for (int c = 0; c < 12; c++)
				if (c < Myrmex.NumColumn)
					BackSlot[c].Button.Visible = true;
				else
					BackSlot[c].Button.Visible = false;

			tiUI.Enabled = true;
			pictureBox1.Size = this.Size;
			pictureBox1.SendToBack();
		}

		public int MouseDownX, MouseDownY;
		private void btComponents_MouseDown(object sender, EventArgs e)
		{
			if (Myrmex.GameOver)
				return;

			string gamename = (string)((Button)sender).Tag;
			DecktetDeck deck = Myrmex.GetCardBelongingDeck(gamename);
			if (deck.GameName.StartsWith("Finished"))
				return;

			if (deck.GameName == "Main")
			{
				History.Add(Myrmex.Clone());
				Myrmex.Deal();
				return;
			}

			int mdcolumn = Myrmex.GetColumnIndex(gamename);
			int mdrow = Myrmex.GetRowIndex(gamename);
			bool movable = Myrmex.CheckFloatingUpValid(mdcolumn, mdrow);

			if (movable)
			{
				History.Add(Myrmex.Clone());
				Myrmex.FloatingUp(mdcolumn, mdrow);

				MouseDownX = Control.MousePosition.X;
				MouseDownY = Control.MousePosition.Y;
			}
		}
		private void btComponents_MouseMove(object sender, MouseEventArgs e)
		{
			if (Myrmex.FloatingCardCount == 0)
				return;

			Myrmex.SetFloatingPosition(Control.MousePosition.X - MouseDownX, Control.MousePosition.Y - MouseDownY);
		}
		private void btComponents_MouseUp(object sender, MouseEventArgs e)
		{
			if (Myrmex.FloatingCardCount == 0)
				return;

			int allowd = (int)((Myrmex.Columns[1][0].PositionX - Myrmex.Columns[0][0].PositionX) * 0.75);
			int mind1 = 999;
			int mind2 = 999;
			int minc1 = -1;
			int minc2 = -1;
			for (int c = 0; c < Myrmex.NumColumn; c++)
			{
				int d = (int)Math.Abs(Myrmex.Columns[c][0].PositionX - Myrmex.Floating[0].PositionX);
				if (d < mind1)
				{
					mind2 = mind1;
					mind1 = d;
					minc2 = minc1;
					minc1 = c;
				}
				else if (d < mind2)
				{
					mind2 = d;
					minc2 = c;
				}
			}

			if (Myrmex.CheckFloatingDownValid(minc1))
				Myrmex.FloatingDown(minc1);
			else if	(mind2 < allowd && Myrmex.CheckFloatingDownValid(minc2))
				Myrmex.FloatingDown(minc2);
			else
			{
				Myrmex.FloatingBack();
				History.RemoveAt(History.Count - 1);
			}

			MouseDownX = 0;
			MouseDownY = 0;
		}

		private void btSettings_Click(object sender, EventArgs e)
		{
			Myrmex_FormConfig cform = new Myrmex_FormConfig();
			cform.Show();
		}
		private void btSuggest_Click(object sender, EventArgs e)
		{
			if (Myrmex.GameOver)
				return;

			// temp
			List<int[]> all = Myrmex.GetAllValidActions();
			foreach (int[] action in all)
			{
				Myrmex.PrintActionFeatures(action);
				Console.Write("\n");
			}

			//int[] bestaction = Myrmex.GuessBestAction();
			//int[] bestaction = Myrmex.DeduceBestAction();
			//int[] bestaction = Myrmex.DeduceLoseRate().Action;
		}
		private void btUndo_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				Myrmex = History[History.Count - 1];
				History.RemoveAt(History.Count - 1);
				foreach (string gamename in Myrmex.AllComponentsForUI)
					 Myrmex.GetComponent(gamename).UIFlyingZOrder = 50;
			}
		}
		private void btReset_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				Myrmex = History[0];
				History.Clear();
			}
		}
		private void btNewGame_Click(object sender, EventArgs e)
		{
			if (Myrmex_Config.NumColumn * Myrmex_Config.NumRow > (Myrmex_Config.RankTo - Myrmex_Config.RankFrom + 1) * 6)
			{
				MessageBox.Show("Invalid game settings. Not enough cards to setup. Please change settings.");
				return;
			}

			btNewGame.Text = "...";
			btNewGame.Refresh();

			NewGame();
			History.Clear();

			if (Myrmex_Config.AllowUndo)
				btUndo.Visible = true;
			else
				btUndo.Visible = false;
			if (Myrmex_Config.AllowSuggest)
				btSuggest.Visible = true;
			else
				btSuggest.Visible = false;
			
			btNewGame.Text = "New Game";

			AllComponentInstantToStartPosition();

			for (int c = 0; c < 12; c++)
				if (c < Myrmex.NumColumn)
					BackSlot[c].Button.Visible = true;
				else
					BackSlot[c].Button.Visible = false;
		}

		private Dictionary<string, UIButton> CardShotName2Button = new Dictionary<string, UIButton>();
		private DateTime LasttiUITime = DateTime.Now;
		private void tiUI_Tick(object sender, EventArgs e)
		{

			string showmode = "image";

			// generage new button for new component
			foreach (string gamename in Myrmex.AllComponentsForUI)
			{
				if (!CardShotName2Button.ContainsKey(gamename))
				{
					UIButton bt = new UIButton();
					bt.Button = new RoundedButton();
					bt.Button.Tag = gamename;
					DecktetCard card = Myrmex.GetComponent(gamename);
					bt.Button.Size = new Size(100, 140);
					bt.Button.Location = new Point((int)card.PositionX[0] - bt.Button.Width / 2, (int)card.PositionY[0] - bt.Button.Height / 2);
					bt.Button.BackgroundImageLayout = ImageLayout.Zoom;
					bt.Button.FlatStyle = FlatStyle.Flat;
					bt.Button.FlatAppearance.BorderSize = 0;
					bt.Button.FlatAppearance.BorderColor = Config.BackTextureColor;
					bt.Button.FlatAppearance.MouseOverBackColor = Config.BackTextureColor;
					bt.Button.DrawTopGrayLine = true;
					bt.Button.MouseDown += btComponents_MouseDown;
					bt.Button.MouseMove += btComponents_MouseMove;
					bt.Button.MouseUp += btComponents_MouseUp;
					this.Controls.Add(bt.Button);
					CardShotName2Button[gamename] = bt;
				}
			}
			// destroy buttons for disappeared game component
			List<string> toremove = new List<string>();
			foreach (string gamename in CardShotName2Button.Keys)
			{
				if (Myrmex.GetComponent(gamename) == null)
				{
					Button button = CardShotName2Button[gamename].Button;
					this.Controls.Remove(button);
					button.Dispose();
					toremove.Add(gamename);
				}
			}
			foreach (string gamename in toremove)
				CardShotName2Button.Remove(gamename);

			// generate image for once at beginning
			foreach (string gamename in Myrmex.AllComponentsForUI)
			{
				DecktetCard card = Myrmex.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				if (bt.Button.BackgroundImage == null)
				{
					bt.UIX = Myrmex.MainDeck.PositionX;
					bt.UIY = Myrmex.MainDeck.PositionY;
					//bt.Button.Location = new Point((int)(bt.UIX + 0.5f), (int)(bt.UIY + 0.5f));
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
					bt.UIFaceUp = false;
				}
			}
			
			// card face up or down
			foreach (string gamename in Myrmex.AllComponentsForUI)
			{
				DecktetCard card = Myrmex.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				if (card.UIFlipFaceStartTime > DateTime.Now)
					continue;

				if (card.FaceUp != bt.UIFaceUp)
				{
					if (card.FaceUp)
					{
						string iname = "c" + card.ShortName + "_h";
						if (card.Rank == "Ace")
							iname += "_a";
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
						bt.UIFaceUp = true;
					}
					else
					{
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
						bt.UIFaceUp = false;
					}
				}
			}

			
			// update z-order
			bool zorder_need_update = false;
			List<string> deck_zorder_calculated = new List<string>();
			foreach (string gamename in Myrmex.AllComponentsForUI)
			{
				DecktetDeck deck = Myrmex.GetCardBelongingDeck(gamename);
				if (!deck_zorder_calculated.Contains(deck.GameName))
				{
					for (int i = 0; i < deck.Cards.Count; i++)
					{
						DecktetCard card = deck.Cards[i];
						int zorder = card.UIFlyingZOrder + deck.UIZOrder + i;
						if (card.UIFlyOnTop && CardShotName2Button[card.GameName].UIIsFlying)
						{
							zorder += 200;
						}
						if (zorder != CardShotName2Button[card.GameName].UIZOrder)
						{
							CardShotName2Button[card.GameName].UIZOrder = zorder;
							zorder_need_update = true;
						}
					}
					deck_zorder_calculated.Add(deck.GameName);
				}
			}
			if (zorder_need_update)
			{
				Myrmex.AllComponentsForUI = Myrmex.AllComponentsForUI.OrderBy(o => CardShotName2Button[o].UIZOrder).ToList();
				bool startprocess = false;
				int buttonzmax = 9999;
				foreach (string gamename in Myrmex.AllComponentsForUI)
				{
					UIButton bt = CardShotName2Button[gamename];
					if (!startprocess)
					{
						int buttonz = this.Controls.GetChildIndex(bt.Button);
						if (buttonz > bt.UIZOrder)
							startprocess = true;
						buttonzmax = buttonz;
					}
					if (startprocess)
						bt.Button.BringToFront();
				}
			}

			// fly
			float dt = (float)(DateTime.Now - LasttiUITime).TotalSeconds;
			if (dt > 0.1f)
				dt = 0.1f;
			LasttiUITime = DateTime.Now;
			foreach (string gamename in Myrmex.AllComponentsForUI)
			{
				//if (CardShotName2Button.ContainsKey(gamename))
				{
					// update animation component position every frame
					DecktetCard card = Myrmex.GetComponent(gamename);
					UIButton bt = CardShotName2Button[gamename];

					float distance = (float)Math.Sqrt(Math.Pow((card.PositionX[0] - bt.UIX), 2) + Math.Pow((card.PositionY[0] - bt.UIY), 2));
					float flyd = Myrmex_Config.AnimationSpeed * dt;
					if (card.UIFlyInstantly)
						flyd = 10000;
					if (card.UIFlyInstantlyOnce)
					{
						flyd = 10000;
						card.UIFlyInstantlyOnce = false;
					}

					if (distance > 0 || card.PositionX.Count > 1)
					{
						bt.UIIsFlying = true;
					}
					if (card.UIFlyingStartTime[0] < DateTime.Now)
					{

						if (distance > flyd)
						{
							float r = flyd / distance;
							bt.UIX += (card.PositionX[0] - bt.UIX) * r;
							bt.UIY += (card.PositionY[0] - bt.UIY) * r;
						}
						else if (distance > 0)
						{
							bt.UIX = card.PositionX[0];
							bt.UIY = card.PositionY[0];
							//UINeedUpdate = true;
						}
						else
						{
							if (card.PositionX.Count > 1)
							{
								card.PositionX.RemoveAt(0);
								card.PositionY.RemoveAt(0);
								card.UIFlyingStartTime.RemoveAt(0);
							}
							else
							{
								bt.UIIsFlying = false;
								card.UIFlyingZOrder = 0;
							}
						}

						//bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
						bt.Button.Left = (int)(bt.UIX - bt.Button.Width / 2 + 0.5f);
						bt.Button.Top = (int)(bt.UIY - bt.Button.Height / 2 + 0.5f);
					}
				}
			}

			// ui buttons and texts
			if (History.Count > 0 && MouseDownX == 0)
			{
				btUndo.Enabled = true;
				btUndo.BackColor = Color.Gold;
			}
			else
			{
				btUndo.Enabled = false;
				btUndo.BackColor = Config.BackTextureColor;
			}

			if (Myrmex.GameWon == 1)
			{
				lbStatusWin.Text = "Clear!";
				lbStatusWin.ForeColor = Color.Gold;
				lbStatusWin.BackColor = Config.BackTextureColor;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else if (Myrmex.GameLost == 1)
			{
				lbStatusWin.Text = "No valid actions possible.";
				lbStatusWin.ForeColor = Color.Black;
				lbStatusWin.BackColor = Color.LightGray;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else
			{
				lbStatusWin.Text = "";
				lbStatusWin.Visible = false;
			}
			
			
		}

		private void AllComponentInstantToStartPosition()
		{
			foreach (string shortname in CardShotName2Button.Keys)
			{
				DecktetCard card = Myrmex.GetComponent(shortname);
				UIButton bt = CardShotName2Button[shortname];
				bt.UIX = Myrmex.MainDeck.PositionX;
				bt.UIY = Myrmex.MainDeck.PositionY;
				bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
				//if (card.FaceUp)
				if (false)
				{
					string iname = "c" + card.ShortName + "_h";
					if (card.Rank == "Ace")
						iname += "_a";
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
					bt.UIFaceUp = true;
				}
				else
				{
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
					bt.UIFaceUp = false;
				}
			}
		}

		private void Myrmex_Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (!btUndo.Visible)
				return;
			if (MouseDownX > 0)
				return;

			if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
				btUndo_Click(sender, null);
		}

		private void Myrmex_Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Myrmex.GameOver)
			{
				File.Delete("./myrmex_save.xml");
				File.Delete("./myrmex_undohistory.xml");
				return;
			}

			XmlSerializer xmls = new XmlSerializer(typeof(Myrmex));
			FileStream fs = new FileStream("./myrmex_save.xml", FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			xmls.Serialize(sw, Myrmex);
			sw.Close();
			fs.Close();

			xmls = new XmlSerializer(typeof(List<Myrmex>));
			fs = new FileStream("./myrmex_undohistory.xml", FileMode.Create);
			sw = new StreamWriter(fs);
			xmls.Serialize(sw, History);
			sw.Close();
			fs.Close();
		}

		private void btTest_Click(object sender, EventArgs e)
		{
			
		}

	}
}
