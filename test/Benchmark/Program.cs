#region ENBREA.CSV - Copyright (C) 2021 ST�BER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 ST�BER SYSTEMS GmbH
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