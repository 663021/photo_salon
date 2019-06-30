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
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        OleDbConnection SqlConnection;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = for_path + @"Content\giphy.gif";
            mediaElement.Source = new Uri(connectString);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Play();

            image.Source = new BitmapImage(new Uri(connectString));

            connectString = connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, 0, 0, 1);
            mediaElement.Play();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("INSERT INTO [Пользователи] ([Логин], [Пароль], ФИО)VALUES(@LN, @FN, @Fam)", SqlConnection);
            command1.Parameters.AddWithValue("LN", textBox.Text);
            command1.Parameters.AddWithValue("FN", passwordBox.Password.ToString());
            command1.Parameters.AddWithValue("Fam", textBox_Copy.Text);


            if (textBox.Text != "")
            {
                if (passwordBox.Password.ToString() != "")
                {
                    if (textBox_Copy.Text != "")
                    {
                        if (passwordBox_Copy.Password.ToString() != "")
                        {
                            if (passwordBox_Copy.Password.ToString() == passwordBox.Password.ToString())
                            {
                                await command1.ExecuteNonQueryAsync();
                                MessageBox.Show("Пользватель успешно зарегестрирован");
                                MainWindow o = new MainWindow();
                                o.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }
                }
            }

            textBox_Copy.Background = Brushes.Red;
            textBox_Copy.Opacity = 0.4;

            textBox.Background = Brushes.Red;
            textBox.Opacity = 0.4;

            passwordBox_Copy.Background = Brushes.Red;
            passwordBox_Copy.Opacity = 0.4;

            passwordBox.Background = Brushes.Red;
            passwordBox.Opacity = 0.4;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();

            textBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox.Opacity = 1;
            passwordBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            passwordBox.Opacity = 1;
            textBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy.Opacity = 1;
            passwordBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            passwordBox_Copy.Opacity = 1;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            textBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox.Opacity = 1;
            passwordBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            passwordBox.Opacity = 1;
            textBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy.Opacity = 1;
            passwordBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            passwordBox_Copy.Opacity = 1;
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            this.Close();
        }
    }
}
