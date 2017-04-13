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
    /// Interaction logic for MenuRhymeWindow.xaml
    /// </summary>
    public partial class MenuRhymeWindow : Window
    {
        public class Permission
        {
            public static List<Permission> lista = new List<Permission>();
            public Label lineEnd;
            public TextBox rhymeCode;
            public Permission(Label l, TextBox t)
            {
                lineEnd = l;
                rhymeCode = t;
                lista.Add(this);
            }
        }
        public MenuRhymeWindow()
        {
            InitializeComponent();
            fill();
        }

        private void fill()
        {
            int firstRectangle = 185;
            for (int i = 0; i < Poemwriter.Line.lineList.Count; i++)
            {
                int length = Poemwriter.Line.lineList[i].length;
                TextBox tb1 = new TextBox();
                tb1.Name = "t" + i.ToString();
                tb1.Margin = new Thickness(350, firstRectangle, 0, 0);
                tb1.Height = 35;
                tb1.Width = 35;
                tb1.HorizontalAlignment = HorizontalAlignment.Left;
                tb1.VerticalAlignment = VerticalAlignment.Top;
                tb1.Text = "";
                tb1.FontSize = 25;
                tb1.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
                rhymegrid.Children.Add(tb1);

                Label l1 = new Label();
                l1.Name = "l"+i.ToString();
                l1.Content = Poemwriter.Line.lineList[i].toneCode[length - 2]+""+ Poemwriter.Line.lineList[i].toneCode[length - 1];
                l1.Margin = new Thickness(200, firstRectangle, 0, 0);
                l1.Height = 35;
                l1.Width = 75;
                l1.HorizontalAlignment = HorizontalAlignment.Left;
                l1.VerticalAlignment = VerticalAlignment.Top;
                rhymegrid.Children.Add(l1);
                Permission v = new Permission(l1, tb1);
                firstRectangle += 50;

            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb1 = sender as TextBox;
            for (int i = 0; i < Permission.lista.Count; i++)
            {    
                if (tb1.Text == Permission.lista[i].rhymeCode.Text && Permission.lista.Find(x => x.rhymeCode.Name == tb1.Name).lineEnd.Content.ToString()!=Permission.lista[i].lineEnd.Content.ToString())
                {
                    tb1.Clear();
                    break;
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            for (int i = 0; i < Permission.lista.Count; i++)
            {
                if (Permission.lista[i].rhymeCode.Text == "") { ok = false; }

            }
            if (ok)
            {
                Poemwriter.Line.addRhymToListElements(Permission.lista.Select(x => x.rhymeCode.ToString()).ToArray());
                AddWordWindow aw = new AddWordWindow();
                aw.Show();
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
