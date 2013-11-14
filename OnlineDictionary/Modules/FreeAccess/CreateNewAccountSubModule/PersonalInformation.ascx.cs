using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Modules_FreeAccess_CreateNewAccountSubModule_PersonalInformation : System.Web.UI.UserControl
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

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadDetails()
    {
        if (SetupCountryId != 0) //Loading all details of current Account
        {
            DictionaryModel.Account currentAccount = entities.Accounts.First(acc => acc.Id == SetupAccountId);
            MembershipUser user = Membership.GetUser(SetupASPNETID);
            
            txtFirstName.Text = currentAccount.FirstName;
            txtLastName.Text = currentAccount.LastName;
            ddlCountry.SelectedValue = currentAccount.Country.ToString();
            hdnEditPersonalInformation.Value = "Edite Account";
        }

        DataBindCountries();
    }

    protected void DataBindCountries()
    {
        ddlCountry.DataSource = entities.Countries;
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "Id";
        ddlCountry.DataBind();
        ddlCountry.SelectedIndex = 27;
    }

    protected void lbtnNextStep_Click(object sender, EventArgs e)
    {
        try
        {


        string sss = Session["SetupASPNETID"].ToString();
        Guid setASPNETID = Guid.Parse(Session["SetupASPNETID"].ToString());
                int setAccId = Int32.Parse(Session["SetupAcountId"].ToString());
                DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.ASPNETID == setASPNETID);
            
            if (hdnEditPersonalInformation.Value == "") //New Account
            {
                account.FirstName = txtFirstName.Text.Trim();
                account.LastName = txtLastName.Text.Trim();
                account.Country = Int32.Parse((ddlCountry.SelectedValue != "0") ? ddlCountry.SelectedValue :"27");

                entities.SaveChanges();
                

                if (StepCompleted != null)
                {
                    NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.PersonalInformation);

                    args.ASPNETID = setASPNETID;
                    args.CurrentAccountId = setAccId;
                    args.CurrentCountryId = account.Country.Value;

                    StepCompleted.Invoke(this, args);
                }
            }
            else // Edit Account
            {
                account.FirstName = txtFirstName.Text.Trim();
                account.LastName = txtLastName.Text.Trim();
                account.Country = Int32.Parse((ddlCountry.SelectedValue != "0") ? ddlCountry.SelectedValue : "27");

                entities.SaveChanges();

                if (StepCompleted != null)
                {
                    NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.PersonalInformation);

                    args.ASPNETID = setASPNETID;
                    args.CurrentAccountId = SetupAccountId;
                    args.CurrentCountryId = account.Country.Value;

                    StepCompleted.Invoke(this, args);
                }
            }
        }
        catch
        {
            lblError.Text = "Възникна проблем, моля опитайте по-късно.";
        }
    }

}