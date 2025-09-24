using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("PhieuNhap")]
    public class PhieuNhap
    {
        [Key]
        [StringLength(10)]
        public string MaPN { get; set; }

        public DateTime? NgayLap { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TongTien { get; set; }

        // Navigation properties
        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        public virtual ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; } = new List<CTPhieuNhap>();
    }
}