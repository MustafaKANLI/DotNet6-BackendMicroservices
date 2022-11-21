namespace AuthService.Application.Mappings;

// USERS
using UsersService.Application.Features.Users.Queries.GetAllUsers;

using AutoMapper;
using UsersService.Application.Features.SharedViewModels;
using UsersService.Domain.Entities;
using Common.Parameters;


public class GeneralProfile: Profile
{
  public GeneralProfile()
  {
    CreateMap<User, UserViewModel>();
    CreateMap<GetAllUsersQuery, RequestParameter>();
  }
}
