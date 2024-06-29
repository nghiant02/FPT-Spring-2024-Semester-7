using Microsoft.Win32;
using System.Windows;

namespace ImportCSVFileToDB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Csv Files|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var csvData = CSVData.GetCsvData(openFileDialog.FileName);
                dgCsv.ItemsSource = csvData;
            }
        }
    }
}
