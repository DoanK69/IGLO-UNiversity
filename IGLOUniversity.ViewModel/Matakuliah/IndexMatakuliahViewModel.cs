using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Matakuliah
{
    public class IndexMatakuliahViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public IEnumerable<GridMatakuliahViewModel> GridMatakuliah { get; set; }
    }
}
