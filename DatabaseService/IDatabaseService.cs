using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService
{
    public interface IDatabaseService
    {
        T GetValue<T>(string databaseName, string tableName, string fieldName, string keyFieldName, string keyFieldValue);
    }
}
