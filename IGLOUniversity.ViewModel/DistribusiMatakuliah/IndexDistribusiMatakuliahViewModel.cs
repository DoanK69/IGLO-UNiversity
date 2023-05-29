using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.DistribusiMatakuliah
{
    public class IndexDistribusiMatakuliahViewModel
    {

        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public IEnumerable<GridDistribusiMatakuliahViewModel> GridDistribusi { get; set; }
    }
}
