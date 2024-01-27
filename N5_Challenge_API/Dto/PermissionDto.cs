using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5_Challenge_API.Dto
{
    public class PermissionDto
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public int IdPermissionType { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public EmployeeDto Employee { get; set; } = null!;
        public PermissionTypeDto PermissionType{ get; set; } = null!;
    }
}
