using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.OleDb;
using Microsoft.Win32;
using Xceed.Words.NET;

namespace Photo_Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class TablePageProduct : Window
    {
        public TablePageProduct()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        OleDbConnection SqlConnection;

        public class Item
        {
            public string a_1 { get; set; }
            public string a_2 { get; set; }
            public string a_3 { get; set; }
            public string a_4 { get; set; }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Наименование";
            c1.Binding = new Binding("a_1");
            c1.Width = 210;
            DataGrid1.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Тип товара";
            c2.Width = 210;
            c2.Binding = new Binding("a_2");
            DataGrid1.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Количество";
            c3.Width = 110;
            c3.Binding = new Binding("a_3");
            DataGrid1.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Цена";
            c4.Width = 110;
            c4.Binding = new Binding("a_4");
            DataGrid1.Columns.Add(c4);

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Товары]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                DataGrid1.Items.Add(new Item() { a_1 = Convert.ToString(sqlReader["Наименование"]), a_2 = Convert.ToString(sqlReader["Тип товара"]), a_3 = Convert.ToString(sqlReader["Количество"]), a_4 = Convert.ToString(sqlReader["Цена"]) });
            }
        }

        private void Button_Copy16_Click(object sender, RoutedEventArgs e)
        {
            FirstPage o = new FirstPage();
            o.Show();
            this.Hide();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            AddPageProduct o = new AddPageProduct();
            o.Show();
            o.Tag = "2";
            this.Hide();
        }

        private void Button_Copy17_Click(object sender, RoutedEventArgs e)
        {
            MainWindow o = new MainWindow();
            o.Show();
            this.Hide();
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            EditPageProduct o = new EditPageProduct();
            o.Show();
            o.Tag = "2";
            this.Hide();
        }

        private void Button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            DeletePageProduct o = new DeletePageProduct();
            o.Show();
            o.Tag = "2";
            this.Hide();
        }

        private async void Button_Copy_Click1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == false)
                return;

            string filename = saveFileDialog1.FileName;
            string pathDocument = filename + ".docx";
            DocX document = DocX.Create(pathDocument);

            document.InsertParagraph("Товары фотосалона").
                FontSize(16).
                Bold().
                Alignment = Alignment.left;


            Xceed.Words.NET.Paragraph paragraph = document.InsertParagraph();
            paragraph.Alignment = Alignment.right;

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Товары]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                document.InsertParagraph("1. Наименование - " + Convert.ToString(sqlReader["Наименование"]));
                document.InsertParagraph("2. Тип товара - " + Convert.ToString(sqlReader["Тип товара"]));
                document.InsertParagraph("3. Количество - " + Convert.ToString(sqlReader["Количество"]));
                document.InsertParagraph("4. Цена - " + Convert.ToString(sqlReader["Цена"]));
                document.InsertParagraph("---------------------------------------------");
            }

            document.Save();
        }
    }
}
