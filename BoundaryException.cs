/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in help/Idmr.Common.html
 * Version: 1.0
 */

/* CHANGE LOG
 * 110914 - added (string, string, Exception) overload
 * 110922 - added _defaultMessage(string, string)
 */

using System;

namespace Idmr.Common
{
    [Serializable]
	/// <summary>Exception typically used in Idmr code for 2-dimensional areas</summary>
	public class BoundaryException : Exception
	{
		static string _defaultMessage(string param, string limits) { return "Parameter '" + param + "' is not within acceptable limits (" + limits + ")"; }

        /// <summary>Creates a new exception</summary>
        public BoundaryException()
        {
        }
		/// <summary>Creates a new exception with a specified error message</summary>
		/// <param name="message">The specified error message</param>
		public BoundaryException(string message)
            : base(message)
		{
		}

        /// <summary>Creates a new exception with a standardized error message</summary>
        /// <param name="param">The parameter that caused the exception</param>
        /// <param name="limits">Acceptable range for <i>param</i></param>
        /// <remarks>Message is in the form "Parameter '<i>param</i>' is not within acceptable limits ('<i>limits</i>')"</remarks>
        public BoundaryException(string param, string limits)
            : base(_defaultMessage(param, limits))
        {
        }

        /// <summary>Creates a new exception with a specified error message and a reference to the initial exception</summary>
        /// <param name="message">The specified error message</param>
        /// <param name="innerEx">The initial Exception</param>
        public BoundaryException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
		
		/// <summary>Creates a new exception with a standardized error message</summary>
        /// <param name="param">The parameter that caused the exception</param>
        /// <param name="limits">Acceptable range for <i>param</i></i></param>
		/// <param name="innerEx">The initial Exception</param>
        /// <remarks>Message is in the form "Parameter '<i>param</i>' is not within acceptable limits ('<i>limits</i>')"</remarks>
        public BoundaryException(string param, string limits, Exception innerEx)
            : base(_defaultMessage(param, limits), innerEx)
        {
        }

        /// <summary>Creates a new exception using serialized data</summary>
        protected BoundaryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
	}
}