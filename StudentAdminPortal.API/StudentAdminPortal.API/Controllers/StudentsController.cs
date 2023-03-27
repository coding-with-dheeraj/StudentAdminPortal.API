using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.API.Controllers
{
    //Since we created a new Controller, we need to annotate the controller
    [ApiController]

    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        //We need to inject IStudentRepository through Builder Services in Program.cs and using Dependency Inversion Principle
        //we need inject it in StudentsController

        //Created a constructor
        //Ctrl + . to create and assign field 'studentRepository'
        //We also need to inject AutoMapper here, by importing using AutoMapper statement
        //Ctrl + . on mapper and create and assign field
        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        //We also need to annotate the Http Get method
        //THe Route for this action method is the name "controller"
        [HttpGet]
        [Route("[controller]")]

        //GetAllStudents method
        //public IActionResult GetAllStudents()
        //{
        //    //returns an Ok object because this is a RESTful API
        //    return Ok(studentRepository.GetStudents());
        //}

        //Method to Get All Students through the Domain Model, instead of Data Model

        /**********************************
        Making the Action Method Async as well
        **********************************/
        public async Task<IActionResult> GetAllStudentsAsync() 
        {
            //This is fetched from the Data Model

            /*
             using await here to fetch data coming from the Database
             */
            var students = await studentRepository.GetStudentsAsync();

            ////Now, creating a new variable that holds the list 
            ////Ctrl + . to Import the using statement that fetches the List of students from the Domain Model
            //var domainModelsStudents = new List<Student>();

            ////Iterating through the foreach loop
            //foreach (var student in students) 
            //{
            //    //Populating this list of students from the Domain Model
            //    domainModelsStudents.Add(new Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        Mobile = student.Mobile,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Address = new Address()
            //        {
            //            Id = student.Address.Id,
            //            PhysicalAddress = student.Address.PhysicalAddress,
            //            PostalAddress = student.Address.PostalAddress,
            //        },
            //        Gender = new Gender()
            //        {
            //            Id = student.Gender.Id,
            //            Description = student.Gender.Description
            //        }
            //    });        
            //}

            ////Return the list 
            //return Ok(domainModelsStudents);

            //Return the List
            return Ok(mapper.Map<List<DomainModels.Student>>(students));
        }

    }
}
