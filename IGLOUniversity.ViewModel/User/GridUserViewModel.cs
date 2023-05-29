using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class GridUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string IsAdmin { get; set; }
        public string Mahasiswa { get; set; }
        public string Password { get; set; }
    }
}
