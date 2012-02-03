/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.0
 */
 
/* CHANGELOG
 * prev - made generic
 * 300112 - moved from Idmr.Platform
 */

using System;

namespace Idmr.Common
{
	/// <summary>Collection class for fixed-size arrays</summary>
	public abstract class FixedSizeCollection<T> where T : class
	{
		/// <summary>The collection contents</summary>
		protected T[] _items;
		protected int _count = 0;

		/// <summary>Gets or sets a single item within the Collection</summary>
		public T this[int index]
		{
			get { return _getItem(index); }
			set { _setItem(index, value); }
		}

		/// <summary>Gets the number of objects in the collection</summary>
		/// <remarks>May not necessarily be the size of the internal array</remarks>
		public int Count { get { return _count; } }

		/// <summary>Gets the item at the specified index</summary>
		/// <remarks>Returns <i>null</i> for invalid <i>index</i> values</remarks>
		protected T _getItem(int index)
		{
			if (index >= 0 && index < _count) return _items[index];
			else return null;
		}
		/// <summary>Sets the items at the specified index</summary>
		/// <remarks>No effect for invalid <i>index</i> values</remarks>
		protected void _setItem(int index, T item)
		{
			if (index >= 0 && index < _items.Length) _items[index] = item;
		}
	}
}