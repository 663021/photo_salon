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
using System.Data.OleDb;

namespace Photo_Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AddPageOrder : Window
    {
        public AddPageOrder()
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
            connectString = for_path + @"Content\68c4ae27d6e3477.gif";

            image1.Source = new BitmapImage(new Uri(connectString));
            image.Source = new BitmapImage(new Uri(for_path + @"Content\employee4.png"));
            mediaElement.Source = new Uri(connectString);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Play();

            connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Наименование";
            c1.Binding = new Binding("a_1");
            c1.Width = 210;
            DataGrid1.Columns.Add(c1);


            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Стоимость";
            c3.Width = 110;
            c3.Binding = new Binding("a_3");
            DataGrid1.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Срок выполнения";
            c4.Width = 110;
            c4.Binding = new Binding("a_4");
            DataGrid1.Columns.Add(c4);

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Услуги]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                DataGrid1.Items.Add(new Item() { a_1 = Convert.ToString(sqlReader["Наименование"]), a_3 = Convert.ToString(sqlReader["Стоимость"]), a_4 = Convert.ToString(sqlReader["Срок"]) });
            }

            sqlReader.Close();

            command1 = new OleDbCommand("SELECT * FROM [Клиенты]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                ComboBox.Items.Add(Convert.ToString(sqlReader["Фамилия"]) + " "+ Convert.ToString(sqlReader["Имя"]));
            }

            strArr = new string[DataGrid1.Items.Count];
        }

        private void Button_Copy17_Click(object sender, RoutedEventArgs e)
        {
            MainWindow o = new MainWindow();
            o.Show();
            this.Hide();
        }

        private void Button_Copy16_Click_1(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(this.Tag) == "1")
            {
                FirstPage o = new FirstPage();
                o.Show();
                this.Hide();
            }
            else
            {
                TablePageOrder o = new TablePageOrder();
                o.Show();
                this.Hide();
            }
        }
        
        private void MediaElement_MediaEnded_3_1(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, 0, 0, 1);
            mediaElement.Play();
        }

        private void MediaElement_MediaOpened_3_1(object sender, RoutedEventArgs e)
        {
            image1.Visibility = Visibility.Hidden;
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();

            ComboBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            ComboBox.Opacity = 1;
        }

        string[] strArr = null;
        public int for_price = 0;

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var id_del = DataGrid1.SelectedIndex;
            var ci = new DataGridCellInfo(DataGrid1.Items[id_del], DataGrid1.Columns[2]);
            var content = ci.Column.GetCellContent(ci.Item) as TextBlock;

            for_price += Convert.ToInt32(content.Text);
            textBox_Copy2.Text = for_price.ToString();

            ci = new DataGridCellInfo(DataGrid1.Items[id_del], DataGrid1.Columns[1]);
            content = ci.Column.GetCellContent(ci.Item) as TextBlock;

            strArr[id_del] = content.Text;

            var bc = new BrushConverter();

            ComboBox.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            ComboBox.Opacity = 1;

            data.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            data.Opacity = 1;

            textBox_Copy2.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            textBox_Copy2.Opacity = 1;

        }

        private void CheckBox_Checked1(object sender, RoutedEventArgs e)
        {
            var id_del = DataGrid1.SelectedIndex;
            var ci = new DataGridCellInfo(DataGrid1.Items[id_del], DataGrid1.Columns[2]);
            var content = ci.Column.GetCellContent(ci.Item) as TextBlock;

            for_price -= Convert.ToInt32(content.Text);
            textBox_Copy2.Text = for_price.ToString();

            strArr[id_del] = null;

            var bc = new BrushConverter();

            ComboBox.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            ComboBox.Opacity = 1;

            data.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            data.Opacity = 1;

            textBox_Copy2.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            textBox_Copy2.Opacity = 1;

        }

        private async void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox.Text == "")
            {
                ComboBox.Background = Brushes.Red;
                ComboBox.Opacity = 0.4;
                return;
            }
            if (data.Text == "")
            {
                data.Background = Brushes.Red;
                data.Opacity = 0.4;
                return;
            }
            if (textBox_Copy2.Text == "0")
            {
                textBox_Copy2.Background = Brushes.Red;
                textBox_Copy2.Opacity = 0.4;
                return;
            }

            SqlConnection = new OleDbConnection(connectString);
            SqlConnection.Open();

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Салон]", SqlConnection);

            command1 = new OleDbCommand("INSERT INTO [Заказы] (Клиент,Услуги,[Дата создания],[Дата выполнения],Цена)VALUES(@1,@2,@3,@4,@5)", SqlConnection);
            command1.Parameters.AddWithValue("1", ComboBox.Text);
            
            string for_str = "";
            for (int i = 0; i < strArr.Count(); i++)
            {
                if (strArr[i] != null)
                {
                    for_str += strArr[i] + " - ";
                }
            }
            command1.Parameters.AddWithValue("2", for_str);
            command1.Parameters.AddWithValue("3", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " " + DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year);
            command1.Parameters.AddWithValue("4", data.Text);
            command1.Parameters.AddWithValue("5", textBox_Copy2.Text);

            await command1.ExecuteNonQueryAsync();

            MessageBox.Show("Данные успешно добавлены");

            AddPageOrder a = new AddPageOrder();
            a.Tag = this.Tag;
            a.Show();
            this.Close();
        }

        private void Data_CalendarOpened(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            ComboBox.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            ComboBox.Opacity = 1;

            data.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            data.Opacity = 1;

            textBox_Copy2.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
            textBox_Copy2.Opacity = 1;
        }
    }
}
