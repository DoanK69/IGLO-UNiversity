using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        private static UserRepository _instance = new UserRepository();
        public static UserRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var user = _context.Users.SingleOrDefault(m => m.Id == (int)id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<User> GetAll()
        {
            var _context = new IGLOUniversityContext();
            return _context.Users;
        }

        public User GetSingle(object id)
        {
            var _context = new IGLOUniversityContext();
            var user = new User();
            return user = _context.Users.SingleOrDefault(m => m.Id == (int)id);
        }

        public User GetSingleUsernamePassword(object id, object pass)
        {
            var _context = new IGLOUniversityContext();
            var user = new User();
            return user = _context.Users.FirstOrDefault(m => m.UserName == (string)id && m.Password == (string)pass);
        }

        public bool CekUsernamePassword(string username, string pass)
        {
            var _context = new IGLOUniversityContext();
            return _context.Users.Any(m => m.UserName == username && m.Password == pass);
        }

        public bool Insert(User model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User model)
        {
            var _context = new IGLOUniversityContext();
            try
            {
                var olduser = _context.Users.SingleOrDefault(m => m.Id == model.Id);
                MapingModel(olduser, model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetRole(string username)
        {
            using (var _context = new IGLOUniversityContext())
            {
                return _context.Users.SingleOrDefault(a => a.UserName == username).IsAdmin;
            }
        }

        public int? GetId(string username)
        {
            var _context = new IGLOUniversityContext();
            var id = _context.Users.SingleOrDefault(a => a.UserName == username).MahasiswaId;
            return id;
        }
    }
}
