using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MahasiswaController : ControllerBase
    {
        [HttpGet("{page}")]
        public IndexMahasiswaViewModel Get(int page)
        {
            var result = MahasiswaProvider.GetIndex(page);
            return result;
        }

        [HttpGet("Edit/{id}")]
        public UpsertMahasiswaViewModel GetEdit(int id)
        {
            var result = MahasiswaProvider.GetEdit(id);
            if (result != null)
                return result;
            else
                return new UpsertMahasiswaViewModel();
        }

        [HttpPost]
        public string Insert([FromBody] UpsertMahasiswaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MahasiswaProvider.PostSave(model);
                    return "Success";
                }
                catch (Exception)
                {
                    return "Fail Insert Data";
                    throw;
                }
            }
            return "Fail Insert Data";
        }

        [HttpPut]
        public string Edit([FromBody] UpsertMahasiswaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MahasiswaProvider.PostSave(model);
                    return "Success";
                }
                catch (Exception ex)
                {
                    return "Fail Edit Data";
                    throw;
                }
            }
            return "Fail Edit Data";
        }

        [HttpDelete("Delete/{id}")]
        public string Delete(int id)
        {
            var result = MahasiswaProvider.Delete(id);
            if (result)
            {
                return $"Fail Delete";
            }
            else
            {
                return $"Success";
            }
        }
    }
}
