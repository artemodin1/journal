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
using Tutorial.SqlConn;
using MySql.Data.MySqlClient;
using System.IO;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string number_Cabinet;
        public static string Number_Cabinet { get => number_Cabinet; set => number_Cabinet = value; }

        FileStream file1 = new FileStream("Nickname.txt", FileMode.OpenOrCreate); //создаем файловый поток
        public MainWindow()
        {
            InitializeComponent();
            StreamReader reader_file = new StreamReader(file1);
            number_Cabinet = reader_file.ReadToEnd();
            if (number_Cabinet == "")
                ToLogin();
            reader_file.Close();
            Room.Header = "Ваш кабинет: " + number_Cabinet;
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                string sql = "SELECT * FROM appeal where cabinet = '" + number_Cabinet + "';";

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    myPanel.Children.Add(new Label { Content = reader[1].ToString() + " | " + reader[3].ToString() + " | " + reader[4].ToString()});
                    count++;
                }
                if (count == 0) 
                { 
                    myPanel.Children.Add(new Label { Content = "Из заданного кабинета еще не поступало запросов"});                  
                }
                reader.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    MessageBox.Show("Не удалось подключиться к серверу!");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ToLogin(object sender, RoutedEventArgs e) 
        {
            ToLogin();
        }

        private void ToLogin()
        {
            login _login = new login();
            _login.Show();
            this.Close();
        }

        private void NewRequest(object sender, RoutedEventArgs e)
        {

            List _list = new List();
            _list.Show();
            this.Close();
        }
    }
}
