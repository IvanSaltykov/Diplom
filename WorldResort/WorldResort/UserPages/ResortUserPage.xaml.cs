using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
using WorldResort.Model;
using WorldResort.UserDialog;

namespace WorldResort.UserPages
{
    /// <summary>
    /// Логика взаимодействия для ResortUserPage.xaml
    /// </summary>
    public partial class ResortUserPage : Page
    {
        public ResortUserPage()
        {
            InitializeComponent();
            getResort();
            comboBoxSpesialResortPage.Items.Add("Все типы");
            comboBoxSpesialResortPage.Items.Add("Особый климат");
            comboBoxSpesialResortPage.Items.Add("Минеральные воды");
            comboBoxSpesialResortPage.Items.Add("Лечебные грази");
            comboBoxSpesialResortPage.SelectedIndex = 0;
        }
        private List<ResortForListView> _resortList = new List<ResortForListView>();
        private async Task getResort()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<ResortResponse>>($"api/resortcity");
            foreach (var item in response)
            {
                _resortList.Add(
                    new ResortForListView { 
                        Id = item.Id, 
                        Name = item.Name, 
                        image = Manager.GetImageFromByteArray(item.img), 
                        Description = item.Description,
                        Climate = item.Climate,
                        TherapeuticMud = item.TherapeuticMud,
                        MineralWater = item.MineralWater,
                    }
                );
            }
            UpdateList();
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var resort = (sender as ListView).SelectedItem as ResortForListView;
            HotelsUserDialog hotelsDialog = new HotelsUserDialog(resort);
            hotelsDialog.ShowDialog();
        }

        private void comboBoxSpesialResortPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void textBoxNameSearchResortPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }
        private void UpdateList()
        {
            var filterList = new List<ResortForListView>();
            listView.ItemsSource = null;
            
            if (comboBoxSpesialResortPage.SelectedIndex == 0)
                filterList = _resortList;
            
            if (comboBoxSpesialResortPage.SelectedIndex == 1)
                filterList = _resortList.Where(c => c.Climate.Equals(true)).ToList();
            
            if (comboBoxSpesialResortPage.SelectedIndex == 2)
                filterList = _resortList.Where(c => c.MineralWater.Equals(true)).ToList();
            
            if (comboBoxSpesialResortPage.SelectedIndex == 3)
                filterList = _resortList.Where(c => c.TherapeuticMud.Equals(true)).ToList();
            
            filterList = filterList.Where(c => c.Name.ToLower().Contains(textBoxNameSearchResortPage.Text.ToLower())).ToList();
            listView.ItemsSource = filterList;
        }
    }
}
