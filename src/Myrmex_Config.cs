using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class Myrmex_Config
	{
		public static int NumColumn = 8;
		public static int NumRow = 4;
		public static int RankFrom = 1;
		public static int RankTo = 10;

		public static bool AllFaceUp = false;

		public static bool AvoidUnsolvableGame = false;
		public static bool AllowUndo = true;
		public static bool AllowSuggest = false;

		public static int RowPositionInterval = 20;
		public static float AnimationSpeed = 1500; // (pixels/s)

		// the following are used by xml serializor
		public int NumColumn_s { get { return NumColumn; } set { NumColumn = value; } }
		public int NumRow_s { get { return NumRow; } set { NumRow = value; } }
		public int RankFrom_s { get { return RankFrom; } set { RankFrom = value; } }
		public int RankTo_s { get { return RankTo; } set { RankTo = value; } }
		public bool AllFaceUp_s { get { return AllFaceUp; } set { AllFaceUp = value; } }
		public bool AvoidUnsolvableGame_s { get { return AvoidUnsolvableGame; } set { AvoidUnsolvableGame = value; } }
		public bool AllowUndo_s { get { return AllowUndo; } set { AllowUndo = value; } }
		public bool AllowSuggest_s { get { return AllowSuggest; } set { AllowSuggest = value; } }
	}
}
