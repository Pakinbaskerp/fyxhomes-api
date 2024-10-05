using System.Net;
using System.Net.Mail;
using API.Contract.IService;
using API.Data.Dto;
using ClosedXML.Excel;

namespace API.Service
{
    public class EmailExtractorService : IEmailExtractorService
    {
        public List<EmailExtractorDto> emailExtratorDtos(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (fileExtension != ".xlsx")
            {
                throw new InvalidDataException("Invalid file format. Please upload an .xlsx file.");
            }

            List<EmailExtractorDto> emailExtratorDtos = new List<EmailExtractorDto>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var workBook = new XLWorkbook(stream))
                {
                    var workSheet = workBook.Worksheet(1);

                    foreach (var row in workSheet.RowsUsed().Skip(1))
                    {
                        var dto = new EmailExtractorDto
                        {
                            Email = row.Cell(1).GetValue<string>(),
                            Name = row.Cell(2).GetValue<string>()
                        };

                        emailExtratorDtos.Add(dto);
                    }
                }
            }

            return emailExtratorDtos;

        }

       

    }
}