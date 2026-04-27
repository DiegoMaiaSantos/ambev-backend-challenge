using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;

        public List<UpdateSaleItemDto> Items { get; set; } = new();
    }

    public class UpdateSaleItemDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
