namespace UsersService.Application.Features.Customers.Queries.GetById;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Application.Features.SharedViewModels;
using Common.Wrappers;
using Common.Parameters;
using MediatR;
using Mapster;
using Common.Exceptions;

public class GetByIdQuery : IRequest<Response<CustomerViewModel>>
{
  public int Id { get; set; }
  
}

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Response<CustomerViewModel>>
{
  private readonly ICustomerRepositoryAsync _CustomerRepository;
  public GetByIdQueryHandler(ICustomerRepositoryAsync CustomerRepository)
  {
    _CustomerRepository = CustomerRepository;
  }

  public async Task<Response<CustomerViewModel>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
  {

    var Customer = await _CustomerRepository.GetByIdAsync(request.Id);

    
        if(Customer == null)
        {
            throw new ApiException("Customer Not Found!");
        }
   

    return new Response<CustomerViewModel>(Customer.Adapt<CustomerViewModel>());
  }
}
