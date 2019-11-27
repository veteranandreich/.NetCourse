using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lab02
{
    public partial class Window1 : Window
    {
        private List<Record> PrevList = null;
        public Window1(List<Record> PrevList)
        {
            InitializeComponent();
            this.PrevList = PrevList;
            CompareFiles();
        }
        
        private void CompareFiles()
        {
            var NewExcelFile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\temp.xlsx", 1);
            int j = 0;
            for(j = 0; j<PrevList.Count(); j++)
            {
                if (PrevList[j].LastUpdateTime != DateTime.FromOADate(Convert.ToDouble(NewExcelFile.GetElement(j+3, 10))))
                {
                    var r2 = new Record("New", Int32.Parse(NewExcelFile.GetElement(j+3, 1)), NewExcelFile.GetElement(j+3, 2), NewExcelFile.GetElement(j+3, 3), NewExcelFile.GetElement(j+3, 4),
                    NewExcelFile.GetElement(j+3, 5), NewExcelFile.GetElement(j+3, 6) == "1", NewExcelFile.GetElement(j+3, 7) == "1", NewExcelFile.GetElement(j+3, 8) == "1",
                    DateTime.FromOADate(Convert.ToDouble(NewExcelFile.GetElement(j+3, 9))), DateTime.FromOADate(Convert.ToDouble(NewExcelFile.GetElement(j+3, 10))));
                    PrevList[j].Comment = "Old";
                    UpdateInfoGrid.Items.Add(PrevList[j]);
                    UpdateInfoGrid.Items.Add(r2);
                }
            }
            while (NewExcelFile.GetElement(j + 3, 1) != "")
            {
                var r2 = new Record("New", Int32.Parse(NewExcelFile.GetElement(j + 3, 1)), NewExcelFile.GetElement(j + 3, 2), NewExcelFile.GetElement(j + 3, 3), NewExcelFile.GetElement(j + 3, 4),
                    NewExcelFile.GetElement(j + 3, 5), NewExcelFile.GetElement(j + 3, 6) == "1", NewExcelFile.GetElement(j + 3, 7) == "1", NewExcelFile.GetElement(j + 3, 8) == "1",
                    DateTime.FromOADate(Convert.ToDouble(NewExcelFile.GetElement(j + 3, 9))), DateTime.FromOADate(Convert.ToDouble(NewExcelFile.GetElement(j + 3, 10))));
                UpdateInfoGrid.Items.Add(r2);
            }
            NewExcelFile.Dispose();
        }
    }
}
