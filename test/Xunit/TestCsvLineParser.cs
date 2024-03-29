﻿#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
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
        public void SupportConvertingEmptyLine()
        {
            var textLine = "";

            var csvParser = new CsvLineParser(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvParser);

                var fieldsArray = csvParser.Parse(textLine);
                Assert.Single(fieldsArray);

                var fieldsList = new List<string>();
                csvParser.Parse(textLine, fieldsList);
                Assert.Single(fieldsList);
            }
        }

        [Fact]
        public void SupportConvertingEmptyValues()
        {
            var textLine = ";bbb1;";

            var csvParser = new CsvLineParser(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvParser);

                var fields = csvParser.Parse(textLine);
                Assert.Equal(3, fields.Count());
                Assert.Equal("", fields[0]);
                Assert.Equal("bbb1", fields[1]);
                Assert.Equal("", fields[2]);
            }
        }

        [Fact]
        public void SupportConvertingToArray()
        {
            var textLine = "aaa1;bbb1;ccc1";

            var csvParser = new CsvLineParser(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvParser);

                var fields = csvParser.Parse(textLine);

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

            var csvParser = new CsvLineParser(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvParser);

                var fields = new List<string>();

                csvParser.Parse(textLine, fields);

                Assert.Equal(3, fields.Count);
                Assert.Equal("aaa1", fields[0]);
                Assert.Equal("bbb1", fields[1]);
                Assert.Equal("ccc1", fields[2]);
            }
        }
    }
}
