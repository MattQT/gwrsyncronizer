using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwrsyncronizer.DataAccess.DbModel
{
    public class Egids
    {
        #region IIdentifiedRecord<int> Members

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        #endregion

        #region Properties
        [Column("egid")]
        public string Egid { get; set; }
        [Column("gem_nr")]
        public string GemNr { get; set; }
        [Column("gem_name")]
        public string GemName { get; set; }
        [Column("gb_kreis")]
        public string GbKreis { get; set; }
        [Column("parz_nr")]
        public string ParzNr { get; set; }
        [Column("amt_gebnr")]
        public string AmtGebnr { get; set; }
        [Column("geb_status")]
        public string GebStatus { get; set; }
        [Column("geb_kategorie")]
        public string GebKategorie { get; set; }
        [Column("geb_flaeche")]
        public int GebFlaeche { get; set; }
        [Column("anz_geschosse")]
        public int AnzGeschosse { get; set; }
        [Column("baujahr1")]
        public string Baujahr1 { get; set; }
        [Column("baujahr2")]
        public string Baujahr2 { get; set; }
        [Column("renovation1")]
        public string Renovation1 { get; set; }
        [Column("renovation2")]
        public string Renovation2 { get; set; }
        [Column("abbruch")]
        public string Abbruch { get; set; }
        [Column("blob")]
        public string Blob { get; set; }
        [Column("guid")]
        public string Guid { get; set; }
        [Column("valid_from")]
        public DateTime ValidFrom { get; set; }
        [Column("valid_now")]
        public int ValidNow { get; set; }
        [Column("created_user")]
        public string CreatedUser { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("modified_user")]
        public string ModifiedUser { get; set; }
        [Column("modified_at")]
        public DateTime ModifiedAt { get; set; }



        #endregion
    }
}
