/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.1
 */

/* CHANGELOG
 * 120210 - added WriteToArray(short[], Array, int)
 * 120220 - all TrimArray.fullArray changed to type Array
 * 120225 - all WriteToArray.array changed to type Array
 * 120226 - class is static
 * *** v1.1 ***
 * 120411 - added WriteToArray(short[], Array, ref int)
 */
using System;

namespace Idmr.Common
{
	/// <summary>Primarily a wrapper for Buffer.BlockCopy</summary>
	public static class ArrayFunctions
	{
		/// <summary>Retrieves a string from a byte array</summary>
		/// <param name="array">The raw byte array</param>
		/// <param name="offset">The location of the starting character</param>
		/// <param name="length">The number of characters to read</param>
		/// <remarks>String is assumed to be ASCII-encoded, trims null chars.</remarks>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> or <i>length</i> are less than zero<br/><b>-or-</b><br/><i>offset</i> or <i>length</i> result in a range outside the range of <i>array</i></exception>
		/// <returns>An ASCII-encoded string</returns>
		public static string ReadStringFromArray(byte[] array, int offset, int length) { return System.Text.Encoding.ASCII.GetString(array, offset, length).Trim('\0'); }

		#region TrimArray
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in <i>fullArray</i></param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		/// <exception cref="ArgumentException"><i>fullArray</i> is not an array of primitives<br/><b>-or-</b><br/>The length of <i>fullArray</i> is less than <i>offset</i> plus the length of <i>trimmedArray</i></exception>
		/// <exception cref="ArgumentNullException"><i>fullArray</i> or <i>trimmedArray</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void TrimArray(Array fullArray, int offset, byte[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length);
		}
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in fullArray</param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		/// <exception cref="ArgumentException"><i>fullArray</i> is not an array of primitives<br/><b>-or-</b><br/>The length of <i>fullArray</i> is less than <i>offset</i> plus the length of <i>trimmedArray</i></exception>
		/// <exception cref="ArgumentNullException"><i>fullArray</i> or <i>trimmedArray</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void TrimArray(Array fullArray, int offset, short[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length << 1);
		}
		/// <summary>Using <i>fullArray</i> starting at <i>offset</i>, fill <i>trimmedArray</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="fullArray">The original array</param>
		/// <param name="offset">The starting location in fullArray</param>
		/// <param name="trimmedArray">The array to fill with copied values</param>
		/// <exception cref="ArgumentException"><i>fullArray</i> is not an array of primitives<br/><b>-or-</b><br/>The length of <i>fullArray</i> is less than <i>offset</i> plus the length of <i>trimmedArray</i></exception>
		/// <exception cref="ArgumentNullException"><i>fullArray</i> or <i>trimmedArray</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void TrimArray(Array fullArray, int offset, int[] trimmedArray)
		{
			Buffer.BlockCopy(fullArray, offset, trimmedArray, 0, trimmedArray.Length << 2);
		}
		#endregion

		#region WriteToArray ref
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented 4</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus four (4)<br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(int value, Array array, ref int offset)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 4);
			offset += 4;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented 2</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus two (2)<br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(short value, Array array, ref int offset)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 2);
			offset += 2;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented the length of the string</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus the length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(string value, Array array, ref int offset)
		{
			Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(value), 0, array, offset, value.Length);
			offset += value.Length;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented the length of <i>value</i></param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus the length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> or <i>value</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(byte[] value, Array array, ref int offset)
		{
			Buffer.BlockCopy(value, 0, array, offset, value.Length);
			offset += value.Length;
		}
		/// <summary>Copy <i>value</i> to the array at <i>offset</i> and increment</summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array, will be incremented the byte length of <i>value</i></param>
		/// <exception cref="ArgumentException">The byte length of <i>array</i> is less than <i>offset</i> plus the byte length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> or <i>value</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(short[] value, Array array, ref int offset)
		{
			Buffer.BlockCopy(value, 0, array, offset, value.Length << 1);
			offset += value.Length << 1;
		}
		#endregion
		
		#region WriteToArray
		/// <summary>Copy <i>value</i> to the array at <i>offset</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus four (4)<br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(int value, Array array, int offset) { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 4); }
		/// <summary>Copy <i>value</i> to the array at <i>offset</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus two (2)<br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(short value, Array array, int offset) { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, offset, 2); }
		/// <summary>Copy <i>value</i> to the array at <i>offset</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus the length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> is <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(string value, Array array, int offset) { Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(value), 0, array, offset, value.Length); }
		/// <summary>Copy <i>value</i> to the array at <i>offset</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		/// <exception cref="ArgumentException">The length of <i>array</i> is less than <i>offset</i> plus the length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> or <i>value</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(byte[] value, Array array, int offset) { Buffer.BlockCopy(value, 0, array, offset, value.Length); }
		/// <summary>Copy <i>value</i> to the array at <i>offset</i></summary>
		/// <remarks>Is a simple wrapper for <see cref="Buffer.BlockCopy"/></remarks>
		/// <param name="value">The value to be copied to the array, will copy entire array</param>
		/// <param name="array">The array to which <i>value</i> is to be copied</param>
		/// <param name="offset">The offset of the starting byte in the array</param>
		/// <exception cref="ArgumentException">The byte length of <i>array</i> is less than <i>offset</i> plus the byte length of <i>value</i><br/><b>-or-</b><br/><i>array</i> is not an array of primitives</exception>
		/// <exception cref="ArgumentNullException"><i>array</i> or <i>value</i> are <b>null</b></exception>
		/// <exception cref="ArgumentOutOfRangeException"><i>offset</i> is less than zero</exception>
		public static void WriteToArray(short[] value, Array array, int offset) { Buffer.BlockCopy(value, 0, array, offset, value.Length << 1); }
		#endregion
	}
}