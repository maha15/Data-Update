using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace ETLfordashboard.Transform
{
    class CashProjection
    {

        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newCP = new DataTable("dbo.CashProjection");

            // Add column objects to the table. 
            DataColumn Id = new DataColumn();
            Id.DataType = System.Type.GetType("System.Int32");
            Id.ColumnName = "Id";
            Id.AllowDBNull = false;
            Id.AutoIncrement = true;
            Id.AutoIncrementSeed = 234;
            newCP.Columns.Add(Id);

            DataColumn CompanyId = new DataColumn();
            CompanyId.DataType = System.Type.GetType("System.Int32");
            CompanyId.ColumnName = "CompanyId";
            CompanyId.AllowDBNull = false;
            newCP.Columns.Add(CompanyId);

            DataColumn OutletId = new DataColumn();
            OutletId.DataType = System.Type.GetType("System.Int32");
            OutletId.ColumnName = "OutletId";
            OutletId.AllowDBNull = false;
            newCP.Columns.Add(OutletId);

            DataColumn Date = new DataColumn();
            Date.DataType = System.Type.GetType("System.DateTime");
            Date.ColumnName = "Date";
            newCP.Columns.Add(Date);

            DataColumn NetCashBalance = new DataColumn();
            NetCashBalance.DataType = System.Type.GetType("System.Decimal");
            NetCashBalance.ColumnName = "NetCashBalance";
            newCP.Columns.Add(NetCashBalance);

            
            for (int row = 0; row < fileTable.Rows.Count; row++)
            {
                for (int col = 33; col < 33 + 32; col++)
                {

                   // Console.Write(fileTable.Rows[row][col] + "\t");
                    
                       try {
                           DateTime result;
                           if (DateTime.TryParse(fileTable.Rows[row][col].ToString(), out result)) { 

                             
                                // Add some new rows to the collection. 
                                DataRow enterrow = newCP.NewRow();
                                enterrow["Id"] = 0; // Dont care situation since  auto increment in db
                                enterrow["CompanyId"] = GlobalVar.companyID;
                                enterrow["OutletId"] = GlobalVar.outletID;

                                Console.WriteLine("Fetched Date from Row" + row + "col: " + col);
                                enterrow["Date"] = GetDate(fileTable.Rows[row][col]);

                                 if (row == 136)
                                     enterrow["NetCashBalance"] = Get(fileTable.Rows[(row + 17)].ItemArray[col]);
                                 else
                                    enterrow["NetCashBalance"] = Get(fileTable.Rows[(row + 18)].ItemArray[col]);

                                newCP.Rows.Add(enterrow);
                                newCP.AcceptChanges();
                                
                               // Console.ReadLine();
                              

                           }
                       }
                       catch (Exception e)
                       {
                           Console.WriteLine("Exception in row no:" + row + "Message" + e.Message);
                       // Console.ReadLine();
                      //  Console.ReadLine();
                    }
                      

                }
              //  Console.WriteLine();
             //   Console.ReadLine();
                    

                    
                }

            
            // Return the new DataTable. 
            return newCP;
        }

        private bool IsValidDate(object v)
        {
            if (v is DateTime)
                return true;
            else
                return false;
        }

        private object GetDate(object v)
        {
            try
            {
                DateTime DateString = (DateTime)v;
                // IFormatProvider culture = new CultureInfo("en-US", true);
                //DateTime dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd", culture);
                var sqlFormattedDate = DateString.Date.ToString("yyyy-MM-dd");
                Console.WriteLine("Date: " + sqlFormattedDate );
                return sqlFormattedDate;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in converting date: " + e.Message);
                return null;
            }
        }

        private object Get(object v)
        {
             Console.Write("Data: " + v + "\t DataType: " + v.GetType().ToString());
            return v;
        }
    }
}
