/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.2
 */

/* CHANGELOG
 * v1.3, XXXXXX
 * [NEW] IsModified, _isModified, _isLoading, Tag, _tag
 * v1.2, 121024
 * [UPD] null check in Count
 * [FIX] _setItem correctly uses Count instead of Capacity
 * v1.1, XXXXXX
 * [UPD] moved from Idmr.Platform
 * [UPD] T[] converted to List<T>
 * [NEW] IEnumerable<T> implementation
 * v1.0, XXXXXX
 * - Release
 */

using System;
using System.Collections.Generic;

namespace Idmr.Common
{
	/// <summary>Collection class for fixed-size arrays</summary>
	/// <typeparam name="T">Class type to be used in the collection</typeparam>
	public abstract class FixedSizeCollection<T> : IEnumerable<T> where T : class
	{
		/// <summary>Flag indicating that changed have been made since initialization</summary>
		protected bool _isModified;
		/// <summary>Flag indicating that the initialization process is active, preventing changes to <see cref="_isModified"/></summary>
		protected bool _isLoading;
		/// <summary>The collection contents</summary>
		protected List<T> _items;
		/// <summary>User-specified content</summary>
		protected object _tag;
		
		/// <summary>Gets or sets user-specified content</summary>
		public virtual object Tag
		{
			get { return _tag; }
			set
			{
				_tag = value;
				if (!_isLoading) _isModified = true;
			}
		}

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
		
		/// <summary>Gets whether or not changes have been made to the Collection since intialization</summary>
		public virtual bool IsModified { get { return _isModified; } }

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
			if (!_isLoading) _isModified = true;
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