using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.DataModels
{
    //StudentAdminContext class is inheriting from base class DbContext
    //CTRL + . to import using EF Core statement, to import the DbContext in this class
    public class StudentAdminContext : DbContext
    {
        //Creating a constuctor using ctor Tab Tab
        //With parameter DbContextOptions of type StudentAdminContext, passing it to the base class
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options): base(options) 
        {
            
        }

        //Now, I will create three properties which will act as DbSets and when I run EF Core migrations
        //I will use these properties to create tables in our Database.
        //This property will be of type DbSet, and DbSet will be of type Student, and so on.
        public DbSet<Student> Student { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }

        //Now our DbContext is Ready
    }
}
