using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace network_programming_midterm_2
{
    class DB
    {
        // Create connection to localhost Dictionary database
        private static SqlConnection dbConn = new SqlConnection(@"Data source=localhost;Initial Catalog=Dictionary;Integrated Security=True;");
        private static SqlCommand cmd = new SqlCommand();
        
        public static bool Authentication(string username, string password)
        {
            // Open connection to db
            dbConn.Open();
            cmd.Connection = dbConn;
            cmd.CommandText = $"SELECT COUNT(*) FROM Users WHERE Username='{username}' AND Pswd='{password}'";
            int count = (int)cmd.ExecuteScalar();
            // Close db connection
            dbConn.Close();
            // Check authentication result
            if (count == 1)
                return true;
            return false;
        }
        public static bool Register(string username, string password)
        {
            // Open connection to db
            dbConn.Open();
            cmd.Connection = dbConn;
            // Check if username has already existed in db
            cmd.CommandText = $"SELECT COUNT(*) FROM Users WHERE Username='{username}'";
            int count = (int)cmd.ExecuteScalar();
            // Return false if username has already existed in db
            if (count != 0)
            {
                dbConn.Close();
                return false;
            }
            // Else insert username and password (in digest form) into database
            // Get new id for new user
            string usrid = GetNumberOfRows("Users").ToString("D3");
            // Check if database connection is still open
            if(dbConn.State != ConnectionState.Open)
            {
                dbConn.Open();
                cmd.Connection = dbConn;
            }
            cmd.CommandText = $"INSERT INTO Users(UserId, Username, Pswd) VALUES ('USR{usrid}', '{username}', '{password}')";
            cmd.ExecuteNonQuery();

            dbConn.Close();
            return true;
        }
        // A property supports defining ID for new data
        public static int GetNumberOfRows(string tb_name)
        {
            if(dbConn.State != ConnectionState.Open)
            {
                dbConn.Open();
            }
            cmd.Connection = dbConn;
            // Check if table name is valid. i.e. Table exists
            cmd.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='Dictionary' AND TABLE_NAME='{tb_name}'";
            int count = (int)cmd.ExecuteScalar();
            if(count == 1)
            {
                // Count number of rows in that table
                cmd.CommandText = $"SELECT COUNT(*) FROM {tb_name}";
                count = (int)cmd.ExecuteScalar();
            }
            else
            {
                count = 0;
            }
            dbConn.Close();
            // Return number of rows according to table name
            return count;
        }
    }
}
