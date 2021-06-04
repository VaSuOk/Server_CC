using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Server_CC.DataContext.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server_CC.DataContext
{
    public class AdminContext
    {
        public ActionResult<int> AdminLogin( string Login, string Password)
        {
            DataTable temp = new DataTable();
            try
            {

                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `ID` FROM `administration` WHERE `Login` = @Login AND `Password` = @Password", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = Login;
                command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = Password;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                DBConnection.Get_Instance().Disconnect();
                if (temp.Rows.Count > 0)
                {
                    return Convert.ToInt32(temp.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                DBConnection.Get_Instance().Disconnect();
                return -1;
            }
        }
    }
}
