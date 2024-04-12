using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class DefenseCapture1p
	{
		public DecktetDeck Main;
		public DecktetDeck Discarded;
		public DecktetDeck[][] Battleground;
		public DecktetDeck[] Side;
		public DecktetDeck[] Defense;
		public DecktetDeck[] Captured;
		public DecktetDeck[] Damaged;
		public DecktetDeck[] Hand;

		public int ActionPoint = 0;
		public int ActionPointCapacity = 0;
		public bool GameOver = false;
		public int GameWon = 0, GameLost = 0;

		public int BGRows = 3;
		public int BGCols = 3;
		public int NCapturedToWin = 12;
		public int NDamageToLose = 20;
		public int NCapturedToSurprise = 8;
		public bool Surprise_RFaceDown_Enabled = false;
		public bool Surprise_BGFillAll_Enabled = false;

		public float AccumulatedDelay = 0f;

		public DefenseCapture1p()
		{
		}

		public DefenseCapture1p Clone()
		{
			DefenseCapture1p clone = new DefenseCapture1p();

			clone.ActionPoint = ActionPoint;
			clone.ActionPointCapacity = ActionPointCapacity;
			clone.GameOver = GameOver;
			clone.GameWon = GameWon;
			clone.GameLost = GameLost;

			clone.BGRows = BGRows;
			clone.BGCols = BGCols;
			clone.NCapturedToWin = NCapturedToWin;
			clone.NDamageToLose = NDamageToLose;
			clone.NCapturedToSurprise = NCapturedToSurprise;
			clone.Surprise_RFaceDown_Enabled = Surprise_RFaceDown_Enabled;
			clone.Surprise_BGFillAll_Enabled = Surprise_BGFillAll_Enabled;

			clone.Main = Main.Clone();
			clone.Discarded = Discarded.Clone();
			clone.Battleground = new DecktetDeck[BGRows][];
			for (int r = 0; r < BGRows; r++)
			{
				clone.Battleground[r] = new DecktetDeck[BGCols];
				for (int c = 0; c < BGCols; c++)
				{
					clone.Battleground[r][c] = Battleground[r][c].Clone();
				}
			}
			clone.Side = new DecktetDeck[BGRows];
			for (int r = 0; r < BGRows; r++)
				clone.Side[r] = Side[r].Clone();
			clone.Defense = new DecktetDeck[BGCols];
			for (int c = 0; c < BGCols; c++)
				clone.Defense[c] = Defense[c].Clone();
			clone.Captured = new DecktetDeck[20];
			for (int c = 0; c < 20; c++)
				clone.Captured[c] = Captured[c].Clone();
			clone.Damaged = new DecktetDeck[20];
			for (int c = 0; c < 20; c++)
				clone.Damaged[c] = Damaged[c].Clone();
			clone.Hand = new DecktetDeck[12];
			for (int c = 0; c < 12; c++)
				clone.Hand[c] = Hand[c].Clone();

			clone.AllComponentsForUI = AllComponentsForUI;

			return clone;
		}
	
		public void Setup(bool seeded = false)
		{
			
			Main = DecktetDeck.New44();
			//Main.DestroyRank("Court");
			Main.GameName = "Main";
			Main.SetGameRandNum("Crown", 10);
			Main.SetGameRandNum("Court", 10);
			Main.SetGameRandNum("Pawn", 10);
			Main.PositionX = 70;
			Main.PositionY = 230;
			Main.SizeW = 110;
			Main.SizeH = 110;
			Main.InitAllCardsPosition();
			foreach (DecktetCard card in Main.Cards)
			{
				card.UIAlwaysFlyOnTop = true;
				card.UIAlwaysFlyInstantly = false;
			}
			foreach (DecktetCard card in Main.Cards)
				AllComponentsForUI.Add(card.GameName);

			Discarded = new DecktetDeck();
			Discarded.GameName = "Discarded";
			Discarded.PositionX = 70;
			Discarded.PositionY = 390;
			Discarded.SizeW = 110;
			Discarded.SizeH = 110;

			Battleground = new DecktetDeck[BGRows][];
			for (int r = 0; r < BGRows; r++)
			{
				Battleground[r] = new DecktetDeck[BGCols];
				for (int c = 0; c < BGCols; c++) 
				{

					Battleground[r][c] = new DecktetDeck();
					Battleground[r][c].GameName = "BG_" + r + "," + c;
					Battleground[r][c].GameInfo1 = r;
					Battleground[r][c].GameInfo2 = c;
					Battleground[r][c].PositionX = 220 + c * 120;
					Battleground[r][c].PositionY = 60 + r * 120;
					Battleground[r][c].SizeW = 110;
					Battleground[r][c].SizeH = 110;
					//Battleground[r][c].UIZOrder = r + c;
				}
			}

			Side = new DecktetDeck[BGRows];
			for (int r = 0; r < BGRows; r++)
			{
				Side[r] = new DecktetDeck();
				Side[r].GameName = "Side_" + r;
				Side[r].GameInfo1 = r;
				Side[r].PositionX = 220 + BGCols * 120 + 20;
				Side[r].PositionY = 60 + r * 120;
				Side[r].SizeW = 110;
				Side[r].SizeH = 110;
				//Side[r].UIZOrder = r + 50;
			}

			Defense = new DecktetDeck[BGCols];
			for (int c = 0; c < BGCols; c++)
			{
				Defense[c] = new DecktetDeck();
				Defense[c].GameName = "Defense_" + c;
				Defense[c].GameInfo1 = c;
				Defense[c].PositionX = 220 + c * 120;
				Defense[c].PositionY = 60 + BGRows * 120 + 20;
				Defense[c].SizeW = 110;
				Defense[c].SizeH = 110;
				//Defense[r].UIZOrder = r + 50;
			}

			Captured = new DecktetDeck[20];
			for (int c = 0; c < 20; c++)
			{
				Captured[c] = new DecktetDeck();
				Captured[c].GameName = "Captured_" + c;
				Captured[c].GameInfo1 = c;
				Captured[c].PositionX = 220 + (BGCols + 1) * 120 + c * 28 + 50;
				Captured[c].PositionY = 60 + (BGRows - 0.5f) * 120;
				Captured[c].SizeW = 110;
				Captured[c].SizeH = 110;
				Captured[c].UIZOrder = c;
			}

			Damaged = new DecktetDeck[20];
			for (int c = 0; c < 20; c++)
			{
				Damaged[c] = new DecktetDeck();
				Damaged[c].GameName = "Damaged_" + c;
				Damaged[c].GameInfo1 = c;
				Damaged[c].PositionX = 220 + (BGCols + 1) * 120 + c * 28 + 50;
				Damaged[c].PositionY = 60 + 0.5f * 120;
				Damaged[c].SizeW = 110;
				Damaged[c].SizeH = 110;
				Damaged[c].UIZOrder = c;
			}

			Hand = new DecktetDeck[12];
			for (int c = 0; c < 12; c++)
			{
				Hand[c] = new DecktetDeck();
				Hand[c].GameName = "Hand_" + c;
				Hand[c].GameInfo1 = c;
				Hand[c].PositionX = 220 + 65 + c * 28;
				Hand[c].PositionY = 60 + (BGRows + 1.5f) * 120 - 10;
				Hand[c].SizeW = 110;
				Hand[c].SizeH = 110;
				Hand[c].UIZOrder = c;
			}

			Main.Shuffle(seeded);
			for (int i = Main.Count() - 1; i >= 0; i--)
			{
				DecktetCard card = Main.Cards[i];
				//if (GetCapturedTotal() == 4)
				//	break;
				if (GetCapturedTotal("1~9") < 2 && card.GameRankNum < 10)
					CapturedAdd(Main.Pull(card), 0.1f);
				if (GetCapturedTotal("10") < 2 && (card.Rank == "Court" || card.Rank == "Pawn"))
					CapturedAdd(Main.Pull(card), 0.1f);
			}

			EnermyPhase();
			DrawStep();
		}

		public void EnermyPhase()
		{
			// attack
			for (int c = 0; c < BGCols; c++)
			{
				if (Battleground[BGRows - 1][c].IsEmpty())
					continue;

				DecktetCard attack = Battleground[BGRows - 1][c].PickUpSingle();
				if (Defense[c].IsEmpty())
				{
					AccumulatedDelay += 0.5f;
					DamagedAdd(attack, AccumulatedDelay);
					continue;
				}
				DecktetCard defense = Defense[c].GetSingleCard();
				if (attack.GameRankNum >= defense.GameRankNum)
				{
					AccumulatedDelay += 0.5f;
					DamagedAdd(Defense[c].PickUpSingle(), AccumulatedDelay);
					Discarded.Add(attack, AccumulatedDelay);
				}
				else
				{
					AccumulatedDelay += 0.5f;
					Discarded.Add(attack, AccumulatedDelay);
				}
			}

			// advance
			AccumulatedDelay += 0.2f;
			for (int r = BGRows - 2; r >= 0; r--)
				for (int c = 0; c < BGCols; c++)
				{
					if (Battleground[r][c].IsEmpty())
						continue;

					DecktetCard card = Battleground[r][c].PickUpSingle();
					card.FaceUp = true;
					Battleground[r + 1][c].Add(card, AccumulatedDelay);
				}

			// reinforce
			for (int c = 0; c < BGCols; c++)
			{
				AccumulatedDelay += 0.6f;
				DecktetCard card = Draw();
				if (card == null)
					break;
				card.UIFlipFaceStartTime = DateTime.Now.AddSeconds(AccumulatedDelay - 0.3f);
                Battleground[0][c].Add(card, AccumulatedDelay, 120 - 2 * c);
			}

			// surprise
			if (GetCapturedTotal() >= NCapturedToSurprise)
			{
				if (Surprise_BGFillAll_Enabled)
					Surprise_BGFillAll();
				if (Surprise_RFaceDown_Enabled)
					Surprise_RFaceDown();

			}
		}
		public void Surprise_BGFillAll()
		{
			for (int r = BGRows - 1; r >= 0; r--)
				for (int c = 0; c < BGCols; c++)
				{
					if (Battleground[r][c].IsEmpty())
					{
						AccumulatedDelay += 0.6f;
						DecktetCard card = Draw();
						if (card == null)
							return;
						card.UIFlipFaceStartTime = DateTime.Now.AddSeconds(AccumulatedDelay - 0.3f);
						Battleground[r][c].Add(card, AccumulatedDelay, 120);
					}
				}
		}
		public void Surprise_RFaceDown()
		{
			for (int c = 0; c < BGCols; c++)
			{
				if (!Battleground[0][c].IsEmpty())
				{
					Battleground[0][c].GetSingleCard().FaceUp = false;
				}
			}
		}
		public void DrawStep()
		{
			int hand = GetHandTotal();
            int todraw = GetCapturedTotal("1~9") - hand;
			for (int i = hand; i < hand + todraw; i++)
			{
				DecktetCard card = Draw();
				if (card == null)
					break;
				AccumulatedDelay += 0.6f;
                card.UIFlipFaceStartTime = DateTime.Now.AddSeconds(AccumulatedDelay - 0.3f);
				Hand[i].Add(card, AccumulatedDelay, 110 - 2 * i);
			}
			ActionPointCapacity = GetCapturedTotal("10");
			ActionPoint = ActionPointCapacity;
		}

		public int GetCapturedTotal(string type = "")
		{
			if (type == "")
			{
				for (int c = 0; c < 20; c++)
					if (Captured[c].IsEmpty())
						return c;
			}
			else if (type == "1~9")
			{
				int count = 0;
				for (int c = 0; c < 20; c++)
					if (Captured[c].IsEmpty())
						return count;
					else
						if (Captured[c].GetSingleCard().GameRankNum <= 9 && Captured[c].GetSingleCard().GameRankNum >= 1)
							count++;
			}
			else if (type == "10")
			{
				int count = 0;
				for (int c = 0; c < 20; c++)
					if (Captured[c].IsEmpty())
						return count;
					else
						if (Captured[c].GetSingleCard().GameRankNum == 10)
							count++;
			}

			throw new Exception();
		}
		public int GetDamagedRandSum()
		{
			int sum = 0;
			for (int c = 0; c < 20; c++)
			{
				if (Damaged[c].IsEmpty())
					break;
				sum += Damaged[c].GetSingleCard().GameRankNum;
			}
			return sum;
		}
		public int GetHandTotal()
		{
			int c = 0;
			for (c = 0; c < 12; c++)
				if (Hand[c].IsEmpty())
					break;
			return c;
		}

		public DecktetCard Draw()
		{
			if (Main.Count() > 0)
				return Main.Draw();
			else
			{
				while (!Discarded.IsEmpty())
				{
					DecktetCard card = Discarded.Draw();
					card.FaceUp = false;
					card.UIFlipFaceStartTime = DateTime.Now.AddSeconds(AccumulatedDelay);
					Main.Add(card, AccumulatedDelay);
				}
				if (Main.IsEmpty())
					throw new Exception();
				Main.Shuffle();
				return Main.Draw();
			}
		}
		public void CapturedAdd(DecktetCard card, float timedelay = 0f)
		{
			for (int c = 0; c < 20; c++)
				if (Captured[c].IsEmpty())
				{
					Captured[c].Add(card, timedelay);
					CheckGameOver();
					return;
				}
		}
		public void DamagedAdd(DecktetCard card, float timedelay = 0f)
		{
			for (int c = 0; c < 20; c++)
				if (Damaged[c].IsEmpty())
				{
					Damaged[c].Add(card, timedelay);
					CheckGameOver();
					return;
				}
		}
		public void HandAdd(DecktetCard card, float timedelay = 0f)
		{
			for (int c = 0; c < 12; c++)
				if (Hand[c].IsEmpty())
				{
					Hand[c].Add(card, timedelay);
					return;
				}
		}
		public void HandOrganize()
		{
			int toi = 0;
			for (int i = 0; i < 12; i++)
			{
				if (!Hand[i].IsEmpty())
				{
					if (toi != i)
						Hand[toi].Add(Hand[i].PickUpSingle(), AccumulatedDelay);
					toi++;
                }
			}
		}

		public bool CheckActionValid(int[] action)
		{
			int player = action[0];
			int type = action[1];
			int param1 = action[2];

			return true;
		}
		public void PrintActionFeatures(int[] action)
		{
			foreach (int n in action)
				Console.Write(n.ToString() + ", ");
			//Console.Write("\n");
		}
		public void TakeAction(int[] action)
		{
			if (GameOver)
				return;
			//if (!CheckActionValid(action))
			//	throw new Exception();
			AccumulatedDelay = 0f;

			int player = action[0];
			int type = action[1];

			if (type == 1) // play
			{
				if (ActionPoint <= 0)
					throw new Exception();
				int handi = action[2];
				int defensei = action[3];
				int sidei = action[4];
				if (defensei >= 0 && sidei >= 0)
					throw new Exception();
				if (defensei >= 0)
				{
					if (!Defense[defensei].IsEmpty())
					{
						Discarded.Add(Defense[defensei].PickUpSingle());
						AccumulatedDelay += 0.3f;
					}
					Defense[defensei].Add(Hand[handi].PickUpSingle(), AccumulatedDelay);
					HandOrganize();
					ActionPoint--;
				}
				else
				{
					if (!Side[sidei].IsEmpty())
					{
						Discarded.Add(Side[sidei].PickUpSingle());
						AccumulatedDelay += 0.3f;
					}
					Side[sidei].Add(Hand[handi].PickUpSingle(), AccumulatedDelay);
					HandOrganize();
					ActionPoint--;
				}
			}
			else if (type == 2) // counterattack
			{
				if (ActionPoint <= 0)
					throw new Exception();
				int defensei = action[2];
				int sidei = action[3];
				if (defensei >= 0 && sidei >= 0)
					throw new Exception();
				if (defensei >= 0)
				{
					int c = defensei;
					for (int r = 0; r < BGRows; r++)
					{
						if (Defense[c].IsEmpty() || Side[r].IsEmpty() || Battleground[r][c].IsEmpty())
							continue;

						DecktetCard affected = Battleground[r][c].GetSingleCard();
						DecktetCard defense = Defense[c].GetSingleCard();
						DecktetCard side = Side[r].GetSingleCard();
						if (!affected.FaceUp)
							continue;
						if (!defense.ShareSuitWith(affected) || !side.ShareSuitWith(affected))
							continue;

						if (defense.GameRankNum + side.GameRankNum == affected.GameRankNum)
						{
							CapturedAdd(Battleground[r][c].PickUpSingle(), AccumulatedDelay);
							AccumulatedDelay += 0.5f;
						}
						else if (defense.GameRankNum + side.GameRankNum > affected.GameRankNum)
						{
							Discarded.Add(Battleground[r][c].PickUpSingle(), AccumulatedDelay);
							AccumulatedDelay += 0.5f;
						}
					}
					ActionPoint--;
				}
				else
				{
					int r = sidei;
					for (int c = 0; c < BGCols; c++)
					{
						if (Defense[c].IsEmpty() || Side[r].IsEmpty() || Battleground[r][c].IsEmpty())
							continue;

						DecktetCard affected = Battleground[r][c].GetSingleCard();
						DecktetCard defense = Defense[c].GetSingleCard();
						DecktetCard side = Side[r].GetSingleCard();
						if (!affected.FaceUp)
							continue;
						if (!defense.ShareSuitWith(affected) || !side.ShareSuitWith(affected))
							continue;

						if (defense.GameRankNum + side.GameRankNum == affected.GameRankNum)
						{
							CapturedAdd(Battleground[r][c].PickUpSingle(), AccumulatedDelay);
							AccumulatedDelay += 0.5f;
						}
						else if (defense.GameRankNum + side.GameRankNum > affected.GameRankNum)
						{
							Discarded.Add(Battleground[r][c].PickUpSingle(), AccumulatedDelay);
							AccumulatedDelay += 0.5f;
						}
					}
					ActionPoint--;
				}
			}
			else if (type == 3) // interchange
			{
				if (ActionPoint <= 0)
					throw new Exception();
				int handi = action[2];
				int capturedi = action[3];
				DecktetCard handcard = Hand[handi].PickUpSingle();
				DecktetCard capturedcard = Captured[capturedi].PickUpSingle();
				Hand[handi].Add(capturedcard);
				Captured[capturedi].Add(handcard);
				ActionPoint--;
			}
			else if (type == 4) // sacrifice
			{
				if (ActionPoint <= 0)
					throw new Exception();
				int handi = action[2];
				int damagedi = action[3];
				DecktetCard handcard = Hand[handi].PickUpSingle();
				DecktetCard damagedcard = Damaged[damagedi].PickUpSingle();
				if (handcard.GameRankNum <= damagedcard.GameRankNum)
					throw new Exception();
				Hand[handi].Add(damagedcard);
				Damaged[damagedi].Add(handcard);
				ActionPoint--;
				CheckGameOver();
			}
			else if (type == 5) // withdraw
			{
				if (ActionPoint <= 0)
					throw new Exception();
				int defensei = action[2];
				int sidei = action[3];
				if (defensei >= 0 && sidei >= 0)
					throw new Exception();
				if (defensei >= 0)
				{
					HandAdd(Defense[defensei].PickUpSingle());
					ActionPoint--;
				}
				else
				{
					HandAdd(Side[sidei].PickUpSingle());
					ActionPoint--;
				}
			}
			else if (type == 9) // discard
			{
				for (int i = 0; i < 12; i++)
				{
					bool todiscard = action[2 + i] == 1;
					if (todiscard)
					{
						Discarded.Add(Hand[i].PickUpSingle());
						AccumulatedDelay = 0.4f;
					}
				}
				HandOrganize();
				EnermyPhase();
				if (GetCapturedTotal() >= NCapturedToSurprise)
					EnermyPhase();
				DrawStep();
			}
		}
		public void CheckGameOver()
		{
			if (GetCapturedTotal() >= NCapturedToWin)
			{
				GameOver = true;
				GameWon = 1;
			}
			else if (GetDamagedRandSum() >= NDamageToLose)
			{
				GameOver = true;
				GameLost = 1;
			}
		}

		public List<string> AllComponentsForUI = new List<string>();
		public DecktetCard GetComponent(string gamename)
		{
			if (Main.Contains(gamename))
				return Main.GetCard(gamename);
			if (Discarded.Contains(gamename))
				return Discarded.GetCard(gamename);
			for (int r = 0; r < BGRows; r++)
			{
				for (int c = 0; c < BGCols; c++)
				{
					if (Battleground[r][c].Count() > 0 && Battleground[r][c].GetSingleCard().GameName == gamename)
						return Battleground[r][c].GetSingleCard();
				}
			}
			for (int r = 0; r < BGRows; r++)
			{
				if (Side[r].Count() > 0 && Side[r].GetSingleCard().GameName == gamename)
					return Side[r].GetSingleCard();
			}
			for (int c = 0; c < BGCols; c++)
			{
				if (Defense[c].Count() > 0 && Defense[c].GetSingleCard().GameName == gamename)
					return Defense[c].GetSingleCard();
			}
			for (int c = 0; c < 20; c++)
			{
				if (Captured[c].Count() > 0 && Captured[c].GetSingleCard().GameName == gamename)
					return Captured[c].GetSingleCard();
			}
			for (int c = 0; c < 20; c++)
			{
				if (Damaged[c].Count() > 0 && Damaged[c].GetSingleCard().GameName == gamename)
					return Damaged[c].GetSingleCard();
			}
			for (int c = 0; c < 12; c++)
			{
				if (Hand[c].Count() > 0 && Hand[c].GetSingleCard().GameName == gamename)
					return Hand[c].GetSingleCard();
			}

			return null;
		}
		public DecktetDeck GetCardBelongingDeck(string gamename)
		{
			if (Main.Contains(gamename))
				return Main;
			if (Discarded.Contains(gamename))
				return Discarded;
			for (int r = 0; r < BGRows; r++)
			{
				for (int c = 0; c < BGCols; c++)
				{
					if (Battleground[r][c].Count() > 0 && Battleground[r][c].GetSingleCard().GameName == gamename)
						return Battleground[r][c];
				}
			}
			for (int r = 0; r < BGRows; r++)
			{
				if (Side[r].Count() > 0 && Side[r].GetSingleCard().GameName == gamename)
					return Side[r];
			}
			for (int c = 0; c < BGCols; c++)
			{
				if (Defense[c].Count() > 0 && Defense[c].GetSingleCard().GameName == gamename)
					return Defense[c];
			}
			for (int c = 0; c < 20; c++)
			{
				if (Captured[c].Count() > 0 && Captured[c].GetSingleCard().GameName == gamename)
					return Captured[c];
			}
			for (int c = 0; c < 20; c++)
			{
				if (Damaged[c].Count() > 0 && Damaged[c].GetSingleCard().GameName == gamename)
					return Damaged[c];
			}
			for (int c = 0; c < 12; c++)
			{
				if (Hand[c].Count() > 0 && Hand[c].GetSingleCard().GameName == gamename)
					return Hand[c];
			}

			return null;
		}
	}
}
