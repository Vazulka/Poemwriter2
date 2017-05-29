using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.IO;

namespace poemwriter2
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string[] poemFromTxt;
        OpenFileDialog ofg = new OpenFileDialog();
        StreamReader sr;
        
        public MainWindow()
        {
            Poemwriter.Words.Read();
            Poemwriter.Schema.schemaCodeReader();
            InitializeComponent();
        }
        private void allTrue()
        {
            fList.IsEnabled = true;
            fBox.IsEnabled = true;
            fTxt.IsEnabled = true;
        }
        private void changeEnable(System.Windows.Controls.MenuItem mI)
        {
            System.Windows.Controls.MenuItem[] mArr = new System.Windows.Controls.MenuItem[] { fList, fBox, fTxt };
            for (int i = 0; i < mArr.Length; i++)
            {
                if (mArr[i] != mI) { mArr[i].IsEnabled = false; }
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Write_Click(object sender, RoutedEventArgs e)
        {        
            Poemwriter.Line.writer(Poemwriter.Line.lineList);           
            for (int i = 0; i < Poemwriter.Line.lineList.Count; i++)
            {
                for (int j = Poemwriter.Line.lineList[i].wordsInLine.Count-1; j >-1; j--)
                {
                    textBox.Text += (" " + Poemwriter.Line.lineList[i].wordsInLine[j].baseWord).ToLower();
                }
                textBox.Text += "\n";
            }            
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Poemwriter.Line.lineList.Count; i++)
            {
                Poemwriter.Line.lineList[i].wordsInLine.Clear();
                Poemwriter.Line.lineList[i].lastVowel = '\0';
                Poemwriter.Line.lineList[i].penultVowel = '\0';
            }
            textBox.IsReadOnly = false;
            allTrue();
            MenuRhymeWindow.Permission.lista.Clear();
            textBox.Clear();
        }        
        private void New_click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            allTrue();
            MenuRhymeWindow.Permission.lista.Clear();
            Poemwriter.Line.lineList.Clear();
            Poemwriter.Line.addedword.Clear();
            Poemwriter.Words.newWordlist.Clear();
        }
        private void fList_Click(object sender, RoutedEventArgs e)
        {
            changeEnable(fList);
            MenuSchemaWindow mw = new MenuSchemaWindow();
            mw.Show();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void fBox_Click(object sender, RoutedEventArgs e)
        {
            changeEnable(fBox);
            MenuBoxWindow mbw = new MenuBoxWindow();
            mbw.Show();
        }

        private void fTxt_Click(object sender, RoutedEventArgs e)
        {
            ofg.ShowDialog();
            ofg.DefaultExt = ".txt";
            ofg.Filter= "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            try
            {
                poemFromTxt = File.ReadAllLines(ofg.FileName,Encoding.Default);
            }
            catch (ArgumentException) { }

            changeEnable(fTxt);
            MenuBoxWindow mbw = new MenuBoxWindow();
            mbw.Show();
        }
    }
}
