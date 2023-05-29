using IGLOUniversity.Provider;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Basilisk.ViewModel.Login;
using Microsoft.AspNetCore.Authorization;

namespace IGLOUniversity.Web.Controllers
{

    public class UserController : BaseController
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int page, string search = "")
        {
            SetUsernameRole(User.Claims);
            var model = UserProvider.GetIndex(page, search);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserProvider.PostSave(model);
                    return Json(new { success = true, message = "Data berhasil ditambah" });
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Data gagal ditambah" });
                }
            }
            var validationMessage = GetValidationMessage(ModelState);
            return Json(new { success = false, message = "Data gagal ditambah", validations = validationMessage });

        }

        public IActionResult GetDropDownStatus()
        {
            var model = UserProvider.GetStatus();
            return Json(model);
        }

        public IActionResult GetDropDownMahasiswa()
        {
            var model = UserProvider.GetMahasiswa();
            return Json(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = UserProvider.GetEdit(id);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserProvider.IsAuthentication(model))
                {
                    var claims = new List<Claim>
                    {
                    new Claim (ClaimTypes.NameIdentifier, model.Username),
                    new Claim (ClaimTypes.Role, UserProvider.GetRoleName(model.Username))
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal);
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("LoginFailed");
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult LoginFailed()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var deleteSuccess = UserProvider.Delete(id);
            if (deleteSuccess)
            {
                return Json(new { success = true, message = "Data berhasil dihapus" });
            }
            return Json(new { success = true, message = "Data gagal dihapus" });
        }
    }
}
