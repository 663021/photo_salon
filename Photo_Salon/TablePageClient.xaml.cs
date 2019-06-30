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
    public partial class TablePageClient : Window
    {
        public TablePageClient()
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
            public string a_5 { get; set; }
            public string a_6 { get; set; }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Фамилия";
            c1.Binding = new Binding("a_1");
            c1.Width = 110;
            DataGrid1.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Имя";
            c2.Width = 110;
            c2.Binding = new Binding("a_2");
            DataGrid1.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Отчество";
            c3.Width = 110;
            c3.Binding = new Binding("a_3");
            DataGrid1.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Адрес";
            c4.Width = 110;
            c4.Binding = new Binding("a_4");
            DataGrid1.Columns.Add(c4);

            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "E-mail";
            c5.Width = 190;
            c5.Binding = new Binding("a_5");
            DataGrid1.Columns.Add(c5);

            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "Телефон";
            c6.Width = 110;
            c6.Binding = new Binding("a_6");
            DataGrid1.Columns.Add(c6);

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Клиенты]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                DataGrid1.Items.Add(new Item() { a_1 = Convert.ToString(sqlReader["Фамилия"]), a_2 = Convert.ToString(sqlReader["Имя"]), a_3 = Convert.ToString(sqlReader["Отчество"]), a_4 = Convert.ToString(sqlReader["Адрес"]), a_5 = Convert.ToString(sqlReader["E-mail"]), a_6 = Convert.ToString(sqlReader["Телефон"])});
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
            AddPageClient o = new AddPageClient();
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
            EditPageClient o = new EditPageClient();
            o.Show();
            o.Tag = "2";
            this.Hide();
        }

        private void Button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            DeletePageClient o = new DeletePageClient();
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

            document.InsertParagraph("Список клиентов фотосалона").
                FontSize(16).
                Bold().
                Alignment = Alignment.left;


            Xceed.Words.NET.Paragraph paragraph = document.InsertParagraph();
            paragraph.Alignment = Alignment.right;

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Клиенты]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                document.InsertParagraph("1. Фамилия - " + Convert.ToString(sqlReader["Фамилия"]));
                document.InsertParagraph("2. Имя - " + Convert.ToString(sqlReader["Имя"]));
                document.InsertParagraph("3. Отчество - " + Convert.ToString(sqlReader["Отчество"]));
                document.InsertParagraph("4. Адрес - " + Convert.ToString(sqlReader["Адрес"]));
                document.InsertParagraph("5. E-mail - " + Convert.ToString(sqlReader["E-mail"]));
                document.InsertParagraph("6. Телефон - " + Convert.ToString(sqlReader["Телефон"]));
                document.InsertParagraph("---------------------------------------------");
            }

            document.Save();
        }
    }
}
