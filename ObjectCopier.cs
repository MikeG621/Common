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
 * - Release
 */

using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Idmr.Common
{
	/// <summary>Class to provide a deep copy extension method.</summary>
	public static class ObjectCopier
	{
		/// <summary>Returns a deep copy of the object</summary>
		/// <typeparam name="T">The type of the object being cloned.</typeparam>
		/// <param name="original">The object to be cloned.</param>
		/// <exception cref="System.ArgumentException"><i>original</i> is not a serializable object.</exception>
		/// <returns>The cloned object.</returns>
		/// <remarks>This is an extension method that provides deep copy functionality to any object. Object must be serializable.</remarks>
		public static T DeepClone<T>(this T original) where T : class
		{
			if (!typeof(T).IsSerializable)
				throw new ArgumentException("original must be serializable.", "original");
			if (Object.ReferenceEquals(original, null)) return default(T);
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, original);
				stream.Position = 0;
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
