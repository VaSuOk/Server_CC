using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_CC.DataContext.DataBase
{
    public class DBConnection
    {
        public MySqlConnection connection;

        private static DBConnection dataBase;
        private DBConnection()
        {
            connection = new MySqlConnection("server=localhost; port=3306; username=root; password=; database=сconstruction;");
        }
        public static DBConnection Get_Instance() { return dataBase == null ? dataBase = new DBConnection() : dataBase; }
        public void Connect()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else Console.WriteLine("БД не знайдена");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Disconnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public bool Is_Connected()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            return false;
        }
    }
}
