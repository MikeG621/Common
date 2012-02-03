/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.0
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