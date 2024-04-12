using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class DecktetDeck
	{
		public string GameName;
		public List<DecktetCard> Cards = new List<DecktetCard>();
		public float PositionX, PositionY;
		public float SizeW, SizeH;
		//public bool UpdateArravingCardPosition = true;

		public int GameInfo1, GameInfo2;

		public DecktetDeck()
		{
			//GameName = Guid.NewGuid().ToString();
		}
		public DecktetDeck(string name)
		{
			GameName = name;
		}
		public static DecktetDeck New36()
		{
			DecktetDeck deck = new DecktetDeck();

			deck.Add(new DecktetCard("Ace", "k", ""));
			deck.Add(new DecktetCard("Ace", "l", ""));
			deck.Add(new DecktetCard("Ace", "m", ""));
			deck.Add(new DecktetCard("Ace", "s", ""));
			deck.Add(new DecktetCard("Ace", "w", ""));
			deck.Add(new DecktetCard("Ace", "y", ""));
			deck.Add(new DecktetCard("2", "mk", "p"));
			deck.Add(new DecktetCard("2", "sy", "l"));
			deck.Add(new DecktetCard("2", "wl", "le"));
			deck.Add(new DecktetCard("3", "mw", "e"));
			deck.Add(new DecktetCard("3", "sk", "p"));
			deck.Add(new DecktetCard("3", "ly", "p"));
			deck.Add(new DecktetCard("4", "yk", "e"));
			deck.Add(new DecktetCard("4", "ms", "l"));
			deck.Add(new DecktetCard("4", "wl", "p"));
			deck.Add(new DecktetCard("5", "sw", "e"));
			deck.Add(new DecktetCard("5", "ml", "l"));
			deck.Add(new DecktetCard("5", "yk", "p"));
			deck.Add(new DecktetCard("6", "mw", "p"));
			deck.Add(new DecktetCard("6", "lk", "le"));
			deck.Add(new DecktetCard("6", "sy", "p"));
			deck.Add(new DecktetCard("7", "sk", "l"));
			deck.Add(new DecktetCard("7", "wy", "l"));
			deck.Add(new DecktetCard("7", "ml", "e"));
			deck.Add(new DecktetCard("8", "yk", "e"));
			deck.Add(new DecktetCard("8", "ms", "p"));
			deck.Add(new DecktetCard("8", "wl", "l"));
			deck.Add(new DecktetCard("9", "wy", "l"));
			deck.Add(new DecktetCard("9", "lk", "p"));
			deck.Add(new DecktetCard("9", "ms", "e"));
			deck.Add(new DecktetCard("Crown", "k", "l"));
			deck.Add(new DecktetCard("Crown", "l", "le"));
			deck.Add(new DecktetCard("Crown", "m", "p"));
			deck.Add(new DecktetCard("Crown", "s", "p"));
			deck.Add(new DecktetCard("Crown", "w", "l"));
			deck.Add(new DecktetCard("Crown", "y", "e"));

			return deck;
		}
		public static DecktetDeck New44()
		{
			DecktetDeck deck = DecktetDeck.New36();
			deck.Add(new DecktetCard("Pawn", "wly", "l"));
			deck.Add(new DecktetCard("Pawn", "msl", "e"));
			deck.Add(new DecktetCard("Pawn", "swk", "p"));
			deck.Add(new DecktetCard("Pawn", "myk", "p"));
			deck.Add(new DecktetCard("Court", "mwk", "p"));
			deck.Add(new DecktetCard("Court", "swy", "l"));
			deck.Add(new DecktetCard("Court", "mly", "e"));
			deck.Add(new DecktetCard("Court", "slk", "l"));
			return deck;
		}
		public static DecktetDeck New45()
		{
			DecktetDeck deck = DecktetDeck.New44();
			deck.Add(new DecktetCard("0", "", ""));
			return deck;
		}
		public static DecktetCard NewCard(string shortname)
		{
			DecktetDeck dd = New45();
			foreach (DecktetCard card in dd.Cards)
				if (card.ShortName == shortname)
					return card;
			return null;
		}
		public DecktetDeck Clone()
		{
			DecktetDeck clone = new DecktetDeck();
			clone.GameName = GameName;
			clone.PositionX = PositionX;
			clone.PositionY = PositionY;
			clone.SizeW = SizeW;
			clone.SizeH = SizeH;
			clone.GameInfo1 = GameInfo1;
			clone.GameInfo2 = GameInfo2;

			clone.UIZOrder = UIZOrder;

			//clone.UpdateArravingCardPosition = UpdateArravingCardPosition;
			foreach (DecktetCard card in Cards)
				clone.Add(card.Clone());

			return clone;
		}

		public void SetGameRandNum(string rank, int ranknum)
		{
			foreach (DecktetCard card in Cards)
				if (card.Rank == rank)
					card.GameRankNum = ranknum;
		}

		public bool IsEmpty()
		{
			return Cards.Count == 0;
		}
		public int Count()
		{
			return Cards.Count;
		}
		public int CountType(string type)
		{
			int count = 0;
			foreach (DecktetCard card in Cards)
				if (card.HasType(type))
					count++;
			return count;
		}
		public bool Contains(string gamename)
		{
			foreach (DecktetCard card in Cards)
				if (card.GameName == gamename)
					return true;
			return false;
		}
		public DecktetCard GetSingleCard()
		{
			if (Cards.Count != 1)
				throw new Exception();
			return Cards[0];
		}
		public DecktetCard GetCard(string gamename)
		{
			foreach (DecktetCard card in Cards)
				if (card.GameName == gamename)
					return card;
			return null;
		}

		public void Add(DecktetCard card, float timedelay = 0, int flyingzorder = 0)
		{
			if (Cards.Contains(card))
				throw new Exception();

			Cards.Add(card);
			if (true)
			{
				UIAnimationStep step = new UIAnimationStep();
				step.PositionX = PositionX;
				step.PositionY = PositionY;
				step.SizeW = SizeW;
				step.SizeH = SizeH;
				step.FlyZOrder = flyingzorder;
				step.StartTime = DateTime.Now.AddSeconds(timedelay);
				card.UIAnimationSteps.Add(step);

			}
		}
		public void AddAtBottom(DecktetCard card, float timedelay = 0, int flyingzorder = 0)
		{
			if (Cards.Contains(card))
				throw new Exception();

			Cards.Insert(0, card);
			if (true)
			{
				UIAnimationStep step = new UIAnimationStep();
				step.PositionX = PositionX;
				step.PositionY = PositionY;
				step.SizeW = SizeW;
				step.SizeH = SizeH;
				step.FlyZOrder = flyingzorder;
				step.StartTime = DateTime.Now.AddSeconds(timedelay);
				card.UIAnimationSteps.Add(step);
			}
		}
		public void Extend(DecktetDeck deck, float timedelay = 0)
		{
			foreach (DecktetCard card in deck.Cards)
			{
				if (Cards.Contains(card))
					throw new Exception();
				Cards.Add(card);
				if (true)
				{
					UIAnimationStep step = new UIAnimationStep();
					step.PositionX = PositionX;
					step.PositionY = PositionY;
					step.SizeW = SizeW;
					step.SizeH = SizeH;
					step.StartTime = DateTime.Now.AddSeconds(timedelay);
					card.UIAnimationSteps.Add(step);
				}
			}
		}
		public void Destroy(string shortname)
		{
			Pull(shortname);
		}
		public void DestroyRank(string rank)
		{
			for (int i = Cards.Count - 1; i >= 0; i--)
			{
				if (Cards[i].Rank == rank)
				{
					Cards.RemoveAt(i);
				}
			}
		}
		public void Shuffle(bool seeded = false)
		{
			if (seeded)
				Util.NextSeed();

			int n = Cards.Count;
			while (n > 1)
			{
				n--;
				int k;
				if (seeded)
					k = Util.SeededRnd.Next(n + 1);
				else
					k = Util.Rnd.Next(n + 1);
				DecktetCard card = Cards[k];
				Cards[k] = Cards[n];
				Cards[n] = card;
			}
		}
		public DecktetCard Draw(bool tofaceup = true)
		{
			DecktetCard card = Cards[Cards.Count - 1];
			Cards.RemoveAt(Cards.Count - 1);
			if (tofaceup)
				card.FaceUp = true;
			return card;
		}
		public DecktetCard PickUpSingle()
		{
			if (Cards.Count != 1)
				throw new Exception();
			DecktetCard card = Cards[0];
			Cards.Clear();
			return card;
		}
		public DecktetCard Pull(string shortname, bool tofaceup = true)
		{
			int count = 0;
			DecktetCard thecard = null;
			foreach (DecktetCard card in Cards)
			{
				if (card.ShortName.StartsWith(shortname))
				{
					thecard = card;
					count++;
				}
			}
			if (count == 1)
			{
				Cards.Remove(thecard);
				if (tofaceup)
					thecard.FaceUp = true;
				return thecard;
			}
			else
			{
				throw new Exception();
			}
		}
		public DecktetCard Pull(DecktetCard thecard, bool tofaceup = true)
		{
			int count = 0;
			foreach (DecktetCard card in Cards)
			{
				if (card == thecard)
					count++;
			}
			if (count == 1)
			{
				Cards.Remove(thecard);
				if (tofaceup)
					thecard.FaceUp = true;
				return thecard;
			}
			else
			{
				throw new Exception();
			}
		}

		public void InitAllCardsPosition()
		{
			foreach (DecktetCard card in Cards)
			{
				UIAnimationStep step = new UIAnimationStep();
				step.PositionX = PositionX;
				step.PositionY = PositionY;
				step.SizeW = SizeW;
				step.SizeH = SizeH;
				step.FlyZOrder = 0;
				step.StartTime = DateTime.Now;
				card.UIAnimationSteps.Clear();
				card.UIAnimationSteps.Add(step);
			}
		}
	// only for UI, not related to any game logic
	public int UIZOrder;
	}
}
