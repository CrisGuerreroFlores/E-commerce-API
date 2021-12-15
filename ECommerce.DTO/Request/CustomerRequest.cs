using System;

namespace ECommerce.DTO.Request
{
    public class CustomerRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Dni { get; set; }
    }
}
