using AutoMapper;
using MediatR;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Dto.Topic;
using N5_Challenge_API.Entitys;
using N5_Challenge_API.Enum;
using N5_Challenge_API.Integration;
using N5_Challenge_API.Integration.Topic;
using N5_Challenge_API.Repository.Interfaces;

namespace N5_Challenge_API.Commands
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ModifyPermissionCommandHandler> _logger;
        private readonly IElasticSearchIntegration _elasticSearchIntegration;
        private readonly IProducerMessage _producerMessage;

        public ModifyPermissionCommandHandler(ILogger<ModifyPermissionCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper, IElasticSearchIntegration elasticSearchIntegration,
             IProducerMessage producerMessage)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticSearchIntegration = elasticSearchIntegration;
            _producerMessage = producerMessage;
        }

        public async Task Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            string methodName = "ModifyPermissionCommandHandler.Handle ";
            _logger.LogDebug(methodName + "Init");
            var permissionBd = await _unitOfWork.Repository().GetById<permision>(request.IdPermission);
            if (permissionBd != null)
            {
                if (permissionBd.Enabled)
                {
                    permissionBd.Enabled = false;
                    _unitOfWork.Repository().Update<permision>(permissionBd);
                    await _unitOfWork.CommitAsync(cancellationToken);

                   
                    await _elasticSearchIntegration.IndexDocumento(permissionBd, "search-modify-permission");
                   
                    PermissionActionDto message = new PermissionActionDto();
                    message.Id = Guid.NewGuid();
                    message.Name = ActionTypeEnum.MODIFY;
                    await _producerMessage.Push(message);
                }
                else
                {
                    throw new Exception("Permission is alredy disabled");
                }
            }
            else
            {
                throw new Exception("Permission not valid or not found");
            }
            _logger.LogDebug(methodName + "End");
            return;
        }
    }
}
