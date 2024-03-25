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
	public partial class Adaman_Form : Form
	{
		Adaman Adaman;
		List<Adaman> History = new List<Adaman>();

		bool[] ResourceSelected;
		int CapitalSelectedIndex = -1;
		int PalaceSelectedIndex = -1;

		bool UINeedUpdate = true;

		public int[] FormCurrentSelectedAction()
		{
			int[] action = new int[2 + Adaman.CardPerRow];
			action[0] = PalaceSelectedIndex;
			action[1] = CapitalSelectedIndex;
			for (int i = 0; i < Adaman.CardPerRow; i++)
				action[2 + i] = ResourceSelected[i] ? 1 : 0;
			return action;
		}
		public void ParseActionToCurrentSelected(int[] action)
		{
			PalaceSelectedIndex = action[0];
			CapitalSelectedIndex = action[1];
			for (int i = 0; i < Adaman.CardPerRow; i++)
				ResourceSelected[i] = action[2 + i] == 1;
		}
		public void CheckCurrentSelectedActionValid()
		{
			int[] action = FormCurrentSelectedAction();
			if (Adaman.CheckActionValid(action))
			{
				btConfirm.BackColor = Color.Gold;
				btConfirm.Enabled = true;
			}
			else
			{
				btConfirm.BackColor = Config.BackTextureColor;
				btConfirm.Enabled = false;
			}
		}
		public void ClearSelection()
		{
			CapitalSelectedIndex = -1;
			PalaceSelectedIndex = -1;
			for (int i = 0; i < Adaman.CardPerRow; i++)
				ResourceSelected[i] = false;
			CheckCurrentSelectedActionValid();
		}
		private void NewGame(bool seeded = false)
		{
			Adaman = new Adaman();
			Adaman.Setup(seeded);

			while (Adaman_Config.AvoidUnsolvableGame)
			{
				bool canbewon = false;
				for (int tc = 0; tc < 5; tc++)
				{
					Adaman deduce = Adaman.Clone();
					while (!deduce.GameOver)
					{
						//int[] bestaction = Adaman.GuessBestAction();
						int[] bestaction = deduce.DeduceBestAction();
						//int[] bestaction = Adaman.DeduceLoseRate().Action;
						deduce.TakeAction(bestaction);
					}
					if (deduce.GameWon > 0)
					{
						canbewon = true;
						break;
					}
				}
				if (canbewon)
					break;

				Adaman = new Adaman();
				Adaman.Setup(seeded);
			}
		}

		public Adaman_Form()
		{
			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(Adaman_Config));
				FileStream fs = new FileStream("./adaman_config.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				Adaman_Config mc = (Adaman_Config)xmls.Deserialize(sr);
				sr.Close();
				fs.Close();
			}
			catch
			{
			}

			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			int CardPerRow = Adaman_Config.CardPerRow;

			ResourceSelected = new bool[CardPerRow];

			//btConfirm.BackColor = SystemColors.ButtonFace;
			btConfirm.Left = CardPerRow * 150 + 230;
			btSuggest.Left = CardPerRow * 150 + 230;
			btSettings.Left = CardPerRow * 150 + 230;
			btNewGame.Left = CardPerRow * 150 + 230;
			btReset.Left = CardPerRow * 150 + 230;
			btUndo.Left = CardPerRow * 150 + 230;
			btTest.Left = CardPerRow * 150 + 500;
			//lbStatusPersonLeft.Left = CardPerRow * 150 + 230;
			if (!Adaman_Config.AllowUndo) btUndo.Visible = false;
			if (!Adaman_Config.AllowSuggest) btSuggest.Visible = false;

			btNewGame_Click(sender, e);
			tiUI.Enabled = true;
			pictureBox1.Size = this.Size;
			pictureBox1.SendToBack();
		}

		private void btComponents_Click(object sender, EventArgs e)
		{
			if (Adaman.GameOver)
				return;

			string gamename = (string)((Button)sender).Tag;
			string deckname = Adaman.GetCardBelongingDeck(gamename).GameName;
			if (deckname.StartsWith("R"))
			{
				int index = int.Parse(deckname.Remove(0, 1));
				if (!Adaman.ResourceCard[index].IsEmpty())
					ResourceSelected[index] = !ResourceSelected[index];
			}
			else if (deckname.StartsWith("C"))
			{
				int index = int.Parse(deckname.Remove(0, 1));
				if (index == CapitalSelectedIndex)
				{
					CapitalSelectedIndex = -1;
				}
				else if (Adaman.CapitalCard[index].IsEmpty())
				{
					PalaceSelectedIndex = -1;
					CapitalSelectedIndex = -1;
				}
				else
				{
					CapitalSelectedIndex = index;
					PalaceSelectedIndex = -1;
				}
			}
			else if (deckname.StartsWith("P"))
			{
				int index = int.Parse(deckname.Remove(0, 1));
				if (index == PalaceSelectedIndex)
				{
					PalaceSelectedIndex = -1;
				}
				else if (Adaman.PalaceCard[index].IsEmpty())
				{
					PalaceSelectedIndex = -1;
					CapitalSelectedIndex = -1;
				}
				else
				{
					PalaceSelectedIndex = index;
					CapitalSelectedIndex = -1;
				}
			}

			CheckCurrentSelectedActionValid();
			UINeedUpdate = true;
		}

		private void btConfirm_Click(object sender, EventArgs e)
		{
			History.Add(Adaman.Clone());
			int[] action = FormCurrentSelectedAction();
			Adaman.TakeAction(action);

			//Console.WriteLine("-----");
			//List<int[]> all = Adaman.GetAllValidActions();
			//foreach (int[] a in all)
			//	Adaman.PrintActionFeatures(a);

			ClearSelection();
			UINeedUpdate = true;
		}

		private void btSettings_Click(object sender, EventArgs e)
		{
			Adaman_FormConfig cform = new Adaman_FormConfig();
			cform.Show();
		}
		private void btSuggest_Click(object sender, EventArgs e)
		{
			if (Adaman.GameOver)
				return;

			//int[] bestaction = Adaman.GuessBestAction();
			int[] bestaction = Adaman.DeduceBestAction();
			//int[] bestaction = Adaman.DeduceLoseRate().Action;
			if (bestaction != null)
			{
				ParseActionToCurrentSelected(bestaction);
				CheckCurrentSelectedActionValid();
			}
			else
			{
				ClearSelection();
			}

			UINeedUpdate = true;
		}
		private void btUndo_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				Adaman = History[History.Count - 1];
				History.RemoveAt(History.Count - 1);
			}

			ClearSelection();
			UINeedUpdate = true;
		}
		private void btReset_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				foreach (string shortname in History[0].AllComponentsForUI)
				{
					//History[0].GetComponent(shortname).UIX = Adaman.GetComponent(shortname).UIX;
					//History[0].GetComponent(shortname).UIY = Adaman.GetComponent(shortname).UIY;
					//History[0].GetComponent(shortname).UIFaceUp = Adaman.GetComponent(shortname).UIFaceUp;
				}
				Adaman = History[0];
				History.Clear();
			}

			UINeedUpdate = true;
		}
		private void btNewGame_Click(object sender, EventArgs e)
		{
			btNewGame.Text = "...";
			btNewGame.Refresh();

			NewGame();
			ClearSelection();
			History.Clear();

			if (Adaman_Config.AllowUndo)
				btUndo.Visible = true;
			else
				btUndo.Visible = false;
			if (Adaman_Config.AllowSuggest)
				btSuggest.Visible = true;
			else
				btSuggest.Visible = false;

			btNewGame.Text = "New Game";

			AllComponentInstantToStartPosition();
			UINeedUpdate = true;
		}

		private Dictionary<string, UIButton> CardShotName2Button = new Dictionary<string, UIButton>();
		private DateTime LasttiUITime = DateTime.Now;
		private void tiUI_Tick(object sender, EventArgs e)
		{
			string showmode = "image";

			// generage new button for new component
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				if (!CardShotName2Button.ContainsKey(gamename))
				{
					// generage new button for new component
					UIButton bt = new UIButton();
					bt.Button = new RoundedButton();
					bt.Button.Tag = gamename;
					DecktetCard card = Adaman.GetComponent(gamename);
					bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
					bt.Button.Size = new Size(150, 210);
					bt.Button.BackgroundImageLayout = ImageLayout.Zoom;
					bt.Button.FlatStyle = FlatStyle.Flat;
					bt.Button.FlatAppearance.BorderSize = 6;
					bt.Button.FlatAppearance.BorderColor = Config.BackTextureColor;
					bt.Button.FlatAppearance.MouseOverBackColor = Config.BackTextureColor;
					bt.Button.Click += btComponents_Click;
					this.Controls.Add(bt.Button);
					CardShotName2Button[gamename] = bt;
				}
			}
			// destroy buttons for disappeared game component
			List<string> toremove = new List<string>();
			foreach (string gamename in CardShotName2Button.Keys)
			{
				if (Adaman.GetComponent(gamename) == null)
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
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				DecktetCard card = Adaman.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				if (bt.Button.BackgroundImage == null)
				{
					bt.UIX = Adaman.MainDeck.PositionX;
					bt.UIY = Adaman.MainDeck.PositionY;
					//bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
					bt.UIFaceUp = false;
				}
			}

			// card face up or down
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				DecktetCard card = Adaman.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				//if (card.UIAnimationStartTime > DateTime.Now)
				//	continue;

				if (card.FaceUp != bt.UIFaceUp)
				{
					if (card.FaceUp)
					{
						string iname = "c" + card.ShortName;
						if (Adaman_Config.AlternativeAceImage && card.Rank == "Ace")
							iname += "_a";
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
						bt.UIFaceUp = card.FaceUp;
					}
					else
					{
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
						bt.UIFaceUp = card.FaceUp;
					}
				}
			}


			// highlight selection
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				//DecktetCard card = Adaman.GetComponent(shortname);
				UIButton bt = CardShotName2Button[gamename];
				DecktetDeck belongdeck = Adaman.GetCardBelongingDeck(gamename);

				bool selected = false;
				if (PalaceSelectedIndex >= 0 && belongdeck.GameName == "P" + PalaceSelectedIndex)
					selected = true;
				if (CapitalSelectedIndex >= 0 && belongdeck.GameName == "C" + CapitalSelectedIndex)
					selected = true;
				if (belongdeck.GameName.StartsWith("R") && ResourceSelected[int.Parse(belongdeck.GameName.Remove(0, 1))])
					selected = true;

				if (selected)
				{
					bt.Button.Width = 146;
					bt.Button.Height = 200;
					bt.Button.FlatAppearance.BorderSize = 6;
					bt.Button.FlatAppearance.BorderColor = Color.Gold;
					bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));

				}
				else
				{
					bt.Button.Width = 138;
					bt.Button.Height = 189;
					bt.Button.FlatAppearance.BorderSize = 0;
					bt.Button.FlatAppearance.BorderColor = Config.BackTextureColor;
					bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
				}
			}

			// update z-order
			bool zorder_need_update = false;
			List<string> deck_zorder_calculated = new List<string>();
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				DecktetDeck deck = Adaman.GetCardBelongingDeck(gamename);
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
				Adaman.AllComponentsForUI = Adaman.AllComponentsForUI.OrderBy(o => CardShotName2Button[o].UIZOrder).ToList();
				bool startprocess = false;
				int buttonzmax = 9999;
				foreach (string gamename in Adaman.AllComponentsForUI)
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
			LasttiUITime = DateTime.Now;
			foreach (string gamename in Adaman.AllComponentsForUI)
			{
				// update animation component position every frame
				DecktetCard card = Adaman.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				float distance = (float)Math.Sqrt(Math.Pow((card.PositionX[0] - bt.UIX), 2) + Math.Pow((card.PositionY[0] - bt.UIY), 2));
				float flyd = Adaman_Config.AnimationSpeed * dt;

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

					bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
				}
			}

			// ui buttons and texts
			if (History.Count > 0)
			{
				btUndo.Enabled = true;
				btUndo.BackColor = Color.Gold;
			}
			else
			{
				btUndo.Enabled = false;
				btUndo.BackColor = Config.BackTextureColor;
			}

			if (Adaman.GameWon == 1)
			{
				lbStatusWin.Text = "You won! Score: " + Adaman.ScoreGame();
				lbStatusWin.BackColor = Color.White;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else if (Adaman.GameLost == 1)
			{
				lbStatusWin.Text = "Can't place 6th Personality into palace.\n";
				lbStatusWin.Text += "You Lost! Score: " + Adaman.ScoreGame();
				lbStatusWin.BackColor = Color.LightGray;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else if (Adaman.GameLost == 2)
			{
				lbStatusWin.Text = "No valid actions possible.\n";
				lbStatusWin.Text += "You Lost! Score: " + Adaman.ScoreGame();
				lbStatusWin.BackColor = Color.LightGray;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else
			{
				lbStatusWin.Text = "";
				lbStatusWin.Visible = false;
			}

			lbStatusCardsLeft.Text = Adaman.MainDeck.Count().ToString() + " cards left";
			lbStatusPersonLeft.Text = Adaman.DiscardedPersonalityDeck.CountType("Personality") + "/" + Adaman.TotalPersonCount;
			if (Adaman.DiscardedPersonalityDeck.Count() > 0)
				lbStatusPersonLeft.Visible = true;
			else
				lbStatusPersonLeft.Visible = false;

			UINeedUpdate = false;
		}
		private void AllComponentInstantToStartPosition()
		{
			foreach (string shortname in CardShotName2Button.Keys)
			{
				//DecktetCard card = Adaman.GetComponent(shortname);
				UIButton bt = CardShotName2Button[shortname];
				bt.UIX = Adaman.MainDeck.PositionX;
				bt.UIY = Adaman.MainDeck.PositionY;
				bt.Button.Location = new Point((int)(bt.UIX - bt.Button.Width / 2 + 0.5f), (int)(bt.UIY - bt.Button.Height / 2 + 0.5f));
				bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("back3");
				bt.UIFaceUp = false;
			}
		}

		private void btTest_Click(object sender, EventArgs e)
		{
			int lostAtBeginning1 = 0;
			int lostAtBeginning2 = 0;
			int lost1 = 0;
			int lost2 = 0;
			int win = 0;
			int N = 100;
			DateTime start = DateTime.Now;
			for (int g = 0; g < N; g++)
			{
				Console.WriteLine(g);
				//Adaman = new Adaman();
				//Adaman.Setup(true);
				NewGame(true);
				if (Adaman.GameLost == 1)
				{
					lostAtBeginning1++;
					Console.WriteLine("lost at beginning");
					continue;
				}
				else if (Adaman.GameLost == 2)
				{
					lostAtBeginning2++;
					Console.WriteLine("lost at beginning");
					continue;
				}

				for (int pc = 0; pc < 5; pc++)
				{
					while (!Adaman.GameOver)
					{
						//int[] bestaction = Adaman.GuessBestAction();
						int[] bestaction = Adaman.DeduceBestAction();
						//int[] bestaction = Adaman.DeduceLoseRate().Action;
						Adaman.TakeAction(bestaction);
					}
					if (Adaman.GameWon == 1)
						break;
					Adaman = Adaman.GetReset();
				}

				if (Adaman.GameLost == 1)
					lost1++;
				else if (Adaman.GameLost == 2)
					lost2++;
				else if (Adaman.GameWon == 1)
					win++;
			}
			Console.WriteLine((DateTime.Now - start).TotalSeconds);

			Console.WriteLine("lost at beginning 1: " + (float)lostAtBeginning1 / N);
			Console.WriteLine("lost at beginning 2: " + (float)lostAtBeginning2 / N);
			Console.WriteLine("lost 1: " + (float)lost1 / N);
			Console.WriteLine("lost 2: " + (float)lost2 / N);
			Console.WriteLine("win: " + (float)win / N);
		}

	}

}
