using Microsoft.Data.SqlClient;

namespace AjaxDemo.Data
{

    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ppl";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Ppl (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }
        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Ppl WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void Edit(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Ppl SET firstName = @first, lastName = @last, age = @age WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", person.Id);
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
