using Basilisk.ViewModel;
using Basilisk.ViewModel.Login;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class UserProvider : BaseProvider
    {
        private static IEnumerable<GridUserViewModel> GetDataIndex()
        {
            var user = UserRepository.GetRepository().GetAll().ToList();
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();

            var model = (from usr in user
                         join mhs in mahasiswa on usr.MahasiswaId equals mhs.Id into g
                         from grup in g.DefaultIfEmpty()
                         select new GridUserViewModel
                         {
                             Id = usr.Id,
                             UserName = usr.UserName,
                             Status = usr.Status,
                             IsAdmin = usr.IsAdmin == true ? "Ya" : "Tidak",
                             Password = usr.Password,
                             Mahasiswa = usr.MahasiswaId == null ? "N/A" : $"{grup.NamaDepan} {(grup.NamaTengah == null ? "" : grup.NamaTengah)} {(grup.NamaBelakang == null ? "" : grup.NamaBelakang)}"
                         }).ToList();

            return model;
        }

        public static IndexUserViewModel GetIndex(int page, string search)
        {
            IEnumerable<GridUserViewModel> dataUser = GetDataIndex();
            if (!string.IsNullOrEmpty(search))
            {
                dataUser = dataUser.Where(a => a.UserName.Contains(search) || a.Status == search || a.IsAdmin.Contains(search) || a.Mahasiswa.Contains(search));
            }

            int totalData = dataUser.Count();
            int totalHalaman = GetHalaman(totalData);
            int skip = GetSkip(page);

            dataUser = dataUser.Skip(skip).Take(TotalDataPerPage);

            var model = new IndexUserViewModel
            {
                TotalData = totalData,
                TotalHalaman = totalHalaman,
                GridUser = dataUser
            };

            return model;
        }

        public static UpsertUserViewModel GetEdit(int id)
        {
            var model = new UpsertUserViewModel();
            try
            {
                var oldUser = UserRepository.GetRepository().GetSingle(id);
                MapingModel(model, oldUser);
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public static int? GetIdMahasiswa(string username)
        {
            var id = UserRepository.GetRepository().GetId(username);
            return id;
        }

        public static void PostSave(UpsertUserViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    User user = new User();
                    MapingModel(user, model);
                    UserRepository.GetRepository().Insert(user);
                }
                else
                {
                    User oldUser = UserRepository.GetRepository().GetSingle(model.Id);
                    MapingModel(oldUser, model);
                    UserRepository.GetRepository().Update(oldUser);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<DropDownListViewModel> GetStatus()
        {
            var result = new List<DropDownListViewModel>
            {
                new DropDownListViewModel { StringValue = "Aktif", Text = "Aktif" },
                new DropDownListViewModel { StringValue = "Tidak Aktif", Text = "Tidak Aktif" },
                new DropDownListViewModel { StringValue = "Terkunci", Text = "Terkunci" }
            };

            return result;
        }

        public static List<DropDownListViewModel> GetMahasiswa()
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var result = mahasiswa.Select(a => new DropDownListViewModel
            {
                IntValue = a.Id,
                Text = $"{a.NamaDepan} {(a.NamaTengah == null ? "" : a.NamaTengah)} {(a.NamaBelakang == null ? "" : a.NamaBelakang)}"
            }).ToList();

            return result;
        }

        public static bool IsAuthentication(LoginViewModel model)
        {
            var account = UserRepository.GetRepository().CekUsernamePassword(model.Username, model.Password);
            if (account)
            {
                return true;
            }
            return false;
        }

        public static string GetRoleName(string username)
        {
            if (UserRepository.GetRepository().GetRole(username))
            {
                return "Administrator";
            }
            return "Mahasiswa";
        }

        public static bool Delete(int id)
        {
            try
            {
                var cek = UserRepository.GetRepository().GetSingle(id);
                if (cek != null)
                {
                    UserRepository.GetRepository().Delete(id);
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
