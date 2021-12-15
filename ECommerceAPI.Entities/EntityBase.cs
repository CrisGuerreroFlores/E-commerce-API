using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Entities
{
    public class EntityBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }
        public bool Status { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            Status = true;
        }
    }
}
