namespace Spectrum.Model
{
    using System.Collections.Specialized;
    using System.Net.Mail;

    public class EmailRequest
    {
        public string Server { get; set; }

        public MailMessage Message { get; set; } 

        public ListDictionary Tokens { get; set; }
    }
}
