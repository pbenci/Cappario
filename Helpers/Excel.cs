using Microsoft.Office.Interop.Excel;
using System;

namespace Cappario
{
    public static class Excel
    {
        public static Application ExcelApp = new Application();

        public static string GetRightBranchFromZipCode(string ZipCode)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Cappario.xlsx";
            Workbook wb = ExcelApp.Workbooks.Open(FilePath);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            var Range = ExcelApp.Range["B:B"];
            try
            {
                var RowNumber = Range.Find(ZipCode).Row;
                return Convert.ToString(ws.Cells[RowNumber, 1].Value);
            }
            catch (NullReferenceException)
            {
                Results.Log("The ZIP code" + " " + ZipCode + " " + "isn't in the Cappario.xlsx file");
                return null;
            }
        }

        public static string GetRightBranchFromId(string Id)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Branches.xlsx";
            Workbook wb = ExcelApp.Workbooks.Open(FilePath);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            var Range = ExcelApp.Range["B:B"];
            try
            {
                var RowNumber = Range.Find(Id).Row;
                return Convert.ToString(ws.Cells[RowNumber, 1].Value);
            }
            catch (NullReferenceException)
            {
                Results.Log("The branch with id" + " " + Id + " " + "isn't in the Cappario.xlsx file");
                return null;
            }
        }
    }
}