using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Security.Claims;
using SysVotaciones.EN;
using Microsoft.AspNetCore.Authentication;
using SysVotaciones.BLL;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.Identity.Client;

namespace Sys.Votaciones.Views.Controllers
{
    public class StudentController : Controller
    {
        //private readonly HttpClient _httpClientCRMAPI;

        //public StudentController(IHttpClientFactory httpClientFactory)
        //{
        //    httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        //    _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");

        //}

        [HttpPost]
        public async Task<IActionResult> Login(Student student)
        {
            try
            {
                var user = await StudentBLL.Login2(student);

                if (user == null)
                {
                    ViewBag.Error = "credenciales invalidas";
                    return View(new Student()
                    {
                        StudentCode = student.StudentCode,
                        Password = student.Password,
                    });
                }

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, user.StudentCode),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");

            }
            catch
            {
                ViewBag.Error = "ha ocurrido un error inesperado";
                return View(new Student()
                {
                    StudentCode = student.StudentCode, 
                    Password = student.Password,
                });
            }

        
            
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
