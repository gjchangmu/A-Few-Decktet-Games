using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using nsDecktet;

namespace nsForm
{
	public partial class Main_Form : Form
	{
		

		public Main_Form()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		

		private void btAdaman_Click(object sender, EventArgs e)
		{
			Adaman_Form form = new Adaman_Form();
			this.Hide();
			form.ShowDialog();
			this.Show();
		}

		private void btMyrmex_Click(object sender, EventArgs e)
		{
			Myrmex_Form form = new Myrmex_Form();
			this.Hide();
			form.ShowDialog();
			this.Show();
		}

		private void btDefenseCapture1p_Click(object sender, EventArgs e)
		{
			DefenseCapture1p_Form form = new DefenseCapture1p_Form();
			this.Hide();
			form.ShowDialog();
			this.Show();
		}
	}
}
