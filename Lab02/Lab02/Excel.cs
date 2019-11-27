using System;
using System.IO;
using OfficeOpenXml;

namespace Lab02
{
    class ExcelFile
    {
        ExcelPackage excelfile = null;
        ExcelWorksheet ws = null;
        public ExcelFile(string path, int sheet)
        {
            excelfile = new ExcelPackage(new FileInfo(path));
            ws = excelfile.Workbook.Worksheets[sheet];
        }
        public string GetElement (int row, int column)
        {
            return Convert.ToString(ws.Cells[row, column].Value);
        }
        public void Dispose()
        {
            excelfile.Dispose();
        }
    }
}
