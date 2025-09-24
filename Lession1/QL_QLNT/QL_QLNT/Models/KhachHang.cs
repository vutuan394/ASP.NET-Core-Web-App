using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [StringLength(10)]
        public string MaKH { get; set; }

        [StringLength(255)]
        public string HoTen { get; set; }

        public string Email { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(11)]
        public string DienThoai { get; set; }

        public string DiaChi { get; set; }

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
    }
}