namespace OtherService.API.Controllers.v1;

using OtherService.Application.Features.Entities.Queries.GetAllEntities;

using Microsoft.AspNetCore.Mvc;
using Common.Parameters;

public class EntityController : BaseApiController
{
  // POST api/<controller>
  //[HttpPost]
  //public async Task<IActionResult> Create(CreateUserCommand command)
  //{
  //  return Ok(await Mediator.Send(command));
  //}

  // GET: api/<controller>
  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] RequestParameter filter)
  {
    return Ok(await Mediator.Send(new GetAllEntitiesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
  }

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
