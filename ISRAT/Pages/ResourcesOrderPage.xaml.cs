using ISRAT.DataSet1TableAdapters;
using ISRAT.Model;
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
    /// Логика взаимодействия для ResourcesOrderPage.xaml
    /// </summary>
    public partial class ResourcesOrderPage : Page
    {
        ResourcesOrderTableAdapter resourcesOrderTableAdapter = new ResourcesOrderTableAdapter();
        TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
        StatusTableAdapter statusTableAdapter = new StatusTableAdapter();
        ResourcesTableAdapter resourcesTableAdapter = new ResourcesTableAdapter();
        public ResourcesOrderPage()
        {
            InitializeComponent();
        }

        private bool FieldsCheck()
        {
            if(TaskIDBox.SelectedValue != null && ResourceIDBox.SelectedValue != null && StatusIDBox.SelectedValue != null) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateDataGrid()
        {
            ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetData();
        }

        private void ClearFields()
        {
            ResourceIDBox.SelectedValue = null;
            StatusIDBox.SelectedValue = null;
            TaskIDBox.SelectedValue = null;

            QuantityBox.Text = string.Empty;
        }

        private void AddResOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesOrderTableAdapter.InsertQuery(int.Parse(ResourceIDBox.SelectedValue.ToString()), int.Parse(QuantityBox.Text), int.Parse(TaskIDBox.SelectedValue.ToString()),
                            int.Parse(StatusIDBox.SelectedValue.ToString()), CurrentUser.UserID);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView resourceOrderRowView = ResOrderDataGrid.SelectedItem as DataRowView;
                if (ResOrderDataGrid.SelectedItem != null)
                {
                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            resourcesOrderTableAdapter.UpdateQuery(int.Parse(ResourceIDBox.SelectedValue.ToString()), int.Parse(QuantityBox.Text), int.Parse(TaskIDBox.SelectedValue.ToString()),
                            int.Parse(StatusIDBox.SelectedValue.ToString()), int.Parse(resourceOrderRowView.Row[0].ToString()));
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

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView resourceOrderRowView = ResOrderDataGrid.SelectedItem as DataRowView;
            if (ResOrderDataGrid.SelectedItem != null)
            {
                
                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesOrderTableAdapter.DeleteQuery(int.Parse(resourceOrderRowView.Row[0].ToString()));
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
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByResourceID(result);
                        break;
                    }
                case 2:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByQuantity(result);
                        break;
                    }
                case 3:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByTaskID(result);
                        break;
                    }
                case 4:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByStatusID(result);
                        break;
                    }
                case 5:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResOrderDataGrid.ItemsSource = resourcesOrderTableAdapter.GetSortedTableByUserID(result);
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
            if (CurrentUser.RoleID == 3)
            {
                AdminStartPage.administrator.ChangeFrame(0);
            }
            else if (CurrentUser.RoleID == 1005)
            {
                SeniorManagerStartPage.seniorManager.ChangeFrame(0);
            }
        }

        private void QuantityBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Разрешены только цифры!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            ColumnNameBox.ItemsSource = ResOrderDataGrid.Columns;
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
            ColumnNameBox.DisplayMemberPath = "Header";

            ResourceIDBox.ItemsSource = resourcesTableAdapter.GetData();
            ResourceIDBox.DisplayMemberPath = "Name";
            ResourceIDBox.SelectedValuePath = "ID";

            StatusIDBox.ItemsSource = statusTableAdapter.GetData();
            StatusIDBox.DisplayMemberPath = "Name";
            StatusIDBox.SelectedValuePath = "ID";

            TaskIDBox.ItemsSource = tasksTableAdapter.GetData();
            TaskIDBox.DisplayMemberPath = "Name";
            TaskIDBox.SelectedValuePath = "ID";
        }

        private void ResOrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView resourceOrderRowView = ResOrderDataGrid.SelectedItem as DataRowView;
            if (ResOrderDataGrid.SelectedItem != null)
            {
                ResourceIDBox.SelectedValue = resourceOrderRowView.Row[1].ToString();
                QuantityBox.Text = resourceOrderRowView.Row[2].ToString();
                TaskIDBox.SelectedValue = resourceOrderRowView.Row[3].ToString();
                StatusIDBox.SelectedValue = resourceOrderRowView.Row[4].ToString();
            }
        }
    }
}
