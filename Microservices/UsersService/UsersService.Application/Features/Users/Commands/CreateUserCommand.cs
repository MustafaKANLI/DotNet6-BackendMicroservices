using AutoMapper;
using Common.Wrappers;
using MediatR;
using UsersService.Application.Interfaces.Repositories;
using UsersService.Domain.Entities;

namespace UsersService.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Response<string>>
{
  public string FirstName { get; set; } = default!;
  public int Age { get; set; } = default!;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
{
  private readonly IUserRepositoryAsync _userRepository;
  private readonly IMapper _mapper;
  public CreateUserCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = _mapper.Map<User>(request);

    await _userRepository.AddAsync(user);

    return new Response<string>(user.Id.ToString(), "User created");
  }
}