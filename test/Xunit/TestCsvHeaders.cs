#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Linq;
using Xunit;

namespace Enbrea.Csv.Tests
{

    /// <summary>
    /// Unit tests for <see cref="CsvHeaders"/> and <see cref="CsvHeaders{TEntity}"/>.
    /// </summary>
    public class TestCsvHeaders
    {
        [Fact]
        public void TestExpression()
        {
            var csvHeaders1 = new CsvHeaders("A", "B", "C", "D");
            var csvHeaders2 = new CsvHeaders<SampleObject>(x => new { x.A, x.B, x.C, x.D });

            Assert.True(csvHeaders1.SequenceEqual(csvHeaders2));
        }

        [Fact]
        public void TestHeaderAttributes()
        {
            var csvHeaders1 = new CsvHeaders("A", "e");
            var csvHeaders2 = new CsvHeaders<SampleObject>(x => new { x.A, x.E });

            Assert.True(csvHeaders1.SequenceEqual(csvHeaders2));
        }
    }
}
