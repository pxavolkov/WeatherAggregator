using System.Net;
using System.Net.Mail; 

namespace WeatherAggregator.Core.Logic
{
    public static class EmailSender
    {
        private static string fromEmailAddress = @"weatheraggregatortest@gmail.com";
        private static string password = @"123!@qweQW";
        private static string hostAddress = "smtp.gmail.com";

        public static void SendEmail(MailMessage mail)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.Port = 587;
                smtp.Host = hostAddress;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromEmailAddress, password);

                smtp.Send(mail);
            }
        }
    }
}
