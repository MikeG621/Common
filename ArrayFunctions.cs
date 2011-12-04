/*
 * Idmr.Common.ArrayFunctions.cs, Class file for common array functions
 * Copyright (C) 2011 Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 3.0 of the License, or (at your
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

using System;

namespace Idmr.Common
{
	/// <summary>Primarily a wrapper for Buffer.BlockCopy</summary>
	public class ArrayFunctions
	{
		/// <summary>Retrieves the ASCII string from the byte array with the given location and length</summary>
		/// <param name"array">The raw byte array</param>
		/// <param name="offset">The location of the starting character</param>
		/// <param name="length">The number of characters to read</param>
		/// <remarks>String is assumed to be ASCII-encoded, trims null chars</remarks>
		public static string ReadStringFromArray(byte[] array, int offset, int length) { return System.Text.Encoding.ASCII.GetString(array, offset, length).Trim('\0'); }
		
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in <i>fullArray</i></param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		public static void TrimArray(byte[] fullArray, int offset, byte[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length);
		}
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in fullArray</param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		public static void TrimArray(byte[] fullArray, int offset, short[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length << 1);
		}
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in fullArray</param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		public static void TrimArray(short[] fullArray, int offset, short[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length << 1);
		}
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in fullArray</param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		public static void TrimArray(byte[] fullArray, int offset, int[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length << 2);
		}
		
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented 4</param>
		public static void WriteToArray(int value, byte[] array, ref int offset)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 4);
			offset += 4;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented 2</param>
		public static void WriteToArray(short value, byte[] array, ref int offset)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 2);
			offset += 2;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented the length of the string</param>
		public static void WriteToArray(string value, byte[] array, ref int offset)
		{
			Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(value), 0, array, offset, value.Length);
			offset += value.Length;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented the length of <i>value</i></param>
		public static void WriteToArray(byte[] value,  byte[] array, ref int offset)
		{
			Buffer.BlockCopy(value, 0, array, offset, value.Length);
			offset += value.Length;
		}
		/// <summary>Copy value to the array at offset</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		public static void WriteToArray(int value, byte[] array, int offset) { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 4); }
		/// <summary>Copy value to the array at offset</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		public static void WriteToArray(short value, byte[] array, int offset) { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 2); }
		/// <summary>Copy value to the array at offset</summary>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		public static void WriteToArray(string value, byte[] array, int offset) { Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(value), 0, array, offset, value.Length); }
		/// <summary>Copy value to the array at offset</summary>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		public static void WriteToArray(byte[] value, byte[] array, int offset) { Buffer.BlockCopy(value, 0, array, offset, value.Length); }
	}
}