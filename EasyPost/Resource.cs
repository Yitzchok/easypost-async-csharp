/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyPost
{
    public class Resource : IResource
    {
        /// <summary>
        /// Returns the resource as a dictionary of JSON style named parameters
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> AsDictionary()
        {
            return GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(info => ToJsonStyle(info.Name), GetValue);
        }

        /// <summary>
        /// Converts a name from the usual C# style CamelCase to the JSON style camel_case
        /// </summary>
        /// <param name="name">Name to convert</param>
        /// <returns>Converted name</returns>
        private static string ToJsonStyle(
            string name)
        {
            var sb = new StringBuilder();
            var wasLower = false;
            foreach (var c in name) {
                if (char.IsUpper(c)) {
                    if (wasLower) {
                        sb.Append('_');
                    }
                    wasLower = false;
                } else {
                    wasLower = true;
                }
                sb.Append(char.ToLowerInvariant(c));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the value for the property. If the property is a reference to another resources we 
        /// recurse into that resource and dump it as well as a dictionary.
        /// </summary>
        /// <param name="info">Property info for the property</param>
        /// <returns>Value for the property</returns>
        private object GetValue(
            PropertyInfo info)
        {
            var value = info.GetValue(this, null);

            if (value is IResource) {
                return ((IResource)value).AsDictionary();
            } else if (value is IEnumerable<IResource>) {
                return ((IEnumerable<IResource>)value).Select(resource => resource.AsDictionary()).ToList();
            } else {
                return value;
            }
        }
    }
}
