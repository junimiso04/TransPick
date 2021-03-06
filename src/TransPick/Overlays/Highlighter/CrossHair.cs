﻿using GameOverlay.Windows;
using TransPick.Unmanaged;

namespace TransPick.Overlays.Highlighter
{
	internal class CrossHair : Highlighter
	{
		#region ::Constructor::

		internal CrossHair(bool isShowInfo)
		{
			_isShowInfo = isShowInfo;
		}

		#endregion

		#region ::Overlay Drawer::

		protected override void DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			var brushes = _overlay.Brushes;

			System.Drawing.Point cursorPoint = InputDevices.GetCursorPoint();

			// Draw horizontal Left/Right line.
			gfx.DashedLine(brushes["red"], Display.GetLeft(), cursorPoint.Y, cursorPoint.X - 1, cursorPoint.Y, 1.0f);
			gfx.DashedLine(brushes["red"], cursorPoint.X + 1, cursorPoint.Y, Display.GetRight(), cursorPoint.Y, 1.0f);

			// Draw vertical Top/Bottom line.
			gfx.DashedLine(brushes["blue"], cursorPoint.X, Display.GetTop(), cursorPoint.X, cursorPoint.Y - 1, 1.0f);
			gfx.DashedLine(brushes["blue"], cursorPoint.X, cursorPoint.Y + 1, cursorPoint.X, Display.GetBottom(), 1.0f);
		}

		#endregion
    }
}
