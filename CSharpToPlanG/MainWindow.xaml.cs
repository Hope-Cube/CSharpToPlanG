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
using Path = System.IO.Path;

namespace CSharpToPlanG
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
        private void swap_Click(object sender, RoutedEventArgs e)
        {
            (inCode.Text, outCode.Text) = (outCode.Text, inCode.Text);
        }

        private void inport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the initial directory to the current working directory
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            openFileDialog.Filter = "Text Files (*.txt)|*.txt|C# Files (*.cs)|*.cs|PLANG Files (*.plang)|*.plang|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Determine the file extension
                string fileExtension = Path.GetExtension(filePath).ToLower();

                // Read and display the content based on file type
                switch (fileExtension)
                {
                    case ".txt":
                    case ".cs":
                    case ".plang":
                        ReadAndDisplayFileContent(filePath);
                        break;
                    default:
                        MessageBox.Show("Unsupported file type");
                        break;
                }
            }
        }
        private void ReadAndDisplayFileContent(string filePath)
        {
            try
            {
                // Read the content of the selected file
                string fileContent = File.ReadAllText(filePath);

                // Display the content in a TextBox (assuming you have a TextBox named "fileContentTextBox" in your XAML)
                inCode.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void convert_Click(object sender, RoutedEventArgs e)
        {
            outCode.Text = ConvertCode(inCode.Text);
        }
        private string ConvertCode(string code)
        {

        }
    }
}
