using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using Tutorial.SqlConn;

namespace Tutorial.SqlConn
{
    class DBMySQLUtils
    {

        public static MySqlConnection
                 GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

    }
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "194.28.213.54";
            int port = 3306;
            string database = "help_log";
            string username = "root";
            string password = "awds12qe";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}


namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для List.xaml
    /// </summary>
    public partial class List : Window
    {
        private void Send_Message(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                conn.Open();
                if (textBoxCabinet.Text != "" && textBoxHelp.Text != "")
                {

                    string sql = "INSERT INTO `help_log`.`appeal` (`ID` , `cause` , `cabinet` , `date` , `comment`)VALUES( NULL, '" + textBoxHelp.Text + "', '" + textBoxCabinet.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'Не рассмотренно');";

                    MySqlCommand command = new MySqlCommand(sql, conn);


                    int a = command.ExecuteNonQuery();
                    conn.Close();
                    if (a == 0)
                        MessageBox.Show("Что-то пошло не так");
                    else
                    {
                        MessageBox.Show("Ваше обращение успешно отправлено в " + DateTime.Now.ToString("HH:mm"));
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Заполните пустые поля");
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

        public List()
        {
            InitializeComponent();
            textBoxCabinet.Text = MainWindow.Number_Cabinet;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow _mainWindow = new MainWindow();
            _mainWindow.Show();
            this.Close();
        }
    }
}
