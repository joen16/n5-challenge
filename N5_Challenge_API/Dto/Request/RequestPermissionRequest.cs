using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5_Challenge_API.Dto.Request
{
    public class RequestPermissionRequest
    {
        [Required]
        public int IdEmployee { get; set; }

        [Required]
        public int IdPermissionType { get; set; }
    }
}
