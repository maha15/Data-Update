using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ETLfordashboard
{
    class InFile
    {
        public DataSet getData()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string filePath = "C:\\Users\\Maha Amjad\\source\\repos\\ETLfordashboard\\ETLfordashboard\\Required Format - Updated.xlsx";
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    //Use the AsDataSet extension method
                    var result = reader.AsDataSet();
                    return result;

                }
            }
        }
    }
}
