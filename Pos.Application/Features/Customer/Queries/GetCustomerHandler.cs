﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Response.Customer;

namespace Pos.Application.Features.Customer.Queries
{
    public class GetCustomerCommand : IRequest<GetCustomerResponse>
    {
        public int Id { get; }

        public GetCustomerCommand(int id)
        {
            Id = id;
        }
    }
    public class GetCustomerHandler : IRequestHandler<GetCustomerCommand, GetCustomerResponse>
    {
        private readonly IPosDbContext _dbContext;

        public GetCustomerHandler(IPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetCustomerResponse> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (customer == null)
            {
                return null; 
            }

            var response = new GetCustomerResponse
            {
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                CompanyName = customer.CompanyName
            };

            return response;
        }
    }
}
