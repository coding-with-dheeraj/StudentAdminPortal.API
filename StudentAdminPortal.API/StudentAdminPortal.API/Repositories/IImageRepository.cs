using System.Net;

namespace StudentAdminPortal.API.Repositories
{
    public interface IImageRepository
    {
        //It will contain the upload method
        //Return type of this method is Task of string
        Task<String>Upload(IFormFile file, string fileName);
    }
}
