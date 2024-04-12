using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public static class Util
	{
		public static int SeedTick = 0;
		public static Random SeededRnd;
		public static void NextSeed()
		{
			SeedTick++;
			SeededRnd = new Random(SeedTick);
		}

		public static Random Rnd = new Random();
	}
}
