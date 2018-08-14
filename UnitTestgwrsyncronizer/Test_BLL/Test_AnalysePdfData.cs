using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gwrsyncronizer.BLL;
using gwrsyncronizer.Model;

namespace UnitTestgwrsyncronizer.Test_BLL
{
    [TestClass]
    public class Test_AnalysePdfData
    {
        [TestMethod]
        public void Test_ConvertTextToPdf()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Schulhausweg 7, 4571 Lüterkofen
Gemeinde Eidg. Gebäudeidentifikator 191 544 631
2455 Lüterkofen-Ichertswil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1572 
Gebäudestatus Gebäudekategorie
bestehend Einfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
105 2
2017 Nach 2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Schulhausweg 7 4571 Lüterkofen
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
191544631 001  Parterre bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 2 2
/ ";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housing = apd.ConvertTextToList(data);

            // Then
            Assert.IsFalse(housing.Contains("Bundesamt für Statistik"));
        }

        [TestMethod]
        public void Test_Pdf_191544631_Egid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Schulhausweg 7, 4571 Lüterkofen
Gemeinde Eidg. Gebäudeidentifikator 191 544 631
2455 Lüterkofen-Ichertswil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1572 
Gebäudestatus Gebäudekategorie
bestehend Einfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
105 2
2017 Nach 2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("191 544 631".Replace(" ", string.Empty), housing.Egid);
            Assert.IsNull(housing.Abbruch);
            Assert.AreEqual("105", housing.GebFlaeche);
            Assert.AreEqual("2017", housing.Baujahr1);
        }

        [TestMethod]
        public void Test_Pdf_302034483_Egid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Turbinenstrasse 31, 8005 Zürich
Gemeinde Eidg. Gebäudeidentifikator 302 034 483
261 Zürich
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
283 6939 75931
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
439 11
2012 2011-2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 3
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("302034483".Replace(" ", string.Empty), housing.Egid);
            Assert.IsNull(housing.Abbruch);
            Assert.AreEqual("439", housing.GebFlaeche);
            Assert.AreEqual("2012", housing.Baujahr1);
        }

        [TestMethod]
        public void Test_Pdf_160000643_Egid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Bahnhofstrasse 7a, 6210 Sursee
Bahnhofstrasse 7b, 6210 Sursee
Centralstrasse 8a, 6210 Sursee
Centralstrasse 8b, 6210 Sursee
Gemeinde Eidg. Gebäudeidentifikator 160 000 643
1103 Sursee
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
423 370 224
Gebäudestatus bestehend Gebäudekategorie Gebäude mit teilweiser 
Wohnnutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
2080 6
2002 2001-2005 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:58 1 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("160000643".Replace(" ", string.Empty), housing.Egid);
            Assert.IsNull(housing.Abbruch);
            Assert.AreEqual("2080", housing.GebFlaeche);
            Assert.AreEqual("2002", housing.Baujahr1);
        }

        [TestMethod]
        public void Test_Pdf_001328932_Egid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Sunnmatt 34, 3305 Iffwil
Gemeinde Eidg. Gebäudeidentifikator 001 328 932
541 Iffwil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1177 
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
677 2
 Vor 1919 1986-1990 
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Sunnmatt 34 3305 Iffwil
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
1328932 001  Parterre, mehrgeschoss. bestehend
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("001 328 932".Replace(" ", string.Empty), housing.Egid);
            Assert.IsNull(housing.Abbruch);
            Assert.AreEqual("bestehend", housing.GebStatus);
            Assert.AreEqual("Wohngebäude mit Nebennutzung", housing.GebKategorie);
            Assert.AreEqual("677", housing.GebFlaeche);
            Assert.AreEqual("Vor 1919", housing.Baujahr2);
            Assert.AreEqual(data, housing.Blob);
        }

        [TestMethod]
        public void Test_Pdf_191544631_Egid_Edid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Schulhausweg 7, 4571 Lüterkofen
Gemeinde Eidg. Gebäudeidentifikator 191 544 631
2455 Lüterkofen-Ichertswil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1572 
Gebäudestatus Gebäudekategorie
bestehend Einfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
105 2
2017 Nach 2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Schulhausweg 7 4571 Lüterkofen
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
191544631 001  Parterre bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 2 2
/ ";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("0", housing.HousingEdids[0].Edid);
            Assert.AreEqual("Schulhausweg", housing.HousingEdids[0].Strasse);
            Assert.AreEqual("7", housing.HousingEdids[0].Eingangnummer);
            Assert.AreEqual("4571", housing.HousingEdids[0].Plz);
            Assert.AreEqual("Lüterkofen", housing.HousingEdids[0].Ort);
        }

        [TestMethod]
        public void Test_Pdf_001328932_Egid_Edid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Sunnmatt 34, 3305 Iffwil
