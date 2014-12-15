/*
 * Idmr.Common.dll, Library file with common IDMR resources
 * Copyright (C) 2007-2014 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the MPL v2.0 or later
 * 
 * Full notice in help/Idmr.Common.chm
 * Version: 1.3
 */

/* CHANGE LOG
 * v1.3, 141214
 * [UPD] switch to MPL
 * v1.1, XXXXXX
 * [NEW] ctr(string, string, Exception)
 * [NEW] _defaultMessage(string, string)
 * [NEW] Serialization use
 * v1.0, XXXXXX
 * - Release
 */

using System;
using System.Runtime.Serialization;

namespace Idmr.Common
{
	/// <summary>Exception typically used in IDMR code for 2-dimensional areas.</summary>
    [Serializable] public class BoundaryException : Exception
	{
		static string _defaultMessage(string param, string limits) { return "Parameter '" + param + "' is not within acceptable limits (" + limits + ")"; }

        /// <summary>Creates a new exception.</summary>
        public BoundaryException()
        {
        }
		/// <summary>Creates a new exception with a specified error message.</summary>
		/// <param name="message">The specified error message.</param>
		public BoundaryException(string message)
            : base(message)
		{
		}

        /// <summary>Creates a new exception with a standardized error message.</summary>
        /// <param name="param">The parameter that caused the exception.</param>
        /// <param name="limits">Acceptable range for <i>param</i>.</param>
        /// <remarks>Message is in the form <b>"Parameter '<i>param</i>' is not within acceptable limits ('<i>limits</i>')"</b>.</remarks>
        public BoundaryException(string param, string limits)
            : base(_defaultMessage(param, limits))
        {
        }

        /// <summary>Creates a new exception with a specified error message and a reference to the initial exception.</summary>
        /// <param name="message">The specified error message.</param>
        /// <param name="innerEx">The initial Exception.</param>
        public BoundaryException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
		
		/// <summary>Creates a new exception with a standardized error message.</summary>
        /// <param name="param">The parameter that caused the exception.</param>
        /// <param name="limits">Acceptable range for <i>param</i>.</param>
		/// <param name="innerEx">The initial Exception.</param>
        /// <remarks>Message is in the form <b>"Parameter '<i>param</i>' is not within acceptable limits ('<i>limits</i>')"</b>.</remarks>
        public BoundaryException(string param, string limits, Exception innerEx)
            : base(_defaultMessage(param, limits), innerEx)
        {
        }

        /// <summary>Creates a new exception using serialized data.</summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected BoundaryException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
	}
}