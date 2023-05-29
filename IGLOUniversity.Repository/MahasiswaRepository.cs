using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class MahasiswaRepository : BaseRepository, IRepository<Mahasiswa>
    {
        private static MahasiswaRepository _instance = new MahasiswaRepository();
        public static MahasiswaRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var mahasiswa = _context.Mahasiswas.SingleOrDefault(m => m.Id == (int)id);
                _context.Mahasiswas.Remove(mahasiswa);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Mahasiswa> GetAll()
        {
            var _context = new IGLOUniversityContext();
            return _context.Mahasiswas;
        }

        public Mahasiswa GetSingle(object id)
        {
            var _context = new IGLOUniversityContext();
            var mahasiswa = new Mahasiswa();
            return mahasiswa = _context.Mahasiswas.SingleOrDefault(m => m.Id == (int)id);
        }

        public bool Insert(Mahasiswa model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                _context.Mahasiswas.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Mahasiswa model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var oldMahasiswa = _context.Mahasiswas.SingleOrDefault(m => m.Id == model.Id);
                MapingModel(oldMahasiswa, model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
