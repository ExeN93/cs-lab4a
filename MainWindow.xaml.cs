using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4a_2_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _sourceFolder;
        private string _destinationFolder;

        public MainWindow()
        {
            InitializeComponent();
        }
        // Wybór katalogu źródłowego
        private void BtnSelectSource_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _sourceFolder = dialog.SelectedPath;
                tbStatus.Text = "Wybrano katalog źródłowy: " + _sourceFolder;
            }
        }

        // Wybór katalogu docelowego
        private void BtnSelectDestination_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _destinationFolder = dialog.SelectedPath;
                tbStatus.Text = "Wybrano katalog docelowy: " + _destinationFolder;
            }
        }

        // Funkcja kopiowania zdjęć z zachowaniem nazewnictwa
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_sourceFolder) || string.IsNullOrEmpty(_destinationFolder))
            {
                System.Windows.MessageBox.Show("Proszę wybrać katalog źródłowy i docelowy.");
                return;
            }

            try
            {
                KopiujZdjecia(_sourceFolder);
                tbStatus.Text = "Kopiowanie zakończone pomyślnie!";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Wystąpił błąd: " + ex.Message);
            }
        }

        // Rekurencyjna funkcja kopiująca pliki
        private void KopiujZdjecia(string folder)
        {
            foreach (var file in Directory.GetFiles(folder, "*.jpg", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(file);
                DateTime creationTime = fileInfo.CreationTime;

                // Utworzenie nazwy w formacie rokmiesiacdziengodzinaminutasekunda_folderzrodlowy_nazwapliku.jpg
                string formattedTime = creationTime.ToString("yyyyMMddHHmmss");
                string folderName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file));
                string newFileName = $"{formattedTime}_{folderName}_{fileInfo.Name}";

                // Ścieżka docelowa
                string destFilePath = System.IO.Path.Combine(_destinationFolder, newFileName);

                // Kopiowanie pliku
                File.Copy(file, destFilePath, true);
            }
        }
    }
}
