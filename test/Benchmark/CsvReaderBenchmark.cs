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
using System.Collections.Generic;
using System.Text;

namespace Enbrea.Csv.Tests
{
    public class CsvReaderBenchmark
    {
        [Params(1000)]
        public int NumberOfCsvRecords;

        private string _data;

        [Benchmark]
        public void ReadAllPerformance()
        {
            using var csvReader = new CsvReader(_data);

            foreach (var line in csvReader.ReadAll())
            {
            }
        }

        [Benchmark]
        public void ReadLineAsActionPerformance()
        {
            using var csvReader = new CsvReader(_data);
            
            while (csvReader.ReadLine((i, s) => { }) > 0)
            {
            }
        }

        [Benchmark]
        public void ReadLineAsListPerformance()
        {
            var fields = new List<string>();
            using var csvReader = new CsvReader(_data);
            
            while (csvReader.ReadLine(fields) > 0)
            {
            }
        }

        [Benchmark]
        public void ReadNormalizeAllPerformance()
        {
            using var csvReader = new CsvReader(_data);
            
            foreach (var line in csvReader.ReadAllLines())
            {
            }
        }

        [GlobalSetup]
        public void Setup()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumberOfCsvRecords; i++)
            {
                sb.Append("aaa;bbb;ccc;ddd;eee;fff");
            }
            _data = sb.ToString();
        }
    }
}