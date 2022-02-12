using MySocialNetworkV2Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string User { get; set; }
        public string UserImage { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
}
