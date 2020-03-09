using Npgsql;
using System;

namespace DatabaseService
{
    public class NpgsqlDatabaseService : IDatabaseService
    {
        public T GetValue<T>(string databaseName, string tableName, string fieldName, string keyFieldName, string keyFieldValue)
        {
            var connString = $"Host=myserver;Username=mylogin;Password=mypass;Database={databaseName}";
            T retValue = default(T);
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand($"SELECT {fieldName} FROM {tableName} WHERE {keyFieldName} EQUALS {keyFieldValue}", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        retValue = reader.GetFieldValue<T>(0);
            }
            return retValue;
        }

    }
}
