using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WorldResort
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            Manager.adminFrameWindow = frameMainWindow;
        }
        private void buttonGetResortMainWindow_Click(object sender, RoutedEventArgs e)
        {
            frameMainWindow.Navigate(new AdminPages.GetCityResortAdminPage());
        }

        private void buttonPostResortMainWindow_Click(object sender, RoutedEventArgs e)
        {
            frameMainWindow.Navigate(new AdminPages.PostCityResortAdminPage());
        }

        private void buttonGetCountryMainWindow_Click(object sender, RoutedEventArgs e)
        {
            frameMainWindow.Navigate(new AdminPages.GetCountryAdminPage());
        }

        private void buttonPostHotelMainWindow_Click(object sender, RoutedEventArgs e)
        {
            frameMainWindow.Navigate(new AdminPages.PostHotelResortAdminPage());
        }
    }
}
