using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_FreeAccess_ShortSearch : ListPager
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

    public string keywords
    {
        get
        {
            if (ViewState["Keywords"] == null)
            {
                return null;
            }
            return ViewState["Keywords"] as string;
        }
        set
        {
            ViewState["Keywords"] = value;
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
        base.PageChanged += new EventHandler(Repeater_PageChanged);

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["word"] != null && Request.QueryString["word"] != "")
            {
                keywords = Util.DecodeFrom64(Request.QueryString["word"]);
                txtSearch.Text = keywords;
                DataBindLanguages();
                DataBindRepeaterSearch();

            }
            else
            {
                pnlEnterWord.Visible = true;
                DataBindLanguages();
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (txtSearch.Text != string.Empty &&  rptSearchResult.Items.Count == 0)
        {
            pnlNoFound.Visible = true;
            lblResultCount.Text = "Съвпадения: 0";
        }

        base.SetUIControls();
    }

    #region DataBind Methods

    protected void DataBindLanguages()
    {
        ddlLanguage.DataSource = entities.Languages;
        ddlLanguage.DataTextField = "Name";
        ddlLanguage.DataValueField = "Id";
        ddlLanguage.DataBind();
    }

    protected void DataBindRepeaterSearch()
    {
        pnlEnterWord.Visible = pnlNoFound.Visible = false;
        System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("total", typeof(int));

        if (txtSearch.Text != "")
        {
            keywords = txtSearch.Text;
        }

        int searchMethod = Int32.Parse(ddlSearchMethod.SelectedValue);
        int languageId = Int32.Parse(ddlLanguage.SelectedValue);

        rptSearchResult.DataSource = entities.GetSearchedWord(languageId, searchMethod, keywords, SortingColumn, SortingDirection == "sortingasc" ? "ASC" : "DESC", total, base.StartRow, base.PageSize);
        rptSearchResult.DataBind();

        if ((int)total.Value > 20)
        {
            pnlPagingTop.Visible = pnlPaigingBottom.Visible = true;
            base.TotalRows = (int)total.Value;
        }
        else
        {
            pnlPagingTop.Visible = pnlPaigingBottom.Visible = false;
        }
        lblResultCount.Visible = true;

        if (searchMethod == 2)
        {
            lblResultCount.Visible = false;
            pnlPagingTop.Visible = pnlPaigingBottom.Visible = false;
        }

        lblResultCount.Text = "Съвпадения: " + total.Value.ToString();
    }

    #endregion

    #region Buttons

    protected void lbtnTranslate_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text.Length >= 3)
        {
            DataBindRepeaterSearch();
        }
        else if (txtSearch.Text == string.Empty)
        {
            pnlEnterWord.Visible = true;
            rptSearchResult.DataSource = null;
            rptSearchResult.DataBind();
        }
    }

    protected void Repeater_PageChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text != string.Empty)
        {
            DataBindRepeaterSearch();
        }
    }

    #endregion

}