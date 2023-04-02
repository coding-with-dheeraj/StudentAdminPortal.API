using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    //This class is an Implementation Class
    //Because this class is the implementation of the IStudentRepository
    //So here the interface is implemented
    //Ctrl + . (to get rid of the red squigly line)
    //Implement interface everytime a new method in the repository is created
    public class SqlStudentRepository : IStudentRepository
    {
        //Created and Assigned field 'context'
        private readonly StudentAdminContext context;

        //To implement the method, we need a context to communicate with the Database
        //So we created StudentAdminContext earlier
        //And used DI to inject context in the Program.cs file

        //Now we create a constructor, to inject our Context
        //Ctrl + . (To create and Assign field 'context')
        //Throught this SqlStudentRepository we will use this context to communicate to the Database
        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        //In the GetStudents() method, we will use context to access the Student table
        //Since we want the list of all students
        //Using LINQ method ToList() to return the list
        //public List<Student> GetStudents()
        //{
        //   return  context.Student.ToList();
        //}

        //Since we need to implement Gender and Address properties
        //We use Include() statement, by importing EF Core
        //public List<Student> GetStudents()
        //{
        //    return context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToList();
        //}

        //We are going to make the return type, Task of List of Students
        //Use the async keyword to determine that it is asynchronous
        //Use await keyword before return
        //Change the ToList() to ToListAsync()
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        //This method is generate after implementing the IStudentRepository method
        //Here we use Async and Await functionality
        //The method will have a single parameter
        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        //we will use context to go to the DB and look up the Gender table and get ToListAsync()
        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student?> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);

            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();
                return existingStudent;

            }

            return null;
        }

        // The interface created in IStudentRepository is IMPLEMENTED here
        //Making it async
        public async Task<Student> DeleteStudent(Guid studentId)
        {
            //Fetching student from the Get Student async method
            var student = await GetStudentAsync(studentId);

            if(student != null)
            {
                //Throught this SqlStudentRepository we will use this context to communicate to the Database
                //So by using context with remove method allows us to delete it from the DB
                //Saving changes
                //And Returning student for displaying purposes
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }

            //If student is empty
            return null;
        }

        //The interface AddStudent is implemented here

        public async Task<Student> AddStudent(Student request)
        {
            //Using context to go to the DB and return the Add operator
            //Using a var student to store the value while it waits for the completion of
            //an asynchronous operation before continuing with the execution of the program.
            //After that, using context to save changes
            var student = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }
    }
}
