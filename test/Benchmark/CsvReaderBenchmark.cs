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

using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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
            var a = new string[3];
            var l = new List<string[]>();

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using var strReader = new StringReader(_data);
            using var csvReader = new CsvHelper.CsvReader(strReader, config);

            while (csvReader.Read())
            {
                a[0] = csvReader.GetField(0);
                a[1] = csvReader.GetField(1);
                a[2] = csvReader.GetField(2);
                l.Add((string[])a.Clone());
            }

            if (l.Count != NumberOfCsvRecords) throw new Exception("Wrong number of records");
        }

        [Benchmark]
        public void TestEnbreaCsv()
        {
            var a = new string[3];
            var l = new List<string[]>();

            using var csvReader = new CsvReader(_data);

            csvReader.Configuration.Separator = ';';

            while (csvReader.ReadLine((i, s) => { a[i] = s; }) > 0)
            {
                l.Add((string[])a.Clone());
                a = new string[3];
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