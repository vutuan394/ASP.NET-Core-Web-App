using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("ManHinh")]
    public class ManHinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMH { get; set; }

        [StringLength(15)]
        public string TenMH { get; set; }

        // Navigation properties
        public virtual ICollection<Quyen> Quyens { get; set; } = new List<Quyen>();
    }
}