using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using StressBall.Manager;

namespace StressBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StressBallController : ControllerBase
    {

        private readonly StressBallDBManager _stressBallManager;

        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="context"></param>
        public StressBallController(StressBallContext context)
        {
            _stressBallManager = new StressBallDBManager(context);
        }

        /// <summary>
        /// Fins all stress ball - can filter by acceleration and dateTime
        /// </summary>
        /// <param name="accelerationFilter"></param>
        /// <param name="dateTimeFilter"></param>
        /// <returns></returns>
        /// <response code="404">List is not found</response>
        /// <response code="200">Everything was OK</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<StressBallData>> GetAll([FromQuery] double? accelerationFilter, [FromQuery] DateTime? dateTimeFilter)
        {
            List<StressBallData> stressBalls = _stressBallManager.GetAll(accelerationFilter, dateTimeFilter);

            if (!stressBalls.Any())
            {
                return NotFound("No stress ball found");
            }
            return Ok(stressBalls);
        }

        /// <summary>
        /// Finds ball by an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">List is not found</response>
        /// <response code="200">Everything was OK</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            StressBallData stressBall = _stressBallManager.GetById(id);

            if (stressBall == null)
            {
                return NotFound("No such ball found, id: " + id);
            }
            return Ok(stressBall);
        }

        /// <summary>
        /// Finds ball by Id and updates it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="404">List is not found</response>
        /// <response code="200">Everything was OK</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public StressBallData Put(int id, [FromBody] StressBallData value)
        {
            return _stressBallManager.Update(id, value);
        }

        /// <summary>
        /// Creates a new stress ball object
        /// </summary>
        /// <param name="newStressBall"></param>
        /// <returns></returns>
        /// <response code="201">Item was created</response>
        /// <response code="400">Error 400: Are you missing a parameter?</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<StressBallData> Post([FromBody] StressBallData newStressBall)
        {
            StressBallData stressball = new StressBallData();

            if (newStressBall.Speed == null || newStressBall.DateTimeNow == null)
            {
                return BadRequest(newStressBall);
            }

            stressball = _stressBallManager.Add(newStressBall);
            return Created("api/stressball/" + stressball.Id, stressball);
        }
        
        /// <summary>
        /// Deletes an stress ball found by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// /// <response code="200">Everything was OK</response>
        /// <response code="404">Error: List is not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public ActionResult<StressBallData> Delete(int id)
        {
            StressBallData stressBall = _stressBallManager.GetById(id);

            if (stressBall == null)
            {
                return NotFound("ball not found, id: " + id);
            }
            return _stressBallManager.Delete(id);
        }

        //This is at test to github
    }
}
