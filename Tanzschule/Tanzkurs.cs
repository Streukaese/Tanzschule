using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanzschule
{
    public class Tanzkurs
    {
        public int KursId { get; set; }
        public string Kursname { get; set; }
        public string Tanzstil { get; set; }
        public string Tanzlehrer { get; set; }
        public int Kontakt { get; set; }
        public int FSK { get; set; }
        public DateTime? Kursbeginn { get; set; }
        public Tanzkurs(int kursId, string kursname, string tanzstil, string tanzlehrer, int kontakt, int fsk, DateTime? kursbeginn)
        {
            KursId = kursId;
            Kursname = kursname;
            Tanzstil = tanzstil;
            Tanzlehrer = tanzlehrer;
            Kontakt = kontakt;
            FSK = fsk;
            Kursbeginn = kursbeginn;
        }
        public override string ToString()
        {
            return "BlaBla" + KursId + Kursname + Tanzstil;
        }
    }
}
