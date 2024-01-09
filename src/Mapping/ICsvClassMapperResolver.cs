#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
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
        /// <summary>
        /// Registers a class mapper for a specifc type
        /// </summary>
        /// <typeparam name="TEntity">The type</typeparam>
        /// <param name="mapper">The class mapper</param>
        public void AddMapper<TEntity>(ICsvClassMapper mapper) => AddMapper(typeof(TEntity), mapper);

        /// <summary>
        /// Registers a class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="mapper">The class mapper</param>
        void AddMapper(Type type, ICsvClassMapper mapper);

        /// <summary>
        /// Gives back the registered class mapper for a specifc type
        /// </summary>
        /// <typeparam name="TEntity">The type</typeparam>
        /// <returns>The registered class mapper</returns>
        public ICsvClassMapper GetMapper<TEntity>() => GetMapper(typeof(TEntity));

        /// <summary>
        /// Gives back the registered class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The registered class mapper</returns>
        public ICsvClassMapper GetMapper(Type type);

        /// <summary>
        /// Removes all registered class mappers
        /// </summary>
        void RemoveAllMappers();

        /// <summary>
        /// Removes the class mapper for a specifc type
        /// </summary>
        /// <param name="type">The type</param>
        void RemoveMapper(Type type);
    }
}