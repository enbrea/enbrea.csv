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
    /// Unit tests for <see cref="CsvLineBuilder"/>.
    /// </summary>
    public class TestCsvLineBuilder
    {
        [Fact]
        public void SupportConvertingFromArray()
        {
            var fields = new string[] { "aaa1", "bbb1", "ccc1" };

            var csvBuilder = new CsvLineBuilder(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvBuilder);

                var textLine = csvBuilder.ToString(fields);

                Assert.Equal("aaa1;bbb1;ccc1", textLine);
            }
        }

        [Fact]
        public void SupportConvertingFromCollection()
        {
            var fields = new List<string>() { "aaa1", "bbb1", "ccc1" };

            var csvBuilder = new CsvLineBuilder(new CsvConfiguration { Separator = ';' });
            {
                Assert.NotNull(csvBuilder);

                var textLine = csvBuilder.ToString(fields);

                Assert.Equal("aaa1;bbb1;ccc1", textLine);
            }
        }
    }
}
