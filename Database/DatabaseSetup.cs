namespace Avaliacao2BimLp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;

public class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateStudentTable();
    }

    private void CreateStudentTable()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Students(
                registration varchar(30) not null primary key,
                name varchar(30) not null,
                city varchar(30) not null,
                former boolean not null
            );
        ");
    }
}