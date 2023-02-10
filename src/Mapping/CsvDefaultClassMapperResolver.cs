#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Enbrea.Csv
{
    /// <summary>
    /// Default implementation of an <see cref="ICsvClassMapperResolver"/>
    /// </summary>
    public class CsvDefaultClassMapperResolver : ICsvClassMapperResolver
    {
        private readonly ConcurrentDictionary<Type, ICsvClassMapper> _mappers = new ConcurrentDictionary<Type, ICsvClassMapper>();

        /// <summary>
        /// Registers a class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="mapper">The class mapper</param>
        public void AddMapper(Type type, ICsvClassMapper mapper)
        {
            _mappers[type] = mapper;
        }

        /// <summary>
        /// Gives back the registered class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The registered class mapper</returns>
        public ICsvClassMapper GetMapper(Type type)
        {
            ICsvClassMapper mapper;
            if (!_mappers.TryGetValue(type, out mapper))
            {
                mapper = new CsvDefaultClassMapper(type);
                _mappers.TryAdd(type, mapper);
            }
            return mapper;
        }

        /// <summary>
        /// Removes all registered class mappers
        /// </summary>
        public void RemoveAllMappers()
        {
            _mappers.Clear();
        }

        /// <summary>
        /// Removes the class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        public void RemoveMapper(Type type)
        {
            _mappers.TryRemove(type, out _);
        }
    }
}