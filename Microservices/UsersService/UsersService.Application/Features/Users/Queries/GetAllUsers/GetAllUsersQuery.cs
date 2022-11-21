namespace UsersService.Application.Features.Users.Queries.GetAllUsers;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Application.Features.SharedViewModels;
using Common.Wrappers;
using Common.Parameters;
using AutoMapper;
using MediatR;

public class GetAllUsersQuery : IRequest<PagedResponse<IEnumerable<UserViewModel>>>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<IEnumerable<UserViewModel>>>
{
  private readonly IUserRepositoryAsync _UserRepository;
  private readonly IMapper _mapper;
  public GetAllUsersQueryHandler(IUserRepositoryAsync UserRepository, IMapper mapper)
  {
    _UserRepository = UserRepository;
    _mapper = mapper;
  }

  public async Task<PagedResponse<IEnumerable<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
  {
    var validFilter = _mapper.Map<RequestParameter>(request);
    var dataCount = await _UserRepository.GetDataCount();
    var Users = await _UserRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

    var UserViewModels = new List<UserViewModel>();

    foreach (var p in Users)
    {
      var User = _mapper.Map<UserViewModel>(p);
      UserViewModels.Add(User);
    }

    return new PagedResponse<IEnumerable<UserViewModel>>(UserViewModels, validFilter.PageNumber, validFilter.PageSize, dataCount);
  }
}
