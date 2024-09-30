using LocalDBWebAPI.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Buffers.Text;
using System.ComponentModel;
using System;
using System.Data.SQLite;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace LocalDBWebAPI.Data
{
    public class DBManager
    {
        private static string connectionString = "Data Source=mydatabase.db;Version=3;";
        
        public static bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // SQL command to create a table named "StudentTable"
                        command.CommandText = @"
                    CREATE TABLE StudentTable (
                        ID INTEGER,
                        Name TEXT,
                        Age INTEGER
                    )";

                        // Execute the SQL command to create the table
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                Console.WriteLine("Table created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false; // Create table failed

        }
        public static bool Insert(Student student)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // SQL command to insert data into the "StudentTable" table
                        command.CommandText = @"
                        INSERT INTO StudentTable (ID, Name, Age)
                        VALUES (@ID, @Name, @Age)";

                        // Define parameters for the query
                        command.Parameters.AddWithValue("@ID", student.Id);
                        command.Parameters.AddWithValue("@Name", student.Name);
                        command.Parameters.AddWithValue("@Age", student.Age);

                        // Execute the SQL command to insert data
                        int rowsInserted = command.ExecuteNonQuery();

                        // Check if any rows were inserted
                        connection.Close();
                        if (rowsInserted > 0)
                        {
                            return true; // Insertion was successful
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false; // Insertion failed
        }
   
        public static bool Delete(int id)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to delete data by ID
                        command.CommandText = $"DELETE FROM StudentTable WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", id);

                        // Execute the SQL command to delete data
                        int rowsDeleted = command.ExecuteNonQuery();

                        // Check if any rows were deleted
                        connection.Close();
                        if (rowsDeleted > 0)
                        {
                            return true; // Deletion was successful
                        }
                    }
                    connection.Close();
                }

                return false; // No rows were deleted
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Deletion failed
            }
        }
        public static bool Update(Student student)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to update data by ID
                        command.CommandText = $"UPDATE StudentTable SET Name = @Name, Age = @Age WHERE ID = @ID";
                        command.Parameters.AddWithValue("@Name", student.Name);
                        command.Parameters.AddWithValue("@Age", student.Age);
                        command.Parameters.AddWithValue("@ID", student.Id);

                        // Execute the SQL command to update data
                        int rowsUpdated = command.ExecuteNonQuery();
                        connection.Close();
                        // Check if any rows were updated
                        if (rowsUpdated > 0)
                        {
                            return true; // Update was successful
                        }
                    }
                    connection.Close();
                }

                return false; // No rows were updated
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Update failed
            }
        }

        public static List<Student> GetAll()
        {
            List<Student> studentList = new List<Student>();

            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to select all student data
                        command.CommandText = "SELECT * FROM StudentTable";

                        // Execute the SQL command and retrieve data
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student();
                                student.Id = Convert.ToInt32(reader["ID"]);
                                student.Name = reader["Name"].ToString();
                                student.Age = Convert.ToInt32(reader["Age"]);         

                                // Create a Student object and add it to the list
                                studentList.Add(student);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return studentList;
        }

        public static Student GetById(int id)
        {
            Student student = null;

            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to select a student by ID
                        command.CommandText = "SELECT * FROM StudentTable WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", id);

                        // Execute the SQL command and retrieve data
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new Student();
                                student.Id = Convert.ToInt32(reader["ID"]);
                                student.Name = reader["Name"].ToString();
                                student.Age = Convert.ToInt32(reader["Age"]);
                               
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return student;
        }

        public static void DBInitialize()
        {
            if (CreateTable())
            {
                Student student = new Student();
                student.Id = 1;
                student.Name = "Sajib";
                student.Age = 20;
                Insert(student);

                student = new Student();
                student.Id = 2;
                student.Name = "Mistry";
                student.Age = 30;
                Insert(student);

                student = new Student();
                student.Id = 3;
                student.Name = "Mike";
                student.Age = 40;
                Insert(student);

            }
        }
    }


}

    
