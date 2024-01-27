using AutoMapper;
using MediatR;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Dto.Topic;
using N5_Challenge_API.Entitys;
using N5_Challenge_API.Enum;
using N5_Challenge_API.Integration;
using N5_Challenge_API.Integration.Topic;
using N5_Challenge_API.Repository.Interfaces;

namespace N5_Challenge_API.Querys
{
    /// <summary>
    /// GEt permissions by id employee
    /// </summary>
    public class GetPermissionByEmployeeQueryHandler : IRequestHandler<GetPermissionByEmployeeQuery, List<PermissionDto>>
    {
        private readonly ILogger<GetPermissionByEmployeeQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticSearchIntegration _elasticSearchIntegration;
        private readonly IProducerMessage _producerMessage;

        public GetPermissionByEmployeeQueryHandler(ILogger<GetPermissionByEmployeeQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IElasticSearchIntegration elasticSearchIntegration,
             IProducerMessage producerMessage)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticSearchIntegration = elasticSearchIntegration;
            _producerMessage = producerMessage;
        }

        public async Task<List<PermissionDto>> Handle(GetPermissionByEmployeeQuery request, CancellationToken cancellationToken)
        {
            string methodName = "GetPermissionByEmployeeQueryHandler.Handle ";
            _logger.LogDebug(methodName + "Init");
            List<PermissionDto> response;
            var employee = await _unitOfWork.Repository().GetById<employee>(request.IdEmployee);
            if (employee != null)
            {
                var list = await _unitOfWork.Repository().FindListAsync<permision>(x => x.IdEmployee == request.IdEmployee && x.Enabled , null, cancellationToken);


                list.ForEach(async x =>
                {
                    await _elasticSearchIntegration.IndexDocumento(x, "search-get-permission");
                });


                PermissionActionDto message = new PermissionActionDto();
                message.Id = Guid.NewGuid();
                message.Name = ActionTypeEnum.GET;
                await _producerMessage.Push(message);

                response = _mapper.Map<List<PermissionDto>>(list);
            }
            else
            {
                throw new Exception("Employee not found");
            }
            _logger.LogDebug(methodName + "End");
            return response;
        }

    }
}
