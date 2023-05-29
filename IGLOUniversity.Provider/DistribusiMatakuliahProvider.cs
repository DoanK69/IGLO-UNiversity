using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.DistribusiMatakuliah;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class DistribusiMatakuliahProvider : BaseProvider
    {
        private static IEnumerable<GridDistribusiMatakuliahViewModel> GetDataIndex()
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var matakuliah = MatakuliahRepository.GetRepository().GetAll().ToList();
            var kelas = KelasRepository.GetRepository().GetAll().ToList();
            var distribusiMatakuliah = DistribusiMatakuliahRepository.GetRepository().GetAll().ToList();

            var model = (from mhs in mahasiswa
                         join d in distribusiMatakuliah on mhs.Id equals d.IdMahasiswa into a
                         from d in a.DefaultIfEmpty()
                         join k in kelas on d?.IdKelas equals k.Id into b
                         from k in b.DefaultIfEmpty()
                         join m in matakuliah on k?.IdMatakuliah equals m.Id into c
                         from m in c.DefaultIfEmpty()
                         group m?.Sks by mhs into grp
                         select new GridDistribusiMatakuliahViewModel
                         {
                             Id = grp.Key.Id,
                             Nim = grp.Key.Nim,
                             Nama = $"{grp.Key.NamaDepan} {(grp.Key.NamaTengah == null ? "" : grp.Key.NamaTengah)} {(grp.Key.NamaBelakang == null ? "" : grp.Key.NamaBelakang)}",
                             AsalSma = grp.Key.AsalSma,
                             NomorHp = grp.Key.NomorHp,
                             Alamat = grp.Key.Alamat == "" ? "N/A" : grp.Key.Alamat,
                             TotalSks = grp.Sum() == null ? 0 : grp.Sum()
                         }).ToList();

            return model;
        }

        public static IndexDistribusiMatakuliahViewModel GetIndex(int page)
        {
            IEnumerable<GridDistribusiMatakuliahViewModel> dataDistribusi = GetDataIndex();

            int totalData = dataDistribusi.Count();
            int totalHalaman = GetHalaman(totalData);
            int skip = GetSkip(page);

            dataDistribusi = dataDistribusi.Skip(skip).Take(TotalDataPerPage);

            var model = new IndexDistribusiMatakuliahViewModel
            {
                TotalData = totalData,
                TotalHalaman = totalHalaman,
                GridDistribusi = dataDistribusi
            };

            return model;
        }

        public static DetailDistribusiViewModel GetDetail(int id)
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var matakuliah = MatakuliahRepository.GetRepository().GetAll().ToList();
            var kelas = KelasRepository.GetRepository().GetAll().ToList();
            var distribusiMatakuliah = DistribusiMatakuliahRepository.GetRepository().GetAll().ToList();

            var model = new DetailDistribusiViewModel
            {
                Mahasiswa = (from mhs in mahasiswa
                             where mhs.Id == id
                             select new GridMahasiswaViewModel
                             {
                                 Id = mhs.Id,
                                 Nim = mhs.Nim,
                                 NamaDepan = mhs.NamaDepan,
                                 NamaTengah = mhs.NamaTengah,
                                 NamaBelakang = mhs.NamaBelakang,
                                 AsalSma = mhs.AsalSma
                             }).SingleOrDefault(),

                GridKelas = (from mhs in mahasiswa
                             join dis in distribusiMatakuliah on mhs.Id equals dis.IdMahasiswa
                             join kls in kelas on dis.IdKelas equals kls.Id
                             join matkul in matakuliah on kls.IdMatakuliah equals matkul.Id
                             where mhs.Id == id
                             select new GridKelasViewModel
                             {
                                 Id = dis.Id,
                                 Nama = kls.Nama,
                                 Matakuliah = matkul.Nama,
                                 Sks = matkul.Sks,
                                 Semester = kls.Semester,
                                 Kapasitas = kls.Kapasitas,
                                 Status = dis.Status
                             }).ToList()
            };

            return model;
        }

        public static DetailDistribusiViewModel GetDetailMahasiswa(int id)
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var matakuliah = MatakuliahRepository.GetRepository().GetAll().ToList();
            var kelas = KelasRepository.GetRepository().GetAll().ToList();
            var distribusiMatakuliah = DistribusiMatakuliahRepository.GetRepository().GetAll().ToList();

            var model = new DetailDistribusiViewModel
            {
                Mahasiswa = (from mhs in mahasiswa
                             join d in distribusiMatakuliah on mhs.Id equals d.IdMahasiswa into a
                             from d in a.DefaultIfEmpty()
                             join k in kelas on d?.IdKelas equals k.Id into b
                             from k in b.DefaultIfEmpty()
                             join m in matakuliah on k?.IdMatakuliah equals m.Id into c
                             from m in c.DefaultIfEmpty()
                             group m?.Sks by mhs into grp
                             where grp.Key.Id == id
                             select new GridMahasiswaViewModel
                             {
                                 Id = grp.Key.Id,
                                 Nim = grp.Key.Nim,
                                 NamaLengkap = MahasiswaProvider.GetFullName(grp.Key.NamaDepan, grp.Key.NamaTengah, grp.Key.NamaBelakang),                               
                                 AsalSma = grp.Key.AsalSma,
                                 NomorHp = grp.Key.NomorHp,
                                 Alamat = grp.Key.Alamat,
                                 TotalSks = grp.Sum() == null ? 0 : grp.Sum()
                             }).SingleOrDefault(),

                GridKelas = (from mhs in mahasiswa
                             join dis in distribusiMatakuliah on mhs.Id equals dis.IdMahasiswa
                             join kls in kelas on dis.IdKelas equals kls.Id
                             join matkul in matakuliah on kls.IdMatakuliah equals matkul.Id
                             where mhs.Id == id
                             select new GridKelasViewModel
                             {
                                 Id = dis.Id,
                                 Nama = kls.Nama,
                                 Matakuliah = matkul.Nama,
                                 Sks = matkul.Sks,
                                 Semester = kls.Semester,
                                 Kapasitas = kls.Kapasitas,
                                 Status = dis.Status
                             }).ToList()
            };

            return model;
        }

        public static List<DropDownListViewModel> GetKelas()
        {
            var kelas = KelasRepository.GetRepository().GetAll().ToList();
            var result = kelas.Select(a => new DropDownListViewModel
            {
                IntValue = a.Id,
                LongValue = a.Kapasitas,
                Text = a.Nama
            }).ToList();

            return result;
        }

        public static bool PostSave(InsertDistribusiMatakuliahViewModel model)
        {
            try
            {
                var cek = DistribusiMatakuliahRepository.GetRepository().GetAll().Any(d => d.IdMahasiswa == model.IdMahasiswa && d.IdKelas == model.IdKelas);
                if (!cek)
                {
                    DistribusiMatakuliah distribusiMatakuliah = new DistribusiMatakuliah();
                    MapingModel(distribusiMatakuliah, model);
                    DistribusiMatakuliahRepository.GetRepository().Insert(distribusiMatakuliah);
                    return true;
                }
                return false;
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {                
                DistribusiMatakuliahRepository.GetRepository().Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
