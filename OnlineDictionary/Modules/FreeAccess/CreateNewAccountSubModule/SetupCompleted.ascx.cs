using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Modules_FreeAccess_CreateNewAccountSubModule_SetupCompleted : System.Web.UI.UserControl
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

    #endregion
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void LoadDetails()
    {
        Guid setASPNETID = (Guid)Session["SetupASPNETID"];
        int setAccId = Int32.Parse(Session["SetupAcountId"].ToString());
        DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.ASPNETID == SetupASPNETID);
        MembershipUser user = Membership.GetUser(SetupASPNETID);

        txtEmail.Text = user.Email;
        txtFirstName.Text = account.FirstName;
        txtLastName.Text = account.LastName;
        //txtCountry.Text = entities.Countries.Select((country => country.Id == account.Country)Name;
        //txtArea.Text = 
        //    txtCity.Text = 
        //    txtVillage.Text = 
        txtAddress.Text = account.Address;
        txtPhone.Text = account.Phone.ToString();
        txtNotes.Text = account.Notes;

    }

    protected void lbtnContinue_Click(object sender, EventArgs e)
    {
        if (StepCompleted != null)
        {
            NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.SetupCompleted);

            Guid setASPNETID = Guid.Parse(Session["SetupASPNETID"].ToString());
            int setAccId = Int32.Parse(Session["SetupAcountId"].ToString());
            args.SetupCompleted = 1;
            args.CurrentAccountId = setAccId;
            args.ASPNETID = setASPNETID;

            StepCompleted.Invoke(this, args);
        }
    }
}