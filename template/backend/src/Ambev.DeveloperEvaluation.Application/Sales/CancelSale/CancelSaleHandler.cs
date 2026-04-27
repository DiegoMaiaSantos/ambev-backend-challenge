using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<CancelSaleHandler> _logger;

        public CancelSaleHandler(ISaleRepository repository, ILogger<CancelSaleHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new Exception("Sale not found");

            sale.IsCancelled = true;

            await _repository.UpdateAsync(sale, cancellationToken);

            _logger.LogInformation("SaleCancelled event triggered for SaleId: {SaleId}", sale.Id);

            return new CancelSaleResult
            {
                Success = true
            };
        }
    }
}
