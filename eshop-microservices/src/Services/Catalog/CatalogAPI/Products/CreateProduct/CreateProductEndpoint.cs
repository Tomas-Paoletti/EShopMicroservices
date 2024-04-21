
namespace CatalogAPI.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>(); //mapping object  to command
                var result = await sender.Send(command); //send command to handler
                var response = result.Adapt<CreateProductResponse>(); //mapping result to response
                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct") // Sets the name of the endpoint
                .Produces<CreateProductResponse>(StatusCodes.Status201Created) // Specifies the response type and status code
                .Produces(StatusCodes.Status400BadRequest) // Specifies the status code for a bad request
                .WithSummary("Creates a new product") // Adds a summary for the endpoint
                .WithDescription("Creates a new product in the catalog"); // Adds a description for the endpoint
        }
    }
}
