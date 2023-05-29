using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.DistribusiMatakuliah;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Web.Controllers
{
    public class DistribusiMatakuliahController : BaseController
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = DistribusiMatakuliahProvider.GetIndex(page);
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            SetUsernameRole(User.Claims);
            var model = new DetailDistribusiViewModel();
            var username = GetUsername(User.Claims);
            var cekMahasiswa = UserProvider.GetIdMahasiswa(username);
            if (cekMahasiswa != null)
            {
                id = Convert.ToInt32(cekMahasiswa);
                model = DistribusiMatakuliahProvider.GetDetailMahasiswa(id);
                return View("DetailMahasiswa", model);
            }
            else
            {
                model = DistribusiMatakuliahProvider.GetDetail(id);
                return View(model);
            }        
        }

        [HttpGet]
        public IActionResult GetDropDownKelas()
        {
            var model = DistribusiMatakuliahProvider.GetKelas();
            return Json(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] InsertDistribusiMatakuliahViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cek = DistribusiMatakuliahProvider.PostSave(model);
                    if (cek)
                    {
                        return Json(new { success = true, message = "Data berhasil ditambah" });
                    }
                    return Json(new { success = false, message = "Kamu sudah mengambil kelas ini" });
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Data gagal ditambah" });
                }
            }
            var validationMessage = GetValidationMessage(ModelState);
            return Json(new { success = false, message = "Data gagal ditambah", validations = validationMessage });
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var cek = DistribusiMatakuliahProvider.Delete(id);
            if (cek)
            {
                return Json(new { success = true, message = "Data berhasil dihapus" });
            }
            return Json(new { success = false, message = "Data gagal dihapus" });
        }
    }
}
