/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2014 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the MPL v2.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.3
 */

/* CHANGELOG
 * v1.3, 141214
 * [UPD] switch to MPL
 * v1.1, XXXXXX
 * [NEW] MatchesWildcard
 * [NEW] GetTrimmed(string)
 * [UPD] class is static
 * v1.0, XXXXXX
 * - Release
 */

using System;
using System.Text.RegularExpressions;

namespace Idmr.Common
{
	/// <summary>String handling functions.</summary>
	public static class StringFunctions
	{
		#region GetFileName
		/// <summary>Extracts the file name and extension from a path.</summary>
		/// <param name="filePath">The full path.</param>
		/// <returns>The file name with extension.</returns>
		public static string GetFileName(string filePath) { return GetFileName(filePath, true); }
		
		/// <summary>Extracts the file name from a path.</summary>
		/// <param name="filePath">The full path.</param>
		/// <param name="withExtension">Determines if the file extension is returned with the file name</param>
		/// <returns>The file name.</returns>
		public static string GetFileName(string filePath, bool withExtension)
		{
			string str = filePath.Substring(filePath.LastIndexOf('\\') + 1);
			if (!withExtension) str = str.Remove(str.LastIndexOf('.'));
			return str;
		}
		#endregion GetFileName
		
		#region GetTrimmed
		/// <summary>Truncates a string to a given length and removes trailing null chars ('\0').</summary>
		/// <param name="text">The original string.</param>
		/// <param name="limit">The maximum allowed length.</param>
		/// <returns><i>text</i> truncated to <i>limit</i> characters if <i>limit</i> is less than <i>text</i>.Length with trailing null characters removed.</returns>
		public static string GetTrimmed(string text, int limit) { return GetTrimmed(text.Length > limit ? text.Substring(0, limit) : text); }
		
		/// <summary>Removes trailing null chars ('\0') from the string.</summary>
		/// <param name="text">The original string.</param>
		/// <returns><i>text</i> without trailing null chars.</returns>
		public static string GetTrimmed(string text) { return text.TrimEnd('\0'); }
		#endregion GetTrimmed
		
		#region MatchesWildcard
		/// <summary>Indicates whether the specified text matches the specified wildcard string.</summary>
		/// <param name="text">The full text to search.</param>
		/// <param name="wildcard">The wildcard string using "*" and "?".</param>
		/// <exception cref="ArgumentNullException"><i>text</i> or <i>wildcard</i> are <b>null</b>.</exception>
		/// <returns><b>true</b> if the strings match.</returns>
		/// <remarks>Case-insensitive.</remarks>
		public static bool MatchesWildcard(string text, string wildcard) { return MatchesWildcard(text, wildcard, false); }
		
		/// <summary>Indicates whether the specified text matches the specified wildcard string.</summary>
		/// <param name="text">The full text to search.</param>
		/// <param name="wildcard">The wildcard string using "*" and "?".</param>
		/// <param name="caseSensitive">Controls if <i>text</i> and <i>wildcard</i> are both treated as case-sensitive.</param>
		/// <exception cref="ArgumentNullException"><i>text</i> or <i>wildcard</i> are <b>null</b>.</exception>
		/// <returns><b>true</b> if the strings match.</returns>
		public static bool MatchesWildcard(string text, string wildcard, bool caseSensitive)
		{
			string regex = "^" + Regex.Escape(wildcard).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			if (!caseSensitive)
			{
				regex = regex.ToLower();
				text = text.ToLower();
			}
			return Regex.IsMatch(text, regex);
		}
		#endregion MatchesWildcard
	}
}