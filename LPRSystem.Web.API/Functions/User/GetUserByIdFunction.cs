using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Models.User;
using LPRSystem.Web.API.Manager.Services;
using LPRSystem.Web.API.Manager.Services.Role;
using LPRSystem.Web.API.Manager.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Functions.User
{
    public class GetUserByIdFunction
    {
        private readonly ILogger<GetUserByIdFunction> _logger;
        private readonly IGetUserByIdManager _manager;
        private readonly IRequestParser<GetUserByIdRequest> _requestParser;

        public GetUserByIdFunction(ILogger<GetUserByIdFunction> logger,
            IGetUserByIdManager manager,
            IRequestParser<GetUserByIdRequest> requestParser)
        {
            _logger = logger;
            _manager=manager;
            _requestParser = requestParser;
        }

        [Function("GetUserById")]
        public async Task<IActionResult> GetUserById([HttpTrigger(AuthorizationLevel.Anonymous,"get",Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var request = await _requestParser.ParseAsync(req);

                if (request.UserId <= 0)
                {
                    _logger.LogWarning("Invalid userId provided: {UserId}", request.UserId);
                    return new BadRequestObjectResult("Invalid userId parameter.");
                }
                var response = await _manager.ExecuteAsync(request);

                if(response == null)
                {
                    _logger.LogWarning("No user found for userId: {UserId}", request.UserId);
                    return new NotFoundObjectResult($"No user found for userId: {request.UserId}");

                }
                return new OkObjectResult(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
