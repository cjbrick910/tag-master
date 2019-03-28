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
            // declares keyword variable as a string array with 1 element inside
            string[] keywords = new string[1];
            //sends user input to the keywords variable
            keywords[0] = textbox2.Text;
            //grabs user iput of directory to a string
           string dir = TextBox1.Text;
            //turns the string into a DirectoryInfo object di1
            DirectoryInfo di1 = new DirectoryInfo(dir);
            //declares rootDir as another DirectoryInfo object with the same value as di1
            System.IO.DirectoryInfo rootDir = di1;
            //sends the keywords variable and the di1 variable to the WalkDirectoryTree method, which does most of the processing
            WalkDirectoryTree(di1, keywords);

        }
        public static void WalkDirectoryTree(System.IO.DirectoryInfo root, string[] keyword)
        {
            //creates fileinfo and directoryinfo objects for use later
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // gets all files and subdirectories inside the rootDir
            try
            {
                files = root.GetFiles("*.*");
            }
            //writes a console message for if a directory is not found
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            //main processing loop
            if (files != null)
            {
                
                foreach (System.IO.FileInfo fi in files)
                {
                   //for each loop, the program grabs the full path of each file in the directory, and puts it into a string <file>
                    try
                    {
                    string file = fi.FullName;
                    //uses the path to create a new ShellFile object for adding complex file properties
                    var shellFile = ShellFile.FromFilePath(file);
                    //sets the keyword property of the ShellFile object to the value in <keyword>
                    shellFile.Properties.System.Keywords.Value = keyword;
                    }
                    //if the file is not a media file, program throws a message box with the file name and an OK button
                    catch (Microsoft.WindowsAPICodePack.Shell.PropertySystem.PropertySystemException)
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
