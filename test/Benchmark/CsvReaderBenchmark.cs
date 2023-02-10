#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Enbrea.Csv.Tests
{
    public class CsvReaderBenchmark
    {
        [Params(1000000)]
        public int NumberOfCsvRecords;

        private string _data;

        [Benchmark]
        public void TestCsvHelper()
        {
            var l = new List<string[]>();

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                CacheFields = true
            };

            using var strReader = new StringReader(_data);
            using var csvReader = new CsvHelper.CsvReader(strReader, config);

            while (csvReader.Read())
            {
                var a = new string[3];
                a[0] = csvReader.GetField(0);
                a[1] = csvReader.GetField(1);
                a[2] = csvReader.GetField(2);
                l.Add(a);
            }

            if (l.Count != NumberOfCsvRecords) throw new Exception("Wrong number of records");
        }

        [Benchmark]
        public async Task TestCsvHelperAsync()
        {
            var l = new List<string[]>();

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                CacheFields = true
            };

            using var strReader = new StringReader(_data);
            using var csvReader = new CsvHelper.CsvReader(strReader, config);

            while (await csvReader.ReadAsync())
            {
                var a = new string[3];
                a[0] = csvReader.GetField(0);
                a[1] = csvReader.GetField(1);
                a[2] = csvReader.GetField(2);
                l.Add(a);
            }

            if (l.Count != NumberOfCsvRecords) throw new Exception("Wrong number of records");
        }

        [Benchmark]
        public void TestEnbreaCsv()
        {
            var c = new string[3];
            var l = new List<string[]>();

            var config = new CsvConfiguration()
            {
                Separator = ';',
                CacheValues = true
            };

            using var strReader = new StringReader(_data);
            var csvReader = new CsvReader(strReader, config);

            while (csvReader.ReadLine(c) > 0)
            {
                var a = new string[3];
                a[0] = c[0];
                a[1] = c[1];
                a[2] = c[2];
                l.Add(a);
            }

            if (l.Count != NumberOfCsvRecords) throw new Exception("Wrong number of records");
        }

        [Benchmark]
        public async Task TestEnbreaCsvAsync()
        {
            var c = new string[3];
            var l = new List<string[]>();

            var config = new CsvConfiguration()
            {
                Separator = ';',
                CacheValues = true
            };

            using var strReader = new StringReader(_data);
            var csvReader = new CsvReader(strReader, config);

            while (await csvReader.ReadLineAsync(c) > 0)
            {
                var a = new string[3];
                a[0] = c[0];
                a[1] = c[1];
                a[2] = c[2];
                l.Add(a);
            }

            if (l.Count != NumberOfCsvRecords) throw new Exception("Wrong number of records");
        }

        [GlobalSetup]
        public void Setup()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumberOfCsvRecords; i++)
            {
                sb.Append("aaa;bbb;ccc\n");
            }
            _data = sb.ToString();
        }
    }
}