using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, ILogger<CreateSaleHandler> logger, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                SaleNumber = Guid.NewGuid().ToString(),
                SaleDate = DateTime.UtcNow,
                CustomerName = command.CustomerName,
                BranchName = command.BranchName,
                IsCancelled = false
            };

            decimal total = 0;

            foreach (var item in command.Items)
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

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            _logger.LogInformation("SaleCreated event triggered for SaleId: {SaleId}", createdSale.Id);

            return new CreateSaleResult
            {
                Id = createdSale.Id,
                TotalAmount = createdSale.TotalAmount
            };
        }
    }
}
