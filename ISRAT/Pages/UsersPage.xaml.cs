using ISRAT.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ISRAT.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();
        public UsersPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView userRowView = UsersDataGrid.SelectedItem as DataRowView;
            if (UsersDataGrid.SelectedItem != null)
            {
                string sMessageBoxText = "Вы уверены, что хотите удалить запись?";
                string sCaption = "Удаление";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
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

        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFieldsEmpty())
            {
                string sMessageBoxText = "Вы уверены, что хотите добавить запись?";
                string sCaption = "Добавление";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        usersTableAdapter.InsertQuery(NameBox.Text, SurnameBox.Text, MiddleNameBox.Text, LoginBox.Text,
                            PasswordBox.Text, (int)RoleIDBox.SelectedValue);
                        UpdateDataGrid();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void UpdateDataGrid()
        {
            UsersDataGrid.ItemsSource = usersTableAdapter.GetData();
        }

        private bool IsFieldsEmpty()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(SurnameBox.Text) && !string.IsNullOrEmpty(MiddleNameBox.Text)
                && !string.IsNullOrEmpty(LoginBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text) && RoleIDBox.SelectedValue != null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
