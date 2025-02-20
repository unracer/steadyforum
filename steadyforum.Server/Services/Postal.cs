using System.Collections.ObjectModel;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

public class Postal 
{
    public string Title { get; set; }

    public int sendEmail()
    {
        SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);
        smtpClient.Credentials = new System.Net.NetworkCredential("[email protected]", "myIDPassword");
        smtpClient.UseDefaultCredentials = true;
        //uncomment if you don't want to use the network
        //credentialssmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true; 
        MailMessage mail = new MailMessage();
        //Setting From, To and 
        CCmail.From = new MailAddress("info @MyWebsiteDomainName", "MyWeb Site");
        mail.To.Add(new MailAddress("info @MyWebsiteDomainName"));
        mail.CC.Add(new MailAddress("[email protected]"));
        smtpClient.Send(mail);

        return 1;
    }
}