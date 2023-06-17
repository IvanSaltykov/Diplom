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
using WorldResort.Model;

namespace WorldResort.AdminDialog
{
    /// <summary>
    /// Логика взаимодействия для CityResortAdminDialog.xaml
    /// </summary>
    public partial class CityResortAdminDialog : Window
    {
        public CityResortAdminDialog(City city)
        {
            InitializeComponent();
            initialComponent(city);
        }
        private void initialComponent(City city)
        {
            textBlockIdCityResortDialog.Text = "Id: " + city.Id;
            textBlockNameeCityResortDialog.Text = "Имя: " + city.Name;
            textBlockCountryCityResortDialog.Text = "Страна: " + city.CountryId;
            textBlockPartWorldCityResortDialog.Text = "Часть света: " + city.PartWorldId;
            textBlockCliamateCityResortDialog.Text = "Особый климат: " + city.Climate;
            textBlockMineralWaterCityResortDialog.Text = "Минеральные воды: " + city.MineralWater;
            textBlockTherapeuticMudCityResortDialog.Text = "Лечебные грязи: " + city.TherapeuticMud;
            Manager.GetImageFromByteArray(city.img);
        }
    }
}
