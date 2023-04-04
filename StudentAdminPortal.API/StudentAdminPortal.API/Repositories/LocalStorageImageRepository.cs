namespace StudentAdminPortal.API.Repositories
{
    //This class inherits from IImage Repository
    //The interface defined in IImage Repository is implemented here
    //This class is basically an implementation of a Local Storage
    public class LocalStorageImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            //Here we are providing the path to save the image at the directory defined with the @ decorator
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            //Using Stream that takes filePath and FileMode.Create (to create it)
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            //Use the file to copy it to the provided location using 
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }
        
        //After the new file is saved, we will provide a relative path (i.e relative to the base url) to the image that is stored
        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
