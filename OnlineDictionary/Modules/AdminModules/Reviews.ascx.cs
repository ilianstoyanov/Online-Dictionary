using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.IO;

public partial class Modules_AdminModules_Reviews : System.Web.UI.UserControl
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
        tblFiles.Columns.Add("date", typeof(string));
        tblFiles.Columns.Add("firstName", typeof(string));
        tblFiles.Columns.Add("lastName", typeof(string));
        tblFiles.Columns.Add("published", typeof(bool));
        tblFiles.Columns.Add("rating", typeof(string));
        tblFiles.Columns.Add("personalReview", typeof(string));
        string xmlFilePath = Path.Combine(Server.MapPath("~/"), @"App_Data\testimonials.xml");
        XDocument xDoc = XDocument.Load(xmlFilePath);
        foreach (XElement xFile in xDoc.Root.Elements())
        {
            string date = xFile.Attribute("date").Value;
            string firstName = xFile.Attribute("firstName").Value;
            string lastName = xFile.Attribute("lname").Value;
            string rating = xFile.Attribute("rating").Value;
            bool published = bool.Parse(xFile.Attribute("published").Value);
            string personalReview = xFile.Attribute("personalReview").Value;
            tblFiles.Rows.Add(date, firstName, lastName, published, rating, personalReview);
        }

        rptExistingReview.DataSource = tblFiles;
        rptExistingReview.DataBind();
    }

    protected void chkPublished_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkPublished = (CheckBox)sender;

        string xmlFilePath = Path.Combine(Server.MapPath("~/"), @"App_Data\testimonials.xml");
        XDocument xDoc = XDocument.Load(xmlFilePath);
        string reviewDate = chkPublished.ToolTip.ToString();
        XElement xReview = xDoc.Root.Elements().FirstOrDefault(x => x.Attribute("date").Value == reviewDate);

        if (xReview.Attribute("published").Value == "true")
        {
            xReview.Attribute("published").Value = "false";
        }
        else xReview.Attribute("published").Value = "true";

        xDoc.Save(Server.MapPath("~/App_Data/testimonials.xml"));
    }

    protected void lbtnDelete_Click(object sender, CommandEventArgs e)
    {
        LinkButton lbtnDelete = (LinkButton)sender;

        string xmlFilePath = Path.Combine(Server.MapPath("~/"), @"App_Data\testimonials.xml");
        XDocument xDoc = XDocument.Load(xmlFilePath);

        xDoc.Root.Elements().FirstOrDefault(x => x.Attribute("date").Value == e.CommandArgument.ToString()).Remove();
        xDoc.Save(xmlFilePath);
       
        DataBindReview();
    }

}