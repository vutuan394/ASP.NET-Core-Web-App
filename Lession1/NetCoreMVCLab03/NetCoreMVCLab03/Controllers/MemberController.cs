using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLab03.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreMVCLab03.Controllers
{
    public class MemberController : Controller
    {
        // Danh sách member tĩnh (thay cho database)
        public static readonly List<Member> members = new List<Member>
        {
            new Member { Username = "member1", Fullname = "Trịnh Văn Chung", Password = "123456", Email = "chungtv@mail.com" },
            new Member { Username = "member2", Fullname = "Nguyễn Thị Mai", Password = "123456", Email = "maint@mail.com" },
            new Member { Username = "member3", Fullname = "Lê Văn Hùng", Password = "123456", Email = "hunglv@mail.com" },
            new Member { Username = "member4", Fullname = "Phạm Thị Lan", Password = "123456", Email = "lanpt@mail.com" },
            new Member { Username = "member5", Fullname = "Hoàng Văn Nam", Password = "123456", Email = "namhv@mail.com" }
        };

        // GET: Hiển thị danh sách member
        public IActionResult Index()
        {
            return View(members);
        }

        // GET: Hiển thị form thêm mới member
        public IActionResult Create()
        {
            return View();
        }

        // POST: Xử lý thêm mới member (Binding Data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                // Tạo MemberId mới
                member.MemberId = Guid.NewGuid().ToString();

                // Thêm member vào danh sách
                members.Add(member);

                // Chuyển hướng về trang danh sách
                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu không hợp lệ, trả về view với model hiện tại
            return View(member);
        }

        // GET: Hiển thị chi tiết member
        public IActionResult Details(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: Hiển thị form chỉnh sửa
        public IActionResult Edit(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Xử lý chỉnh sửa member
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingMember = members.FirstOrDefault(m => m.MemberId == id);
                if (existingMember != null)
                {
                    existingMember.Username = member.Username;
                    existingMember.Fullname = member.Fullname;
                    existingMember.Email = member.Email;
                    // Lưu ý: Trong thực tế cần xử lý password phức tạp hơn
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Hiển thị form xóa
        public IActionResult Delete(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Xử lý xóa member
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member != null)
            {
                members.Remove(member);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}