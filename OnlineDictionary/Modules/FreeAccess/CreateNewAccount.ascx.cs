using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_FreeAccess_CreateNewAccount : System.Web.UI.UserControl
{
    #region Variables

    DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();

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

    public int SetupCompleted
    {
        get
        {
            return Convert.ToInt32(ViewState["SetupCompleted"]);
        }
        set
        {
            ViewState["SetupCompleted"] = value.ToString();
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        stepAccountSecurity.StepCompleted += new NewAccountStepCompletedEventHandler(WizardStepCompleted);
        stepPersonalInformation.StepCompleted += new NewAccountStepCompletedEventHandler(WizardStepCompleted);
        stepAdditionalInformation.StepCompleted += new NewAccountStepCompletedEventHandler(WizardStepCompleted);
        stepCompleted.StepCompleted += new NewAccountStepCompletedEventHandler(WizardStepCompleted);

    }

    void WizardStepCompleted(object sender, NewAccountStepCompletedEventArgs e)
    {
        stepAccountSecurity.Visible = false;
        stepPersonalInformation.Visible = false;
        stepAdditionalInformation.Visible = false;
        stepCompleted.Visible = false;

        SetupASPNETID = e.ASPNETID;
        SetupAccountId = e.CurrentAccountId;
        
        switch (e.CurrentStepCompleted)
        {
            case NewAccountWizardSteps.AccountSecurity:
                lbtnPersonalInformation.Enabled = true;
                stepPersonalInformation.Visible = true;
                stepPersonalInformation.SetupASPNETID = SetupASPNETID;
                stepPersonalInformation.SetupAccountId = SetupAccountId;
                stepPersonalInformation.LoadDetails();
                break;
            case NewAccountWizardSteps.PersonalInformation:
                SetupCountryId = e.CurrentCountryId;
                lbtnAdditionalInformation.Enabled = true;
                stepAdditionalInformation.Visible = true;
                stepAdditionalInformation.SetupASPNETID = SetupASPNETID;
                stepAdditionalInformation.SetupAccountId = SetupAccountId;
                stepAdditionalInformation.SetupCountryId = SetupCountryId;
                stepAdditionalInformation.LoadDetails();
                break;
            case NewAccountWizardSteps.AdditionalInformation:
                stepCompleted.Visible = true;
                stepCompleted.SetupASPNETID = SetupASPNETID;
                stepCompleted.SetupAccountId = SetupAccountId;
                stepCompleted.LoadDetails();
                break;
            case NewAccountWizardSteps.SetupCompleted:
                SetupCompleted = e.SetupCompleted;
                lbtnAccountSecurity.Enabled = false;
                lbtnPersonalInformation.Enabled = false;
                lbtnAdditionalInformation.Enabled = false;
                stepAccountSecurity.Visible = false;
                stepAccountSecurity.SetupCompleted = SetupCompleted;
                stepAdditionalInformation.LoadDetails();
                pnlCreateNewAccount.Visible = false;
                pnlSuccess.Visible = true;
                break;
            default:
                break;
        }
        UpdatePanelNewAccount.Update();
    }

    protected void btnStep_Click(object sender, EventArgs e)
    {
        stepAccountSecurity.Visible = false;
        stepPersonalInformation.Visible = false;
        stepAdditionalInformation.Visible = false;
        stepCompleted.Visible = false;

        LinkButton btn = (LinkButton)sender;
        NewAccountWizardSteps currentClickedStep = (NewAccountWizardSteps)Enum.Parse(typeof(NewAccountWizardSteps), btn.CommandArgument);

        switch (currentClickedStep)
        {
            case NewAccountWizardSteps.AccountSecurity:
                stepAccountSecurity.Visible = true;
                stepAccountSecurity.SetupASPNETID = SetupASPNETID;
                stepAccountSecurity.SetupAccountId = SetupAccountId;
                stepAccountSecurity.LoadDetails();
                break;
            case NewAccountWizardSteps.PersonalInformation:
                stepPersonalInformation.Visible = true;
                stepPersonalInformation.SetupASPNETID = SetupASPNETID;
                stepPersonalInformation.SetupAccountId = SetupAccountId;
                stepPersonalInformation.SetupCountryId = SetupCountryId;
                stepPersonalInformation.LoadDetails();
                break;
            case NewAccountWizardSteps.AdditionalInformation:
                stepAdditionalInformation.Visible = true;
                stepAdditionalInformation.SetupASPNETID = SetupASPNETID;
                stepAdditionalInformation.SetupAccountId = SetupAccountId;
                stepAdditionalInformation.SetupCountryId = SetupCountryId;
                stepAdditionalInformation.LoadDetails();
                break;
            default:
                break;
        }
        UpdatePanelNewAccount.Update();
    }

}