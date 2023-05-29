using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Matakuliah;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MatakuliahController : BaseController
    {
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = MatakuliahProvider.GetIndex(page);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertMatakuliahViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cek = MatakuliahProvider.PostSave(model);
                    if (cek)
                    {
                        return Json(new { success = true, message = "Data berhasil ditambah" });
                    }
                    return Json(new { success = false, message = "*Nama matakuliah sudah ada" });
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
            var model = MatakuliahProvider.GetEdit(id);
            return Json(model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var deleteSuccess = MatakuliahProvider.Delete(id);
            if (deleteSuccess)
            {
                return Json(new { success = true, message = "Data berhasil dihapus" });
            }
            return Json(new { success = true, message = "Data gagal dihapus" });
        }
    }
}
