using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCoreMVCLab03.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Summary { get; set; }

        public List<Book> GetBookList()
        {
            return new List<Book>
            {
                new Book {
                    Id = 1,
                    Title = "Tâm lý học tội phạm - Phác họa chân dung kẻ phạm tội",
                    AuthorId = 1,
                    GenreId = 5,
                    Price = 150000,
                    TotalPage = 320,
                    Image = "/css/image/b1.jpg",
                    Summary = "Cuốn sách chuyên sâu về tâm lý học tội phạm, phân tích và phác họa chân dung các loại tội phạm. Dịch giả: Đỗ Ái Nhi"
                },
                new Book {
                    Id = 2,
                    Title = "Toàn thư tâm lý học - Khát vọng nguy hiểm bị giấu trong vô thức",
                    AuthorId = 2,
                    GenreId = 5,
                    Price = 180000,
                    TotalPage = 450,
                    Image = "/css/image/b2.jpg",
                    Summary = "Bộ sách toàn diện về tâm lý học, khám phá vô thức và các phương pháp kiểm tra tính cách. Tác giả: Motofumi Fukahori, Dịch giả: Phương Hoa"
                },
                new Book {
                    Id = 3,
                    Title = "Hồ sơ Chung - Tiểu thuyết trinh thám tâm lý",
                    AuthorId = 3,
                    GenreId = 4,
                    Price = 120000,
                    TotalPage = 280,
                    Image = "/css/image/b3.jpg",
                    Summary = "Tiểu thuyết trinh thám tâm lý hấp dẫn của tác giả Franck Thilliez. Dịch giả: Nguyễn Thị Tuới"
                },
                new Book {
                    Id = 4,
                    Title = "Tâm lý học - Nghệ thuật giải mã hành vi",
                    AuthorId = 4,
                    GenreId = 5,
                    Price = 135000,
                    TotalPage = 350,
                    Image = "/css/image/b4.jpg",
                    Summary = "Cuốn sách về nghệ thuật giải mã hành vi con người thông qua tâm lý học. Tác giả: Trần Lộ, Dịch giả: Trần Cẩm Ninh"
                },
                new Book {
                    Id = 5,
                    Title = "Ghi chép pháp y 4 - Những cái chết bí ẩn",
                    AuthorId = 5,
                    GenreId = 4,
                    Price = 110000,
                    TotalPage = 300,
                    Image = "/css/image/b5.jpg",
                    Summary = "Tập 4 của series ghi chép pháp y với những vụ án và cái chết bí ẩn. Tác giả: Lưu Hiểu Huy, Dịch giả: Bùi Thanh Thủy"
                }
            };
        }

        public Book GetBookById(int id)
        {
            return GetBookList().FirstOrDefault(b => b.Id == id);
        }

        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Diệp Hồng Vũ" },
            new SelectListItem { Value = "2", Text = "Motofumi Fukahori" },
            new SelectListItem { Value = "3", Text = "Franck Thilliez" },
            new SelectListItem { Value = "4", Text = "Trần Lộ" },
            new SelectListItem { Value = "5", Text = "Lưu Hiểu Huy" }
        };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Truyền tranh" },
            new SelectListItem { Value = "2", Text = "Văn học đương đại" },
            new SelectListItem { Value = "3", Text = "Phật học phổ thông" },
            new SelectListItem { Value = "4", Text = "Trinh thám" },
            new SelectListItem { Value = "5", Text = "Tâm lý học" },
            new SelectListItem { Value = "6", Text = "Pháp y" }
        };

        public string GetAuthorName(int authorId)
        {
            return Authors.FirstOrDefault(a => a.Value == authorId.ToString())?.Text;
        }

        public string GetGenreName(int genreId)
        {
            return Genres.FirstOrDefault(g => g.Value == genreId.ToString())?.Text;
        }
    }
}