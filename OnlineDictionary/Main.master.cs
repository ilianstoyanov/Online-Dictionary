using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool showImage;
            imgSpendTime.ImageUrl = (Page as BasePage).SetUserTracking(out showImage);
            imgSpendTime.Visible = showImage;

            MembershipUser current = (this.Page as BasePage).user;
            if (current != null)
            {
                pnlLoginButton.Visible = false;
                pnlUserStatus.Visible = true;
                lblUserNames.Text += (Page as BasePage).CurrentUserNames;
            }

            Dictionary<string, string> menuItemElements = (Page as BasePage).MenuItems;

            if (menuItemElements.Count >= 8)
            {
                hLinkHome.Visible = true;
                rptMenuMain.DataSource = menuItemElements.Where(el => el.Value.ToLower() != "writeareview.aspx".ToLower() && el.Value.ToLower() != "registration.aspx".ToLower() && el.Value.ToLower() != "Default.aspx".ToLower());
            }
            else
            rptMenuMain.DataSource = menuItemElements.Where(page => page.Value.ToString().ToLower() != "writeareview.aspx".ToLower() && page.Value.ToString().ToLower() != "registration.aspx".ToLower());
            
            rptMenuMain.DataBind();
        }
    }

    protected void rptMenuMain_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //HyperLink link = e.Item.FindControl("hLinkMenu") as HyperLink;

        //if (link != null && "/" + link.NavigateUrl.ToLower() == Request.RawUrl.ToLower())
        //{
        //    HtmlGenericControl ctrl = e.Item.FindControl("btnMenuContainer") as HtmlGenericControl;
        //    ctrl.Attributes.Add("class", "static selected");
        //}
    }


    protected void LoginStatusMain_LoggedOut(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect(ResolveUrl("/"));
    }

    protected void lbtnLogin_Click(object sender, EventArgs e)
    {
        //lbtnLogin.Visible = false;
        //pnlLogin.Visible = true;
        //ucLogin.Visible = true;
        //ucCreateNewAccount.Visible = false;
    }

    protected void lbtnCreateAccount_Click(object sender, EventArgs e)
    {
       // ucLogin.Visible = true;
        //ucCreateNewAccount.Visible = true;
    }

    protected void lbtnClose_Click(object sender, EventArgs e)
    {
    //    lbtnLogin.Visible = true;
    //    pnlLogin.Visible = false;
    //    ucLogin.Visible = false;
    //    ucCreateNewAccount.Visible = false;
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtSearchTaskMasterPage.Text.Length >= 3)
        {
            string test = "/AdvancedSearch.aspx?word=" + Util.EncodeTo64(txtSearchTaskMasterPage.Text);//DELETE ME !
            Response.Redirect("/AdvancedSearch.aspx?word=" + Util.EncodeTo64(txtSearchTaskMasterPage.Text));
        }
    }

}
