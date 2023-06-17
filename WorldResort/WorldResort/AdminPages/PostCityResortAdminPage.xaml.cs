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
using f = System.Windows.Forms;
using Image = System.Drawing.Image;

namespace WorldResort.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для PostCityResortAdminPage.xaml
    /// </summary>
    public partial class PostCityResortAdminPage : Page
    {
        private Guid _partWorldId;
        private Guid _countryId;
        private byte[] _image;
        public PostCityResortAdminPage()
        {
            InitializeComponent();
            getPartWorld();
        }
        private void getPartWorld()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = Manager.client.GetFromJsonAsync<List<PartWorld>>("api/partworld");
            comboBoxPartWorldPostCityResort.DisplayMemberPath = "Name";
            comboBoxPartWorldPostCityResort.ItemsSource = response.Result;
        }

        private void comboBoxPartWorldPostCityResort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var partWorld = comboBoxPartWorldPostCityResort.SelectedValue as PartWorld;
            _partWorldId = partWorld.Id;
            var response = Manager.client.GetFromJsonAsync<List<Country>>($"api/partworld/{_partWorldId}/country");
            comboBoxCountryPostCityResort.IsEnabled = true;
            comboBoxCountryPostCityResort.DisplayMemberPath = "Name";
            comboBoxCountryPostCityResort.ItemsSource = response.Result;
        }

        private void comboBoxCountryPostCityResort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var country = comboBoxCountryPostCityResort.SelectedValue as Country;
            _countryId = country.id;
            textBoxNameCityResort.IsEnabled = true;

        }

        private void textBoxNameCityResort_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxNameCityResort.Text.Length > 0)
            {
                checkBoxClimatePostCityResort.IsEnabled = true;
                checkBoxMineralWaterPostCityResort.IsEnabled = true;
                checkBoxTherapeuticMudPostCityResort.IsEnabled = true;
                textBoxDescriptionCityResort.IsEnabled = true;
                buttonPostCityResort.IsEnabled = true;
            }
            else
            {
                checkBoxClimatePostCityResort.IsEnabled = false;
                checkBoxMineralWaterPostCityResort.IsEnabled = false;
                checkBoxTherapeuticMudPostCityResort.IsEnabled = false;
                buttonPostCityResort.IsEnabled = false;
            }
        }

        private void buttonPostCityResort_Click(object sender, RoutedEventArgs e)
        {
            if (_image != null &&
                textBoxNameCityResort.Text != "" &&
                textBoxDescriptionCityResort.Text != "")
            {
                CreateCity createCity = new CreateCity()
                {
                    Name = textBoxNameCityResort.Text,
                    PartWorldId = _partWorldId.ToString(),
                    CountryId = _countryId.ToString(),
                    Climate = (bool)checkBoxClimatePostCityResort.IsChecked,
                    TherapeuticMud = (bool)checkBoxTherapeuticMudPostCityResort.IsChecked,
                    MineralWater = (bool)checkBoxMineralWaterPostCityResort.IsChecked,
                    img = _image,
                    Description = textBoxDescriptionCityResort.Text
                };
                var response = Manager.client.PostAsJsonAsync("api/resortcity", createCity);
                response.Result.EnsureSuccessStatusCode();
                if (!response.Result.IsSuccessStatusCode)
                    MessageBox.Show(response.Result.IsSuccessStatusCode.ToString());
                else
                    MessageBox.Show("Элемент создан");
            }
            else
                MessageBox.Show("Введите все данные");
        }

        private void buttonCreatePhotoPostCityResort_Click(object sender, RoutedEventArgs e)
        {
            AddImg();
        }
        private void AddImg()
        {
            f.OpenFileDialog dialog = new f.OpenFileDialog();
            dialog.Filter = "| *.png; *.jpg";
            if (dialog.ShowDialog() == f.DialogResult.OK)
            {
                imagePhotobuttonCreatePhotoPostCityResort.Source = new BitmapImage(new Uri(dialog.FileName));
                byte[] imageBytes = File.ReadAllBytes(dialog.FileName);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = stream.ToArray();
                    _image = bytes;
                }
            }
        }

        private void textBoxDescriptionCityResort_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
