"select kursId, kursname, tanzstil, tanzlehrer, kontakt, fsk, kursbeginn from Tanzkurse"

"select id, vorname, nachname, adresse, plz, hausnummer, geburtsdatum from Taenzer"

"SELECT kursId, kursname, tanzstil, tanzlehrer, kontakt, fsk, kursbeginn FROM `Tanzkurse`"

"SELECT id, vorname, nachname, adresse, plz, hausnummer, geburtsdatum FROM `Taenzer`"
 
"INSERT INTO `Tanzkurse` (`kursId`, `kursname`, `tanzstil`, `tanzlehrer`, `kontakt`, `fsk`, `kursbeginn`) 
VALUES (NULL, @kursname, @tanzstil, @tanzlehrer, @kontakt, @fsk, @kursbeginn)"

"INSERT INTO `Taenzer` (`id`, `vorname`, `nachname`, `adresse`, `plz`, `hausnummer`, `geburtsdatum`) VALUES 
(NULL, 'Berlin', 'Berlin', '50')"

"DELETE FROM `Tanzkurse` WHERE `Tanzkurse`.`kursId` = @KursId;"

"DELETE FROM `Taenzer` WHERE `Taenzer`.`id` = @Id;"

"UPDATE `tanzkurse` SET `Kursname` = @Kursname, `Tanzstil` = @Tanzstil, `Tanzlehrer` = @Tanzlehrer, 
`Kontakt` = @Kontakt, `Fsk` = @Fsk, `Kursbeginn` = @Kursbeginn WHERE 
`tanzkurse`.`kursId` = @kursId"

"UPDATE `taenzer` SET `Vorname` = @Vorname, `Nachname` = @Nachname, `Adresse` = @Adresse, 
`Postleitzahl` = @Postleitzahl, `Hausnummer` = @Hausnummer, `Geburtsdatum` = @Geburtsdatum WHERE 
`taenzer`.`id` = @id" 