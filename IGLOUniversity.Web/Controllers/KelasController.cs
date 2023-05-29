using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Kelas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IGLOUniversity.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KelasController : BaseController
    {
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = KelasProvider.GetIndex(page);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertKelasViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cek = KelasProvider.PostSave(model);
                    if (cek)
                    {
                        return Json(new { success = true, message = "Data berhasil ditambah" });
                    }
                    return Json(new { success = false, message = "*Nama kelas sudah ada" });
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Data gagal ditambah" });
                }
            }
            var validationMessage = GetValidationMessage(ModelState);
            return Json(new { success = false, message = "Data gagal ditambah", validations = validationMessage });
        }

        [HttpGet]
        public IActionResult GetDropDownMatkul() 
        {
            var model = KelasProvider.GetMatkul();
            return Json(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = KelasProvider.GetEdit(id);
            return Json(model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var deleteSuccess = KelasProvider.Delete(id);
            if (deleteSuccess)
            {
                return Json(new { success = true, message = "Data berhasil dihapus" });
            }
            return Json(new { success = true, message = "Data gagal dihapus" });
        }
    }
}
