using ISRAT.DataSet1TableAdapters;
using ISRAT.Model;
using ISRAT.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ISRAT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Administrator administratorWindow = new Administrator();
        SeniorManager seniorManager = new SeniorManager();
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void HideAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginTextBox.Text) && !string.IsNullOrEmpty(PassTextBox.Password))
            {
                string loginPattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]{6,20}$";
                string passwordPattern = @"^(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{6,20}$";
                if (Regex.IsMatch(PassTextBox.Password, passwordPattern) && Regex.IsMatch(LoginTextBox.Text, loginPattern))
                {
                    switch(FindUser(LoginTextBox.Text, PassTextBox.Password))
                    {
                        case 3:
                            {
                                Hide();
                                Administrator.mainWindow = this;
                                administratorWindow.Show();
                                break;
                            }
                        case 1005:
                            {
                                Hide();
                                SeniorManager.mainWindow = this;
                                seniorManager.Show();
                                break;
                            }
                        case -1:
                            {
                                MessageBox.Show("Пользователь не найден", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("Длина логина и пароля должна быть от 6 до 20 символов\nПароль должен иметь хотя бы одну цифру и спец.символ\nРазрешены только английские символы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int FindUser(in string login, in string password)
        {
            int userRoleID;
            DataTable authTable = usersTableAdapter.GetData();
            for (int i = 0; i < authTable.Rows.Count; i++)
            {
                if (login == (string)authTable.Rows[i][4])
                {
                    if (password == (string)authTable.Rows[i][5])
                    {
                        CurrentUser.RoleID = Convert.ToInt32(authTable.Rows[i][6]);
                        CurrentUser.UserID = Convert.ToInt32(authTable.Rows[i][0]);
                        userRoleID = Convert.ToInt32(authTable.Rows[i][6]);
                        return userRoleID;
                    }
                }
            }
            return -1;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //LoginTextBox.Text = "";
            //PassTextBox.Password = "";
        }
    }
}
