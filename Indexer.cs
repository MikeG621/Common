/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.1
 */

/* CHANGELOG
 * *** v1.1 ***
 */

using System;

namespace Idmr.Common
{
	/// <summary>Generic class primarily to provide basic Indexer capabilities</summary>
	/// <typeparam name="T">The type used to establish the array</typeparam>
	[Serializable] public class Indexer<T>
	{
		int _stringMaxLength;
		
		static string _lengthError = "Array lengths must match";
		static string _typeError = "T must be of type string for lengthLimit to apply";
		
		/// <summary>The content array</summary>
		protected T[] _items;
		/// <summary>The Read-Only flags</summary>
		/// <remarks>Size must equal <see cref="Length"/>, causes <see>Item</see>.set to throw when a given index is <b>true</b></remarks>
		protected bool[] _readOnly = null;

		#region constructors
		/// <summary>Default constructor for derived classes</summary>
		protected Indexer() { }

		/// <summary>Initialize with an existing array</summary>
		/// <param name="items">The array of type T to initialize with</param>
		public Indexer(T[] items) { _items = items; }
		
		/// <summary>Initialize with an initial size</summary>
		/// <remarks>If T is of type <i>string</i>, all items will be set to ""</remarks>
		/// <param name="quantity">The number of elements</param>
		public Indexer(int quantity)
		{
			_items = new T[quantity];
			if (typeof(T) == typeof(string)) for (int i = 0; i < quantity; i++) _items[i] = (T)(object)"";
		}

		/// <summary>Initialize with an existing string array with the specified length limit</summary>
		/// <param name="items">The string array</param>
		/// <param name="lengthLimit">Maximum length for each string. Can only be used if T is of type <i>string</i></param>
		/// <exception cref="InvalidOperationException">T is not of type <i>string</i></exception>
		public Indexer(T[] items, int lengthLimit)
		{
			if (typeof(T) != typeof(string)) throw new InvalidOperationException(_typeError);
			_items = items;
			_stringMaxLength = lengthLimit;
		}
		
		/// <summary>Initialize with an existing string array with the specified length limit and Read/Write controls</summary>
		/// <param name="items">The string array</param>
		/// <param name="lengthLimit">Maximum length for each string. Can only be used if T is of type <i>string</i></param>
		/// <param name="readOnly">The array of read-only flags to prevent editing after intialization. Length must equal items.Length</param>
		/// <exception cref="ArgumentException"><i>items</i> and <i>readOnly</i> do not have the same length</exception>
		/// <exception cref="InvalidOperationException">T is not of type <i>string</i></exception>
		public Indexer(T[] items, int lengthLimit, bool[] readOnly)
		{
			if (items.Length != readOnly.Length) throw new ArgumentException(_lengthError);
			if (typeof(T) != typeof(string)) throw new InvalidOperationException(_typeError);
			_items = items;
			_stringMaxLength = lengthLimit;
			_readOnly = readOnly;
		}
		
		/// <summary>Initialize with an existing array with Read/Write controls</summary>
		/// <param name="items">The array of type T to initialize with</param>
		/// <param name="readOnly">The array of read-only flags to prevent editing after intialization. Length must equal items.Length</param>
		/// <exception cref="ArgumentException"><i>items</i> and <i>readOnly</i> do not have the same length</exception>
		public Indexer(T[] items, bool[] readOnly)
		{
			if (items.Length != readOnly.Length) throw new ArgumentException(_lengthError);
			_items = items;
			_readOnly = readOnly;
		}
		#endregion

		/// <summary>Gets the size of the array</summary>
		public int Length { get { return _items.Length; } }

		/// <summary>Gets or sets a single value in the array</summary>
		/// <remarks>Cannot set if <see cref="_readOnly"/>[<i>index</i>] is <b>true</b>. If <i>lengthLimit</i> was used, string may be truncated</remarks>
		/// <param name="index">Item index</param>
		/// <exception cref="IndexOutOfRangeException">Invalid <i>index</i> value</exception>
		/// <exception cref="InvalidOperationException">Item is read-only</exception>
		public virtual T this[int index]
		{
			get { return _items[index]; }
			set
			{
				if (_readOnly != null && _readOnly[index]) throw new InvalidOperationException("Cannot set index " + index + ", value is Read Only");
				if (_stringMaxLength != 0) _items[index] = (T)(object)StringFunctions.GetTrimmed(value.ToString(), _stringMaxLength);
				else _items[index] = value;
			}
		}
	}
}
