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

namespace Photo_Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class FirstPage : Window
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = for_path + @"Content\giphy.gif";

            image.Source = new BitmapImage(new Uri(for_path + @"Content\1499262.jpg"));
            image1.Source = new BitmapImage(new Uri(for_path + @"Content\1499263.jpg"));
            image2.Source = new BitmapImage(new Uri(for_path + @"Content\1499264.jpg"));
            image3.Source = new BitmapImage(new Uri(for_path + @"Content\14992631.jpg"));
        }

        private void Button_Copy16_Click(object sender, RoutedEventArgs e)
        {
            MainWindow o = new MainWindow();
            o.Show();
            this.Hide();
        }

        private void Button_Copy12_Click(object sender, RoutedEventArgs e)
        {
            TablePageClient o = new TablePageClient();
            o.Show();
            this.Hide();
        }

        private void Button_Copy13_Click(object sender, RoutedEventArgs e)
        {
            AddPageClient o = new AddPageClient();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy14_Click(object sender, RoutedEventArgs e)
        {
            EditPageClient o = new EditPageClient();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy15_Click(object sender, RoutedEventArgs e)
        {
            DeletePageClient o = new DeletePageClient();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy8_Click(object sender, RoutedEventArgs e)
        {
            TablePageProduct o = new TablePageProduct();
            o.Show();
            this.Hide();
        }

        private void Button_Copy9_Click(object sender, RoutedEventArgs e)
        {
            AddPageProduct o = new AddPageProduct();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy10_Click(object sender, RoutedEventArgs e)
        {
            EditPageProduct o = new EditPageProduct();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy11_Click(object sender, RoutedEventArgs e)
        {
            DeletePageProduct o = new DeletePageProduct();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            TablePageServices o = new TablePageServices();
            o.Show();
            this.Hide();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            AddPageServices o = new AddPageServices();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            EditPageSevices o = new EditPageSevices();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            DeletePageServices o = new DeletePageServices();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            AddPageOrder o = new AddPageOrder();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy4_Click_1(object sender, RoutedEventArgs e)
        {
            TablePageOrder o = new TablePageOrder();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy7_Click(object sender, RoutedEventArgs e)
        {
            DeletePageOrder o = new DeletePageOrder();
            o.Show();
            o.Tag = "1";
            this.Hide();
        }

        private void Button_Copy17_Click(object sender, RoutedEventArgs e)
        {
            EditWindow o = new EditWindow();
            o.Name = this.Name;
            o.Show();
            this.Hide();
        }
    }
}
