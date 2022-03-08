#region ENBREA.CSV - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV dictionary is a CSV file/stream with 2 columns. Each record represents a key/value pair.
    /// </summary>
    public class CsvDictionary : CsvAccess, IEnumerable<KeyValuePair<string, string>>
    {
        private readonly List<KeyValuePair<string, string>> _keyValuePairs = new List<KeyValuePair<string, string>>(); 

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDictionary"/> class.
        /// </summary>
        public CsvDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDictionary"/> class.
        /// </summary>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvDictionary(ICsvConverterResolver csvConverterResolver)
            : base(csvConverterResolver)
        {
        }

        /// <summary>
        /// Number of key/value pairs 
        /// </summary>
        public int Count => _keyValuePairs.Count;

        /// <summary>
        /// Gets or sets the value of the specified key.
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>A string value</returns>
        public string this[string key]
        {
            get
            {
                var i = _keyValuePairs.FindIndex(x => x.Key == key);
                if (i != -1)
                {
                    return _keyValuePairs[i].Value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Key \"{key}\" not found");
                }
            }
            set
            {
                var i = _keyValuePairs.FindIndex(x => x.Key == key);
                if (i != -1)
                {
                    _keyValuePairs[i] = KeyValuePair.Create(key, value);
                }
                else
                {
                    _keyValuePairs.Add(KeyValuePair.Create(key, value));
                }
            }
        }

        /// <summary>
        /// Removes all key/value pairs.
        /// </summary>
        public void Clear()
        {
            _keyValuePairs.Clear();
        }

        /// <summary>
        /// Creates a new object instance and tries to assign all values from the csv dictionary via reflection. 
        /// </summary>
        /// <returns>Pointer to the newly created object instance</returns>
        public TEntity CreateAndGetValues<TEntity>()
        {
            var entity = Activator.CreateInstance<TEntity>();
            GetValues(entity);
            return entity;
        }

        /// <summary>
        /// Support for iteration over a key/value pairs collection
        /// </summary>
        /// <returns></returns>
        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return _keyValuePairs.GetEnumerator();
        }

        /// <summary>
        /// Support for iteration over a non-generic collection.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyValuePairs.GetEnumerator();
        }

        /// <summary>
        /// Gets the value of the specified key
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public object GetValue(string key, ICsvConverter converter)
        {
            return converter.FromString(this[key]);
        }

        /// <summary>
        /// Gets the typed value of the specified key
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Key name</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(string key)
        {
            return GetValue<T>(key, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Gets the typed value of the specified key
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Key name</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(string key, ICsvConverter converter)
        {
            return (T)GetValue(key, converter);
        }

        /// <summary>
        /// Gets the typed value of the specified key
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="name">Key name</param>
        /// <returns>A typed value</returns>
        public object GetValue(Type type, string key)
        {
            return GetValue(key, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Tries to assign all values from the csv dictionary to a custom object. 
        /// </summary>
        /// <param name="entity">Pointer to the custom object instance</param>
        /// <returns>Number of assigned values</returns>
        public int GetValues<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            int c = 0;
            foreach (var item in _keyValuePairs)
            {
                if (CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().ContainsValue(item.Key))
                {
                    if (TryGetValue(CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValueType(item.Key), item.Key, out var value))
                    {
                        CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().SetValue(entity, item.Key, value);
                        c++;
                    }
                }
            }
            return c;
        }

        /// <summary>
        /// Reads the csv dictionary out of a CSV source
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <returns>
        /// Number of key/value pairs read
        /// </returns>
        public int Load(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException(nameof(textReader));
            }

            _keyValuePairs.Clear();

            var c = 0;
            var csvValues = new List<string>();

            var csvReader = new CsvReader(textReader);

            while (csvReader.ReadLine(csvValues) > 0)
            {
                if (csvValues.Count > 1)
                {
                    this[csvValues[0]] = csvValues[1];
                }
                c++;
            }

            return c;
        }

        /// <summary>
        /// Reads the csv dictionary out of a CSV source
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        //  parameter contains the number of key/value pairs read.</returns>
        public async Task<int> LoadAsync(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException(nameof(textReader));
            }

            _keyValuePairs.Clear();

            var c = 0;
            var csvValues = new List<string>();

            var csvReader = new CsvReader(textReader);

            while (await csvReader.ReadLineAsync(csvValues) > 0)
            {
                if (csvValues.Count > 1)
                {
                    this[csvValues[0]] = csvValues[1];
                }
                c++;
            }

            return c;
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="key">Key name</param>
        /// <param name="value">The value</param>
        public void SetValue<T>(string key, T value)
        {
            SetValue(key, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="key">Key name</param>
        /// <param name="value">The value</param>
        public void SetValue<T>(string key, T value, ICsvConverter valueConverter)
        {
            this[key] = valueConverter.ToString(value);
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="key">Key name</param>
        /// <param name="value">The value</param>
        public void SetValue(Type type, string key, object value)
        {
            SetValue(key, value, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Applies all relevant values from an object to the csv dictionary via reflection.
        /// </summary>
        /// <param name="entity">Pointer to the object instance</param>
        /// <returns>Number of values applied</returns>
        public int SetValues<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            int c = 0;
            typeof(TEntity).VisitMembers((key, memberInfo, isAlternativeName) =>
            {
                if (!isAlternativeName)
                {
                    if (CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().ContainsValue(key))
                    {
                        var value = CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValue(entity, key);
                        SetValue(CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValueType(key), key, value);
                        c++;
                    }
                }
            });
            return c;
        }

        /// <summary>
        /// Writes the csv dictionary to a CSV target
        /// </summary>
        /// <param name="textWriter">A <see cref="TextWriter"/></param>
        /// <returns>Number of key/value pairs written</returns>
        public int Store(TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            var c = 0;
            var wasPreviousWrite = false;
            var csvWriter = new CsvWriter(textWriter);
            foreach (var keyValuePair in _keyValuePairs)
            {
                if (wasPreviousWrite)
                {
                    csvWriter.WriteLine();
                }
                wasPreviousWrite = true;
                csvWriter.WriteValues(keyValuePair.Key, keyValuePair.Value);
                c++;
            }
            return c;
        }

        /// <summary>
        /// Writes the csv dictionary to a CSV target
        /// </summary>
        /// <param name="textWriter">A <see cref="TextWriter"/></param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        //  parameter contains the number of key/value pairs written.</returns>
        public async Task<int> StoreAsync(TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            var c = 0;
            var wasPreviousWrite = false;
            var csvWriter = new CsvWriter(textWriter);
            foreach (var keyValuePair in _keyValuePairs)
            {
                if (wasPreviousWrite)
                {
                    await csvWriter.WriteLineAsync();
                }
                wasPreviousWrite = true;
                await csvWriter.WriteValuesAsync(keyValuePair.Key, keyValuePair.Value);
                c++;
            }
            return c;
        }

        /// <summary>
        /// Tries to read the value of the specified key. 
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="value">If the key within the csv dictionary was found, contains the value. If not contains null</param>
        /// <returns>true if the key within the cuurent the csv dictionary was found; otherwise, false.</returns>
        public bool TryGetValue(string key, out string value)
        {
            var keyValuePair = _keyValuePairs.Find(x => x.Key == key);
            if (keyValuePair.Key != null)
            {
                value = keyValuePair.Value;
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the specified key. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="key">Key name</param>
        /// <param name="value">If the key within the csv dictionary was found, contains the value. If not contains null</param>
        /// <returns>true if the key within the cuurent the csv dictionary was found; otherwise, false.</returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            var keyValuePair = _keyValuePairs.Find(x => x.Key == key);
            if (keyValuePair.Key != null)
            {
                value = GetValue<T>(key, ConverterResolver.GetConverter<T>());
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the specified key. 
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="key">Key name</param>
        /// <param name="value">If the key within the csv dictionary was found, contains the value. If not contains null</param>
        /// <returns>true if the key within the cuurent the csv dictionary was found; otherwise, false.</returns>
        public bool TryGetValue(Type type, string key, out object value)
        {
            var keyValuePair = _keyValuePairs.Find(x => x.Key == key);
            if (keyValuePair.Key != null)
            {
                value = GetValue(type, key);
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to set the value of the the specified key.
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="value">The value</param>
        /// <returns>true if key was found; otherwise, false.</returns>
        public bool TrySetValue(string key, string value)
        {
            var keyValuePair = _keyValuePairs.Find(x => x.Key == key);
            if (keyValuePair.Key != null)
            {
                this[key] = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to set the value of the the specified key.
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="value">The value</param>
        /// <returns>true if key was found; otherwise, false.</returns>
        public bool TrySetValue<T>(string key, T value)
        {
            var keyValuePair = _keyValuePairs.Find(x => x.Key == key);
            if (keyValuePair.Key != null)
            {
                SetValue(key, value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the specified key. The value is used as param for the specified delegate. 
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        public void UseValue(string key, Action<string> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(key, out string value))
            {
                useAction(value);
            }
        }

        /// <summary>
        /// Tries to read the typed value of the specified key. The value is used as param for the specified delegate. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Key name</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        public void UseValue<T>(string key, Action<T> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(key, out T value))
            {
                useAction(value);
            }
        }
    }
}
