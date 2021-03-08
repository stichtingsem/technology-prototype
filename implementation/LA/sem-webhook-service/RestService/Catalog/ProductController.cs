using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestService.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestService.Catalog
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository ProductRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }

        /// <summary>
        /// Get specific Product by ID
        /// </summary>
        /// <remarks>Get a specific product by EAN</remarks>
        /// <param name="ean"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/product/{ean}")]
        [Route("/product/{ean}")]
        [ValidateModelState]
        [SwaggerOperation("GetProductEan")]
        [SwaggerResponse(statusCode: 200, type: typeof(Product), description: "OK")]
        public virtual IActionResult GetProductEan([FromRoute][Required] string ean)
        {
            return new ObjectResult(ProductRepository.GetProductEan(ean));

            ////TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            //// return StatusCode(200, default(Product));
        }

        /// <summary>
        /// Get Products from Catalogue
        /// </summary>
        /// <remarks>Get products from the catalogue - either all or those matching specific criteria.</remarks>
        /// <param name="subjectCode">Filter by specific subject code </param>
        /// <param name="level">Filter by specific level </param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <param name="status">Filter by status </param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/products")]
        [Route("/products")]
        [ValidateModelState]
        [SwaggerOperation("GetProducts")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Product>), description: "OK")]
        public virtual IActionResult GetProducts([FromQuery] string subjectCode, [FromQuery] string level, [FromQuery] int? start, [FromQuery] int? limit, [FromQuery] string status)
        {
            return new ObjectResult(ProductRepository.GetProducts(subjectCode, level, start, limit, status));

            ////TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            //// return StatusCode(200, default(List<Product>));
        }
    }
}
