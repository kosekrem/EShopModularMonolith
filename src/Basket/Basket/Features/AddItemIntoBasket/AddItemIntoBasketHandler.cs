using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItemDto) 
    : ICommand<AddItemIntoBasketResult>;
public record AddItemIntoBasketResult(Guid Id);
public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.ShoppingCartItemDto.ProductId).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.ShoppingCartItemDto.Quantity).NotEmpty().WithMessage("Quantity is required");
    }
}

public class AddItemIntoBasketHandler
    (IBasketRepository repository, ISender sender)
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);
        
        var result = await sender.Send(
            new GetProductByIdQuery(command.ShoppingCartItemDto.ProductId), cancellationToken);

           shoppingCart.AddItem(
               command.ShoppingCartItemDto.ProductId,
               command.ShoppingCartItemDto.Quantity,
               command.ShoppingCartItemDto.Color,
               result.Product.Price,
               result.Product.Name);
        
        
        await repository.SaveChangesAsync(command.UserName, cancellationToken);
        
        return new AddItemIntoBasketResult(shoppingCart.Id);
    }
}