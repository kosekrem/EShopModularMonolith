using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductRequest(PaginationRequest PaginationRequest);
public record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products", async ([AsParameters] PaginationRequest request, ISender sender) => 
            {
                var result = await sender.Send(new GetProductsQuery(request));

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithSummary("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}