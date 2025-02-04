using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ModuloGestionCliente.Models.DB;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace ModuloGestionCliente.Controllers.Business
{
    public class LoginClienteController : Controller
    {
        private readonly ProyectContext _context;

        public LoginClienteController(ProyectContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View("~/Views/LoginCliente/Login.cshtml");
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Correo, string Contrasea)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/LoginCliente/Login.cshtml");
            }

            var user = _context.Clientes.FirstOrDefault(c => c.Correo == Correo && c.Contrasea == Contrasea);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View("~/Views/LoginCliente/Login.cshtml");
            }

            // Crear claims para la autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombres),
                new Claim(ClaimTypes.Email, user.Correo)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["LoginSuccess"] = true;
            return RedirectToAction("Index", "Transaccions");
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
