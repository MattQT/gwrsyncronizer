using gwrsyncronizer.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace gwrsyncronizer.BLL
{
    public class AnalysePdfData
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Housing AnalysePdf(List<string> dataList, string orignialData)
        {
            Housing housing = new Housing
            {
                GuidEgid = Guid.NewGuid().ToString(),
                HousingEdids = new List<HousingEdid>(),
                Blob = orignialData
            };

            List<string> languageSettings = IdentifyLanguage(dataList);

            // Analyse Gemeinde
            AnalyseCommunityTag(dataList, housing, languageSettings);

            // Analyse Parzellennummer
            AnalyseParcelInformation(dataList, housing, languageSettings);

            // Analyse Gebäudestatus
            AnalydeBuildingStatus(dataList, housing, languageSettings);

            // Analyse Baujahr
            AnalyseConstructionYear(dataList, housing, languageSettings);

            // Analyse EDID/EWID
            AnalyseEdidEwid(dataList, housing);

            return housing;
        }

        private void AnalyseEdidEwid(List<string> dataList, Housing housing)
        {
            List<int> indicesEdids = dataList.Select((s, y) => new { Str = s, Index = y })
                                                            .Where(x => x.Str.Contains("EDID"))
                                                            .Select(x => x.Index).ToList();
            for (int n = 0; n < indicesEdids.Count; n++)
            {
                string tempGuidEgid = Guid.NewGuid().ToString();
                string toAnalyse = dataList[indicesEdids[n] + 1];
                //0 Schulhausweg 7 4571 Lüterkofen
                List<string> edid = AnalyseEdidValue(toAnalyse);
                List<HousingEgidEwid> hee = new List<HousingEgidEwid>();

                // Analyse EWID
                //for (int n = 0; n < indicesEdids.Count; n++)
                //{
                int start = indicesEdids[n];

                int end = dataList.Count - start;

                if (n + 1 <= indicesEdids.Count - 1)
                {
                    end = indicesEdids[n + 1] - start;
                }

                //
                var sublist = dataList.GetRange(start, end);

                List<int> allIndicesEgidEwid = sublist.Select((s, y) => new { Str = s, Index = y })
                .Where(x => x.Str.Contains("EGID/EWID"))
                .Select(x => x.Index).ToList();

                if (allIndicesEgidEwid.Count > 0)
                {
                    AnalyseEwidValues(tempGuidEgid, hee, sublist);
                }

                if (edid.Count == 5)
                {
                    housing.HousingEdids.Add(
                    new HousingEdid
                    {
                        GuidEdid = tempGuidEgid,
                        GuidEgidReference = housing.GuidEgid,
                        Edid = edid[0],
                        Strasse = edid[1],
                        Eingangnummer = edid[2],
                        Plz = edid[3],
                        Ort = edid[4],
                        HousingEgidEwids = hee
                    });
                }
                else
                {
                    Logger.Error("Edid missing");
                }
            }
        }

        private void AnalyseEwidValues(string tempGuidEgid, List<HousingEgidEwid> hee, List<string> sublist)
        {
            var searchPattern = new Regex(@"^\d{4,}", RegexOptions.IgnoreCase);

            List<int> resultList = sublist.Select((s, y) => new { Str = s, Index = y })
                .Where(x => searchPattern.IsMatch(x.Str))
                .Select(x => x.Index).ToList();

            foreach (var item in resultList)
            {
                string ewidToAnalyse = sublist[item];
                CheckEwidStringToAnalyse(sublist, item, ref ewidToAnalyse);

                List<string> egidEwid = DivideEwidValuesToElements(ewidToAnalyse);

                hee.Add(new HousingEgidEwid
                {
                    GuidEwid = Guid.NewGuid().ToString(),
                    GuidEdidReference = tempGuidEgid,
                    EgidEwid = egidEwid[0],
                    Ewid = egidEwid[1],
                    AdminNr = egidEwid[2],
                    PhysNr = egidEwid[3],
                    Stockwerk = egidEwid[4],
                    Lage = egidEwid[5],
                    WohnungStatus = egidEwid[6],
                    Blob = ewidToAnalyse
                });

            }

        }

        public List<string> DivideEwidValuesToElements(string ewidToAnalyse)
        {
            Match match = TestWithRegularExpression(@"^\d+\s\d+", ewidToAnalyse);
            string egidEwid = string.Empty;
            string ewid = string.Empty;
            if (match.Success)
            {
                egidEwid = SplitLine(match.Value, 2)[0];
                ewid = SplitLine(match.Value, 2)[1];
                ewidToAnalyse = ewidToAnalyse.Replace(match.Value, string.Empty).Trim();
            }

            int last = ewidToAnalyse.LastIndexOf(' ');
            string status = string.Empty;
            if (ewidToAnalyse.Length > 0)
            {
                status = ewidToAnalyse.Substring(last, ewidToAnalyse.Length - last).Trim();
                ewidToAnalyse = ewidToAnalyse.Replace(status, string.Empty).TrimEnd();
            }
            
            match = TestWithRegularExpression(@"^\d+((_\d+)\s|\s)", ewidToAnalyse);
            string adminNr = string.Empty;
            if (match.Success)
            {
                adminNr = match.Value.Trim();
                ewidToAnalyse = ewidToAnalyse.Replace(match.Value, string.Empty);
            }
            else
            {
                Match innerMatch = TestWithRegularExpression(@"_\d+\s", ewidToAnalyse);
                if (innerMatch.Success)
                {
                    adminNr = innerMatch.Value.Replace("_", string.Empty).Trim();
                    ewidToAnalyse = ewidToAnalyse.Replace(innerMatch.Value, string.Empty);
                }
            }

            string stockwerk = string.Empty;
            List<string> parterreList = new List<string>();
            parterreList.Add("Parterre, mehrgeschoss.");
            parterreList.Add("Parterre");
            parterreList.Add("Rez-de-chaussée");
            parterreList.Add("REZ, sur plus. étages");
            parterreList.Add("Pianterreno");
            parterreList.Add("Pianterre., di più piani");
            foreach (var item in parterreList)
            {
                if (ewidToAnalyse.Contains(item))
                {
                    stockwerk = item;
                    ewidToAnalyse = ewidToAnalyse.Replace(item, string.Empty).TrimStart();
                    break;
                }
            }

            if (string.IsNullOrEmpty(stockwerk))
            {
                match = TestWithRegularExpression(@"((\d+([.°]|\w+)\s[\w]*)(-\w+)*)", ewidToAnalyse);
                if (match.Success)
                {
                    Match innermatch = TestWithRegularExpression(@"\,\s.*\.", ewidToAnalyse);
                    if (innermatch.Success)
                    {
                        int pointIndex = innermatch.Value.IndexOf('.');
                        string tmpStockwerk = innermatch.Value.Substring(0, pointIndex+1);
                        stockwerk = string.Format("{0}{1}", match.Value, tmpStockwerk);
                    }
                    else
                    {
                        stockwerk = match.Value.Trim();
                    }
                    
                    ewidToAnalyse = ewidToAnalyse.Replace(stockwerk, string.Empty);
                }
                else
                {

                    Logger.Error("No Stockwerk match so far {0}", ewidToAnalyse);
                    last = ewidToAnalyse.LastIndexOf(' ');
                    last = last == -1 ? ewidToAnalyse.Length : last;
                    stockwerk = ewidToAnalyse.Substring(0, last).Trim();
                }
            }

            if (ewidToAnalyse.Length > 0)
            {
                ewidToAnalyse = ewidToAnalyse.Replace(stockwerk, string.Empty);
            }


            string toAnalyse = ewidToAnalyse.Replace(" ", string.Empty);
            string lage = string.Empty;
            if (toAnalyse.Length > 0)
            {
                lage = ewidToAnalyse.Trim();
            }

            List<string> result = new List<string>();
            result.Add(string.IsNullOrEmpty(egidEwid) ? "-1" : egidEwid);
            result.Add(string.IsNullOrEmpty(ewid) ? "-1" : ewid);
            result.Add(string.IsNullOrEmpty(adminNr) ? string.Empty : adminNr);
            result.Add(string.Empty);
            result.Add(string.IsNullOrEmpty(stockwerk) ? string.Empty : stockwerk);
            result.Add(string.IsNullOrEmpty(lage) ? string.Empty : lage);
            result.Add(string.IsNullOrEmpty(status) ? string.Empty : status);
            return result;
        }

        public void AnalyseConstructionYear(List<string> dataList, Housing housing, List<string> languageSettings)
        {
            List<int> indicesConstructionYear = dataList.Select((s, y) => new { Str = s, Index = y })
                                                            .Where(x => x.Str.Contains(languageSettings[4]))
                                                            .Select(x => x.Index).ToList();

            if (indicesConstructionYear.Count == 1)
            {
                // Builidng Area and numbre levels
                Match buildingMatch = TestWithRegularExpression(@"\([\w]*\s[\w]*²\)\s[\d]{1,}\s((\w+\.\s\w+\s)|(\w+\s\w+\s\w+\s))[\d]{1,}", dataList[indicesConstructionYear[0] + 1]);
                if (buildingMatch.Success)
                {
                    List<string> values = new List<string>();
                    foreach (Match m in Regex.Matches(dataList[indicesConstructionYear[0] + 1], @"[\d]{1,}"))
                        values.Add(m.Value);

                    housing.GebFlaeche = string.IsNullOrEmpty(values[0].Replace(" ", string.Empty)) ? "0" : values[0].Replace(" ", string.Empty);
                    housing.AnzGeschosse = string.IsNullOrEmpty(values[1].Replace(" ", string.Empty)) ? "0" : values[1].Replace(" ", string.Empty);


                }
                else if (dataList[indicesConstructionYear[0] + 1].Contains(languageSettings[6]))
                {
                    string toAnalyseBuilding = dataList[indicesConstructionYear[0] + 2];

                    List<string> building = SplitLine(toAnalyseBuilding, 2);
                    housing.GebFlaeche = string.IsNullOrEmpty(building[0].Replace(" ", string.Empty)) ? "0" : building[0].Replace(" ", string.Empty);
                    housing.AnzGeschosse = string.IsNullOrEmpty(building[1].Replace(" ", string.Empty)) ? "0" : building[1].Replace(" ", string.Empty);
                }
                else
                {
                    string toAnalyseBuilding = dataList[indicesConstructionYear[0] + 1];

                    List<string> building = SplitLine(toAnalyseBuilding, 2);
                    housing.GebFlaeche = string.IsNullOrEmpty(building[0].Replace(" ", string.Empty)) ? "0" : building[0].Replace(" ", string.Empty);
                    housing.AnzGeschosse = string.IsNullOrEmpty(building[1].Replace(" ", string.Empty)) ? "0" : building[1].Replace(" ", string.Empty);
                }


                // Construction Year
                List<int> indicesEdids = dataList.Select((s, y) => new { Str = s, Index = y })
                                                .Where(x => x.Str.Contains("EDID"))
                                                .Select(x => x.Index).ToList();

                string toAnalyseYears;
                if (indicesEdids.Count > 0)
                {
                    toAnalyseYears = dataList[indicesEdids[0] - 1];
                }
                else
                {
                    //if (dataList.Count >= indicesConstructionYear[0] + 3)
                    //{
                    //    toAnalyseYears = dataList[indicesConstructionYear[0] + 1].Contains(languageSettings[6])
                    //        ? dataList[indicesConstructionYear[0] + 3]
                    //        : dataList[indicesConstructionYear[0] + 2];
                    //}
                    //else
                    //{
                    //    toAnalyseYears = dataList[indicesConstructionYear[0] + 1];
                    //}
                    toAnalyseYears = dataList[dataList.Count - 1];
                }

                ConstructionYear(housing, toAnalyseYears);

            }
            else
            {
                Logger.Error("More than one Baujahr tag found");
            }
        }

        public void ConstructionYear(Housing housing, string toAnalyseYears)
        {
            // Test if string starts with space Baujahr1
            Match match = TestWithRegularExpression(@"^\s{1}", toAnalyseYears);
            if (match.Success)
            {
                housing.Baujahr1 = null;
                int length = match.Value.Length;
                toAnalyseYears = toAnalyseYears.Substring(length, toAnalyseYears.Length - length);
            }
            else
            {
                Match matchYear = TestWithRegularExpression(@"^(\d{1,4}\s|[\w]*\s\d{1,4}\s)", toAnalyseYears);
                if (matchYear.Success)
                {
                    housing.Baujahr1 = matchYear.Value.Trim();
                    int length = matchYear.Value.Length;
                    toAnalyseYears = toAnalyseYears.Substring(length, toAnalyseYears.Length - length);
                }
            }

            // Baujahr2
            match = TestWithRegularExpression(@"^(\d{1,4}|[\d]*-[\d]*|^([\w]*)\s\d{4})\s", toAnalyseYears);
            if (match.Success)
            {
                housing.Baujahr2 = match.Value.Trim();
                int length = match.Value.Length - 1;
                toAnalyseYears = toAnalyseYears.Substring(length, toAnalyseYears.Length - length);
            }

            // Renovation2
            match = TestWithRegularExpression(@"\s(-\s|[\d]*-[\d]*\s)", toAnalyseYears);
            if (match.Success)
            {
                housing.Renovation2 = match.Value.Trim();
                int length = match.Value.Length;
                toAnalyseYears = toAnalyseYears.Substring(0, toAnalyseYears.Length - length);
            }

            // Renovation1
            match = TestWithRegularExpression(@"^(\d{1,4}|[\d]*-[\d]*|^([\w]*)\s\d{4})", toAnalyseYears);
            if (match.Success)
            {
                housing.Renovation1 = match.Value.Trim();
                int length = match.Value.Length;
                toAnalyseYears = toAnalyseYears.Substring(length, toAnalyseYears.Length - length);
            }

            // Abbruch
            // At the moment we only have existing Values!


        }

        private void AnalydeBuildingStatus(List<string> dataList, Housing housing, List<string> languageSettings)
        {
            List<int> indicesCommunity = dataList.Select((s, y) => new { Str = s, Index = y })
                                                            .Where(x => x.Str.Contains(languageSettings[3]))
                                                            .Select(x => x.Index).ToList();
            List<int> indicesConstructionYear = dataList.Select((s, y) => new { Str = s, Index = y })
                                                .Where(x => x.Str.Contains(languageSettings[4]))
                                                .Select(x => x.Index).ToList();

            if (indicesCommunity.Count == 1)
            {
                if (indicesCommunity[0] + 1 == indicesConstructionYear[0])
                {
                    List<string> statusKategorie = SplitLine(dataList[indicesCommunity[0]], 4);
                    housing.GebStatus = statusKategorie[1];
                    housing.GebKategorie = statusKategorie[3];
                }
                else if (indicesCommunity[0] + 1 != indicesConstructionYear[0])
                {
                    if (!dataList[indicesCommunity[0] + 1].Contains(languageSettings[5]))
                    {
                        string temp = String.Format("{0} {1}", dataList[indicesCommunity[0]], dataList[indicesCommunity[0] + 1]);
                        List<string> tempStatusKategorie = SplitLine(temp, 4);
                        housing.GebStatus = tempStatusKategorie[1];
                        housing.GebKategorie = tempStatusKategorie[3];
                    }
                    else
                    {
                        List<string> statusKategorie = SplitLine(dataList[indicesCommunity[0] + 1], 2);
                        StringBuilder sb = new StringBuilder();
                        if (!dataList[indicesCommunity[0] + 2].Contains(languageSettings[4]))
                        {
                            sb.Append(statusKategorie[1]);
                            sb.Append(dataList[indicesCommunity[0] + 2]);
                        }
                        else
                        {
                            sb.Append(statusKategorie[1]);
                        }

                        housing.GebStatus = statusKategorie[0];
                        housing.GebKategorie = sb.ToString();
                    }

                }
                else
                {
                    Logger.Error("Parzellennummer identifikation went wrong");
                }
            }
            else
            {
                Logger.Error("More than one Gebäudestatus tags found");
            }
        }

        private void AnalyseParcelInformation(List<string> dataList, Housing housing, List<string> languageSettings)
        {
            List<int> indicesCommunity = dataList.Select((s, y) => new { Str = s, Index = y })
                                                .Where(x => x.Str.Contains(languageSettings[2]))
                                                .Select(x => x.Index).ToList();

            if (indicesCommunity.Count == 1)
            {
                if (dataList[indicesCommunity[0] + 3].Contains(languageSettings[3]))
                {
                    List<string> parz = SplitLine(dataList[indicesCommunity[0] + 2], 3);
                    housing.GbKreis = parz[0];
                    housing.ParzNr = parz[1];
                    housing.AmtGebnr = parz[2];
                }
                else
                {
                    Logger.Error("Parzellennummer identifikation went wrong");
                }
            }
            else
            {
                Logger.Error("More than one Parzellennummer tag found");
            }
        }

        public void AnalyseCommunityTag(List<string> dataList, Housing housing, List<string> languageSettings)
        {
            List<int> indicesCommunity = dataList.Select((s, y) => new { Str = s, Index = y })
                                    .Where(x => x.Str.Contains(languageSettings[1]))
                                    .Select(x => x.Index).ToList();

            if (indicesCommunity.Count == 1)
            {
                string toTest = dataList[indicesCommunity[0]];
                Match m = TestWithRegularExpression(@"\s\d{3}\s\d{3}\s\d{3}|\s\d{3}\d{3}\d{3}", dataList[indicesCommunity[0]]);
                if (m.Success)
                {
                    toTest = toTest.Replace(m.Value, string.Empty).Replace(languageSettings[1], string.Empty).Replace(languageSettings[0], string.Empty).TrimStart();
                    housing.Egid = m.Value.Replace(" ", string.Empty);
                    if (toTest.Length > 0)
                    {
                        int first = toTest.IndexOf(" ", StringComparison.Ordinal);
                        housing.GemName = toTest.Substring(first, toTest.Length - first).Trim();
                        housing.GemNr = toTest.Substring(0, first).Trim();
                    }
                    else
                    {
                        string numberName = dataList[indicesCommunity[0] + 1];
                        int first = numberName.IndexOf(" ", StringComparison.Ordinal);
                        housing.GemName = numberName.Substring(first, numberName.Length - first).Trim();
                        housing.GemNr = numberName.Substring(0, first).Trim();
                    }

                }
                else
                {
                    toTest = dataList[indicesCommunity[0] + 1];
                    Match matchInner = TestWithRegularExpression(@"\s\d{3}\s\d{3}\s\d{3}|\s\d{3}\d{3}\d{3}", toTest);
                    if (matchInner.Success)
                    {
                        string tempEgid = matchInner.Value;
                        housing.Egid = tempEgid.Replace(" ", string.Empty);
                        toTest = toTest.Replace(tempEgid, string.Empty);
                        List<string> parzInner = SplitLine(toTest, 2);
                        housing.GemNr = parzInner[0];
                        housing.GemName = parzInner[1];

                    }
                }
            }
            else
            {
                Logger.Error("More than one Gemeinde tag found");
            }
        }

        public void CheckEwidStringToAnalyse(List<string> sublist, int item, ref string ewidToAnalyse)
        {

            string toValidate = sublist[item];

            Match m = TestWithRegularExpression(@"^\d{4,}\s\d+", toValidate);
            if (!m.Success)
            {
                if (item + 1 < sublist.Count)
                {
                    ewidToAnalyse = String.Format("{0} {1}", toValidate, sublist[item + 1]);
                }
                else
                {
                    Logger.Error("No matching values to validate {0}", ewidToAnalyse);
                }
                
            }

        }

        public List<string> ConvertTextToList(string data)
        {
            // ToDo: extract list into a config file
            List<string> valueToRemove = new List<string>();
            valueToRemove.Add("Eidgenössisches Departement des Innern EDI");
            valueToRemove.Add("Bundesamt für Statistik");
            valueToRemove.Add("Utilisateur Invité");
            valueToRemove.Add("Druckdatum");
            valueToRemove.Add("/");
            valueToRemove.Add("Département fédéral de l'intérieur DFI");
            valueToRemove.Add("Office fédéral de la statistique OFS");
            valueToRemove.Add("Date d'impression");
            valueToRemove.Add("Dipartimento federale dell'interno DFI");
            valueToRemove.Add("Ufficio federale di statistica");
            valueToRemove.Add("Data di stampa");



            using (StringReader reader = new StringReader(data))
            {
                List<string> housingList = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Substring(0, 1) == "/") continue;
                    if (!valueToRemove.Any(x => line.Contains(x)) || line.Contains("EGID/EWID") || (line.Contains("/") && line.Length > 1))
                    {
                        housingList.Add(line);
                        //Console.WriteLine(line);
                    }

                    //Console.WriteLine(line);

                    // Do something with the line
                }
                return housingList;
            }
        }

        private List<string> SplitLine(string value, int slices)
        {
            string delimStr = " ";
            char[] delimiter = delimStr.ToCharArray();
            var split = value.Split(delimiter, slices);
            return split.ToList();
        }

        private Match TestWithRegularExpression(string reg, string value)
        {
            Regex regex = new Regex(reg);
            Match match = regex.Match(value);
            return match;

        }

        public List<string> AnalyseEdidValue(string value)
        {
            List<string> result = new List<string>();

            // finds the edid
            Match match = TestWithRegularExpression(@"[\d]{1,}\s", value);
            if (match.Success)
            {
                result.Add(match.Value.Replace(" ", string.Empty));

                value = value.Substring(match.Value.Length, value.Length - match.Value.Length);
            }

            // finds the plz
            match = TestWithRegularExpression(@"[\d]{4,4}\s", value);
            if (match.Success)
            {
                string plzValue = match.Value;
                string plzOrt = value.Substring(match.Index, value.Length - match.Index);
                value = value.Replace(plzOrt, string.Empty).Trim();

                match = TestWithRegularExpression(@"[\d]", value);
                if (match.Success)
                {
                    int lastIndex = value.LastIndexOf(' ');
                    if (lastIndex == -1)
                    {
                        result.Add(value);
                        result.Add(string.Empty);
                    }
                    else
                    {
                        string number = value.Substring(lastIndex, value.Length - lastIndex).Trim();
                        value = value.Replace(number, string.Empty).Trim();
                        result.Add(value);
                        result.Add(number);
                    }
                }
                else
                {
                    result.Add(value);
                    result.Add(string.Empty);
                }

                result.Add(plzValue.Trim());
                plzOrt = plzOrt.Replace(plzValue, string.Empty).Trim();
                result.Add(plzOrt);
            }
            return result;
        }

        public List<string> IdentifyLanguage(List<string> data)
        {
            List<string> languageSettings = new List<string>();

            // Fr
            if (data.Any(x => x.Contains("Commune")))
            {
                languageSettings.Add("Id. fédéral de bâtiment");
                languageSettings.Add("Commune");
                languageSettings.Add("Numéro de parcelle");
                languageSettings.Add("Statut du bâtiment");
                languageSettings.Add("Année de construction");
                languageSettings.Add("existant");
                languageSettings.Add("Surface");
                return languageSettings;
            }
            else if (data.Any(x => x.Contains("Comune")))
            {
                languageSettings.Add("Id. federale dell'edificio");
                languageSettings.Add("Comune");
                languageSettings.Add("Numero di particella");
                languageSettings.Add("Stato dell'edificio");
                languageSettings.Add("Anno di costruzione");
                languageSettings.Add("esistente");
                languageSettings.Add("Superficie");
                return languageSettings;
            }
            else
            {
                // Standard De
                languageSettings.Add("Eidg. Gebäudeidentifikator");
                languageSettings.Add("Gemeinde");
                languageSettings.Add("Parzellennummer");
                languageSettings.Add("Gebäudestatus");
                languageSettings.Add("Baujahr");
                languageSettings.Add("bestehend");
                languageSettings.Add("Gebäudefläche");

                return languageSettings;
            }
        }
    }
}

