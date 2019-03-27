using Microsoft.WindowsAPICodePack.Shell;
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

namespace tagmast
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
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string[] keywords = new string[1];
            keywords[0] = textbox2.Text;
           string dir = TextBox1.Text;
            DirectoryInfo di1 = new DirectoryInfo(dir);
            System.IO.DirectoryInfo rootDir = di1;
            WalkDirectoryTree(di1, keywords);

        }
        public static void WalkDirectoryTree(System.IO.DirectoryInfo root, string[] keyword)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    try
                    {
                    string file = fi.FullName;
                    var shellFile = ShellFile.FromFilePath(file);
                    shellFile.Properties.System.Keywords.Value = keyword;
                    }
       
                    catch (Microsoft.WindowsAPICodePack.Shell.PropertySystem.PropertySystemException e)
                    {
                        string caption = "Error: non media files found";
                        string message = fi.FullName;
                        MessageBox.Show(message, caption, MessageBoxButton.OK);
                    }
                    
                    
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, keyword);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
