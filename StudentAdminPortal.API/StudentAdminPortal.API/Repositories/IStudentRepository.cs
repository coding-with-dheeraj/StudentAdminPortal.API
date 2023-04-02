using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    //The IStudentRepository will communicate with the Database(Student table) using the DbContext we have defined
    //It will pull all the values in the table
    public interface IStudentRepository
    {
        //Defining the method that returns a list of students from the table
        //List<Student> GetStudents(); //this is a sychnonous method, which we will change to async later

        //we are going to change the definition of the IStudentRepository by making it a Task of List of type Student.
        //We are also going to use the using System.Threading.Tasks statement.
        Task<List<DataModels.Student>> GetStudentsAsync();

        //We will define a method that returns only a single Student
        //We will pass a single parameter
        Task<DataModels.Student> GetStudentAsync(Guid studentId);

        //Defining our GetGenderAsync method of type task of list of gender
        Task<List<DataModels.Gender>>GetGendersAsync();

        //Creating an Exist Method 
        Task<bool> Exists(Guid studentId);

        //Creating updateStudent method
        //Student type and Student object, both is coming from DataModel
        Task<DataModels.Student> UpdateStudent(Guid studentId, DataModels.Student request);

        //Creating an Interface for DeleteStudent method
        //It will be a Task of type Student from DataModels
        Task<DataModels.Student> DeleteStudent(Guid studentId);

        //Creating an Interface for AddStudent method
        //Student type and object is both coming from the DataModels
        Task<DataModels.Student> AddStudent(DataModels.Student request);
    }
}
