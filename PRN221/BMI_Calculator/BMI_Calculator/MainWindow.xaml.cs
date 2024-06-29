using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateBMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double weight = Convert.ToDouble(txtWeight.Text);
                double height = Convert.ToDouble(txtHeight.Text);

                double bmi = CalculateBMIValue(weight, height);
                string status = GetBMIStatus(bmi);

                txtBMI.Text = bmi.ToString("F2");
                txtStatus.Text = status;

                // Color the BMI textbox based on BMI categories
                ColorBMITextbox(bmi);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for weight and height.");
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtWeight.Clear();
            txtHeight.Clear();
            txtBMI.Clear();
            txtStatus.Clear();
        }

        private double CalculateBMIValue(double weight, double heightInCentimeters)
        {
            // BMI formula: weight / ((height / 100.0) * (height / 100.0))
            return weight / ((heightInCentimeters / 100.0) * (heightInCentimeters / 100.0));
        }

        private string GetBMIStatus(double bmi)
        {
            if (bmi < 18.5)
                return "Underweight";
            else if (bmi >= 18.5 && bmi < 24.9)
                return "Normal Weight";
            else if (bmi >= 25 && bmi < 29.9)
                return "Overweight";
            else
                return "Obese";
        }

        private void ColorBMITextbox(double bmi)
        {
            if (bmi < 18.5)
                txtBMI.Foreground = Brushes.Blue; 
            else if (bmi >= 18.5 && bmi < 24.9)
                txtBMI.Foreground = Brushes.Green; 
            else if (bmi >= 25 && bmi < 29.9)
                txtBMI.Foreground = Brushes.Orange; 
            else
                txtBMI.Foreground = Brushes.Red; 
        }
    }
}