using ExcelDataReader;
using System;
using System.IO;
using System.Data;
using ETLfordashboard.Transform;

namespace ETLfordashboard
{
    class Program
    {

        static void Main(string[] args)
        {

            InFile readfile = new InFile();
            DataSet inputData = readfile.getData();
           
      
            int count = 0;
            
            foreach (DataTable sheetvar  in inputData.Tables)
            {
                string caseSwitch = sheetvar.TableName.Trim();

                Console.WriteLine(caseSwitch);
                count++;
                Console.Write(count + "). ");

                        
                switch (caseSwitch)
                {
                    case "Dash Borad":
                      //  Console.WriteLine(" Dash Borad");
                        break;
                    case "A- Company":
                        Console.WriteLine("A-Company");
                        //Make the data load ready
                       /* Company comp = new Company();
                        DataTable dbReadyTable = comp.MakeTable(sheetvar);
                        // insert it in Database
                        Database db = new Database();
                        db.insertrows(dbReadyTable); */
                        break;
                    case "A-Outlet":
                        Console.WriteLine("A-Outlet");
                        /*Outlet outlet = new Outlet();
                        DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                        Database db = new Database();
                        db.insertrows(dbReadyTable);*/
                        break;
                    case "A-User":
                        Console.WriteLine("A-User");
                       // User outlet = new User();
                        //DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                       // Database db = new Database();
                       // db.insertrows(dbReadyTable);
                        break;
                    case "A-User Role":
                        Console.WriteLine("A-User Role");
                        break;
                    case "A-Product Category":
                        Console.WriteLine("A-Product Category");
                       //  ProductCategory outlet = new ProductCategory();
                       //  DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                       //  Database db = new Database();
                       //  db.insertrows(dbReadyTable);
                        break;
                    case "A-Cash Projection":
                        Console.WriteLine("A-Cash Projection");
                      //  CashProjection outlet = new CashProjection();
                      //  DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                     //   Database db = new Database();
                     //   db.insertrows(dbReadyTable);
                        break;
                    case "A-AP":
                        Console.WriteLine("A-AP");
                       
                        break;
                    case "A-AR":
                        Console.WriteLine("A-AR");
                      //  AmountReciveable outlet = new AmountReciveable();
                       // DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                        //   Database db = new Database();
                        //  db.insertrows(dbReadyTable);
                        break;
                    case "Sales & Product Costing":
                        Console.WriteLine("Sales & Product Costing");
                       //  SaleTransaction outlet = new SaleTransaction();
                        // DataTable dbReadyTable = outlet.MakeTable(sheetvar);
                        // insert it in Database
                       //   Database db = new Database();
                       //   db.insertrows(dbReadyTable);
                        break;
                    case "Expense":
                        Console.WriteLine("Expense");
                       // Expense exp = new Expense();
                        //DataTable dbReadyTable = exp.MakeTable(sheetvar);
                        // insert it in Database
                        //Database db = new Database();
                      //  Console.WriteLine("--------------- INSERT IN DB");
                       //  db.insertrows(dbReadyTable);
                        break;
                    case "Sales Forecast":
                        Console.WriteLine("Sales Forecast");
                        break;
                    case "Cash Projection - Saad":
                        Console.WriteLine("Cash Projection - Saad");

                        CashProjection cp = new CashProjection();
                        DataTable dbReadyTable = cp.MakeTable(sheetvar);
                        // insert it in Database
                        Database db = new Database();
                        //  Console.WriteLine("--------------- INSERT IN DB");
                        db.insertrows(dbReadyTable);

                        break;
                    case "Jan July 2017 Income Statement":
                        Console.WriteLine("Jan July 2017 Income Statement");
                        break;
                    case "Jan July 2017 Balance sheet":
                        Console.WriteLine("Jan July 2017 Balance sheet");
                        break;
                    case "Report and Data input Formats":
                        Console.WriteLine("Report and Data input Formats");
                        break;
                    case "Recommended Accounting System":
                        Console.WriteLine("Recommended Accounting System");
                        break;
                    case "BS and Income Statement":
                        Console.WriteLine("BS and Income Statement");
                        break;
                    case "Bank Reconciliation Format":
                        Console.WriteLine("Bank Reconciliation Format");
                        break;
                    case "Accounting adjustment workplan":
                        Console.WriteLine("Accounting adjustment workplan");
                        break;
                    case "Journal Entry Template":
                        Console.WriteLine("Journal Entry Template");
                        break;
                    case "Chart of Account":
                        Console.WriteLine("Chart of Account");
                        break;

                    default:
                         Console.WriteLine("Default case");
                        break;
                }
                Console.WriteLine();
                    
            }
        }
            
        
    }

    public static class GlobalVar
    {
        public const int companyID = 5;
        public const int outletID = 125;

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

}
