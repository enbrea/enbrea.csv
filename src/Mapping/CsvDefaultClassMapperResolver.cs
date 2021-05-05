#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections.Generic;

namespace Enbrea.Csv
{
    /// <summary>
    /// Default implementation of an <see cref="ICsvClassMapperResolver"/>
    /// </summary>
    public class CsvDefaultClassMapperResolver : ICsvClassMapperResolver
    {
        private readonly Dictionary<Type, ICsvClassMapper> _mappers = new Dictionary<Type, ICsvClassMapper>();

        public void AddMapper(Type type, ICsvClassMapper mapper)
        {
            _mappers[type] = mapper;
        }

        public ICsvClassMapper GetMapper(Type type)
        {
            ICsvClassMapper mapper;
            if (!_mappers.TryGetValue(type, out mapper))
            {
                mapper = new CsvDefaultClassMapper(type);
                _mappers.Add(type, mapper);
            }
            return mapper;
        }

        public void RemoveAllMappers()
        {
            _mappers.Clear();
        }

        public void RemoveMapper(Type type)
        {
            _mappers.Remove(type);
        }
    }
}