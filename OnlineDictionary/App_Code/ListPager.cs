using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ListPager
/// </summary>
public class ListPager : System.Web.UI.UserControl
{
    protected int TotalRows // array based row count start index = 0
    {
        get
        {
            if (ViewState["totalRows"] == null)
                ViewState["totalRows"] = 0;
            return Convert.ToInt32(ViewState["totalRows"]);
        }
        set { ViewState["totalRows"] = (value).ToString(); }
    }

    public int StartRow
    {
        get
        {
            if (ViewState["startRow"] == null)
                ViewState["startRow"] = 1;
            return Convert.ToInt32(ViewState["startRow"]);
        }
        set { ViewState["startRow"] = value.ToString(); }
    }

    public int PageSize
    {
        get
        {
            if (ViewState["pageSize"] == null)
                ViewState["pageSize"] = 10;
            return Convert.ToInt32(ViewState["pageSize"]);
        }
        set { ViewState["pageSize"] = value.ToString(); }
    }

    public event EventHandler PageChanged;



    protected LinkButton lbtnPrevious;
    protected LinkButton lbtnNext;
    protected TextBox txtCurrentPage;
    protected Label lblTotalItems;
    protected LinkButton lbtnPreviousBottom;
    protected LinkButton lbtnNextBottom;
    protected TextBox txtCurrentPageBottom;
    protected Label lblTotalItemsBottom;
    public ListPager()
    {

    }

    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        StartRow -= PageSize;
        if (StartRow < 1)
        {
            StartRow = 1;
        }
        FirePageChanged();
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        StartRow += PageSize;
        FirePageChanged();
    }

    protected void txtCurrentPage_TextChanged(object sender, EventArgs e)
    {
        string newPageTextNumber = ((TextBox)sender).Equals(txtCurrentPage) ? txtCurrentPage.Text : txtCurrentPageBottom.Text;

        int newPage = int.MinValue;
        if (Int32.TryParse(newPageTextNumber, out newPage))
        {
            if (newPage > 0)
            {
                if (GetTotalPages() >= newPage)
                {
                    StartRow = (newPage * PageSize) - PageSize + 1;
                }
                FirePageChanged();
            }

        }
    }


    private void FirePageChanged()
    {
        EventArgs args = new EventArgs();

        if (PageChanged != null)
        {
            PageChanged.Invoke(this, args);
        }
    }




    protected void Page_PreRender(object sender, EventArgs e)
    {
        SetUIControls();
    }

    public void SetUIControls()
    {
        if (txtCurrentPage != null)
        {
            if (TotalRows > 0)
            {
                if (lbtnPrevious != null)
                    lbtnPrevious.Visible = StartRow > 1;

                if (lbtnNext != null)
                    lbtnNext.Visible = StartRow < TotalRows - (PageSize - 1);
            }
            else
            {
                if (lbtnPrevious != null)
                    lbtnPrevious.Visible = false;


                if (lbtnNext != null)
                    lbtnNext.Visible = false;

                txtCurrentPage.Text = "0";
            }
            lblTotalItems.Text = GetTotalPages().ToString();
            txtCurrentPage.Text = TotalRows == 0 ? "0" : (((StartRow - 1) / PageSize) + 1).ToString();


            //bottom paging
            lbtnPreviousBottom.Visible = lbtnPrevious.Visible;
            lbtnNextBottom.Visible = lbtnNext.Visible;
            lblTotalItemsBottom.Text = lblTotalItems.Text;
            txtCurrentPageBottom.Text = txtCurrentPage.Text;
        }

    }

    private int GetTotalPages()
    {
        int totalPages = 0;
        if (TotalRows == 0)
        {
            return totalPages;
        }
        else
        {
            int remaining = TotalRows % PageSize;

            totalPages = (TotalRows - remaining) / PageSize;

            if (remaining > 0)
            {
                totalPages++;
            }
            return totalPages;
        }
    }
}
