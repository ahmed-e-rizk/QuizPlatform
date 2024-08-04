using QuizPlatform.Models;

namespace QuizPlatform.Helder
{
    public static class UploadImage
    {

        public static ImageStorage SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return new ImageStorage();            }

            var fileExtension = Path.GetExtension(imageFile.FileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFile.FileName);
            var fileName = fileNameWithoutExtension+ Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Images"), fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return new ImageStorage()
            {
               Name =fileName,
               FileSize = imageFile.Length,
               FuLlPath = filePath
            };
        }

        public static bool DeleteImage(string fullPath)
        {

            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
