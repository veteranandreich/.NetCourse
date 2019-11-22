using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab02
{
    class ExcelFile
    {
        private Excel.Worksheet ws;
        private Excel.Workbook wb;
        private Excel.Application excel;
        public ExcelFile(string path, int sheet)
        {
            this.excel = new Microsoft.Office.Interop.Excel.Application();
            this.wb = excel.Workbooks.Open(path);
            this.ws= wb.Worksheets[sheet];
        }
        public string GetElement (int row, int column)
        {
            if (ws.Cells[row, column].value != "")
            {
                return ws.Cells[row, column].value;
            }
            return "";
        }
        public void Close()
        {
            wb.Close();
        }
    }
}
