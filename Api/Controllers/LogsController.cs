using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Queries.LogQueries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public LogsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }


        // GET: api/Logs
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearchDto search, [FromServices] IGetLogsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

    }
}
