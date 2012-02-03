/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.0
 */

/* CHANGE LOG
 * 110922 - added _defaultMessage and (Exception) overload
 */

using System;

namespace Idmr.Common
{
    [Serializable]
	/// <summary>Exception for file save failures, typically uses innerException</summary>
	public class SaveFileException : Exception
	{
		static string _defaultMessage = "File could not be saved";
		
        /// <summary>Create a new exception with a default error message</summary>
		/// <remarks>Default message is "File could not be saved"</remarks>
        public SaveFileException()
			: base(_defaultMessage)
        {
        }
		/// <summary>Create a new exception with a specified error message</summary>
		/// <param name="message">The specified error message</param>
        public SaveFileException(string message)
            : base(message)
		{
		}
        /// <summary>Create a new exception with a specified error message and a reference to the initial exception</summary>
        /// <param name="message">The specified error message</param>
        /// <param name="innerEx">The initial exception</param>
		public SaveFileException(string message, Exception innerEx)
            : base(message, innerEx)
		{
		}
		/// <summary>Create a new exception with a default error message and a reference to the initial exception</summary>
		/// <param name="innerEx">The initial exception</param>
		/// <remarks>Default message is "File could not be saved"</remarks>
		public SaveFileException(Exception innerEx)
			: base(_defaultMessage + ": " + innerEx.Message, innerEx)
		{
		}

        /// <summary>Create a new exception using serialized data</summary>
        protected SaveFileException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
	}
}