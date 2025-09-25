using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace NetCoreMVCLab03.Models
{
    public class Category
    {
        [key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]]
        public string Name { get; set; }
        [Column[TypeName = "nvarchar(255)"]]
        public Status {get; set; }

        public DataTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<Product> Products { get; set; }
    }
}
