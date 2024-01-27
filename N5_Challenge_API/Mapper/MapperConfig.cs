using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5_Challenge_API.Mapper
{
    public class MapperConfig
    {
        public static IMapper InitializeAutomapper()
        {

            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<PermisssionProfile>();
                cfg.AddProfile<PermissionTypeProfile>();
            });
            //Create an Instance of Mapper and return that Instance
            return config.CreateMapper();

        }
    }
}
