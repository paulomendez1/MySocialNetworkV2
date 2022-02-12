using MySocialNetworkV2Core.Entities.CustomEntities;
using MySocialNetworkV2Core.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetworkV2Core.Entities
{
    [PostWithoutBadWords]
    public class Post : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        public int IdUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
