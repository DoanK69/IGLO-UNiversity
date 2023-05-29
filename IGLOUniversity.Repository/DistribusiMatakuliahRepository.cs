using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class DistribusiMatakuliahRepository : BaseRepository, IRepository<DistribusiMatakuliah>
    {
        private static DistribusiMatakuliahRepository _instance = new DistribusiMatakuliahRepository();
        public static DistribusiMatakuliahRepository GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var distribusiMatakuliah = _context.DistribusiMatakuliahs.SingleOrDefault(m => m.Id == (int)id);
                _context.DistribusiMatakuliahs.Remove(distribusiMatakuliah);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<DistribusiMatakuliah> GetAll()
        {
            var _context = new IGLOUniversityContext();
            return _context.DistribusiMatakuliahs;
        }

        public DistribusiMatakuliah GetSingle(object id)
        {
            var _context = new IGLOUniversityContext();
            var distribusiMatakuliah = new DistribusiMatakuliah();
            return distribusiMatakuliah = _context.DistribusiMatakuliahs.SingleOrDefault(m => m.Id == (int)id);
        }

        public bool Insert(DistribusiMatakuliah model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                _context.DistribusiMatakuliahs.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(DistribusiMatakuliah model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var oldDistribusiMatakuliah = _context.DistribusiMatakuliahs.SingleOrDefault(m => m.Id == model.Id);
                MapingModel(oldDistribusiMatakuliah, model);
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
