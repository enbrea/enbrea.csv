#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// A static factory for <see cref="ICsvClassMapperResolver"/> implementations
    /// </summary>
    public static class CsvClassMapperResolverFactory
    {
        private static ICsvClassMapperResolver _classMapResolver = new CsvDefaultClassMapperResolver();

        public static ICsvClassMapperResolver GetResolver()
        {
            return _classMapResolver;
        }

        public static void SetResolver(ICsvClassMapperResolver resolver)
        {
            _classMapResolver = resolver;
        }
    }
}