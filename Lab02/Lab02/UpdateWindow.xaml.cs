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
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            CompareFiles();
        }

        public bool IsTrue(string s)
        {
            return (s == "1");
        }

        private void CompareFiles()
        {
            MessageBox.Show("Обращаем внимание, что генерация отчета может занять некоторое время");
            var excelFile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
            var NewExcelFile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\temp.xlsx", 1);
            int i = 3;
            while(excelFile.GetElement(i,1) != null) 
            {
                if (excelFile.GetElement(i, 10) != NewExcelFile.GetElement(i, 10))
                {
                    var r1 = new Record("Old", Int32.Parse(excelFile.GetElement(i, 1)), excelFile.GetElement(i, 2), excelFile.GetElement(i, 3), excelFile.GetElement(i, 4),
                        excelFile.GetElement(i, 5), excelFile.GetElement(i, 6) == "1", excelFile.GetElement(i, 7) == "1", excelFile.GetElement(i, 8) == "1",
                        DateTime.Parse(excelFile.GetElement(i, 9)), DateTime.Parse(excelFile.GetElement(i, 10)));

                    var r2 = new Record("New", Int32.Parse(NewExcelFile.GetElement(i, 1)), NewExcelFile.GetElement(i, 2), NewExcelFile.GetElement(i, 3), NewExcelFile.GetElement(i, 4),
                        NewExcelFile.GetElement(i, 5), NewExcelFile.GetElement(i, 6) == "1", NewExcelFile.GetElement(i, 7) == "1", NewExcelFile.GetElement(i, 8) == "1",
                        DateTime.Parse(NewExcelFile.GetElement(i, 9)), DateTime.Parse(NewExcelFile.GetElement(i, 10)));
                    UpdateInfoGrid.Items.Add(r1);
                    UpdateInfoGrid.Items.Add(r2);
                }
                i++;
            }
            excelFile.Close();
            NewExcelFile.Close();
            System.IO.File.Delete("file.xlsx");
            System.IO.File.Move("temp.xlsx", "file.xlsx");
        }
    }
}
