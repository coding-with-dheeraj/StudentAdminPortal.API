using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]

    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        //Here in the controller, is where we access the student repository
        //We will inject the repository in the Consturctor
        //Create and assign field for studnetRepository
        //Also inject IMapper from AutoMapper
        //Similarly, create and assign field for the property mapper (Ctrl + . )
        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]

        //Since we are using async, the ActionMethod should be a type of Task
        public async Task<IActionResult> GetAllGenders() 
        {
            //Using the repository to get the GetGendersAsync() method and store it in a varible, genderList
            var genderList = await studentRepository.GetGendersAsync();

            if(genderList == null || !genderList.Any()) 
            {
                return NotFound();

            }

            //Gender type is used from the Domain Model
            return Ok(mapper.Map<List<Gender>>(genderList));
        }
    }
}
