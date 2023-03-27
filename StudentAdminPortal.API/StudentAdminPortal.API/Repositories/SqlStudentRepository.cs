using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    //This class is an Implementation Class
    //Because this class is the implementation of the IStudentRepository
    //So here the interface is implemented
    //Ctrl + . (to get rid of the red squigly line)
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
    }
}
