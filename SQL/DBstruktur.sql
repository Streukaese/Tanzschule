create database Tanzschule;

use database Tanzschule;

CREATE TABLE Taenzer (
    `id` int(5) NOT NULL auto_increment PRIMARY KEY,
 `Vorname` VARCHAR(30) NOT NULL ,
  `Nachname` VARCHAR(60) NOT NULL ,
`Adresse` VARCHAR(50) NOT NULL,
`Postleitzahl` int(5) NOT NULL,
`Hausnummer` int(3) NOT NULL,
`Datum` DATE NOT NULL
) ENGINE = InnoDB;


CREATE TABLE Tanzkurse (
    `KursId` int(5) NOT NULL auto_increment PRIMARY KEY,
 `Kursname` VARCHAR(30) NOT NULL ,
  `Tanzstil` VARCHAR(60) NOT NULL ,
`Tanzlehrer` VARCHAR(50) NOT NULL,
`Kontakt` int(15) NOT NULL,
`Altersspanne` int(5) NOT NULL,
`Kursbeginn` DATETIME NOT NULL
) ENGINE = InnoDB;


CREATE TABLE `tanzschule`.`anmeldungen` (
    `ID` INT NOT NULL AUTO_INCREMENT,
    `TaenzerId` INT NOT NULL,
    `KursId` INT NOT NULL,
    PRIMARY KEY (`ID`),
    FOREIGN KEY (`TaenzerId`) REFERENCES `taenzer`(`ID`),
    FOREIGN KEY (`Kursnummer`) REFERENCES `tanzkurse`(`Kursnummer`)
) ENGINE = InnoDB;