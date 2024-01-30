using ISRAT.DataSet1TableAdapters;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ISRAT.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();
        RolesTableAdapter rolesTableAdapter = new RolesTableAdapter();
        public UsersPage()
        {
            InitializeComponent();
            UpdateDataGrid();

            ColumnNameBox.ItemsSource = UsersDataGrid.Columns;
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
            ColumnNameBox.DisplayMemberPath = "Header";

            RoleIDBox.ItemsSource = rolesTableAdapter.GetData();
            RoleIDBox.SelectedValuePath = "ID";
            RoleIDBox.DisplayMemberPath = "Name";
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView userRowView = UsersDataGrid.SelectedItem as DataRowView;
            if (UsersDataGrid.SelectedItem != null)
            {
                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        usersTableAdapter.DeleteQuery(int.Parse(userRowView.Row[0].ToString()));
                        UpdateDataGrid();
                        break;
                }

            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (FiledsCheck())
            {
                DataRowView userRowView = UsersDataGrid.SelectedItem as DataRowView;
                if (UsersDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            usersTableAdapter.UpdateQuery(NameBox.Text, SurnameBox.Text, MiddleNameBox.Text, LoginBox.Text, PasswordBox.Text, int.Parse(RoleIDBox.SelectedValue.ToString()), int.Parse(userRowView.Row[0].ToString()));
                            UpdateDataGrid();
                            ClearFields();
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("Выберите запись для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (FiledsCheck())
            {

                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        usersTableAdapter.InsertQuery(NameBox.Text, SurnameBox.Text, MiddleNameBox.Text, LoginBox.Text,
                            PasswordBox.Text, (int)RoleIDBox.SelectedValue);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            switch (ColumnNameBox.SelectedValue)
            {
                case 0:
                    {
                        int.TryParse(FindBox.Text, out result);
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
                case 2:
                    {
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableBySurname(FindBox.Text);
                        break;
                    }
                case 3:
                    {
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByMiddleName(FindBox.Text);
                        break;
                    }
                case 4:
                    {
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByLogin(FindBox.Text);
                        break;
                    }
                case 5:
                    {
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByPassword(FindBox.Text);
                        break;
                    }
                case 6:
                    {
                        int.TryParse(FindBox.Text, out result);
                        UsersDataGrid.ItemsSource = usersTableAdapter.GetSortedTableByRoleID(result);
                        break;
                    }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView userRowView = UsersDataGrid.SelectedItem as DataRowView;
            if (UsersDataGrid.SelectedItem != null)
            {
                NameBox.Text = userRowView.Row[1].ToString();
                SurnameBox.Text = userRowView.Row[2].ToString();
                MiddleNameBox.Text = userRowView[3].ToString();
                LoginBox.Text = userRowView[4].ToString();
                PasswordBox.Text = userRowView[5].ToString();
                RoleIDBox.SelectedValue = userRowView[6].ToString();
            }
        }

        private void UpdateDataGrid()
        {
            UsersDataGrid.ItemsSource = usersTableAdapter.GetData();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            SurnameBox.Text = string.Empty;
            MiddleNameBox.Text = string.Empty;
            LoginBox.Text= string.Empty;
            PasswordBox.Text = string.Empty;
            RoleIDBox.SelectedValue = null;
        }

        private bool FiledsCheck()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(SurnameBox.Text)
                && !string.IsNullOrEmpty(LoginBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text) && RoleIDBox.SelectedValue != null)
            {
                string loginPattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]{6,20}$";
                string passwordPattern = @"^(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{6,20}$";
                if (LoginBox.Text.Length < 6)
                {
                    MessageBox.Show("Логин должен быть от 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (PasswordBox.Text.Length < 6)
                {
                    MessageBox.Show("Пароль должен быть от 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (Regex.IsMatch(PasswordBox.Text, passwordPattern) && Regex.IsMatch(LoginBox.Text, loginPattern))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Длина логина и пароля должна быть от 6 до 20 символов\nПароль должен иметь хотя бы одну цифру и спец.символ\nРазрешены только английские символы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}
