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
        List<Var> plangprogramvars = new List<Var>();
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
        string pc = @"
            PROGRAM oszto
                VALTOZOK:
                i: EGÉSZ
                a, i : egesz
                be: a
                be: i
                ki: a + i
                CIKLUS AMÍG i < 5
                KI: i
                i := i + 1
                CIKLUS_VÉGE
            PROGRAM_VEGE
            ";
        private string ConvertCode(string code)
        {
            List<string> plangLines = code.Split('\n').ToList();
            List<string> usings = Usings(code);
            List<string> vars = Vars(plangLines);
            List<string> cSharpLines = new List<string>();

            string convCode = string.Empty;
            return convCode;
        }
        private List<string> Vars(List<string> planglines)
        {
            List<string> vars = new List<string>();
            foreach (string line in planglines)
            {
                string l = line.ToLower();
                if (l.Contains("logikai"))
                {
                    if (!l.Contains("["))
                    {
                        string[] egesztomb = l.Split(':');
                        string[] tomb0 = egesztomb[0].Split(new string[] { ",", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tomb0.Length - 2; i += 2)
                        {
                            vars.Add($"bool[] {tomb0[i]} = new bool[{tomb0[i + 1]}]");
                        }
                    }
                    else
                    {
                        string[] logikai = l.Split(':');
                        vars.Add($"bool {logikai[0]}");
                        plangprogramvars.Add(new Var(Var.Variable.Bool, logikai[0]))
                    }
                }
                else if (l.Contains("egesz"))
                {
                    if (!l.Contains("["))
                    {
                        string[] egesztomb = l.Split(':');
                        string[] tomb0 = egesztomb[0].Split(new string[] { ",", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tomb0.Length - 2; i += 2)
                        {
                            vars.Add($"int[] {tomb0[i]} = new int[{tomb0[i + 1]}]");
                        }
                    }
                    else
                    {
                        string[] egesz = l.Split(':');
                        vars.Add($"int {egesz[0]}");
                    }
                }
                else if (l.Contains("valos"))
                {
                    if (!l.Contains("["))
                    {
                        string[] egesztomb = l.Split(':');
                        string[] tomb0 = egesztomb[0].Split(new string[] { ",", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tomb0.Length - 2; i += 2)
                        {
                            vars.Add($"float[] {tomb0[i]} = new float[{tomb0[i + 1]}]");
                        }
                    }
                    else
                    {
                        string[] egesz = l.Split(':');
                        vars.Add($"float {egesz[0]}");
                    }
                }
                else if (l.Contains("karakter"))
                {
                    if (!l.Contains("["))
                    {
                        string[] egesztomb = l.Split(':');
                        string[] tomb0 = egesztomb[0].Split(new string[] { ",", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tomb0.Length - 2; i += 2)
                        {
                            vars.Add($"char[] {tomb0[i]} = new char[{tomb0[i + 1]}]");
                        }
                    }
                    else
                    {
                        string[] egesz = l.Split(':');
                        vars.Add($"char {egesz[0]}");
                    }
                }
                else if (l.Contains("szoveg"))
                {
                    if (!l.Contains("["))
                    {
                        string[] egesztomb = l.Split(':');
                        string[] tomb0 = egesztomb[0].Split(new string[] { ",", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tomb0.Length - 2; i += 2)
                        {
                            vars.Add($"string[] {tomb0[i]} = new string[{tomb0[i + 1]}]");
                        }
                    }
                    else
                    {
                        string[] egesz = l.Split(':');
                        vars.Add($"string {egesz[0]}");
                    }
                }
                else if (l.Contains("befajl"))
                {

                    string[] befajl = l.Split(':');
                    vars.Add($"StreamReader {befajl[0]} = null");
                }
                else if (l.Contains("kifajl"))
                {
                    string[] kifajl = l.Split(':');
                    vars.Add($"StreamWriter {kifajl[0]} = null");
                }
            }
            return vars;
        }
        private List<string> Usings(string code) 
        {
            List<string> usings = new List<string>() { "System" };
            if (new[] { "befajl", "kifajl" }.Any(code.Contains))
            {
                usings.Add("System.IO");
            }
            return usings;
        }
    }
}
