using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace Webcam_App
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private bool isCapturing = false; // Start capturing when the application starts

        public MainWindow()
        {
            InitializeComponent();
            InitializeWebcam();
            StartCapture();
        }

        private void InitializeWebcam()
        {
            // Get the list of available video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                // Use the first available video device
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += FinalFrame_NewFrame;

                // Start capturing frames from the webcam immediately
                if (isCapturing)
                    videoSource.Start();
            }
            else
            {
                System.Windows.MessageBox.Show("No video devices found.");
            }
        }

        private void StartCapture()
        {
            if (videoSource != null)
            {
                if (isCapturing)
                {
                    // Stop capturing frames from the webcam
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    isCapturing = false;

                    // Show a message after capturing
                    System.Windows.MessageBox.Show("Frame captured and saved successfully!", "Capture Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Start capturing frames from the webcam
                    videoSource.Start();
                    isCapturing = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {              
            TakeScreenshotAndSave();               
        }

        private void TakeScreenshotAndSave()
        {
            // Check if the pictureBox has an image
            if (pictureBox.Source != null)
            {
                // Convert the WPF ImageSource to a BitmapImage
                BitmapImage bitmapImage = (BitmapImage)pictureBox.Source;

                // Create a BitmapEncoder and encode the BitmapImage
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                // Create a unique filename based on the current timestamp
                string fileName = $"screenshot_{DateTime.Now:yyyyMMddHHmmssfff}.jpg";

                // Set the default folder path
                string defaultFolderPath = @"C:\Users\nghia\Pictures\CapturedFrames";

                // Combine the folder path and filename to get the full path
                string fullPath = Path.Combine(defaultFolderPath, fileName);

                // Create or overwrite the file with the encoded image
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }

                // Show a message after capturing and saving
                System.Windows.MessageBox.Show($"Frame captured and saved successfully at:\n{fullPath}", "Capture Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("No image to capture.", "Capture Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isCapturing) // Ensure capturing is active before processing frames
            {
                // Convert AForge's Bitmap to System.Drawing.Bitmap
                System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

                // Display the bitmap in the WPF Image control
                Dispatcher.Invoke(() =>
                {
                    pictureBox.Source = ConvertBitmapToBitmapImage(bitmap);
                });
            }
        }

        private BitmapImage ConvertBitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = "";
            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                txtfolderPath.Text = folderPath;
                DisplayFilesInListView(folderPath);
            }
        }

        private void DisplayFilesInListView(string folderPath)
        {
            listView.Items.Clear();

            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    var fileItem = new { Image = fileInfo.Extension, Name = fileInfo.Name, Path = fileInfo.FullName };

                    // Add the object to the ListView
                    listView.Items.Add(fileItem);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
