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
using System.Data.SqlClient;

namespace czas_pracy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Window p = new Dodaj(); 
                p.Show();
        }
        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {
            Window p = new Edytuj();
            p.Show();
        }
        private void Wyswietl_Click(object sender, RoutedEventArgs e)
        {
            Window p = new Raport();
            p.Show();
        }
        private void Wyjscie_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }



    }
}
