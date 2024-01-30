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
    /// Логика взаимодействия для RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        RolesTableAdapter rolesTableAdapter = new RolesTableAdapter();
        public RolesPage()
        {
            InitializeComponent();
            UpdateDataGrid();

            ColumnNameBox.ItemsSource = RolesDataGrid.Columns;
            ColumnNameBox.DisplayMemberPath = "Header";
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
        }

        private bool FieldsCheck()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(DescriptionBox.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void UpdateDataGrid()
        {
            RolesDataGrid.ItemsSource = rolesTableAdapter.GetData();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        rolesTableAdapter.InsertQuery(NameBox.Text, DescriptionBox.Text);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView resourceRowView = RolesDataGrid.SelectedItem as DataRowView;
                if (RolesDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            rolesTableAdapter.UpdateQuery(NameBox.Text, DescriptionBox.Text, int.Parse(resourceRowView.Row[0].ToString()));
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

        private void DeleteRoleButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView roleRowView = RolesDataGrid.SelectedItem as DataRowView;
            if (RolesDataGrid.SelectedItem != null)
            {
                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        rolesTableAdapter.DeleteQuery(int.Parse(roleRowView.Row[0].ToString()));
                        UpdateDataGrid();
                        break;
                }

            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        RolesDataGrid.ItemsSource = rolesTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        RolesDataGrid.ItemsSource = rolesTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
                case 2:
                    {
                        RolesDataGrid.ItemsSource = rolesTableAdapter.GetSortedTableByDesc(FindBox.Text);
                        break;
                    }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }

        private void RolesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView roleRowView = RolesDataGrid.SelectedItem as DataRowView;
            if (RolesDataGrid.SelectedItem != null)
            {
                NameBox.Text = roleRowView.Row[1].ToString();
                DescriptionBox.Text = roleRowView.Row[2].ToString();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}
