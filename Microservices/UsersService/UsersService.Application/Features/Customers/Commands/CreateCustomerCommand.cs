using Common.Wrappers;
using Mapster;
using MediatR;
using UsersService.Application.Interfaces.Repositories;
using UsersService.Domain.Entities;

namespace UsersService.Application.Features.Customers.Commands;

public class CreateCustomerCommand : IRequest<Response<string>>
{
  public string Name { get; set; } = default!;
 
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<string>>
{
  private readonly ICustomerRepositoryAsync _customerRepository;
  public CreateCustomerCommandHandler(ICustomerRepositoryAsync customerRepository)
  {
        _customerRepository = customerRepository;
  }

  public async Task<Response<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
  {
    var customer = request.Adapt<Customer>();

    await _customerRepository.AddAsync(customer);

    return new Response<string>(customer.Id.ToString(), "Customer created");
  }
}