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
      /// <summary>
      /// Get specific Product by ID
      /// </summary>
      /// <remarks>Get a specific product by EAN</remarks>
      /// <param name="ean"></param>
      /// <response code="200">OK</response>
      [HttpGet]
      [Route("/api/product/{ean}")]
      [ValidateModelState]
      [SwaggerOperation("GetProductEan")]
      [SwaggerResponse(statusCode: 200, type: typeof(Product), description: "OK")]
      public virtual IActionResult GetProductEan([FromRoute][Required] string ean)
      {
         //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
         // return StatusCode(200, default(Product));
         string exampleJson = null;
         exampleJson = "{\n  \"longDescription\" : \"longDescription\",\n  \"models\" : [ {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  }, {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  } ],\n  \"bundledProducts\" : [ \"bundledProducts\", \"bundledProducts\" ],\n  \"streams\" : [ {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  }, {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  } ],\n  \"relatedProducts\" : [ \"relatedProducts\", \"relatedProducts\" ],\n  \"defaultAccessUrl\" : \"defaultAccessUrl\",\n  \"shortDescription\" : \"shortDescription\",\n  \"media\" : {\n    \"productImageUrls\" : [ null, null ],\n    \"mainThumbnailUrl\" : {\n      \"width\" : \"width\",\n      \"description\" : \"description\",\n      \"type\" : \"type\",\n      \"url\" : \"url\",\n      \"height\" : \"height\"\n    },\n    \"productPdfUrls\" : [ null, null ],\n    \"productVideoUrls\" : [ null, null ]\n  },\n  \"type\" : \"physical\",\n  \"trialAccessUrl\" : \"trialAccessUrl\",\n  \"levelSubjects\" : [ {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  }, {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  } ],\n  \"ean\" : \"ean\",\n  \"name\" : \"name\",\n  \"status\" : \"active\"\n}";

         var example = exampleJson != null
         ? JsonConvert.DeserializeObject<Product>(exampleJson)
         : default(Product);            //TODO: Change the data returned
         return new ObjectResult(example);
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
      [ValidateModelState]
      [SwaggerOperation("GetProducts")]
      [SwaggerResponse(statusCode: 200, type: typeof(List<Product>), description: "OK")]
      public virtual IActionResult GetProducts([FromQuery] string subjectCode, [FromQuery] string level, [FromQuery] int? start, [FromQuery] int? limit, [FromQuery] string status)
      {
         //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
         // return StatusCode(200, default(List<Product>));
         string exampleJson = null;
         exampleJson = "[ {\n  \"longDescription\" : \"longDescription\",\n  \"models\" : [ {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  }, {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  } ],\n  \"bundledProducts\" : [ \"bundledProducts\", \"bundledProducts\" ],\n  \"streams\" : [ {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  }, {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  } ],\n  \"relatedProducts\" : [ \"relatedProducts\", \"relatedProducts\" ],\n  \"defaultAccessUrl\" : \"defaultAccessUrl\",\n  \"shortDescription\" : \"shortDescription\",\n  \"media\" : {\n    \"productImageUrls\" : [ null, null ],\n    \"mainThumbnailUrl\" : {\n      \"width\" : \"width\",\n      \"description\" : \"description\",\n      \"type\" : \"type\",\n      \"url\" : \"url\",\n      \"height\" : \"height\"\n    },\n    \"productPdfUrls\" : [ null, null ],\n    \"productVideoUrls\" : [ null, null ]\n  },\n  \"type\" : \"physical\",\n  \"trialAccessUrl\" : \"trialAccessUrl\",\n  \"levelSubjects\" : [ {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  }, {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  } ],\n  \"ean\" : \"ean\",\n  \"name\" : \"name\",\n  \"status\" : \"active\"\n}, {\n  \"longDescription\" : \"longDescription\",\n  \"models\" : [ {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  }, {\n    \"price\" : 0.8008281904610115,\n    \"price_period\" : \"price_period\",\n    \"name\" : \"pre-pay\",\n    \"description\" : \"Pre-Pay: This product must be paid for before it can be used.\",\n    \"id\" : \"24e39454-5360-4ba4-819f-03e59b8dd679\",\n    \"price_currency\" : \"price_currency\"\n  } ],\n  \"bundledProducts\" : [ \"bundledProducts\", \"bundledProducts\" ],\n  \"streams\" : [ {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  }, {\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"id\" : \"id\"\n  } ],\n  \"relatedProducts\" : [ \"relatedProducts\", \"relatedProducts\" ],\n  \"defaultAccessUrl\" : \"defaultAccessUrl\",\n  \"shortDescription\" : \"shortDescription\",\n  \"media\" : {\n    \"productImageUrls\" : [ null, null ],\n    \"mainThumbnailUrl\" : {\n      \"width\" : \"width\",\n      \"description\" : \"description\",\n      \"type\" : \"type\",\n      \"url\" : \"url\",\n      \"height\" : \"height\"\n    },\n    \"productPdfUrls\" : [ null, null ],\n    \"productVideoUrls\" : [ null, null ]\n  },\n  \"type\" : \"physical\",\n  \"trialAccessUrl\" : \"trialAccessUrl\",\n  \"levelSubjects\" : [ {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  }, {\n    \"level\" : \"level\",\n    \"subjectCode\" : \"subjectCode\"\n  } ],\n  \"ean\" : \"ean\",\n  \"name\" : \"name\",\n  \"status\" : \"active\"\n} ]";

         var example = exampleJson != null
         ? JsonConvert.DeserializeObject<List<Product>>(exampleJson)
         : default(List<Product>);            //TODO: Change the data returned
         return new ObjectResult(example);
      }
   }
}
