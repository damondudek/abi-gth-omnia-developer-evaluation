using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.Common
{
    public class ProductRules : IProductRules
    {
        private readonly IProductRepository _productRepository;

        public ProductRules(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CheckRulesToCreate(CreateProductCommand command, CancellationToken cancellationToken)
        {
            await CheckIsProductExistsByTitle(command.Title, cancellationToken);
        }

        public async Task CheckRulesToUpdate(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingProduct is null)
                throw new KeyNotFoundException($"Product with id {command.Id} does not exist");

            if (existingProduct.Title != command.Title)
                await CheckIsProductExistsByTitle(command.Title, cancellationToken);
        }

        private async Task CheckIsProductExistsByTitle(string title, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByTitleAsync(title, cancellationToken);
            if (existingProduct is not null)
                throw new ArgumentException($"Product with title {title} already exists");
        }
    }

    public interface IProductRules
    {
        Task CheckRulesToCreate(CreateProductCommand command, CancellationToken cancellationToken);
        Task CheckRulesToUpdate(UpdateProductCommand command, CancellationToken cancellationToken);
    }
}