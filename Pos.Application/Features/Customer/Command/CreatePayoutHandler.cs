using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Customer;
using Pos.Application.Contracts.Response.Customer;

namespace Pos.Application.Features.Customer.Command
{

    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public CreateCustomerRequest Request { get; }

        public CreateCustomerCommand(CreateCustomerRequest request)
        {
            Request = request;
        }

    }

    public class CreatePayoutHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IPosDbContext _dbContext;

        public CreatePayoutHandler(IPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = new Domain.Entities.Entities.Customer()
            {
                Name = request.Request.Name,
                Address = request.Request.Address,
                Email = request.Request.Email,
                CompanyName = request.Request.CompanyName

            };
            _dbContext.Customer.Add(data);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new CreateCustomerResponse
            {
                PayoutId = new Guid(),
            };

            return response;
        }

    }
}
