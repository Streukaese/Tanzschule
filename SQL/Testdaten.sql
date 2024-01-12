"INSERT INTO `taenzer` (`id`, `vorname`, `nachname`, `adresse`, `postleitzahl`, `hausnummer`, `geburtsdatum`) 
VALUES (NULL, 'Jens', 'Lehmann', 'Dannenwalder Weg', '13409', '123', '2015.02.06'),
(NULL, 'Sarah', 'Wagenknecht', 'Finsterwalder Str', '13409', '10', 'NULL'),
(NULL, 'Hans', 'George', 'Finsterwalder Str', '13409', '12', '2019.01.01'),
(NULL, 'Tina', 'Turner', 'Senftenberger Ring', '13403', '56', '2020.07.14'),
(NULL, 'Dieter', 'Bohlen', 'Hanswerder', '12345', '66', '2014.01.09'),
(NULL, 'Götz', 'George', 'Quickborner Str', '13403', '91', '2018.09.11')"


"INSERT INTO `tanzkurse` (`KursId`, `Kursname`, `Tanzstil`, `Tanzlehrer`, `Kontakt`, `Fsk`, `Kursbeginn`) 
VALUES ('1', 'Kinder Barlett (6-10)', 'Ballett', 'Frau Krüger', '0123456789', '6', '2024-01-09 12:00:00'), 
('2', 'Jugend (mädchen) Ballett (10-15)', 'Ballett', 'Frau Donau', '0836219458', '10', '2024-01-09 14:00:00'),
(NULL, 'Streetboys (jugend)', 'HipHop', 'Herr Monti', '93374628', '12', '2024-01-10 13:00:00'), 
(NULL, 'Panthers (Erwachsene)', 'Hiphop / Streetdance', 'Frau Doro', '47439213', '18', '2024-01-10 19:00:00')"

INSERT INTO `anmeldungen` (`ID`, `TaenzerId`, `KursId`) VALUES 
(NULL, '8', '2'), (NULL, '3', '2'), (NULL, '4', '2'), (NULL, '2', '1')