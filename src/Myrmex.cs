using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class Myrmex
	{
		public DecktetDeck MainDeck;
		public DecktetDeck[][] Columns;
		public DecktetDeck[] Floating;
		public DecktetDeck[] Finished;

		public bool PawnInsteadOfCrown;
		public int NumColumn;
		public int NumRow;
		public int RankFrom;
		public int RankTo;

		public bool AllFaceUp;

		public bool GameOver = false;
		public int GameWon = 0, GameLost = 0;
		public int FloatingCardCount;
		public int FloatingSourceColumnIndex;
		public float FloatingOrinX, FloatingOrinY;
		public int FinishedDeckCount = 0;
		//public Myrmex Beginning;

		public Myrmex()
		{
			NumColumn = Myrmex_Config.NumColumn;
			NumRow = Myrmex_Config.NumRow;
			RankFrom = Myrmex_Config.RankFrom;
			RankTo = Myrmex_Config.RankTo;

			AllFaceUp = Myrmex_Config.AllFaceUp;
		}

		public Myrmex Clone()
		{
			Myrmex clone = new Myrmex();

			clone.PawnInsteadOfCrown = PawnInsteadOfCrown;
			clone.NumColumn = NumColumn;
			clone.NumRow = NumRow;
			clone.RankFrom = RankFrom;
			clone.RankTo = RankTo;
			clone.AllFaceUp = AllFaceUp;
			clone.GameOver = GameOver;
			clone.GameWon = GameWon;
			clone.GameLost = GameLost;
			clone.FinishedDeckCount = FinishedDeckCount;

			clone.MainDeck = MainDeck.Clone();
			clone.Columns = new DecktetDeck[NumColumn][];
			for (int c = 0; c < NumColumn; c++)
			{
				clone.Columns[c] = new DecktetDeck[30];
				for (int r = 0; r < 30; r++)
				{
					clone.Columns[c][r] = Columns[c][r].Clone();
				}
			}
			clone.Floating = new DecktetDeck[15];
			for (int f = 0; f < 15; f++)
				clone.Floating[f] = Floating[f].Clone();
			clone.Finished = new DecktetDeck[6];
			for (int f = 0; f < 6; f++)
				clone.Finished[f] = Finished[f].Clone();

			clone.AllComponentsForUI = AllComponentsForUI;

			return clone;
		}
	
		public void Setup(bool seeded = false)
		{
			if (!PawnInsteadOfCrown)
			{
				MainDeck = DecktetDeck.New36();
				MainDeck.GameName = "Main";

				//foreach (DecktetCard card in MainDeck.Cards)
				//	card.GameName = card.ShortName;
				DecktetDeck MainDeck2 = DecktetDeck.New36();
				MainDeck2.PositionX = 63;
				MainDeck2.PositionY = 560;
				MainDeck2.DestroyRank("Ace");
				MainDeck2.DestroyRank("Crown");
				foreach (DecktetCard card in MainDeck2.Cards)
					card.GameName = card.ShortName + "2";
				MainDeck.Extend(MainDeck2);

			}

			MainDeck.SetGameRandNum("Crown", 10);
			MainDeck.PositionX = 63;
			MainDeck.PositionY = 560;

			if (RankTo <= 9)
			{
				MainDeck.DestroyRank("Crown");
				for (int i = RankTo + 1; i <= 9; i++)
					MainDeck.DestroyRank(i.ToString());
			}
			if (RankTo == 11)
			{
				DecktetCard p = DecktetDeck.NewCard("Pawn_wly");
				DecktetCard p2 = DecktetDeck.NewCard("Pawn_wly");
				p.GameName = p.ShortName;
				p2.GameName = p2.ShortName + "2";
				MainDeck.Add(p);
				MainDeck.Add(p2);
				p = DecktetDeck.NewCard("Pawn_msl");
				p2 = DecktetDeck.NewCard("Pawn_msl");
				p.GameName = p.ShortName;
				p2.GameName = p2.ShortName + "2";
				MainDeck.Add(p);
				MainDeck.Add(p2);
				p = DecktetDeck.NewCard("Pawn_myk");
				p2 = DecktetDeck.NewCard("Pawn_myk");
				p.GameName = p.ShortName;
				p2.GameName = p2.ShortName + "2";
				MainDeck.Add(p);
				MainDeck.Add(p2);
				MainDeck.SetGameRandNum("Pawn", 10);
				MainDeck.SetGameRandNum("Crown", 11);
			}
			if (RankFrom >= 2)
				MainDeck.DestroyRank("Ace");
			if (RankFrom <= 9)
			{
				for (int i = 2; i <= RankFrom - 1; i++)
					MainDeck.DestroyRank(i.ToString());
			}

			foreach (DecktetCard card in MainDeck.Cards)
			{
				card.UIFlyOnTop = false;
				card.UIFlyInstantly = false;
			}
			MainDeck.InitAllCardsPosition();
			MainDeck.Shuffle();

			foreach (DecktetCard card in MainDeck.Cards)
				AllComponentsForUI.Add(card.GameName);

			Columns = new DecktetDeck[NumColumn][];
			for (int c = 0; c < NumColumn; c++)
			{
				Columns[c] = new DecktetDeck[30];
				for (int r = 0; r < 30; r++) 
				{
			
					Columns[c][r] = new DecktetDeck();
					Columns[c][r].GameName = "Column_" + c + "," + r;
					Columns[c][r].GameInfo1 = c;
					Columns[c][r].GameInfo2 = r;
					Columns[c][r].PositionX = 176 + c * 109;
					Columns[c][r].PositionY = 80 + r * Myrmex_Config.RowPositionInterval;
					Columns[c][r].UIZOrder = r + c;
					if (r < NumRow)
					{
						if (!AllFaceUp)
							Columns[c][r].Add(MainDeck.Draw(r == NumRow - 1), (r * NumColumn + c) * 0.1f);
						else
							Columns[c][r].Add(MainDeck.Draw(true), (r * NumColumn + c) * 0.1f);
					}
				}
			}

			Floating = new DecktetDeck[15];
			for (int r = 0; r < 15; r++)
			{
				Floating[r] = new DecktetDeck();
				Floating[r].GameName = "Floating_" + r;
				//Floating[r].PositionX = 0;
				//Floating[r].PositionY = 0;
				Floating[r].UIZOrder = r + 50;
			}

			Finished = new DecktetDeck[6];
			for (int f = 0; f < 6; f++)
			{
				Finished[f] = new DecktetDeck();
				Finished[f].GameName = "Finished_" + f;
				Finished[f].PositionX = 63;
				Finished[f].PositionY = 235 + f * (Myrmex_Config.RowPositionInterval + 3);
				Finished[f].UIZOrder = f * 10;
			}
		}

		public int GetColumnCardCount(int column)
		{
			int n = 0;
			while (Columns[column][n].Count() == 1)
				n++;
			return n;
		}
		public int GetColumnIndex(string gamename)
		{
			DecktetDeck deck = GetCardBelongingDeck(gamename);
			if (deck.GameName != "Main")
				return deck.GameInfo1;
			else
				throw new Exception();
		}
		public int GetRowIndex(string gamename)
		{
			DecktetDeck deck = GetCardBelongingDeck(gamename);
			if (deck.GameName != "Main")
				return deck.GameInfo2;
			else
				throw new Exception();
		}

		public List<int[]> GetAllValidActions()
		{
			List<int[]> all = new List<int[]>();

			for (int ci_src = 0; ci_src < NumColumn; ci_src++)
			{
				for (int ci_dest = 0; ci_dest < NumColumn; ci_dest++)
				{
					if (ci_dest == ci_src)
						continue;
					for (int ri = GetColumnCardCount(ci_src) - 1; ri >= 0; ri--)
					{
						// move the cards at row ri and downward, from column ci_src to column ci_dest
						int[] action = new int[4];
						action[0] = 1;
						action[1] = ci_src;
						action[2] = ci_dest;
						action[3] = ri;

						if (CheckActionValid(action))
							all.Add(action);
					}
				}
			}

			if (MainDeck.Count() > 0)
			{
				// deal
				int[] action = new int[4];
				action[0] = 2;
				action[1] = 0;
				action[2] = 0;
				action[3] = 0;
				all.Add(action);
			}
		
			return all;
		}
		public List<int[]> GetNValidActionsWithHighestScore(int n)
		{
			List<int[]> all = GetAllValidActions();
			List<KeyValuePair<int[], int>> action_score = new List<KeyValuePair<int[], int>>();
			foreach (int[] a in all)
			{
				int score = ScoreAction(a);

				int ci;
				for (ci = action_score.Count - 1; ci >= 0; ci--)
				{
					if (score < action_score[ci].Value)
						break;
				}
				if (ci != action_score.Count - 1 || ci < n - 1)
				{
					action_score.Insert(ci + 1, new KeyValuePair<int[], int>(a, score));
					if (action_score.Count > n)
						action_score.RemoveAt(n);
				}
			}
			List<int[]> output = new List<int[]>();
			foreach (KeyValuePair<int[], int> pair in action_score)
				output.Add(pair.Key);
			return output;
		}
		public bool CheckFloatingUpValid(int columni, int rowi)
		{
			int toprank = Columns[columni][rowi].GetSingleCard().GameRankNum;
			int suits = 0x3F;
			for (int rii = rowi; rii < GetColumnCardCount(columni); rii++)
			{
				DecktetCard card = Columns[columni][rii].GetSingleCard();
				if (card.GameRankNum != toprank)
					return false;
				if (!card.FaceUp)
					return false;
				if (!card.HasSuit("Moon")) suits = suits & 0x3E;
				if (!card.HasSuit("Sun")) suits = suits & 0x3D;
				if (!card.HasSuit("Wave")) suits = suits & 0x3B;
				if (!card.HasSuit("Leaf")) suits = suits & 0x37;
				if (!card.HasSuit("Wyrm")) suits = suits & 0x2F;
				if (!card.HasSuit("Knot")) suits = suits & 0x1F;
				toprank--;
			}
			if (suits == 0)
				return false;

			return true;
		}
		public bool CheckFloatingDownValid(int columni)
		{
			if (columni == FloatingSourceColumnIndex)
				return false;
			int destn = GetColumnCardCount(columni);
			if (destn > 0 && Columns[columni][destn - 1].GetSingleCard().GameRankNum != Floating[0].GetSingleCard().GameRankNum + 1)
				return false;
			return true;
		}
		public bool CheckActionValid(int[] action)
		{
			int ci_src = action[1];
			int ci_dest = action[2];
			int ri = action[3];
			if (action[0] == 1)
			{
				bool movable = CheckFloatingUpValid(ci_src, ri);
				if (!movable)
					return false;

				int destn = GetColumnCardCount(ci_dest);
				if (destn > 0 && Columns[ci_dest][destn - 1].GetSingleCard().GameRankNum != Columns[ci_src][ri].GetSingleCard().GameRankNum + 1)
					return false;
			}

			if (action[0] == 2 && MainDeck.Count() == 0)
				return false;

			return true;
		}
		bool[] resourses_sa;
		public int ScoreAction(int[] action)
		{
			int score = 0;

			return score;
		}
		public int[] GuessBestAction()
		{
			string strategyname = "score";
			List<int[]> all = GetAllValidActions();

			// random
			if (strategyname == "random")
			{
				int ai = Util.Rnd.Next(all.Count);
				return all[ai];
			}

			// score
			if (strategyname == "score")
			{
				int maxscore = -1000;
				int[] maxscore_a = null;
				foreach (int[] a in all)
				{
					int score = ScoreAction(a);
					if (score > maxscore)
					{
						maxscore = score;
						maxscore_a = a;
					}
				}

				return maxscore_a;
			}

			throw new Exception();
		}
		public int[] DeduceBestAction()
		{
			List<int[]> top = GetNValidActionsWithHighestScore(3);

			// score and try each
			Dictionary<int[], int> tried = new Dictionary<int[], int>();
			foreach (int[] action in top)
			{
				int lostcount = 0;
				for (int trygame = 0; trygame < 20; trygame++)
				{
					int tryround = 20;
					Myrmex deduceboard = this.Clone();
					deduceboard.MainDeck.Shuffle();
					deduceboard.TakeAction(action);
					while (!deduceboard.GameOver && tryround > 0)
					{
						tryround--;
						deduceboard.TakeAction(deduceboard.GuessBestAction());
					}
					if (deduceboard.GameWon > 0 && deduceboard.GameLost > 0)
						throw new Exception();
					if (deduceboard.GameLost > 0)
						lostcount++;
				}

				tried.Add(action, lostcount);
			}

			int minlostcount = 10000;
			int[] minlostcount_a = null;
			foreach (int[] a in tried.Keys)
				if (tried[a] < minlostcount)
				{
					minlostcount = tried[a];
					minlostcount_a = a;
				}
			return minlostcount_a;
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
			if (!CheckActionValid(action))
				throw new Exception();

	
		}
		public void FloatingUp(int columni, int rowi)
		{
			FloatingCardCount = GetColumnCardCount(columni) - rowi;

			int srccardcnt = GetColumnCardCount(columni);
			for (int rii = rowi; rii < srccardcnt; rii++)
			{
				DecktetCard card = Columns[columni][rii].GetSingleCard();
				Floating[rii - rowi].PositionX = card.PositionX[0];
				Floating[rii - rowi].PositionY = card.PositionY[0];
				Floating[rii - rowi].Add(Columns[columni][rii].PickUpSingle());

				card.UIFlyInstantly = true;
			}
			FloatingSourceColumnIndex = columni;
			FloatingOrinX = Floating[0].PositionX;
			FloatingOrinY = Floating[0].PositionY;
		}
		public void FloatingDown(int columni)
		{
			int destn = GetColumnCardCount(columni);
			for (int rii = 0; rii < FloatingCardCount; rii++)
			{
				Floating[rii].GetSingleCard().UIFlyInstantly = false;
				Columns[columni][rii + destn].Add(Floating[rii].PickUpSingle());
			}
			FloatingCardCount = 0;

			// flip a card to face-up if revealed
			int srcn = GetColumnCardCount(FloatingSourceColumnIndex);
			if (srcn > 0)
			{
				Columns[FloatingSourceColumnIndex][srcn - 1].GetSingleCard().UIFlipFaceStartTime = DateTime.Now.AddSeconds(0.3f);
				Columns[FloatingSourceColumnIndex][srcn - 1].GetSingleCard().FaceUp = true;
			}

			// check a finished pile
			CheckApplyFinishedPile(columni);

			CheckGameOver();
		}
		public void FloatingBack()
		{
			int destn = GetColumnCardCount(FloatingSourceColumnIndex);
			for (int rii = 0; rii < FloatingCardCount; rii++)
			{
				Floating[rii].GetSingleCard().UIFlyInstantly = false;
				Columns[FloatingSourceColumnIndex][rii + destn].Add(Floating[rii].PickUpSingle(), 0, 10);
			}
			FloatingCardCount = 0;
		}
		public void SetFloatingPosition(int dx, int dy)
		{
			for (int f = 0; f < FloatingCardCount; f++)
			{
				Floating[f].PositionX = FloatingOrinX + dx;
				Floating[f].PositionY = FloatingOrinY + dy + Myrmex_Config.RowPositionInterval * f;
				Floating[f].InitAllCardsPosition();
			}
		}
		public void Deal()
		{
			if (MainDeck.Count() == 0)
				throw new Exception();

			for (int c = 0; c < NumColumn && MainDeck.Count() > 0; c++)
			{
				Columns[c][GetColumnCardCount(c)].Add(MainDeck.Draw(true), 0.15f * c);
				CheckApplyFinishedPile(c, 0.15f * c + 1f);
			}

			CheckGameOver();
		}
		public void CheckApplyFinishedPile(int columni, float delay = 0f)
		{
			int pilelength = RankTo - RankFrom + 1;
			int destn = GetColumnCardCount(columni);
			if (destn >= pilelength && CheckFloatingUpValid(columni, destn - pilelength))
			{
				int count = 0;
				if (RankTo < 10)
					for (int f = destn - pilelength; f < destn; f++)
						Finished[FinishedDeckCount].Add(Columns[columni][f].PickUpSingle(), delay + 0.3f + count++ * 0.15f, 40 + f);
				else
					for (int f = destn - 1; f >= destn - pilelength; f--)
						Finished[FinishedDeckCount].Add(Columns[columni][f].PickUpSingle(), delay + 0.3f + count++ * 0.15f, 40 + f * 2);
				FinishedDeckCount++;

				// flip a card to face-up if revealed after finishing a pile
				int srcn = GetColumnCardCount(columni);
				if (srcn > 0)
				{
					Columns[columni][srcn - 1].GetSingleCard().UIFlipFaceStartTime = DateTime.Now.AddSeconds(delay + 1.6f);
					Columns[columni][srcn - 1].GetSingleCard().FaceUp = true;
				}
			}
		}
		public void CheckGameOver()
		{
			if (FinishedDeckCount == 6)
			{
				GameOver = true;
				GameWon = 1;
			}
			else if (GetAllValidActions().Count == 0)
			{
				GameOver = true;
				GameLost = 1;
			}
		}

		public List<string> AllComponentsForUI = new List<string>();
		public DecktetCard GetComponent(string gamename)
		{
			for (int c = 0; c < NumColumn; c++)
			{
				for (int r = 0; r < GetColumnCardCount(c); r++)
				{
					if (Columns[c][r].Count() > 0 && Columns[c][r].GetSingleCard().GameName == gamename)
						return Columns[c][r].GetSingleCard();
				}
			}
			for (int f = 0; f < FloatingCardCount; f++)
			{
				if (Floating[f].Count() > 0 && Floating[f].GetSingleCard().GameName == gamename)
					return Floating[f].GetSingleCard();
			}
			for (int f = 0; f < 6; f++)
			{
				if (Finished[f].Contains(gamename))
					return Finished[f].GetCard(gamename);
			}
			if (MainDeck.Contains(gamename))
				return MainDeck.GetCard(gamename);
			return null;
		}
		public DecktetDeck GetCardBelongingDeck(string gamename)
		{
			for (int c = 0; c < NumColumn; c++)
			{
				for (int r = 0; r < GetColumnCardCount(c); r++)
				{
					if (Columns[c][r].Count() > 0 && Columns[c][r].GetSingleCard().GameName == gamename)
						return Columns[c][r];
				}
			}
			for (int f = 0; f < FloatingCardCount; f++)
			{
				if (Floating[f].Count() > 0 && Floating[f].GetSingleCard().GameName == gamename)
					return Floating[f];
			}
			for (int f = 0; f < 6; f++)
			{
				if (Finished[f].Contains(gamename))
					return Finished[f];
			}
			if (MainDeck.Contains(gamename))
				return MainDeck;
			return null;
		}
	}
}
