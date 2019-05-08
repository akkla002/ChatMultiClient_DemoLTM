using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Server
{
    public class DataProvider
    {
        private static DataProvider instance;

        private string connectionSTR = @"Data Source=.\;Initial Catalog=SQL_ChatMulti;Integrated Security=True";

        internal static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        private DataProvider() { }
        private SqlCommand GetCommand(string query, object[] parameters = null)
        {
            SqlCommand command = new SqlCommand(query);
            if (parameters != null)
            {
                string[] listWord = query.Split(' ');
                int i = 0;
                foreach (string item in listWord)
                {
                    if (item[0] == '@')
                    {
                        command.Parameters.AddWithValue(item, parameters[i]);
                        i++;
                    }
                }
            }
            return command;
        }
        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();
                SqlCommand command = GetCommand(query, parameters);
                command.Connection = conn;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                conn.Close();
            }
            GC.Collect();
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameters = null)
        {
            int rowAccept;
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();
                SqlCommand command = GetCommand(query, parameters);
                command.Connection = conn;
                rowAccept = command.ExecuteNonQuery();
                conn.Close();
            }
            GC.Collect();
            return rowAccept;
        }
        public object ExecuteScalar(string query, object[] parameters = null)
        {
            object data;
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();
                SqlCommand command = GetCommand(query, parameters);
                command.Connection = conn;
                data = command.ExecuteScalar();
                conn.Close();
            }
            GC.Collect();
            return data;
        }
    }
}
