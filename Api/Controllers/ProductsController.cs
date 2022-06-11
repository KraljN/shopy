
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.ProductCommands;
using Application.DataTransfer;
using Application.Queries.ProductQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public ProductsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearchDto search, [FromServices] IGetProductsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProductQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromForm] ProductDto dto, [FromServices] ICreateProductCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] ProductDto dto, [FromServices] IUpdateProductCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
