﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using TransPick.Features.Unmanaged;

namespace TransPick.Features.Image
{
    internal static class AreaCapturer
    {
        #region ::Capture Methods::

        internal static BitmapImage CaptureArea(Point leftUpperPoint, Size size)
        {
            int left = Monitor.GetLeft();
            int top = Monitor.GetTop();
            int right = Monitor.GetRight();
            int bottom = Monitor.GetBottom();

            if (leftUpperPoint.X < left || leftUpperPoint.Y < top || leftUpperPoint.X > right || leftUpperPoint.Y > bottom)
                throw new ArgumentOutOfRangeException($"The specified LeftUpperPoint is out of screen range(Input: {leftUpperPoint.X}, {leftUpperPoint.Y}, Minimum: {left}, {top}, Maximum: {right}, {bottom}).");

            if (leftUpperPoint.X + size.Width > Monitor.GetWidth())
                throw new ArgumentOutOfRangeException($"The horizontal size of the specified area exceeds the screen range(Input: {size.Width}, Maximum: {Monitor.GetWidth()}).");

            if (leftUpperPoint.Y + size.Height > Monitor.GetHeight())
                throw new ArgumentOutOfRangeException($"The vertical size of the specified area exceeds the screen range(Input: {size.Height}, Maximum: {Monitor.GetHeight()}).");

            BitmapImage bitmap = new BitmapImage();

            using (Bitmap temp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
            {
                // Bitmap 이미지 변경을 위해 Graphics 객체 생성
                using (Graphics graphics = Graphics.FromImage(temp))
                {
                    // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                    graphics.CopyFromScreen(leftUpperPoint.X, leftUpperPoint.Y, 0, 0, temp.Size);
                }

                using (MemoryStream memory = new MemoryStream())
                {
                    temp.Save(memory, ImageFormat.Bmp);
                    memory.Position = 0;

                    bitmap.BeginInit();
                    bitmap.StreamSource = memory;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }

                temp.Dispose();
            }

            return bitmap;
        }

        #endregion
    }
}