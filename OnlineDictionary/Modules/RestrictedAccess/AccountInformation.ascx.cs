using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Modules_RestrictedAccess_AccountInformation : System.Web.UI.UserControl
{
    #region Variables

    public DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!Page.IsPostBack)
        {
            DataBindNames();
            DataBindAddress();
        }
    }

    #region DataBind

    protected void DataBindNames()
    {
        int accountId = (Page as BasePage).CurrentAccountId;
        DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == accountId);

        txtFirstName.Text = account.FirstName;
        txtFirstName.Enabled = false;
        txtLastName.Text = account.LastName;
        txtLastName.Enabled = false;
        lbtnChangeNames.Visible = true;
        lbtnCancelNames.Visible = false;
        lbtnSaveNames.Visible = false;
        imgPersonalPicture.ImageUrl = "~/Files/FileUpload/" + (account.Picture == null ? "Default.png" : account.Picture);
        imgPersonalPicture.ToolTip = account.FirstName + " " + account.LastName;
        imgPersonalPicture.Width = 400;
        imgPersonalPicture.Height = 300;
    }

    protected void DataBindAddress()
    {
        ddlCountry.Visible = ddlArea.Visible = ddlCity.Visible = ddlVillage.Visible = false;
        txtCountry.Enabled = txtArea.Enabled = txtCity.Enabled = txtVillage.Enabled = false;

        int accountId = (Page as BasePage).CurrentAccountId;
        DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == accountId);

        txtCountry.Visible = true;
        txtCountry.Text = entities.Countries.First(coutnry => coutnry.Id == account.Country).Name;

        int areaId;
        if (Int32.TryParse(account.Area.ToString(), out areaId))
        {
            lblCity.Visible = txtArea.Visible = true;
            txtArea.Text = entities.Areas.First(area => area.Id == account.Area).Name;
        }
        else
        {
            lblArea.Visible = txtArea.Visible = false;
        }

        int cityId;
        if (Int32.TryParse(account.City.ToString(), out cityId))
        {
            lblCity.Visible = txtCity.Visible = true;
            txtCity.Text = entities.Cities.First(city => city.Id == account.City).Name;
        }
        else
        {
            lblCity.Visible = txtCity.Visible = false;
        }

        int villageId;
        if (Int32.TryParse(account.Village.ToString(), out villageId))
        {

            lblVillage.Visible = txtVillage.Visible = true;
            txtVillage.Text = entities.Villages.First(village => village.Id == account.Village).Name;
        }
        else
        {
            lblVillage.Visible = txtVillage.Visible = false;
        }

        if (account.Address != null && account.Address != "")
        {
            lblAddress.Visible = txtAddress.Visible = true;
            txtAddress.Text = account.Address;
            txtAddress.Enabled = false;
        }
        else
        {
            lblAddress.Visible = txtAddress.Visible = false;
        }

        lbtnChangeAddress.Visible = true;
        lbtnCancelAddress.Visible = false;
        lbtnSaveAddress.Visible = false;
    }

    #region OnChangeAddress

    protected void DataBindCountry()
    {
        ddlCountry.DataSource = entities.Countries.OrderBy(o => o.Name);
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "Id";
        ddlCountry.DataBind();
    }

    protected void DataBindArea()
    {
        ddlArea.DataSource = entities.Areas;
        ddlArea.DataTextField = "Name";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
    }

    protected void DataBindCity()
    {
        ddlCity.DataSource = entities.Cities;
        ddlCity.DataTextField = "Name";
        ddlCity.DataValueField = "Id";
        ddlCity.DataBind();
    }

    protected void DataBindVillage()
    {
        ddlVillage.DataSource = entities.Villages;
        ddlVillage.DataTextField = "Name";
        ddlVillage.DataValueField = "Id";
        ddlVillage.DataBind();
    }

    #endregion


    #endregion

    #region Picture

    protected void lbtnChangePicture_Click(object sender, EventArgs e)
    {
        pnlChangePicture.Visible = true;
    }

    protected void lbtnCancelChangePicture_Click(object sender, EventArgs e)
    {
        pnlChangePicture.Visible = false;
    }

    protected void lbtnSaveChangePicture_Click(object sender, EventArgs e)
    {
        if (fuSource.HasFile)
        {
            int currentUserId = (Page as BasePage).CurrentAccountId;
            DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == currentUserId);
            string fileExt = System.IO.Path.GetExtension(fuSource.PostedFile.FileName);

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
                    entities.SaveChanges();
                    Response.Redirect("~/AccountInformation.aspx");
                }
                catch (Exception ex)
                {
                    lblResultMessage.Text = "Error: " + ex.Message;
                }
            }
            else lblResultMessage.Text = "Файла е прекалено голям.";
        }

    }

    #endregion

    #region Names

    protected void lbtnChangeNames_Click(object sender, EventArgs e)
    {
        lbtnChangeNames.Visible = false;
        lbtnCancelNames.Visible = true;
        lbtnSaveNames.Visible = true;

        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
    }

    protected void lbtnCancelNames_Click(object sender, EventArgs e)
    {
        DataBindNames();
    }

    protected void lbtnSaveNames_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int accountId = (Page as BasePage).CurrentAccountId;
            DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == accountId);

            account.FirstName = txtFirstName.Text;
            account.LastName = txtLastName.Text;

            entities.SaveChanges();
            DataBindNames();
        }
    }

    #endregion

    #region Address

    protected void lbtnChangeAddress_Click(object sender, EventArgs e)
    {

        int accountId = (Page as BasePage).CurrentAccountId;
        DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == accountId);

        lbtnChangeAddress.Visible = false;
        lbtnCancelAddress.Visible = lbtnSaveAddress.Visible = true;

        lblCountry.Visible = lblArea.Visible = lblCity.Visible = lblVillage.Visible = true;
        txtCountry.Visible = txtArea.Visible = txtCity.Visible = txtVillage.Visible = false;
        ddlCountry.Visible = ddlArea.Visible = ddlCity.Visible = ddlVillage.Visible = true;
        lblAddress.Visible = txtAddress.Enabled = true;

        DataBindCountry();
        ddlCountry.SelectedValue = account.Country == null ? "0" : account.Country.ToString();

        DataBindArea();
        ddlArea.SelectedValue = account.Area == null ? "0" : account.Area.ToString();

        DataBindCity();
        ddlCity.SelectedValue = account.City == null ? "0" : account.City.ToString();

        DataBindVillage();
        ddlVillage.SelectedValue = account.Village == null ? "0" : account.Village.ToString();

    }

    protected void lbtnCancelAddress_Click(object sender, EventArgs e)
    {
        DataBindAddress();
    }

    protected void lbtnSaveAddress_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int accounId = (Page as BasePage).CurrentAccountId;
            DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == accounId);

            account.Country = Int32.Parse(ddlCountry.SelectedValue);

            int? areaId = Int32.Parse(ddlArea.SelectedValue);
            account.Area = areaId != 0 ? areaId : null;

            int? cityId = Int32.Parse(ddlCity.SelectedValue);
            account.City = cityId != 0 ? cityId : null;

            int? villageId = Int32.Parse(ddlVillage.SelectedValue);
            account.Village = villageId != 0 ? villageId : null;

            account.Address = txtAddress.Text == string.Empty ? null : txtAddress.Text;
            entities.SaveChanges();

            DataBindAddress();
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "0")
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem(text: "Област", value: "0"));
            ddlArea.SelectedIndex = ddlCity.SelectedIndex = ddlVillage.SelectedIndex = 0;
            int countryId = Int32.Parse(ddlCountry.SelectedValue);
            List<DictionaryModel.Area> areas = entities.Areas.Where(ar => ar.CountryId == countryId).OrderBy(o => o.Name).ToList();
            if (areas.Count > 0)
            {
                ddlArea.Enabled = true;
                ddlArea.DataSource = areas;
            }
            else
            {
                ddlArea.Enabled = ddlCity.Enabled = ddlVillage.Enabled = false;
            }

            ddlCity.SelectedIndex = 0;
            ddlVillage.SelectedIndex = 0;
            ddlArea.DataBind();
        }
        else
        {
            ddlArea.SelectedIndex = ddlCity.SelectedIndex = ddlVillage.SelectedIndex = 0;
            ddlArea.Enabled = ddlCity.Enabled = ddlVillage.Enabled = false;
        }
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlArea.SelectedValue != "0")
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem(text: "Град", value: "0"));
            ddlCity.SelectedIndex = 0;
            int areaId = Int32.Parse(ddlArea.SelectedValue);
            List<DictionaryModel.City> cities = entities.Cities.Where(c => c.AreaId == areaId).OrderBy(o => o.Name).ToList();

            if (cities.Count > 0)
            {
                ddlCity.Enabled = true;
                ddlVillage.Enabled = false;
                ddlVillage.SelectedIndex = 0;
                ddlCity.DataSource = cities;
            }
            else
            {
                ddlCity.Enabled = ddlVillage.Enabled = false;

            }

            ddlCity.DataBind();

        }
        else
        {
            ddlCity.SelectedIndex = ddlVillage.SelectedIndex = 0;
            ddlCity.Enabled = ddlVillage.Enabled = false;
        }
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue != "0")
        {
            ddlVillage.Items.Clear();
            ddlVillage.Items.Add(new ListItem(text: "Село", value: "0"));
            ddlVillage.SelectedIndex = 0;
            int citiesId = Int32.Parse(ddlCity.SelectedValue);
            List<DictionaryModel.Village> vilige = entities.Villages.Where(cc => cc.CityId == citiesId).OrderBy(o => o.Name).ToList();

            if (vilige.Count > 0)
            {
                ddlVillage.Enabled = true;
                ddlVillage.DataSource = vilige;
            }
            else
            {
                ddlVillage.Enabled = false;

            }
            ddlVillage.DataBind();
        }
        else
        {
            ddlVillage.SelectedIndex = 0;
            ddlVillage.Enabled = false;
        }
    }

    #endregion

    #region Change Password

    protected void lbtnChangePassword_Click(object sender, EventArgs e)
    {
        ChangePasswordAccountInformation.Visible = true;
        pnlChangePasswordShow.Visible = false;
    }

    protected void lbtnCancelChangePassword_Click(object sender, EventArgs e)
    {
        pnlChangePasswordShow.Visible = true;
        ChangePasswordAccountInformation.Visible = false;
    }

    #endregion
}