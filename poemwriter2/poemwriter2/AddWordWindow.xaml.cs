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
    /// Interaction logic for AddWordWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
    {
        static int counter = 1;       
        static string tbNames="";
        static List<TextBox> listtb = new List<TextBox>();
        static List<TextBox> mora = new List<TextBox>();
        public AddWordWindow()
        {
            InitializeComponent();
            addWord();
            for (int i = 0; i < Poemwriter.Line.lineList.Count; i++)
            {
                lineListBox.Text += Poemwriter.Line.lineList[i].toneCode;
                if(i!= Poemwriter.Line.lineList.Count-1)
                {
                    lineListBox.Text += "\n";
                }

            }
        }
        private void addWord()
        {
            TextBox tb1 = new TextBox();
            tb1.Name = "t" + counter.ToString();
            tb1.Margin = new Thickness(10, 0, 0, 0);
            tb1.Height = 35;
            tb1.Width = 250;
            tb1.HorizontalAlignment = HorizontalAlignment.Left;
            tb1.VerticalAlignment = VerticalAlignment.Top;
            tb1.Text = "";
            tb1.SelectionBrush = new SolidColorBrush(Colors.Peru);
            tb1.Background = null;
            tb1.FontSize = 25;
            tb1.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            listtb.Add(tb1);
            stackpChildren2.Children.Add(tb1);

            Label l1 = new Label();
            l1.Name = "l" + counter.ToString();
            l1.Content = counter.ToString() + ". szó  :";
            l1.Margin = new Thickness(100, 0, 0, 0);
            l1.Height = 35;
            l1.Width = 160;
            l1.FontFamily = new FontFamily("papyrus");
            l1.FontSize = 25;       
            l1.HorizontalAlignment = HorizontalAlignment.Left;
            l1.VerticalAlignment = VerticalAlignment.Top;
            stackpChildren1.Children.Add(l1);
  

            TextBox moratb1 = new TextBox();  
            moratb1.Name = "t" + counter.ToString();
            moratb1.Margin = new Thickness(10, 0, 0, 0);
            moratb1.Height = 35;
            moratb1.Width = 250;
            moratb1.HorizontalAlignment = HorizontalAlignment.Left;
            moratb1.VerticalAlignment = VerticalAlignment.Top;
            moratb1.Text = "";
            moratb1.SelectionBrush = new SolidColorBrush(Colors.Peru);
            moratb1.Background = null;
            moratb1.FontSize = 25;
            mora.Add(moratb1);
            stackpChildren3.Children.Add(moratb1);
            counter++;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb.Text!=""&& !tbNames.Contains(tb.Name))
            {
                tbNames += tb.Name;
                
                addWord();
            }
            if(tb.Text!="")
            {
                for (int i = 0; i < mora.Count; i++)
                {
                    if (tb.Name == mora[i].Name)
                    {
                        mora[i].Clear();
                        mora[i].Text = Poemwriter.Words.codemake(Poemwriter.Words.clearing(tb.Text).Replace(" ", "").ToUpper());
                    
                    }
                }
            }
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listtb = listtb.OrderByDescending(x => x.Text.Length).ToList();
            for (int i = 0; i < listtb.Count; i++)
            {
                if (listtb[i].Text.Length > 0)
                {
                    Poemwriter.Words nw = new Poemwriter.Words(listtb[i].Text, true);
                }
            }
            Poemwriter.Line.addWordAndRhym(Poemwriter.Line.lineList, Poemwriter.Line.addedword);
            this.Close();
        }
    }
}
