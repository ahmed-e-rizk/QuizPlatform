using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Helder;
using QuizPlatform.Models;

namespace QuizPlatform.Mapping
{
    public class ImageStorageNameResolver : IMemberValueResolver<QuizDto, Quiz, IFormFile, ImageStorage>
    {
        public ImageStorage Resolve(QuizDto source, Quiz destination, IFormFile sourceMember, ImageStorage destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                // Return null or a default ImageStorage if needed
                return null;
            }

            return UploadImage.SaveImage(sourceMember);


        }
    }
}
