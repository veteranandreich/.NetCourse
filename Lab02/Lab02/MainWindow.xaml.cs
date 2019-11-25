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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab02
{
    
    public partial class MainWindow : Window
    {
        private WebClient wc = new WebClient();
        private List<Record> RecordList = new List<Record>();
        private int LastInGrid;
        public MainWindow()
        {
            InitializeComponent();
            ShowDataGrid();
        }

        private void ShowDataGrid()
        {
            ExcelFile excelfile = null;
            try
            {
                excelfile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
            }
            catch (Exception)
            {
                MessageBox.Show("У вас нет таблицы УБИ");
                wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "file.xlsx");
                excelfile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
            }
            finally
            {
                int i = 3;
                while (excelfile.GetElement(i, 1) != null)
                {
                    var r = new Record("УБИ." + Int32.Parse(excelfile.GetElement(i, 1)), Int32.Parse(excelfile.GetElement(i, 1)), excelfile.GetElement(i, 2), excelfile.GetElement(i, 3), excelfile.GetElement(i, 4),
                        excelfile.GetElement(i, 5), excelfile.GetElement(i, 6) == "1", excelfile.GetElement(i, 7) == "1", excelfile.GetElement(i, 8) == "1",
                        DateTime.Parse(excelfile.GetElement(i, 9)), DateTime.Parse(excelfile.GetElement(i, 10)));
                    if (r.Id <= 15)
                    {
                        InfoGrid.Items.Add(r);
                    }
                    RecordList.Add(r);
                    i++;
                }
                LastInGrid = 15;
                excelfile.Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "temp.xlsx");
                var result = MessageBox.Show($"Загрузка завершена, количество обновленных записей {CountUpdates()}. Хотите получить подробный отчет?", "Информация об обновлении", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) ShowUpdateWindow();
                if (result == MessageBoxResult.No)
                {
                    System.IO.File.Delete("file.xlsx");
                    System.IO.File.Move("temp.xlsx", "file.xlsx");
                }
            }
            catch (WebException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private int CountUpdates()
        {
            var excelfile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
            int row = 3;
            const int column = 10;
            int counter = 0;
            DateTime LastUpdate = new DateTime();
            while(excelfile.GetElement(row, column) != null)
            {
                DateTime date = DateTime.Parse(excelfile.GetElement(row, column));
                if (date > Properties.Settings.Default.LastUpdate)
                {
                    counter++;
                    if (date > LastUpdate) LastUpdate = date;
                }
                row++;
            }
            if (LastUpdate != DateTime.MinValue)
            {
                Properties.Settings.Default.LastUpdate = LastUpdate;
                Properties.Settings.Default.Save();
            }
            excelfile.Close();
            return counter;
        }

        private void ShowUpdateWindow()
        {
            Window1 UpdateWindow = new Window1();
            UpdateWindow.Show();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (LastInGrid > RecordList.Count()) return;
            InfoGrid.Items.Clear();
            int i = 0;
            for (i = LastInGrid; i<= LastInGrid + 14; i++)
            {
                try
                {
                    InfoGrid.Items.Add(RecordList[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            if (i >= 1)
            {
                LastInGrid += 15;
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (LastInGrid == 15) return;
            InfoGrid.Items.Clear();
            for (int i = LastInGrid - 30; i <= LastInGrid - 16; i++)
            {
                try
                {
                    InfoGrid.Items.Add(RecordList[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Нет предыдущих записей");
                }
            }
            LastInGrid -= 15;
        }
    }
}
