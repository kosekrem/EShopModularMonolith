namespace Basket.Features.GetBasket;

public record GetBasketQuery(string UserName)
    : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCartDto ShoppingCart);
public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(q => q.UserName).NotEmpty().WithMessage("UserName cannot be empty");
    }
}


public class GetBasketHandler(IBasketRepository repository) 
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName, true ,cancellationToken: cancellationToken);

        var basketDto = basket.Adapt<ShoppingCartDto>();
        
        return new GetBasketResult(basketDto);
    }
    
}