using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IGLOUniversity.Web.Controllers
{
    public class MahasiswaController : BaseController
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = MahasiswaProvider.GetIndex(page);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertMahasiswaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cek = MahasiswaProvider.PostSave(model);
                    if (cek)
                    {
                        return Json(new { success = true, message = "Data berhasil ditambah" });
                    }
                    return Json(new { success = false, message = "*Nim sudah ada" });
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
        public IActionResult Edit(int id)
        {
            var model = MahasiswaProvider.GetEdit(id);
            return Json(model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var deleteSuccess = MahasiswaProvider.Delete(id);
            if (deleteSuccess)
            {
                return Json(new { success = true, message = "Data berhasil dihapus" });
            }
            return Json(new { success = true, message = "Data gagal dihapus" });
        }
    }
}
