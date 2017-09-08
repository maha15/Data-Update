using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard.Transform
{
    class Expense
    {


        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newExpense = new DataTable("dbo.Expense");

            // Add column objects to the table. 
            DataColumn company_id = new DataColumn();
            company_id.DataType = System.Type.GetType("System.Int32");
            company_id.ColumnName = "company_id";
            company_id.AllowDBNull = false;
            newExpense.Columns.Add(company_id);

            DataColumn expense_id = new DataColumn();
            expense_id.DataType = System.Type.GetType("System.Int32");
            expense_id.ColumnName = "expense_id";
            expense_id.AllowDBNull = false;
            expense_id.AutoIncrement = true;
            expense_id.AutoIncrementSeed = 58; 
            newExpense.Columns.Add(expense_id);

            DataColumn outlet_id = new DataColumn();
            outlet_id.DataType = System.Type.GetType("System.Int32");
            outlet_id.ColumnName = "outlet_id";
            outlet_id.AllowDBNull = false;
            newExpense.Columns.Add(outlet_id);

            DataColumn account_name = new DataColumn();
            account_name.DataType = System.Type.GetType("System.String");
            account_name.ColumnName = "account_name";
            account_name.AllowDBNull = false;
            newExpense.Columns.Add(account_name);

            DataColumn expense_cat = new DataColumn();
            expense_cat.DataType = System.Type.GetType("System.String");
            expense_cat.ColumnName = "expense_cat";
            expense_cat.AllowDBNull = false;
            newExpense.Columns.Add(expense_cat);

            DataColumn expanse_sub_cat = new DataColumn();
            expanse_sub_cat.DataType = System.Type.GetType("System.String");
            expanse_sub_cat.ColumnName = "expanse_sub_cat";
            newExpense.Columns.Add(expanse_sub_cat);

            DataColumn expense_date = new DataColumn();
            expense_date.DataType = System.Type.GetType("System.DateTime");
            expense_date.ColumnName = "expense_date";
            expense_date.AllowDBNull = false;
            newExpense.Columns.Add(expense_date);

            DataColumn cost_center = new DataColumn();
            cost_center.DataType = System.Type.GetType("System.String");
            cost_center.ColumnName = "cost_center";
            newExpense.Columns.Add(cost_center);

            DataColumn expense_location = new DataColumn();
            expense_location.DataType = System.Type.GetType("System.String");
            expense_location.ColumnName = "expense_location";
            newExpense.Columns.Add(expense_location);

            DataColumn cost = new DataColumn();
            cost.DataType = System.Type.GetType("System.Decimal");
            cost.ColumnName = "cost";
            cost.AllowDBNull = false;
            newExpense.Columns.Add(cost);


            DataColumn tax = new DataColumn();
            tax.DataType = System.Type.GetType("System.Decimal");
            tax.ColumnName = "tax";
            newExpense.Columns.Add(tax);

            DataColumn others = new DataColumn();
            others.DataType = System.Type.GetType("System.Decimal");
            others.ColumnName = "others";
            newExpense.Columns.Add(others);
            
            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete(); // col names could have been taken from here
            fileTable.Rows[2].Delete();
            fileTable.Rows[3].Delete();
            fileTable.Rows[4].Delete();
            fileTable.AcceptChanges();
            int rowno = 6;
            foreach (DataRow filerow in fileTable.Rows)
            {
                var filecol = filerow.ItemArray;

                try
                {
                    // Add some new rows to the collection. 
                    DataRow row = newExpense.NewRow();
                    row["company_id"] = GlobalVar.companyID;
                    row["expense_id"] = rowno; // Don't care since AutoIncrement
                    row["outlet_id"] = GlobalVar.outletID;
                    row["account_name"] = GetAccountName(filerow[3], "account_name"); 
                    row["expense_cat"] = GetCategory(filerow[4], "expense_cat"); // ????? ASK AIMEN UPDATE AS NULLABLE OR PUT DUMMY VALUE???
                    row["expanse_sub_cat"] = GetSubCategory(filerow[2], "expanse_sub_cat"); // Excel Col "G/L account" no other relevant col
                    row["expense_date"] = GetDate(filerow[5]);
                    row["cost_center"] = GetCostCenter(filerow[6], "cost_center");// Get(filerow[9]); // Expects int in DB - given string in excel
                    row["expense_location"] = GetExpLoc(filerow[10], "expense_location");
                    row["cost"] = GetCost(filerow[12], "cost");
                    row["tax"] = Get(filerow[13], "tax");
                    row["others"] = DBNull.Value;
                   

                    newExpense.Rows.Add(row);
                    newExpense.AcceptChanges();

                   // Console.WriteLine("******************** Inserted Row: " + rowno + "*************************");
                    rowno++;
                 //   Console.Read();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in row no:" + rowno + "Message \n" + e.Message);
                    rowno++;
                   // Console.ReadLine();
                }

            }
            // Return the new DataTable. 
            return newExpense;
        }

        private object GetCost(object v1, string v2)
        {
            return Math.Abs(Convert.ToInt32(v1));
        }

        private object GetExpLoc(object v1, string v2)
        {
            return GlobalVar.Truncate(v1.ToString(), 30);
        }

        private object GetCostCenter(object v1, string v2)
        {
            return GlobalVar.Truncate(v1.ToString(), 30);
        }

        private object GetAccountName(object v1, string v2)
        {
            string accName = v1.ToString();
            return GlobalVar.Truncate(accName, 100);
        }

        private object GetSubCategory(object v1, string v2)
        {
            return GlobalVar.Truncate("Sub Category A", 5);
       
            
        }

        private object GetCategory(object v1, string v2)
        {
            return GlobalVar.Truncate("Category A", 100);
        }

        private object GetInt(object v)
        {

            if (v is Int32)
            {
                return (int)v;
            }
            else
                throw new Exception("- in this row");
        }

        private object GetCustomerID(object v)
        {
            //ADD IN DB AND INCLUDE VALUE HERE 
            // check if null or empty assign empty
            string inputcust = v.ToString();
            if (inputcust == "" || inputcust == null)
            {
                return DBNull.Value;
            }
            //check if name exists in db -> Does get the id Else insert in DB and get ID
            Database db = new Database();
            int customer_id = db.getcustId(inputcust);
            if (customer_id == 0)
            {
                //insert customer in databse
                customer_id = db.insertcustomer(inputcust);
            }
            return customer_id;
        }

        private object GetTCGS(object v)
        {
            string withBrack = v.ToString();
            withBrack.Replace("(", ""); // never called
            withBrack.Replace(")", "");
            //  Console.WriteLine("COGS: " + withBrack);

            return Convert.ToInt32(withBrack);
        }

        private object GetProductId(object v)
        {
            Database db = new Database();
            int prodId = (int)db.getproductid(v.ToString());
         //   Console.WriteLine("Product ID: " + prodId);
            return prodId;
        }

        private object GetDate(object v)
        {
            DateTime DateString = (DateTime)v;
            DateTime dt  = new DateTime(DateString.Year, DateString.Month, 1);
            // IFormatProvider culture = new CultureInfo("en-US", true);
            //DateTime dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd", culture);
            var sqlFormattedDate = dt.Date.ToString("yyyy-MM-dd");
           // Console.WriteLine("Date" + sqlFormattedDate);
            return sqlFormattedDate;
        }

        private object Get(object v, String fieldName)
        {
          //  Console.WriteLine("Col  " +fieldName+ ": " + v + "\t DataType: " + v.GetType().ToString());
            return v;
        }



    }
}
