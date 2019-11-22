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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            WebClient wc = new WebClient();
            MessageBox.Show($"{System.IO.Directory.GetCurrentDirectory()}");
            try
            {
                wc.DownloadFile(new Uri(Properties.Settings.Default.Link), "file.xlsx");
                MessageBox.Show("Загрузка завершена");
            }
            catch (WebException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx");
                ExcelFile excelfile = new ExcelFile($@"{System.IO.Directory.GetCurrentDirectory()}\file.xlsx", 1);
                MessageBox.Show(excelfile.GetElement(2, 1));
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Для начала обновите файл");
            }
        }
    }
}
