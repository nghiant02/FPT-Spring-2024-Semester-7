using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Linq;

namespace ImportCSVFileToDB
{
    public class CSVData
    {
        public static DataView GetCsvData(string path)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(",");

                if (!parser.EndOfData)
                {
                    string[] columns = parser.ReadFields();
                    dataTable.Columns.AddRange(columns.Select(col => new DataColumn(col)).ToArray());
                }

                while (!parser.EndOfData)
                {
                    dataTable.Rows.Add(parser.ReadFields());
                }
            }

            return dataTable.DefaultView;
        }
    }
}
