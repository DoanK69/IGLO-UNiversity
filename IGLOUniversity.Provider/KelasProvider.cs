using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Matakuliah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class KelasProvider : BaseProvider
    {
        private static IEnumerable<GridKelasViewModel> GetDataIndex()
        {
            var kelas = KelasRepository.GetRepository().GetAll().ToList();

            var model = (from kls in kelas
                         join matkul in MatakuliahRepository.GetRepository().GetAll().ToList() on kls.IdMatakuliah equals matkul.Id
                         select new GridKelasViewModel
                         {
                             Id = kls.Id,
                             Nama = kls.Nama,
                             Matakuliah = matkul.Nama,
                             Sks = matkul.Sks,
                             Kapasitas = kls.Kapasitas,
                             Semester = kls.Semester
                         }).ToList();

            return model;
        }

        public static IndexKelasViewModel GetIndex(int page)
        {
            IEnumerable<GridKelasViewModel> dataKelas = GetDataIndex();

            int totalData = dataKelas.Count();
            int totalHalaman = GetHalaman(totalData);
            int skip = GetSkip(page);

            dataKelas = dataKelas.Skip(skip).Take(TotalDataPerPage);
            
            var model = new IndexKelasViewModel
            {
                TotalData = totalData,
                TotalHalaman = totalHalaman,
                GridKelas = dataKelas
            };

            return model;
        }

        public static UpsertKelasViewModel GetEdit(int id)
        {
            var model = new UpsertKelasViewModel();
            try
            {
                var oldKelas = KelasRepository.GetRepository().GetSingle(id);
                MapingModel(model, oldKelas);
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public static bool PostSave(UpsertKelasViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var cek = KelasRepository.GetRepository().GetAll().Any(c => c.Nama == model.Nama);
                    if (!cek)
                    {
                        Kela kelas = new Kela();
                        MapingModel(kelas, model);
                        KelasRepository.GetRepository().Insert(kelas);
                        return true;
                    }
                    return false;
                    
                }
                else
                {
                    var oldKelas = KelasRepository.GetRepository().GetSingle(model.Id);
                    MapingModel(oldKelas, model);
                    KelasRepository.GetRepository().Update(oldKelas);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<DropDownListViewModel> GetMatkul()
        {
            var matkul = MatakuliahRepository.GetRepository().GetAll().ToList();
            var result = matkul.Select(a => new DropDownListViewModel
            {
                IntValue = a.Id,
                Text = $"{a.Nama}"
            }).ToList();

            return result;
        }


        public static bool Delete(int id)
        {
            try
            {
                var checkDistribusi = DistribusiMatakuliahRepository.GetRepository().GetAll().Any(a => a.IdKelas == id);
                if (!checkDistribusi)
                {
                    KelasRepository.GetRepository().Delete(id);
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
