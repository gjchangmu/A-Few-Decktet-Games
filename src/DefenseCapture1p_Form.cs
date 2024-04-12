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
	public partial class DefenseCapture1p_Form : Form
	{
		DefenseCapture1p DC;
		List<DefenseCapture1p> History = new List<DefenseCapture1p>();

		bool[] HandSelected = new bool[12];
		int DefenseSelectedIndex = -1;
		int SideSelectedIndex = -1;
		int DamagedSelectedIndex = -1;
		int CapturedSelectedIndex = -1;

		List<UIButton> DefenseSlot = new List<UIButton>();
		List<UIButton> SideSlot = new List<UIButton>();

		float EffectiveScale = 1f;
		float EffectiveWindowLeft = 0;
		float EffectiveWindowTop = 0;

		private void NewGame(bool seeded = false)
		{
			DC = new DefenseCapture1p();
			DC.Setup(seeded);
		}

		public DefenseCapture1p_Form()
		{
			InitializeComponent();
		}
		private void DefenseCapture1p_Form_Load(object sender, EventArgs e)
		{
			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(DefenseCapture1p_Config));
				FileStream fs = new FileStream("./defendcapture_config.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				DefenseCapture1p_Config mc = (DefenseCapture1p_Config)xmls.Deserialize(sr);
				sr.Close();
				fs.Close();
			}
			catch
			{
			}
			try
			{
				XmlSerializer xmls = new XmlSerializer(typeof(DefenseCapture1p));
				FileStream fs = new FileStream("./defendcapture_save.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				DC = (DefenseCapture1p)xmls.Deserialize(sr);
				foreach (string gamename in DC.AllComponentsForUI)
				{
					DecktetCard card = DC.GetComponent(gamename);
					foreach (UIAnimationStep step in card.UIAnimationSteps)
						step.FlyInstantly = true;
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
				XmlSerializer xmls = new XmlSerializer(typeof(List<DefenseCapture1p>));
				FileStream fs = new FileStream("./defendcapture_undohistory.xml", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				History = (List<DefenseCapture1p>)xmls.Deserialize(sr);
				sr.Close();
				fs.Close();
			}
			catch
			{
			}


			//pictureBox1.SendToBack();
			for (int c = 0; c < DC.BGCols; c++)
			{
				UIButton bt = new UIButton();
				bt.Button = new RoundedButton();
				bt.Button.Tag = "Defense" + c.ToString();
				bt.Button.FlatStyle = FlatStyle.Flat;
				bt.Button.FlatAppearance.BorderSize = 1;
				bt.Button.FlatAppearance.BorderColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.BackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.MouseUp += btComponents_MouseUp;
				this.Controls.Add(bt.Button);
				bt.Button.SendToBack();
				DefenseSlot.Add(bt);
			}
			for (int r = 0; r < DC.BGRows; r++)
			{
				UIButton bt = new UIButton();
				bt.Button = new RoundedButton();
				bt.Button.Tag = "Side" + r.ToString();
				bt.Button.FlatStyle = FlatStyle.Flat;
				bt.Button.FlatAppearance.BorderSize = 1;
				bt.Button.FlatAppearance.BorderColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.BackColor = Color.FromArgb(255, 178, 206, 170);
				bt.Button.MouseUp += btComponents_MouseUp;
				this.Controls.Add(bt.Button);
				bt.Button.SendToBack();
				SideSlot.Add(bt);
			}

			lbActionPoint.Parent = pictureBox1;
			lbStatusWin.Parent = pictureBox1;
			lbAreaMain.Parent = pictureBox1;
			lbAreaDiscard.Parent = pictureBox1;
			lbAreaHand.Parent = pictureBox1;
			lbAreaDamaged.Parent = pictureBox1;
			lbAreaCaptured.Parent = pictureBox1;
			pictureBox1.SendToBack();
			DefenseCapture1p_Form_SizeChanged(sender, e);
			CheckCurrentSelectedActionValid();
			tiUI.Enabled = true;
		}

		private void btComponents_MouseUp(object sender, MouseEventArgs e)
		{
			if (DC.GameOver)
				return;

			string buttontag = (string)((Button)sender).Tag;
			if (buttontag.StartsWith("Defense"))
			{
				int si = int.Parse(buttontag[buttontag.Length - 1].ToString());
				if (si != DefenseSelectedIndex)
					DefenseSelectedIndex = si;
				else
					DefenseSelectedIndex = -1;
				SideSelectedIndex = -1;
				DamagedSelectedIndex = -1;
				CapturedSelectedIndex = -1;
				CheckCurrentSelectedActionValid();
				return;
			}
			else if (buttontag.StartsWith("Side"))
			{
				int si = int.Parse(buttontag[buttontag.Length - 1].ToString());
				if (si != SideSelectedIndex)
					SideSelectedIndex = si;
				else
					SideSelectedIndex = -1;
				DefenseSelectedIndex = -1;
				DamagedSelectedIndex = -1;
				CapturedSelectedIndex = -1;
				CheckCurrentSelectedActionValid();
				return;
			}

			string gamename = (string)((Button)sender).Tag;
			DecktetDeck deck = DC.GetCardBelongingDeck(gamename);
			if (deck.GameName.StartsWith("Hand"))
			{
				int hi = deck.GameInfo1;
				HandSelected[hi] = !HandSelected[hi];
			}
			if (deck.GameName.StartsWith("Defense"))
			{
				int si = deck.GameInfo1;
				if (si != DefenseSelectedIndex)
					DefenseSelectedIndex = si;
				else
					DefenseSelectedIndex = -1;
				SideSelectedIndex = -1;
				DamagedSelectedIndex = -1;
				CapturedSelectedIndex = -1;
			}
			else if (deck.GameName.StartsWith("Side"))
			{
				int si = deck.GameInfo1;
				if (si != SideSelectedIndex)
					SideSelectedIndex = si;
				else
					SideSelectedIndex = -1;
				DefenseSelectedIndex = -1;
				DamagedSelectedIndex = -1;
				CapturedSelectedIndex = -1;
			}
			else if (deck.GameName.StartsWith("Damaged"))
			{
				int si = deck.GameInfo1;
				if (si != DamagedSelectedIndex)
					DamagedSelectedIndex = si;
				else
					DamagedSelectedIndex = -1;
				DefenseSelectedIndex = -1;
				SideSelectedIndex = -1;
				CapturedSelectedIndex = -1;
			}
			else if (deck.GameName.StartsWith("Captured"))
			{
				int si = deck.GameInfo1;
				if (si != CapturedSelectedIndex)
					CapturedSelectedIndex = si;
				else
					CapturedSelectedIndex = -1;
				DefenseSelectedIndex = -1;
				SideSelectedIndex = -1;
				DamagedSelectedIndex = -1;
			}
			CheckCurrentSelectedActionValid();
		}
		private int[] FormCurrentSelectedAction(int confirmindex)
		{
			int[] action = new int[14];
			action[0] = 1;

			if (confirmindex == 0)
			{
				// discard hand
				action[1] = 9;
				for (int i = 0; i < 12; i++)
					if (HandSelected[i])
						action[2 + i] = 1;
				return action;
			}

			int count4 = 0;
			if (DefenseSelectedIndex >= 0) count4++;
			if (SideSelectedIndex >= 0) count4++;
			if (DamagedSelectedIndex >= 0) count4++;
			if (CapturedSelectedIndex >= 0) count4++;
			int handsi = GetSingleHandSelectedIndex();
			if (handsi == -2 || count4 > 1)
				return null;
			if (handsi >= 0 && DefenseSelectedIndex >= 0)
			{
				// play into Defense Area
				action[1] = 1;
				action[2] = handsi;
				action[3] = DefenseSelectedIndex;
				action[4] = -1;
				return action;
			}
			else if (handsi >= 0 && SideSelectedIndex >= 0)
			{
				// play into Side Area
				action[1] = 1;
				action[2] = handsi;
				action[3] = -1;
				action[4] = SideSelectedIndex;
				return action;
			}
			else if (confirmindex == 1 && handsi == -1 && DefenseSelectedIndex >= 0 && !DC.Defense[DefenseSelectedIndex].IsEmpty())
			{
				// counterattack from defense
				action[1] = 2;
				action[2] = DefenseSelectedIndex;
				action[3] = -1;
				return action;
			}
			else if (confirmindex == 1 && handsi == -1 && SideSelectedIndex >= 0 && !DC.Side[SideSelectedIndex].IsEmpty())
			{
				// counterattack from side
				action[1] = 2;
				action[2] = -1;
				action[3] = SideSelectedIndex;
				return action;
			}
			else if (confirmindex == 2 && handsi == -1 && DefenseSelectedIndex >= 0 && !DC.Defense[DefenseSelectedIndex].IsEmpty())
			{
				// withdraw from defense
				action[1] = 5;
				action[2] = DefenseSelectedIndex;
				action[3] = -1;
				return action;
			}
			else if (confirmindex == 2 && handsi == -1 && SideSelectedIndex >= 0 && !DC.Side[SideSelectedIndex].IsEmpty())
			{
				// withdraw from side
				action[1] = 5;
				action[2] = -1;
				action[3] = SideSelectedIndex;
				return action;
			}
			else if (handsi >= 0 && CapturedSelectedIndex >= 0)
			{
				// interchange
				action[1] = 3;
				action[2] = handsi;
				action[3] = CapturedSelectedIndex;
				return action;
			}
			else if (handsi >= 0 && DamagedSelectedIndex >= 0)
			{
				// sacrifice
				if (DC.Hand[handsi].IsEmpty() || DC.Damaged[DamagedSelectedIndex].IsEmpty())
					throw new Exception();
				if (DC.Hand[handsi].GetSingleCard().GameRankNum <= DC.Damaged[DamagedSelectedIndex].GetSingleCard().GameRankNum)
					return null;

				action[1] = 4;
				action[2] = handsi;
				action[3] = DamagedSelectedIndex;
				return action;
			}

			return null;
		}
		private void CheckCurrentSelectedActionValid()
		{
			int[] action = FormCurrentSelectedAction(0);
			if (action != null && action[1] == 9 && !DC.GameOver)
			{
				btDiscardHand.BackColor = Color.SteelBlue;
				btDiscardHand.Enabled = true;
				btDiscardHand.Visible = true;
				btDiscardHand.Text = "Discard " + GetHandSelectedCount() + " Cards\nEnd Turn";
			}
			else
			{
				btDiscardHand.BackColor = Config.BackTextureColor;
				btDiscardHand.Enabled = false;
				btDiscardHand.Visible = false;
				//btDiscardHand.Text = "End Turn";
			}

			action = FormCurrentSelectedAction(1);
			if (DC.ActionPoint > 0 && action != null && action[1] != 9)
			{
				btConfirm1.BackColor = Color.Gold;
				btConfirm1.Enabled = true;
				btConfirm1.Visible = true;
				if (action[1] == 1)
					btConfirm1.Text = "🡹 Play";
				else if (action[1] == 2)
					btConfirm1.Text = "⚔ Counterattack";
				else if (action[1] == 3)
					btConfirm1.Text = "🠜🠞 Interchange";
				else if (action[1] == 4)
					btConfirm1.Text = "🗡 Sacrifice";
			}
			else
			{
				btConfirm1.BackColor = Config.BackTextureColor;
				btConfirm1.Enabled = false;
				btConfirm1.Visible = false;
			}

			action = FormCurrentSelectedAction(2);
			if (DC.ActionPoint > 0 && action != null && action[1] == 5)
			{
				btConfirm2.BackColor = Color.Gold;
				btConfirm2.Enabled = true;
				btConfirm2.Visible = true;
				btConfirm2.Text = "🡻 Withdraw";
			}
			else
			{
				btConfirm2.BackColor = Config.BackTextureColor;
				btConfirm2.Enabled = false;
				btConfirm2.Visible = false;
			}
		}
		private int GetSingleHandSelectedIndex()
		{
			int count = 0;
			int si = -1;
			for (int i = 0; i < 12; i++)
			{
				if (HandSelected[i])
				{
					si = i;
					count++;
				}
			}
			if (count == 1)
				return si;
			else if (count == 0)
				return -1;
			else if (count > 1)
				return -2;
			else
				throw new Exception();
		}
		private int GetHandSelectedCount()
		{
			int count = 0;
			for (int i = 0; i < 12; i++)
			{
				if (HandSelected[i])
				{
					count++;
				}
			}
			return count;
		}
		private void ClearSelection()
		{
			DefenseSelectedIndex = -1;
			SideSelectedIndex = -1;
			DamagedSelectedIndex = -1;
			CapturedSelectedIndex = -1;
            for (int i = 0; i < 12; i++)
				HandSelected[i] = false;
			CheckCurrentSelectedActionValid();
		}
		private void btConfirm_Click(object sender, EventArgs e)
		{
			if (DC.GameOver)
				return;

			int confirmindex = 0;
			if ((Button)sender == btConfirm1)
				confirmindex = 1;
			else if ((Button)sender == btConfirm2)
				confirmindex = 2;

			int[] action = FormCurrentSelectedAction(confirmindex);
			if (action != null && action[1] != 9)
			{
				History.Add(DC.Clone());
				DC.TakeAction(action);
				ClearSelection();
			}

		}
		private void btDiscardHand_Click(object sender, EventArgs e)
		{
			if (DC.GameOver)
				return;

			int[] action = FormCurrentSelectedAction(0);
			if (action != null && action[1] == 9)
			{
				History.Add(DC.Clone());
				DC.TakeAction(action);
				ClearSelection();
			}
		}

		private void btSettings_Click(object sender, EventArgs e)
		{
			//Myrmex_FormConfig cform = new Myrmex_FormConfig();
			//cform.Show();
		}
		private void btUndo_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				DC = History[History.Count - 1];
				History.RemoveAt(History.Count - 1);
				ClearSelection();
			}
		}
		private void btReset_Click(object sender, EventArgs e)
		{
			if (History.Count > 0)
			{
				DC = History[0];
				History.Clear();
			}
		}
		private void btNewGame_Click(object sender, EventArgs e)
		{
			btNewGame.Text = "...";
			btNewGame.Refresh();

			NewGame();
			History.Clear();

			if (DefenseCapture1p_Config.AllowUndo)
				btUndo.Visible = true;
			else
				btUndo.Visible = false;
			CheckCurrentSelectedActionValid();

			btNewGame.Text = "New Game";

			AllComponentInstantToStartPosition();
		}

		private Dictionary<string, UIButton> CardShotName2Button = new Dictionary<string, UIButton>();
		private DateTime LasttiUITime = DateTime.Now;
		private void tiUI_Tick(object sender, EventArgs e)
		{
			string showmode = "image";

			// generage new button for new component
			foreach (string gamename in DC.AllComponentsForUI)
			{
				if (!CardShotName2Button.ContainsKey(gamename))
				{
					UIButton bt = new UIButton();
					bt.Button = new RoundedButton();
					bt.Button.Tag = gamename;
					DecktetCard card = DC.GetComponent(gamename);
					//bt.Button.Size = new Size(100, 140);
					bt.UIX = card.UIAnimationSteps[0].PositionX;
					bt.UIY = card.UIAnimationSteps[0].PositionY;
					bt.UIW = card.UIAnimationSteps[0].SizeW;
					bt.UIH = card.UIAnimationSteps[0].SizeH;
					bt.Button.Size = new Size((int)card.UIAnimationSteps[0].SizeW, (int)card.UIAnimationSteps[0].SizeH);
					bt.Button.Location = new Point((int)card.UIAnimationSteps[0].PositionX - bt.Button.Width / 2, (int)card.UIAnimationSteps[0].PositionY - bt.Button.Height / 2);
					bt.Button.BackgroundImageLayout = ImageLayout.Zoom;
					bt.Button.FlatStyle = FlatStyle.Flat;
					bt.Button.FlatAppearance.BorderSize = 0;
					bt.Button.FlatAppearance.BorderColor = Config.BackTextureColor;
					bt.Button.FlatAppearance.MouseOverBackColor = Config.BackTextureColor;
					bt.Button.DrawLeftGrayLine = true;
					bt.Button.MouseUp += btComponents_MouseUp;
					this.Controls.Add(bt.Button);
					CardShotName2Button[gamename] = bt;
				}
			}
			// destroy buttons for disappeared game component
			List<string> toremove = new List<string>();
			foreach (string gamename in CardShotName2Button.Keys)
			{
				if (DC.GetComponent(gamename) == null)
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
			foreach (string gamename in DC.AllComponentsForUI)
			{
				DecktetCard card = DC.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				if (bt.Button.BackgroundImage == null)
				{
					bt.UIX = DC.Main.PositionX;
					bt.UIY = DC.Main.PositionY;
					//bt.Button.Location = new Point((int)(bt.UIX + 0.5f), (int)(bt.UIY + 0.5f));
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("sback");
					bt.UIFaceUp = false;
				}
			}
			
			// card face up or down
			foreach (string gamename in DC.AllComponentsForUI)
			{
				DecktetCard card = DC.GetComponent(gamename);
				UIButton bt = CardShotName2Button[gamename];

				if (card.UIFlipFaceStartTime > DateTime.Now)
					continue;

				if (card.FaceUp != bt.UIFaceUp)
				{
					if (card.FaceUp)
					{
						string iname = "s" + card.ShortName;
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
						bt.UIFaceUp = true;
					}
					else
					{
						bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("sback");
						bt.UIFaceUp = false;
					}
				}
			}

			// highlight selection
			foreach (string gamename in DC.AllComponentsForUI)
			{
				//DecktetCard card = Adaman.GetComponent(shortname);
				UIButton bt = CardShotName2Button[gamename];
				DecktetCard card = DC.GetComponent(gamename);
				DecktetDeck belongdeck = DC.GetCardBelongingDeck(gamename);

				bool selected = false;
				if (belongdeck.GameName.StartsWith("Defense") && DefenseSelectedIndex == belongdeck.GameInfo1)
					selected = true;
				if (belongdeck.GameName.StartsWith("Side") && SideSelectedIndex == belongdeck.GameInfo1)
					selected = true;
				if (belongdeck.GameName.StartsWith("Damaged") && DamagedSelectedIndex == belongdeck.GameInfo1)
					selected = true;
				if (belongdeck.GameName.StartsWith("Captured") && CapturedSelectedIndex == belongdeck.GameInfo1)
					selected = true;
				if (belongdeck.GameName.StartsWith("Hand") && HandSelected[belongdeck.GameInfo1])
					selected = true;

				if (selected && !bt.UISelected)
				{
					string iname = "s" + card.ShortName + "_s";
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
					bt.UISelected = true;
				}
				else if (!selected && bt.UISelected)
				{
					string iname = "s" + card.ShortName;
					bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(iname);
					bt.UISelected = false;
				}
			}
			for (int c = 0; c < DC.BGCols; c++)
			{
				if (c == DefenseSelectedIndex && !DefenseSlot[c].UISelected)
				{
					DefenseSlot[c].Button.FlatAppearance.BorderColor = Color.Gold;
					DefenseSlot[c].Button.FlatAppearance.MouseOverBackColor = Color.Gold;
					DefenseSlot[c].Button.FlatAppearance.MouseDownBackColor = Color.Gold;
					DefenseSlot[c].Button.BackColor = Color.Gold;
					DefenseSlot[c].UISelected = true;
				}
				else if (c != DefenseSelectedIndex && DefenseSlot[c].UISelected)
				{
					DefenseSlot[c].Button.FlatAppearance.BorderColor = Color.FromArgb(255, 178, 206, 170);
					DefenseSlot[c].Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 178, 206, 170);
					DefenseSlot[c].Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 178, 206, 170);
					DefenseSlot[c].Button.BackColor = Color.FromArgb(255, 178, 206, 170);
					DefenseSlot[c].UISelected = false;
				}
			}
			for (int r = 0; r < DC.BGRows; r++)
			{
				if (r == SideSelectedIndex && !SideSlot[r].UISelected)
				{
					SideSlot[r].Button.FlatAppearance.BorderColor = Color.Gold;
					SideSlot[r].Button.FlatAppearance.MouseOverBackColor = Color.Gold;
					SideSlot[r].Button.FlatAppearance.MouseDownBackColor = Color.Gold;
					SideSlot[r].Button.BackColor = Color.Gold;
					SideSlot[r].UISelected = true;
				}
				else if (r != SideSelectedIndex && SideSlot[r].UISelected)
				{
					SideSlot[r].Button.FlatAppearance.BorderColor = Color.FromArgb(255, 178, 206, 170);
					SideSlot[r].Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 178, 206, 170);
					SideSlot[r].Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 178, 206, 170);
					SideSlot[r].Button.BackColor = Color.FromArgb(255, 178, 206, 170);
					SideSlot[r].UISelected = false;
				}
			}

			// update z-order
			bool zorder_need_update = false;
			List<string> deck_zorder_calculated = new List<string>();
			foreach (string gamename in DC.AllComponentsForUI)
			{
				DecktetDeck deck = DC.GetCardBelongingDeck(gamename);
				if (!deck_zorder_calculated.Contains(deck.GameName))
				{
					for (int i = 0; i < deck.Cards.Count; i++)
					{
						DecktetCard card = deck.Cards[i];
						int zorder = card.UIAnimationSteps[0].FlyZOrder + deck.UIZOrder + i;
						if (card.UIAlwaysFlyOnTop && CardShotName2Button[card.GameName].UIIsFlying)
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
				DC.AllComponentsForUI = DC.AllComponentsForUI.OrderBy(o => CardShotName2Button[o].UIZOrder).ToList();
				bool startprocess = false;
				int buttonzmax = 9999;
				foreach (string gamename in DC.AllComponentsForUI)
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
			foreach (string gamename in DC.AllComponentsForUI)
			{
				//if (CardShotName2Button.ContainsKey(gamename))
				{
					// update animation component position every frame
					DecktetCard card = DC.GetComponent(gamename);
					UIButton bt = CardShotName2Button[gamename];

					float distance = (float)Math.Sqrt(Math.Pow((card.UIAnimationSteps[0].PositionX - bt.UIX), 2) + Math.Pow((card.UIAnimationSteps[0].PositionY - bt.UIY), 2));
					float flyd = DefenseCapture1p_Config.AnimationSpeed * dt;
					if (card.UIAlwaysFlyInstantly)
						flyd = 10000;
					if (card.UIAnimationSteps[0].FlyInstantly)
					{
						flyd = 10000;
						//card.UIFlyInstantlyOnce = false;
					}

					if (distance > 0 || card.UIAnimationSteps.Count > 1)
					{
						bt.UIIsFlying = true;
					}
					if (card.UIAnimationSteps[0].StartTime < DateTime.Now)
					{

						if (distance > flyd)
						{
							float r = flyd / distance;
							bt.UIX += (card.UIAnimationSteps[0].PositionX - bt.UIX) * r;
							bt.UIY += (card.UIAnimationSteps[0].PositionY - bt.UIY) * r;

							bt.UIW += (card.UIAnimationSteps[0].SizeW - bt.UIW) * r;
							bt.UIH += (card.UIAnimationSteps[0].SizeH - bt.UIH) * r;
							bt.Button.HighQuality = false;
						}
						else if (distance > 0)
						{
							bt.UIX = card.UIAnimationSteps[0].PositionX;
							bt.UIY = card.UIAnimationSteps[0].PositionY;

							bt.UIW = card.UIAnimationSteps[0].SizeW;
							bt.UIH = card.UIAnimationSteps[0].SizeH;

							bt.Button.HighQuality = true;
							bt.Button.Invalidate();
						}
						else
						{
							if (card.UIAnimationSteps.Count > 1)
							{
								card.UIAnimationSteps.RemoveAt(0);
							}
							else
							{
								bt.UIIsFlying = false;
								card.UIAnimationSteps[0].FlyZOrder = 0;
							}
						}

						ToScaledLocationSize(bt.Button, bt.UIX, bt.UIY, bt.UIW, bt.UIH);
					}
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
			if (!DC.Damaged[0].IsEmpty())
				lbAreaDamaged.Visible = true;
			else
				lbAreaDamaged.Visible = false;
			if (!DC.Discarded.IsEmpty())
				lbAreaDiscard.Visible = true;
			else
				lbAreaDiscard.Visible = false;
			lbActionPoint.Text = "Action Point: " + DC.ActionPoint + "/" + DC.ActionPointCapacity;
			if (DC.GameWon == 1)
			{
				lbStatusWin.Text = "You won!";
				lbStatusWin.ForeColor = Color.Black;
				lbStatusWin.BackColor = Color.Gold;
				lbStatusWin.BringToFront();
				lbStatusWin.Visible = true;
			}
			else if (DC.GameLost == 1)
			{
				lbStatusWin.Text = "You lost.";
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
				DecktetCard card = DC.GetComponent(shortname);
				UIButton bt = CardShotName2Button[shortname];
				bt.UIW = DC.Main.SizeW;
				bt.UIH = DC.Main.SizeH;
				bt.UIX = DC.Main.PositionX;
				bt.UIY = DC.Main.PositionY;
				ToScaledLocationSize(bt.Button, bt.UIX, bt.UIY, bt.UIW, bt.UIH);

				//bt.Button.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("sback");
				//bt.UIFaceUp = false;
			}
		}

		private void DefenseCapture1p_Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (!btUndo.Visible)
				return;

			if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
				btUndo_Click(sender, null);
		}

		private void DefenseCapture1p_Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DC.GameOver)
			{
				File.Delete("./defendcapture_save.xml");
				File.Delete("./defendcapture_undohistory.xml");
				return;
			}

			XmlSerializer xmls = new XmlSerializer(typeof(DefenseCapture1p));
			FileStream fs = new FileStream("./defendcapture_save.xml", FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			xmls.Serialize(sw, DC);
			sw.Close();
			fs.Close();

			xmls = new XmlSerializer(typeof(List<DefenseCapture1p>));
			fs = new FileStream("./defendcapture_undohistory.xml", FileMode.Create);
			sw = new StreamWriter(fs);
			xmls.Serialize(sw, History);
			sw.Close();
			fs.Close();
		}

		private void DefenseCapture1p_Form_SizeChanged(object sender, EventArgs e)
		{
			if (this.Width < 640 || this.Height < 360 || this.WindowState == FormWindowState.Minimized)
				return;

			if ((float)this.Width / this.Height >= 1280f / 720f)
			{
				EffectiveScale = this.Height / 720f;
				EffectiveWindowLeft = (this.Width - this.Height / 720f * 1280f) / 2f;
				EffectiveWindowTop = 0;
			}
			else
			{
				EffectiveScale = this.Width / 1280f;
				EffectiveWindowLeft = 0;
				EffectiveWindowTop = (this.Height - this.Width / 1280f * 720f) / 2f;
			}

			ToScaledLocationSize(btSettings, 63, 27, 100, 30);
			ToScaledLocationSize(btNewGame, 63, 62, 100, 30);
			ToScaledLocationSize(btReset, 63, 97, 100, 30);
			ToScaledLocationSize(btUndo, 63, 630, 100, 30);
			ToScaledLocationSize(btDiscardHand, 1160, 630, 170, 65, 14);
			ToScaledLocationSize(btConfirm1, 960, 630, 170, 50, 14);
			ToScaledLocationSize(btConfirm2, 780, 630, 170, 50, 14);
			ToScaledLocationSize(lbActionPoint, 1160, 580, 170, 60, 16);
			ToScaledLocationSize(lbStatusWin, 900, 480, 200, 50, 28);
			ToScaledLocationSize(lbAreaMain, DC.Main.PositionX, DC.Main.PositionY + 70, 150, 30, 12);
			ToScaledLocationSize(lbAreaDiscard, DC.Discarded.PositionX, DC.Discarded.PositionY + 70, 150, 30, 12);
			ToScaledLocationSize(lbAreaHand, DC.Hand[0].PositionX + 20, DC.Hand[0].PositionY + 70, 150, 30, 12);
			ToScaledLocationSize(lbAreaDamaged, DC.Damaged[0].PositionX + 20, DC.Damaged[0].PositionY + 70, 150, 30, 12);
			ToScaledLocationSize(lbAreaCaptured, DC.Captured[0].PositionX + 20, DC.Captured[0].PositionY + 70, 150, 30, 12);

			for (int c = 0; c < DefenseSlot.Count; c++)
				ToScaledLocationSize(DefenseSlot[c].Button, DC.Defense[c].PositionX, DC.Defense[c].PositionY, DC.Defense[c].SizeW, DC.Defense[c].SizeH);
			for (int r = 0; r < SideSlot.Count; r++)
				ToScaledLocationSize(SideSlot[r].Button, DC.Side[r].PositionX, DC.Side[r].PositionY, DC.Side[r].SizeW, DC.Side[r].SizeH);

			pictureBox1.Width = this.Width;
			pictureBox1.Height = this.Height;

		}

		private void ToScaledLocationSize(Control control, float left, float top, float width, float height, float fontsize = 11)
		{
			int aimw = (int)(width * EffectiveScale + 0.5f);
			int aimh = (int)(height * EffectiveScale + 0.5f);
			int aiml = (int)(left * EffectiveScale + EffectiveWindowLeft - aimw / 2 + 0.5f);
			int aimt = (int)(top * EffectiveScale + EffectiveWindowTop - aimh / 2 + 0.5f);
			if (control.Left != aiml || control.Top != aimt)
				control.Location = new Point(aiml, aimt);
			if (control.Width != aimw || control.Height != aimh)
				control.Size = new Size(aimw, aimh);
			if ((int)control.Font.SizeInPoints != (int)(fontsize * EffectiveScale))
				control.Font = new Font(control.Font.Name, (int)(fontsize * EffectiveScale));


		}

		private void btTest_Click(object sender, EventArgs e)
		{
			
		}
	}
}
