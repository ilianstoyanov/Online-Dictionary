using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Configuration;

public partial class Modules_FreeAccess_About : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbtnSendEmail_Click(object sender, EventArgs e)
    {
        string from = txtEmail.Text.Trim().ToLower();
      
        MailMessage mail = new MailMessage();

        mail.To.Add(ConfigurationManager.AppSettings["adminEmail"].Trim().ToLower());
        mail.From = new MailAddress(from, txtFirstName.Text + " " + txtLastName.Text, System.Text.Encoding.UTF8);
        mail.Subject = txtSubject.Text;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = txtMessageBody.Text;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();

        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["creditinalEmail"].Trim().ToLower(), ConfigurationManager.AppSettings["creditinalPassword"].Trim().ToLower());
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        try
        {
            client.Send(mail);
            lblError.Text = "Съобщението е успешно изпратено.";
        }
        catch
        {
            lblError.Text = "Възникна проблем. Моля опитайте отново.";
        } 
    }
}