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