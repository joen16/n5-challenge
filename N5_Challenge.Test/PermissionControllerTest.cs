using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using N5_Challenge_API.Controllers;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5_Challenge.Test
{
    public class PermissionControllerTest
    {
        private readonly PermissionController _controller;
        private readonly Mock<ISender> _sender;
        private readonly Mock<ILogger<PermissionController>> _logger;

        public PermissionControllerTest()
        {
            _sender = new Mock<ISender>();
            _logger = new Mock<ILogger<PermissionController>>();
            _controller = new PermissionController(_logger.Object, _sender.Object);
        }

        [Fact]
        public async void GetPermissionByEmployee_ReturnData_OK()
        {
            var request = new RequestPermissionRequest();
            request.IdPermissionType = 1;
            request.IdEmployee = 1;
            var result_create = await _controller.RequestPermission(request);
            Assert.NotNull(result_create);

            var result_get = await _controller.GetByEmployee(1);
            Assert.NotNull(result_get);
            
        }
    }
}
