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
using System.IO;
using System.Linq;
using System.Text;
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

        [Fact]
        public void TestNoHeaderInFile()
        {
            var csvData =
                "a1;b1;c1";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' }, "A", "B", "C");

            Assert.Equal(3, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", "a1");
            csvTableWriter.SetValue("B", "b1");
            csvTableWriter.SetValue("C", "c1");

            csvTableWriter.Write();

            Assert.Equal(csvData, sb.ToString());
        }
    }
}
