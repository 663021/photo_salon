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
    public partial class EditPageProduct : Window
    {
        public EditPageProduct()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        OleDbConnection SqlConnection;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = for_path + @"Content\68c4ae27d6e3467.gif";

            image1.Source = new BitmapImage(new Uri(connectString));
            image.Source = new BitmapImage(new Uri(for_path + @"Content\new-product.png"));
            mediaElement.Source = new Uri(connectString);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Play();

            connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Товары]", SqlConnection);

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
                TablePageProduct o = new TablePageProduct();
                o.Show();
                this.Hide();
            }
        }

        private void MediaElement_MediaEnded_1_3(object sender, RoutedEventArgs e)
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

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Товары]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                if (ComboBox.Text == Convert.ToString(sqlReader["Наименование"]))
                {
                    textBox_Copy.Text = Convert.ToString(sqlReader["Тип товара"]);
                    textBox_Copy1.Text = Convert.ToString(sqlReader["Наименование"]);
                    textBox_Copy2.Text = Convert.ToString(sqlReader["Количество"]);
                    textBox_Copy3.Text = Convert.ToString(sqlReader["Цена"]);
                }
            }
        }

        private async void Button_Copy16_Click_1_1_11(object sender, RoutedEventArgs e)
        {
            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("UPDATE [Товары] SET [Тип товара]=@LN, [Наименование]=@FN, Количество=@Fam, Цена=@Adress WHERE [Наименование]=@7", SqlConnection);
            command1.Parameters.AddWithValue("LN", textBox_Copy.Text);
            command1.Parameters.AddWithValue("FN", textBox_Copy1.Text);
            command1.Parameters.AddWithValue("Fam", textBox_Copy2.Text);
            command1.Parameters.AddWithValue("Adress", textBox_Copy3.Text);
            command1.Parameters.AddWithValue("7", ComboBox.Text);

            if (textBox_Copy.Text != "")
            {
                if (textBox_Copy1.Text != "")
                {
                    if (textBox_Copy2.Text != "")
                    {
                        if (textBox_Copy3.Text != "")
                        {
                            await command1.ExecuteNonQueryAsync();
                            MessageBox.Show("Данные успешно изменены");
                            textBox_Copy.Text = "";
                            textBox_Copy1.Text = "";
                            textBox_Copy2.Text = "";
                            textBox_Copy3.Text = "";

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
            textBox_Copy2.Background = Brushes.Red;
            textBox_Copy2.Opacity = 0.4;
            textBox_Copy3.Background = Brushes.Red;
            textBox_Copy3.Opacity = 0.4;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();


            textBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy.Opacity = 1;
            textBox_Copy1.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy1.Opacity = 1;
            textBox_Copy2.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy2.Opacity = 1;
            textBox_Copy3.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy3.Opacity = 1;
        }
    }
}
