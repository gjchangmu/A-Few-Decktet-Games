using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using nsDecktet;

namespace nsForm
{
	public partial class Adaman_FormConfig : Form
	{
		public Adaman_FormConfig()
		{
			InitializeComponent();
		}

		private void btBase_Click(object sender, EventArgs e)
		{
			rbDeck36.Checked = true;
			cbNoReplenishAt5.Checked = false;
			cbAvoidUnsolvable.Checked = false;
		}

		private void btEasy_Click(object sender, EventArgs e)
		{
			rbDeck42.Checked = true;
			cbNoReplenishAt5.Checked = false;
			cbAvoidUnsolvable.Checked = true;
		}

		private void btVeryEasy_Click(object sender, EventArgs e)
		{
			rbDeck44.Checked = true;
			cbNoReplenishAt5.Checked = false;
			cbAvoidUnsolvable.Checked = true;
		}

		private void btHard_Click(object sender, EventArgs e)
		{
			rbDeck37.Checked = true;
			cbNoReplenishAt5.Checked = false;
			cbAvoidUnsolvable.Checked = false;
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (rbDeck36.Checked)
				Adaman_Config.DecktetCardCount = "36";
			else if (rbDeck37.Checked)
				Adaman_Config.DecktetCardCount = "37";
			else if (rbDeck42.Checked)
				Adaman_Config.DecktetCardCount = "42";
			else if (rbDeck44.Checked)
				Adaman_Config.DecktetCardCount = "44";

			if (cbAllowSuggest.Checked)
				Adaman_Config.AllowSuggest = true;
			else
				Adaman_Config.AllowSuggest = false;
			if (cbAllowUndo.Checked)
				Adaman_Config.AllowUndo = true;
			else
				Adaman_Config.AllowUndo = false;
			if (cbAvoidUnsolvable.Checked)
				Adaman_Config.AvoidUnsolvableGame = true;
			else
				Adaman_Config.AvoidUnsolvableGame = false;
			if (cbNoReplenishAt5.Checked)
				Adaman_Config.NoReplenishAt5 = true;
			else
				Adaman_Config.NoReplenishAt5 = false;
			if (cbAddA.Checked)
				Adaman_Config.AlternativeAceImage = true;
			else
				Adaman_Config.AlternativeAceImage = false;

			XmlSerializer xmls = new XmlSerializer(typeof(Adaman_Config));
			FileStream fs = new FileStream("./adaman_config.xml", FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			xmls.Serialize(sw, new Adaman_Config());
			sw.Close();
			fs.Close();

			this.Close();
		}

		private void Adaman_FormConfig_Load(object sender, EventArgs e)
		{
			if (Adaman_Config.DecktetCardCount == "36")
				rbDeck36.Checked = true;
			else if (Adaman_Config.DecktetCardCount == "37")
				rbDeck37.Checked = true;
			if (Adaman_Config.DecktetCardCount == "42")
				rbDeck42.Checked = true;
			if (Adaman_Config.DecktetCardCount == "44")
				rbDeck44.Checked = true;

			if (Adaman_Config.AllowSuggest)
				cbAllowSuggest.Checked = true;
			if (Adaman_Config.AllowUndo)
				cbAllowUndo.Checked = true;
			if (Adaman_Config.AvoidUnsolvableGame)
				cbAvoidUnsolvable.Checked = true;
			if (Adaman_Config.NoReplenishAt5)
				cbNoReplenishAt5.Checked = true;
			if (Adaman_Config.AlternativeAceImage)
				cbAddA.Checked = true;

			btBase.Text = "Normal\n(Original Base Rule)";
		}
	}
}
