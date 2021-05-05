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

namespace Enbrea.Csv
{
    /// <summary>
    /// Resolves entity types to their corresponding property mapper.
    /// </summary>
    public interface ICsvClassMapperResolver
    {
        public void AddMapper<TEntity>(ICsvClassMapper mapper) => AddMapper(typeof(TEntity), mapper);

        void AddMapper(Type type, ICsvClassMapper mapper);

        public ICsvClassMapper GetMapper<TEntity>() => GetMapper(typeof(TEntity));

        public ICsvClassMapper GetMapper(Type type);

        void RemoveAllMappers();
    }
}