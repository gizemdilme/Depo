using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Depo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Depo.Controllers
{
    public class StartpController : Controller
    {
        private readonly WarehouseContext _warehouseContext;

        public StartpController(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        // Anasayfa
        public IActionResult Index()
        {
            return View();
        }

        // Giriş Sayfası GET
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Giriş POST
        [HttpPost]
        public async Task<IActionResult> Login(Logincs logincs)
        {
            var user = await _warehouseContext.Kullanicis
                .SingleOrDefaultAsync(x => x.Mail == logincs.Mail);

            if (user != null && user.Sifre == logincs.Sifre)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Mail),
                    
                    new Claim("Role", "User")  // Rol ekleyebilirsin
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = logincs.LoggedStatus
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToAction("Index", "Home");
            }

            ViewData["OnayMesaji"] = "❌ Kullanıcı bulunamadı veya şifre yanlış!";
            return View();
        }

       

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Kullanici yeniKullanici)
        {
            if (string.IsNullOrEmpty(yeniKullanici.Mail) || string.IsNullOrEmpty(yeniKullanici.Sifre))
            {
                ViewData["Hata"] = "Lütfen tüm alanları doldurun.";
                return View();
            }

            bool emailVarMi = await _warehouseContext.Kullanicis.AnyAsync(x => x.Mail == yeniKullanici.Mail);
            if (emailVarMi)
            {
                ViewData["Hata"] = "Bu mail zaten kayıtlı!";
                return View();
            }

            _warehouseContext.Kullanicis.Add(yeniKullanici);
            await _warehouseContext.SaveChangesAsync();

            return RedirectToAction("Login");
        }

    }
}
