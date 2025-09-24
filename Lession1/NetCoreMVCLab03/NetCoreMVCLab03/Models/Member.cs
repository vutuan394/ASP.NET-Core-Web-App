using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreMVCLab03.Models
{
    public class Member
    {
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Username là bắt buộc")]
        [StringLength(50, ErrorMessage = "Username không quá 50 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ và tên không quá 100 ký tự")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Password là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public Member()
        {
            MemberId = Guid.NewGuid().ToString();
        }
    }
}