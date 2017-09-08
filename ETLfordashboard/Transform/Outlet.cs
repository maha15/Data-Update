using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard.Transform
{
    class Outlet
    {



        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newOutlet = new DataTable("dbo.Outlet");

            // Add  column objects to the table. 
            DataColumn outlet_id = new DataColumn();
            outlet_id.DataType = System.Type.GetType("System.Int32");
            outlet_id.ColumnName = "outlet_id";
            outlet_id.AllowDBNull = false;
            newOutlet.Columns.Add(outlet_id);

            DataColumn company_id = new DataColumn();
            company_id.DataType = System.Type.GetType("System.Int32");
            company_id.ColumnName = "company_id";
            company_id.AllowDBNull = false;
            newOutlet.Columns.Add(company_id);

            DataColumn Name = new DataColumn();
            Name.DataType = System.Type.GetType("System.String");
            Name.ColumnName = "Name";
            newOutlet.Columns.Add(Name);

            DataColumn location = new DataColumn();
            location.DataType = System.Type.GetType("System.String");
            location.ColumnName = "location";
            newOutlet.Columns.Add(location);

            DataColumn country = new DataColumn();
            country.DataType = System.Type.GetType("System.String");
            country.ColumnName = "country";
            newOutlet.Columns.Add(country);

            DataColumn state = new DataColumn();
            state.DataType = System.Type.GetType("System.String");
            state.ColumnName = "state";
            newOutlet.Columns.Add(state);


            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete();
            fileTable.Rows[2].Delete(); // col names could have been taken from here
            fileTable.AcceptChanges();
            foreach (DataRow filerow in fileTable.Rows)
            {
                var filecol = filerow.ItemArray;


                // Add some new rows to the collection. 
                DataRow row = newOutlet.NewRow();
                Console.WriteLine("ID: \t" + filerow[0].GetType().ToString());
                row["outlet_id"] = GetOutletId(filerow[0]);
                row["company_id"] = GlobalVar.companyID;
                row["Name"] = GetName(filerow[1]);
                row["location"] = GetLoc(filerow[3]);
                row["country"] = GetCountry(filerow[2]);
                row["state"] = GetState(filerow[4]);


                newOutlet.Rows.Add(row);
                newOutlet.AcceptChanges();
                break; // one outlet in this dataset loopin twice. trim extra rows

            }
            // Return the new DataTable. 
            return newOutlet;
        }

        private object GetLoc(object v)
        {
            Console.WriteLine("Loc: " + v + "\t DataType: " +v.GetType().ToString());
            return v;
        }

        private object GetCountry(object v)
        {
            Console.WriteLine("Country: " + v + "\t DataType: "+ v.GetType().ToString());
            return v;
        }

        private object GetState(object v)
        {
             Console.WriteLine("State: " + v + "\t DataType: " +v.GetType().ToString());
            return v;
        }

        private object GetOutletId(object v)
        {
            Console.WriteLine("Outlet Id: " + v + "\t DataType: "+ v.GetType().ToString());
            return v;
        }

        private object GetAddress(object v)
        {
            Console.WriteLine("Add:" + v.GetType().ToString());
            return v;
        }

        private object GetName(object v)
        {
            Console.WriteLine("Name: " + v + "\t Data Type:" + v.GetType().ToString());
            return v;
        }

    }


}
