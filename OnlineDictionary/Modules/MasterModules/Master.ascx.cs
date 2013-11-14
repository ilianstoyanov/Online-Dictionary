using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class Modules_Master_Master : ListPager
{
    #region Variables

    DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();

    public string SortingColumn
    {
        get
        {
            if (ViewState["SortingColumn"] == null)
            {
                return "FirstName";
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
        DataBindAdmins();
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
            txtStartDate.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
            DataBindGridViewSpendTime();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (GridViewSpendTime.Rows.Count == 0)
        {
            DataBindGridViewSpendTime();
        }

        base.SetUIControls();
    }

    #region Spend Time

    protected void DataBindGridViewSpendTime()
    {
        System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("total", typeof(int));
        string keywords = null;
        if (txtSearch.Text.Trim() != string.Empty && txtSearch.Text.Trim().ToLower() != "search")
        {
            keywords = txtSearch.Text;
        }

        DateTime startDate = DateTime.Parse(txtStartDate.Text);
        DateTime endDate = DateTime.Parse(txtEndDate.Text);

        GridViewSpendTime.DataSource = entities.GetSpendTime(keywords, total, base.StartRow, base.PageSize, startDate, endDate);
        GridViewSpendTime.DataBind();

        base.TotalRows = (int)total.Value;
    }

    protected void rptAdmins_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "RemoveFromAdmin")
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Roles.RemoveUserFromRole(e.CommandArgument.ToString(), "Admin");
                Roles.AddUserToRole(e.CommandArgument.ToString(), "User");
                DataBindAdmins();
                Label lblAdminFirstName = (Label)e.Item.FindControl("lblAdminFirstName");
                Label lblAdminLastName = (Label)e.Item.FindControl("lblAdminLastName");
                lblMessage.Text = "Корекцията за " + lblAdminFirstName.Text + " " + lblAdminLastName.Text + " e направена успешно.";
                lbtnAddAdmin.Visible = true;
            }
        }
    }

    protected void GridPageChanged(object sender, EventArgs e)
    {
        DataBindGridViewSpendTime();
    }

    protected void txtSearch_Changed(object sender, EventArgs e)
    {
        DataBindGridViewSpendTime();
    }

    #endregion

    #region Admins

    protected void DataBindAdmins()
    {
        rptAdmins.DataSource = entities.GetAllAdmins();
        rptAdmins.DataBind();
    }

    #region Admin Buttons

    protected void lbtnShowAddAdminForm_Click(object sender, EventArgs e)
    {
        pnlAddAdminForm.Visible = true;
        lbtnShowAddAdminForm.Visible = false;
        lblMessage.Text = null;
        DataBindUsers();
    }

    protected void lbtnCancelAddAdmin_Click(object sender, EventArgs e)
    {
        lbtnShowAddAdminForm.Visible = true;
        ddlUsers.SelectedIndex = 0;
        pnlAddAdminForm.Visible = false;
    }

    protected void lbtnAddAdmin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string[] users = Roles.GetUsersInRole("User");
            foreach (string user in users)
            {
                if (user == ddlUsers.SelectedValue)
                {
                    Roles.RemoveUserFromRole(ddlUsers.SelectedValue, "User");
                    Roles.AddUserToRole(ddlUsers.SelectedValue, "Admin");
                }
            }

            pnlAddAdminForm.Visible = false;
            lbtnShowAddAdminForm.Visible = true;
            DataBindAdmins();
            lblMessage.Text = ddlUsers.SelectedItem.Text + " e добавен като администратор.";
            ddlUsers.SelectedIndex = 0;
        }
    }

    #endregion

    #region Helpers Methods

    protected void DataBindUsers()
    {
        System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("total", typeof(int));

        var result = entities.GetAllUsers(null, "FirstName", "ASC", 1, 100000, total);

        DataTable allUsers = new DataTable();
        allUsers.Columns.Add("Name", typeof(string));
        allUsers.Columns.Add("currentUserName", typeof(string));
        foreach (var user in result)
        {
            string name = user.FirstName + " " + user.LastName;
            string currentUserName = user.currentUserName.ToString();
            allUsers.Rows.Add(name, currentUserName);
        }

        ddlUsers.DataSource = allUsers;
        ddlUsers.DataTextField = "Name";
        ddlUsers.DataValueField = "currentUserName";
        ddlUsers.DataBind();
    }

    #endregion

    #endregion

    #region General Buttons

    protected void lbtnUsersSpendTime_Click(object sender, EventArgs e)
    {
        pnlOption.Visible = false;
        pnlShowSpendTime.Visible = true;
    }

    protected void lbtnShowAllAdmins_Click(object sender, EventArgs e)
    {
        pnlOption.Visible = false;
        pnlAllAdmins.Visible = lbtnShowAddAdminForm.Visible = true;
        lblMessage.Text = null;
        DataBindAdmins();
    }

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        pnlAllAdmins.Visible = pnlShowSpendTime.Visible = pnlAddAdminForm.Visible = false;
        pnlOption.Visible = true;
    }

    #endregion
}