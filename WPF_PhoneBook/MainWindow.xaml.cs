using PhoneBook;
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
using Microsoft.EntityFrameworkCore;

namespace WPF_PhoneBook
{

    public partial class Person
    {
        public string? CityName => City?.Name;
        public int? CityZip => City?.Zip;
        public string? NumberList => Numbers?.Aggregate("", (c, a) => c + (c.Length > 0 ? ", " : "") + a.NumberString);

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ccnPhoneBook cn;
        public MainWindow()
        {
            InitializeComponent();
            cn = new ccnPhoneBook();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Minden adat megjelenítése 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            grCity.Visibility = Visibility.Collapsed;
            dgCities.Visibility = Visibility.Collapsed;
            dgAll.Visibility = Visibility.Visible;

            dgAll.ItemsSource = cn.People.Include(p => p.City).Include(p => p.Numbers).ToList();
        }

        private void miCities(object sender, RoutedEventArgs e)
        {
            grCity.Visibility = Visibility.Collapsed;
            dgCities.Visibility = Visibility.Visible;
            dgAll.Visibility = Visibility.Collapsed;

            dgCities.ItemsSource = cn.Cities.ToList();
        }

        private void miCitiesNM_Click(object sender, RoutedEventArgs e)
        {

            grCity.Visibility = Visibility.Visible;
            dgCities.Visibility = Visibility.Collapsed;
            dgAll.Visibility = Visibility.Collapsed;

            grCity.DataContext = cn.Cities.ToList();
        }

        private void cbTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ((ComboBox)sender).SelectedItem as City;
            tbCity.Text = c.Name;
            tbZip.Text = c.Zip.ToString();

        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            grCity.Visibility = Visibility.Collapsed;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbCity.Text.Length == 0)
            {
                MessageBox.Show("Kérem adjon meg egy város nevet!");
                return;
            }
            var res = int.TryParse(tbZip.Text, out int zip);

            if (!res || (res && zip < 1))
            {
                MessageBox.Show("Kérem adjon meg valós irányító számot!");
                return;
            }

            if (cbCityName.SelectedItem is not City c) return;
            c.Name = tbCity.Text;

            if(zip != c.Zip)
            {
                var nc = new City { Zip = (short)zip, Name = c.Name };
                foreach (var p in c.People)
                {
                    p.City = nc;
                    nc.People.Add(p);
                }
                cn.Cities.Add(nc);
                cn.Cities.Remove(c);
            }

            cn.SaveChanges();
            grCity.Visibility = Visibility.Collapsed;

        }

        private void btSaveNewCity_Click(object sender, RoutedEventArgs e)
        {
            if (tbCity.Text.Length == 0)
            {
                MessageBox.Show("Kérem adjon meg egy város nevet!");
                return;
            }
            var res = int.TryParse(tbZip.Text, out int zip);

            if (!res || (res && zip < 1))
            {
                MessageBox.Show("Kérem adjon meg valós irányító számot!");
                return;
            }

            if (cn.Cities.Any(c => c.Zip == zip))
            {
                MessageBox.Show("Ez az irányítószám " + zip.ToString() + "már szerepel az adatbázisban");
                return;
            }

            var nc = new City { Zip = (short)zip, Name = tbCity.Text.ToString()};

            cn.Cities.Add(nc);
            cn.SaveChanges();

            grCity.Visibility = Visibility.Collapsed;




        }
    }
}