Gemeinde Eidg. Gebäudeidentifikator 001 328 932
541 Iffwil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1177 
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
677 2
 Vor 1919 1986-1990 
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Sunnmatt 34 3305 Iffwil
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
1328932 001  Parterre, mehrgeschoss. bestehend
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("0", housing.HousingEdids[0].Edid);
            Assert.AreEqual("Sunnmatt", housing.HousingEdids[0].Strasse);
            Assert.AreEqual("34", housing.HousingEdids[0].Eingangnummer);
            Assert.AreEqual("3305", housing.HousingEdids[0].Plz);
            Assert.AreEqual("Iffwil", housing.HousingEdids[0].Ort);
        }

        [TestMethod]
        public void Test_Pdf_160000643_Egid_Edid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Bahnhofstrasse 7a, 6210 Sursee
Bahnhofstrasse 7b, 6210 Sursee
Centralstrasse 8a, 6210 Sursee
Centralstrasse 8b, 6210 Sursee
Gemeinde Eidg. Gebäudeidentifikator 160 000 643
1103 Sursee
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
423 370 224
Gebäudestatus bestehend Gebäudekategorie Gebäude mit teilweiser 
Wohnnutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
2080 6
2002 2001-2005 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:58 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Bahnhofstrasse 7a 6210 Sursee
EDID Strasse Eingangsnummer PLZ Ort
1 Bahnhofstrasse 7b 6210 Sursee
EDID Strasse Eingangsnummer PLZ Ort
2 Centralstrasse 8a 6210 Sursee
EDID Strasse Eingangsnummer PLZ Ort
3 Centralstrasse 8b 6210 Sursee
Utilisateur Invité
Druckdatum: 25.07.2018 13:58 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("0", housing.HousingEdids[0].Edid);
            Assert.AreEqual("Bahnhofstrasse", housing.HousingEdids[0].Strasse);
            Assert.AreEqual("7a", housing.HousingEdids[0].Eingangnummer);
            Assert.AreEqual("6210", housing.HousingEdids[0].Plz);
            Assert.AreEqual("Sursee", housing.HousingEdids[0].Ort);

            Assert.AreEqual("3", housing.HousingEdids[3].Edid);
            Assert.AreEqual("Centralstrasse", housing.HousingEdids[3].Strasse);
            Assert.AreEqual("8b", housing.HousingEdids[3].Eingangnummer);
            Assert.AreEqual("6210", housing.HousingEdids[3].Plz);
            Assert.AreEqual("Sursee", housing.HousingEdids[3].Ort);
        }

        [TestMethod]
        public void Test_Pdf_191544631_Egid_Edid_Ewid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Schulhausweg 7, 4571 Lüterkofen
Gemeinde Eidg. Gebäudeidentifikator 191 544 631
2455 Lüterkofen-Ichertswil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1572 
Gebäudestatus Gebäudekategorie
bestehend Einfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
105 2
2017 Nach 2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Schulhausweg 7 4571 Lüterkofen
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
191544631 001  Parterre bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("191544631", housing.HousingEdids[0].HousingEgidEwids[0].EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids[0].HousingEgidEwids[0].Ewid);

            Assert.AreEqual("191544631 001  Parterre bestehend", housing.HousingEdids[0].HousingEgidEwids[0].Blob);
        }

        [TestMethod]
        public void Test_Pdf_001328932_Egid_Edid_Ewid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Sunnmatt 34, 3305 Iffwil
Gemeinde Eidg. Gebäudeidentifikator 001 328 932
541 Iffwil
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1177 
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
677 2
 Vor 1919 1986-1990 
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Sunnmatt 34 3305 Iffwil
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
1328932 001  Parterre, mehrgeschoss. bestehend
Utilisateur Invité
Druckdatum: 26.07.2018 08:50 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("1328932", housing.HousingEdids[0].HousingEgidEwids[0].EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids[0].HousingEgidEwids[0].Ewid);
            Assert.AreEqual("1328932 001  Parterre, mehrgeschoss. bestehend", housing.HousingEdids[0].HousingEgidEwids[0].Blob);
        }

        [TestMethod]
        public void Test_Pdf_302034483_Egid_Edid_Ewid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Turbinenstrasse 31, 8005 Zürich
