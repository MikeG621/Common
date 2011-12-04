/*
 * Idmr.Common.LoadFileException.cs, Exception class for load failures
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

/* CHANGE LOG
 * 110922 - added _defaultMessage and (Exception) overload
 */

using System;

namespace Idmr.Common
{
	[Serializable]
	/// <summary>Exception for file load failures, typically uses innerException</summary>
	public class LoadFileException : Exception
	{
		static string _defaultMessage = "File could not be loaded";
		
		/// <summary>Create a new exception with a default error message</summary>
		/// <remarks>Default message is "File could not be loaded"</remarks>
        public LoadFileException()
			: base(_defaultMessage)
        {
        }
		/// <summary>Create a new exception with a specified error message</summary>
		/// <param name="message">The specified error message</param>
        public LoadFileException(string message)
            : base(message)
		{
		}
        /// <summary>Create a new exception with a specified error message and a reference to the initial exception</summary>
        /// <param name="message">The specified error message</param>
        /// <param name="innerEx">The initial exception</param>
		public LoadFileException(string message, Exception innerEx)
            : base(message, innerEx)
		{
		}
		/// <summary>Create a new exception with a default error message and a reference to the initial exception</summary>
		/// <param name="innerEx">The initial exception</param>
		/// <remarks>Default message is "File could not be loaded"</remarks>
		public LoadFileException(Exception innerEx)
			: base(_defaultMessage + ": " + innerEx.Message, innerEx)
		{
		}

        /// <summary>Create a new exception using serialized data</summary>
        protected LoadFileException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
	}
}