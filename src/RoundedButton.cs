using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsForm
{
	public class RoundedButton : Button
	{
		public bool DrawTopGrayLine = false;

		public int CurrentPathWidth, CurrentPathHeight;

		GraphicsPath GetRoundPath(RectangleF Rect, int radius)
		{
			//float r2 = radius / 2f;
			GraphicsPath GraphPath = new GraphicsPath();

			GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
			GraphPath.AddLine(Rect.X + radius, Rect.Y, Rect.Width - radius - 1, Rect.Y);
			GraphPath.AddArc(Rect.X + Rect.Width - radius - 1, Rect.Y, radius, radius, 270, 90);
			GraphPath.AddLine(Rect.Width - 0, Rect.Y + radius / 2, Rect.Width - 0, Rect.Height - radius / 2 - 1);
			GraphPath.AddArc(Rect.X + Rect.Width - radius - 1, Rect.Y + Rect.Height - radius - 1, radius, radius, 0, 90);
			GraphPath.AddLine(Rect.Width - radius - 1, Rect.Height , Rect.X + radius, Rect.Height);
			GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius - 1, radius, radius, 90, 90);
			GraphPath.AddLine(Rect.X, Rect.Height - radius - 1, Rect.X, Rect.Y + radius);

			GraphPath.CloseFigure();
			return GraphPath;
		}
		GraphicsPath GetRoundPathTop(RectangleF Rect, int radius)
		{
			//float r2 = radius / 2f;
			GraphicsPath GraphPath = new GraphicsPath();

			GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
			GraphPath.AddLine(Rect.X + radius, Rect.Y, Rect.Width - radius - 1, Rect.Y);
			GraphPath.AddArc(Rect.X + Rect.Width - radius - 1, Rect.Y, radius, radius, 270, 90);

			return GraphPath;
		}

		GraphicsPath GraphPath = null, GraphPathTop = null;
		Pen pen = null;
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			base.OnPaint(e);

			int borderRadius = this.Width / 10;
			float borderThickness = 2f;
			if (GraphPath == null || CurrentPathWidth != this.Width || CurrentPathHeight != this.Height)
			{
				CurrentPathWidth = this.Width;
				CurrentPathHeight = this.Height;
				RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
				GraphPath = GetRoundPath(Rect, borderRadius);
				GraphPathTop = GetRoundPathTop(Rect, borderRadius);
				this.Region = new Region(GraphPath);
			}

			if (pen == null)
			{
				pen = new Pen(Color.Silver, borderThickness);
				pen.Alignment = PenAlignment.Inset;
			}
			if (DrawTopGrayLine)
				e.Graphics.DrawPath(pen, GraphPathTop);
		}
	}
}
