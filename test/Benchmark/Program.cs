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

using BenchmarkDotNet.Running;

namespace Enbrea.Csv.Tests
{
    public class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run<CsvReaderBenchmark>();
        }
    }
}