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
            for (int i = 0; i < textBox1.LineCount; i++)
            {
                string tempLine = Poemwriter.Words.clearing( textBox1.GetLineText(i));
                tempLine = Poemwriter.Words.codemake(tempLine.Replace(" ", ""));
            }
        }
    }
}
