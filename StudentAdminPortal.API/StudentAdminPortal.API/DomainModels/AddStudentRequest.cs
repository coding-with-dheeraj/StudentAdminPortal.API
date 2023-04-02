namespace StudentAdminPortal.API.DomainModels
{
    //We create this class in the DomainModels because that is what is exposed to the user/ outside world
    //Here we receive all the info that the user needs to create a student record

    public class AddStudentRequest
    {
        //Here we are not passing the ID because we dont want to create that
        //The API will automatically genenrate that
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public Guid GenderId { get; set; }

        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }

    }
}
