#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Enbrea.Csv
{
    /// <summary>
    /// Encapsulates csv header information. Used for table based csv access.
    /// </summary>
    public class CsvHeaders : IEnumerable<string>
    {
        private readonly IList<string> _names;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaders"/> class.
        /// </summary>
        /// <param name="names">List of header names</param>
        public CsvHeaders(IList<string> names)
        {
            _names = names;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaders"/> class.
        /// </summary>
        public CsvHeaders() : this(new List<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaders"/> class.
        /// </summary>
        /// <param name="names">List of header names</param>
        public CsvHeaders(params string[] names) : this(names.ToList())
        {
        }

        /// <summary>
        /// Number of headers 
        /// </summary>
        public int Count => _names.Count;

        /// <summary>
        /// Gets the name of the header at the specified index.
        /// </summary>
        /// <param name="i">Index of the header</param>
        /// <returns>A string value</returns>
        public string this[int i]
        {
            get
            {
                return _names[i];
            }
        }

        /// <summary>
        /// Returns a value indicating whether a header name matches the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions to search for.</param>
        /// <returns>true if header is found; otherwise, false.</returns>
        public bool Contains(Predicate<string> match) 
        {
            return IndexOf(match) != -1; 
        }

        /// <summary>
        /// Support for iteration over a string collection
        /// </summary>
        /// <returns></returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _names.GetEnumerator();
        }

        /// <summary>
        /// Support for iteration over a non-generic collection.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _names.GetEnumerator();
        }

        /// <summary>
        /// Returns the index of the header that matches the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions to search for.</param>
        /// <returns>The zero-based index of header, if found; otherwise, –1.</returns>
        public int IndexOf(Predicate<string> match)
        {
            for (int i = 0; i < _names.Count; i++)
            {
                if (match(_names[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the header that matches the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions to search for.</param>
        /// <param name="index">The zero-based index of header, if found; otherwise, –1.</param>
        /// <returns>True, if found; otherwise, false</returns>
        public bool TryIndexOf(Predicate<string> match, out int index)
        {
            for (int i = 0; i < _names.Count; i++)
            {
                if (match(_names[i]))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        /// <summary>
        /// Adds a new header to the list of headers
        /// </summary>
        /// <param name="name">A header name</param>
        internal void Add(string name)
        {
            _names.Add(name);
        }

        /// <summary>
        /// Removes all headers from the internal list of headers
        /// </summary>
        internal void Clear()
        {
            _names.Clear();
        }

        /// <summary>
        /// Replaces all headers in the internal list of headers with a set of new ones
        /// </summary>
        /// <param name="names">List of header name</param>
        internal void Replace(IEnumerable<string> names)
        {
            _names.Clear();
            foreach (var name in names)
            {
                _names.Add(name);
            }
        }
    }
}
