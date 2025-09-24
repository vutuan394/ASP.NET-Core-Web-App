using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("NhaCungCap")]
    public class NhaCungCap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNCC { get; set; }

        [StringLength(255)]
        public string TenNCC { get; set; }

        // Navigation properties
        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}