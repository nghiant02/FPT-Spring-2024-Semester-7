using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;
using WinForms = System.Windows.Forms;

namespace File_Explorer
{
    public partial class MainWindow : Window
    {
        private const string DefaultIconPath = "C:/PRN221/File_Explorer/10147_icons/imageres_5.ico";
        private const string DeleteConfirmationMessage = "Are you sure you want to delete '{0}'?";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog
            {
                InitialDirectory = ""
            };

            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
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
                foreach (string directory in Directory.GetDirectories(folderPath))
                {
                    listView.Items.Add(new FileFolderItem
                    {
                        Icon = new BitmapImage(new Uri(DefaultIconPath)),
                        Type = "Folder",
                        Name = Path.GetFileName(directory),
                        Path = directory
                    });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Error accessing directories: {ex.Message}");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is FileFolderItem selectedItem)
            {
                string confirmationMessage = string.Format(DeleteConfirmationMessage, selectedItem.Name);
                MessageBoxResult confirmation = MessageBox.Show(confirmationMessage, "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (selectedItem.Type == "File")
                        {
                            File.Delete(selectedItem.Path);
                        }
                        else if (selectedItem.Type == "Folder")
                        {
                            Directory.Delete(selectedItem.Path, true); // true for recursive delete
                        }

                        // Refresh the ListView
                        RefreshListView(selectedItem.Path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting item: {ex.Message}");
                    }
                }
            }
        }

        private void RefreshListView(string folderPath)
        {
            DisplayFilesInListView(Path.GetDirectoryName(folderPath));
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is FileFolderItem selectedItem)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter the new name:", "Rename", selectedItem.Name);

                if (!string.IsNullOrEmpty(newName) && newName != selectedItem.Name)
                {
                    string newPath = Path.Combine(Path.GetDirectoryName(selectedItem.Path), newName);

                    try
                    {
                        if (selectedItem.Type == "File")
                        {
                            File.Move(selectedItem.Path, newPath);
                        }
                        else if (selectedItem.Type == "Folder")
                        {
                            Directory.Move(selectedItem.Path, newPath);
                        }

                        // Refresh the ListView
                        RefreshListView(selectedItem.Path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error renaming item: {ex.Message}");
                    }
                }
            }
        }
    }

    public class FileFolderItem
    {
        public ImageSource Icon { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}
