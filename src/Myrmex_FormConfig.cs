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
	public partial class Myrmex_FormConfig : Form
	{
		public Myrmex_FormConfig()
		{
			InitializeComponent();
		}

		private void btBase_Click(object sender, EventArgs e)
		{
			rbColumn8.Checked = true;
			rbRow4.Checked = true;
			rbRangeFromA.Checked = true;
			rbRangeTo10.Checked = true;
			cbAllFaceUp.Checked = false;
		}

		private void btNormal_Click(object sender, EventArgs e)
		{
			rbColumn10.Checked = true;
			rbRow4.Checked = true;
			rbRangeFromA.Checked = true;
			rbRangeTo10.Checked = true;
			cbAllFaceUp.Checked = false;
		}

		private void btEasy_Click(object sender, EventArgs e)
		{
			rbColumn10.Checked = true;
			rbRow3.Checked = true;
			rbRangeFromA.Checked = true;
			rbRangeTo9.Checked = true;
			cbAllFaceUp.Checked = false;
		}

		private void btVeryEasy_Click(object sender, EventArgs e)
		{
			rbColumn10.Checked = true;
			rbRow3.Checked = true;
			rbRangeFromA.Checked = true;
			rbRangeTo7.Checked = true;
			cbAllFaceUp.Checked = false;
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (rbColumn7.Checked)
				Myrmex_Config.NumColumn = 7;
			else if (rbColumn8.Checked)
				Myrmex_Config.NumColumn = 8;
			else if (rbColumn9.Checked)
				Myrmex_Config.NumColumn = 9;
			else if (rbColumn10.Checked)
				Myrmex_Config.NumColumn = 10;

			if (rbRow1.Checked)
				Myrmex_Config.NumRow = 1;
			else if (rbRow2.Checked)
				Myrmex_Config.NumRow = 2;
			else if (rbRow3.Checked)
				Myrmex_Config.NumRow = 3;
			else if (rbRow4.Checked)
				Myrmex_Config.NumRow = 4;
			else if (rbRow5.Checked)
				Myrmex_Config.NumRow = 5;

			if (rbRangeFromA.Checked)
				Myrmex_Config.RankFrom = 1;
			else if (rbRangeFrom2.Checked)
				Myrmex_Config.RankFrom = 2;

			if (rbRangeTo5.Checked)
				Myrmex_Config.RankTo = 5;
			else if (rbRangeTo7.Checked)
				Myrmex_Config.RankTo = 7;
			else if (rbRangeTo9.Checked)
				Myrmex_Config.RankTo = 9;
			else if (rbRangeTo10.Checked)
				Myrmex_Config.RankTo = 10;
			else if (rbRangeTo11.Checked)
				Myrmex_Config.RankTo = 11;

			if (cbAllFaceUp.Checked)
				Myrmex_Config.AllFaceUp = true;
			else
				Myrmex_Config.AllFaceUp = false;

			if (cbAllowSuggest.Checked)
				Myrmex_Config.AllowSuggest = true;
			else
				Myrmex_Config.AllowSuggest = false;
			if (cbAllowUndo.Checked)
				Myrmex_Config.AllowUndo = true;
			else
				Myrmex_Config.AllowUndo = false;
			if (cbAvoidUnsolvable.Checked)
				Myrmex_Config.AvoidUnsolvableGame = true;
			else
				Myrmex_Config.AvoidUnsolvableGame = false;

			XmlSerializer xmls = new XmlSerializer(typeof(Myrmex_Config));
			FileStream fs = new FileStream("./myrmex_config.xml", FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			xmls.Serialize(sw, new Myrmex_Config());
			sw.Close();
			fs.Close();

			this.Close();
		}

		private void Adaman_FormConfig_Load(object sender, EventArgs e)
		{
			btBase.Text = "Hard\n(Original Rule)";
			if (Myrmex_Config.NumColumn == 7)
				rbColumn7.Checked = true;
			else if (Myrmex_Config.NumColumn == 8)
				rbColumn8.Checked = true;
			else if (Myrmex_Config.NumColumn == 9)
				rbColumn9.Checked = true;
			else if (Myrmex_Config.NumColumn == 10)
				rbColumn10.Checked = true;

			if (Myrmex_Config.NumRow == 1)
				rbRow1.Checked = true;
			else if (Myrmex_Config.NumRow == 2)
				rbRow2.Checked = true;
			else if (Myrmex_Config.NumRow == 3)
				rbRow3.Checked = true;
			else if (Myrmex_Config.NumRow == 4)
				rbRow4.Checked = true;
			else if (Myrmex_Config.NumRow == 5)
				rbRow5.Checked = true;

			if (Myrmex_Config.RankFrom == 1)
				rbRangeFromA.Checked = true;
			else if (Myrmex_Config.RankFrom == 2)
				rbRangeFrom2.Checked = true;

			if (Myrmex_Config.RankTo == 5)
				rbRangeTo5.Checked = true;
			else if (Myrmex_Config.RankTo == 7)
				rbRangeTo7.Checked = true;
			else if (Myrmex_Config.RankTo == 9)
				rbRangeTo9.Checked = true;
			else if (Myrmex_Config.RankTo == 10)
				rbRangeTo10.Checked = true;
			else if (Myrmex_Config.RankTo == 11)
				rbRangeTo11.Checked = true;

			if (Myrmex_Config.AllFaceUp)
				cbAllFaceUp.Checked = true;

			if (Myrmex_Config.AllowSuggest)
				cbAllowSuggest.Checked = true;
			if (Myrmex_Config.AllowUndo)
				cbAllowUndo.Checked = true;
			if (Myrmex_Config.AvoidUnsolvableGame)
				cbAvoidUnsolvable.Checked = true;
		}
	}
}
