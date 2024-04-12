using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class Adaman_Config
	{
		public static string DecktetCardCount = "36";
		public static int CardPerRow = 5;
		public static bool NoReplenishAt5 = false;

		public static bool AvoidUnsolvableGame = false;
		public static bool AllowUndo = true;
		public static bool AllowSuggest = true;
		public static bool AlternativeAceImage = false;

		public static float AnimationSpeed = 1500; // (pixels/s)
		public static float AnimationDelay = 0.3f; // (s)

		public string DecktetCardCount_s { get { return DecktetCardCount; } set { DecktetCardCount = value; } }
		public int CardPerRow_s { get { return CardPerRow; } set { CardPerRow = value; } }
		public bool NoReplenishAt5_s { get { return NoReplenishAt5; } set { NoReplenishAt5 = value; } }
		public bool AvoidUnsolvableGame_s { get { return AvoidUnsolvableGame; } set { AvoidUnsolvableGame = value; } }
		public bool AllowUndo_s { get { return AllowUndo; } set { AllowUndo = value; } }
		public bool AllowSuggest_s { get { return AllowSuggest; } set { AllowSuggest = value; } }
		public bool AlternativeAceImage_s { get { return AlternativeAceImage; } set { AlternativeAceImage = value; } }

	}
}
