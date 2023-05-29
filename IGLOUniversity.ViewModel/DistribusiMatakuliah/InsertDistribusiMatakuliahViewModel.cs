using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.DistribusiMatakuliah
{
    public class InsertDistribusiMatakuliahViewModel
    {
        public int Id { get; set; }
        public int IdMahasiswa { get; set; }
        public int IdKelas { get; set; }
        public string Status { get; set; }
    }
}
