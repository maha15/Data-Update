using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ETLfordashboard.Transform
{


    //enum owner contributer us lehaz se map
    class User
    {
        public DataTable MakeTable(DataTable fileTable)
        // Create a new DataTable for Company. 
        {
            DataTable newUser = new DataTable("dbo.User");

            // Add  column objects to the table. 
            DataColumn UserId = new DataColumn();
            UserId.DataType = System.Type.GetType("System.Int32");
            UserId.ColumnName = "UserId";
            UserId.AllowDBNull = false;
            newUser.Columns.Add(UserId);

            DataColumn FirstName = new DataColumn();
            FirstName.DataType = System.Type.GetType("System.String");
            FirstName.ColumnName = "FirstName";
            FirstName.AllowDBNull = false;
            newUser.Columns.Add(FirstName);

            DataColumn LastName = new DataColumn();
            LastName.DataType = System.Type.GetType("System.String");
            LastName.ColumnName = "LastName";
            newUser.Columns.Add(LastName);

            DataColumn Email = new DataColumn();
            Email.DataType = System.Type.GetType("System.String");
            Email.ColumnName = "Email";
            Email.AllowDBNull = false;
            newUser.Columns.Add(Email);

            DataColumn SecretCode = new DataColumn();
            SecretCode.DataType = System.Type.GetType("System.String");
            SecretCode.ColumnName = "SecretCode";
            SecretCode.AllowDBNull = false;
            newUser.Columns.Add(SecretCode);

            DataColumn ContactNumber = new DataColumn();
            ContactNumber.DataType = System.Type.GetType("System.String");
            ContactNumber.ColumnName = "ContactNumber";
            newUser.Columns.Add(ContactNumber);

            DataColumn OfficeAddress = new DataColumn();
            OfficeAddress.DataType = System.Type.GetType("System.String");
            OfficeAddress.ColumnName = "OfficeAddress";
            newUser.Columns.Add(OfficeAddress);

            DataColumn UniqueTaxId = new DataColumn();
            UniqueTaxId.DataType = System.Type.GetType("System.String");
            UniqueTaxId.ColumnName = "UniqueTaxId";
            newUser.Columns.Add(UniqueTaxId);

            DataColumn SalesTaxId = new DataColumn();
            SalesTaxId.DataType = System.Type.GetType("System.String");
            SalesTaxId.ColumnName = "SalesTaxId";
            newUser.Columns.Add(SalesTaxId);

            DataColumn Active = new DataColumn();
            Active.DataType = System.Type.GetType("System.Int32");
            Active.ColumnName = "Active";
            Active.AllowDBNull = false;
            newUser.Columns.Add(Active);

            DataColumn ActivationCode = new DataColumn();
            ActivationCode.DataType = System.Type.GetType("System.String");
            ActivationCode.ColumnName = "ActivationCode";
            newUser.Columns.Add(ActivationCode);

            DataColumn ActivationDate = new DataColumn();
            ActivationDate.DataType = System.Type.GetType("System.DateTime");
            ActivationDate.ColumnName = "ActivationDate";
            newUser.Columns.Add(ActivationDate);

            DataColumn CreatedDate = new DataColumn();
            CreatedDate.DataType = System.Type.GetType("System.DateTime");
            CreatedDate.ColumnName = "CreatedDate";
            newUser.Columns.Add(CreatedDate);

            DataColumn hdyhau_id = new DataColumn();
            hdyhau_id.DataType = System.Type.GetType("System.Int32");
            hdyhau_id.ColumnName = "hdyhau_id";
            newUser.Columns.Add(hdyhau_id);



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
                    row["UserId"] = Get(filerow[1]);
                    row["FirstName"] = GetFirstName(filerow[2]);
                    row["Email"] = GetEmail(filerow[3]);
                    row["SecretCode"] = KeyGenerator.GetUniqueKey(25);// GetCountry(filerow[2]); // not nullable and not given in file
                    row["ContactNumber"] = Get(filerow[5]);
                    row["OfficeAddress"] = string.Empty;
                    row["UniqueTaxId"] = string.Empty;
                    row["SalesTaxId"] = string.Empty;
                    row["Active"] = GetActiveState(filerow[8]); // db small int 
                    row["ActivationCode"] = null;
                    row["ActivationDate"] = new DateTime(); // Assuming at time of entry 
                    row["CreatedDate"] = new DateTime(); // Assuming at time of entry
                    row["hdyhau_id"] = DBNull.Value; // Not provided in file and field name not descriptive
                    row["LastName"] = string.Empty;// not given in current data set


                    newUser.Rows.Add(row);
                    newUser.AcceptChanges();
                    rowno++;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception in row no:" + rowno + "Message" + e.Message);
                     rowno++;
                }
             
            }
            // Return the new DataTable. 
            return newUser;
        }

        private object GetEmail(object v)
        {
            string email = MyToString(v);
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email: " + v + " \t Not provided ");

                throw new Exception("Email");
            }
            Console.WriteLine("Email: " + v + " \t DataType: " + v.GetType().ToString());

            return v;
        }

        private object GetFirstName(object v)
        {
            string name = MyToString(v);
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("FN: " + v + " \t Not provided " );

                throw new Exception("First Name Not provided");
            }
            Console.WriteLine("FN: " + v + " \t DataType: " + v.GetType().ToString());

            return v;
        }

        private static string MyToString(object o)
        {
            if (o == DBNull.Value || o == null)
                return "";

            return o.ToString();
        }
        private object GetActiveState(object v)
        {
            string inp = (string)v;
            if (inp.Trim().Equals("Active"))
            {
                Console.WriteLine("Active: " + v +  "1 \t DataType: " + v.GetType().ToString());

                return 1;
            }
            Console.WriteLine("Active: " + v + "0 \t DataType: " + v.GetType().ToString());

            return 0;
        }

        private object Get(object v)
        {
            Console.WriteLine("Get Col: " + v + "\t DataType: " + v.GetType().ToString());
            return v;
        }
    }




    // REF: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
    class KeyGenerator
    {
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
