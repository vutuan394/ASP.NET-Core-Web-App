using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoai { get; set; }

        [StringLength(100)]
        public string TenLoai { get; set; }

        // Navigation properties
        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}