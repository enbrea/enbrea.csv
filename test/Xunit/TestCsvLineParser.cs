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

using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvLineParser"/>.
    /// </summary>
    public class TestCsvLineParser
    {
        [Fact]
        public void SupportConvertingToArray()
        {
            var textLine = "aaa1;bbb1;ccc1";

            var csvParser = new CsvLineParser();
            {
                Assert.NotNull(csvParser);

                var fields = csvParser.Read(textLine);

                Assert.Equal(3, fields.Count());
                Assert.Equal("aaa1", fields[0]);
                Assert.Equal("bbb1", fields[1]);
                Assert.Equal("ccc1", fields[2]);
            }
        }

        [Fact]
        public void SupportConvertingToCollection()
        {
            var textLine = "aaa1;bbb1;ccc1";

            var csvParser = new CsvLineParser();
            {
                Assert.NotNull(csvParser);

                var fields = new List<string>();

                csvParser.Read(textLine, fields);

                Assert.Equal(3, fields.Count);
                Assert.Equal("aaa1", fields[0]);
                Assert.Equal("bbb1", fields[1]);
                Assert.Equal("ccc1", fields[2]);
            }
        }

        [Fact]
        public void SupportConvertingEmptyLine()
        {
            var textLine = "";

            var csvParser = new CsvLineParser();
            {
                Assert.NotNull(csvParser);

                var fieldsArray = csvParser.Read(textLine);
                Assert.Single(fieldsArray);

                var fieldsList = new List<string>();
                csvParser.Read(textLine, fieldsList);
                Assert.Single(fieldsList);
            }
        }
    }
}
