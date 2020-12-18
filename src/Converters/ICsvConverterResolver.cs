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
        void AddConverter<T>(ICsvConverter valueConverter);

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The converter instance</returns>
        ICsvConverter GetConverter<T>();

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The converter instance</returns>
        ICsvConverter GetConverter(Type type);

        /// <summary>
        /// Unregisters the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        void RemoveConverter<T>();
    }
}