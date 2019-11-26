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
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            try
            {
                UpdateDataGrid(new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1));
            }
            catch (Exception)
            {
                MessageBox.Show("У вас нет таблицы УБИ. Пробую загрузить файл...");
                try
                {
                    wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "file.xlsx");
                    UpdateDataGrid(new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1));
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Невозможно загрузить файл, проверьте доступ к интернету");
                    throw ex;
                }
            }
        }

        private int UpdateDataGrid(ExcelFile excelfile)
        {
            int i = 3;
            int counter = 0;
            DateTime LastUpdate = new DateTime();
            InfoGrid.Items.Clear();
            RecordList.Clear();
            while (excelfile.GetElement(i, 1) != null)
            {
                var r = new Record("УБИ." + Int32.Parse(excelfile.GetElement(i, 1)), Int32.Parse(excelfile.GetElement(i, 1)), excelfile.GetElement(i, 2), excelfile.GetElement(i, 3), excelfile.GetElement(i, 4),
                    excelfile.GetElement(i, 5), excelfile.GetElement(i, 6) == "1", excelfile.GetElement(i, 7) == "1", excelfile.GetElement(i, 8) == "1",
                    DateTime.Parse(excelfile.GetElement(i, 9)), DateTime.Parse(excelfile.GetElement(i, 10)));
                if (r.LastUpdateTime > Properties.Settings.Default.LastUpdate)
                {
                    counter++;
                    if (r.LastUpdateTime > LastUpdate) LastUpdate = r.LastUpdateTime;
                }
                if (r.Id <= 15)
                {
                    InfoGrid.Items.Add(r);
                }
                RecordList.Add(r);
                i++;
            }
            Properties.Settings.Default.LastUpdate = LastUpdate;
            Properties.Settings.Default.Save();
            LastInGrid = 15;
            excelfile.Close();
            return counter;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "temp.xlsx");
                var result = MessageBox.Show($"Загрузка завершена, количество обновленных записей {UpdateDataGrid(new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\temp.xlsx", 1))}. Хотите получить подробный отчет?", "Информация об обновлении", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) 
                {
                    ShowUpdateWindow();
                }
                System.IO.File.Delete("file.xlsx");
                System.IO.File.Move("temp.xlsx", "file.xlsx");
            }
            catch (WebException exc)
            {
                MessageBox.Show(exc.Message);
            }
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

        private void IdBox_Changed(object sender, TextChangedEventArgs e)
        {
            if (IdBox.Text == "")
            {
                InfoGrid.Items.Clear();
                for (int i = 0; i <= 14; i++)
                {
                    InfoGrid.Items.Add(RecordList[i]);
                }
                LastInGrid = 15;
            }
            else
            {
                int value = 0;
                if ((Int32.TryParse(IdBox.Text, out value)) && (value <= RecordList.Count) && (value > 0))
                {
                    InfoGrid.Items.Clear();
                    InfoGrid.Items.Add(RecordList[value - 1]);
                    LastInGrid = 15;
                }
            }
        }
        private void IsEnter(object sender, KeyEventArgs e)
        {
            int value = 0;
            if ((e.Key == Key.Return) && (Int32.TryParse(IdBox.Text, out value)) && (value <= RecordList.Count) && (value > 0))
            {
                MessageBox.Show(RecordList[value - 1].ToString());
            }
        }
    }
}
