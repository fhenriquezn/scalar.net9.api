using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scalar.net9.api.Models;
using scalar.net9.api.Models.v2;
using System.Net;

namespace scalar.net9.api.Controllers.v2
{
    [Authorize]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController(ILogger<ProductsController> logger, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [EndpointSummary("Get all products from api v2")]
        [EndpointDescription("Get a list of product from api v1 with more fields")]
        [ProducesResponseType<IList<Product>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpRequestMessage request = new(HttpMethod.Get, "products");

            var response = await _httpClientFactory.CreateClient("dummyjson").SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<ProductResponse<Product>>(content);
                return Ok(products!.Products);
            }
            return BadRequest();
        }

        [EndpointSummary("Get a product from api v2")]
        [EndpointDescription("Get an specific product from api v2 with more fields using a different model")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"products/{id}");

            var response = await _httpClientFactory.CreateClient("dummyjson").SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(content);
                return Ok(product);
            }
            return BadRequest();
        }
    }
}
