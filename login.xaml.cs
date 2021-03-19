using System;
using System.Collections.Generic;
using System.IO;
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

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
            number_cabinet.Text = MainWindow.Number_Cabinet;
        }
        public void Set_Cabinet()
        {
            if (number_cabinet.Text != "")
            {
                MainWindow.Number_Cabinet = number_cabinet.Text;
                File.WriteAllText(@"Nickname.txt", string.Empty);
                File.WriteAllText(@"Nickname.txt", MainWindow.Number_Cabinet);
                MainWindow _mainWindow = new MainWindow();
                _mainWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Поле пустое.");
        }

        public void Press(object sender, RoutedEventArgs e) 
        {
            Set_Cabinet();
        }

        private void Stack_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Set_Cabinet();
            }
        }
    }
}
