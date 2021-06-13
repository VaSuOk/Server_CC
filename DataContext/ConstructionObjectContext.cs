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
    public class ConstructionObjectContext
    {
        public ConstructionObjectContext()
        {

        }

        public int CreateConstructionObject(ConstructionObject constructionObject)
        {
            if (CreateCustomer(constructionObject.customer) == 1)
            {
                try
                {
                    int i = GetCustomerID(constructionObject.customer.PIB, constructionObject.customer.Phone);
                    DBConnection.Get_Instance().Connect();
                    MySqlCommand command = new MySqlCommand(
                                "INSERT INTO `constructionobject` ( `ID_Customer`, `Region`, `Sity`, `Street`, `TypeBuilding`,`TypeRoof`,`RoofMaterial`,`WallMaterial`,`DataCreate`) " +
                                "VALUES ( @ID_Customer, @Region, @Sity, @Street, @TypeBuilding, @TypeRoof, @RoofMaterial, @WallMaterial, @DataCreate)", DBConnection.Get_Instance().connection);


                    command.Parameters.Add("@ID_Customer", MySqlDbType.Int32).Value = i;
                    command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = constructionObject.Region;
                    command.Parameters.Add("@Sity", MySqlDbType.VarChar).Value = constructionObject.Sity;
                    command.Parameters.Add("@Street", MySqlDbType.VarChar).Value = constructionObject.Street;

                    command.Parameters.Add("@TypeBuilding", MySqlDbType.VarChar).Value = constructionObject.TypeBuilding;
                    command.Parameters.Add("@TypeRoof", MySqlDbType.VarChar).Value = constructionObject.TypeRoof;
                    command.Parameters.Add("@RoofMaterial", MySqlDbType.VarChar).Value = constructionObject.RoofMaterial;
                    command.Parameters.Add("@WallMaterial", MySqlDbType.VarChar).Value = constructionObject.WallMaterial;
                    command.Parameters.Add("@DataCreate", MySqlDbType.VarChar).Value = constructionObject.DataCreate;

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
                    DBConnection.Get_Instance().Disconnect();
                    string ar = ex.Message;
                    throw;
                }
            }
            else return 0;
        }

        public int CreateCustomer(Customer customer)
        {
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "INSERT INTO `customer` ( `PIB`, `Phone`, `Email`) " +
                            "VALUES ( @PIB, @Phone, @Email)", DBConnection.Get_Instance().connection);


                command.Parameters.Add("@PIB", MySqlDbType.VarChar).Value = customer.PIB;
                command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = customer.Phone;
                command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = customer.Email;

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
                DBConnection.Get_Instance().Disconnect();
                string ar = ex.Message;
                throw;
            }
        }
        public int GetCustomerID(string PIB, string Phone)
        {
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `ID` FROM `customer` WHERE `PIB` = @PIB AND `Phone` = @Phone ", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@PIB", MySqlDbType.VarChar).Value = PIB;
                command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = Phone;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                DBConnection.Get_Instance().Disconnect();
                if (temp.Rows.Count > 0)
                {
                    DBConnection.Get_Instance().Disconnect();
                    return Convert.ToInt32(temp.Rows[0][0]);
                }

                else
                {
                    DBConnection.Get_Instance().Disconnect();
                    return 0;
                }
                    
            }
            catch
            {
                DBConnection.Get_Instance().Disconnect();
                return -1;
            }
        }
        public Customer GetCustomerByID(int ID)
        {
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `ID` FROM `customer` WHERE `ID` = @ID ", DBConnection.Get_Instance().connection);
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                DBConnection.Get_Instance().Disconnect();
                if (temp.Rows.Count > 0)
                {
                    Customer customer = new Customer();
                    customer.ID = Convert.ToInt32(temp.Rows[0][0]);
                    customer.PIB = Convert.ToString(temp.Rows[0][1]);
                    customer.Phone = Convert.ToString(temp.Rows[0][2]);
                    customer.Email = Convert.ToString(temp.Rows[0][3]);
                    return customer;
                }

                else
                {
                    DBConnection.Get_Instance().Disconnect();
                    return null;
                }

            }
            catch
            {
                DBConnection.Get_Instance().Disconnect();
                return null;
            }
        }

        public List<ConstructionObject> GetAllBObject(string Region, string Sity, string Type)
        {
            string query; bool isMany = false;
            if (Region == "none") { query = ""; }
            else { query = "`Region` = '" + Region + "' "; isMany = true; }
            if (Sity == "none") { query += ""; }
            else
            {
                if (isMany) { query += " AND  `Sity` = '" + Sity + "' "; }
                else { isMany = true; query = " `Sity` = '" + Sity + "' "; }
            }
            if (Type == "none") { query += ""; }
            else
            {
                if (isMany) { query += " AND  `TypeBuilding` = '" + Type + "' "; }
                else { isMany = true; query = " `TypeBuilding` = '"+Type+"' "; }
            }

            if (query != "") { query = "WHERE " + query; }

            List<ConstructionObject> listObject = new List<ConstructionObject>();
            DataTable temp = new DataTable();
            try
            {
                DBConnection.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `constructionobject` " + query, DBConnection.Get_Instance().connection);
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        ConstructionObject constructionObject = new ConstructionObject();
                        constructionObject.ID = Convert.ToInt32(temp.Rows[i][0]);
                        constructionObject.customer = GetCustomerByID(Convert.ToInt32(temp.Rows[i][1]));
                        constructionObject.Region = Convert.ToString(temp.Rows[i][2]);
                        constructionObject.Sity = Convert.ToString(temp.Rows[i][3]);
                        constructionObject.Street = Convert.ToString(temp.Rows[i][4]);
                        constructionObject.TypeBuilding = Convert.ToString(temp.Rows[i][5]);
                        constructionObject.TypeRoof = Convert.ToString(temp.Rows[i][6]);
                        constructionObject.RoofMaterial = Convert.ToString(temp.Rows[i][7]);
                        constructionObject.WallMaterial = Convert.ToString(temp.Rows[i][8]);
                        constructionObject.DataCreate = Convert.ToString(temp.Rows[i][9]);
                        constructionObject.Image = (byte[])(temp.Rows[0][10]);
                        listObject.Add(constructionObject);
                    }
                    DBConnection.Get_Instance().Disconnect();

                    return listObject;
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
