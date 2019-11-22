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
        private ExcelFile excelfile;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "file.xlsx");
                MessageBox.Show($"Загрузка завершена, количество обновленных записей {CountUpdates()}"); //cсделать кнопку отчет
            }
            catch (WebException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private int CountUpdates()
        {
            try
            {
                excelfile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("У вас нет таблицы УБИ");
            }
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

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(excelfile.GetElement(2, 1));
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Для начала обновите файл");
            }
        }
    }
}
