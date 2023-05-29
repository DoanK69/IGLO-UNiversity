using Basilisk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Utility
{
    public class GlobalConfiguration
    {
        public static IEnumerable<MenuViewModel> Menus()
        {
            var data = new List<MenuViewModel>
            {
                new MenuViewModel { Role = "Administrator", Controller = "User", Action ="Index", Title = "User" },
                new MenuViewModel { Role = "Administrator", Controller = "Mahasiswa", Action ="Index", Title = "Mahasiswa" },
                new MenuViewModel { Role = "Administrator", Controller = "Matakuliah", Action ="Index", Title = "Matakuliah" },
                new MenuViewModel { Role = "Administrator", Controller = "Kelas", Action ="Index", Title = "Kelas" },
                new MenuViewModel { Role = "Administrator", Controller = "DistribusiMatakuliah", Action ="Index", Title = "Atur Rencana Studi" },

                //Mahasiswa
                new MenuViewModel { Role = "Mahasiswa", Controller = "DistribusiMatakuliah", Action ="Detail", Title = "Rencana Studi-Ku" }
            };

            return data;
        }
    }
}
