using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICartRules _cartRules;

    public CreateCartHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ICartRules cartRules)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _cartRules = cartRules;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productIds = command.Products.Select(p => p.ProductId).Distinct().ToList();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);
        
        var cart = _mapper.Map<Cart>(command);

        foreach (var cartProduct in cart.Products)
        {
            var product = products.FirstOrDefault(p => p.Id == cartProduct.ProductId) ?? throw new ValidationException($"Product {cartProduct.ProductId} not found");
            cartProduct.UpdateProductInfo(product);
        }

        _cartRules.ValidatePurchase(cart.Products.ToList());

        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);
        var result = _mapper.Map<CreateCartResult>(createdCart);
        return result;
    }
}