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
using System.Windows.Shapes;
using WorldResort.UserPages;

namespace WorldResort
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            Manager.userFrameWindow = frameUserWindow;
            Manager.userFrameWindow.Navigate(new ResortUserPage());
        }

        private void buttonProfileUserWindow_Click(object sender, RoutedEventArgs e)
        {
            Manager.userFrameWindow.Navigate(new ProfileUserPage());
        }

        private void buttonResortUserWindow_Click(object sender, RoutedEventArgs e)
        {
            Manager.userFrameWindow.Navigate(new ResortUserPage());
        }

        private void buttonFavouriteUserWindow_Click(object sender, RoutedEventArgs e)
        {
            Manager.userFrameWindow.Navigate(new FavouriteUserPage());
        }
    }
}
