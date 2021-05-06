#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Csv.Tests
{
    public class SampleObject
    {
        public int A;
        [CsvHeader("B", "B1")]
        public string B;
        [CsvHeader("C", "C2")]
        public bool? C;
        public DateTime D;
        [CsvHeader("e")]
        public string E;
        [CsvNotMapped]
        public string F;

        public override bool Equals(object obj)
        {
            return
                (A == ((SampleObject)obj).A) &&
                (B == ((SampleObject)obj).B) &&
                (C == ((SampleObject)obj).C) &&
                (D == ((SampleObject)obj).D) &&
                (E == ((SampleObject)obj).E) &&
                (F == ((SampleObject)obj).F);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
