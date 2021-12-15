using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTO.Request
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "El campo es un nombre obligatorio")]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage = "El ancho del campo es muy largo")]
        public string Description { get; set; }
    }
}
