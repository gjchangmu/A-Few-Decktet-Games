using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDecktet
{
	public class Token
	{
		public string Type;
		public string ShortName; // determines which image from resources to use

		public string GameName;

		public Token()
		{
		}
		public Token Clone()
		{
			Token clone = new Token();
			clone.Type = Type;
			clone.ShortName = ShortName;
			clone.GameName = GameName;

			clone.UIAlwaysFlyOnTop = UIAlwaysFlyOnTop;
			clone.UIAlwaysFlyInstantly = UIAlwaysFlyInstantly;

			return clone;
		}

		// only for UI, not related to any game logic
		public List<UIAnimationStep> UIAnimationSteps = new List<UIAnimationStep>();
		public bool UIAlwaysFlyOnTop = true;
		public bool UIAlwaysFlyInstantly = false;
	}
}
