using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {

        [HttpGet("notfound")] // Get : /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {

            // Code

            return NotFound(); // 404 Not Found

        }


        [HttpGet("servererror")] // Get : /api/buggy/servererror
        public IActionResult GetServerErrorRequest()
        {

            throw new Exception(); // 500 Internal Server Error

            return Ok(); // 200 OK
        }

        [HttpGet("badrequest")] // Get : /api/buggy/badrequest
        public IActionResult GetBadRequest()
        {

            return BadRequest(); // 400 Bad Request

        }


        [HttpGet("badrequest/{id}/{age}")] // Get : /api/buggy/badrequest
        public IActionResult GetBadRequest(int id , int age) // Validation Error
        {
            // Code
            return BadRequest(); // 400 Bad Request



        }

        [HttpGet("unauthorized")] // Get : /api/buggy/unauthorized
        public IActionResult GetUnauthorizedRequest()
        {

            return Unauthorized(); // 401 Unauthorized

        }



    }
}
