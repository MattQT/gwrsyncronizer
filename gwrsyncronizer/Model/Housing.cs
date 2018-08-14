using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwrsyncronizer.Model
{
    public class Housing
    {
        public int Id { get; set; }
        public string Egid { get; set; }
        public string GemNr { get; set; }
        public string GemName { get; set; }
        public string GbKreis { get; set; }
        public string ParzNr { get; set; }
        public string AmtGebnr { get; set; }
        public string GebStatus { get; set; }
        public string GebKategorie { get; set; }
        public string GebFlaeche { get; set; }
        public string AnzGeschosse { get; set; }
        public string Baujahr1 { get; set; }
        public string Baujahr2 { get; set; }
        public string Renovation1 { get; set; }
        public string Renovation2 { get; set; }
        public string Abbruch { get; set; }
        public string Blob { get; set; }
        public string GuidEgid { get; set; }
        public List<HousingEdid> HousingEdids { get; set; }

    }

    public class HousingEdid
    {
        public int Id { get; set; }
        public string Edid { get; set; }
        public string Strasse { get; set; }
        public string Eingangnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Blob { get; set; }
        public string GuidEdid { get; set; }
        public string GuidEgidReference { get; set; }
        public List<HousingEgidEwid> HousingEgidEwids { get; set; }

    }

    public class HousingEgidEwid
    {
        public int Id { get; set; }
        public string EgidEwid { get; set; }
        public string Ewid { get; set; }
        public string AdminNr { get; set; }
        public string PhysNr { get; set; }
        public string Stockwerk { get; set; }
        public string Lage { get; set; }
        public string WohnungStatus { get; set; }
        public string Blob { get; set; }
        public string GuidEwid { get; set; }
        public string GuidEdidReference { get; set; }

    }
}
