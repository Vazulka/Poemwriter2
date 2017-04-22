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
using System.Windows.Shapes;

namespace poemwriter2
{
    /// <summary>
    /// Interaction logic for MenuBoxWindow.xaml
    /// </summary>
    public partial class MenuBoxWindow : Window
    {
        public MenuBoxWindow()
        {
            InitializeComponent();
        }
        static string[] tempLine;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void coding_click(object sender, RoutedEventArgs e)
        {
            tempLine = new string[textBoxBase.LineCount];
            for (int i = 0; i < textBoxBase.LineCount; i++)
            {
                tempLine[i] = Poemwriter.Words.clearing(textBoxBase.GetLineText(i));
                tempLine[i] = Poemwriter.Words.codemake(tempLine[i].Replace(" ", "").ToUpper());
                textBoxTone.Text += tempLine[i] + "\n";
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            if (tempLine.Length != 0)
            {
                Poemwriter.Line.readFromSchema(tempLine);
                MenuRhymeWindow mr = new MenuRhymeWindow();
                mr.Show();
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            label1.Visibility = Visibility.Collapsed;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            label1.Visibility = Visibility.Collapsed;
        }

        private void Save_click(object sender, RoutedEventArgs e)
        {
            if (textBoxTone.Text != "")
            {
                if (textBox.Text != "")
                {
                    int result = (int)MessageBox.Show("Biztos hozzá adod a Schema listához?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (result == 6)
                    {
                        List<string> templist = new List<string>();
                        templist.Add("*"+textBox.Text);
                        for (int i = 0; i < textBoxTone.LineCount; i++)
                        {
                            if (textBoxTone.GetLineText(i) != "")
                            {
                                templist.Add(textBoxTone.GetLineText(i));
                            }
                        }
                        templist.Add(";");
                        Poemwriter.Schema.schemaWriter(templist);
                    }


            }
                else { MessageBox.Show("Adj neki nevet"); }
            }
            else { MessageBox.Show("Kódold le a verset"); }
        }

    }
}
