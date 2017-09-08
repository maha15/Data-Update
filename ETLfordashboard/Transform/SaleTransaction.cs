using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard.Transform
{
    class SaleTransaction
    {
        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newSR = new DataTable("dbo.SaleTransaction");

            // Add column objects to the table. 
            DataColumn CompanyId = new DataColumn();
            CompanyId.DataType = System.Type.GetType("System.Int32");
            CompanyId.ColumnName = "CompanyId";
            CompanyId.AllowDBNull = false;
            newSR.Columns.Add(CompanyId);

            DataColumn TransactionId = new DataColumn();
            TransactionId.DataType = System.Type.GetType("System.Int32");
            TransactionId.ColumnName = "TransactionId";
            TransactionId.AllowDBNull = false;
            newSR.Columns.Add(TransactionId);

            DataColumn OutletId = new DataColumn();
            OutletId.DataType = System.Type.GetType("System.Int32");
            OutletId.ColumnName = "OutletId";
            newSR.Columns.Add(OutletId);

            DataColumn TransactionDate = new DataColumn();
            TransactionDate.DataType = System.Type.GetType("System.DateTime");
            TransactionDate.ColumnName = "TransactionDate";
            TransactionDate.AllowDBNull = false;
            newSR.Columns.Add(TransactionDate);

            DataColumn ProductId = new DataColumn();
            ProductId.DataType = System.Type.GetType("System.Int32");
            ProductId.ColumnName = "ProductId";
            ProductId.AllowDBNull = false;
            newSR.Columns.Add(ProductId);

            DataColumn InvoiceId = new DataColumn();
            InvoiceId.DataType = System.Type.GetType("System.String");
            InvoiceId.ColumnName = "InvoiceId";
            newSR.Columns.Add(InvoiceId);

            DataColumn CustomerId = new DataColumn();
            CustomerId.DataType = System.Type.GetType("System.Int32");
            CustomerId.ColumnName = "CustomerId";
            newSR.Columns.Add(CustomerId);

            DataColumn BillToNumber = new DataColumn();
            BillToNumber.DataType = System.Type.GetType("System.Int32");
            BillToNumber.ColumnName = "BillToNumber";
            newSR.Columns.Add(BillToNumber);

            DataColumn BillToPartyName = new DataColumn();
            BillToPartyName.DataType = System.Type.GetType("System.String");
            BillToPartyName.ColumnName = "BillToPartyName";
            newSR.Columns.Add(BillToPartyName);

            DataColumn StateCode = new DataColumn();
            StateCode.DataType = System.Type.GetType("System.String");
            StateCode.ColumnName = "StateCode";
            newSR.Columns.Add(StateCode);

            DataColumn Country = new DataColumn();
            Country.DataType = System.Type.GetType("System.String");
            Country.ColumnName = "Country";
            newSR.Columns.Add(Country);

            DataColumn ShipToState = new DataColumn();
            ShipToState.DataType = System.Type.GetType("System.String");
            ShipToState.ColumnName = "ShipToState";
            newSR.Columns.Add(ShipToState);

            DataColumn TotalSales = new DataColumn();
            TotalSales.DataType = System.Type.GetType("System.Decimal");
            TotalSales.ColumnName = "TotalSales";
            TotalSales.AllowDBNull = false;
            newSR.Columns.Add(TotalSales);


            DataColumn Discount = new DataColumn();
            Discount.DataType = System.Type.GetType("System.Decimal");
            Discount.ColumnName = "Discount";
            newSR.Columns.Add(Discount);

            DataColumn NetSales = new DataColumn();
            NetSales.DataType = System.Type.GetType("System.Decimal");
            NetSales.ColumnName = "NetSales";
            newSR.Columns.Add(NetSales);

            DataColumn Commissions = new DataColumn();
            Commissions.DataType = System.Type.GetType("System.Decimal");
            Commissions.ColumnName = "Commissions";
            newSR.Columns.Add(Commissions);

            DataColumn TotalCogs = new DataColumn();
            TotalCogs.DataType = System.Type.GetType("System.Decimal");
            TotalCogs.ColumnName = "TotalCogs";
            newSR.Columns.Add(TotalCogs);

            DataColumn GrossMargin = new DataColumn();
            GrossMargin.DataType = System.Type.GetType("System.Decimal");
            GrossMargin.ColumnName = "GrossMargin";
            newSR.Columns.Add(GrossMargin);

            DataColumn GrossMarginRate = new DataColumn();
            GrossMarginRate.DataType = System.Type.GetType("System.Double");
            GrossMarginRate.ColumnName = "GrossMarginRate";
            newSR.Columns.Add(GrossMarginRate);
            

            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete(); // col names could have been taken from here
            fileTable.Rows[2].Delete();
            fileTable.Rows[3].Delete();
            fileTable.AcceptChanges();
            int rowno = 6;
            foreach (DataRow filerow in fileTable.Rows)
            {
                var filecol = filerow.ItemArray;

                try
                {
                    // Add some new rows to the collection. 
                    DataRow row = newSR.NewRow();
                    row["CompanyId"] = GlobalVar.companyID;
                    row["TransactionId"] = rowno;  //Transaction ID mapped to invoice no 
                    row["OutletId"] = GlobalVar.outletID;
                    row["TransactionDate"] = GetDate(filerow[2]); // Invoice Date in Excel Mapped to Transaction Date here
                    row["ProductId"] = GetProductId(filerow[7]) ;
                    row["InvoiceId"] = Get(filerow[3]); 
                    row["CustomerId"] = GetCustomerID(filerow[8]);// insert in customer table in DB
                    row["BillToNumber"] = DBNull.Value;//TODO: NULL Get(filerow[9]); // Expects int in DB - given string in excel
                    row["BillToPartyName"] = Get(filerow[10]); 
                    row["StateCode"] = Get(filerow[12]); 
                    row["Country"] = Get(filerow[11]); 
                    row["ShipToState"] = Get(filerow[13]);
                    row["TotalSales"] = GetTotalSales(filerow[15]); 
                    row["Discount"] = Get(filerow[16]);
                    row["NetSales"] = Get(filerow[18]);
                    row["Commissions"] = Get(filerow[17]); // GetInt Comission has int in file in place of - the PK is null in such rows and would be catched before hand
                    row["TotalCogs"] = GetTCGS(filerow[23]); 
                    row["GrossMargin"] = Get(filerow[24]);
                    row["GrossMarginRate"] = Get(filerow[25]);

                    newSR.Rows.Add(row);
                    newSR.AcceptChanges();
                    Console.WriteLine("--------------------------** Row inserted: " + rowno + " **-----------------------");

                    rowno++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in row no:" + rowno + "Message \n" + e.Message);
                    rowno++;
                    Console.ReadLine();
                 
                }

            }
            // Return the new DataTable. 
            return newSR;
        }

        private object GetTotalSales(object v)
        {
            Console.WriteLine("TS Data: " + v + "\t DataType: " + v.GetType().ToString());
            if ( v == DBNull.Value)
            {
                return 0;
            }
            return v;
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
            if(customer_id == 0)
            {
                //insert customer in databse
                customer_id = db.insertcustomer(inputcust);
            }
            return customer_id;
        }

        private object GetTCGS(object v)
        {
            Console.WriteLine("Col Data CGS: " + Math.Abs(Convert.ToInt32(v)));
            return Math.Abs(Convert.ToInt32(v));
        }

        private object GetProductId(object v)
        {
            Database db = new Database();
            int prodId = (int)db.getproductid(v.ToString());
            Console.WriteLine("Product ID: " + prodId);
            return prodId;
        }

        private object GetDate(object v)
        {
            DateTime DateString = (DateTime)v;
            // IFormatProvider culture = new CultureInfo("en-US", true);
            //DateTime dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd", culture);
            var sqlFormattedDate = DateString.Date.ToString("yyyy-MM-dd");
            Console.WriteLine("Date" + sqlFormattedDate);
            return sqlFormattedDate;
        }

        private object Get(object v)
        {
            Console.WriteLine("Col Data: " + v + "\t DataType: " + v.GetType().ToString());
            return v;
        }

    }
}
