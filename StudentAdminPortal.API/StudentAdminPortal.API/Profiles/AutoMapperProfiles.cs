using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using DataModels = StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles
{
    // This class inherits from Profile Class (Ctrl + . to import using AutoMapper statement)
    public class AutoMapperProfiles: Profile
    {
        //creating a constructor
        public AutoMapperProfiles()
        {
            //We will use CreateMap option offered by the Profile class
            //It asks for a type and a destination
            //Since we are mapping DataModels to our DomainModel
            //So our Source will be Students from DataModel
            //And Destination that it will be Mapping to Students of DomainModel 
            //We are using DataModels.Students to specify beacause both the source and the destination have the same name
            //Incase we want to Reverse the data coming from the DataModel to the DomainModel, we will use ReverseMap()
            CreateMap<DataModels.Student, Student>()
                .ReverseMap();

            CreateMap<DataModels.Gender, Gender>()
                .ReverseMap();

            CreateMap<DataModels.Address, Address>()
                .ReverseMap();
        }
    }
}
