using ISRAT.DataSet1TableAdapters;
using ISRAT.Windows;
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
    /// Логика взаимодействия для ResourcesPage.xaml
    /// </summary>
    public partial class ResourcesPage : Page
    {
        private ResourcesTableAdapter resourcesTableAdapter = new ResourcesTableAdapter();
        public static Administrator administrator;

        public ResourcesPage()
        {
            InitializeComponent();
            ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetData();
            ColumnNameBox.ItemsSource = ResourcesDataGrid.Columns;
            ColumnNameBox.DisplayMemberPath = "Header";
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
        }

        private bool FieldsCheck()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(CharacteristicsBox.Text)
              && !string.IsNullOrEmpty(Quantity.Text) && !string.IsNullOrEmpty(Type.Text))
            {
                return true;
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

        private void ChangeResourceButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView resourceRowView = ResourcesDataGrid.SelectedItem as DataRowView;
                if (ResourcesDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            resourcesTableAdapter.UpdateQuery(NameBox.Text, CharacteristicsBox.Text, int.Parse(Quantity.Text), Type.Text, int.Parse(resourceRowView.Row[0].ToString()));
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

        private void AddResourceButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {

                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesTableAdapter.InsertQuery(NameBox.Text, CharacteristicsBox.Text, int.Parse(Quantity.Text), Type.Text);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void DeleteResourceButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView resourceRowView = ResourcesDataGrid.SelectedItem as DataRowView;
            if (ResourcesDataGrid.SelectedItem != null)
            {

                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        resourcesTableAdapter.DeleteQuery(int.Parse(resourceRowView.Row[0].ToString()));
                        UpdateDataGrid();
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Разрешены только цифры!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ResourcesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView resourceRowView = ResourcesDataGrid.SelectedItem as DataRowView;
            if (ResourcesDataGrid.SelectedItem != null)
            {
                NameBox.Text = resourceRowView.Row[1].ToString();
                CharacteristicsBox.Text = resourceRowView.Row[2].ToString();
                Quantity.Text = resourceRowView.Row[3].ToString();
                Type.Text = resourceRowView.Row[4].ToString();
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
                        ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                    case 1:
                    {
                        ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
                    case 2:
                    {
                        ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetSortedTableByCharacteristics(FindBox.Text);
                        break;
                    }
                    case 3:
                    {
                        int.TryParse(FindBox.Text, out result);
                        ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetSortedTableByQuantity(result);
                        break;
                    }
                    case 4:
                    {
                        ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetSortedTableByType(FindBox.Text);
                        break;
                    }
            }
            
        }

        private void UpdateDataGrid()
        {
            ResourcesDataGrid.ItemsSource = resourcesTableAdapter.GetData();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            CharacteristicsBox.Text = string.Empty;
            Quantity.Text = string.Empty;
            Type.Text = string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }
    } 
}
