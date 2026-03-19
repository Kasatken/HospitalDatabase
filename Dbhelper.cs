using System;
using System.Data;
using Npgsql;

namespace PolyclinicApp
{
    // Класс-помощник для работы с базой данных
    public static class DbHelper
    {
        
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(Program.ConnectionString);
        }

        // Выполнить SELECT и вернуть таблицу
        public static DataTable ExecuteQuery(string sql, params NpgsqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }

        // Выполнить INSERT,UPDATE,DELETE
        public static int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Выполнить запрос и вернуть значение
        public static object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Проверить подключение
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}