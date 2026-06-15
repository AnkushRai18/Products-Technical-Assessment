using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Feature.Command;
using Products.APPLICATION.Feature.Query;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await mediator.Send(new GetAllProducts());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =
                await mediator.Send(new GetProductById(id));

            return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var result =
                await mediator.Send(new AddProducts(dto));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ProductDto dto)
        {
            dto.Id = id;

            var result =
                await mediator.Send(new UpdateProduct(dto));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await mediator.Send(new DeleteProduct(id));

            return Ok(result);
        }
    }
}
