using API.Contract.IService;
using API.Data.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace API.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly IEmailExtractorService _emailExtractorService;

        public EmailController(IEmailExtractorService emailExtractorService)
        {
            _emailExtractorService = emailExtractorService;
        }
        [HttpGet]
        [Route("api/email/template-export")]
        public IActionResult DownlodTemplate()
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Templates", "email_template.xlsx");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Template file not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", "email_template.xlsx");

        }

        [HttpPost]
        [Route("api/email/detail-import")]
        public IActionResult ImportEmail([FromForm] IFormFile file)
        {


            return Ok(_emailExtractorService.emailExtratorDtos(file));
        }
    
[HttpPost]
[Route("api/send-mail")]
    public IActionResult SendEmail([FromBody] EmailMessageDto emailMessage,
                                       [FromQuery] string host,
                                       [FromQuery] int port,
                                       [FromQuery] string userName,
                                       [FromQuery] string password)
        {
            try
            {
                SendMail(emailMessage, host, port, userName, password);
                return Ok(new { message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private void SendMail(EmailMessageDto emailMessage, string host, int port, string userName, string password)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(host)
                {
                    Port = port,
                    Timeout = 10000,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(userName, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage message = new MailMessage
                {
                    From = new MailAddress(userName),
                    Subject = emailMessage.Subject,
                    IsBodyHtml = true,
                    Body = emailMessage.Body
                };

                message.To.Add(string.Join(",", emailMessage.ToAddress));

                // Uncomment below line to send the actual email
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}