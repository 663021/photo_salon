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
    public partial class EditPageSevices : Window
    {
        public EditPageSevices()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        OleDbConnection SqlConnection;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = for_path + @"Content\68c4ae27d6e34.gif";

            image1.Source = new BitmapImage(new Uri(connectString));
            image.Source = new BitmapImage(new Uri(for_path + @"Content\rg5fgsdh53gd55t.png"));
            mediaElement.Source = new Uri(connectString);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Play();

            connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Услуги]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                ComboBox.Items.Add(Convert.ToString(sqlReader["Наименование"]));
            }
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
                TablePageServices o = new TablePageServices();
                o.Show();
                this.Hide();
            }
        }

        private void MediaElement_MediaEnded_3(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, 0, 0, 1);
            mediaElement.Play();
        }

        private void MediaElement_MediaOpened_3(object sender, RoutedEventArgs e)
        {
            image1.Visibility = Visibility.Hidden;
        }

        private async void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Услуги]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                if (ComboBox.Text == Convert.ToString(sqlReader["Наименование"]))
                {
                    textBox_Copy.Text = Convert.ToString(sqlReader["Наименование"]);
                    textBox_Copy1.Text = Convert.ToString(sqlReader["Тип услуги"]);
                    textBox_Copy3.Text = Convert.ToString(sqlReader["Стоимость"]);
                    textBox_Copy4.Text = Convert.ToString(sqlReader["Срок"]);
                }
            }
        }

        private async void Button_Copy16_Click_1_1_22(object sender, RoutedEventArgs e)
        {
            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("UPDATE [Услуги] SET [Наименование]=@LN, [Тип услуги]=@FN, Стоимость=@Fam, Срок=@Adress WHERE [Наименование]=@7", SqlConnection);
            command1.Parameters.AddWithValue("LN", textBox_Copy.Text);
            command1.Parameters.AddWithValue("FN", textBox_Copy1.Text);
            command1.Parameters.AddWithValue("Fam", textBox_Copy3.Text);
            command1.Parameters.AddWithValue("Adress", textBox_Copy4.Text);
            command1.Parameters.AddWithValue("7", ComboBox.Text);

            if (textBox_Copy.Text != "")
            {
                if (textBox_Copy1.Text != "")
                {
                    if (textBox_Copy4.Text != "")
                    {
                        if (textBox_Copy3.Text != "")
                        {
                            await command1.ExecuteNonQueryAsync();
                            MessageBox.Show("Данные успешно изменены");
                            textBox_Copy.Text = "";
                            textBox_Copy1.Text = "";
                            textBox_Copy3.Text = "";
                            textBox_Copy4.Text = "";

                            ComboBox.Text = "";

                            return;
                        }
                    }
                }
            }

            textBox_Copy.Background = Brushes.Red;
            textBox_Copy.Opacity = 0.4;
            textBox_Copy1.Background = Brushes.Red;
            textBox_Copy1.Opacity = 0.4;
            textBox_Copy3.Background = Brushes.Red;
            textBox_Copy3.Opacity = 0.4;
            textBox_Copy4.Background = Brushes.Red;
            textBox_Copy4.Opacity = 0.4;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();


            textBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy.Opacity = 1;
            textBox_Copy1.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy1.Opacity = 1;
            textBox_Copy4.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy4.Opacity = 1;
            textBox_Copy3.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy3.Opacity = 1;
        }
    }
}
