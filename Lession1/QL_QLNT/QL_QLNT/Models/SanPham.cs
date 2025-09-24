using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        [StringLength(10)]
        public string MaSP { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public bool? TinhTrang { get; set; }

        public int? MaNCC { get; set; }

        public string MoTa { get; set; }

        public int? MaLoai { get; set; }

        [StringLength(50)]
        public string KichThuoc { get; set; }

        [StringLength(50)]
        public string MauSac { get; set; }

        [StringLength(50)]
        public string ChatLieu { get; set; }

        [StringLength(50)]
        public string XuatXu { get; set; }

        [StringLength(20)]
        public string DonVi { get; set; }

        [StringLength(255)]
        public string ChuongTrinhApDung { get; set; }

        public string GhiChu { get; set; }

        public int? SoLuongTon { get; set; }

        public string HinhAnh { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal DonGia { get; set; }

        // Navigation properties
        [ForeignKey("MaLoai")]
        public virtual Loai Loai { get; set; }

        [ForeignKey("MaNCC")]
        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; } = new List<CTHoaDon>();
        public virtual ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; } = new List<CTPhieuNhap>();
    }
}