using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [StringLength(10)]
        public string MaHD { get; set; }

        public DateTime? NgayLap { get; set; }

        [StringLength(10)]
        public string MaKH { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        public bool? TinhTrang { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal? TongTien { get; set; }

        // Navigation properties
        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; } = new List<CTHoaDon>();
    }
}