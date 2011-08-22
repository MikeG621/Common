/*
 * Idmr.Common.Graphics.cs, Class file for common graphics functions
 * Copyright (C) 2010 Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 2 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to:
 * Free Software Foundation, Inc.
 * 59 Temple Place, Suite 330
 * Boston, MA 02111-1307 USA
 */
 
 // 110804 - ConvertTo1bpp(Bitmap, Color) - apparently hadn't implemented transparent yet...
 // 110815 - Added BoundaryException class, GetBitmapData, Added BoundaryException(string)

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Idmr.Common
{
	public class Graphics
	{
		/// <returns>The palette index of the given color</returns>
		/// <param name="red">The R component, 0-255</param>
		/// <param name="green">The G component, 0-255</param>
		/// <param name="blue">The B component, 0-255</param>
		/// <param name="palette">The Palette being used</param>
		public static byte PaletteIndex(byte red, byte green, byte blue, ColorPalette palette)
		{
			return PaletteIndex(Color.FromArgb(red, green, blue), palette);
		}
		/// <returns>The palette index of the given color</returns>
		/// <param name="color">The color being searched for</param>
		/// <param name="palette">The Palette being used</param>
		public static byte PaletteIndex(Color color, ColorPalette palette)
		{
			int diff = 9999;	// high number for default
			double temp;
			byte index=0;
			for (int i = 0; i < 0xFF; i++)
			{
				temp = Math.Pow((palette.Entries[i].R - color.R), 2) + Math.Pow((palette.Entries[i].G - color.G), 2) + Math.Pow((palette.Entries[i].B - color.B), 2);
				if (temp < diff) { diff = (int)temp; index = (byte)i; }
				if (diff == 0) break;
			}
			return index;
		}
		
		/// <returns>8bppIndexed Bitmap of the given image with the given palette</returns>
		/// <param name="image">The image to be converted</param>
		/// <param name="palette">The palette to be used</param>
		public static Bitmap ConvertTo8bpp(Bitmap image, ColorPalette palette)
		{
			image = new Bitmap(image);	// convert to 32bppRGB
			Bitmap new8bit = new Bitmap(image.Width, image.Height, PixelFormat.Format8bppIndexed);
			//BitmapData bd32 = image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadWrite, image.PixelFormat);
			BitmapData bd32 = GetBitmapData(image);
			byte[] pix32 = new byte[bd32.Stride * bd32.Height];
			CopyImageToBytes(bd32, pix32);	// 32bppImage to Bytes
			//BitmapData bd8 = new8bit.LockBits(new Rectangle(new Point(), new8bit.Size), ImageLockMode.ReadWrite, new8bit.PixelFormat);
			BitmapData bd8 = GetBitmapData(new8bit);
			byte[] pix8 = new byte[bd8.Stride * bd8.Height];
			for (int y = 0; y < image.Height; y++)
				for (int x = 0, pos32 = y*bd32.Stride, pos8 = y*bd8.Stride; x < image.Width; x++)
					pix8[pos8+x] = PaletteIndex(pix32[pos32+x*4+2], pix32[pos32+x*4+1], pix32[pos32+x*4], palette);
			CopyBytesToImage(pix8, bd8);	// Bytes to 8bppImage
			image.UnlockBits(bd32);
			new8bit.UnlockBits(bd8);
			new8bit.Palette = palette;
			return new8bit;
		}
		
		/// <returns>1bppIndexed Bitmap of the given image with black (#000) as transparent</returns>
		/// <param name="image">The image to be converted</param>
		public static Bitmap ConvertTo1bpp(Bitmap image)
		{
			return ConvertTo1bpp(image, Color.FromArgb(0,0,0));
		}
		/// <returns>1bppIndexed Bitmap of the given image</returns>
		/// <param name="image">The image to be converted</param>
		/// <param name="transparent">The color to be used for transparency</param>
		public static Bitmap ConvertTo1bpp(Bitmap image, Color transparent)
		{
			image = new Bitmap(image);	// convert to 32bppRGB
			Bitmap new1bit = new Bitmap(image.Width, image.Height, PixelFormat.Format1bppIndexed);
			//BitmapData bd32 = image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadWrite, image.PixelFormat);
			BitmapData bd32 = GetBitmapData(image);
			byte[] pix32 = new byte[bd32.Stride * bd32.Height];
			CopyImageToBytes(bd32, pix32);	// 32bppImage to Bytes
			//BitmapData bd1 = new1bit.LockBits(new Rectangle(new Point(), new1bit.Size), ImageLockMode.ReadWrite, new1bit.PixelFormat);
			BitmapData bd1 = GetBitmapData(new1bit);
			byte[] pix1 = new byte[bd1.Stride * bd1.Height];
			for (int y = 0; y < image.Height; y++)
				for (int x = 0, pos32 = y*bd32.Stride, pos1 = y*bd1.Stride; x < image.Width; x++)
					if (pix32[pos32+x*4] != transparent.B || pix32[pos32+x*4+1] != transparent.G || pix32[pos32+x*4+2] != transparent.R) pix1[pos1+x/8] |= (byte)(0x80 >> (x&7));
			CopyBytesToImage(pix1, bd1);	// Bytes to 1bppImage
			image.UnlockBits(bd32);
			new1bit.UnlockBits(bd1);
			return new1bit;
		}
		
		/// <remarks>Wrapper for the appropriate Marshal.Copy overload</remarks>
		/// <param name="imageData">The BitmapData object for the image</param>
		/// <param name="bytes">The byte array to be used for the pixel data</param>
		public static void CopyImageToBytes(BitmapData imageData, byte[] bytes)
		{
			System.Runtime.InteropServices.Marshal.Copy(imageData.Scan0, bytes, 0, bytes.Length);
		}
		
		/// <remarks>Wrapper for the appropriate Marshal.Copy overload</remarks>
		/// <param name="imageData">The BitmapData object for the image</param>
		/// <param name="bytes">The byte array to be used for the pixel data</param>
		public static void CopyBytesToImage(byte[] bytes, BitmapData imageData)
		{
			System.Runtime.InteropServices.Marshal.Copy(bytes, 0, imageData.Scan0, bytes.Length);
		}
		
		public static BitmapData GetBitmapData (Bitmap image)
		{
			return image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadWrte, image.PixelFormat);
		}
	}
	
	public class BoundaryException : Exception
	{
		/// <summary>Create a new exception with a standardized Message</summary>
		/// <param name="variable">The parameter that has caused the exception</param>
		/// <param name="limits">The acceptable bounds for <i>variable</i></param>
		/// <remarks>Message is of the form 'Parameter <i>variable</i> is not within acceptable limits (<i>limits</i>)'</remarks>
		public BoundaryException(string variable, string limits)
		{
			Message = "Parameter '" + variable + "' is not within acceptable limits (" + limits + ")";
		}
		
		/// <summary>Create a new exception with the specified Message</summary>
		/// <param name="message">The Message value to be used</param>
		public BoundaryException(string message)
		{
			Message = message;
		}
	}
}