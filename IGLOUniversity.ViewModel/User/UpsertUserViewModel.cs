using Basilisk.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class UpsertUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Username harus dipilih")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Status harus dipilih")]
        public string Status { get; set; }     
        public bool IsAdmin { get; set; }
        public int? MahasiswaId { get; set; }
        public string Password { get; set; }
        public List<DropDownListViewModel>? DropDownStatus { get; set; }
        public List<DropDownListViewModel>? DropDownMahasiswa { get; set; }
    }
}
