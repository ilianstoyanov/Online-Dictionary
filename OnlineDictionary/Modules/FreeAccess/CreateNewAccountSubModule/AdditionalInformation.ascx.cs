using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Modules_FreeAccess_CreateNewAccountSubModule_AdditionalInformation : System.Web.UI.UserControl
{
    #region Variables

    DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();
    public event NewAccountStepCompletedEventHandler StepCompleted;

    public Guid SetupASPNETID
    {
        get
        {
            return new Guid(ViewState["SetupASPNETID"].ToString());
        }
        set
        {
            ViewState["SetupASPNETID"] = value.ToString();
        }
    }

    public int SetupAccountId
    {
        get
        {
            return Convert.ToInt32(ViewState["SetupAccountId"]);
        }
        set
        {
            ViewState["SetupAccountId"] = value.ToString();
        }
    }

    public int SetupCountryId
    {
        get
        {

            return Convert.ToInt32(ViewState["SetupCountryId"]);
        }
        set
        {
            ViewState["SetupCountryId"] = value.ToString();
        }
    }

    protected int SetupAreaId;
    protected int SetupCityId;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!Page.IsPostBack)
        {
            SetupAreaId = SetupCountryId = 27;
            DataBindArea();
        }
    }

    public void LoadDetails()
    {
        DataBindArea();
    }

    #region Buttons

    protected void DataBindArea()
    {
        ddlArea.DataSource = entities.Areas.Where(country => country.CountryId == SetupCountryId);
        ddlArea.DataTextField = "Name";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
    }

    protected void DataBindCity()
    {
        ddlCity.DataSource = entities.Cities.Where(area => area.AreaId == SetupAreaId);
        ddlCity.DataTextField = "Name";
        ddlCity.DataValueField = "Id";
        ddlCity.DataBind();
    }

    protected void DataBindVillage()
    {
        ddlVillage.DataSource = entities.Villages.Where(city => city.CityId == SetupCityId);
        ddlVillage.DataTextField = "Name";
        ddlVillage.DataValueField = "Id";
        ddlVillage.DataBind();
    }

    protected void lbtnClear_Click(object sender, EventArgs e)
    {
        ddlArea.SelectedValue = null;
        ddlCity.SelectedValue = null;
        ddlVillage.SelectedValue = null;
        txtAddress.Text = null;
        txtPhone.Text = null;
        txtNotes.Text = null;
    }

    protected void lbtnFinishStep_Click(object sender, EventArgs e)
    {
        try
        {
            Guid setASPNETID = (Guid)Session["SetupASPNETID"];
            int setAccId = Int32.Parse(Session["SetupAcountId"].ToString());
            DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.ASPNETID == setASPNETID);

            if (ddlArea.SelectedValue != "0")
            {
                account.Area = Int32.Parse(ddlArea.SelectedValue);
                if (ddlCity.SelectedValue != "0")
                {
                    account.City = Int32.Parse(ddlCity.SelectedValue);
                    if (ddlVillage.SelectedValue != "0") account.Village = Int32.Parse(ddlVillage.SelectedValue); 
                }
            }

            int phone;
            if (Int32.TryParse(txtPhone.Text, out phone)) account.Phone = phone;

            account.Address = txtAddress.Text;
            account.Notes = txtNotes.Text;
            
            if (fuSource.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuSource.PostedFile.FileName);
                if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".bmp")
                {
                    if ((fuSource.PostedFile.ContentLength > 0) && (fuSource.PostedFile.ContentLength < 10000000))
                    {
                        string currentTime = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                        string fileName = fuSource.PostedFile.FileName;
                        string fileDirectory = Server.MapPath("~/Files/FileUpload");
                        string SaveLocation = fileDirectory + "\\" + currentTime + fileName;
                        //string displayedImgThumb = Server.MapPath("~/Files/FileUpload") + "/Thumb/";
                        if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

                        try
                        {
                            fuSource.PostedFile.SaveAs(SaveLocation);
                            //System.Drawing.Image myimg = System.Drawing.Image.FromFile(SaveLocation);
                            //myimg = myimg.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                            //myimg.Save(displayedImgThumb + SaveLocation, myimg.RawFormat);
                            account.Picture = currentTime + fileName;
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "Error: " + ex.Message;
                        }
                    }
                    else lblError.Text = "Файла е прекалено голям."; 
                }
            }
            
            entities.SaveChanges();

            if (StepCompleted != null)
            {
                NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.AdditionalInformation);

                args.ASPNETID = setASPNETID;
                args.CurrentAccountId = setAccId;

                StepCompleted.Invoke(this, args);
            }
        }
        catch
        {
            lblError.Text = "Възникна проблем, моля опитайте по-късно.";
        }
    }

    #endregion

    protected void ddlArea_SelectedIndedChanged(object sender, EventArgs e)
    {
        if (ddlArea.SelectedValue != "0")
        {
            SetupAreaId = Int32.Parse(ddlArea.SelectedValue);
            ddlCity.Enabled = true;
            DataBindCity();
            ddlCity.SelectedValue = "0";
            ddlVillage.SelectedValue = "0";
            ddlVillage.Enabled = false;
        }
        else
        {
            ddlCity.SelectedValue = "0";
            ddlCity.Items.Clear();
            ddlCity.SelectedValue = "0";
            ddlCity.Enabled = false;
            ddlVillage.SelectedValue = "0";
            
            ddlVillage.Enabled = false;
        }
    }

    protected void ddlCity_SelectedIndedChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue != "0")
        {
            SetupCityId = Int32.Parse(ddlCity.SelectedValue);
            ddlVillage.Enabled = true;
            DataBindVillage();
            ddlVillage.SelectedValue = "0";
            //ddlVillage.Items.Clear();

        }
        else
        {
            ddlVillage.SelectedValue = "0";
            ddlVillage.Items.Clear();
            ddlVillage.Enabled = false;
        }
    }

    #region FileUpload

    #endregion
}