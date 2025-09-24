using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("CTHoaDon")]
    public class CTHoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCT { get; set; }

        [StringLength(10)]
        public string MaHD { get; set; }

        [StringLength(10)]
        public string MaSP { get; set; }

        public int? SoLuong { get; set; }

        // Navigation properties
        [ForeignKey("MaHD")]
        public virtual HoaDon HoaDon { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham SanPham { get; set; }
    }
}