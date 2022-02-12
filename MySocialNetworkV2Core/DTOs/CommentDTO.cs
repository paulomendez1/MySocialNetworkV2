using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdPost { get; set; }
        public string User { get; set; }
        public string UserImage { get; set; }

    }
}
