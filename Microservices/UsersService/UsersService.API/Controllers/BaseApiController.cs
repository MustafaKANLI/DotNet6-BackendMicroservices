namespace UsersService.API.Controllers;

using AutoMapper;
using Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Extensions;
using RulesEngine.Interfaces;
using RulesEngine.Models;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
  private IMediator? _mediator;
  private IMapper? _mapper;
  private IRulesEngine? _rulesEngine;
  protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
  protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>()!;
  protected IRulesEngine RulesEngine => _rulesEngine ??= HttpContext.RequestServices.GetService<IRulesEngine>()!;
  protected async Task<List<string>> ExecuteRulesAnyAsync(string workflowName, params object[] inputs)
  {
    List<RuleResultTree> resultList = await RulesEngine.ExecuteAllRulesAsync("User", inputs);
    List<string> errors = new();
    List<string> succeededRules = new();

    resultList.ForEach(result =>
    {
      if (!result.IsSuccess) errors.Add($"{result.Rule.RuleName} - Failed");
      else succeededRules.Add($"{result.Rule.RuleName} - Succeeded");
    });

    if (succeededRules.Count > 0)
    {
      return succeededRules;
    }
    

    var exception = new ApiException("One ore more rules failed.");
    exception.Errors = errors;

    throw exception;
  }

  protected async Task<List<string>> ExecuteRulesAllAsync(string workflowName, params object[] inputs)
  {
    List<RuleResultTree> resultList = await RulesEngine.ExecuteAllRulesAsync("User", inputs);
    List<string> errors = new();
    List<string> succeededRules = new();

    resultList.ForEach(result =>
    {
      if (!result.IsSuccess) errors.Add($"{result.Rule.RuleName} - Failed");
      else succeededRules.Add($"{result.Rule.RuleName} - Succeeded");
    });

    if(errors.Count > 0)
    {
      var exception = new ApiException("One ore more rules failed.");
      exception.Errors = errors;

      throw exception;
    }

    return succeededRules;
  }
}
