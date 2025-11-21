using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scalar.net10.api.Models;
using scalar.net10.api.Models.v1;

namespace scalar.net10.api.Controllers.v1
{
    [Authorize]
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController(ILogger<ProductsController> logger, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [EndpointSummary("Get all products from api v1")]
        [EndpointDescription("Get a list of product from api v1 with some fields")]
        [ProducesResponseType<IList<Product>>(StatusCodes.Status200OK)]
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

        [EndpointSummary("Get a product from api v1")]
        [EndpointDescription("Get an specific product from api v1 with some fields")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
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
