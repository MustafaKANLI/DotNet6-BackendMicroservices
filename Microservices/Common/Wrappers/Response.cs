using Microsoft.AspNetCore.Mvc;

namespace Common.Wrappers;

public class Response<T>
{

  public bool Succeeded { get; set; }
  public string? Message { get; set; }
  public List<string>? Errors { get; set; }
  public T? Data { get; set; }

  public Response()
  {
  }

  public Response(T data, string message = "")
  {
      Succeeded = true;
      Message = message;
      Data = data;
  }
  public Response(string message)
  {
      Succeeded = false;
      Message = message;
  }
  
  public static BadRequestObjectResult ModelValidationErrorResponse(ActionContext context)
  {
    
    var errors = context.ModelState
      .Where(modelError => modelError.Value.Errors.Count > 0)
      .Select(modelError => modelError.Value.Errors.FirstOrDefault()?.ErrorMessage)
      .ToList();

    var response = new Response<T>
    {
      Succeeded = false,
      Errors = errors,
      Message = "Validation errors occured",
    };

    return new BadRequestObjectResult(response);
  }
}
