using HotelManagement_Services.Interfaces;


namespace HotelManagement_Services.Implements
{
    public class PictureService : IPictureService
    {

        public PictureService()
        {
        }


        public string SaveImageToEnv(string filePath)
        {
            string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", Path.GetFileName(filePath));
            // Ensure the "Images" folder exists
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
            //Copy file to destination
            File.Copy(filePath, destinationPath, true);
            return destinationPath;
        }
    }
}

