using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwrsyncronizer.DataAccess.DbModel
{
    public class Edids
    {
        #region IIdentifiedRecord<int> Members

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        #endregion

        #region Properties
        [Column("edid")]
        public string Edid { get; set; }
        [Column("strasse")]
        public string Strasse { get; set; }
        [Column("eingangnummer")]
        public string Eingangnummer { get; set; }
        [Column("plz")]
        public string Plz { get; set; }
        [Column("ort")]
        public string Ort { get; set; }
        [Column("blob")]
        public string Blob { get; set; }
        [Column("guid")]
        public string Guid { get; set; }
        [Column("guid_egid_reference")]
        public string GuidEgidReference { get; set; }
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
