using QL_QLNT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("NhomNguoiDung")]
    public class NhomNguoiDung
    {
        [Key]
        [StringLength(6)]
        public string MaNhom { get; set; }

        [StringLength(255)]
        public string TenNhom { get; set; }

        // Navigation properties
        public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
        public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
        public virtual ICollection<Quyen> Quyens { get; set; } = new List<Quyen>();
    }
}