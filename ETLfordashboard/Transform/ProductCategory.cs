using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard.Transform
{
    
    class ProductCategory
    {
        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newUser = new DataTable("dbo.ProductCategory");

            // Add  column objects to the table. 
            DataColumn Id = new DataColumn();
            Id.DataType = System.Type.GetType("System.Int32");
            Id.ColumnName = "Id";
            Id.AllowDBNull = false;
            newUser.Columns.Add(Id);

            DataColumn CompanyId = new DataColumn();
            CompanyId.DataType = System.Type.GetType("System.Int32");
            CompanyId.ColumnName = "CompanyId";
            CompanyId.AllowDBNull = false;
            newUser.Columns.Add(CompanyId);

            DataColumn Name = new DataColumn();
            Name.DataType = System.Type.GetType("System.String");
            Name.ColumnName = "Name";
            newUser.Columns.Add(Name);

          


            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete();
            fileTable.Rows[2].Delete(); // col names could have been taken from here
            fileTable.AcceptChanges();
            int rowno = 3;
            foreach (DataRow filerow in fileTable.Rows)
            {
                var filecol = filerow.ItemArray;

                try
                {
                    // Add some new rows to the collection. 
                    DataRow row = newUser.NewRow();
                    row["Id"] = Get(filerow[0]);
                    row["CompanyId"] = GlobalVar.companyID;
                    row["Name"] = Get(filerow[1]);
                   

                    newUser.Rows.Add(row);
                    newUser.AcceptChanges();
                    rowno++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in row no:" + rowno + "Message" + e.Message);
                    rowno++;
                }

            }
            // Return the new DataTable. 
            return newUser;
        }

        private object Get(object v)
        {
            Console.WriteLine("Col Data: " + v + "\t DataType: " + v.GetType().ToString());
            return v;
        }
    }
}
