using MySocialNetworkV2Core.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.DTOs.CreationDTOs
{
    [PostWithoutBadWords]
    public class PostCreationDTO
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public int IdUser { get; set; }
    }
}
