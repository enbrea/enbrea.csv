#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// Types of CSV diff operations
    /// </summary>
    public enum CsvDiffType { 
        AddedOnly, 
        DeletedOnly, 
        UpdatedOnly, 
        AddedOrUpdatedOnly 
    };
}
