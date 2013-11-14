using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Web.Security;

public partial class Modules_RestrictedAccess_WriteAReview : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataBindReview();
        }
    }

    protected void DataBindReview()
    {
        DataTable tblFiles = new DataTable();
        tblFiles.Columns.Add("firstName", typeof(string));
        tblFiles.Columns.Add("lastName", typeof(string));
        tblFiles.Columns.Add("rating", typeof(string));
        tblFiles.Columns.Add("personalReview", typeof(string));
        string xmlFilePath = Path.Combine(Server.MapPath("~/"), @"App_Data\testimonials.xml");
        XDocument xDoc = XDocument.Load(xmlFilePath);
        foreach (XElement xFile in xDoc.Root.Elements())
        {
            if (xFile.Attribute("published").Value == "true")
            {
                string firstName = xFile.Attribute("firstName").Value;
                string lastName = xFile.Attribute("lname").Value;
                string rating = xFile.Attribute("rating").Value;
                string personalReview = xFile.Attribute("personalReview").Value;
                tblFiles.Rows.Add(firstName, lastName, rating, personalReview);
            }
        }

        rptExistingReview.DataSource = tblFiles;
        rptExistingReview.DataBind();
    }

    protected void lbtnWriteAReview_Click(object sender, EventArgs e)
    {
            pnlExistingReview.Visible = lbtnWriteAReview.Visible = false;
            pnlWriteAReview.Visible = pnlWriteAReviewInfo.Visible = true;
            ScriptManager.RegisterStartupScript(UpdatePanelReview, GetType(), "runStars", "SelectStar();", true);
    }

    protected void lbtnCancelWriteAReview_Click(object sender, EventArgs e)
    {
        pnlWriteAReview.Visible = false;
        pnlExistingReview.Visible = lbtnWriteAReview.Visible = true;
    }

    protected void rev_btnSubmit_Click(object sender, EventArgs e)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Server.MapPath("~/App_Data/testimonials.xml"));
        XmlElement xElem = xDoc.CreateElement("testimonial");

        xElem.SetAttribute("date", DateTime.Now.ToString());
        xElem.SetAttribute("firstName", (txtFirstName.Text == "Име") ? String.Empty : txtFirstName.Text);
        xElem.SetAttribute("lname", (txtLastName.Text == "Фамилия") ? String.Empty : txtLastName.Text);
        xElem.SetAttribute("email", txtEmail.Text);

        string published = "false";

        xElem.SetAttribute("published", published);
        xElem.SetAttribute("rating", rbtnlRating.SelectedItem.Value);


        xElem.SetAttribute("personalReview", txtBoxReview.Text.Trim());
        xElem.InnerText = txtBoxReview.Text;

        string usermessage = "Вашето мнение е изпратено успешно.";

        xDoc.DocumentElement.AppendChild(xElem);
        bool reviewSuccess = false;
        try
        {
            btnSubmit.Enabled = false;
            xDoc.Save(Server.MapPath("~/App_Data/testimonials.xml"));
            pnlMessage.Visible = true;
            litMessage.Text = usermessage;
            pnlMessage.CssClass = "textbox center success";
            btnSubmit.Visible = false;
            reviewSuccess = true;
        }
        catch (Exception)
        {
            btnSubmit.Enabled = true;
            pnlMessage.Visible = true;
            litMessage.Text = "Възникна проблем.<br />Моля опитайте отново.";
            reviewSuccess = false;
        }
        // After review popup
        decimal rating = 0;
        bool getRating = Decimal.TryParse(rbtnlRating.SelectedItem.Value, out rating);

        pnlRating.Visible = true;
    }

    protected void lbtnClose_click(object sender, EventArgs e)
    {
        pnlRating.Visible = pnlLeaveReview.Visible = pnlWriteAReviewInfo.Visible = lbtnCancelWriteAReview.Visible = false;
        pnlExistingReview.Visible = true;
        DataBindReview();
    }

}