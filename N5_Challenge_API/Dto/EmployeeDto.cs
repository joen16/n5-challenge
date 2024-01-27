using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5_Challenge_API.Dto
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IEnumerable<PermissionDto> PermissionList { get; set; }
    }
}
