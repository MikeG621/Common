/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.1
 */

/* CHANGELOG
 * 260212 - class is static, added GetTrimmed(string)
 * *** v1.1 ***
 */

using System;

namespace Idmr.Common
{
	/// <summary>String handling functions</summary>
	public static class StringFunctions
	{
		/// <summary>Extracts the file name and extension from a path</summary>
		/// <param name="filePath">The full path</param>
		/// <returns>The file name with extension</returns>
		public static string GetFileName(string filePath) { return GetFileName(filePath, true); }
		/// <summary>Extracts the file name from a path</summary>
		/// <param name="filePath">The full path</param>
		/// <param name="withExtension">Determines if the file extension is returned with the file name</param>
		/// <returns>The file name</returns>
		public static string GetFileName(string filePath, bool withExtension)
		{
			string str = filePath.Substring(filePath.LastIndexOf('\\') + 1);
			if (!withExtension) str = str.Remove(str.LastIndexOf('.'));
			return str;
		}
		
		/// <summary>Truncates a string to a given length and removes trailing null chars ('\0')</summary>
		/// <param name="text">The original string</param>
		/// <param name="limit">The maximum allowed length</param>
		/// <returns><i>text</i> truncated to <i>limit</i> characters if <i>limit</i> is less than <i>text</i>.Length with trailing null characters removed</returns>
		public static string GetTrimmed(string text, int limit) { return GetTrimmed(text.Length > limit ? text.Substring(0, limit) : text); }
		/// <summary>Removes trailing null chars ('\0') from the string</summary>
		/// <param name="text">The original string</param>
		/// <returns><i>text</i> without trailing null chars</returns>
		public static string GetTrimmed(string text) { return text.TrimEnd('\0'); }
	}
}