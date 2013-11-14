using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.IO;
using System.Text;

public partial class Modules_RestrictedAccess_Tests : System.Web.UI.UserControl
{
    #region Variables

    protected string testName
    {
        get
        {
            return ViewState["testName"] != null ? ViewState["testName"] as string : string.Empty;
        }
        set
        {
            ViewState["testName"] = value;
        }
    }

    public int controlsCount
    {
        get
        {
            return Convert.ToInt32(ViewState["controlsCount"]);
        }
        set
        {
            ViewState["controlsCount"] = value.ToString();
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataBindTests();
        }
    }

    protected void DataBindTests()
    {
        DataTable tblTests = new DataTable();
        tblTests.Columns.Add("name", typeof(string));
        tblTests.Columns.Add("level", typeof(string));
        string xmlFilePath = Server.MapPath("~/App_Data/Tests.xml");
        XDocument xDoc = XDocument.Load(xmlFilePath);
        foreach (XElement xTest in xDoc.Root.Elements())
        {
            string name = xTest.Element("title").Value;
            string level = xTest.Element("level").Value;
            tblTests.Rows.Add(name, level);
        }

        rptTests.DataSource = tblTests;
        rptTests.DataBind();
    }

    protected void rptTests_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Start")
        {
            pnlGenerelTests.Visible = false;
            pnlShowTest.Visible = pnlQuestions.Visible = true;
            testName = e.CommandArgument.ToString();
            BindAllControls();
        }
    }


    protected void BindAllControls()
    {
        string testPath = Server.MapPath("~/App_Data/Tests.xml");
        XDocument xDocTests = XDocument.Load(testPath);

        XElement elementsOfTest = xDocTests.Root.Elements().FirstOrDefault(el => el.Element("title").Value == testName);

        foreach (XElement element in elementsOfTest.Elements())
        {
            if (element.Name == "title")
            {
                lblTitle.Text = element.Value;
            }
            else if (element.Name == "level")
            {
                lblLevel.Text = element.Value;
            }
            else if (element.Name == "description")
            {
                lblDescription.Text = element.Value;
            }
            else if (element.Name == "exmaple")
            {
                lblExample.Text = "Пример: " + element.Value;
            }
            else if (element.Name == "questions")
            {
                string[] allQuestions = element.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                controlsCount = allQuestions.Length;

                for (int i = 0; i < allQuestions.Length; i++)
                {
                    string[] text = allQuestions[i].Replace("[]", "|[]|").Split(new string[] { "|" }, StringSplitOptions.None);

                    for (int j = 0; j < text.Length; j++)
                    {
                        if (text[j] == "[]")
                        {
                            TextBox txt = new TextBox();
                            txt.ID = "txtSearch" + i;
                            txt.CssClass = "jsCheckTest margintop10";
                            txt.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            this.pnlShowTest.Controls.Add(txt);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Text = text[j];
                            lbl.CssClass = "label margintop10";
                            this.pnlShowTest.Controls.Add(lbl);
                        }
                    }

                    Literal litNewLine = new Literal();
                    litNewLine.Text = "<br />";
                    this.pnlShowTest.Controls.Add(litNewLine);
                }
            }
        }
    }

    protected void lbtnCheck_Click(object sender, EventArgs e)
    {
        string testPath = Server.MapPath("~/App_Data/Tests.xml");
        XDocument xDocTests = XDocument.Load(testPath);

        XElement elementsOfTest = xDocTests.Root.Elements().FirstOrDefault(el => el.Element("title").Value == testName);

        string[] questions = elementsOfTest.Element("questions").Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string [] correctAnswers = elementsOfTest.Element("answer").Value.Split(new string [] {"|"},StringSplitOptions.RemoveEmptyEntries);

        string [] answers = txtAnswers.Text.Replace("$","$|").Split(new string[] { "$" }, StringSplitOptions.RemoveEmptyEntries);


        int count = 0;
        int negativeCount = 0;
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (correctAnswers[i].ToLower().Trim() == answers[i].Replace("|","").ToLower().Trim())
            {
                count++;
                lblErrorAnswers.Text += "<span class='label' >" + questions[i].Replace("[]", "<span class=''>..." + answers[i].Replace("|", "") + "...</span>") + "</span><br />";
            }
            else 
            {
                negativeCount++;
                lblErrorAnswers.Text += "<span class='label' >" + questions[i].Replace("[]", "<span class='reviewGuest'>..." + answers[i].Replace("|","") + "...</span>") + "</span><br />";
            }
        }
        pnlShowResult.Visible = true;
        lblTestResult.Text = "Правилни отговори: " + count.ToString() + "<br />";
        lblTestResult.Text += "Грешни отговори: " + negativeCount.ToString() + "";
    }

    protected void lbtnShowError_Click(object sender, EventArgs e)
    {
        pnlErrorAnswers.Visible = true;
    }

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        pnlGenerelTests.Visible = true;
        pnlShowTest.Visible = false;
    }

    protected void lbtnCloseResult_Click(object sender, EventArgs e)
    {
        pnlShowResult.Visible = pnlQuestions.Visible = pnlShowTest.Visible = false;
        pnlGenerelTests.Visible = true;
        lblErrorAnswers.Text = null;
        pnlErrorAnswers.Visible = false;
    }


}