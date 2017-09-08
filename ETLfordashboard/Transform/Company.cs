using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ETLfordashboard
{
    class Company
    {

        
        public Company()
        {
            
        }
        

        public  DataTable MakeTable( DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newCompany = new DataTable("dbo.Company");

            // Add  column objects to the table. 
            DataColumn company_id = new DataColumn();
            company_id.DataType = System.Type.GetType("System.Int32");
            company_id.ColumnName = "company_id";
            newCompany.Columns.Add(company_id);

            DataColumn name = new DataColumn();
            name.DataType = System.Type.GetType("System.String");
            name.ColumnName = "name";
            newCompany.Columns.Add(name);

            DataColumn Address = new DataColumn();
            Address.DataType = System.Type.GetType("System.String");
            Address.ColumnName = "Address";
            newCompany.Columns.Add(Address);

            DataColumn TaxRefNo = new DataColumn();
            TaxRefNo.DataType = System.Type.GetType("System.String");
            TaxRefNo.ColumnName = "TaxRefNo";
            newCompany.Columns.Add(TaxRefNo);

            DataColumn SalesTaxRefNo = new DataColumn();
            SalesTaxRefNo.DataType = System.Type.GetType("System.String");
            SalesTaxRefNo.ColumnName = "SalesTaxRefNo";
            newCompany.Columns.Add(SalesTaxRefNo);

            DataColumn PayrollRefNo = new DataColumn();
            PayrollRefNo.DataType = System.Type.GetType("System.String");
            PayrollRefNo.ColumnName = "PayrollRefNo";
            newCompany.Columns.Add(PayrollRefNo);

            DataColumn NumberOfEmployee = new DataColumn();
            NumberOfEmployee.DataType = System.Type.GetType("System.Int32");
            NumberOfEmployee.ColumnName = "NumberOfEmployee";
            newCompany.Columns.Add(NumberOfEmployee);

            DataColumn Attachment = new DataColumn();
            Attachment.DataType = System.Type.GetType("System.String");
            Attachment.ColumnName = "Attachment";
            newCompany.Columns.Add(Attachment);

            fileTable.Rows[0].Delete();
            fileTable.Rows[1].Delete();
            fileTable.Rows[2].Delete(); // col names could have been taken from here
            fileTable.AcceptChanges();
            foreach (DataRow filerow in fileTable.Rows)
            {
                    var filecol = filerow.ItemArray;
                
                    // Add some new rows to the collection. 
                    DataRow row = newCompany.NewRow();
                    Console.WriteLine("ID: \t" + filerow[0].GetType().ToString());
                    row["company_id"] = GlobalVar.companyID;
                    row["name"] = GetName(filerow[1]);
                    row["Address"] = GetAddress(filerow[2]);
                    row["TaxRefNo"] = GetTaxRef(filerow[3]);
                    row["SalesTaxRefNo"] = GetSalesTaxRef(filerow[4]);
                    row["PayrollRefNo"] = GetPayRollRef(filerow[5]);
                    row["NumberOfEmployee"] = GetNoOfEmployees(filerow[6]);
                    row["Attachment"] = GetAttatchment(filerow[7]);

                    newCompany.Rows.Add(row);
                    newCompany.AcceptChanges();
                

            }
            // Return the new DataTable. 
            return newCompany;
        }

        private object GetAttatchment(object v)
        {
            Console.WriteLine("Attahcement is:\t" + v.GetType().ToString());
            return v;
        }

        private object GetNoOfEmployees(object v)
        {
            Console.WriteLine("Empl:\t" + v.GetType().ToString());
            return v;
        }

        private object GetPayRollRef(object v)
        {
            Console.WriteLine("Pay:" + v.GetType().ToString());
            return v;
        }

        private object GetSalesTaxRef(object v)
        {
            Console.WriteLine("Sales:" + v.GetType().ToString());
            return v;
        }

        private object GetTaxRef(object v)
        {
            Console.WriteLine("Tax:" + v.GetType().ToString());
            return v;
        }

        private object GetAddress(object v)
        {
            Console.WriteLine("add:" + v.GetType().ToString());
            return v;
        }

        private object GetName(object v)
        {
            Console.WriteLine("Name: " + v.GetType().ToString());
            return v;
        }
    }
}
