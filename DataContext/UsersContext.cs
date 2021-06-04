using Microsoft.AspNetCore.Mvc;
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
    public class UsersContext
    {
        public UsersContext()
        {

        }

        #region Methods
        public int CreateUser(User user)
        {
            try { 
            DBConnection.Get_Instance().Connect();
            MySqlCommand command = new MySqlCommand(
                        "INSERT INTO `user` ( `Name`, `Surname`, `Email`, `Phone`, `Region`,`Sity`,`Image`,`Birthday`) " +
                        "VALUES ( @Name, @Surname, @Email, @Phone, @Region, @Sity, @Image, @Birthday)", DBConnection.Get_Instance().connection);


            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
            command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = user.Surname;
            command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = user.Email;
            command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = user.Phone;

            command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = user.Region;
            command.Parameters.Add("@Sity", MySqlDbType.VarChar).Value = user.Sity;
            command.Parameters.Add("@Image", MySqlDbType.MediumBlob).Value = user.UserImage;
            command.Parameters.Add("@Birthday", MySqlDbType.VarChar).Value = user.Birthday;

            if (command.ExecuteNonQuery() > 0)
                return 1;
            else
                return 0;
            }
            catch (Exception ex)
            {
                string ar = ex.Message;
                throw;
            }
        }
        public int GetUserID(string Name, string Phone)
        {
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `ID` FROM `user` WHERE `Name` = @Name AND `Phone` = @Phone ", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name;
                command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = Phone;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                DBConnection.Get_Instance().Disconnect();
                if (temp.Rows.Count > 0)
                    return Convert.ToInt32(temp.Rows[0][0]);
                else
                    return 0;
            }
            catch
            {
                DBConnection.Get_Instance().Disconnect();
                return -1;
            }
        }
        public User GetUserByID(int ID)
        {
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `ID` = @ID", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    adapter.Fill(temp);

                    User user = new User(); user.id = Convert.ToUInt32(temp.Rows[0][0]);
                    user.id = Convert.ToUInt32(temp.Rows[0][0]);
                    user.Name = Convert.ToString(temp.Rows[0][1]);
                    user.Surname = Convert.ToString(temp.Rows[0][2]); user.Email = Convert.ToString(temp.Rows[0][3]);
                    user.Phone = Convert.ToString(temp.Rows[0][4]); user.Region = Convert.ToString(temp.Rows[0][5]);
                    user.Sity = Convert.ToString(temp.Rows[0][6]); user.UserImage = (byte[])(temp.Rows[0][7]); 
                    user.Birthday = Convert.ToString(temp.Rows[0][8]);

                    DBConnection.Get_Instance().Disconnect();

                    return user;
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
        public int UpdateUser(User user)
        {
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "UPDATE `user` SET `Name` = @Name, `Surname` = @Surname, `Email` = @Email, `Phone` = @Phone, `Region` = @Region, `Sity` = @Sity, `Image` = @Image, `Age` = @Age " +
                            "WHERE `ID` = @ID", DBConnection.Get_Instance().connection);

                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = user.id;
                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
                command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = user.Surname;
                command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = user.Email;
                command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = user.Phone;
                command.Parameters.Add("@Sity", MySqlDbType.VarChar).Value = user.Sity;
                command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = user.Region;
                command.Parameters.Add("@Image", MySqlDbType.MediumBlob).Value = user.UserImage;


                

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
            catch(Exception ex)
            {
                string tmp = ex.Message;
                DBConnection.Get_Instance().Disconnect();
                return -1;
            }
        }
        #endregion
    }
}
