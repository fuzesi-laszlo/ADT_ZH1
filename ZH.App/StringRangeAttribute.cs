using System;
using System.Collections.Generic;

namespace ZH.App
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringRangeAttribute : Attribute
    {
        /// <summary>
        /// Returns a new instance of the <see cref="StringRangeAttribute"/> class.
        /// </summary>
        /// <param name="allowedStrings">The allowed string(s).</param>
        public StringRangeAttribute(params string[] allowedStrings)
        {
            this.WhiteList = new List<string>(allowedStrings);
        }

        /// <summary>
        /// Gets or Sets the list of allowed strings.
        /// </summary>
        public IList<string> WhiteList { get; set; }

    }
}