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
            for (int i = 0; i < Poemwriter.Schema.schemaList.Count; i++)
            {
                string listelement = Poemwriter.Schema.schemaList[i].poemForm.ToString();
                listBox.Items.Add(listelement);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void select_from(object sender, SelectionChangedEventArgs e)
        {
            listBox2.Items.Clear();
            Poemwriter.Schema s = Poemwriter.Schema.schemaList.Find(x => x.poemForm == listBox.SelectedItem.ToString());
            for (int i = 0; i < s.poemCode.Length; i++)
            {
                listBox2.Items.Add(s.poemCode[i].ToString());

            }
          
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            Poemwriter.Line.lineList.Clear();
            Poemwriter.Line.readFromSchema( Poemwriter.Schema.schemaList.Find(x => x.poemForm == listBox.SelectedItem.ToString()).poemCode);
            MenuRhymeWindow mr = new MenuRhymeWindow();
            mr.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
