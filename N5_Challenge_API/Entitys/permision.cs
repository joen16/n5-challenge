using System;
using System.Collections.Generic;

namespace N5_Challenge_API.Entitys
{
    public partial class permision
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public int IdPermissionType { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual employee IdEmployeeNavigation { get; set; } = null!;
        public virtual permission_type IdPermissionTypeNavigation { get; set; } = null!;
    }
}
