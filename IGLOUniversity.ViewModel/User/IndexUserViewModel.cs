using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class IndexUserViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public IEnumerable<GridUserViewModel> GridUser { get; set; }
    }
}
