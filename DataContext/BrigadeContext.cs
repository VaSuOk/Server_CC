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
    public class BrigadeContext
    {
        public static int CreateBrigade(Brigade brigade)
        {
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "INSERT INTO `brigade` ( `Name`, `WorkRegion`, `WorkStage`, `Amount`, `user1`,`user2`,`user3`,`user4`,`user5`,`user6`,`user7`,`user8`) " +
                            "VALUES ( @Name, @WorkRegion, @WorkStage, @Amount, @user1, @user2, @user3, @user4, @user5, @user6, @user7, @user8)", DBConnection.Get_Instance().connection);


                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = brigade.Name;
                command.Parameters.Add("@WorkRegion", MySqlDbType.VarChar).Value = brigade.WorkRegion;
                command.Parameters.Add("@WorkStage", MySqlDbType.VarChar).Value = brigade.WorkStage;
                command.Parameters.Add("@Amount", MySqlDbType.VarChar).Value = brigade.Amount;

                command.Parameters.Add("@user1", MySqlDbType.Int32).Value = brigade.ID_user1;
                command.Parameters.Add("@user2", MySqlDbType.Int32).Value = brigade.ID_user2;
                command.Parameters.Add("@user3", MySqlDbType.Int32).Value = brigade.ID_user3;
                command.Parameters.Add("@user4", MySqlDbType.Int32).Value = brigade.ID_user4;
                command.Parameters.Add("@user5", MySqlDbType.Int32).Value = brigade.ID_user5;
                command.Parameters.Add("@user6", MySqlDbType.Int32).Value = brigade.ID_user6;
                command.Parameters.Add("@user7", MySqlDbType.Int32).Value = brigade.ID_user7;
                command.Parameters.Add("@user8", MySqlDbType.Int32).Value = brigade.ID_user8;

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

        public static List<Brigade> GetBrigade(string Region, string Stage, bool isWork)
        {
            string query; bool isMany = false;
            if (Region == "none") { query = ""; }
            else { query = "`WorkRegion` = '" + Region + "'"; isMany = true; }
            if (Stage == "none") { query += ""; }
            else
            {
                if (isMany) { query += " AND  `WorkStage` = '" + Stage + "'"; }
                else { isMany = true; query = " `WorkStage` = '" + Stage + "'"; }
            }
            if (!isWork) { query += ""; }
            else
            {
                if (isMany) { query += " AND  `TaskID` > '0'"; }
                else { isMany = true; query = " `TaskID` > '0'"; }
            }

            if (query != "") { query = "WHERE " + query; }

            List<Brigade> listBrigade = new List<Brigade>();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `brigade` " + query, DBConnection.Get_Instance().connection);
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Brigade brigade = new Brigade();
                        brigade.ID = Convert.ToInt32(temp.Rows[i][0]);
                        brigade.Name = Convert.ToString(temp.Rows[i][1]);
                        brigade.WorkRegion = Convert.ToString(temp.Rows[i][2]);
                        brigade.WorkStage = Convert.ToString(temp.Rows[i][3]);
                        brigade.Amount = Convert.ToInt32(temp.Rows[i][4]);

                        brigade.ID_user1 = Convert.ToInt32(temp.Rows[i][5]);
                        brigade.ID_user2 = Convert.ToInt32(temp.Rows[i][6]);
                        brigade.ID_user3 = Convert.ToInt32(temp.Rows[i][7]);
                        brigade.ID_user4 = Convert.ToInt32(temp.Rows[i][8]);
                        brigade.ID_user5 = Convert.ToInt32(temp.Rows[i][9]);
                        brigade.ID_user6 = Convert.ToInt32(temp.Rows[i][10]);
                        brigade.ID_user7 = Convert.ToInt32(temp.Rows[i][11]);
                        brigade.ID_user8 = Convert.ToInt32(temp.Rows[i][12]);
                        brigade.TaskID = Convert.ToInt32(temp.Rows[i][13]);

                        listBrigade.Add(brigade);
                    }
                    DBConnection.Get_Instance().Disconnect();

                    return listBrigade;
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
    }
}
