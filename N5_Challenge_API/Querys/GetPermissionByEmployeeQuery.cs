using MediatR;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Entitys;

namespace N5_Challenge_API.Querys
{
    public class GetPermissionByEmployeeQuery: IRequest<List<PermissionDto>>
    {
        public GetPermissionByEmployeeQuery(long idEmployee)
        {
            this.IdEmployee = idEmployee;
        }
        public long IdEmployee { get; set; }
    }
}
