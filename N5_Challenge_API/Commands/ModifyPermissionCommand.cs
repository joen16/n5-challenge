using MediatR;
using N5_Challenge_API.Dto;

namespace N5_Challenge_API.Commands
{
    public class ModifyPermissionCommand : IRequest
    {

        public ModifyPermissionCommand(long idPermission)
        {
            this.IdPermission = idPermission;
        }
   
        public long IdPermission { get; set; }
    }
}
