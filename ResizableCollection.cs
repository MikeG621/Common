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
	/// <summary>Collection class for variable-sized arrays</summary>
	public abstract class ResizableCollection<T> : FixedSizeCollection<T> where T : class
	{
		protected int _itemLimit;
		protected int _itemMinimum;

		/// <summary>Gets the maximum number of objects allowed in the Collection</summary>
		public int ItemLimit { get { return _itemLimit; } }

		/// <summary>Adds the given item to the end of the Collection</summary>
		/// <param name="item">The item to be added</param>
		/// <returns>The index of the added item if successfull, otherwise -1</returns>
		public int Add(T item) { return _add(item); }
		
		/// <summary>Inserts the given item at the specified index</summary>
		/// <param name="index">Location of the item</param>
		/// <param name="item">The item to be added</param>
		/// <returns>The index of the added item if successfull, otherwise -1</returns>
		public int Insert(int index, T item) { return _insert(index, item); }
		
		/// <summary>Adds <i>item</i> to the end of the collection</summary>
		/// <returns>Index of <i>item</i> if added, -1 if already at ItemLimit</returns>
		protected int _add(T item)
		{
			if (_count < _itemLimit)
			{
				T[] tempItems = _items;
				_items = new T[_count+1];
				for (int i=0;i<(_count);i++) _items[i] = tempItems[i];
				_items[_count] = item;
				_count++;
				return (short)(_count-1);
			}
			else return -1;
		}
		/// <summary>Adds <i>item</i> to the specified location in the collection</summary>
		/// <returns>Index of <i>item</i> if added, -1 if already at ItemLimit or invalid <i>index</i> value</returns>
		protected int _insert(int index, T item)
		{
			if (_count < ItemLimit && index >= 0 && index <= _count)
			{
				T[] tempItems = _items;
				_items = new T[_count+1];
				for (int i=0;i<index;i++) _items[i] = tempItems[i];
				_items[index] = item;
				for (int i=index;i<_count;i++) _items[i+1] = tempItems[i];
				_count++;
				return index;
			}
			else return -1;
		}
		/// <summary>Removes the specified index. Returns the next valid <i>index</i> value</summary>
		protected int _removeAt(int index)
		{
			_count--;
			T[] tempItems = _items;
			_items = new T[_count];
			for (int i=0;i<index;i++) _items[i] = tempItems[i];
			for (int i=index;i<_count;i++) _items[i] = tempItems[i+1];
			return (index == _count ? index-1 : index);
		}
	}
}