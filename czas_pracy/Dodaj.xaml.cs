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
                        string selekcikZ = @"Select ID_Pracownik , TypUmowy from Pracownik Where pesel='" + pesel + "'";
                        SqlCommand SZ = new SqlCommand(selekcikZ, con);

                        int result=0;// = (int)SZ.ExecuteScalar();
                            string typU="";

                        using (SZ)
                        {
                            SqlDataReader reader = SZ.ExecuteReader();
                            while (reader.Read())
                            {
                                result = reader.GetInt32(0);
                                typU = reader.GetString(1);

                            }
                            reader.Close();
                        }

                        if (typU != "o prace   ") { MessageBox.Show("Dany pracownik pracuje na umowe zlecenie - nie przysługuja mu żadne zwolnienia"); break; }

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


                    try
                    {
                        pesel = RPesel.Text;
                        string selekcikR = @"Select ID_Pracownik , TypUmowy, Wynagrodzenie , CzasPracy from Pracownik Where pesel='" + pesel + "'";
                        SqlCommand SR = new SqlCommand(selekcikR, con);

                        int Rresult =0;
                        string TypUmowy = "";
                        decimal ile_na_godzine=0;
                        int ile_h_na_dzien=0;
                        using (SR)
                        {
                            SqlDataReader reader =SR.ExecuteReader();
                            while (reader.Read())
                            {
                               Rresult = reader.GetInt32(0);
                               TypUmowy = reader.GetString(1);
                               ile_na_godzine = reader.GetDecimal(2);
                              ile_h_na_dzien = reader.GetInt32(3);
                            }
                        reader.Close();
                        }
                       


                        string Rmiesiac = Rmm.Text;
                        
                        string Rrok = Rrrrr.Text;
                    int godziny_przepracowane = Convert.ToInt32(RGodzinyPrzepracowane.Text);

                        string selekcikData = @"Select ID_Rozliczenie from Rozliczenie Where ID_Pracownik='" + Rresult + "' AND" +
                            " Miesiac=" + "'" + Rrok + "." + Rmiesiac + ".1'";
                        SqlCommand Rsprawdzdate = new SqlCommand(selekcikData, con);

                        var sprawdzdate = Rsprawdzdate.ExecuteScalar();
                        if (sprawdzdate != null) MessageBox.Show("Ten pracownik posiada juz rozliczenie za ten miesiac");
                        else
                        {
                            //MessageBox.Show(selekcikData);
                            string godziny = RGodzinyPrzepracowane.Text;
                            decimal premia = Convert.ToDecimal(RPremia.Text);

                            DateTime normalny = new DateTime(Convert.ToInt32(Rrok), Convert.ToInt32(Rmiesiac), 1);
                            DateTime normalny_koniec=new DateTime(Convert.ToInt32(Rrok), Convert.ToInt32(Rmiesiac), 
                                DateTime.DaysInMonth(Convert.ToInt32(Rrok), Convert.ToInt32(Rmiesiac)));

                            double ile_dni_roboczych=GetBusinessDays(normalny, normalny_koniec);
                            double ile_dni_urlopu=0;
                            decimal PlatnoscZaUrlop=0;



                            int dlugoscUrlopu=0;
                            DateTime Urlop_start = new DateTime();
                            decimal platnoscUrlopu=0;
                        int id_zwolnienia = new int();
                            using (SqlCommand command = new SqlCommand("select DataRozpoczecia , dlugosc , platnosc ,ID_Zwolnienie from Zwolnienie "+
                                        "Where DataRozpoczecia  Like '"+Rrok+"-"+Rmiesiac+"-__'"+"AND ID_Pracownik='"+Rresult+"'", con))
                            {
                                SqlDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    dlugoscUrlopu = reader.GetInt32(1);    
                                    Urlop_start = reader.GetDateTime(0);
                                    platnoscUrlopu = reader.GetDecimal(2);
                                id_zwolnienia= reader.GetInt32(3);
                            }
                            reader.Close();
                            }

                            if (dlugoscUrlopu == 0) MessageBox.Show("jest null");
                            else
                            {
                                
                                DateTime Urlop_koniec = new DateTime();
                                Urlop_koniec = Urlop_start;
                                Urlop_koniec=Urlop_koniec.AddDays(dlugoscUrlopu);
                                ile_dni_urlopu = GetBusinessDays(Urlop_start, Urlop_koniec);
                                
                                PlatnoscZaUrlop = (int)ile_dni_urlopu * platnoscUrlopu *ile_na_godzine * ile_h_na_dzien; // Co ???
                                MessageBox.Show(Convert.ToString(PlatnoscZaUrlop));
                            }

                        double dni_przepracowane = ile_dni_roboczych - ile_dni_urlopu;
                        decimal brutto;
                        if (TypUmowy == "o prace   ") brutto = (godziny_przepracowane * ile_na_godzine) + PlatnoscZaUrlop + premia;
                        else brutto = ile_na_godzine + premia;


                        string premia_z_kropka = Convert.ToString(premia);
                        premia_z_kropka = premia_z_kropka.Replace(",", ".");
                        string brutto_kropka = Convert.ToString(brutto);
                        brutto_kropka = brutto_kropka.Replace(",", ".");


                        string insercikR = @"INSERT INTO Rozliczenie VALUES(" + "'" + Rresult + "' ,'" +id_zwolnienia + "' ,'"
                       + Rmiesiac + ".01." + Rrok + "' ," + "'" + godziny_przepracowane + "'," + "'" + premia_z_kropka + "'," + "'" + brutto_kropka + "')";


                        SqlCommand DR = new SqlCommand(insercikR, con);
                        DR.ExecuteNonQuery();
                         if(TypUmowy !="o prace   ")MessageBox.Show("Pracownik pracuje na umowe zlecenie wiec jego przepracowane godziny nie są brane pod uwage");
                            MessageBox.Show("Rozliczenie zostało dodane!");

                            RPesel.Text = ""; Rmm.Text = ""; Rrrrr.Text=""; RGodzinyPrzepracowane.Text = ""; RPremia.Text = "";

                    }
                    }
                    catch
                    {
                        MessageBox.Show("Nie znaleziono pracownika z takim peselem");
                    }






                    break;


            }


        }

        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays =
                1 + ((endD - startD).TotalDays * 5 -
                (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
