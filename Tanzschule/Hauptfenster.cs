using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Tanzschule
{
    public partial class Tanzschule : Form
    {

        public Tanzschule()
        {
            InitializeComponent();
        }

        readonly Dictionary<int, Tanzkurs> kursById = new Dictionary<int, Tanzkurs>();
        readonly Dictionary<int, Taenzer> TaenzerById = new Dictionary<int, Taenzer>();
        Tanzkurs markierterKurs = null;

        void AddTaenzer(Taenzer t)
        {
            TaenzerById[t.Id] = t;

            int index = dataGridViewTaenzer.Rows.Add();
            dataGridViewTaenzer.Rows[index].Cells["ColumnId"].Value = t.Id;
            dataGridViewTaenzer.Rows[index].Cells["ColumnVorname"].Value = t.Vorname;
            dataGridViewTaenzer.Rows[index].Cells["ColumnNachname"].Value = t.Nachname;
            dataGridViewTaenzer.Rows[index].Cells["ColumnAdresse"].Value = t.Adresse;
            dataGridViewTaenzer.Rows[index].Cells["ColumnPostleitzahl"].Value = t.Postleitzahl;
            dataGridViewTaenzer.Rows[index].Cells["ColumnHausnummer"].Value = t.Hausnummer;
            dataGridViewTaenzer.Rows[index].Cells["ColumnGeburtsdatum"].Value = t.Geburtsdatum;
        }
        void AddTanzkurs(Tanzkurs tk)
        {
            kursById[tk.KursId] = tk;

            int index = dataGridViewTanzkurs.Rows.Add();
            dataGridViewTanzkurs.Rows[index].Cells["ColumnkursId"].Value = tk.KursId;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnKursname"].Value = tk.Kursname;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnTanzstil"].Value = tk.Tanzstil;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnTanzlehrer"].Value = tk.Tanzlehrer;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnKontakt"].Value = tk.Kontakt;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnFsk"].Value = tk.FSK;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnKursbeginn"].Value = tk.Kursbeginn;
        }
        void AddTaenzerImKurs(Taenzer imKurs)
        {
            int index = dataGridViewTaenzerInKursen.Rows.Add();
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerId"].Value = imKurs.IdAnmeldung;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerTaenzerId"].Value = imKurs.Id;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerVorname"].Value = imKurs.Vorname;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerNachname"].Value = imKurs.Nachname;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerAdresse"].Value = imKurs.Adresse;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerPostleitzahl"].Value = imKurs.Postleitzahl;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerHausnummer"].Value = imKurs.Hausnummer;
            dataGridViewTaenzerInKursen.Rows[index].Cells["ColumnTaenzerGeburtsdatum"].Value = imKurs.Geburtsdatum;
        }
        void AddListeKurs(Tanzkurs Kurs)
        {
            int index = dataGridViewListeVonKurs.Rows.Add();
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursKursIds"].Value = Kurs.KursId;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursKursId"].Value = Kurs.KursId;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursKursname"].Value = Kurs.Kursname;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursTanzstil"].Value = Kurs.Tanzstil;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursTanzlehrer"].Value = Kurs.Tanzlehrer;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursKontakt"].Value = Kurs.Kontakt;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursFsk"].Value = Kurs.FSK;
            dataGridViewListeVonKurs.Rows[index].Cells["ColumnKursKursbeginn"].Value = Kurs.Kursbeginn;
        }
        
        private void Tanzschule_Load(object sender, EventArgs e)
        {
            Datenbank.Open();       // Table Taenzer
            MySqlCommand cmd = Datenbank.CreateCommand();
            cmd.CommandText = "select id, vorname, nachname, adresse, postleitzahl, hausnummer, geburtsdatum from taenzer;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string vorname = reader.GetString(1);
                string nachname = reader.GetString(2);
                string adresse = reader.GetString(3);
                string postleitzahl = reader.GetString(4);
                string hausnummer = reader.GetString(5);
                DateTime? geburtsdatum = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6);

                AddTaenzer(new Taenzer(id, vorname, nachname, adresse, postleitzahl, hausnummer, geburtsdatum));
            }
            reader.Close();
            Datenbank.Close();

            // -------------------------------------------------------------------------

            Datenbank.Open();       // Table Tanzkurs
            MySqlCommand cmd1 = Datenbank.CreateCommand();
            cmd1.CommandText = "select kursId, kursname, tanzstil, tanzlehrer, kontakt, fsk, kursbeginn from tanzkurs;";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                int kursId = reader1.GetInt32(0);
                string kursname = reader1.GetString(1);
                string tanzstil = reader1.GetString(2);
                string tanzlehrer = reader1.GetString(3);
                int kontakt = reader1.GetInt32(4);
                int fsk = reader1.GetInt32(5);
                DateTime? kursbeginn = reader1.IsDBNull(6) ? null : (DateTime?)reader1.GetDateTime(6);

                AddTanzkurs(new Tanzkurs(kursId, kursname, tanzstil, tanzlehrer, kontakt, fsk, kursbeginn));
            }
            reader1.Close();
            Datenbank.Close();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            if (checkBox.Checked == false)
            {
                string vorname = textBoxVorname.Text;
                if (vorname.Length == 0)
                {
                    textBoxVorname.Focus();
                    return;
                }
                string nachname = textBoxNachname.Text;
                if (nachname.Length == 0)
                {
                    textBoxNachname.Focus();
                    return;
                }
                string adresse = textBoxAdresse.Text;
                if (adresse.Length == 0)
                {
                    textBoxAdresse.Focus();
                    return;
                }
                string plz = textBoxPlz.Text;
                if (plz.Length == 0)
                {
                    textBoxPlz.Focus();
                    return;
                }
                string hausnummer = textBoxHausnummer.Text;
                if (hausnummer.Length == 0)
                {
                    textBoxHausnummer.Focus();
                    return;
                }
                DateTime geburtsdatum = dateTimePickerGeburtsdatum.Value;
                if (geburtsdatum == null || geburtsdatum >= DateTime.Now)
                {
                    dateTimePickerGeburtsdatum.Focus();
                    return;
                }

                Datenbank.Open();       // Table Taenzer - Taenzer
                MySqlCommand command = Datenbank.CreateCommand();
                command.CommandText = "INSERT INTO `Taenzer` (`id`, `vorname`, `nachname`, `adresse`, `postleitzahl`, `hausnummer`, `geburtsdatum`) VALUES (NULL, @vorname, @nachname, @adresse, @postleitzahl, @hausnummer, @geburtsdatum)";
                command.Parameters.AddWithValue("Vorname", vorname);
                command.Parameters.AddWithValue("Nachname", nachname);
                command.Parameters.AddWithValue("Adresse", adresse);
                command.Parameters.AddWithValue("Postleitzahl", plz);
                command.Parameters.AddWithValue("Hausnummer", hausnummer);
                command.Parameters.AddWithValue("Geburtsdatum", geburtsdatum);
                //command.Parameters.AddWithValue("IdAnmeldung", idAnmeldung);
                command.ExecuteNonQuery();
                long id = (long)command.LastInsertedId;
                Datenbank.Close();

                AddTaenzer(new Taenzer((int)id, vorname, nachname, adresse, plz, hausnummer, geburtsdatum));

                //---------------------------------------------
            }
            else if (checkBox.Checked == true)
            {
                kursButtonSpeichern();
            }
        }

        private void buttonBearbeiten_Click(object sender, EventArgs e)
        {
            if (checkBox.Checked == false)
            {
                int index = -1;
                if (dataGridViewTaenzer.SelectedRows.Count == 1)
                {
                    index = dataGridViewTaenzer.SelectedRows[0].Index;
                }
                else if (dataGridViewTaenzer.SelectedCells.Count == 1)
                {
                    index = dataGridViewTaenzer.SelectedCells[0].RowIndex;
                }
                if (index == -1)
                {
                    return;
                }
                int id = (int)dataGridViewTaenzer.Rows[index].Cells["ColumnId"].Value;
                Taenzer t = TaenzerById[id];

                string neuVorname = textBoxVorname.Text;
                if (neuVorname.Length == 0)
                {
                    textBoxVorname.Focus();
                    return;
                }
                string neuNachname = textBoxNachname.Text;
                if (neuNachname.Length == 0)
                {
                    textBoxNachname.Focus();
                    return;
                }
                string neuAdresse = textBoxAdresse.Text;
                if (neuAdresse.Length == 0)
                {
                    textBoxAdresse.Focus();
                    return;
                }
                string neuPlz = textBoxPlz.Text;
                if (neuPlz.Length == 0)
                {
                    textBoxPlz.Focus();
                    return;
                }
                string neuHausnummer = textBoxHausnummer.Text;
                if (neuHausnummer.Length == 0)
                {
                    textBoxHausnummer.Focus();
                    return;
                }
                DateTime neuGeburtsdatum = dateTimePickerGeburtsdatum.Value;
                if (neuGeburtsdatum == null || neuGeburtsdatum >= DateTime.Now)
                {
                    dateTimePickerGeburtsdatum.Focus();
                    return;
                }

                Datenbank.Open();
                MySqlCommand command = Datenbank.CreateCommand();
                command.CommandText = "UPDATE `taenzer` SET `Vorname` = @Vorname, `Nachname` = @Nachname, `Adresse` = @Adresse, `Postleitzahl` = @Postleitzahl, `Hausnummer` = @Hausnummer, `Geburtsdatum` = @Geburtsdatum WHERE `taenzer`.`id` = @id";
                command.Parameters.AddWithValue("Vorname", neuVorname);
                command.Parameters.AddWithValue("Nachname", neuNachname);
                command.Parameters.AddWithValue("Adresse", neuAdresse);
                command.Parameters.AddWithValue("Postleitzahl", neuPlz);
                command.Parameters.AddWithValue("Hausnummer", neuHausnummer);
                command.Parameters.AddWithValue("Geburtsdatum", neuGeburtsdatum);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                command.Prepare();
                Datenbank.Close();

                t.Vorname = neuVorname;
                t.Nachname = neuNachname;
                t.Adresse = neuAdresse;
                t.Postleitzahl = neuPlz;
                t.Hausnummer = neuHausnummer;
                t.Geburtsdatum = neuGeburtsdatum;

                dataGridViewTaenzer.Rows[index].Cells["ColumnVorname"].Value = neuVorname;
                dataGridViewTaenzer.Rows[index].Cells["ColumnNachname"].Value = neuNachname;
                dataGridViewTaenzer.Rows[index].Cells["ColumnAdresse"].Value = neuAdresse;
                dataGridViewTaenzer.Rows[index].Cells["ColumnPostleitzahl"].Value = neuPlz;
                dataGridViewTaenzer.Rows[index].Cells["ColumnHausnummer"].Value = neuHausnummer;
                dataGridViewTaenzer.Rows[index].Cells["ColumnGeburtsdatum"].Value = neuGeburtsdatum;


                textBoxVorname.Text = "";
                textBoxNachname.Text = "";
                textBoxAdresse.Text = "";
                textBoxPlz.Text = "";
                textBoxHausnummer.Text = "";
                dateTimePickerGeburtsdatum.Value = DateTime.Now;

            }
            else if (checkBox.Checked == true)
            {
                kursButtonBearbeiten();
            }
}

        private void buttonLoeschen_Click(object sender, EventArgs e)
        {
            if (checkBox.Checked == false)
            {
                int index = -1;
                if (dataGridViewTaenzer.SelectedRows.Count == 1)
                {
                    index = dataGridViewTaenzer.SelectedRows[0].Index;
                }
                else if (dataGridViewTaenzer.SelectedCells.Count == 1)
                {
                    index = dataGridViewTaenzer.SelectedCells[0].RowIndex;
                }
                if (index == -1)
                {
                    return;
                }
                int id = (int)dataGridViewTaenzer.Rows[index].Cells["ColumnId"].Value;

                Datenbank.Open();
                MySqlCommand cmd = Datenbank.CreateCommand();
                cmd.CommandText = "delete from Taenzer where id=" + id;
                cmd.ExecuteNonQuery();

                Datenbank.Close();

                dataGridViewTaenzer.Rows.RemoveAt(index);
                TaenzerById.Remove(id);

                
            }
            else if (checkBox.Checked == true)
            {
                kursButtonLöschen();
            }
        }

        private void buttonAnmelden_Click(object sender, EventArgs e)
        {
            int markiertIndex = -1;
            if (dataGridViewTaenzer.SelectedRows.Count == 1)
            {
                markiertIndex = dataGridViewTaenzer.SelectedRows[0].Index;
            }
            else if (dataGridViewTaenzer.SelectedCells.Count == 1)
            {
                markiertIndex = dataGridViewTaenzer.SelectedCells[0].RowIndex;
            }
            if (markiertIndex == -1)
            {
                return;
            }
            int taenzerId = (int)dataGridViewTaenzer.Rows[markiertIndex].Cells["ColumnId"].Value;
            Taenzer taenzer = TaenzerById[taenzerId];

            markiertIndex = -1;
            if (dataGridViewTanzkurs.SelectedRows.Count == 1)
            {
                markiertIndex = dataGridViewTanzkurs.SelectedRows[0].Index;
            }
            else if (dataGridViewTanzkurs.SelectedCells.Count == 1)
            {
                markiertIndex = dataGridViewTanzkurs.SelectedCells[0].RowIndex;
            }
            if (markiertIndex == -1)
            { 
                return;
            }
            int kursId = (int)dataGridViewTanzkurs.Rows[markiertIndex].Cells["ColumnkursId"].Value;
            Tanzkurs kurs = kursById[kursId];

            AddTaenzerImKurs(taenzer);
            AddListeKurs(kurs);
            Datenbank.Open();       // Table Taenzer
            MySqlCommand command = Datenbank.CreateCommand();
            command.CommandText = "INSERT INTO `anmeldungen` (`taenzerId`, `kursId`) VALUES(@taenzerId, @kursId)";
            command.Parameters.AddWithValue("taenzerId", taenzerId);
            command.Parameters.AddWithValue("kursId", kursId);
            command.ExecuteNonQuery();
            long id = (long)command.LastInsertedId;
            Datenbank.Close();
        }

        private void buttonAbmelden_Click(object sender, EventArgs e)
        {
            if(markierterKurs == null)
            {
                return;
            }
            int indexTaenzer = -1;
            if (dataGridViewTaenzerInKursen.SelectedRows.Count == 1)
            {
                indexTaenzer = dataGridViewTaenzerInKursen.SelectedRows[0].Index;
            }
            else if (dataGridViewTaenzerInKursen.SelectedCells.Count == 1)
            {
                indexTaenzer = dataGridViewTaenzerInKursen.SelectedCells[0].RowIndex;
            }
            if (indexTaenzer == -1)
            {
                return;
            }
            int taenzerId = (int)dataGridViewTaenzerInKursen.Rows[indexTaenzer].Cells["ColumnTaenzerTaenzerId"].Value;
            Taenzer imKurs = TaenzerById[taenzerId];

            Datenbank.Open();
            MySqlCommand cmd = Datenbank.CreateCommand();
            cmd.CommandText = "DELETE FROM anmeldungen WHERE taenzerId = @taenzerId AND kursId=@kursId";
            cmd.Parameters.AddWithValue("taenzerId", taenzerId);
            cmd.Parameters.AddWithValue("kursId", markierterKurs.KursId);           // Prüft den markierterKurs
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Datenbank.Close();


            //-------------------------

            dataGridViewTaenzerInKursen.Rows.RemoveAt(indexTaenzer);        
            dataGridViewTaenzer_SelectionChanged(sender, e);                // Aktualisiert die Liste
        }

        private void dataGridViewTaenzer_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewListeVonKurs.Rows.Clear();

            int index = -1;
            if (dataGridViewTaenzer.SelectedRows.Count == 1)                                    // Innerlich durchgehen, gedanklich verfolgen - Lernen
            {
                index = dataGridViewTaenzer.SelectedRows[0].Index;
            }
            else if (dataGridViewTaenzer.SelectedCells.Count == 1)
            {
                index = dataGridViewTaenzer.SelectedCells[0].RowIndex;
            }
            if (index == -1)
            {
                return;
            }
            DataGridViewCell cell = dataGridViewTaenzer.Rows[index].Cells["ColumnId"];
            if (cell.Value == null)
            {
                return;
            }
            int id = (int)cell.Value;
            Taenzer t = TaenzerById[id];

            Datenbank.Open();       // Table Anmeldung
            MySqlCommand cmd = Datenbank.CreateCommand();
            cmd.CommandText = "select kursId from anmeldungen where taenzerid=" + t.Id;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int kursId = reader.GetInt32(0);
                Tanzkurs tk = kursById[kursId];

                int i = dataGridViewListeVonKurs.Rows.Add();
                dataGridViewListeVonKurs.Rows[i].Cells["ColumnKursKursId"].Value = tk.KursId;
                dataGridViewListeVonKurs.Rows[i].Cells["ColumnKursKursname"].Value = tk.Kursname;
            }
            reader.Close();

            Datenbank.Close();
        }

        private void dataGridViewKurs_SelectionChanged(object sender, EventArgs e)
        {
            markierterKurs = null;
            dataGridViewTaenzerInKursen.Rows.Clear();

            int index = -1;
            if (dataGridViewTanzkurs.SelectedRows.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedRows[0].Index;
            }
            else if (dataGridViewTanzkurs.SelectedCells.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedCells[0].RowIndex;
            }
            if (index == -1)
            {
                return;
            }
            
            labelTeilnehmerInKursen.Text = dataGridViewTanzkurs.SelectedColumns.ToString();
            DataGridViewCell cell = dataGridViewTanzkurs.Rows[index].Cells["ColumnkursId"];
            if (cell.Value == null)
            {
                return;
            } 
            int id = (int)cell.Value;
            Tanzkurs k = kursById[id];
            markierterKurs = k;         // Speichert in markierterKurs  -- um den Kurs bei der Abmeldung zu überprüfen

            Datenbank.Open();       // Table Anmeldung
            MySqlCommand cmd = Datenbank.CreateCommand();
            cmd.CommandText = "select taenzerId from anmeldungen where kursId=" + k.KursId;
            MySqlDataReader reader = cmd.ExecuteReader();       
            while (reader.Read())
            {
                int taenzerid = reader.GetInt32(0);
                Taenzer t = TaenzerById[taenzerid];

                int i = dataGridViewTaenzerInKursen.Rows.Add();
                dataGridViewTaenzerInKursen.Rows[i].Cells["ColumnTaenzerTaenzerId"].Value = t.Id;
                dataGridViewTaenzerInKursen.Rows[i].Cells["ColumnTaenzerVorname"].Value = t.Vorname;
            }
            reader.Close();

            Datenbank.Close();
        }
        
        private void checkBoxTaenzer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                checkBox.Text = "Kurs Hinzufügen,- \rBearbeiten oder Löschen";
                panelTaenzer.Visible = false;
                panelTanzkurs.Visible = true;
            }
            else
            {
                checkBox.Text = "Tänzer Hinzufügen,- \rBearbeiten oder Löschen";
                panelTaenzer.Visible = true;
                panelTanzkurs.Visible = false;
            }
        }

        void kursButtonSpeichern()
        {
            string kursname = textBoxKursname.Text;
            if (kursname.Length == 0)
            {
                textBoxKursname.Focus();
                return;
            }
            string tanzstil = textBoxTanzstil.Text;
            if (tanzstil.Length == 0)
            {
                textBoxTanzstil.Focus();
                return;
            }
            string tanzlehrer = textBoxTanzlehrer.Text;
            if (tanzlehrer.Length == 0)
            {
                textBoxTanzlehrer.Focus();
                return;
            }
            int kontakt = textBoxKontakt.TabIndex;
            if (kontakt == 0)
            {
                textBoxKontakt.Focus();
                return;
            }
            int fsk = textBoxFsk.TabIndex;
            if (fsk == 0)
            {
                textBoxFsk.Focus();
                return;
            }
            DateTime kursbeginn = dateTimePickerKursbeginn.Value;
            if (kursbeginn == null || kursbeginn <= DateTime.Now)
            {
                dateTimePickerKursbeginn.Focus();
                return;
            }

            Datenbank.Open();       // Table Tanzkurs
            MySqlCommand command = Datenbank.CreateCommand();
            command.CommandText = "INSERT INTO `Tanzkurs` (`kursId`, `kursname`, `tanzstil`, `tanzlehrer`, `kontakt`, `fsk`, `kursbeginn`) VALUES (NULL, @kursname, @tanzstil, @tanzlehrer, @kontakt, @fsk, @kursbeginn)";
            command.Parameters.AddWithValue("kursname", kursname);
            command.Parameters.AddWithValue("tanzstil", tanzstil);
            command.Parameters.AddWithValue("tanzlehrer", tanzlehrer);
            command.Parameters.AddWithValue("kontakt", kontakt);
            command.Parameters.AddWithValue("fsk", fsk);
            command.Parameters.AddWithValue("kursbeginn", kursbeginn);
            command.ExecuteNonQuery();
            long kursId = (long)command.LastInsertedId;
            Datenbank.Close();

            AddTanzkurs(new Tanzkurs((int)kursId, kursname, tanzstil, tanzlehrer, kontakt, fsk, kursbeginn));
        }
        void kursButtonBearbeiten()
        {
            //------------------------- Bearbeiten ------------------------------
            int index = -1;
            if (dataGridViewTanzkurs.SelectedRows.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedRows[0].Index;
            }
            else if (dataGridViewTanzkurs.SelectedCells.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedCells[0].RowIndex;
            }
            if (index == -1)
            {
                return;
            }
            int kursId = (int)dataGridViewTanzkurs.Rows[index].Cells["ColumnkursId"].Value;
            Tanzkurs tk = kursById[kursId];

            string neuKursname = textBoxKursname.Text;
            if (neuKursname.Length == 0)
            {
                textBoxKursname.Focus();
                return;
            }
            string neuTanzstil = textBoxTanzstil.Text;
            if (neuTanzstil.Length == 0)
            {
                textBoxTanzstil.Focus();
                return;
            }
            string neuTanzlehrer = textBoxTanzlehrer.Text;
            if (neuTanzlehrer.Length == 0)
            {
                textBoxTanzlehrer.Focus();
                return;
            }
            int neuKontakt = textBoxKontakt.TabIndex;
            if (neuKontakt == 0)
            {
                textBoxKontakt.Focus();
                return;
            }
            int neuFsk = textBoxFsk.TabIndex;
            if (neuFsk == 0)
            {
                textBoxFsk.Focus();
                return;
            }
            DateTime neuKursbeginn = dateTimePickerKursbeginn.Value;
            if (neuKursbeginn == null || neuKursbeginn <= DateTime.Now)
            {
                dateTimePickerKursbeginn.Focus();
                return;
            }

            Datenbank.Open();
            MySqlCommand command = Datenbank.CreateCommand();
            command.CommandText = "UPDATE `tanzkurs` SET `Kursname` = @Kursname, `Tanzstil` = @Tanzstil, `Tanzlehrer` = @Tanzlehrer, `Kontakt` = @Kontakt, `Fsk` = @Fsk, `Kursbeginn` = @Kursbeginn WHERE `tanzkurs`.`kursnummer` = @kursnummer";
            command.Parameters.AddWithValue("Kursname", neuKursname);
            command.Parameters.AddWithValue("Tanzstil", neuTanzstil);
            command.Parameters.AddWithValue("Tanzlehrer", neuTanzlehrer);
            command.Parameters.AddWithValue("Kontakt", neuKontakt);
            command.Parameters.AddWithValue("Fsk", neuFsk);
            command.Parameters.AddWithValue("Kursbeginn", neuKursbeginn);
            command.Parameters.AddWithValue("kursId", kursId);
            command.ExecuteNonQuery();
            command.Prepare();
            Datenbank.Close();

            tk.Kursname = neuKursname;
            tk.Tanzstil = neuTanzstil;
            tk.Tanzlehrer = neuTanzlehrer;
            tk.Kontakt = neuKontakt;
            tk.FSK = neuFsk;
            tk.Kursbeginn = neuKursbeginn;

            dataGridViewTanzkurs.Rows[index].Cells["ColumnKursname"].Value = neuKursname;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnTanzstil"].Value = neuTanzstil;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnTanzlehrer"].Value = neuTanzlehrer;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnKontakt"].Value = neuKontakt;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnFsk"].Value = neuFsk;
            dataGridViewTanzkurs.Rows[index].Cells["ColumnKursbeginn"].Value = neuKursbeginn;

            textBoxKursname.Text = "";
            textBoxTanzstil.Text = "";
            textBoxTanzlehrer.Text = "";
            textBoxKontakt.Text = "";
            textBoxFsk.Text = "";
            dateTimePickerKursbeginn.Value = DateTime.Now;
        }
        void kursButtonLöschen()
        {
            //------------------------- Löschen ------------------------------
            int index = -1;

            if (dataGridViewTanzkurs.SelectedRows.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedRows[0].Index;
            }
            else if (dataGridViewTanzkurs.SelectedCells.Count == 1)
            {
                index = dataGridViewTanzkurs.SelectedCells[0].RowIndex;
            }
            if (index == -1)
            {
                return;
            }

            int kursId = (int)dataGridViewTanzkurs.Rows[index].Cells["ColumnkursId"].Value;

            Datenbank.Open();
            MySqlCommand cmd = Datenbank.CreateCommand();
            cmd.CommandText = "delete from tanzkurs where kursId=" + kursId;
            cmd.ExecuteNonQuery();

            Datenbank.Close();

            dataGridViewTanzkurs.Rows.RemoveAt(index);
            kursById.Remove(kursId);
        }

        private void panelTanzkurs_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}