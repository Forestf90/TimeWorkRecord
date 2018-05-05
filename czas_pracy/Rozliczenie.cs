using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace czas_pracy
{
    public class Rozliczenie
    {

        public int ID_Rozliczenie { get; set; }
        public int ID_Pracownik { get; set; }
        public int ID_Zwolnienie { get; set; }
        public DateTime Miesiac { get; set; }
        public int GodzinyPrzepracowane { get; set; }
        public decimal Premia { get; set; }
        public decimal Brutto { get; set; }
    }
}
