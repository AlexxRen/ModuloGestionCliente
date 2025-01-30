using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModuloGestionCliente.Controllers.Busness
{
    public class LoginController : Controller
    {
       //static string cadena = "Data Souurce=CASAALEX;Database=Proyect;Trusted_Connection=True;TrustServerCertificate=True" ;
        //Get
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout() {

            return View();
        }

        //[HttpPost]



    }
}
