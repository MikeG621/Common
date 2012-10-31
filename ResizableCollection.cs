/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.2
 */

/* CHANGELOG
 * v1.2, 121024
 * [UPD] ItemLimit value of -1 for unlimited size
 * [UPD] null check in _add
 * v1.1, XXXXXX
 * [UPD] T[] converted to List<T>
 * [UPD] moved from Idmr.Platform
 * v1.0, XXXXXX
 * - Release
 */
 
using System;

namespace Idmr.Common
{
	/// <summary>Collection class for variable-sized arrays</summary>
	/// <typeparam name="T">Class type to be used in the collection</typeparam>
	public abstract class ResizableCollection<T> : FixedSizeCollection<T> where T : class
	{
		/// <summary>Maximum number of permitted elements</summary>
		/// <remarks>Defaults to <b>-1</b> (unlimited)</remarks>
		protected int _itemLimit = -1;

		/// <summary>Gets the maximum number of objects allowed in the Collection</summary>
		/// <remarks>A value of <b>-1</b> means unlimited</remarks>
		public int ItemLimit { get { return _itemLimit; } }

		/// <summary>Adds the given item to the end of the Collection</summary>
		/// <param name="item">The item to be added</param>
		/// <returns>The index of the added item if successfull, otherwise <b>-1</b></returns>
		public int Add(T item) { return _add(item); }
		
		/// <summary>Inserts the given item at the specified index</summary>
		/// <param name="index">Location of the item</param>
		/// <param name="item">The item to be added</param>
		/// <returns>The index of the added item if successfull, otherwise <b>-1</b></returns>
		public int Insert(int index, T item) { return _insert(index, item); }
		
		/// <summary>Adds <i>item</i> to the end of the collection</summary>
		/// <param name="item">The item to be added</param>
		/// <returns>Index of <i>item</i> if added<br/><b>-or-</b><br/><b>-1</b> if <see>Count</see> equals <see cref="ItemLimit"/></returns>
		/// <remarks>If the internal List has not been initialized, will initialize with a Capacity of <b>1</b></remarks>
		protected int _add(T item)
		{
			if (ItemLimit == -1 || Count < ItemLimit)
			{
				if (_items == null) _items = new System.Collections.Generic.List<T>(1);
				_items.Add(item);
				return (Count - 1);
			}
			else return -1;
		}
		/// <summary>Adds <i>item</i> to the specified location in the collection</summary>
		/// <param name="index">Location of the item</param>
		/// <param name="item">The item to be added</param>
		/// <returns>Index of <i>item</i> if added<br/><b>-or-</b><br/><b>-1</b> if <see cref="FixedSizeCollection{T}.Count"/> equals <see cref="ItemLimit"/> or invalid <i>index</i> value</returns>
		protected int _insert(int index, T item)
		{
			if ((ItemLimit == -1 || Count < ItemLimit) && index >= 0 && index <= Count)
			{
				_items.Insert(index, item);
				return index;
			}
			else return -1;
		}
		/// <summary>Removes the specified index</summary>
		/// <param name="index">The item index to remove</param>
		/// <returns>Index of the next item after deletion<br/><b>-or-</b><br/><b>-1</b> for invalid <i>index</i> value</returns>
		protected int _removeAt(int index)
		{
			if (index >= 0 && index < Count)
			{
				_items.RemoveAt(index);
				return (index == Count ? index - 1 : index);
			}
			else return -1;
		}
	}
}