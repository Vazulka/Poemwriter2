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
    /// Interaction logic for MenuSchemaWindow.xaml
    /// </summary>
    public partial class MenuSchemaWindow : Window
    {
        public MenuSchemaWindow()
        {
            InitializeComponent();
            fill_list();
        }

        void fill_list()
        {
            for (int i = 0; i < Poemwriter.Schema.getList().Count; i++)
            {
                listBox.Items.Add(Poemwriter.Schema.getList()[i].poemForm);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
