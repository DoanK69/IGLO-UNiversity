using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class UpsertMahasiswaViewModel
    {
        [Required(ErrorMessage = "*Id harus diisi")]
        public int Id { get; set; }
        [Required(ErrorMessage = "*NIM harus diisi")]
        public string Nim { get; set; }
        [Required(ErrorMessage = "*Nama Depan harus diisi")]
        public string NamaDepan { get; set; }
        public string? NamaTengah { get; set; }
        public string? NamaBelakang { get; set; }
        [Required(ErrorMessage = "*Asal SMA harus diisi")]
        public string AsalSma { get; set; }
        [MinLength(11, ErrorMessage = "Min. 11 Angka")]
        [MaxLength(13, ErrorMessage = "Max. 13 Angka")]
        public string? NomorHp { get; set; }
        public string? Alamat { get; set; }
    }
}
