using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_March_27.Data
{
    public class Manager
    {
        private string _connectionString;
            public Manager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPerson(List<Person> people)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            connection.Open();
            foreach (Person person in people)
            {
                command.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES (@firstName, @lastName, @age)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.ExecuteNonQuery();
            }
        }
        public List<Person>GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            List<Person> people = new();
            connection.Open();
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                Person p = new Person
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                };
                people.Add(p);
            }
            return people;
        }
    }
}
