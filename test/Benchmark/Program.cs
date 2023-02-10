#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Enbrea.Csv.Tests
{
    public class Program
    {
        public static void Main()
        {
            IConfig config = null;
#if DEBUG
            config = new DebugInProcessConfig();
#else
            config = null;
#endif            
            _ = BenchmarkRunner.Run<CsvReaderBenchmark>(config);
        }
    }
}