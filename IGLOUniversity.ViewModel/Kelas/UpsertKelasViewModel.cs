using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class UpsertKelasViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Nama kelas harus diisi")]
        public string Nama { get; set; }
        [Required(ErrorMessage = "*Matakuliah harus diisi")]
        public int? IdMatakuliah { get; set; }
        public string? Semester { get; set; }
        public int Kapasitas { get; set; }
    }
}
