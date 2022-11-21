using MassTransit;
using Common.Contracts.Entities;
using OtherService.Application.Interfaces.Repositories;
using Common.Exceptions;

namespace OtherService.Application.Features.Entities.Queries.GetEntity
{
  public class GetEntityDataConsumer :
    IConsumer<GetEntityData>
  {
    readonly IEntityRepositoryAsync _entityRepository;

    public GetEntityDataConsumer(IEntityRepositoryAsync entityRepository)
    {
      _entityRepository = entityRepository;
    }

    public async Task Consume(ConsumeContext<GetEntityData> context)
    {
      var entity = await _entityRepository.GetByIdAsync(context.Message.Id);
      if (entity == null)
        throw new ApiException("Entity not found");

      await context.RespondAsync<GetEntityDataResult>(new
      {
        Data = entity.Data,
      });
    }
  }
}
