namespace AuthService.Application.Mappings;

using AutoMapper;
using UsersService.Application.Features.SharedViewModels;
using UsersService.Domain.Entities;
using Common.Parameters;

// USERS
using UsersService.Application.Features.Users.Queries.GetAllUsers;
using UsersService.Application.Features.Users.Commands;

public class GeneralProfile: Profile
{
  public GeneralProfile()
  {
    CreateMap<CreateUserCommand, User>();
    CreateMap<User, UserViewModel>();
    CreateMap<GetAllUsersQuery, RequestParameter>();
  }
}
