namespace AuthService.API.Controllers.v1;

using AuthService.Application.Features.Users.Commands.RegisterUser;
using AuthService.Application.Features.Users.Commands.AuthenticateUser;

using Microsoft.AspNetCore.Mvc;
using Common.Parameters;

public class UserController : BaseApiController
{
  // POST api/<controller>
  [HttpPost("Register")]
  public async Task<IActionResult> Create(RegisterUserCommand command)
  {
    return Ok(await Mediator.Send(command));
  }

  // POST api/<controller>
  [HttpPost("Authenticate")]
  public async Task<IActionResult> Authenticate(AuthenticateUserCommand command)
  {
    return Ok(await Mediator.Send(command));
  }

  // GET: api/<controller>
  //[HttpGet]
  //public async Task<IActionResult> Get([FromQuery] RequestParameter filter)
  //{
  //  return Ok(await Mediator.Send(new GetAllEntitiesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
  //}

  //// GET: api/<controller>
  //[HttpGet("Search")]
  //public async Task<IActionResult> GetBySearchFilter([FromQuery] GetUsersBySearchFilterParameter filter)
  //{
  //  return Ok(await Mediator.Send(new GetUsersBySearchFilterQuery() { FilterString = filter.FilterString, PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
  //}

  //// GET: api/<controller>/id
  //[HttpGet("{id}")]
  //public async Task<IActionResult> GetById(int id)
  //{
  //  return Ok(await Mediator.Send(new GetUserByIdQuery() { Id = id }));
  //}

  //// PATCH: api/<controller>
  //[HttpPatch]
  //public async Task<IActionResult> Patch(UpdateUserCommand command)
  //{
  //  return Ok(await Mediator.Send(command));
  //}

  //// POST: api/<controller>/deactivate
  //[HttpPost("deactivate")]
  //public async Task<IActionResult> Deactivate(DeactivateUserCommand command)
  //{
  //  return Ok(await Mediator.Send(command));
  //}

  //// POST: api/<controller>/activate
  //[HttpPost("activate")]
  //public async Task<IActionResult> Activate(ActivateUserCommand command)
  //{
  //  return Ok(await Mediator.Send(command));
  //}
}
