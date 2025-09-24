using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Web_QLNT.Models;
using Web_QLNT.Functions;

namespace Web_QLNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly ImageHelper _imageHelper;

        public HomeController(AppDbContext db, IWebHostEnvironment env, ImageHelper imageHelper)
        {
            _db = db;
            _env = env;
            _imageHelper = imageHelper;
        }

        #region Trang Ch?
        public IActionResult Index(int id = 1)
        {
            try
            {
                ViewBag.NCC = _db.NhaCungCaps.ToList();
                ViewBag.Page = id;
                ViewBag.Count = _db.SanPhams.Count();

                var cart = GetCart();
                SaveCart(cart);

                // Phân trang - m?i trang 8 s?n ph?m
                var sanPhams = _db.SanPhams
                    .Include(sp => sp.Loai)
                    .Include(sp => sp.NhaCungCap)
                    .Where(sp => sp.TinhTrang == true) // Ch? hi?n th? s?n ph?m ðang ho?t ð?ng
                    .Skip(8 * (id - 1))
                    .Take(8)
                    .ToList();

                return View(sanPhams);
            }
            catch (Exception ex)
            {
                // X? l? l?i, có th? log l?i
                return View(new List<SanPham>());
            }
        }
        #endregion

        #region Qu?n L? S?n Ph?m
        public IActionResult GetSP(int id)
        {
            try
            {
                ViewBag.NCC = _db.NhaCungCaps.ToList();
                ViewBag.Page = 1;
                ViewBag.LoaiId = id;
                ViewBag.Count = _db.SanPhams.Count(sp => sp.MaLoai == id && sp.TinhTrang == true);

                var sanPhams = _db.SanPhams
                    .Include(sp => sp.Loai)
                    .Include(sp => sp.NhaCungCap)
                    .Where(sp => sp.MaLoai == id && sp.TinhTrang == true)
                    .ToList();

                return View("Index", sanPhams);
            }
            catch (Exception ex)
            {
                return View("Index", new List<SanPham>());
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.NCC = _db.NhaCungCaps.ToList();
                ViewBag.Loai = _db.Loais.ToList();

                var sanPham = _db.SanPhams
                    .Include(sp => sp.Loai)
                    .Include(sp => sp.NhaCungCap)
                    .FirstOrDefault(sp => sp.MaSP == id && sp.TinhTrang == true);

                if (sanPham == null)
                {
                    return NotFound();
                }

                return View(sanPham);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Qu?n L? Bán Hàng
        [HttpPost]
        public IActionResult AddToCart(string id, int quantity = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || quantity <= 0)
                {
                    TempData["ErrorMessage"] = "Thông tin s?n ph?m không h?p l?";
                    return RedirectToAction("Index");
                }

                var product = _db.SanPhams
                    .FirstOrDefault(sp => sp.MaSP == id && sp.TinhTrang == true && sp.SoLuongTon > 0);

                if (product == null)
                {
                    TempData["ErrorMessage"] = "S?n ph?m không t?n t?i ho?c ð? h?t hàng";
                    return RedirectToAction("Index");
                }

                if (quantity > product.SoLuongTon)
                {
                    TempData["ErrorMessage"] = $"S? lý?ng t?n không ð?. Ch? c?n {product.SoLuongTon} s?n ph?m";
                    return RedirectToAction("Index");
                }

                var cart = GetCart();
                cart.AddToCart(product, quantity);
                SaveCart(cart);

                TempData["SuccessMessage"] = "Ð? thêm s?n ph?m vào gi? hàng";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có l?i x?y ra khi thêm vào gi? hàng";
            }

            return RedirectToAction("Index");
        }

        public IActionResult ViewCart()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    TempData["ErrorMessage"] = "M? s?n ph?m không h?p l?";
                    return RedirectToAction("ViewCart");
                }

                var cart = GetCart();
                cart.RemoveFromCart(productId);
                SaveCart(cart);

                TempData["SuccessMessage"] = "Ð? xóa s?n ph?m kh?i gi? hàng";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có l?i x?y ra khi xóa s?n ph?m";
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            try
            {
                var cart = new GioHang();
                SaveCart(cart);
                TempData["SuccessMessage"] = "Ð? xóa toàn b? gi? hàng";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có l?i x?y ra khi xóa gi? hàng";
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult UpdateCartItem(string productId, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(productId) || quantity <= 0)
                {
                    return Json(new { success = false, message = "Thông tin không h?p l?" });
                }

                var product = _db.SanPhams.FirstOrDefault(sp => sp.MaSP == productId);
                if (product == null || quantity > product.SoLuongTon)
                {
                    return Json(new { success = false, message = "S? lý?ng không h?p l?" });
                }

                var cart = GetCart();
                var item = cart.Items.FirstOrDefault(i => i.SanPham.MaSP == productId);

                if (item != null)
                {
                    item.Quantity = quantity;
                    SaveCart(cart);
                }

                return Json(new
                {
                    success = true,
                    totalPrice = cart.GetTotalPrice(),
                    itemPrice = item.SanPham.DonGia * item.Quantity
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có l?i x?y ra" });
            }
        }
        #endregion

        #region Ðãng Nh?p
        public IActionResult Login()
        {
            // N?u ð? ðãng nh?p, chuy?n hý?ng v? trang ch?
            if (HttpContext.Session.GetString("KhachHang") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ViewBag.ErrorMessage = "Vui l?ng nh?p ð?y ð? thông tin";
                    return View();
                }

                var kh = await _db.KhachHangs
                    .FirstOrDefaultAsync(t => t.MaKH == username && t.MatKhau == password && t.TinhTrang == "Active");

                if (kh != null)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = false
                    };

                    HttpContext.Session.SetString("KhachHang", JsonSerializer.Serialize(kh, options));
                    TempData["SuccessMessage"] = $"Chào m?ng {kh.HoTen} ð? tr? l?i!";
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = "Tên ðãng nh?p ho?c m?t kh?u không ðúng";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có l?i x?y ra khi ðãng nh?p";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KhachHang");
            TempData["SuccessMessage"] = "Ð? ðãng xu?t thành công";
            return RedirectToAction("Index");
        }
        #endregion

        #region Ðãng K?
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IFormCollection f, IFormFile fUpLoad)
        {
            try
            {
                // Validate m?t kh?u
                if (f["MatKhau"] != f["ReMatKhau"])
                {
                    ViewBag.ErrorMessage = "M?t kh?u nh?p không trùng kh?p";
                    return View();
                }

                // Ki?m tra username ð? t?n t?i
                var existingUser = await _db.KhachHangs.FirstOrDefaultAsync(k => k.MaKH == f["MaKH"]);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên ðãng nh?p ð? ðý?c s? d?ng";
                    return View();
                }

                // Parse ngày sinh
                if (!DateTime.TryParseExact(f["NgaySinh"], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    ViewBag.ErrorMessage = "Ngày sinh không h?p l?";
                    return View();
                }

                // T?o ð?a ch?
                var diaChi = GetAddress.GetFullAddress(f["SoNha"], f["Duong"], f["Phuong"], f["Quan"], f["ThanhPho"]);

                var kh = new KhachHang
                {
                    MaKH = f["MaKH"],
                    HoTen = f["HoTen"],
                    Email = f["Email"],
                    NgaySinh = ngaySinh,
                    DienThoai = f["DienThoai"],
                    DiaChi = diaChi,
                    MatKhau = f["MatKhau"],
                    MaNhom = "Gue", // Guest
                    TinhTrang = "Active"
                };

                // X? l? upload ?nh
                if (fUpLoad != null && fUpLoad.Length > 0)
                {
                    if (!ImageHelper.IsValidImage(fUpLoad))
                    {
                        ViewBag.ErrorMessage = "File ?nh không h?p l?. Ch? ch?p nh?n file JPG, PNG, GIF";
                        return View();
                    }

                    var fileName = await _imageHelper.SaveImageAsync(fUpLoad, kh.MaKH);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        kh.HinhAnh = fileName;
                    }
                }

                // Lýu vào database
                _db.KhachHangs.Add(kh);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ðãng k? thành công! Vui l?ng ðãng nh?p";
                return RedirectToAction("Login");
            }
            catch (DbUpdateException dbEx)
            {
                ViewBag.ErrorMessage = "L?i khi lýu d? li?u vào database";
                // Có th? log chi ti?t dbEx ? ðây
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có l?i x?y ra khi ðãng k?";
                // Có th? log chi ti?t ex ? ðây
            }

            return View();
        }
        #endregion

        #region Helper Methods
        private GioHang GetCart()
        {
            try
            {
                var cartJson = HttpContext.Session.GetString("Cart");
                if (!string.IsNullOrEmpty(cartJson))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<GioHang>(cartJson, options);
                }
            }
            catch (Exception ex)
            {
                // Log l?i deserialize
            }

            return new GioHang();
        }

        private void SaveCart(GioHang cart)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = false
                };
                var cartJson = JsonSerializer.Serialize(cart, options);
                HttpContext.Session.SetString("Cart", cartJson);
            }
            catch (Exception ex)
            {
                // Log l?i serialize
            }
        }

        // L?y thông tin khách hàng t? session
        private KhachHang GetCurrentCustomer()
        {
            var khachHangJson = HttpContext.Session.GetString("KhachHang");
            if (!string.IsNullOrEmpty(khachHangJson))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<KhachHang>(khachHangJson, options);
            }
            return null;
        }
        #endregion
    }
}