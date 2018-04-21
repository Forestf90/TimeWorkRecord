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

namespace czas_pracy
{
    /// <summary>
    /// Logika interakcji dla klasy Dodaj.xaml
    /// </summary>
    public partial class Dodaj : Window
    {
        SqlConnection con; 
        public Dodaj()
        {
            InitializeComponent();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {

             con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
             con.Open();

            string pesel;
            switch (tab.SelectedIndex)
            {
                
                case 0:
                    string imie = PImie.Text;
                    string nazwisko =PNazwisko.Text;
                    pesel = PPesel.Text;

                   string dzien= Pzdd.Text;
                    string miesiac=Pzmm.Text;
                    string rok= Pzrrrr.Text;



                    string typ_umowy;
                    if (Prace.IsChecked == true) typ_umowy = "o prace";
                    else typ_umowy = "zlecenie";
                    int wynagrodzenie =Convert.ToInt32(PWynagrodzenie.Text);
                    int czaspracy = Convert.ToInt32(PCzasPracy.Text);
                 
                    string aa = @"INSERT INTO Pracownik VALUES("+"'"+pesel+"' ,'"+ imie+ "','" + nazwisko + "', '"
                        + miesiac +"."+dzien+"."+rok+ "' ,"+"NULL,"+ "'" + wynagrodzenie + "',"+ "'" +typ_umowy + "" +
                        "','" + czaspracy + "')";


                    SqlCommand cmd = new SqlCommand(aa, con);
                    cmd.ExecuteNonQuery();

                    PImie.Text = ""; PNazwisko.Text = "";PPesel.Text = "";Pzdd.Text = ""; PCzasPracy.Text = "";
                    Pzmm.Text=""; Pzrrrr.Text = ""; Prace.IsChecked = false; Zlecenie.IsChecked = false; PWynagrodzenie.Text = "";
                    MessageBox.Show("Pracownik został dodany!");

                    break;

                case 1:
                    try
                    {
                        pesel = ZPesel.Text;
                        string selekcikZ = @"Select ID_Pracownik from Pracownik Where pesel='" + pesel + "'";
                        SqlCommand SZ = new SqlCommand(selekcikZ, con);

                        int result = (int)SZ.ExecuteScalar();
                       

                        string Zdzien = Zdd.Text;
                        string Zmiesiac =Zmm.Text;
                        string Zrok = Zrrrr.Text;

                    int dlugosc = Convert.ToInt32(CzasTrwania.Text);
                        string typ_zwolnienia;
                        if (Lekarskie.IsChecked == true) typ_zwolnienia = "Lekarskie";
                        else typ_zwolnienia = "Urlop";

                 decimal platnosc = Decimal.Parse(Platnosc.Text);
                    
                        platnosc = platnosc / 100;

                    string Pla = Convert.ToString(platnosc);
                    Pla=Pla.Replace(",", ".");
                    

                    string insercikZ = @"INSERT INTO Zwolnienie VALUES(" + "'" + result + "' ,'"
                        + Zmiesiac + "." + Zdzien + "." + Zrok + "' ,"+"'"+dlugosc+"'," + "'" +Pla + "'," + "'" + typ_zwolnienia + "')";


                        SqlCommand DZ = new SqlCommand(insercikZ, con);
                        DZ.ExecuteNonQuery();
                        MessageBox.Show("Zwolnienie zostało dodane!");
                        ZPesel.Text = "";Zdd.Text= ""; Zmm.Text = ""; Zrrrr.Text = "";CzasTrwania.Text = "";
                        Platnosc.Text = "";

                    }
                    catch
            {
                MessageBox.Show("Nie znaleziono pracownika z takim peselem");
            }








            break;

                case 2:

                    //zmiana zmienych z zwolnienia na rozliczenie
                    //try
                    //{
                    //    pesel = ZPesel.Text;
                    //    string selekcikZ = @"Select ID_Pracownik from Pracownik Where pesel='" + pesel + "'";
                    //    SqlCommand SZ = new SqlCommand(selekcikZ, con);

                    //    int result = (int)SZ.ExecuteScalar();
                    //    MessageBox.Show(Convert.ToString(result));
                    //}
                    //catch
                    //{
                    //    MessageBox.Show("Nie znaleziono pracownika z takim peselem");
                    //}


                    break;


            }


        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
