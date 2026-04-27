using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public UpdateSaleHandler(ISaleRepository repository, ILogger<UpdateSaleHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new Exception("Sale not found");

            sale.CustomerName = request.CustomerName;
            sale.BranchName = request.BranchName;

            sale.Items.Clear();

            decimal total = 0;

            foreach (var item in request.Items)
            {
                if (item.Quantity > 20)
                    throw new Exception("Cannot sell more than 20 identical items");

                decimal discount = 0;

                if (item.Quantity >= 10)
                    discount = 0.2m;
                else if (item.Quantity >= 4)
                    discount = 0.1m;

                var totalItem = item.Quantity * item.UnitPrice;
                var discountValue = totalItem * discount;
                var finalItemTotal = totalItem - discountValue;

                sale.Items.Add(new SaleItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = discountValue,
                    TotalAmount = finalItemTotal
                });

                total += finalItemTotal;
            }

            sale.TotalAmount = total;

            await _repository.UpdateAsync(sale, cancellationToken);

            _logger.LogInformation("SaleModified event triggered for SaleId: {SaleId}", sale.Id);

            return new UpdateSaleResult
            {
                Id = sale.Id,
                TotalAmount = sale.TotalAmount
            };
        }
    }
}
