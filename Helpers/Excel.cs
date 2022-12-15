using Microsoft.Office.Interop.Excel;
using System;

namespace Cappario
{
    public static class Excel
    {
        public static void ReadFile()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Cappario.xlsx";
            Application Excel = new Application();
            Workbook wb = Excel.Workbooks.Open(FilePath);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            var Range = Excel.Range["B:B"];
            var RowNumber = Range.Find("98168").Row;
            Console.WriteLine(Convert.ToString(ws.Cells[RowNumber, 1].Value));
            Excel.Quit();
        }

        //guarda cap jobiste
        //controlla branch torni
    }
}