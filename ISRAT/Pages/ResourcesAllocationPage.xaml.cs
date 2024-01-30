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
    /// Логика взаимодействия для ResourcesAllocationPage.xaml
    /// </summary>
    public partial class ResourcesAllocationPage : Page
    {
        private TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
        private ResourcesTableAdapter resourcesTableAdapter = new ResourcesTableAdapter();
        private ResourcesAllocationTableAdapter resourcesAllocationTableAdapter = new ResourcesAllocationTableAdapter();

        public ResourcesAllocationPage()
        {
            InitializeComponent();
        }

        private void UpdateDataGrid()
        {
            ResAllDataGrid.ItemsSource = resourcesAllocationTableAdapter.GetData();
        }
        private bool FieldsCheck()
        {
            if (!string.IsNullOrEmpty(QuantityBox.Text) && ResourceIDBox.SelectedValue != null && TaskIDBox.SelectedValue != null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ClearFields()
        {
            TaskIDBox.SelectedValue = null;
            ResourceIDBox.SelectedValue = null;
            QuantityBox.Text = "";
        }

        private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Разрешены только цифры!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResAllDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView resourceAllRowView = ResAllDataGrid.SelectedItem as DataRowView;
            if (ResAllDataGrid.SelectedItem != null)
            {
                TaskIDBox.SelectedValue = resourceAllRowView.Row[1].ToString();
                ResourceIDBox.SelectedValue = resourceAllRowView.Row[2].ToString();
                QuantityBox.Text = resourceAllRowView.Row[3].ToString();
            }
        }

        private void AddResAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                int availableResource;   
                int.TryParse(resourcesTableAdapter.GetSortedTableByID(int.Parse(ResourceIDBox.SelectedValue.ToString())).Rows[0][3].ToString(), out availableResource);
                if (int.Parse(QuantityBox.Text) > availableResource)
                {
                    MessageBox.Show("Количество распределяемого ресурса превышают доступное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesAllocationTableAdapter.InsertQuery(int.Parse(TaskIDBox.SelectedValue.ToString()), int.Parse(ResourceIDBox.SelectedValue.ToString()), int.Parse(QuantityBox.Text));
                        availableResource = availableResource - int.Parse(QuantityBox.Text);
                        resourcesTableAdapter.UpdateQuantity(availableResource, int.Parse(ResourceIDBox.SelectedValue.ToString()));
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeResAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView resourceAllRowView = ResAllDataGrid.SelectedItem as DataRowView;
                if (ResAllDataGrid.SelectedItem != null)
                {
                    int availableResource;
                    int.TryParse(resourcesTableAdapter.GetSortedTableByID(int.Parse(ResourceIDBox.SelectedValue.ToString())).Rows[0][3].ToString(), out availableResource);
                    if (int.Parse(QuantityBox.Text) > availableResource)
                    {
                        MessageBox.Show("Количество распределяемого ресурса превышают доступное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            resourcesAllocationTableAdapter.UpdateQuery(int.Parse(TaskIDBox.SelectedValue.ToString()), int.Parse(ResourceIDBox.SelectedValue.ToString()), int.Parse(QuantityBox.Text), int.Parse(resourceAllRowView.Row[0].ToString()));
                            availableResource = availableResource - int.Parse(QuantityBox.Text);
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

        private void DeleteResAllButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView resourceAllRowView = ResAllDataGrid.SelectedItem as DataRowView;
            if (ResAllDataGrid.SelectedItem != null)
            {
                int availableResource;
                int.TryParse(resourcesTableAdapter.GetSortedTableByID(int.Parse(ResourceIDBox.SelectedValue.ToString())).Rows[0][3].ToString(), out availableResource);
                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesAllocationTableAdapter.DeleteQuery(int.Parse(resourceAllRowView.Row[0].ToString()));
                        availableResource = availableResource + int.Parse(resourceAllRowView.Row[3].ToString());
                        resourcesTableAdapter.UpdateQuantity(availableResource, int.Parse(ResourceIDBox.SelectedValue.ToString()));
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
                        ResAllDataGrid.ItemsSource = resourcesAllocationTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResAllDataGrid.ItemsSource = resourcesAllocationTableAdapter.GetSortedTableByTaskID(result);
                        break;
                    }
                case 2:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResAllDataGrid.ItemsSource = resourcesAllocationTableAdapter.GetSortedTableByResourceID(result);
                        break;
                    }
                case 3:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResAllDataGrid.ItemsSource = resourcesAllocationTableAdapter.GetSortedTableByQuantity(result);
                        break;
                    }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();

            ColumnNameBox.ItemsSource = ResAllDataGrid.Columns;
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
            ColumnNameBox.DisplayMemberPath = "Header";

            TaskIDBox.ItemsSource = tasksTableAdapter.GetData();
            TaskIDBox.DisplayMemberPath = "Name";
            TaskIDBox.SelectedValuePath = "ID";

            ResourceIDBox.ItemsSource = resourcesTableAdapter.GetData();
            ResourceIDBox.DisplayMemberPath = "Name";
            ResourceIDBox.SelectedValuePath = "ID";
        }
    }
}
