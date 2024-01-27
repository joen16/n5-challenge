using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5_Challenge_API.Commands;
using N5_Challenge_API.Dto.Request;
using N5_Challenge_API.Querys;
using System.Net;

namespace N5_Challenge_API.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly ISender _sender;

        public PermissionController(ILogger<PermissionController> logger,
            ISender mediator)
        {
            _sender = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Check Health
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("check")]
        public async Task<IActionResult> GetCheck()
        {
            string methodName = "PermissionController.GetCheck ";

            _logger.LogDebug(methodName + "OK");
            return Ok();
        }


        [HttpGet]
        [Route("by-employee")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        /// <summary>
        /// Get permission by id employee
        /// </summary>
        /// <param name="idEmployee">id employee</param>
        /// <returns>list of permissions</returns>

        public async Task<IActionResult> GetByEmployee(int idEmployee)
        {
            string methodName = "PermissionController.GetByEmployee ";
            _logger.LogDebug(methodName + "Init");
            var query = new GetPermissionByEmployeeQuery(idEmployee);
            var result = await _sender.Send(query);

            _logger.LogDebug(methodName + "End");
            return Ok(result);
        }

        /// <summary>
        /// Request asssing permission to employee
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>list of permissions employee</returns>
        [HttpPost]
        [Route("request-permission")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RequestPermission(RequestPermissionRequest request)
        {
            string methodName = "PermissionController.RequestPermission ";
            _logger.LogDebug(methodName + "Init");
            var query = new RequestPermissionCommand(request.IdEmployee, request.IdPermissionType);
            var result = await _sender.Send(query);
            _logger.LogDebug(methodName + "End");
            return Ok(result);
        }

        /// <summary>
        /// Disable permission by id of Permission
        /// </summary>
        /// <param name="request">request data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("modify-permission")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ModifyPermission(ModifyPermissionRequest request)
        {
            string methodName = "PermissionController.ModifyPermission ";
            _logger.LogDebug(methodName + "Init");
            var cmd = new ModifyPermissionCommand(request.IdPermission);
            await _sender.Send(cmd);
            _logger.LogDebug(methodName + "End");
            return Ok();
        }

    }
}
