using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_QLNT.Models
{
    [Table("Quyen")]
    public class Quyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaQuyen { get; set; }

        public int? MaMH { get; set; }

        [StringLength(6)]
        public string MaNhom { get; set; }

        // Navigation properties
        [ForeignKey("MaMH")]
        public virtual ManHinh ManHinh { get; set; }

        [ForeignKey("MaNhom")]
        public virtual NhomNguoiDung NhomNguoiDung { get; set; }
    }
}