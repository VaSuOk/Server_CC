
using MySql.Data.MySqlClient;
using Server_CC.DataContext.DataBase;
using Server_CC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server_CC.DataContext
{
    public class UserWorkInformationContext
    {
        public List<UserWorkInformation> GetAllUserWI()
        {
            List<UserWorkInformation> listUWI = new List<UserWorkInformation>();
            UsersContext usersContext = new UsersContext();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user_working_information`", DBConnection.Get_Instance().connection);
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        UserWorkInformation userWorkInformation = new UserWorkInformation();
                        userWorkInformation.ID = Convert.ToUInt32(temp.Rows[i][0]);
                        userWorkInformation.user = usersContext.GetUserByID(Convert.ToInt32(temp.Rows[i][1]));
                        userWorkInformation.Stage = (temp.Rows[i][2]).ToString();
                        userWorkInformation.Position = (temp.Rows[i][3]).ToString();
                        userWorkInformation.WorkRegion = (temp.Rows[i][4]).ToString();
                        userWorkInformation.Salary = (temp.Rows[i][5]).ToString();

                        listUWI.Add(userWorkInformation);
                    }
                    DBConnection.Get_Instance().Disconnect();

                    return listUWI;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

        public int InsertUserWorkInformation(UserWorkInformation userWorkInformation)
        {
            try
            {
                UsersContext usersContext = new UsersContext();
                usersContext.CreateUser(userWorkInformation.user);
                int id = usersContext.GetUserID(userWorkInformation.user.Name, userWorkInformation.user.Phone);
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "INSERT INTO `user_working_information` (`ID_User`, `Stage`, `Position`, `Region`, `Salary`) " +
                            "VALUES ( @ID_User, @Stage, @Position, @Region, @Salary)", DBConnection.Get_Instance().connection);

                command.Parameters.Add("@ID_User", MySqlDbType.Int32).Value = id;
                command.Parameters.Add("@Stage", MySqlDbType.VarChar).Value = userWorkInformation.Stage;
                command.Parameters.Add("@Position", MySqlDbType.VarChar).Value = userWorkInformation.Position;
                command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = userWorkInformation.WorkRegion;
                command.Parameters.Add("@Salary", MySqlDbType.VarChar).Value = userWorkInformation.Salary;
                
                if (command.ExecuteNonQuery() > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string ar = ex.Message;
                throw;
            }
        }
    }
}
