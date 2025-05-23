﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudentAdminPortal.API.Controllers
{
    //Since we created a new Controller, we need to annotate the controller
    [ApiController]

    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        

        //We need to inject IStudentRepository through Builder Services in Program.cs and using Dependency Inversion Principle
        //we need inject it in StudentsController

        //Created a constructor
        //Ctrl + . to create and assign field 'studentRepository'
        //We also need to inject AutoMapper here, by importing using AutoMapper statement
        //Ctrl + . on mapper and create and assign field

        //Injecting IImageRepository in the Controller constructor
        //Also create and assign a field of imageRepository
        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
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

        //Here we create a new Controller Action
        //We will define our Route here using the REST naming Convention
        //Then we will define our method as above
        //Making our method async, that returns Task of IActionResult
        //The name of the method is GetStudentAsync()
        //The Controller method expects value from the route of type Guid with the parameter name
        //Once we get the value of StudentID from the route, to our method
        //We can then pass the value to the StudentRepository so that
        //It can fetch the deatils  of the Student from the database
        //Once it is fetched from the database, it can return an object or null
        
        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch a single Student Detail
            //Creating a variable that expects a value 
            var student = await studentRepository.GetStudentAsync(studentId);

            //Return Student
            //Here we check for null cases
            if (student == null) 
            {
                return NotFound();
            }

            //Here we need AutoMapper to convert from the DataModel to the DomainModel
            return Ok(mapper.Map<DomainModels.Student>(student));

        }

        //Creating a new controller method for Updating Student Details
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]

        public async Task<IActionResult> UpdateStudentAsync([FromRoute]  Guid studentId, [FromBody] UpdateStudentRequest request)
        {

            //Checking if the student exists in the database, through the repository
            if (await studentRepository.Exists(studentId))
            {
                //Update Details
                //Here we are mapping from DataModel
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));

                if (updatedStudent != null)
                {
                    //Which is mapped back to the DomainModel
                    return Ok(mapper.Map<DomainModels.Student>(updatedStudent));
                }
            }
            return NotFound();
        }

        //Creating a new controller method for Deleting Student record
        //Using decorator to use the http verb
        //Also creating a route that requires studentId
        //The controller method will have route as the parameter, that reads the studentId
        //Because to Delete a record studentId is the only argument that needed
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]

        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            //Using studentRepository to check if the studentId through the Exist()
            //Using await because its an async method
            if (await studentRepository.Exists(studentId))
            {
                //Delete the student
                var student = await studentRepository.DeleteStudent (studentId);
                return Ok(mapper.Map<DomainModels.Student>(student));
            }

            return NotFound();
        }


        //Add Student Method
        //Using the decorator HttpPost so that the method can be identified and supported with POST operation
        //Using Route that points to the controller
        [HttpPost]
        [Route("[controller]/Add")]

        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            //Implementing AddStudent method
            //CreatedAtAction requires the last parameter to be an object, and since we are to pass Student from
            //Domain Model, we are using Mapper to map the source object to the new destination object
            var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                mapper.Map<DomainModels.Student>(student));
        }


        //Creating Upload image Controller Action
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]

        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            //Check if the student exist in the DB
            if(await studentRepository.Exists(studentId))
            {

                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                //Upload the Image to Local Storage
                var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                //Update the Profile Image path in the DB
                //On successful request, we will return the file image path as a string
                 if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))
                    {
                        return Ok(fileImagePath);
                    }

                 //But if something goes wrong, we will return a Status Code
                 return StatusCode(StatusCodes.Status500InternalServerError,"Error Uploading Image");
            }

            //If student does not exist
            return NotFound();
        }

    }
}
