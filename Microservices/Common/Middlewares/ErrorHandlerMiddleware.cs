using Common.Exceptions;
using Common.Wrappers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Common.Middlewares;

public class ErrorHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlerMiddleware(RequestDelegate next)
  {
    _next = next;
  }
  
  public async Task Invoke(HttpContext context)
  {
    try
    {
        await _next(context);
    }
    catch (Exception error)
    {
      var response = context.Response;
      response.ContentType = "application/json";
      var responseModel = new Response<string>()
      {
        Succeeded = false, Message = error?.Message, 
        Data = error?.Data["DataMessage"]?.ToString() 
      };
      
      switch (error)
      {
        case ApiException e:
          // custom application error
          response.StatusCode = (int)HttpStatusCode.BadRequest;
          responseModel.Errors = e.Errors;
          break;
        case KeyNotFoundException e:
          // not found error
          response.StatusCode = (int)HttpStatusCode.NotFound;
          break;
        default:
          // unhandled error
          response.StatusCode = (int)HttpStatusCode.InternalServerError;
          break;
      }
  
      var result = JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings
      {
        ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new SnakeCaseNamingStrategy()
        },
        Formatting = Formatting.Indented
      });
  
      await response.WriteAsync(result); ;
    }
  }
}
