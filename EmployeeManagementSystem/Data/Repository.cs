using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EmployeeManagementSystem.Models;
using System.Configuration;
using System.Data;


namespace EmployeeManagementSystem.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public void Add(Employee e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Employees (Name, Age, Department, Salary) VALUES (@Name, @Age, @Department, @Salary)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", e.Name);
                    command.Parameters.AddWithValue("@Age", e.Age);
                    command.Parameters.AddWithValue("@Department",e.Department);
                    command.Parameters.AddWithValue("@Salary", e.Salary);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Employee e, int action)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";

                switch (action)
                {
                    case 1:
                        query = "UPDATE Employees SET Name=@Value WHERE Id=@Id";
                        break;
                    case 2:
                        query = "UPDATE Employees SET Department=@Value WHERE Id=@Id";
                        break;
                    case 3:
                        query = "UPDATE Employees SET Salary=@Value WHERE Id=@Id";
                        break;
                    case 4:
                        query = "UPDATE Employees SET Age=@Value WHERE Id=@Id";
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        return;
                }

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value", action == 3 ? (object)e.Salary : action == 4 ? (object)e.Age : e.Name ?? e.Department);
                    command.Parameters.AddWithValue("@Id", e.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();

                string query = "DELETE FROM EMPLOYEES WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@Id" , id);
                }
            }
        }

        public List<T> GetAll()
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Employee
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Age = (int)reader["Age"],
                            Department = reader["Department"].ToString(),
                            Salary = (decimal)reader["Salary"]
                        } as T);
                    }
                }
            }
            return result;
        }

        public DataTable GetById(int id)
        {
            DataTable dt = new DataTable();
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();

                string query = "SELECT (Name, Age, Department, Salary) FROM EMPLOYEES WHERE Id = @Id";
                SqlCommand command  = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);
                }
            return dt;
        }
    }
}
