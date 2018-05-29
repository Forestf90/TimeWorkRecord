using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace czas_pracy
{
    public class Pracownik
    {

        public int ID_Pracownik { get; set; }
        public string pesel { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataZatrudnienia { get; set; }
        public DateTime DataZwolnienia { get; set; }
        public decimal wynagrodzenie { get; set; }
        public string TypUmowy { get; set; }
        public int CzasPracy { get; set; }
    }
}
