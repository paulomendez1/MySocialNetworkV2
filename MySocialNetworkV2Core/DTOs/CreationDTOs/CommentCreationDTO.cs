using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.DTOs.CreationDTOs
{
    public class CommentCreationDTO
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdUser { get; set; }
        public int IdPost { get; set; }

    }
}
