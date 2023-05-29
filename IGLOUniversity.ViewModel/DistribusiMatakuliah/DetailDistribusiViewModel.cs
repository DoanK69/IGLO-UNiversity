using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.DistribusiMatakuliah
{
    public class DetailDistribusiViewModel
    {
        public GridMahasiswaViewModel? Mahasiswa { get; set; }

        public List<GridKelasViewModel> GridKelas { get; set; }
    }
}
