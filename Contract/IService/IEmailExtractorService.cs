using API.Data.Dto;

namespace API.Contract.IService
{
    public interface IEmailExtractorService
    {
         List<EmailExtractorDto> emailExtratorDtos(IFormFile file);
         
    }
}