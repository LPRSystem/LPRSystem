using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.Service.Functions.PaymentMethod
{
    public class SavePaymentMethodFunction
    {
        private readonly ILogger<SavePaymentMethodFunction> _logger;

        public SavePaymentMethodFunction(ILogger<SavePaymentMethodFunction> logger)
        {
            _logger = logger;
        }
        [Function("SavePaymentMethodFunction")]
        public async Task<IActionResult> SavePaymentMethod([HttpTrigger(AuthorizationLevel.Anonymous,"post",Route = "PaymentMethod/SavePaymentMethod")]HttpRequest req)
        {
            _logger.LogInformation(" C# Http trigger function processed a request");

            string requestBody= await new StreamReader(req.Body).ReadToEndAsync();

            //Deserialize the json into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod>(requestBody);
             
            string connectionString =Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("insert into [data].[paymentMethod](Name,Code,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive)values(@name,@code,@createdby,@createdOn,@modifiedby,@modifiedOn,@isactive)", connection);

            //sqlCommand.CammandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@code", requestModel.Code);
            command.Parameters.AddWithValue("@createdby", requestModel.CreatedBy);
            command.Parameters.AddWithValue("@createdon", requestModel.CreatedOn);
            command.Parameters.AddWithValue("@modifiedby", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedon", requestModel.ModifiedOn);
            command.Parameters.AddWithValue("@isactive", requestModel.IsActive);
            command.ExecuteNonQuery();
            connection.Close();

            return new OkObjectResult("Welcome to Azure Functions");
        }
    }
}
