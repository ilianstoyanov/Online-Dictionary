using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AdminModules_AllUsers : ListPager
{
    #region Variables

    DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();

    public string SortingColumn
    {
        get
        {
            if (ViewState["SortingColumn"] == null)
            {
                return "CreateDate";
            }
            return ViewState["SortingColumn"] as string;
        }
        set
        {
            ViewState["SortingColumn"] = value;
        }
    }

    public string SortingDirection
    {
        get
        {
            if (ViewState["SortingDirection"] == null)
            {
                return "sortingasc";
            }
            return ViewState["SortingDirection"] as string;
        }
        set
        {
            ViewState["SortingDirection"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        base.lbtnPrevious = lbtnPrevious;
        base.lbtnNext = lbtnNext;
        base.txtCurrentPage = txtCurrentPage;
        base.lblTotalItems = lblTotalItems;

        //bottom paging
        base.lbtnPreviousBottom = lbtnPreviousBottom;
        base.lbtnNextBottom = lbtnNextBottom;
        base.txtCurrentPageBottom = txtCurrentPageBottom;
        base.lblTotalItemsBottom = lblTotalItemsBottom;

        base.PageSize = 10;
        base.PageChanged += new EventHandler(GridPageChanged);

        if (!Page.IsPostBack)
        {
            DataBindUsers();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (GridViewExistingUsers.Rows.Count == 0)
        {
            DataBindUsers();
        }

        base.SetUIControls();
    }

    protected void DataBindUsers()
    {
        System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("total", typeof(int));
        string keywords = null;
        if (txtSearch.Text.Trim() != string.Empty && txtSearch.Text.Trim().ToLower() != "Търсене")
        {
            keywords = txtSearch.Text;
        }

        GridViewExistingUsers.DataSource = entities.GetAllUsers(keywords, SortingColumn, SortingDirection == "sortingasc" ? "ASC" : "DESC", base.StartRow, base.PageSize, total);
        GridViewExistingUsers.DataBind();

        base.TotalRows = (int)total.Value;
        lblTotalUsers.Text = "Брой: " + total.Value.ToString();
    }

    protected void GridViewExistingUsers_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sorting")
        {
            LinkButton btn = e.CommandSource as LinkButton;
            if (btn.CommandArgument != SortingColumn)
            {
                SortingDirection = "sortingasc";
            }
            else
            {
                SortingDirection = SortingDirection == "sortingasc" ? "sortingdesc" : "sortingasc";
            }
            SortingColumn = btn.CommandArgument;
            DataBindUsers();
        }
        else if (e.CommandName == "BlockUser" || e.CommandName == "UnBlockUser")
        {
            Guid currentUserASPNETID = Guid.Parse(e.CommandArgument.ToString());

            DictionaryModel.aspnet_Membership currentUser = entities.aspnet_Membership.FirstOrDefault(user => user.UserId == currentUserASPNETID);
            if (currentUser != null)
            {
                if (currentUser.IsApproved)
                {
                    currentUser.IsApproved = false;
                }
                else currentUser.IsApproved = true;

                entities.SaveChanges();
                DataBindUsers();
            }
        }
    }

    protected void GridViewExistingUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnUserASPNETID = (HiddenField)e.Row.FindControl("hdnUserASPNETID");
            LinkButton lbtnBlockUser = (LinkButton)e.Row.FindControl("lbtnBlockUser");
            LinkButton lbtnUnBlockUser = (LinkButton)e.Row.FindControl("lbtnUnBlockUser");

            Guid currentASPNETID = Guid.Parse(hdnUserASPNETID.Value.ToString());
            DictionaryModel.aspnet_Membership userIsApprove = entities.aspnet_Membership.FirstOrDefault(user => user.UserId == currentASPNETID && user.IsApproved == false);

            if (userIsApprove != null)
            {
                lbtnUnBlockUser.Visible = true;
                lbtnBlockUser.Visible = false;
            }
            else
            {
                lbtnUnBlockUser.Visible = false;
                lbtnBlockUser.Visible = true;
            }
        }
    }

    protected void GridPageChanged(object sender, EventArgs e)
    {
        DataBindUsers();
    }

    protected void txtSearch_Click(object sender, EventArgs e)
    {
        DataBindUsers();
    }
}