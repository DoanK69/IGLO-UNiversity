using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class IndexMahasiswaViewModel
    {
        public int TotalHalaman { get; set; }
        public int TotalData { get; set; }
        public IEnumerable<GridMahasiswaViewModel> GridMahasiswa { get; set; }
    }
}
