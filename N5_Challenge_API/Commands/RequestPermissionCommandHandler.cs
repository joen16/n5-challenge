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
    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, List<PermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestPermissionCommandHandler> _logger;
        private readonly IElasticSearchIntegration _elasticSearchIntegration;
        private readonly IProducerMessage _producerMessage;

        public RequestPermissionCommandHandler(ILogger<RequestPermissionCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IElasticSearchIntegration elasticSearchIntegration,
             IProducerMessage producerMessage)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticSearchIntegration = elasticSearchIntegration;
            _producerMessage = producerMessage;
        }

        public async Task<List<PermissionDto>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            string methodName = "RequestPermissionCommandHandler.Handle ";
            _logger.LogDebug(methodName + "Init");

            List<PermissionDto> response;
            var list = await _unitOfWork.Repository().FindListAsync<permision>(x => x.IdEmployee == request.IdEmployee && x.IdPermissionType == request.IdPermissionType && x.Enabled, null, cancellationToken);
            if (list == null || !list.Any())
            {
                permision newP = new permision();
                newP.IdPermissionType = request.IdPermissionType;
                newP.IdEmployee = request.IdEmployee;
                newP.CreatedDate = DateTime.Now;
                newP.Enabled = true;
                _unitOfWork.Repository().Add<permision>(newP);
                await _unitOfWork.CommitAsync(cancellationToken);

                var result = await _unitOfWork.Repository().FindListAsync<permision>(x => x.IdEmployee == request.IdEmployee && x.Enabled, null, cancellationToken);

                response = _mapper.Map<List<PermissionDto>>(result);

                await _elasticSearchIntegration.IndexDocumento(newP, "search-request-permission");

                PermissionActionDto message = new PermissionActionDto();
                message.Id = Guid.NewGuid();
                message.Name = ActionTypeEnum.REQUEST;
                await _producerMessage.Push(message);

            }
            else
            {
                throw new Exception("Permission is alredy asigned to employee");
            }
            _logger.LogDebug(methodName + "End");
            return response;
        }
    }
}
