// Programmer: Matthew White
// Date: 5/5/2015
// File: EmailCommunications.cs
// Purpose of this class is to send a email once the user provides the necessary information for the sender and email message.

using System;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace EIAP
{
    class EmailCommunications
    {
        public EmailCommunications()
        {
           instantiateVariables();
        }

                        // -------------------------------   PROPERTIES  --------------------------------------
        private SmtpClient client { get; set; }
        private MailMessage mail { get; set; }
                        // --------------------------------  INTERNAL METHODS  --------------------------------

        // Define and update SMTP settings
        internal void updateSMTPSettings(string host, string port, string username, string password, bool tls)
        {
            
            // Instantiate and pass settings for SMTP Client
            client = new SmtpClient(host, Convert.ToInt32(port));
            client.UseDefaultCredentials = false;    
            client.EnableSsl = tls;
                
            // Instantiate Network Credentials and pass user defined     
            NetworkCredential myCreds = new NetworkCredential(username, password);
           
            // Assigned Network Credentials to SMTPClient
            client.Credentials = myCreds;    
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

      

        // Update the Notification Mail Message
        internal void updateMailMessage(string from, string to, string subject, string body)
        {
              // Instantiate mail message 
                mail = new MailMessage();
                
                // Add To and From in Mail Message
                MailAddress mailFrom = new MailAddress(from);
                MailAddress mailTo = new MailAddress(to);
                mail.From = mailFrom;
                mail.To.Add(mailTo);

                mail.Subject = subject;
                mail.Body = body;
        }

        // Returns true or false, checks if the message or mail object is null
        internal bool isMessageNull()
        {
            if (mail == null)
            {
                return true;
            }
            return false;
        }

        // Send Email Notification
        internal void sendEMail()
        {    // send email
            client.Send(mail);

            // Remove all data 
            client = null;
            mail = null;
        }

        // Instantiate variables used
        private void instantiateVariables()
        {
            client = new SmtpClient();
            mail = new MailMessage();
        }

    }
}
