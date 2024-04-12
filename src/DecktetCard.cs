using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class DecktetCard
	{
		public string Rank;
		public List<string> Suits = new List<string>();
		public List<string> Types = new List<string>();
		public bool FaceUp = false;
		public bool Sideways;

		public string ShortName;
		public int RankNum;

		public string GameName;
		public int GameRankNum;

		public DecktetCard()
		{
		}
		public DecktetCard(string rank, string suits, string types)
		{
			Rank = rank;
			if (rank == "0" || rank == "1" || rank == "2" || rank == "3" || rank == "4" || rank == "5" || rank == "6" || rank == "7" || rank == "8" || rank == "9")
				RankNum = int.Parse(rank);
			else if (rank == "Ace")
				RankNum = 1;
			else if (rank == "Pawn")
				RankNum = 10;
			else if (rank == "Court")
				RankNum = 11;
			else if (rank == "Crown")
				RankNum = 12;
			else
				throw new Exception();
			GameRankNum = RankNum;

			if (rank == "0" && suits.Length != 0)
				throw new Exception();
			else if (rank == "Ace" && suits.Length != 1)
				throw new Exception();
			else if (rank == "Pawn" && suits.Length != 3)
				throw new Exception();
			else if (rank == "Court" && suits.Length != 3)
				throw new Exception();
			else if (rank == "Crown" && suits.Length != 1)
				throw new Exception();
			foreach (char suit in suits)
			{
				if (suit == 'm')
					Suits.Add("Moon");
				if (suit == 's')
					Suits.Add("Sun");
				if (suit == 'w')
					Suits.Add("Wave");
				if (suit == 'l')
					Suits.Add("Leaf");
				if (suit == 'y')
					Suits.Add("Wyrm");
				if (suit == 'k')
					Suits.Add("Knot");
			}

			if (types.Contains("p"))
				Types.Add("Personality");
			if (types.Contains("l"))
				Types.Add("Location");
			if (types.Contains("e"))
				Types.Add("Event");

			ShortName = Rank + "_";
			foreach (string suit in Suits)
			{
				string suitshort;
				if (suit == "Wyrm")
					suitshort = "y";
				else
					suitshort = suit.Substring(0, 1).ToLower();
				ShortName += suitshort;
			}

			GameName = ShortName;
		}
		public DecktetCard Clone()
		{
			DecktetCard clone = new DecktetCard();
			clone.Rank = Rank;
			clone.Suits = Suits; // shallow copy
			clone.Types = Types; // shallow copy
			clone.FaceUp = FaceUp;
			clone.Sideways = Sideways;
			//clone.PositionX = PositionX;
			//clone.PositionY = PositionY;

			clone.ShortName = ShortName;
			clone.RankNum = RankNum;
			clone.GameName = GameName;
			clone.GameRankNum = GameRankNum;

			//clone.UIX = UIX;
			//clone.UIY = UIY;
			//clone.UIFlyingZOrder = UIFlyingZOrder;
			clone.UIAlwaysFlyOnTop = UIAlwaysFlyOnTop;
			clone.UIAlwaysFlyInstantly = UIAlwaysFlyInstantly;

			return clone;
		}

		public bool HasSuit(string suit)
		{
			//if (suit != "Moon" && suit != "Sun" && suit != "Wave" && suit != "Leaf" && suit != "Wyrm" && suit != "Knot")
			//	throw new Exception();

			return Suits.Contains(suit);
		}
		public bool ShareSuitWith(DecktetCard card)
		{
			foreach (string suit in Suits)
				if (card.HasSuit(suit))
					return true;

			return false;
		}
		public bool HasType(string type)
		{
			if (type != "Personality" && type != "Location" && type != "Event")
				throw new Exception();

			return Types.Contains(type);
		}
		public bool IsPersonality()
		{
			return Types.Contains("Personality");
		}

		// only for UI, not related to any game logic
		public List<UIAnimationStep> UIAnimationSteps = new List<UIAnimationStep>();
		public bool UIAlwaysFlyOnTop = true;
		public bool UIAlwaysFlyInstantly = false;
		public DateTime UIFlipFaceStartTime;
	}
}
