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
 * 120212 - T[] to List<T> conversion
 * *** v1.1 ***
 */
 
using System;

namespace Idmr.Common
{
	/// <summary>Collection class for variable-sized arrays</summary>
	/// <typeparam name="T">Class type to be used in the collection</typeparam>
	public abstract class ResizableCollection<T> : FixedSizeCollection<T> where T : class
	{
		/// <summary>Maximum number of permitted elements</summary>
		protected int _itemLimit;

		/// <summary>Gets the maximum number of objects allowed in the Collection</summary>
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
		/// <returns>Index of <i>item</i> if added<br/>-or-<br/><b>-1</b> if <see>Count</see> equals <see cref="ItemLimit"/></returns>
		protected int _add(T item)
		{
			if (Count < ItemLimit)
			{
				_items.Add(item);
				return (Count - 1);
			}
			else return -1;
		}
		/// <summary>Adds <i>item</i> to the specified location in the collection</summary>
		/// <param name="index">Location of the item</param>
		/// <param name="item">The item to be added</param>
		/// <returns>Index of <i>item</i> if added<br/>-or-<br/><b>-1</b> if <see>Count</see> equals <see cref="ItemLimit"/> or invalid <i>index</i> value</returns>
		protected int _insert(int index, T item)
		{
			if (Count < ItemLimit && index >= 0 && index <= Count)
			{
				_items.Insert(index, item);
				return index;
			}
			else return -1;
		}
		/// <summary>Removes the specified index</summary>
		/// <param name="index">The item index to remove</param>
		/// <returns>Index of the next item after deletion<br/>-or-<br/><b>-1</b> for invalid <i>index</i> value</returns>
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