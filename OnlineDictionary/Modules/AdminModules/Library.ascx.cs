using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AdminModules_Library : ListPager
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

        base.PageSize = 50;
        base.PageChanged += new EventHandler(GridPageChanged);

        if (!Page.IsPostBack)
        {
            DataBindGridViewAllWords();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (GridViewAllWords.Rows.Count == 0)
        {
            DataBindGridViewAllWords();
        }

        base.SetUIControls();
    }

    #region GridViewAllWords

    protected void DataBindGridViewAllWords()
    {
        lblResult.Text = null;
        System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("total", typeof(int));

        string keywords = null;
        if (txtSearch.Text != "")
        {
            keywords = txtSearch.Text;
        }
        int language = Int32.Parse(ddlLanguage.SelectedValue);
        int searchMethod = Int32.Parse(ddlSearchMethod.SelectedValue);
        List<DictionaryModel.GetSearchedWord_Result> ddd = entities.GetSearchedWord(language, searchMethod, keywords, SortingColumn, SortingDirection == "sortingasc" ? "ASC" : "DESC", total, base.StartRow, base.PageSize).ToList();
        GridViewAllWords.DataSource = entities.GetSearchedWord(language, searchMethod, keywords, SortingColumn, SortingDirection == "sortingasc" ? "ASC" : "DESC", total, base.StartRow, base.PageSize);
        GridViewAllWords.DataBind();

        if ((int)total.Value == 0)
        {
            pnlNoFound.Visible = true;
        }
        else pnlNoFound.Visible = false;

        base.TotalRows = (int)total.Value;
        lblTotalWords.Text = "Брой: " + total.Value.ToString();
    }

    protected void GridViewAllWords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int wordId;
        if (Int32.TryParse(e.CommandArgument.ToString(), out wordId))
        {
            if (e.CommandName == "EditWord")
            {
                pnlExistingsWords.Visible = false;
                pnlEditWord.Visible = true;

                if (ddlLanguage.SelectedValue == "1")
                {   
                    DictionaryModel.English_Bulgarian word = entities.English_Bulgarian.FirstOrDefault(w => w.Id == wordId);
                    txtWord.Text = word.Word;
                    txtТranscription.Text = word.Тranscription == null ? string.Empty : word.Тranscription.Replace("]", "").Replace("[", "");
                    txtDescription.Text = word.Description;
                    hdnCurrentWordId.Value = wordId.ToString();
                }
                else if (ddlLanguage.SelectedValue == "2")
                {
                    DictionaryModel.Bulgarian_English word = entities.Bulgarian_English.FirstOrDefault(w => w.Id == wordId);
                    txtWord.Text = word.Word;
                    txtТranscription.Text = word.Тranscription == null ? string.Empty : word.Тranscription.Replace("]", "").Replace("[", "");
                    txtDescription.Text = word.Description;
                    hdnCurrentWordId.Value = wordId.ToString();
                }
            }
            else if (e.CommandName == "DeleteWord")
            {
                if (ddlLanguage.SelectedValue == "1")
                {
                    DictionaryModel.English_Bulgarian word = entities.English_Bulgarian.FirstOrDefault(w => w.Id == wordId);
                    entities.DeleteObject(word);
                }
                else if (ddlLanguage.SelectedValue == "2")
                {
                    DictionaryModel.Bulgarian_English word = entities.Bulgarian_English.FirstOrDefault(w => w.Id == wordId);
                    entities.DeleteObject(word);
                }
                
                
                entities.SaveChanges();
                DataBindGridViewAllWords();
                lblResult.Text = "Думата е изтрита успешно.";
            }
        }
    }

    #endregion

    #region Buttons

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        DataBindGridViewAllWords();
    }

    protected void lbtnAddNewWord_Click(object sender, EventArgs e)
    {
        pnlEditWord.Visible = true;
        pnlExistingsWords.Visible = false;
        hdnCurrentWordId.Value = null;
    }

    protected void lbtnCancelChanges_Click(object sender, EventArgs e)
    {
        ClearAllControls();
    }

    protected void lbtnSaveChanges_Click(object sender, EventArgs e)
    {
        int wordId;
        if (Page.IsValid && Int32.TryParse(hdnCurrentWordId.Value.ToString(), out wordId)) // Edit word
        {
            if (ddlLanguage.SelectedValue == "1")
            {
                DictionaryModel.English_Bulgarian word = entities.English_Bulgarian.FirstOrDefault(w => w.Id == wordId);
                word.Word = txtWord.Text;
                word.Тranscription = "[" + txtТranscription.Text + "]";
                word.Description = txtDescription.Text;
            }
            else if (ddlLanguage.SelectedValue == "2")
            {
                DictionaryModel.Bulgarian_English word = entities.Bulgarian_English.FirstOrDefault(w => w.Id == wordId);
                word.Word = txtWord.Text;
                word.Тranscription = txtТranscription.Text;// +"]";
                word.Description = txtDescription.Text;
            }
            entities.SaveChanges();

            ClearAllControls();
            lblResult.Text = "Думата е променена успешно.";

        }

        else // Add new Word
        {
            if (ddlLanguage.SelectedValue == "1")
            {
                DictionaryModel.English_Bulgarian word = new DictionaryModel.English_Bulgarian();
                word.Word = txtWord.Text;
                word.Тranscription = "[" + txtТranscription.Text + "]";
                word.Description = txtDescription.Text;

                entities.English_Bulgarian.AddObject(word);
            }
            else if (ddlLanguage.SelectedValue == "2")
            {
                DictionaryModel.Bulgarian_English word = new DictionaryModel.Bulgarian_English();
                word.Word = txtWord.Text;
                //word.Тranscription = "[" + txtТranscription.Text + "]";
                word.Description = txtDescription.Text;

                entities.Bulgarian_English.AddObject(word);
            }
            entities.SaveChanges();

            ClearAllControls();
            lblResult.Text = "Думата е добавена успешно.";
            pnlEditWord.Visible = true;
            pnlExistingsWords.Visible = false;
            lbtnCancelChanges.Text = "Назад";
        }
    }

    protected void ddlSearch_selectedIndexChanged(object sender, EventArgs e)
    {
        txtSearch.Text = null;
        DataBindGridViewAllWords();
    }

    protected void GridPageChanged(object sender, EventArgs e)
    {
        DataBindGridViewAllWords();
    }
    #endregion

    #region Helpers Methods

    protected void ClearAllControls()
    {
        txtDescription.Text = null;
        txtWord.Text = null;
        txtТranscription.Text = null;
        hdnCurrentWordId.Value = null;
        pnlEditWord.Visible = false;
        pnlExistingsWords.Visible = true;

        DataBindGridViewAllWords();
    }

    #endregion
}