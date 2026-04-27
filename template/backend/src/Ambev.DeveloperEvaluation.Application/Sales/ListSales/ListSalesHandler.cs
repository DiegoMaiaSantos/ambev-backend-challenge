using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<ListSalesCommand, List<Sale>>
    {
        private readonly ISaleRepository _repository;

        public ListSalesHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Sale>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
