using Common.Wrappers;
using Mapster;
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
  public CreateUserCommandHandler(IUserRepositoryAsync userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = request.Adapt<User>();

    await _userRepository.AddAsync(user);

    return new Response<string>(user.Id.ToString(), "User created");
  }
}