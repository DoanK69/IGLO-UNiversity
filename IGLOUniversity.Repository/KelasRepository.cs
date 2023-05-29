using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class KelasRepository : BaseRepository, IRepository<Kela>
    {
        private static KelasRepository _instance = new KelasRepository();
        public static KelasRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var kelas = _context.Kelas.SingleOrDefault(m => m.Id == (int)id);
                _context.Kelas.Remove(kelas);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Kela> GetAll()
        {
            var _context = new IGLOUniversityContext();
            return _context.Kelas;
        }

        public Kela GetSingle(object id)
        {
            var _context = new IGLOUniversityContext();
            var kelas = new Kela();
            return kelas = _context.Kelas.SingleOrDefault(m => m.Id == (int)id);
        }

        public bool Insert(Kela model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                _context.Kelas.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Kela model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var oldKelas = _context.Kelas.SingleOrDefault(m => m.Id == model.Id);
                MapingModel(oldKelas, model);
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
