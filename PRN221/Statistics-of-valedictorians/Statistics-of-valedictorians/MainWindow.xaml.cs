using Microsoft.Win32;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Statistics_of_valedictorians
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;

                txtPath.Text = filePath;

                List<Student> students = LoadCsv(filePath);

                dgScores.ItemsSource = students;
            }
        }

        private List<Student> LoadCsv(string filePath)
        {
            List<Student> records = new List<Student>();

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    records = csv.GetRecords<Student>().ToList();
                }

                // Extract unique years from the loaded data
                var uniqueYears = records.Select(s => s.Year).Distinct().ToList();

                // Populate ComboBox with unique years
                cbYear.ItemsSource = uniqueYears;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading CSV file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return records;
        }
    }
}
