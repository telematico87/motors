using eCommerce.Entities;
using eCommerce.Shared.Helpers;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            try
            {
                var apiKey = ConfigurationsHelper.SendGrid_APIKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.SendGrid_FromEmailAddressName);
                var subject = message.Subject;
                var to = new EmailAddress(message.Destination);
                var plainTextContent = message.Body;
                var htmlContent = message.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = client.SendEmailAsync(msg);
                return client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task SendToEmailAsync(string fromEmailAddressName, string fromEmailAddress, string toEmailAddress, string toEmailSubject, string toEmailBody)
        {
            try
            {
                var apiKey = ConfigurationsHelper.SendGrid_APIKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmailAddress, fromEmailAddressName);
                var to = new EmailAddress(toEmailAddress);

                var subject = toEmailSubject;

                var plainTextContent = toEmailBody;
                var htmlContent = toEmailBody;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);
                return client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }


        }

        public Task SendToEmailAsyncTemplateTest(string fromEmailAddressName, string fromEmailAddress, string toEmailAddress)
        {

            var apiKey = ConfigurationsHelper.SendGrid_APIKey;
            var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("{ Your verified email address }", "{ Sender display name }");
            //var to = new EmailAddress("{ Recipient email address }", "{ Recipient display name }");

            var from = new EmailAddress(fromEmailAddress, fromEmailAddressName);
            var to = new EmailAddress(toEmailAddress);

            var templateId = "d-389d8ceb38ca43148e35b23865ba2a93";
            var dynamicTemplateData = new
            {
                nombre = "Nataly Carrion",                
            };
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);

            return client.SendEmailAsync(msg);            
        }

        public Task SendToEmailAsyncTemplate(string fromEmailAddressName, string fromEmailAddress, string toEmailAddress, string templateId, object dynamicTemplateData)
        {
            var apiKey = ConfigurationsHelper.SendGrid_APIKey;
            var client = new SendGridClient(apiKey);            
            var from = new EmailAddress(fromEmailAddress, fromEmailAddressName);
            var to = new EmailAddress(toEmailAddress);                        
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
            return client.SendEmailAsync(msg);
        }
    }
}