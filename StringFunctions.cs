/*
 * Idmr.Common.StringFunctions.cs, Class file for common strings functions
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
	public class StringFunctions
	{
		/// <summary>Returns the file name and extension given the full path</summary>
		/// <param name="filePath">The full path</param>
		public static string GetFileName(string filePath) { return GetFileName(filePath, true); }
		/// <summary>Returns the file name given the full path</summary>
		/// <param name="filePath">The full path</param>
		/// <param name="withExtension">Determines if the file extension is returned with the file name</param>
		public static string GetFileName(string filePath, bool withExtension)
		{
			string str = filePath.Substring(filePath.LastIndexOf('\\') + 1);
			if (!withExtension) str = str.Remove(str.LastIndexOf('.'));
			return str;
		}
		
		/// <summary>Returns the string trimmed to the specified length limit if required</summary>
		/// <param name="text">The original string</param>
		/// <param name="limit">The maximum allowed length</param>
		public static string GetTrimmed(string text, int limit) { return (text.Length > limit ? text.Substring(0, limit) : text).TrimEnd('\0'); }
	}
}