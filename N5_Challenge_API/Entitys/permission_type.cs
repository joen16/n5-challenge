using System;
using System.Collections.Generic;

namespace N5_Challenge_API.Entitys
{
    public partial class permission_type
    {
        public permission_type()
        {
            permision = new HashSet<permision>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<permision> permision { get; set; }
    }
}
