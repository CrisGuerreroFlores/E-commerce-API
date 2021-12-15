using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Entities
{
    public class Customer : EntityBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        [Required]
        [StringLength(600)]
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(15)]
        public string Dni { get; set; }
    }
}
