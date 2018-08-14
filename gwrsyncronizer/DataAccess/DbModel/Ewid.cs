using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwrsyncronizer.DataAccess.DbModel
{
    public class Ewids
    {
        #region IIdentifiedRecord<int> Members

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        #endregion

        #region Properties
        [Column("egid_ewid")]
        public string EgidEwid { get; set; }
        [Column("ewid")]
        public string Ewid { get; set; }
        [Column("admin_nr")]
        public string AdminNr { get; set; }
        [Column("phys_nr")]
        public string PhysNr { get; set; }
        [Column("stockwerk")]
        public string Stockwerk { get; set; }
        [Column("lage")]
        public string Lage { get; set; }
        [Column("wohnung_status")]
        public string WohnungStatus { get; set; }
        [Column("blob")]
        public string Blob { get; set; }
        [Column("guid")]
        public string Guid { get; set; }
        [Column("guid_edid_reference")]
        public string GuidEdidReference { get; set; }
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
