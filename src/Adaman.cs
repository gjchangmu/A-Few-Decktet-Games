using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class ActionRatePair
	{
		public ActionRatePair(int[] action, float loserate)
		{
			Action = action;
			LoseRate = loserate;
		}
		public int[] Action;
		public float LoseRate;
	}
	public class Adaman
	{
		public DecktetDeck MainDeck;
		public DecktetDeck DiscardedDeck;
		public DecktetDeck DiscardedPersonalityDeck;
		public DecktetDeck[] ResourceCard;
		public DecktetDeck[] CapitalCard;
		public DecktetDeck[] PalaceCard;
		public DecktetDeck Palace6th;

		public string DecktetCardCount;
		public int CardPerRow;
		public bool NoReplenishAt5;

		public bool GameOver = false;
		public int GameWon = 0, GameLost = 0;
		public int TotalPersonCount;
		public Adaman Beginning;

		public Adaman()
		{
			DecktetCardCount = Adaman_Config.DecktetCardCount;
			CardPerRow = Adaman_Config.CardPerRow;
			NoReplenishAt5 = Adaman_Config.NoReplenishAt5;

			ResourceCard = new DecktetDeck[CardPerRow];
			CapitalCard = new DecktetDeck[CardPerRow];
			PalaceCard = new DecktetDeck[CardPerRow];
			for (int i = 0; i < CardPerRow; i++)
			{
				ResourceCard[i] = new DecktetDeck("R" + i);
				ResourceCard[i].PositionX = i * 150 + 280;
				ResourceCard[i].PositionY = 530;
				CapitalCard[i] = new DecktetDeck("C" + i);
				CapitalCard[i].PositionX = i * 150 + 280;
				CapitalCard[i].PositionY = 320;
				PalaceCard[i] = new DecktetDeck("P" + i);
				PalaceCard[i].PositionX = i * 150 + 280;
				PalaceCard[i].PositionY = 110;
			}
			Palace6th = new DecktetDeck("P6th");
			Palace6th.PositionX = CardPerRow * 150 + 280;
			Palace6th.PositionY = 110;

			resources_cav = new bool[CardPerRow];
			resourses_sa = new bool[CardPerRow];
		}

		public Adaman Clone()
		{
			Adaman clone = new Adaman();
			clone.MainDeck = MainDeck.Clone();
			clone.DiscardedDeck = DiscardedDeck.Clone();
			clone.DiscardedPersonalityDeck = DiscardedPersonalityDeck.Clone();
			for (int i = 0; i < CardPerRow; i++)
			{
				if (!ResourceCard[i].IsEmpty())
					clone.ResourceCard[i] = ResourceCard[i].Clone();
				if (!CapitalCard[i].IsEmpty())
					clone.CapitalCard[i] = CapitalCard[i].Clone();
				if (!PalaceCard[i].IsEmpty())
					clone.PalaceCard[i] = PalaceCard[i].Clone();
			}
			clone.Palace6th = Palace6th.Clone();
			clone.GameOver = GameOver;
			clone.GameWon = GameWon;
			clone.GameLost = GameLost;
			clone.TotalPersonCount = TotalPersonCount;

			clone.DecktetCardCount = DecktetCardCount;
			clone.CardPerRow = CardPerRow;
			clone.NoReplenishAt5 = NoReplenishAt5;

			clone.AllComponentsForUI = AllComponentsForUI; // shalow copy

			return clone;
		}
		public Adaman GetReset()
		{
			Beginning.Beginning = Beginning.Clone();
			return Beginning;
		}

		public int GetEmptyResourceIndex()
		{
			for (int i = 0; i < CardPerRow; i++)
				if (ResourceCard[i].IsEmpty())
					return i;
			return -1;
		}
		public int GetEmptyPalaceIndex()
		{
			for (int i = 0; i < CardPerRow; i++)
				if (PalaceCard[i].IsEmpty())
					return i;
			return -1;
		}

		public void Setup(bool seeded = false)
		{
			if (DecktetCardCount == "36")
				MainDeck = DecktetDeck.New36();
			else if (DecktetCardCount == "44")
				MainDeck = DecktetDeck.New44();
			else if (DecktetCardCount == "42")
			{
				MainDeck = DecktetDeck.New44();
				MainDeck.Destroy("Pawn_wly");
				MainDeck.Destroy("Court_mly");
			}
			else if (DecktetCardCount == "37")
			{
				MainDeck = DecktetDeck.New36();
				MainDeck.Add(DecktetDeck.NewCard("Pawn_myk"));
			}
			foreach (DecktetCard card in MainDeck.Cards)
			{
				card.GameName = card.ShortName;
				if (card.Types.Contains("Personality"))
					card.GameName += "(p)";
			}

			foreach (DecktetCard card in MainDeck.Cards)
				AllComponentsForUI.Add(card.GameName);

			TotalPersonCount = MainDeck.CountType("Personality");
			MainDeck.GameName = "Main";
			MainDeck.SetGameRandNum("Pawn", 10);
			MainDeck.SetGameRandNum("Court", 10);
			MainDeck.SetGameRandNum("Crown", 10);
			MainDeck.Shuffle(seeded);
			MainDeck.PositionX = 90;
			MainDeck.PositionY = 150;
			MainDeck.InitAllCardsPosition();

			DiscardedDeck = new DecktetDeck();
			DiscardedDeck.GameName = "Discarded";
			DiscardedDeck.PositionX = 600;
			DiscardedDeck.PositionY = 900;

			DiscardedPersonalityDeck = new DecktetDeck();
			DiscardedPersonalityDeck.GameName = "DiscardedFace";
			DiscardedPersonalityDeck.PositionX = 90;
			DiscardedPersonalityDeck.PositionY = 430;

			ResourceReplenish();
			if (GameOver)
				return;

			List<int[]> all = GetAllValidActions();
			if (all.Count == 0)
			{
				GameOver = true;
				GameLost = 2;
			}

			Beginning = this.Clone();
		}
		public void ResourceReplenish()
		{
			if (GameOver)
				return;

			float deltatime = 0f;
			int seq = 1;
			for (int i = 0; i < CardPerRow; i++)
				if (CapitalCard[i].IsEmpty() && !MainDeck.IsEmpty())
				{
					seq++;
					CapitalCard[i].Add(MainDeck.Draw(), deltatime + seq * Adaman_Config.AnimationDelay, 20 - seq);
				}

			while (GetEmptyResourceIndex() != -1 && !MainDeck.IsEmpty() && !(NoReplenishAt5 && GetEmptyPalaceIndex() == -1))
			{
				int emptyr = GetEmptyResourceIndex();
				DecktetCard drawcard = MainDeck.Draw();
				if (drawcard.IsPersonality())
				{
					int emptyp = GetEmptyPalaceIndex();
					if (emptyp == -1)
					{
						seq++;
						Palace6th.Add(drawcard, deltatime + seq * Adaman_Config.AnimationDelay, 20 - seq);
						GameOver = true;
						GameLost = 1;
						return;
					}
					seq++;
					PalaceCard[emptyp].Add(drawcard, deltatime + seq * Adaman_Config.AnimationDelay, 20 - seq);
				}
				else
				{
					seq++;
					ResourceCard[emptyr].Add(drawcard, deltatime + seq * Adaman_Config.AnimationDelay, 20 - seq);
				}
			}
		}

		public List<int[]> GetAllValidActions()
		{
			List<int[]> all = new List<int[]>();
			for (int psi = 0; psi < CardPerRow; psi++)
				for (int r = 0; r < (1 << CardPerRow); r++)
				{
					int[] action = new int[2 + CardPerRow];
					action[0] = psi;
					action[1] = -1;
					for (int b = 0; b < CardPerRow; b++)
						action[2 + b] = (r & (1 << b)) != 0 ? 1 : 0;

					if (CheckActionValid(action))
						all.Add(action);
				}
			for (int csi = 0; csi < CardPerRow; csi++)
				for (int r = 0; r < (1 << CardPerRow); r++)
				{
					int[] action = new int[2 + CardPerRow];
					action[0] = -1;
					action[1] = csi;
					for (int b = 0; b < CardPerRow; b++)
						action[2 + b] = (r & (1 << b)) != 0 ? 1 : 0;

					if (CheckActionValid(action))
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
		bool[] resources_cav;
		public bool CheckActionValid(int[] action)
		{
			int palacesi = action[0];
			int capitalsi = action[1];
			
			for (int i = 0; i < CardPerRow; i++)
				resources_cav[i] = action[2 + i] == 1;

			for (int i = 0; i < CardPerRow; i++)
				if (resources_cav[i] && ResourceCard[i].IsEmpty())
					return false;
			if (palacesi != -1 && PalaceCard[palacesi].IsEmpty())
				return false;
			if (capitalsi != -1 && CapitalCard[capitalsi].IsEmpty())
				return false;


			if (palacesi == -1 && capitalsi == -1)
				return false;
			if (palacesi != -1 && capitalsi != -1)
				throw new Exception();

			int sumrank = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resources_cav[i])
					sumrank += ResourceCard[i].GetSingleCard().GameRankNum;
			if (sumrank == 0)
				return false;

			DecktetCard aim;
			if (palacesi != -1)
				aim = PalaceCard[palacesi].GetSingleCard();
			else
				aim = CapitalCard[capitalsi].GetSingleCard();

			if (sumrank < aim.GameRankNum)
				return false;
			for (int i = 0; i < CardPerRow; i++)
				if (resources_cav[i] && !ResourceCard[i].GetSingleCard().ShareSuitWith(aim))
					return false;

			return true;
		}
		bool[] resourses_sa;
		public int ScoreAction(int[] action)
		{
			if (DecktetCardCount == "36")
				return ScoreAction36(action);
			else if (DecktetCardCount == "37")
				return ScoreAction36(action);
			else if (DecktetCardCount == "44")
				return ScoreAction44(action);
			else if (DecktetCardCount == "42")
				return ScoreAction42(action);
			else
				throw new Exception();
		}
		public int ScoreAction36(int[] action)
		{
			int palacesi = action[0];
			int capitalsi = action[1];
			bool aim_in_palace, aim_in_capital;
			DecktetCard aim;
			if (palacesi >= 0)
			{
				aim_in_palace = true;
				aim_in_capital = false;
				aim = PalaceCard[palacesi].GetSingleCard();
			}
			else if (capitalsi >= 0)
			{
				aim_in_palace = false;
				aim_in_capital = true;
				aim = CapitalCard[capitalsi].GetSingleCard();
			}
			else
				throw new Exception();
			
			for (int i = 0; i < CardPerRow; i++)
				resourses_sa[i] = action[2 + i] == 1;

			int score = 0;
			if (aim_in_palace)
				score += 20;
			if (aim_in_palace && aim.GameRankNum == 9)
				score += 50;
			if (aim.GameRankNum == 10)
				score += 100;
			if (aim_in_capital && aim.IsPersonality())
				score += 20;
			if (aim_in_capital && aim.GameRankNum == 1)
				score -= 20;
			if (aim_in_capital && !aim.IsPersonality())
				score -= (9 - aim.GameRankNum) * 4;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i] && ResourceCard[i].GetSingleCard().GameRankNum == 1)
					score += 10;

			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					score += 1;

			int sumrank = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumrank += ResourceCard[i].GetSingleCard().GameRankNum;
			score -= sumrank * 4;
			score += aim.GameRankNum * 1;
			int sumsuit = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumsuit += ResourceCard[i].GetSingleCard().Suits.Count;
			score -= sumsuit * 5;

			if (aim_in_capital && aim.Suits.Count == 1 && !aim.IsPersonality())
				score -= 50;
			else if (aim_in_palace && aim.Suits.Count == 3)
				score -= 2;

			int summoonsun = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
				{
					summoonsun += (ResourceCard[i].GetSingleCard().Suits.Contains("Moon") ? 4 : 0) * ResourceCard[i].GetSingleCard().GameRankNum;
					summoonsun += (ResourceCard[i].GetSingleCard().Suits.Contains("Sun") ? 6 : 0) * ResourceCard[i].GetSingleCard().GameRankNum;
				}
			summoonsun -= (aim.Suits.Contains("Moon") ? 4 : 0) * aim.GameRankNum;
			summoonsun -= (aim.Suits.Contains("Sun") ? 6 : 0) * aim.GameRankNum;
			score -= summoonsun * 1;

			return score;
		}
		public int ScoreAction42(int[] action)
		{
			int palacesi = action[0];
			int capitalsi = action[1];
			bool aim_in_palace, aim_in_capital;
			DecktetCard aim;
			if (palacesi >= 0)
			{
				aim_in_palace = true;
				aim_in_capital = false;
				aim = PalaceCard[palacesi].GetSingleCard();
			}
			else if (capitalsi >= 0)
			{
				aim_in_palace = false;
				aim_in_capital = true;
				aim = CapitalCard[capitalsi].GetSingleCard();
			}
			else
				throw new Exception();

			for (int i = 0; i < CardPerRow; i++)
				resourses_sa[i] = action[2 + i] == 1;

			int score = 0;
			if (aim_in_palace)
				score += 20;
			if (aim.GameRankNum >= 9)
				score += 20;
			if (aim_in_capital && aim.IsPersonality())
				score += 20;
			if (aim_in_capital && aim.GameRankNum == 1)
				score -= 20;
			if (aim_in_capital && !aim.IsPersonality())
				score -= (9 - aim.GameRankNum) * 3;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i] && ResourceCard[i].GetSingleCard().GameRankNum == 1)
					score += 20;

			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					score += 1;

			int sumrank = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumrank += ResourceCard[i].GetSingleCard().GameRankNum;
			score -= sumrank * 4;
			score += aim.GameRankNum * 1;
			int sumsuit = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumsuit += ResourceCard[i].GetSingleCard().Suits.Count;
			score -= sumsuit * 5;

			if (aim_in_capital && aim.Suits.Count == 1 && !aim.IsPersonality())
				score -= 30;
			else if (aim_in_palace && aim.Suits.Count == 3)
				score -= 2;

			return score;
		}
		public int ScoreAction44(int[] action)
		{
			int palacesi = action[0];
			int capitalsi = action[1];
			bool aim_in_palace, aim_in_capital;
			DecktetCard aim;
			if (palacesi >= 0)
			{
				aim_in_palace = true;
				aim_in_capital = false;
				aim = PalaceCard[palacesi].GetSingleCard();
			}
			else if (capitalsi >= 0)
			{
				aim_in_palace = false;
				aim_in_capital = true;
				aim = CapitalCard[capitalsi].GetSingleCard();
			}
			else
				throw new Exception();

			for (int i = 0; i < CardPerRow; i++)
				resourses_sa[i] = action[2 + i] == 1;

			int score = 0;
			if (aim_in_palace)
				score += 20;
			if (aim.GameRankNum >= 9)
				score += 20;
			if (aim_in_capital && aim.IsPersonality())
				score += 20;
			if (aim_in_capital && aim.GameRankNum == 1)
				score -= 20;
			if (aim_in_capital && !aim.IsPersonality())
				score -= (9 - aim.GameRankNum) * 3;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i] && ResourceCard[i].GetSingleCard().GameRankNum == 1)
					score += 20;

			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					score += 1;

			int sumrank = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumrank += ResourceCard[i].GetSingleCard().GameRankNum;
			score -= sumrank * 4;
			score += aim.GameRankNum * 1;
			int sumsuit = 0;
			for (int i = 0; i < CardPerRow; i++)
				if (resourses_sa[i])
					sumsuit += ResourceCard[i].GetSingleCard().Suits.Count;
			score -= sumsuit * 5;

			if (aim_in_capital && aim.Suits.Count == 1 && !aim.IsPersonality())
				score -= 30;
			else if (aim_in_palace && aim.Suits.Count == 3)
				score -= 2;

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
					Adaman deduceboard = this.Clone();
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
		public ActionRatePair DeduceLoseRate(int level = 0)
		{
			if (level > 1)
			{
				int trycount = 20;
				int lostcount = 0;
				for (int i = 0; i < trycount; i++)
				{
					Adaman deduceboard = this.Clone();
					deduceboard.MainDeck.Shuffle();
					int tryround = 20;
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

				// temp debug
				/*
				for (int li = 0; li < level; li++)
					Console.Write("   ");
				Console.Write("leaf: " + ((float)lostcount / trycount).ToString() + "\n");
				*/

				return new ActionRatePair(null, (float)lostcount / trycount);
			}
			else
			{
				List<int[]> top = GetNValidActionsWithHighestScore(3);

				Dictionary <int[], float> action_loserate = new Dictionary<int[], float>();
				foreach (int[] action in top)
				{
					Adaman deduceboard = this.Clone();
					deduceboard.MainDeck.Shuffle();

					// temp debug
					/*
					for (int li = 0; li < level; li++)
						Console.Write("   ");
					Console.Write("taking action: [");
					PrintActionFeatures(action);
					Console.Write("]\n");
					*/

					deduceboard.TakeAction(action);
					if (deduceboard.GameLost > 0)
					{
						action_loserate[action] = 1;
					}
					else if (deduceboard.GameWon > 0)
					{
						action_loserate[action] = 0;
					}
					else
					{
						ActionRatePair pair = deduceboard.DeduceLoseRate(level + 1);
						action_loserate[action] = pair.LoseRate;
					}
				}
				int[] minlostrate_a = null;
				float minlostrate = 10000;
				float sumlostrate = 0;
				foreach (int[] a in action_loserate.Keys)
				{
					sumlostrate += action_loserate[a];
					if (action_loserate[a] < minlostrate)
					{
						minlostrate_a = a;
						minlostrate = action_loserate[a];
					}
				}
				float avelostrate = sumlostrate / action_loserate.Count;
				if (minlostrate_a == null)
					throw new Exception();

				float aveormin = minlostrate;

				// temp debug
				/*
				for (int li = 0; li < level; li++)
					Console.Write("   ");
				Console.Write("node best: " + aveormin.ToString() + " [");
				PrintActionFeatures(minlostrate_a);
				Console.Write("]\n");
				*/

				return new ActionRatePair(minlostrate_a, aveormin);
			}
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

			int palacesi = action[0];
			int capitalsi = action[1];
			bool[] resources = new bool[CardPerRow];
			for (int i = 0; i < CardPerRow; i++)
				resources[i] = action[2 + i] == 1;

			for (int i = 0; i < CardPerRow; i++)
				if (resources[i])
				{
					DiscardedDeck.Add(ResourceCard[i].PickUpSingle());
				}
			if (palacesi != -1)
			{
				DiscardedPersonalityDeck.Add(PalaceCard[palacesi].PickUpSingle());
			}
			else if (capitalsi != -1)
			{
				if (CapitalCard[capitalsi].GetSingleCard().IsPersonality())
				{
					DiscardedPersonalityDeck.Add(CapitalCard[capitalsi].PickUpSingle());
				}
				else
				{
					for (int i = 0; i < CardPerRow; i++)
						if (ResourceCard[i].IsEmpty())
						{
							ResourceCard[i].Add(CapitalCard[capitalsi].PickUpSingle(), 0.3f);
							break;
						}
				}
			}
			ResourceReplenish();
			if (GameOver)
				return;

			if (DiscardedPersonalityDeck.CountType("Personality") == TotalPersonCount)
			{
				GameOver = true;
				GameWon = 1;
				return;
			}
			List<int[]> all = GetAllValidActions();
			if (all.Count == 0)
			{
				GameOver = true;
				GameLost = 2;
				return;
			}
		}
		public int ScoreGame()
		{
			int score = 0;
			foreach (DecktetCard card in DiscardedPersonalityDeck.Cards)
				if (card.IsPersonality())
					score += card.GameRankNum;

			if (GameWon > 0)
				foreach (DecktetDeck deck in ResourceCard)
					if (!deck.IsEmpty())
						score += deck.GetSingleCard().GameRankNum;

			return score;
		}

		public List<string> AllComponentsForUI = new List<string>();
		public DecktetCard GetComponent(string gamename)
		{
			for (int i = 0; i < CardPerRow; i++)
			{
				if (!ResourceCard[i].IsEmpty() && ResourceCard[i].GetSingleCard().GameName == gamename)
					return ResourceCard[i].GetSingleCard();
				if (!CapitalCard[i].IsEmpty() && CapitalCard[i].GetSingleCard().GameName == gamename)
					return CapitalCard[i].GetSingleCard();
				if (!PalaceCard[i].IsEmpty() && PalaceCard[i].GetSingleCard().GameName == gamename)
					return PalaceCard[i].GetSingleCard();
			}
			foreach (DecktetCard card in MainDeck.Cards)
				if (card.GameName == gamename)
					return card;
			foreach (DecktetCard card in DiscardedDeck.Cards)
				if (card.GameName == gamename)
					return card;
			foreach (DecktetCard card in DiscardedPersonalityDeck.Cards)
				if (card.GameName == gamename)
					return card;
			if (!Palace6th.IsEmpty() && Palace6th.GetSingleCard().GameName == gamename)
				return Palace6th.GetSingleCard();

			return null;
		}
		public DecktetDeck GetCardBelongingDeck(string gamename)
		{
			for (int i = 0; i < CardPerRow; i++)
			{
				if (!ResourceCard[i].IsEmpty() && ResourceCard[i].GetSingleCard().GameName == gamename)
					return ResourceCard[i];
				if (!CapitalCard[i].IsEmpty() && CapitalCard[i].GetSingleCard().GameName == gamename)
					return CapitalCard[i];
				if (!PalaceCard[i].IsEmpty() && PalaceCard[i].GetSingleCard().GameName == gamename)
					return PalaceCard[i];
			}
			foreach (DecktetCard card in MainDeck.Cards)
				if (card.GameName == gamename)
					return MainDeck;
			foreach (DecktetCard card in DiscardedDeck.Cards)
				if (card.GameName == gamename)
					return DiscardedDeck;
			foreach (DecktetCard card in DiscardedPersonalityDeck.Cards)
				if (card.GameName == gamename)
					return DiscardedPersonalityDeck;
			if (!Palace6th.IsEmpty() && Palace6th.GetSingleCard().GameName == gamename)
				return Palace6th;

			return null;
		}
	}
}
