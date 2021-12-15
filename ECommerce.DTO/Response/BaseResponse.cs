using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DTO.Response
{
    public class BaseResponse<TResult>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public TResult Result { get; set; }
    }
}
