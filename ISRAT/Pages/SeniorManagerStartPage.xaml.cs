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
    /// Логика взаимодействия для SeniorManagerStartPage.xaml
    /// </summary>
    public partial class SeniorManagerStartPage : Page
    {
        public static SeniorManager seniorManager;
        public SeniorManagerStartPage()
        {
            InitializeComponent();
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            seniorManager.ChangeFrame(1);
        }
        private void ResourcesAllocationButton_Click(object sender, RoutedEventArgs e)
        {
            seniorManager.ChangeFrame(2);
        }

        private void ResourcesOrderButton_Click(object sender, RoutedEventArgs e)
        {
            seniorManager.ChangeFrame(3);
        }        
    }
}
