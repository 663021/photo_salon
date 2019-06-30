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
    public partial class MainWindow : Window
    {
        public MainWindow()
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

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Пользователи]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                if (textBox.Text == Convert.ToString(sqlReader["Логин"]))
                {
                    if (passwordBox.Password.ToString() == Convert.ToString(sqlReader["Пароль"]))
                    {
                        FirstPage o = new FirstPage();
                        o.Name = textBox.Text;
                        o.Show();
                        this.Hide();
                        return;
                    }
                }
            }

            textBox.Background = Brushes.Red;
            textBox.Opacity = 0.4;

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
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            textBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox.Opacity = 1;
            passwordBox.Background = (Brush)bc.ConvertFrom("#FF181818");
            passwordBox.Opacity = 1;
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            RegWindow a = new RegWindow();
            a.Show();
            this.Close();
        }
    }
}
