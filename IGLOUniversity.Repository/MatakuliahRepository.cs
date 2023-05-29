using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class MatakuliahRepository : BaseRepository, IRepository<Matakuliah>
    {
        private static MatakuliahRepository _instance = new MatakuliahRepository();
        public static MatakuliahRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var matakuliah = _context.Matakuliahs.SingleOrDefault(m => m.Id == (int)id);
                _context.Matakuliahs.Remove(matakuliah);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Matakuliah> GetAll()
        {
            var _context = new IGLOUniversityContext();
            return _context.Matakuliahs;
        }

        public Matakuliah GetSingle(object id)
        {
            var _context = new IGLOUniversityContext();
            var matakuliah = new Matakuliah();
            return matakuliah = _context.Matakuliahs.SingleOrDefault(m => m.Id == (int)id);
        }

        public bool Insert(Matakuliah model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                _context.Matakuliahs.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Matakuliah model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var oldMatakuliah = _context.Matakuliahs.SingleOrDefault(m => m.Id == model.Id);
                MapingModel(oldMatakuliah, model);
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
