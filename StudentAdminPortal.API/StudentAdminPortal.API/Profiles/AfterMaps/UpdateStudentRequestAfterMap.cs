using StudentAdminPortal.API.DomainModels;
using AutoMapper;
using DataModels = StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
    {
        //Implementing IMappingAction interface (Ctrl + .)
        //AfterMappinbg our source and destination
        public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }

    }
}
