namespace UsersService.API.Controllers;

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
  protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}
