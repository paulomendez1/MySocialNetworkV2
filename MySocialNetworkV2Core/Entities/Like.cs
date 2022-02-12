using MySocialNetworkV2Core.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Entities
{
    public class Like : BaseEntity
    {
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public int IdPost { get; set; }
        public virtual Post Post { get; set; }
    }
}
