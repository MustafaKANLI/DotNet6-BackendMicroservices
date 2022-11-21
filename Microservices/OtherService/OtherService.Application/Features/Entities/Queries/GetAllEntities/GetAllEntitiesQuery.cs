namespace OtherService.Application.Features.Entities.Queries.GetAllEntities;

using OtherService.Application.Interfaces.Repositories;
using OtherService.Application.Features.SharedViewModels;
using Common.Wrappers;
using Common.Parameters;
using AutoMapper;
using MediatR;

public class GetAllEntitiesQuery : IRequest<PagedResponse<IEnumerable<EntityViewModel>>>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllEntitiesQuery, PagedResponse<IEnumerable<EntityViewModel>>>
{
  private readonly IEntityRepositoryAsync _UserRepository;
  private readonly IMapper _mapper;
  public GetAllUsersQueryHandler(IEntityRepositoryAsync UserRepository, IMapper mapper)
  {
    _UserRepository = UserRepository;
    _mapper = mapper;
  }

  public async Task<PagedResponse<IEnumerable<EntityViewModel>>> Handle(GetAllEntitiesQuery request, CancellationToken cancellationToken)
  {
    var validFilter = _mapper.Map<RequestParameter>(request);
    var dataCount = await _UserRepository.GetDataCount();
    var Users = await _UserRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

    var UserViewModels = new List<EntityViewModel>();

    foreach (var p in Users)
    {
      var User = _mapper.Map<EntityViewModel>(p);
      UserViewModels.Add(User);
    }

    return new PagedResponse<IEnumerable<EntityViewModel>>(UserViewModels, validFilter.PageNumber, validFilter.PageSize, dataCount);
  }
}
