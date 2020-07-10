﻿using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TransPick.Unmanaged;

namespace TransPick.Overlays
{
    internal class ScreenHighlighter
    {
		#region ::Fields::

		private OverlayBase _overlay;

		private readonly bool _isShowInfo;

		#endregion

		#region ::Constructor::

		internal ScreenHighlighter( bool isShowInfo)
		{
			_isShowInfo = isShowInfo;
		}

		#endregion

		#region ::Overlay Drawer::

		private void DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			var brushes = _overlay.Brushes;
			var fonts = _overlay.Fonts;

			Screen screen = Screen.FromPoint(InputDevices.GetCursorPoint());

			// Draw area rectangle.
			gfx.DrawRectangle(brushes["red"], screen.Bounds.Left, screen.Bounds.Top, screen.Bounds.Right, screen.Bounds.Bottom, 2.0f);

			// Draw area size box.
			string text = $"{(screen.Primary ? "Primary" : "Sub")} | {screen.Bounds.Width} X {screen.Bounds.Height}";
			gfx.DrawTextWithBackground(fonts["arial-12"], brushes["red"], brushes["white"], screen.Bounds.Left + 6, screen.Bounds.Top + 6, text);
		}

		#endregion

		#region ::Highlighter Starting & Stopping Methods::

		internal void Start()
		{
			_overlay = new OverlayBase(DrawGraphics, _isShowInfo);
			_overlay.Run();
		}

		internal async void StartAsync()
		{
			_overlay = new OverlayBase(DrawGraphics, _isShowInfo);
			var result = await _overlay.RunAsync();
		}

		internal void Stop()
		{
			if (_overlay != null)
			{
				_overlay.Dispose();
			}
		}

		#endregion
	}
}
