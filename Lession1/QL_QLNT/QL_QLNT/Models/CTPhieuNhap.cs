using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("CTPhieuNhap")]
    public class CTPhieuNhap
    {
        [Key]
        [StringLength(10)]
        public string MaCT { get; set; }

        [StringLength(10)]
        public string MaPN { get; set; }

        [StringLength(10)]
        public string MaSP { get; set; }

        public int? SoLuong { get; set; }

        // Navigation properties
        [ForeignKey("MaPN")]
        public virtual PhieuNhap PhieuNhap { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham SanPham { get; set; }
    }
}