using MySocialNetworkV2Core.Entities.CustomEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetworkV2Core.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        [ForeignKey("IdPost")]
        public virtual Post Post { get; set; }
        public int IdPost { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        public int IdUser { get; set; }


    }
}
