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
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        public string for_path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("i") - 1);

        public string connectString;

        OleDbConnection SqlConnection;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectString = for_path + @"Content\giphy.gif";

            connectString = connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + for_path + @"/bdmain.mdb";

            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("SELECT * FROM [Пользователи]", SqlConnection);

            sqlReader = command1.ExecuteReader();

            while (sqlReader.Read())
            {
                if (this.Name == Convert.ToString(sqlReader["Логин"]))
                {
                    textBox.Text = Convert.ToString(sqlReader["Логин"]);
                    textBox_Copy.Text = Convert.ToString(sqlReader["ФИО"]);
                    passwordBox.Password = Convert.ToString(sqlReader["Пароль"]);
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection = new OleDbConnection(connectString);
            await SqlConnection.OpenAsync();
            OleDbDataReader sqlReader = null;

            OleDbCommand command1 = new OleDbCommand("UPDATE [Пользователи] SET [Логин]=@LN, [Пароль]=@FN, ФИО=@Fam WHERE [Логин]=@1", SqlConnection);
            command1.Parameters.AddWithValue("LN", textBox.Text);
            command1.Parameters.AddWithValue("FN", passwordBox.Password.ToString());
            command1.Parameters.AddWithValue("Fam", textBox_Copy.Text);

            command1.Parameters.AddWithValue("1", this.Name);


            if (textBox.Text != "")
            {
                if (passwordBox.Password.ToString() != "")
                {
                    if (textBox_Copy.Text != "")
                    {
                        await command1.ExecuteNonQueryAsync();
                        MessageBox.Show("Данные пользователя успешно изменены");
                        FirstPage o = new FirstPage();
                        o.Name = this.Name;
                        o.Show();
                        this.Hide();
                        return;
                    }
                }
            }

            textBox_Copy.Background = Brushes.Red;
            textBox_Copy.Opacity = 0.4;

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
            textBox_Copy.Background = (Brush)bc.ConvertFrom("#FF181818");
            textBox_Copy.Opacity = 1;
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
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            FirstPage a = new FirstPage();
            a.Name = this.Name;
            a.Show();
            this.Close();
        }
    }
}
