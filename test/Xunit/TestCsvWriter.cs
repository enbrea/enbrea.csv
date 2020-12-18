#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvWriter"/>.
    /// </summary>
    public class TestCsvWriter
    {
        [Fact]
        public void SupportDefaultFields()
        {
            var sb = new StringBuilder();

            using var csvWriter = new CsvWriter(sb);

            Assert.NotNull(csvWriter);

            csvWriter.WriteValue("aaa1");
            csvWriter.WriteValue("bbb1");
            csvWriter.WriteValue("ccc1");

            csvWriter.WriteLine();

            csvWriter.WriteValue("aaa2");
            csvWriter.WriteValue("bbb2");
            csvWriter.WriteValue("ccc2");

            Assert.Equal(
                "aaa1;bbb1;ccc1" + Environment.NewLine +
                "aaa2;bbb2;ccc2", sb.ToString());
        }

        [Fact]
        public void SupportMultilineFields()
        {
            var sb = new StringBuilder();

            using var csvWriter = new CsvWriter(sb);
            
            Assert.NotNull(csvWriter);

            csvWriter.WriteValue("aaa1");
            csvWriter.WriteValue("bbb1");
            csvWriter.WriteValue("A long" + Environment.NewLine +
                "multi line" + Environment.NewLine + "text");

            csvWriter.WriteLine();

            csvWriter.WriteValue("aaa2");
            csvWriter.WriteValue("bbb2");
            csvWriter.WriteValue("ccc2");

            Assert.Equal(
                "aaa1;bbb1;\"A long" + Environment.NewLine +
                "multi line" + Environment.NewLine +
                "text\"" + Environment.NewLine +
                "aaa2;bbb2;ccc2", sb.ToString());
        }
    }
}
