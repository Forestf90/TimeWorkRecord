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

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;



namespace czas_pracy
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Raport : Window
    {
        SqlConnection con;
        Pracownik pracownikdoraportu;
        Rozliczenie do_wydruku;
        bool do_raportu = false;

        public Raport()
        {
        
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            con.Open();
            do_raportu = false;
            RapRozliczenie.IsEnabled = false;
            RapZapisz.IsEnabled = false;
            dataGridView.ItemsSource = WyswietlListePracownikow();
        }


        public List <Pracownik> WyswietlListePracownikow()
        {
            string querry = @"SELECT * FROM Pracownik";

            var wynikiList = con.Query<Pracownik>(querry).ToList();

            return wynikiList;

        }

        public List<Rozliczenie> WyswietlListeRozliczen()
        {
            string querry = @"SELECT * FROM Rozliczenie WHERE ID_Pracownik = '"+pracownikdoraportu.ID_Pracownik+"'";

            var wynikiList = con.Query<Rozliczenie>(querry).ToList();

            return wynikiList;

        }

        private void dataGridView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID_Pracownik") e.Cancel = true; 
        }

        private void dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!do_raportu && dataGridView.SelectedIndex!=-1)
            {
                pracownikdoraportu = new Pracownik();
                RapRozliczenie.IsEnabled = true;
                // pracownikdoraportu 
                int id = dataGridView.SelectedIndex;

                //MessageBox.Show(Convert.ToString(id));
                pracownikdoraportu = (Pracownik)dataGridView.Items.GetItemAt(id);
               // MessageBox.Show(pracownikdoraportu.Imie);
            }
            else if(do_raportu && dataGridView.SelectedIndex!=-1)
            {

                do_wydruku = new Rozliczenie();
                RapZapisz.IsEnabled = true;
                int id = dataGridView.SelectedIndex;
                do_wydruku = (Rozliczenie)dataGridView.Items.GetItemAt(id);
              //  MessageBox.Show(Convert.ToString(do_wydruku.GodzinyPrzepracowane));
            }
        }

        private void RapRozliczenie_Click(object sender, RoutedEventArgs e)
        {
            do_raportu = true;
            RapZapisz.IsEnabled = false;
            dataGridView.ItemsSource = WyswietlListeRozliczen();
        }

        private void RapZapisz_Click(object sender, RoutedEventArgs e)
        {
            
            decimal brutto = do_wydruku.Brutto+ do_wydruku.Premia;
                PdfDocument document = new PdfDocument();
                document.Info.Title = pracownikdoraportu.Nazwisko +" "+Convert.ToString(do_wydruku.Miesiac);

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            

            string text = "za prace wykonana przez .........." +pracownikdoraportu.Nazwisko.Replace(" ", "")+" "+pracownikdoraportu.Imie+ Environment.NewLine +
                         "PESEL ......................................." +pracownikdoraportu.pesel+ Environment.NewLine+
                         "z dnia .........................................." +do_wydruku.Miesiac.ToString("yyyy-MM-dd") + Environment.NewLine +
                         "zawartej z ................................... NajlepszaFirmaxD Sp. z.o.o" + Environment.NewLine+
                         "adres........................................... Randomowa 24, 43-382 Bielsko-Biala" + Environment.NewLine+
                         Environment.NewLine + Environment.NewLine+ Environment.NewLine+
                         "Wynagrodzenie BRUTTO       " +brutto+"zl"+ Environment.NewLine+
                         "Ubezpieczenia spoleczne    " + Environment.NewLine+
                         "Koszt uzyskania przychodu  " + Environment.NewLine+
                         "Procent kosztow uzyskania  " + Environment.NewLine+
                         "Dochod do opodatkowania    " + Environment.NewLine+
                         "Podatek naliczony          " + Environment.NewLine+
                         "Podatek dochodowy          " + Environment.NewLine+
                         "Ubezpieczenia zdrowotne    " + Environment.NewLine+
                         Environment.NewLine+ Environment.NewLine +
                         "Kwota do wyplaty           " + Environment.NewLine+
                         Environment.NewLine + Environment.NewLine +
                       //  "Data otrzymania rachunku :" + do_wydruku.Miesiac.ToString("yyyy-MM-dd") + 
                       "Podpis     .........................";

            XFont fontt = new XFont("Serif", 16 , XFontStyle.Bold);
            gfx.DrawString("Rachunek", fontt, XBrushes.Black,
           new XRect(30, 10, page.Width, page.Height),
           XStringFormats.TopCenter);

            XFont font = new XFont("Serif", 12);


            string[] WierszPodzielony = text.Split('\n');
            for (int i = 0; i < WierszPodzielony.Length; i++)
            {
                gfx.DrawString(WierszPodzielony[i], font, XBrushes.Black,
                new XRect(30, 45+(20*i), page.Width, page.Height),
                XStringFormats.TopLeft);
            }

            
           
            string filename = string.Empty;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = pracownikdoraportu.Nazwisko.Replace(" ","") + do_wydruku.Miesiac.ToString("yyyy-MM-dd"); // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                filename = dlg.FileName;
            }
            document.Save(filename);

        }
    }
}
