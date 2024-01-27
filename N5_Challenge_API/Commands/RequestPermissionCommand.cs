using MediatR;
using N5_Challenge_API.Dto;

namespace N5_Challenge_API.Commands
{
    public class RequestPermissionCommand : IRequest<List<PermissionDto>>
    {

        public RequestPermissionCommand(long idEmployee, int idPermissionType)
        {
            this.IdEmployee = idEmployee;
            this.IdPermissionType = idPermissionType;
        }
   
        public long IdEmployee { get; set; }
        public int IdPermissionType { get; set; }
    }
}