Gemeinde Eidg. Gebäudeidentifikator 302 034 483
261 Zürich
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
283 6939 75931
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
439 11
2012 2011-2015 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 1 3
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Turbinenstrasse 31 8005 Zürich
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
302034483 003 3 Parterre Rechts bestehend
302034483
031 4 Parterre Links bestehend
302034483
001 1 Parterre Links bestehend
302034483 002 2 Parterre Rechts bestehend
302034483
005 102 1. Stock Mitte bestehend
302034483
004 101 1. Stock Links bestehend
302034483 006 103 1. Stock Rechts bestehend
302034483
008 202 2. Stock Mitte bestehend
302034483
007 201 2. Stock Links bestehend
302034483
009 203 2. Stock Rechts bestehend
302034483 011 302 3. Stock Mitte bestehend
302034483
010 301 3. Stock Links bestehend
302034483
012 303 3. Stock Rechts bestehend
302034483 014 402 4. Stock Mitte bestehend
302034483
013 401 4. Stock Links bestehend
302034483
015 403 4. Stock Rechts bestehend
302034483 017 502 5. Stock Mitte bestehend
302034483
016 501 5. Stock Links bestehend
302034483
018 503 5. Stock Rechts bestehend
302034483
020 602 6. Stock Mitte bestehend
302034483
019 601 6. Stock Links bestehend
302034483
021 603 6. Stock Rechts bestehend
302034483
023 702 7. Stock Mitte bestehend
302034483 022 701 7. Stock Links bestehend
302034483
024 703 7. Stock Rechts bestehend
302034483
026 802 8. Stock Mitte bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 2 3
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
302034483
027 803 8. Stock Rechts bestehend
302034483
025 801 8. Stock Links bestehend
302034483 029 902 9. Stock Mitte bestehend
302034483
030 903 9. Stock Rechts bestehend
302034483
028 901 9. Stock Links bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:57 3 3
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("302034483", housing.HousingEdids[0].HousingEgidEwids[0].EgidEwid);
            Assert.AreEqual("003", housing.HousingEdids[0].HousingEgidEwids[0].Ewid);
            Assert.AreEqual("302034483 003 3 Parterre Rechts bestehend", housing.HousingEdids[0].HousingEgidEwids[0].Blob);
            Assert.AreEqual("302034483", housing.HousingEdids[0].HousingEgidEwids.LastOrDefault().EgidEwid);
            Assert.AreEqual("028", housing.HousingEdids[0].HousingEgidEwids.LastOrDefault().Ewid);
            Assert.AreEqual("302034483 028 901 9. Stock Links bestehend", housing.HousingEdids[0].HousingEgidEwids.LastOrDefault().Blob);
        }

        [TestMethod]
        public void Test_Pdf_160000643_Egid_Edid_Ewid()
        {
            // Given
            var data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Bahnhofstrasse 7a, 6210 Sursee
Bahnhofstrasse 7b, 6210 Sursee
Centralstrasse 8a, 6210 Sursee
Centralstrasse 8b, 6210 Sursee
Gemeinde Eidg. Gebäudeidentifikator 160 000 643
1103 Sursee
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
423 370 224
Gebäudestatus bestehend Gebäudekategorie Gebäude mit teilweiser 
Wohnnutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
2080 6
2002 2001-2005 - 
Utilisateur Invité
Druckdatum: 25.07.2018 13:58 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Bahnhofstrasse 7a 6210 Sursee
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
160000643 001  5. Stock Links bestehend
160000643
003  5. Stock Rechts aussen bestehend
160000643
002  5. Stock Mitte bestehend
EDID Strasse Eingangsnummer PLZ Ort
1 Bahnhofstrasse 7b 6210 Sursee
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
160000643 004  5. Stock Mitte bestehend
160000643
005  5. Stock Rechts bestehend
160000643
006  5. Stock Links bestehend
EDID Strasse Eingangsnummer PLZ Ort
2 Centralstrasse 8a 6210 Sursee
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
160000643
007  5. Stock Links bestehend
160000643 009  5. Stock Rechts bestehend
160000643
008  5. Stock Mitte bestehend
EDID Strasse Eingangsnummer PLZ Ort
3 Centralstrasse 8b 6210 Sursee
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
160000643
011  5. Stock Rechts bestehend
160000643
012  5. Stock Links bestehend
160000643
010  5. Stock Mitte bestehend
Utilisateur Invité
Druckdatum: 25.07.2018 13:58 2 2
/";

            // When
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Then
            Assert.AreEqual("160000643", housing.HousingEdids[0].HousingEgidEwids[0].EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids[0].HousingEgidEwids[0].Ewid);
            Assert.AreEqual("160000643 001  5. Stock Links bestehend", housing.HousingEdids[0].HousingEgidEwids[0].Blob);
            Assert.AreEqual("5. Stock", housing.HousingEdids[0].HousingEgidEwids[0].Stockwerk);
            Assert.AreEqual("Links", housing.HousingEdids[0].HousingEgidEwids[0].Lage);
            Assert.AreEqual("160000643", housing.HousingEdids.LastOrDefault().HousingEgidEwids.LastOrDefault().EgidEwid);
            Assert.AreEqual("010", housing.HousingEdids.LastOrDefault().HousingEgidEwids.LastOrDefault().Ewid);
            Assert.AreEqual("160000643 010  5. Stock Mitte bestehend", housing.HousingEdids.LastOrDefault().HousingEgidEwids.LastOrDefault().Blob);
            Assert.AreEqual("5. Stock", housing.HousingEdids.LastOrDefault().HousingEgidEwids.LastOrDefault().Stockwerk);
            Assert.AreEqual("Mitte", housing.HousingEdids.LastOrDefault().HousingEgidEwids.LastOrDefault().Lage);
        }

        [TestMethod]
        public void AnalyseAdresse_WithFiveParams()
        {
            // Arrange
            string value = "0 Im Wolfbiel 5 4206 Seewen SO";
            // string value = "3 Centralstrasse 8b 6210 Sursee";

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> address = apd.AnalyseEdidValue(value);

            // Assert
            Assert.AreEqual(5, address.Count);
            Assert.AreEqual("Im Wolfbiel", address[1]);
            Assert.AreEqual("Seewen SO", address[4]);
        }

        [TestMethod]
        public void AnalyseAdresse_WithFourParams()
        {
            // Arrange
            string value = "0 Auf dem Hubel 4206 Seewen SO";

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> address = apd.AnalyseEdidValue(value);

            // Assert
            Assert.AreEqual(5, address.Count);
            Assert.AreEqual("Auf dem Hubel", address[1]);
            Assert.AreEqual(string.Empty, address[2]);
            Assert.AreEqual("Seewen SO", address[4]);
        }

        [TestMethod]
        public void AnalyseCommunityTag_AllInOneLine()
        {
            // Arrange
            List<string> data = new List<string>();
            data.Add("Gemeinde 2503 Erlinsbach(SO) Eidg. Gebäudeidentifikator 190 119 574");
            Housing housing = new Housing();

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> fake = new List<string>();
            List<string> languageSettings = apd.IdentifyLanguage(fake);
            apd.AnalyseCommunityTag(data, housing, languageSettings);

            // Assert
            Assert.AreEqual("190119574", housing.Egid);
            Assert.AreEqual("Erlinsbach(SO)", housing.GemName);
            Assert.AreEqual("2503", housing.GemNr);

        }

        [TestMethod]
        public void AnalyseCommunityTag_EgidInFristLine()
        {
            // Arrange
            List<string> data = new List<string>();
            data.Add("Gemeinde Eidg. Gebäudeidentifikator 190 119 574");
            data.Add("2503 Erlinsbach(SO)");
            Housing housing = new Housing();

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> fake = new List<string>();
            List<string> languageSettings = apd.IdentifyLanguage(fake);
            apd.AnalyseCommunityTag(data, housing, languageSettings);

            // Assert
            Assert.AreEqual("190119574", housing.Egid);
            Assert.AreEqual("Erlinsbach(SO)", housing.GemName);
            Assert.AreEqual("2503", housing.GemNr);

        }

        [TestMethod]
        public void AnalyseCommunityTag_ParamsOnSecondLine()
        {
            // Arrange
            List<string> data = new List<string>();
            data.Add("Gemeinde Eidg. Gebäudeidentifikator");
            data.Add("2503 Erlinsbach(SO) 190 119 574");
            Housing housing = new Housing();

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> fake = new List<string>();
            List<string> languageSettings = apd.IdentifyLanguage(fake);
            apd.AnalyseCommunityTag(data, housing, languageSettings);

            // Assert
            Assert.AreEqual("190119574", housing.Egid);
            Assert.AreEqual("Erlinsbach(SO)", housing.GemName);
            Assert.AreEqual("2503", housing.GemNr);

        }

        [TestMethod]
        public void AnalyseConstructionYear_AllOnOne()
        {
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Stapfackerstrasse 3, 4658 Däniken SO
Gemeinde Eidg. Gebäudeidentifikator 190 009 706
2572 Däniken
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1922 
Gebäudestatus Gebäudekategorie
bestehend Mehrfamilienhaus, ohne
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) 260 Anz. Geschosse 1
2003 2001-2005 - 
Utilisateur Invité
Druckdatum: 12.07.2018 07:48 1 2
/";

            Housing housing = new Housing();

            // Act
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            housing = apd.AnalysePdf(housingList, data);

            // Assert
            Assert.AreEqual("190009706", housing.Egid);
            Assert.AreEqual("260", housing.GebFlaeche);
            Assert.AreEqual("1", housing.AnzGeschosse);

        }

        [TestMethod]
        public void AnalyseEwidValue_Parterre()
        {
            // Given 

            string value = @"1328932 001  Parterre, mehrgeschoss. bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("1328932", result[0]);
            Assert.AreEqual("001", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("Parterre, mehrgeschoss.", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_Parterre_fr()
        {
            // Given 
            //string value = @"793635 001  REZ, sur plus.étages existant";
            string value = @"793635 001  Rez-de-chaussée existant";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("793635", result[0]);
            Assert.AreEqual("001", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("Rez-de-chaussée", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("existant", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_ParterreMehrgeschoss_fr()
        {
            // Given 
            string value = @"793635 001  REZ, sur plus. étages existant";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("793635", result[0]);
            Assert.AreEqual("001", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("REZ, sur plus. étages", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("existant", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_ParterreMehrgeschoss_it()
        {
            // Given 
            string value = @"11108578 001 _103000292 Pianterre., di più piani esistente";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("11108578", result[0]);
            Assert.AreEqual("001", result[1]);
            Assert.AreEqual("103000292", result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("Pianterre., di più piani", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("esistente", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_ParterreLage()
        {
            // Given 

            string value = @"302034483 003 3 Parterre Rechts bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("302034483", result[0]);
            Assert.AreEqual("003", result[1]);
            Assert.AreEqual("3", result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("Parterre", result[4]);
            Assert.AreEqual("Rechts", result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_StockwerkLage()
        {
            // Given 

            string value = @"160000643 009  5. Stock Rechts bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("160000643", result[0]);
            Assert.AreEqual("009", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("5. Stock", result[4]);
            Assert.AreEqual("Rechts", result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_StockwerkMehrgeschossLage()
        {
            // Given 

            string value = @"160000643 009  5. Stock, mehrgeschoss. Rechts bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("160000643", result[0]);
            Assert.AreEqual("009", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("5. Stock, mehrgeschoss.", result[4]);
            Assert.AreEqual("Rechts", result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        
            [TestMethod]
        public void AnalyseEwidValue_StockwerkMehrgeschossDGLage()
        {
            // Given 

            string value = @"190009706 009  3. Stock, mehrgeschoss. 5 1/2 Zimmer 1. DG/2. DG bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("190009706", result[0]);
            Assert.AreEqual("009", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("3. Stock, mehrgeschoss.", result[4]);
            Assert.AreEqual("5 1/2 Zimmer 1. DG/2. DG", result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_Stockwerk()
        {
            // Given 

            string value = @"160000643 009  5. Stock bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("160000643", result[0]);
            Assert.AreEqual("009", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("5. Stock", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void AnalyseEwidValue_Stockwerk_mehrgeschoss()
        {
            // Given 

            string value = @"160000643 009  5. Stock, mehrgeschoss. bestehend";
            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> result = apd.DivideEwidValuesToElements(value);

            // Assert
            Assert.AreEqual("160000643", result[0]);
            Assert.AreEqual("009", result[1]);
            Assert.AreEqual(string.Empty, result[2]);
            Assert.AreEqual(string.Empty, result[3]);
            Assert.AreEqual("5. Stock, mehrgeschoss.", result[4]);
            Assert.AreEqual(string.Empty, result[5]);
            Assert.AreEqual("bestehend", result[6]);
        }

        [TestMethod]
        public void Test_Pdf_101000543_Egid_Edid_Ewid()
        {
            // Given
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Birkengasse 3, 4583 Aetigkofen
Birkengasse 5, 4583 Aetigkofen
Gemeinde Eidg. Gebäudeidentifikator
2465 Buchegg 101 000 543
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
2441 102 
Gebäudestatus Gebäudekategorie
bestehend Mehrfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
65 3
2002 2001-2005 - 
Utilisateur Invité
Druckdatum: 12.07.2018 07:21 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Birkengasse 3 4583 Aetigkofen
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
101000543 001  Parterre, mehrgeschoss. Links bestehend
EDID Strasse Eingangsnummer PLZ Ort
1 Birkengasse 5 4583 Aetigkofen
EGID/EWID
AdminNr PhysNr Stockwerk Lage Wohnungs status
101000543
002  Parterre, mehrgeschoss. Rechts bestehend
Utilisateur Invité
Druckdatum: 12.07.2018 07:21 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("101000543", housing.HousingEdids[0].HousingEgidEwids[0].EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids[0].HousingEgidEwids[0].Ewid);
            Assert.AreEqual("101000543 001  Parterre, mehrgeschoss. Links bestehend", housing.HousingEdids[0].HousingEgidEwids[0].Blob);
        }

        [TestMethod]
        private void Test_Pdf_101216503_Egid_Edid_Ewid()
        {
            // Given
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Untere Gasse 15, 4622 Egerkingen
Gemeinde Eidg. Gebäudeidentifikator 101 216 503
2401 Egerkingen
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 385 
Gebäudestatus Gebäudekategorie
bestehend Mehrfamilienhaus, ohne 
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz. Geschosse
241 4
2001 2001-2005 - 
Utilisateur Invité
Druckdatum: 12.07.2018 07:21 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Untere Gasse 15 4622 Egerkingen
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
101216503 002  Parterre bestehend
101216503
003  1. Stock bestehend
101216503
004  2. Stock bestehend
101216503 001  3. Stock bestehend
Utilisateur Invité
Druckdatum: 12.07.2018 07:21 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

        }

        [TestMethod]
        public void Test_Pdf_793635fr_Egid()
        {
            // Given
            string data = @"Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
Chemin du Moulin du Choc 11, 1121 Bremblens
Commune Id. fédéral de bâtiment 000 793 635
5622 Bremblens
Numéro de parcelle
Secteur RF N° de parcelle N° officiel du bâtiment
0 123 141
Statut du bâtiment Catégorie de bâtiment
existant Maison d'habitation avec usage 
annexe
Année de construction Rénovation Démolition
Surface du bâtiment (en m²) Nbre de niveaux
133 2
1962 1961-1970 2013 2011-2015 
Utilisateur Invité
Date d'impression: 26.07.2018 21:39 1 2
/Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
EDID Rue N° d'entrée NPA Lieu
0 Chemin du Moulin du Choc 11 1121 Bremblens
EGID/EWID N° admin. du logement N° phys. de logement Etage Situation Statut du logement
793635 001  REZ, sur plus. étages existant
Utilisateur Invité
Date d'impression: 26.07.2018 21:39 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("000793635", housing.Egid);
            Assert.AreEqual("1962", housing.Baujahr1);
            Assert.AreEqual("1961-1970", housing.Baujahr2);
            Assert.AreEqual("2013", housing.Renovation1);
            Assert.AreEqual("2011-2015", housing.Renovation2);
            Assert.IsNull(housing.Abbruch);
        }

        [TestMethod]
        public void Test_Pdf_793635fr_Egid_Edid_Egid()
        {
            // Given
            string data = @"Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
Chemin du Moulin du Choc 11, 1121 Bremblens
Commune Id. fédéral de bâtiment 000 793 635
5622 Bremblens
Numéro de parcelle
Secteur RF N° de parcelle N° officiel du bâtiment
0 123 141
Statut du bâtiment Catégorie de bâtiment
existant Maison d'habitation avec usage 
annexe
Année de construction Rénovation Démolition
Surface du bâtiment (en m²) Nbre de niveaux
133 2
1962 1961-1970 2013 2011-2015 
Utilisateur Invité
Date d'impression: 26.07.2018 21:39 1 2
/Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
EDID Rue N° d'entrée NPA Lieu
0 Chemin du Moulin du Choc 11 1121 Bremblens
EGID/EWID N° admin. du logement N° phys. de logement Etage Situation Statut du logement
793635 001  REZ, sur plus. étages existant
Utilisateur Invité
Date d'impression: 26.07.2018 21:39 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("793635", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().Ewid);
            Assert.AreEqual("REZ, sur plus. étages", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().Stockwerk);
            Assert.AreEqual("existant", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().WohnungStatus);
        }

        [TestMethod]
        public void Test_Pdf_793635de_Egid()
        {
            // Given
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Chemin du Moulin du Choc 11, 1121 Bremblens
Gemeinde Eidg. Gebäudeidentifikator 000 793 635
5622 Bremblens
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 123 141
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
133 2
1962 1961-1970 2013 2011-2015 
Utilisateur Invité
Druckdatum: 06.08.2018 21:19 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Chemin du Moulin du Choc 11 1121 Bremblens
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
793635 001  Parterre, mehrgeschoss. bestehend
Utilisateur Invité
Druckdatum: 06.08.2018 21:19 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("000793635", housing.Egid);
            Assert.AreEqual("1962", housing.Baujahr1);
            Assert.AreEqual("1961-1970", housing.Baujahr2);
            Assert.AreEqual("2013", housing.Renovation1);
            Assert.AreEqual("2011-2015", housing.Renovation2);
            Assert.IsNull(housing.Abbruch);
        }

        [TestMethod]
        public void Test_Pdf_793635de_Egid_Edid_Egid()
        {
            // Given
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Chemin du Moulin du Choc 11, 1121 Bremblens
Gemeinde Eidg. Gebäudeidentifikator 000 793 635
5622 Bremblens
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 123 141
Gebäudestatus Gebäudekategorie
bestehend Wohngebäude mit Nebennutzung
Baujahr Renovation Abbruch Gebäudefläche (in m²) Anz. Geschosse
133 2
1962 1961-1970 2013 2011-2015 
Utilisateur Invité
Druckdatum: 06.08.2018 21:19 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Chemin du Moulin du Choc 11 1121 Bremblens
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
793635 001  Parterre, mehrgeschoss. bestehend
Utilisateur Invité
Druckdatum: 06.08.2018 21:19 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("793635", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().EgidEwid);
            Assert.AreEqual("001", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().Ewid);
            Assert.AreEqual("Parterre, mehrgeschoss.", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().Stockwerk);
            Assert.AreEqual("bestehend", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.FirstOrDefault().WohnungStatus);
        }

        [TestMethod]
        public void Test_Pdf_819938fr_Egid_Edid_Egid()
        {
            // Given
            string data = @"Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
Rue de l'Agriculture 9, 1337 Vallorbe
Rue de l'Agriculture 9a, 1337 Vallorbe
Commune Id. fédéral de bâtiment
5764 Vallorbe 000 819 938
Numéro de parcelle
Secteur RF N° de parcelle N° officiel du bâtiment
0 428 86
Statut du bâtiment Catégorie de bâtiment
existant Maison d'habitation avec usage 
annexe
Année de construction Rénovation Démolition
Surface du bâtiment (en m²) Nbre de niveaux
225 3
1880 Avant 1919 2017 > 2015 
Utilisateur Invité
Date d'impression: 27.07.2018 05:05 1 2
/Département fédéral de l'intérieur DFI
Office fédéral de la statistique OFS
EDID Rue N° d'entrée NPA Lieu
0 Rue de l'Agriculture 9 1337 Vallorbe
EGID/EWID N° admin. du logement N° phys. de logement Etage Situation Statut du logement
819938 002  Rez-de-chaussée existant
819938
003  1er étage existant
819938
004  2e étage existant
EDID Rue N° d'entrée NPA Lieu
1 Rue de l'Agriculture 9a 1337 Vallorbe
EGID/EWID N° admin. du logement N° phys. de logement Etage Situation Statut du logement
Utilisateur Invité
Date d'impression: 27.07.2018 05:05 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("819938", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().EgidEwid);
            Assert.AreEqual("004", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Ewid);
            Assert.AreEqual("2e étage", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Stockwerk);
            Assert.AreEqual("existant", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().WohnungStatus);
        }

        [TestMethod]
        public void Test_Pdf_759972it_Egid_Edid_Egid()
        {
            // Given
            string data = @"Dipartimento federale dell'interno DFI
Ufficio federale di statistica
Via Beroldingen 13, 6850 Mendrisio
Comune Id. federale dell'edificio 000 759 972
5254 Mendrisio
Numero di particella
Settore RF N. di particella N. ufficiale dell'edificio
1 1242 1242a
Stato dell'edificio Categoria dell'edificio
esistente Casa plurifamiliare senza 
utilizzazione accessoria
Anno di costruzione Ristrutturazione Demolizione
Superficie dell'edificio (in m²) Numero di piani
268 2
1938 1919-1945 1990 1986-1990 
Utilisateur Invité
Data di stampa: 25.07.2018 17:28 1 2
/Dipartimento federale dell'interno DFI
Ufficio federale di statistica
EDID Via Numero dell'entrata NPA Luogo
0 Via Beroldingen 13 6850 Mendrisio
EGID/EWID N. amin N. fisico Piano Posizione Stato dell'abitazione
759972 001 10617_2 Pianterreno esistente
759972
002 3711_3 1° piano esistente
Utilisateur Invité
Data di stampa: 25.07.2018 17:28 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("759972", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().EgidEwid);
            Assert.AreEqual("002", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Ewid);
            Assert.AreEqual("1° piano", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Stockwerk);
            Assert.AreEqual("esistente", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().WohnungStatus);
        }

        [TestMethod]
        public void AnalyseConstructionYear_Baujahr1_Baujahr2()
        {
            // Arrange 
            string value = "2017 Nach 2015 - ";

            // Act
            Housing housing = new Housing();
            AnalysePdfData apd = new AnalysePdfData();
            apd.ConstructionYear(housing, value);

            // Assert
            Assert.AreEqual("2017", housing.Baujahr1);
            Assert.AreEqual("Nach 2015", housing.Baujahr2);
            Assert.IsNull(housing.Renovation1);
            Assert.AreEqual("-", housing.Renovation2);
            Assert.IsNull(housing.Abbruch);
        }

        // string value =" Vor 1919 1986-1990 "
        [TestMethod]
        public void AnalyseConstructionYear_Baujahr2_Renovation2()
        {
            // Arrange 
            string value = " Vor 1919 1986-1990 ";

            // Act
            Housing housing = new Housing();
            AnalysePdfData apd = new AnalysePdfData();
            apd.ConstructionYear(housing, value);

            // Assert
            Assert.IsNull(housing.Baujahr1);
            Assert.AreEqual("Vor 1919", housing.Baujahr2);
            Assert.IsNull(housing.Renovation1);
            Assert.AreEqual("1986-1990", housing.Renovation2);
            Assert.IsNull(housing.Abbruch);
        }

        [TestMethod]
        public void AnalyseConstructionYear_Baujahr1_Baujahr2_Renovation1_Renovation2()
        {
            // Arrange 
            string value = "1938 1919-1945 1990 1986-1990 ";

            // Act
            Housing housing = new Housing();
            AnalysePdfData apd = new AnalysePdfData();
            apd.ConstructionYear(housing, value);

            // Assert
            Assert.AreEqual("1938", housing.Baujahr1);
            Assert.AreEqual("1919-1945", housing.Baujahr2);
            Assert.AreEqual("1990", housing.Renovation1);
            Assert.AreEqual("1986-1990", housing.Renovation2);
            Assert.IsNull(housing.Abbruch);
        }

        [TestMethod]
        public void Test_Pdf_190009706_Egid_Edid_Egid()
        {
            // Given
            string data = @"Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
Stapfackerstrasse 3, 4658 Däniken SO
Gemeinde Eidg. Gebäudeidentifikator 190 009 706
2572 Däniken
Parzellennummer
GB-Kreis ParzNr. Amtliche Gebäudenummer
0 1922 
Gebäudestatus Gebäudekategorie
bestehend Mehrfamilienhaus, ohne
Nebennutzung
Baujahr Renovation Abbruch
Gebäudefläche (in m²) Anz.Geschosse
319 4
2003 2001-2005 - 
Utilisateur Invité
Druckdatum: 12.07.2018 07:48 1 2
/Eidgenössisches Departement des Innern EDI
Bundesamt für Statistik
EDID Strasse Eingangsnummer PLZ Ort
0 Stapfackerstrasse 3 4658 Däniken SO
EGID/EWID AdminNr PhysNr Stockwerk Lage Wohnungs status
190009706 002  Parterre Rechts bestehend
190009706
001  Parterre Links bestehend
190009706
010  Parterre, mehrgeschoss.Mitte bestehend
190009706 004  1. Stock Rechts bestehend
190009706
003  1. Stock Links bestehend
190009706
007  2. Stock Rechts bestehend
190009706 005  2. Stock Links bestehend
190009706
006  2. Stock, mehrgeschoss.Mitte bestehend
190009706
008  3. Stock, mehrgeschoss. 5 1/2 Zimmer 1.DG/2. DG bestehend
190009706
009  3. Stock, mehrgeschoss. 5 1/2 Zimmer 1. DG/2. DG bestehend
Utilisateur Invité
Druckdatum: 12.07.2018 07:48 2 2
/";

            // Then
            AnalysePdfData apd = new AnalysePdfData();
            List<string> housingList = apd.ConvertTextToList(data);
            Housing housing = apd.AnalysePdf(housingList, data);

            // Assert

            Assert.AreEqual("190009706", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().EgidEwid);
            Assert.AreEqual("009", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Ewid);
            Assert.AreEqual("3. Stock, mehrgeschoss.", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().Stockwerk);
            Assert.AreEqual("bestehend", housing.HousingEdids.FirstOrDefault().HousingEgidEwids.LastOrDefault().WohnungStatus);
        }

        
    }
}

