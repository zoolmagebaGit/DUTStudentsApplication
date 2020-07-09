using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace DUTStudents.Models
{
    public class Email
    {
        [Required(ErrorMessage = "Recipient's name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Recipient's email address is required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string Recipient { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Sender email password is required")]
        public string Password { get; set; }

        public void SendEmail(Students student)
        {

            var fromAddress = new MailAddress("ngcebozulu55@gmail.com", "Ngcebo");
            var toAddress = new MailAddress(Recipient, Name);
            string fromPassword = Password;
            string subject = student.Name;
            string body = student.HtmlStudent();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}