using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, Sale?>
    {
        private readonly ISaleRepository _repository;

        public GetSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Sale?> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
