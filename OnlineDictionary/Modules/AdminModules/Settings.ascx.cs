using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AdminModules_Settings : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Buttons

    protected void lbtnAllUsers_Click(object sender, EventArgs e)
    {
        pnlOptions.Visible = spTitle.Visible = false;
        pnlAllUsers.Visible = lbtnBack.Visible = true;
    }

    protected void lbtnReviews_Click(object sender, EventArgs e)
    {
        pnlOptions.Visible = spTitle.Visible = false;
        pnlReviews.Visible = lbtnBack.Visible = true;
    }

    protected void lbtnLibrary_Click(object sender, EventArgs e)
    {
        pnlOptions.Visible = spTitle.Visible = false;
        pnlLibrary.Visible = lbtnBack.Visible = true;
    }

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        Panel pnlEditWord = (Panel)ucLibrary.FindControl("pnlEditWord");
        Panel pnlExistingsWords = (Panel)ucLibrary.FindControl("pnlExistingsWords");
        HiddenField hdnCurrentWordId = (HiddenField)ucLibrary.FindControl("hdnCurrentWordId");

        hdnCurrentWordId.Value = null;
        pnlEditWord.Visible = false;
        pnlExistingsWords.Visible = true;
        pnlReviews.Visible = pnlAllUsers.Visible = pnlLibrary.Visible = lbtnBack.Visible = false;
        pnlOptions.Visible = spTitle.Visible = true;
    }

    #endregion
}