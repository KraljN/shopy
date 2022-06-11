using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.OrderCommands;
using Application.DataTransfer;
using Application.Queries.OrderQueries;
using Application.Searches;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public OrdersController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearchDto search, [FromServices] IGetOrdersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOrderQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto, [FromServices] ICreateOrderCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChangeOrderStatusDto dto, [FromServices] IUpdateOrderCommand command)
        {
            dto.OrderId = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }
    }
}
