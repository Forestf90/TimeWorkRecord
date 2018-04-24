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
using System.Data.SqlClient;
using Dapper;


namespace czas_pracy
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Raport : Window
    {
        SqlConnection con;
     
        public Raport()
        {
        
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            con.Open();

            dataGridView.ItemsSource = PrintData();
        }


        public List <Pracownik> PrintData()
        {
            string querry = @"SELECT * FROM Pracownik";

            var wynikiList = con.Query<Pracownik>(querry).ToList();

            return wynikiList;

        }

    }
}
