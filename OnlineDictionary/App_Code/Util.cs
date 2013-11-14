using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }

    static public string DecodeFrom64(string encodedData)
    {
        byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
        string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        return returnValue;
    }

    public static string Md5Hash(string pass)
    {
        MD5 md5 = MD5CryptoServiceProvider.Create();
        byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(pass));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dataMd5.Length; i++)
            sb.AppendFormat("{0:x2}", dataMd5[i]);
        return sb.ToString();
    }

    public static bool SendMail(string to, string from, string subject, string message, bool isHTML)
    {
        MailMessage msg = new MailMessage();
        msg.To.Add(to);
        if (from != null)
        {
            msg.From = new MailAddress(from);
        }

        msg.Subject = subject;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = message;
        msg.IsBodyHtml = isHTML;
        var smtp = new SmtpClient();
        try
        {
            smtp.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            try
            {
                System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/Logs/EmailSent.txt"), "Error: " + DateTime.Now.ToString() + " " + ex.ToString() + Environment.NewLine);
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}