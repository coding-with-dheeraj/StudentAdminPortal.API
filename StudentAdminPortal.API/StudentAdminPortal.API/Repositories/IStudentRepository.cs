using StudentAdminPortal.API.DataModels;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    //The IStudentRepository will communicate with the Database(Student table) using the DbContext we have defined
    //It will pull all the values in the table
    public interface IStudentRepository
    {
        //Defining the method tht returns a list of students from the table
        //List<Student> GetStudents(); //this is a sychnonous method, which we will change to async later

        //we are going to change the definition of the IStudentRepository by making it a Task of List of type Student.
        //We are also going to use the using System.Threading.Tasks statement.
        Task<List<Student>> GetStudentsAsync();

        //We will define a method that returns only a single Student
        //We will pass a single parameter
        Task<Student> GetStudentAsync(Guid studentId);
    }
}
