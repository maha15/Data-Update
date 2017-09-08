using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard.Transform
{
    class AmountReciveable
    {


        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newAR = new DataTable("dbo.AmountReciveable");

            // Add column objects to the table. 
            DataColumn Id = new DataColumn();
            Id.DataType = System.Type.GetType("System.Int32");
            Id.ColumnName = "Id";
            Id.AllowDBNull = false;
            Id.AutoIncrement = true;
            Id.AutoIncrementSeed = 233;
            newAR.Columns.Add(Id);

            DataColumn CompanyId = new DataColumn();
            CompanyId.DataType = System.Type.GetType("System.Int32");
            CompanyId.ColumnName = "CompanyId";
            newAR.Columns.Add(CompanyId);

            DataColumn OutletId = new DataColumn();
            OutletId.DataType = System.Type.GetType("System.Int32");
            OutletId.ColumnName = "OutletId";
            newAR.Columns.Add(OutletId);

            DataColumn TransId = new DataColumn();
            TransId.DataType = System.Type.GetType("System.String");
            TransId.ColumnName = "TransId";
            newAR.Columns.Add(TransId);

            DataColumn CustomerId = new DataColumn();
            CustomerId.DataType = System.Type.GetType("System.Int32");
            CustomerId.ColumnName = "CustomerId";
            newAR.Columns.Add(CustomerId);

            DataColumn CustomerName = new DataColumn();
            CustomerName.DataType = System.Type.GetType("System.String");
            CustomerName.ColumnName = "CustomerName";
            newAR.Columns.Add(CustomerName);

            DataColumn PurchaseOrderNo = new DataColumn();
            PurchaseOrderNo.DataType = System.Type.GetType("System.String");
            PurchaseOrderNo.ColumnName = "PurchaseOrderNo";
            newAR.Columns.Add(PurchaseOrderNo);

            DataColumn Type = new DataColumn();
            Type.DataType = System.Type.GetType("System.String");
            Type.ColumnName = "Type";
            newAR.Columns.Add(Type);

            DataColumn SoftwareDocumentNo = new DataColumn();
            SoftwareDocumentNo.DataType = System.Type.GetType("System.Int32");
            SoftwareDocumentNo.ColumnName = "SoftwareDocumentNo";
            newAR.Columns.Add(SoftwareDocumentNo);
            
            DataColumn DateOfInvoice = new DataColumn();
            DateOfInvoice.DataType = System.Type.GetType("System.DateTime");
            DateOfInvoice.ColumnName = "DateOfInvoice";
            newAR.Columns.Add(DateOfInvoice);


            DataColumn Total = new DataColumn();
            Total.DataType = System.Type.GetType("System.Decimal");
            Total.ColumnName = "Total";
            newAR.Columns.Add(Total);

            DataColumn PaymentTerm = new DataColumn();
            PaymentTerm.DataType = System.Type.GetType("System.String");
            PaymentTerm.ColumnName = "PaymentTerm";
            newAR.Columns.Add(PaymentTerm);

            DataColumn InitDate = new DataColumn();
            InitDate.DataType = System.Type.GetType("System.DateTime");
            InitDate.ColumnName = "InitDate";
            newAR.Columns.Add(InitDate);

            DataColumn DueDate = new DataColumn();
            DueDate.DataType = System.Type.GetType("System.DateTime");
            DueDate.ColumnName = "DueDate";
            newAR.Columns.Add(DueDate);

            DataColumn Action = new DataColumn();
            Action.DataType = System.Type.GetType("System.String");
            Action.ColumnName = "Action";
            newAR.Columns.Add(Action);

            DataColumn Status = new DataColumn();
            Status.DataType = System.Type.GetType("System.String");
            Status.ColumnName = "Status";
            newAR.Columns.Add(Status);

            DataColumn Notes = new DataColumn();
            Notes.DataType = System.Type.GetType("System.String");
            Notes.ColumnName = "Notes";
            newAR.Columns.Add(Notes);

            DataColumn Date = new DataColumn();
            Date.DataType = System.Type.GetType("System.DateTime");
            Date.ColumnName = "Date";
            newAR.Columns.Add(Date);

            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete(); // col names could have been taken from here
            fileTable.AcceptChanges();
            int rowno = 2;
            foreach (DataRow filerow in fileTable.Rows)
            {
                var filecol = filerow.ItemArray;

                try
                {
                    // Add some new rows to the collection. 
                    DataRow row = newAR.NewRow();
                    row["Id"] = 0; // Dont care situation since auto increment in db
                    row["CompanyId"] = GlobalVar.companyID;
                    row["OutletId"] = GlobalVar.outletID;
                    row["TransId"] = Get(filerow[4]);
                    row["CustomerId"] = Get(filerow[5]);
                    row["CustomerName"] = Get(filerow[6]);
                    row["PurchaseOrderNo"] = Get(filerow[7]);
                    row["Type"] = Get(filerow[8]);
                    row["SoftwareDocumentNo"] = Get(filerow[9]);
                    row["DateOfInvoice"] = GetDate(filerow[10]);
                    row["Total"] = Get(filerow[11]);
                    row["PaymentTerm"] = Get(filerow[12]); 
                    row["InitDate"] = GetDate(filerow[10]); ;
                    row["DueDate"] = GetDate(filerow[13]);
                    row["Action"] = DBNull.Value;
                    row["Status"] = DBNull.Value;
                    row["Notes"] = DBNull.Value;
                    row["Date"] = DBNull.Value;

                    newAR.Rows.Add(row);
                    newAR.AcceptChanges();
                    rowno++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in row no:" + rowno + "Message" + e.Message);
                    rowno++;
                }

            }
            // Return the new DataTable. 
            return newAR;
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
