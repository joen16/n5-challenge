using System;
using System.Collections.Generic;

namespace N5_Challenge_API.Entitys
{
    public partial class employee
    {
        public employee()
        {
            permision = new HashSet<permision>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<permision> permision { get; set; }
    }
}
