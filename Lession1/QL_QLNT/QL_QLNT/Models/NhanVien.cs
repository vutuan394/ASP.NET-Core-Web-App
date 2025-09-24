using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(255)]
        public string TenNV { get; set; }

        [StringLength(13)]
        public string CCCD { get; set; }

        public string Email { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(11)]
        public string DienThoai { get; set; }

        public string MatKhau { get; set; }

        [StringLength(6)]
        public string MaNhom { get; set; }

        [StringLength(10)]
        public string TinhTrang { get; set; }

        public string HinhAnh { get; set; }

        // Navigation properties
        [ForeignKey("MaNhom")]
        public virtual NhomNguoiDung NhomNguoiDung { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
    }
}