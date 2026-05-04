
namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsByCategoryQuery for category: {Category}", query.category);

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.category))
                .ToListAsync();

            return new GetProductsByCategoryResult(products);
        }
    }
}
