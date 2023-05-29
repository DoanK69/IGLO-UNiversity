using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Matakuliah
{
    public class UpsertMatakuliahViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Nama harus diisi")]
        public string Nama { get; set; }
        [Range(1, 4, ErrorMessage = "*Min. 1 SKS & Max. 4 SKS")]
        public int Sks { get; set; }
        public string? Deskripsi { get; set; }
    }
}
