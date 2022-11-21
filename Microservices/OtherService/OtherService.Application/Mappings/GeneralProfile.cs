namespace OtherService.Application.Mappings;

// Entities
using OtherService.Application.Features.Entities.Queries.GetAllEntities;

using AutoMapper;
using OtherService.Application.Features.SharedViewModels;
using OtherService.Domain.Entities;
using Common.Parameters;


public class GeneralProfile : Profile
{
  public GeneralProfile()
  {
    CreateMap<Entity, EntityViewModel>();
    CreateMap<GetAllEntitiesQuery, RequestParameter>();
  }
}
