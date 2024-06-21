using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Menu;
using Pos.Application.Contracts.Response.Menu;

namespace Pos.Application.Features.Customer.Command
{

    public class CreateMenuCommand : IRequest<CreateMenuResponse>
    {
        public CreateMenuRequest Request { get; }

        public CreateMenuCommand(CreateMenuRequest request)
        {
            Request = request;
        }

    }

    public class CreateMenuHandler : IRequestHandler<CreateMenuCommand, CreateMenuResponse>
    {
        private readonly IPosDbContext _dbContext;

        public CreateMenuHandler(IPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateMenuResponse> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var data = new Domain.Entities.Entities.Menu()
            {
                Name = request.Request.Name,
                Item = request.Request.Item,
                Size = request.Request.Size,
                Variant = request.Request.Variant,
                price = request.Request.price

            };
            _dbContext.Menu.Add(data);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new CreateMenuResponse
            {
                Id = new Guid(),
            };

            return response;
        }

    }
}
