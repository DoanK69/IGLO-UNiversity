using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class MahasiswaProvider : BaseProvider
    {
        private static IEnumerable<GridMahasiswaViewModel> GetDataIndex()
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();

            var model = (from mhs in mahasiswa
                         select new GridMahasiswaViewModel
                         {
                             Id = mhs.Id,
                             Nim = mhs.Nim,
                             NamaDepan = mhs.NamaDepan,
                             NamaTengah = mhs.NamaTengah == null ? "" : mhs.NamaTengah,
                             NamaBelakang = mhs.NamaBelakang == null ? "" : mhs.NamaBelakang,
                             AsalSma = mhs.AsalSma,
                             NomorHp = mhs.NomorHp == "" ? "N/A" : mhs.NomorHp,
                             Alamat = mhs.Alamat == "" ? "N/A" : mhs.Alamat
                         }).ToList();

            return model;
        }

        public static IndexMahasiswaViewModel GetIndex(int page) 
        {
            IEnumerable<GridMahasiswaViewModel> dataMahasiswa = GetDataIndex();

            int totalData = dataMahasiswa.Count();
            int totalHalaman = GetHalaman(totalData);
            int skip = GetSkip(page);

            dataMahasiswa = dataMahasiswa.Skip(skip).Take(TotalDataPerPage);

            var model = new IndexMahasiswaViewModel
            {
                TotalData = totalData,
                TotalHalaman = totalHalaman,
                GridMahasiswa = dataMahasiswa
            };

            return model;
        }

        public static UpsertMahasiswaViewModel GetEdit(int id)
        {
            var model = new UpsertMahasiswaViewModel();
            try
            {
                var oldMahasiswa = MahasiswaRepository.GetRepository().GetSingle(id);
                MapingModel(model, oldMahasiswa);
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public static bool PostSave(UpsertMahasiswaViewModel model)
        {
            try
            {
                if(model.Id == 0)
                {
                    var cek = MahasiswaRepository.GetRepository().GetAll().Any(m => m.Nim == model.Nim);
                    if (!cek)
                    {
                        Mahasiswa mahasiswa = new Mahasiswa();
                        MapingModel(mahasiswa, model);
                        MahasiswaRepository.GetRepository().Insert(mahasiswa);
                        return true;
                    }
                    return false;
                }
                else
                {
                    Mahasiswa oldMahasiswa = MahasiswaRepository.GetRepository().GetSingle(model.Id);
                    MapingModel(oldMahasiswa, model);
                    MahasiswaRepository.GetRepository().Update(oldMahasiswa);
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
                var checkDistribusi = DistribusiMatakuliahRepository.GetRepository().GetAll().Any(a => a.IdMahasiswa == id);
                var checkUser = UserRepository.GetRepository().GetAll().Any(a => a.MahasiswaId == id);
                if (checkDistribusi == false && checkUser == false)
                {
                    MahasiswaRepository.GetRepository().Delete(id);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetFullName(string namaDepan, string namaTengah, string namaBelakang)
        {
            return $"{namaDepan} {(namaTengah == null ? "" : namaTengah)} {(namaBelakang == null ? "" : namaBelakang)}";
        }
    }
}
