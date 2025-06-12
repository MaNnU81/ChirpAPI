using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.Services.Model.ViewModel
{
    public class CommentViewModel
    {

        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public int ChirpId { get; set; }
    }
}
