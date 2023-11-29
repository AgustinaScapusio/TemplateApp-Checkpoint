using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Template
{
    internal class TemplateClass
    {
        //First, you need to installed Microsoft.Data.SqlClient in Dependences/Nuggets Packages.This is a must
        #region Connection to database
        private const string _connectionString =
         @"Server=(localdb)\MSSQLLocalDB;" +
          "Database = NAMEOFDATABASE;" +
          "Integrated Security=true";
        #endregion
        public int CreateOneRowInDB(string n2, string n3, string n4)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO TableName(Column2, Column3, Column4) \n" +
                "VALUES \n" +
                "(@n2, @n3, @n4)" +
                "SELECT SCOPE_IDENTITY() as ID";
            command.Parameters.AddWithValue("@n2", n2);
            command.Parameters.AddWithValue("@n3", n3);
            command.Parameters.AddWithValue("@n4", n4);
            int id = Convert.ToInt32(command.ExecuteScalar());
            return id;
        }
        public Object? ReadOneRowAtATime(int id)
        {
            using SqlConnection connection = new(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
                "SELECT ID, Column2, Column3, Colum4 \n" +
                "FROM TableName \n" +
                "WHERE ID = @id";
            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Class objectName = new((int)reader["ID"], reader["Column2"]?.ToString() ?? "", reader["Column3"]?.ToString() ?? "", reader["Column4"]?.ToString() ?? "");
                return objectName;
            }
            else
            {
                return null; // No contact found with the given ID
            }
        }
        public List<Object> ReadAllRows()
        {
            List<Object> listName = new ();

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
                "SELECT *\n" +
                "FROM TableName";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Class objectName = new((int)reader["ID"], reader["Column2"]?.ToString() ?? "", reader["Column3"]?.ToString() ?? "", reader["Column4"]?.ToString() ?? "");
                    listName.Add(objectName);
                }

            }
            return listName;
        }

        
        public bool UpdateRowAtATime(int Id, string n2, bool n3, string n4)
        {
            using SqlConnection sqlConnection = new(_connectionString);
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText =
                "UPDATE TabelName \n" +
                "SET Column2 = @n2, Column3= @n3, Column4 = @n4 \n" +
                "WHERE ID=@id";
            command.Parameters.AddWithValue("@n2", n2);
            command.Parameters.AddWithValue("@n3", n3 ? 'T' : 'F');
            command.Parameters.AddWithValue("@n4", n4);
            command.Parameters.AddWithValue("@id", Id);
            sqlConnection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected >= 1)
            {
                return true;
            }
            return false;
        }
        public bool DeleteARowAtATime(int id)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
                "DELETE FROM TableName \n" +
                "WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected >= 1)
            {
                return true;
            }
            return false;
        }



       
    }
}
