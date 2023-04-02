using AutoMapper;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    //After Creating a Mapping Criteria we need to create a new class that implements the IMappingAction
    //Which is basically a Custom Mapping Action, that takes a Source(from DomainModel) and a Destination(to DataModel)
    //Because this class is exposed to the user, so Add method is received in the DomainModel and is taken to the DataModel
    //Implement IMappingAction (Ctrl + .)
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            //Using the destination object, we create Id as a new Guid in the DataModel
            //This ensures that the API will create Guid whenever a new Student is Added
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                //Same thing for address id
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
