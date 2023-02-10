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

namespace Enbrea.Csv
{
    /// <summary>
    /// Resolves types to their corresponding value converters.
    /// </summary>
    public interface ICsvConverterResolver
    {
        /// <summary>
        /// Registers a new value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="valueConverter">The converter instance</param>
        public void AddConverter<T>(ICsvConverter converter) => AddConverter(typeof(T), converter);

        /// <summary>
        /// Registers a new value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="converter">The converter instance</param>
        void AddConverter(Type type, ICsvConverter converter);

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The converter instance</returns>
        public ICsvConverter GetConverter<T>() => GetConverter(typeof(T));

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The converter instance</returns>
        ICsvConverter GetConverter(Type type);

        /// <summary>
        /// Unregisters all value converters
        /// </summary>
        void RemoveAllConverters();

        /// <summary>
        /// Unregisters the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        public void RemoveConverter<T>() => RemoveConverter(typeof(T));

        /// <summary>
        /// Unregisters the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        void RemoveConverter(Type type);
    }
}