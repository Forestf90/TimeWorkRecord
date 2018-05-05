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
using Dapper;
using System.Data.SqlClient;

namespace czas_pracy
{
    /// <summary>
    /// Interaction logic for Edytuj.xaml
    /// </summary>
    public partial class Edytuj : Window
    {
        SqlConnection con;
        Pracownik doedycji;
        public Edytuj()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            //ukryty.Visibility = Visibility;

            string iin = EImie.Text;

            string[] imienazwisko = iin.Split(' ');

            Imiona.ItemsSource = WyswietlListe(imienazwisko[0], imienazwisko[1]);

        }


        public List<Pracownik> WyswietlListe(string imie , string nazwisko)
        {
            string querry = @"SELECT * FROM Pracownik WHERE (Imie = '" + imie + "'OR Nazwisko= '"+ nazwisko+"') AND DataZwolnienia is NULL";

            var wynikiList = con.Query<Pracownik>(querry).ToList();

            return wynikiList;

        }

        private void Imiona_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() != "Imie" && e.Column.Header.ToString() != "Nazwisko") e.Cancel = true;
        }

        private void Imiona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Imiona.SelectedIndex != -1)
            {
                doedycji = new Pracownik();
                int id = Imiona.SelectedIndex;
                doedycji = (Pracownik)Imiona.Items.GetItemAt(id);
                EEdytuj.IsEnabled = true;

            }
            else EEdytuj.IsEnabled = false;
        }

        private void EEdytuj_Click(object sender, RoutedEventArgs e)
        {
            ukryty.Visibility = Visibility; 
            EZapisz.Visibility = Visibility;
            EZwolnij.Visibility = Visibility;

            ENazwisko.Text = doedycji.Nazwisko;
            if (doedycji.TypUmowy == "o prace   ") Eo_prace.IsChecked=true;
            else Ezlecenie.IsChecked=true;

            Eczas_pracy.Text = Convert.ToString(doedycji.CzasPracy);
            Ewynagrodzenie.Text = Convert.ToString(doedycji.wynagrodzenie);
        }

        private void EZapisz_Click(object sender, RoutedEventArgs e)
        {
            decimal platnosc = Decimal.Parse(Ewynagrodzenie.Text);

            

            string Pla = Convert.ToString(platnosc);
            Pla = Pla.Replace(",", ".");


            string zmien;
            if(Ezlecenie.IsChecked==true) zmien= @"UPDATE Pracownik SET Nazwisko='"+ENazwisko.Text+"' ,wynagrodzenie='"+Pla+
                "' , TypUmowy='zlecenie' where ID_Pracownik='"+doedycji.ID_Pracownik+"'";
            else zmien= @"UPDATE Pracownik SET Nazwisko='" + ENazwisko.Text + "' ,wynagrodzenie='" + Pla +
                "' , TypUmowy='o prace', CzasPracy='"+Convert.ToInt32(Eczas_pracy.Text)+"' where ID_Pracownik='" + doedycji.ID_Pracownik + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(zmien, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Zmiany zostaly zapisane!");
            Close();
        }

        private void EZwolnij_Click(object sender, RoutedEventArgs e)
        {
            string zwolnij= @"UPDATE Pracownik SET DataZwolnienia='"+DateTime.Now+"' Where ID_Pracownik='"+doedycji.ID_Pracownik+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(zwolnij, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Pracownik zostal zwolniony!");
            Close();
        }
    }
}
