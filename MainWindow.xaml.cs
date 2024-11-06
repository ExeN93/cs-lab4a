using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4a
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
        private void BrowseCsvFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                InputPathTextBox.Text = openFileDialog.FileName;
                OutputPathTextBox.Text = System.IO.Path.ChangeExtension(openFileDialog.FileName, ".html");
            }
        }

        private void BrowseHtmlFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                OutputPathTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void ConvertCsvToHtml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string csvPath = InputPathTextBox.Text;
                string htmlPath = OutputPathTextBox.Text;

                using (StreamReader sr = new StreamReader(csvPath))
                {
                    string line = sr.ReadToEnd();
                    using (StreamWriter sw = new StreamWriter(htmlPath))
                    {
                        string[] lines = line.Split('\n');
                        string[] headers = lines[0].Split(',');

                        sw.WriteLine("<html><style>\ntable {\n  font-family: arial, sans-serif;\n  border-collapse: collapse;\n  width: 100%;\n}\n\ntd, th {\n  border: 1px solid #dddddd;\n  text-align: left;\n  padding: 8px;\n}\n\ntr:nth-child(even) {\n  background-color: #dddddd;\n}\n</style>");
                        sw.WriteLine("<body><table><tr>");
                        foreach (string header in headers)
                        {
                            sw.WriteLine("<th>" + header.Trim() + "</th>");
                        }
                        sw.WriteLine("</tr>");

                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] row = lines[i].Split(',');
                            sw.WriteLine("<tr>");
                            foreach (string element in row)
                            {
                                sw.WriteLine("<td>" + element.Trim() + "</td>");
                            }
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("</table></body></html>");
                    }
                }

                MessageBox.Show("Conversion successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

