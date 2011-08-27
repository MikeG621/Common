/*
 * Idmr.Common.BoundaryException.cs, Exception class typically for area boundaries
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
    [Serializable]
	/// <summary>Exception typically used in Idmr code for 2-dimensional areas</summary>
	public class BoundaryException : Exception
	{
        /// <summary>Create a new exception</summary>
        public BoundaryException()
        {
        }
		/// <summary>Create a new exception with a specified error message</summary>
		/// <param name="message">The specified error message</param>
		public BoundaryException(string message)
            : base(message)
		{
		}

        /// <summary>Create a new exception with a standardized error message</summary>
        /// <param name="param">The parameter that caused the exception</param>
        /// <param name="limits">Acceptable range for <i>param</i></i></param>
        /// <remarks>Message is in the form "Parameter 'param' is not within acceptable limits (limits)"</remarks>
        public BoundaryException(string param, string limits)
            : base("Parameter '" + param + "' is not within acceptable limits (" + limits + ")")
        {
        }

        /// <summary>Create a new exception with a specified error message and a reference to the initial exception</summary>
        /// <param name="message">The specified error message</param>
        /// <param name="innerEx">The initial exception</param>
        public BoundaryException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        /// <summary>Create a new exception using serialized data</summary>
        protected BoundaryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
	}
}