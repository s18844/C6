using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyklad5.DTOs.Requests;
using Wyklad5.DTOs.Responses;
using Wyklad5.Models;
using Wyklad5.Services;

namespace Wyklad5.Controllers
{
    [Route("api/enrollments")]
    [ApiController] //-> implicit model validation
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
              var response = _service.EnrollStudent(request);

            if (response == null) return NotFound("problem");
             else return Ok(response); 
           
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(EnrollStudentRequest2 request)
        {
            var response = _service.PromoteStudents(request.Semester,request.Studies);
            if (response == null) return NotFound("problem");
            else return Ok("GIT");
            

        }

        //..

        //..


    }
}