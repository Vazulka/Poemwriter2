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

namespace poemwriter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            Poemwriter.Words.Read();
            Poemwriter.Schema.schemaCodeReader();
            InitializeComponent();
        }

  
         private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuSchemaWindow mw = new MenuSchemaWindow();
            mw.Show();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddWordWindow aw2 = new AddWordWindow();
            aw2.Show();
        }

        private void Write_Click(object sender, RoutedEventArgs e)
        {
            Poemwriter.Line.addWordAndRhym(Poemwriter.Line.lineList, Poemwriter.Line.addedword);
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

        private void start(object sender, RoutedEventArgs e)
        {

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
            textBox.Clear();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
