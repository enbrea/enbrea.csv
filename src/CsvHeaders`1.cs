#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Linq.Expressions;

namespace Enbrea.Csv
{
    /// <summary>
    /// Encapsulates strongly typed csv header information. Used for table based csv access.
    /// </summary>
    public class CsvHeaders<TEntity> : CsvHeaders
    {
        private readonly CsvHeadersBuilder<TEntity> _csvHeadersBuilder = new CsvHeadersBuilder<TEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaders{TEntity}"/> class.
        /// </summary>
        public CsvHeaders()
            : base()
        {
            typeof(TEntity).VisitMembers((name, memberInfo, isAlternativeName) => { if (!isAlternativeName) Add(name); });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaders{TEntity}"/> class.
        /// </summary>
        /// <param name="expression">List of csv headers as expression lambda</param>
        public CsvHeaders(Expression<Func<TEntity, object>> expression)
            : base()
        {
            Add(expression);
        }

        /// <summary>
        /// Adds new headers to the list of headers
        /// </summary>
        /// <param name="expression">List of csv headers as expression lambda</param>
        internal void Add(Expression<Func<TEntity, object>> expression)
        {
            _csvHeadersBuilder.Append(this, expression.Body);
        }
    }
}
