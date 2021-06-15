
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
        public List<UserWorkInformation> GetUserWIByStageAndPosition( string Region, string Stage, string Position)
        {
            string query; bool isMany = false;
            if (Region == "none") { query = ""; }
            else { query = "`Region` = '" + Region + "'"; isMany = true; }
            if (Stage == "none") { query += ""; }
            else
            {
                if (isMany) { query += " AND  `Stage` = '" + Stage + "'"; }
                else { isMany = true;  }
            }
            if (Position == "none") { query += ""; }
            else 
            {
                if (isMany) { query += " AND  `Position` = '" + Position + "'"; }
                else { isMany = true; query = " `Position` = '" + Position + "'"; }
            }

            if(query != "") { query = "WHERE " + query; }

            List<UserWorkInformation> listUWI = new List<UserWorkInformation>();
            UsersContext usersContext = new UsersContext();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user_working_information` "+query, DBConnection.Get_Instance().connection);
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
            catch(Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

        public List<UserWorkInformation> GetUserWIByNameAndSurname(string Name, string Surname)
        {
            List<UserWorkInformation> listUWI = new List<UserWorkInformation>();
            UsersContext usersContext = new UsersContext();
            List<User> users = usersContext.GetUserByInitial(Name, Surname);
            try
            {
                if (users != null)
                {
                    foreach (var item in users)
                    {
                        listUWI.Add(GetUserWIByUserID((int)item.id));
                    }
                    return listUWI;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

        public UserWorkInformation GetUserWIByID(int ID)
        {
            UsersContext usersContext = new UsersContext();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user_working_information` WHERE `ID` = @ID", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {

                    UserWorkInformation userWorkInformation = new UserWorkInformation();
                    userWorkInformation.ID = Convert.ToUInt32(temp.Rows[0][0]);
                    userWorkInformation.user = usersContext.GetUserByID(Convert.ToInt32(temp.Rows[0][1]));
                    userWorkInformation.Stage = (temp.Rows[0][2]).ToString();
                    userWorkInformation.Position = (temp.Rows[0][3]).ToString();
                    userWorkInformation.WorkRegion = (temp.Rows[0][4]).ToString();
                    userWorkInformation.Salary = (temp.Rows[0][5]).ToString();

                    DBConnection.Get_Instance().Disconnect();

                    return userWorkInformation;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

        public UserWorkInformation GetUserWIByUserID(int ID)
        {
            UsersContext usersContext = new UsersContext();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user_working_information` WHERE `ID_User` = @ID", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {

                    UserWorkInformation userWorkInformation = new UserWorkInformation();
                    userWorkInformation.ID = Convert.ToUInt32(temp.Rows[0][0]);
                    userWorkInformation.user = usersContext.GetUserByID(Convert.ToInt32(temp.Rows[0][1]));
                    userWorkInformation.Stage = (temp.Rows[0][2]).ToString();
                    userWorkInformation.Position = (temp.Rows[0][3]).ToString();
                    userWorkInformation.WorkRegion = (temp.Rows[0][4]).ToString();
                    userWorkInformation.Salary = (temp.Rows[0][5]).ToString();

                    DBConnection.Get_Instance().Disconnect();

                    return userWorkInformation;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

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
        public int UpdateUserWI(UserWorkInformation  userWorkInformation)
        {
            UsersContext usersContext = new UsersContext();
            usersContext.UpdateUser(userWorkInformation.user);
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "UPDATE `user_working_information` SET `Stage` = @Stage, `Position` = @Position, `Region` = @Region, `Salary` = @Salary " +
                            "WHERE `ID` = @ID", DBConnection.Get_Instance().connection);

                command.Parameters.Add("@Stage", MySqlDbType.VarChar).Value = userWorkInformation.Stage;
                command.Parameters.Add("@Position", MySqlDbType.VarChar).Value = userWorkInformation.Position;
                command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = userWorkInformation.WorkRegion;
                command.Parameters.Add("@Salary", MySqlDbType.VarChar).Value = userWorkInformation.Salary;

                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = userWorkInformation.ID;

                if (command.ExecuteNonQuery() > 0)
                {
                    DBConnection.Get_Instance().Disconnect();
                    return 1;
                }
                else
                {
                    DBConnection.Get_Instance().Disconnect();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return -1;
            }
        }
        public int DeleteUserWI(int id)
        {
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "DELETE FROM `user_working_information` " +
                            "WHERE `ID` = @ID", DBConnection.Get_Instance().connection);

                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;

                if (command.ExecuteNonQuery() > 0)
                {
                    DBConnection.Get_Instance().Disconnect();
                    return 1;
                }
                else
                {
                    DBConnection.Get_Instance().Disconnect();
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
