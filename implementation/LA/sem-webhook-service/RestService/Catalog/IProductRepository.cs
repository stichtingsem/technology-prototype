using IO.Swagger.Models;
using System.Collections.Generic;

namespace RestService.Catalog
{
    public interface IProductRepository
    {
        Product GetProductEan(string ean);
        List<Product> GetProducts(string subjectCode, string level, int? start, int? limit, string status);
    }
}