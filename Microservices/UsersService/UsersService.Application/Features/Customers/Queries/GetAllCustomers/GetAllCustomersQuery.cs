namespace UsersService.Application.Features.Customers.Queries.GetAllCustomers;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Application.Features.SharedViewModels;
using Common.Wrappers;
using Common.Parameters;
using MediatR;
using Mapster;
using Common.Exceptions;

public class GetAllCustomersQuery : IRequest<PagedResponse<IEnumerable<CustomerViewModel>>>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
}

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PagedResponse<IEnumerable<CustomerViewModel>>>
{
  private readonly ICustomerRepositoryAsync _CustomerRepository;
  public GetAllCustomersQueryHandler(ICustomerRepositoryAsync CustomerRepository)
  {
    _CustomerRepository = CustomerRepository;
  }

  public async Task<PagedResponse<IEnumerable<CustomerViewModel>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
  {
    var dataCount = await _CustomerRepository.GetDataCount();
    var Customers = await _CustomerRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

    var CustomerViewModels = new List<CustomerViewModel>();

    foreach (var p in Customers)
    {
      var Customer = p.Adapt<CustomerViewModel>();
      CustomerViewModels.Add(Customer);
    }

    return new PagedResponse<IEnumerable<CustomerViewModel>>(CustomerViewModels, request.PageNumber, request.PageSize, dataCount);
  }
}
