using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            MessageBox.Show("Обращаем внимание, что генерация отчета может занять некоторое время");
            var NewExcelFile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\temp.xlsx", 1);
            int j = 0;
            for(j = 0; j<PrevList.Count(); j++)
            {
                if (PrevList[j].LastUpdateTime != DateTime.Parse(NewExcelFile.GetElement(j + 3, 10)))
                {
                    var r2 = new Record("New", Int32.Parse(NewExcelFile.GetElement(j+3, 1)), NewExcelFile.GetElement(j + 3, 2), NewExcelFile.GetElement(j + 3, 3), NewExcelFile.GetElement(j + 3, 4),
                        NewExcelFile.GetElement(j + 3, 5), NewExcelFile.GetElement(j + 3, 6) == "1", NewExcelFile.GetElement(j + 3, 7) == "1", NewExcelFile.GetElement(j + 3, 8) == "1",
                        DateTime.Parse(NewExcelFile.GetElement(j + 3, 9)), DateTime.Parse(NewExcelFile.GetElement(j + 3, 10)));
                    PrevList[j].Comment = "Old";
                    UpdateInfoGrid.Items.Add(PrevList[j]);
                    UpdateInfoGrid.Items.Add(r2);
                }
            }
            while (NewExcelFile.GetElement(j + 3, 1) != null)
            {
                var r2 = new Record("New", Int32.Parse(NewExcelFile.GetElement(j + 3, 1)), NewExcelFile.GetElement(j + 3, 2), NewExcelFile.GetElement(j + 3, 3), NewExcelFile.GetElement(j + 3, 4),
                        NewExcelFile.GetElement(j + 3, 5), NewExcelFile.GetElement(j + 3, 6) == "1", NewExcelFile.GetElement(j + 3, 7) == "1", NewExcelFile.GetElement(j + 3, 8) == "1",
                        DateTime.Parse(NewExcelFile.GetElement(j + 3, 9)), DateTime.Parse(NewExcelFile.GetElement(j + 3, 10)));
                UpdateInfoGrid.Items.Add(r2);
            }
            NewExcelFile.Close();
        }
    }
}
