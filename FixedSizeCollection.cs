/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.1
 */

/* CHANGELOG
 * prev - made generic
 * 300112 - moved from Idmr.Platform
 * 120212 - T[] to List<T> conversion, implemented IEnumerable<T>
 * *** v1.1 ***
 * 120405 - null check in Count
 * 120510 - _setItem correctly used Count instead of Capacity
 */

using System;
using System.Collections.Generic;

namespace Idmr.Common
{
	/// <summary>Collection class for fixed-size arrays</summary>
	/// <typeparam name="T">Class type to be used in the collection</typeparam>
	public abstract class FixedSizeCollection<T> : IEnumerable<T> where T : class
	{
		/// <summary>The collection contents</summary>
		protected List<T> _items;

		/// <summary>Gets or sets a single item within the Collection</summary>
		/// <param name="index">The item location within the collection</param>
		/// <returns>A single item within the collection<br/>-or-<br/><b>null</b> for invalid values of <i>index</i></returns>
		/// <remarks>No action is taken when attempting to set with invalid values of <i>index</i>.</remarks>
		public T this[int index]
		{
			get { return _getItem(index); }
			set { _setItem(index, value); }
		}

		/// <summary>Gets the number of objects in the collection</summary>
		/// <remarks>If internal List is <b>null</b>, returns <b>-1</b></remarks>
		public int Count { get { return (_items == null ? -1 : _items.Count); } }

		/// <summary>Gets the item at the specified index</summary>
		/// <param name="index">The item location within the collection</param>
		/// <returns>A single item within the collection<br/>-or-<br/><b>null</b> for invalid values of <i>index</i></returns>
		protected T _getItem(int index)
		{
			if (index >= 0 && index < Count) return _items[index];
			else return null;
		}
		/// <summary>Sets the items at the specified index</summary>
		/// <remarks>No effect for invalid <i>index</i> values</remarks>
		/// <param name="index">The item location within the collection</param>
		/// <param name="item">The new item</param>
		protected void _setItem(int index, T item)
		{
			if (index >= 0 && index < Count) _items[index] = item;
		}

		#region IEnumerable<T> Members
		/// <summary>Returns an enumerator that iterations through the collection</summary>
		/// <returns>The enumerator</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return _items.GetEnumerator();
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
		#endregion
	}
}