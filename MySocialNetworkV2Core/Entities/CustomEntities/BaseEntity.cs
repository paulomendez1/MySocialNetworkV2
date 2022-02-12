using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Entities.CustomEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
