using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DTO.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// AQUI TIENE QUE IR EL BINARIO DE LA IMAGEN
        /// </summary>
        public string Base64Image { get; set; }
        public string FileName { get; set; }
        public bool Active { get; set; }
    }
}
