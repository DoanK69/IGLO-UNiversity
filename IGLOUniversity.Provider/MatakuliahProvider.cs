using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.Matakuliah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class MatakuliahProvider : BaseProvider
    {
        private static IEnumerable<GridMatakuliahViewModel> GetDataIndex()
        {
            var mataKuliah = MatakuliahRepository.GetRepository().GetAll().ToList();

            var model = (from matkul in mataKuliah
                         select new GridMatakuliahViewModel
                         {
                             Id = matkul.Id,
                             Nama = matkul.Nama,
                             Sks = matkul.Sks,
                             Deskripsi = (string.IsNullOrEmpty(matkul.Deskripsi) ? "N/A" : matkul.Deskripsi)
                         }).ToList();

            return model;
        }

        public static IndexMatakuliahViewModel GetIndex(int page)
        {
            IEnumerable<GridMatakuliahViewModel> dataMatkul = GetDataIndex();

            int totalData = dataMatkul.Count();
            int totalHalaman = GetHalaman(totalData);
            int skip = GetSkip(page);

            dataMatkul = dataMatkul.Skip(skip).Take(TotalDataPerPage);

            var model = new IndexMatakuliahViewModel
            {
                TotalData = totalData,
                TotalHalaman = totalHalaman,
                GridMatakuliah = dataMatkul
            };

            return model;
        }

        public static UpsertMatakuliahViewModel GetEdit(int id)
        {
            var model = new UpsertMatakuliahViewModel();
            try
            {
                var oldMatkul = MatakuliahRepository.GetRepository().GetSingle(id);
                MapingModel(model, oldMatkul);
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public static bool PostSave(UpsertMatakuliahViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var cek = MatakuliahRepository.GetRepository().GetAll().Any(c => c.Nama == model.Nama);
                    if (!cek)
                    {
                        Matakuliah matkul = new Matakuliah();
                        MapingModel(matkul, model);
                        MatakuliahRepository.GetRepository().Insert(matkul);
                        return true;
                    }
                    return false;
                }
                else
                {
                    var oldMatkul = MatakuliahRepository.GetRepository().GetSingle(model.Id);
                    MapingModel(oldMatkul, model);
                    MatakuliahRepository.GetRepository().Update(oldMatkul);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                var checkKelas = KelasRepository.GetRepository().GetAll().Any(a => a.IdMatakuliah == id);
                if (!checkKelas)
                {
                    MatakuliahRepository.GetRepository().Delete(id);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
