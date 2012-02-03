/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.0
 */
 
/* CHANGE LOG
 * 040811 - ConvertTo1bpp(Bitmap, Color) - apparently hadn't implemented transparent yet...
 * 150811 - Added GetBitmapData
 * 270811 - added array size validation to Copy*To*, added PaletteIndex(,Color[]) overloads
 * 290112 - added GetBitmapData(Bitmap, PixelFormat)
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Idmr.Common
{
	/// <summary>Graphics functions commonly used in Idmr software</summary>
	public class GraphicsFunctions
	{
		#region PaletteIndex
		/// <summary>Returns the first palette index of the closest match to the desired color</summary>
		/// <param name="red">The R component of the desired color</param>
		/// <param name="green">The G component of the desired color</param>
		/// <param name="blue">The B component of the desired color</param>
		/// <param name="palette">The Palette being used</param>
		public static byte PaletteIndex(byte red, byte green, byte blue, ColorPalette palette)
		{
			return PaletteIndex(Color.FromArgb(red, green, blue), palette.Entries);
		}
		/// <summary>Returns the first palette index of the closest match to the desired color</summary>
		/// <param name="color">The desired color</param>
		/// <param name="palette">The Palette being used</param>
		public static byte PaletteIndex(Color color, ColorPalette palette)
		{
			return PaletteIndex(color, palette.Entries);
		}
		/// <summary>Returns the first palette index of the closest match to the desired color</summary>
		/// <param name="red">The R component of the desired color</param>
		/// <param name="green">The R component of the desired color</param>
		/// <param name="blue">The R component of the desired color</param>
		/// <param name="colors">The Color array being used</param>
		public static byte PaletteIndex(byte red, byte green, byte blue, Color[] colors)
		{
			return PaletteIndex(Color.FromArgb(red, green, blue), colors);
		}
		/// <summary>Returns the first palette index of the closest match to the desired color</summary>
		/// <param name="color">The desired color</param>
		/// <param name="colors">The Color array being used</param>
		public static byte PaletteIndex(Color color, Color[] colors)
		{
			int diff = 9999;
			double temp;
			byte index = 0;
			for (int i = 0; i < colors.Length; i++)
			{
				temp = Math.Pow((colors[i].R - color.R), 2) + Math.Pow((colors[i].G - color.G), 2) + Math.Pow((colors[i].B - color.B), 2);
				if (temp < diff) { diff = (int)temp; index = (byte)i; }
				if (diff == 0) break;
			}
			return index;
		}
		#endregion PaletteIndex

		#region ConvertTo8bpp
		/// <summary>Returns an 8bppIndexed Bitmap of the given image with the given palette</summary>
		/// <param name="image">The image to be converted</param>
		/// <param name="palette">The palette to be used</param>
		public static Bitmap ConvertTo8bpp(Bitmap image, ColorPalette palette)
		{
			image = new Bitmap(image);	// convert to 32bppRGB
			Bitmap new8bit = new Bitmap(image.Width, image.Height, PixelFormat.Format8bppIndexed);
			BitmapData bd32 = GetBitmapData(image);
			byte[] pix32 = new byte[bd32.Stride * bd32.Height];
			CopyImageToBytes(bd32, pix32);	// 32bppImage to Bytes
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
		/// <summary>Returns an 8bppIndexed Bitmap of the given image with the given colors</summary>
		/// <param name="image">The image to be converted</param>
		/// <param name="colors">The array of Colors to be used</param>
		public static Bitmap ConvertTo8bpp(Bitmap image, Color[] colors)
		{
			ColorPalette pal = new Bitmap(1, 1, PixelFormat.Format8bppIndexed).Palette;
			for (int i = 0; i < colors.Length; i++) pal.Entries[i] = colors[i];
			return ConvertTo8bpp(image, pal);
		}
		#endregion

		#region ConvertTo1bpp
		/// <summary>Returns a 1bppIndexed Bitmap of the given image with black (#000) as transparent</summary>
		/// <param name="image">The image to be converted</param>
		public static Bitmap ConvertTo1bpp(Bitmap image)
		{
			return ConvertTo1bpp(image, Color.FromArgb(0,0,0));
		}
		/// <summary>Returns a 1bppIndexed Bitmap of the given image</summary>
		/// <param name="image">The image to be converted</param>
		/// <param name="transparent">The color to be used for transparency</param>
		public static Bitmap ConvertTo1bpp(Bitmap image, Color transparent)
		{
			image = new Bitmap(image);	// convert to 32bppRGB
			Bitmap new1bit = new Bitmap(image.Width, image.Height, PixelFormat.Format1bppIndexed);
			BitmapData bd32 = GetBitmapData(image);
			byte[] pix32 = new byte[bd32.Stride * bd32.Height];
			CopyImageToBytes(bd32, pix32);	// 32bppImage to Bytes
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
		#endregion

		/// <summary>Wrapper for the appropriate Marshal.Copy overload</summary>
		/// <param name="imageData">The BitmapData object for the image</param>
		/// <param name="bytes">The byte array to be used for the pixel data</param>
		/// <exception cref="System.ArgumentException"><i>bytes</i> is not the correct length for <i>imageData</i></exception>
		public static void CopyImageToBytes(BitmapData imageData, byte[] bytes)
		{
			if (bytes.Length != imageData.Stride * imageData.Height) throw new ArgumentException("Pixel array size does not match image type", "bytes");
			System.Runtime.InteropServices.Marshal.Copy(imageData.Scan0, bytes, 0, bytes.Length);
		}

		/// <summary>Wrapper for the appropriate Marshal.Copy overload</summary>
		/// <param name="imageData">The BitmapData object for the image</param>
		/// <param name="bytes">The byte array to be used for the pixel data</param>
		/// <exception cref="System.ArgumentException"><i>bytes</i> is not the correct length for <i>imageData</i></exception>
		public static void CopyBytesToImage(byte[] bytes, BitmapData imageData)
		{
			if (bytes.Length != imageData.Stride * imageData.Height) throw new ArgumentException("Image type does not match pixel array size", "imageData");
			System.Runtime.InteropServices.Marshal.Copy(bytes, 0, imageData.Scan0, bytes.Length);
		}

		#region GetBitmapData
		/// <summary>Returns the BitmapData object for the given image</summary>
		/// <param name="image">The image</param>
		public static BitmapData GetBitmapData(Bitmap image)
		{
			return image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadWrite, image.PixelFormat);
		}

		/// <summary>Returns the BitmapData object for the given image with the specified PixelFormat</summary>
		/// <param name="image">The image</param>
		/// <param name="pixelFormat">The desired PixelFormat</param>
		public static BitmapData GetBitmapData(Bitmap image, PixelFormat pixelFormat)
		{
			return image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadWrite, pixelFormat);
		}
		#endregion

		/// <summary>Returns the array of only Colors used in an 8bppIndexed image</summary>
		/// <param name="image">The image to parse, will be modified</param>
		/// <exception cref="System.ArgumentException"><i>image</i> is not 8bppIndexed</exception>
		/// <remarks><i>image</i> is parsed and all color indexes that are not used are removed. <i>image</i> is updated</remarks>
		public static Color[] GetTrimmedColors(Bitmap image)
		{
			if (image.PixelFormat != PixelFormat.Format8bppIndexed) throw new ArgumentException("image must be 8bppIndexed", "image");
			// get 8bpp data
			ColorPalette pal = image.Palette;
			BitmapData bd8 = GetBitmapData(image);
			byte[] pixels8 = new byte[bd8.Stride * bd8.Height];
			bool[] used = new bool[256];
			for (int y = 0; y < bd8.Height; y++)
				for (int x = 0, pos8 = y * bd8.Stride; x < bd8.Width; x++)
					used[pixels8[pos8 + x]] = true;
			// removed unused palette entries
			int count = 1;
			for (int c = 1; c < 256; c++)
			{
				if (!used[c])
				{
					for (int i = count; i < pal.Entries.Length - 1; i++) pal.Entries[i] = pal.Entries[i+1];
					for (int i = 0; i < pixels8.Length; i++) if (pixels8[i] > count) pixels8[i]--;
				}
				else count++;
			}
			CopyBytesToImage(pixels8, bd8);
			image.UnlockBits(bd8);
			// Prep color array
			Color[] colors = new Color[count];
			for (int c = 0; c < count; c++) colors[c] = pal.Entries[c];
			image.Palette = pal;
			return colors;
		}
	}
}