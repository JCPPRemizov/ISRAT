using ISRAT.Windows;
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

namespace ISRAT.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminStartPage.xaml
    /// </summary>
    public partial class AdminStartPage : Page
    {
        public static Administrator administrator;
        public AdminStartPage()
        {
            InitializeComponent();
        }

        private void ResourcesButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(1);
        }
        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(2);
        }

        private void ProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(3);
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(4);
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(5);
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            administrator.ChangeFrame(6);
        }

        private void ResourcesAllocationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResourcesOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
