using Microsoft.AspNetCore.Http;

namespace LPRSystem.Web.API.Manager.Services
{
    public interface IRequestParser<TRequestModel>
    {
        Task<TRequestModel> ParseAsync(HttpRequest request);    
    }
}
