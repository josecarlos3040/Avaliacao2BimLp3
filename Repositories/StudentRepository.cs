namespace Avaliacao2BimLp3.Repositories;
using Avaliacao2BimLp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;

public class StudentRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public StudentRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public Student Save(Student student)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Students VALUES (@Registration, @Name, @City, @Former)", student);

        return student;
    }

    public void MarkAsFormed(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        connection.Execute("UPDATE Students SET former = true WHERE registration = @Registration", new {Registration = registration});
    }

    public List<Student> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<Student>("SELECT * FROM Students");

        return students.ToList();
    }

    public List<Student> GetAllFormed()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<Student>("SELECT * FROM Students WHERE former = true");

        return students.ToList();
    }

    public List<Student> GetAllStudentByCity(string city)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<Student>("SELECT * FROM Students WHERE city LIKE @City", new{City = $"{city}%"});

        return students.ToList();
    }

    public List<Student> GetAllByCities(string[] cities)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<Student>("SELECT * FROM Students WHERE city IN @City", new{City = cities});

        return students.ToList();
    }

    public List<CountStudentGroupByAttribute> CountByFormed()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<CountStudentGroupByAttribute>("SELECT former as AttributeName, COUNT(former) as StudentNumber FROM Students GROUP BY former");

        return students.ToList();
    }

    public List<CountStudentGroupByAttribute> CountByCities()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var students = connection.Query<CountStudentGroupByAttribute>("SELECT city as AttributeName, COUNT(city) as StudentNumber FROM Students GROUP BY city");

        return students.ToList();
    }

    public void Delete(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Students WHERE registration = @Registration", new {Registration = registration});
    }

    public bool ExistsById(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(registration) FROM Students WHERE registration = @Registration", new{Registration = registration}));

        return result;
    }


}