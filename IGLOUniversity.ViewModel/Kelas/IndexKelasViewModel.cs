using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class IndexKelasViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public IEnumerable<GridKelasViewModel> GridKelas { get; set; }
    }

    public class IndexViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public Object Grid { get; set; }
    }
}
