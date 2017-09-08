using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ETLfordashboard
{
  
    public class Database
    {
        string connectionstring = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=NuagePulse;Integrated Security=True";
        SqlConnection myConnection;

        public Database()
        {
        }
        private void openConec()
        {
           
            try
            {
                //Replace with your server credentials/info
                myConnection = new SqlConnection(connectionstring);
                myConnection.Open();
              /*  for (int i = 0; i <= x.Count - 4; i += 4)//Implement by 3...
                {
                    //Replace table_name with your table name, and Column1 with your column names (replace for all).
                    SqlCommand myCommand = new SqlCommand("INSERT INTO table_name (Column1, Column2, Column3, Column4) " +
                                         String.Format("Values ('{0}','{1}','{2}','{3}')", x[i], x[i + 1], x[i + 2], x[i + 3]), myConnection);
                    myCommand.ExecuteNonQuery();
                }*/

            }
            catch (Exception e) { Console.WriteLine(e.ToString());
                CloseCon();
            }
          
        }

        private void CloseCon()
        {
            try { myConnection.Close(); }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public Boolean insertrows(DataTable insertTable)
        {

            openConec();
            getrowcount(insertTable.TableName);



            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(myConnection))
            {
                bulkCopy.DestinationTableName = insertTable.TableName;
                   
                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(insertTable);
                    getrowcount(insertTable.TableName);
                    CloseCon();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    CloseCon();
                    return false;

                }


            }

        }

        private void getrowcount(string tableName)
        {
            SqlCommand commandRowCount = new SqlCommand("SELECT COUNT(*) FROM " + tableName  , myConnection);
            long countStart = System.Convert.ToInt32( commandRowCount.ExecuteScalar());
            Console.WriteLine("row count = {0}", countStart);
        }

        public object getproductid(string productName)
        {
            try
            {
                openConec();
                SqlCommand commandgetproductid = new SqlCommand("SELECT Id FROM dbo.ProductCategory where Name = '" + productName.Trim() + "'", myConnection);
                using (SqlDataReader reader = commandgetproductid.ExecuteReader())
                {
                    object valret = 0;
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                             valret = reader.GetValue(0);
                        }
                       
                    }
                    CloseCon();
                    return valret;
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Product ID: " + e.Message);
                CloseCon();
                return 0;
            }
        }

        internal int getcustId(string inputcust)
        {
            try
            {
                openConec();
                SqlCommand commandgetcusttid = new SqlCommand("SELECT customer_id FROM dbo.Customer where Name = '" + inputcust.Trim() + "'", myConnection);
                using (SqlDataReader reader = commandgetcusttid.ExecuteReader())
                {
                    object valret = 0;
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            valret = reader.GetValue(0);
                        }

                    }
                    CloseCon();
                    return (int)valret;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Get Customer ID: " + e.Message);
                CloseCon();
                return 0;
            }
        }

        internal int insertcustomer(string inputcust)
        {
            try
            {
                openConec();
                SqlCommand insertcustomer = new SqlCommand("INSERT INTO dbo.Customer VALUES (" + GlobalVar.companyID + ", 1 , '" +inputcust+"' )" , myConnection);
                Console.WriteLine("INSERT INTO dbo.Customer VALUES (" + GlobalVar.companyID + ", 1 , '" + inputcust + "' )");
                using (SqlDataReader reader = insertcustomer.ExecuteReader())
                {
                    object valret = 0;
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            valret = reader.GetValue(0);
                        }

                    }
                    CloseCon();
                    return (int)valret;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Get Customer ID: " + e.Message);
                CloseCon();
                return 0;
            }
        }
    }
}
