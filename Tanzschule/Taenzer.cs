using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tanzschule
{
    public class Taenzer
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Adresse { get; set; }
        public string Postleitzahl { get; set; }
        public string Hausnummer { get; set; }
        public DateTime? Geburtsdatum { get; set; }     // teilen eventuelle Bedingung mit ? - normalerweise: 1 bedingung , 2 bedingung
        public object IdAnmeldung { get; internal set; }

        //public int IdAnmeldung { get; set; }
        public Taenzer(int id, string vorname, string nachname, string adresse, string postleitzahl, string hausnummer, DateTime? geburtsdatum)
        {
            Id = id;
            Vorname = vorname;
            Nachname = nachname;
            Adresse = adresse;
            Postleitzahl = postleitzahl;
            Hausnummer = hausnummer;
            Geburtsdatum = geburtsdatum;
            //IdAnmeldung = idAnmeldung;
        }
        public override string ToString()
        {
            return "Der Tänzer " + Vorname + " " + Nachname + " ist in der " + Adresse + " " + Postleitzahl + " , wohnt in der Hausnummer: " + Hausnummer + " und ist am: " + Geburtsdatum.ToString(/*"yyyy-MM-dd"*/) + " geboren."; // ToString.(/*.ToString("yyyy-MM-dd"*/)
        }
    }
}
