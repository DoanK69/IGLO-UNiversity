using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.DistribusiMatakuliah
{
    public class GridDistribusiMatakuliahViewModel
    {
        public int Id { get; set; }
        public string Nim { get; set; }
        public string Nama { get; set; }
        public string AsalSma { get; set; }
        public string NomorHp { get; set; }
        public string Alamat { get; set; }
        public int? TotalSks { get; set; }
    }
}
