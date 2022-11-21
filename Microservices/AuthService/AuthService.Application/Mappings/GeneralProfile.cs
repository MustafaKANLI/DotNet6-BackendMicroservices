namespace AuthService.Application.Mappings;

using AutoMapper;
using AuthService.Application.Features.SharedViewModels;
using AuthService.Domain.Entities;
using Common.Parameters;


public class GeneralProfile : Profile
{
  public GeneralProfile()
  {
    //CreateMap<Entity, EntityViewModel>();
    //CreateMap<GetAllEntitiesQuery, RequestParameter>();
  }
}
