using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Modules_FreeAccess_CreateNewAccountSubModule_AccountSecurity : System.Web.UI.UserControl
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

    }

    public void LoadDetails()
    {
        if (SetupAccountId != null) //Loading all Security details of current Account
        {
            MembershipUser user = Membership.GetUser(SetupASPNETID);
            hdnEditAccountSecutiry.Value = "Edit Account";
            txtEmail.Text = user.Email.Trim();
            txtEmail.Enabled = true;

            chkApproved.Checked = user.IsApproved;
        }
        else if (SetupCompleted != null)
        {
            HideEditAddControls();
        }
    }

    #region Buttons

    protected void lbtnGeneratePassword_Click(object sender, EventArgs e)
    {
        lblCurrentGeneratePassword.Visible = true;
        txtPassword.Attributes["Value"] = txtConfirmPassword.Attributes["Value"] = lblGeneratePassword.Text = Membership.GeneratePassword(8, 1);
    }

    protected void lbtnClear_Click(object sender, EventArgs e)
    {
        HideEditAddControls();
    }

    protected void HideEditAddControls()
    {
        txtEmail.Text = null;
        txtPassword.Attributes["Value"] = null;
        txtConfirmPassword.Attributes["Value"] = null;
        txtConfirmPassword.Text = null;
        txtQuestion.Text = null;
        txtAnswer.Text = null;
        txtConfirmAnswer.Text = null;
        txtReminder.Text = null;
        lblCurrentGeneratePassword.Visible = false;
        lblGeneratePassword.Text = null;
    }

    protected void lbtnNextStep_Click(object sender, EventArgs e)
    {
        try
        {
            bool password = txtPassword.Text == txtConfirmPassword.Text;
            bool answer = txtAnswer.Text == txtConfirmAnswer.Text;
            
            if (!password)
            {
                lblError.Text = "Въведените пароли не съвпадат.";
                return;
            }

            if (password && answer)
            {
                if (hdnEditAccountSecutiry.Value == "") //New Account
                {
                    DictionaryModel.Account account = new DictionaryModel.Account();

                    MembershipCreateStatus status;
                    MembershipUser user = Membership.CreateUser(DateTime.Now.ToString("ddhhmmssfff"), txtPassword.Text, txtEmail.Text.Trim().ToLower(), txtQuestion.Text.Trim(), txtAnswer.Text.Trim(), chkApproved.Checked, out status);

                    if (status != MembershipCreateStatus.Success)
                    {
                        switch (status)
                        {
                            case MembershipCreateStatus.DuplicateEmail:
                                lblError.Text = "Въведеният Email адрес е зает.";
                                break;
                            case MembershipCreateStatus.DuplicateProviderUserKey:
                                lblError.Text = "Дублиращ се потребителски код.";
                                break;
                            case MembershipCreateStatus.DuplicateUserName:
                                lblError.Text = "Въведеното потребителско име е заето.";
                                break;
                            case MembershipCreateStatus.InvalidAnswer:
                                lblError.Text = "Въведохте невалиден отговор на тайният въпрос.";
                                break;
                            case MembershipCreateStatus.InvalidEmail:
                                lblError.Text = "Въведохте невалиден Email адрес.";
                                break;
                            case MembershipCreateStatus.InvalidPassword:
                                lblError.Text = "Въведохте невалидна парола.";
                                break;
                            case MembershipCreateStatus.InvalidProviderUserKey:
                                lblError.Text = "Невалиден потретбителски код.";
                                break;
                            case MembershipCreateStatus.InvalidQuestion:
                                lblError.Text = "Веведохте невалиден таен въпрос.";
                                break;
                            case MembershipCreateStatus.InvalidUserName:
                                lblError.Text = "Въведохте невалидно потребителско име.";
                                break;
                            case MembershipCreateStatus.ProviderError:
                                lblError.Text = "Възникна грешка, моля опитайте отново.";
                                break;
                            case MembershipCreateStatus.UserRejected:
                                lblError.Text = "Вашият профил е спрян.";
                                break;
                            default:
                                break;
                        }
                        return;
                    }

                    user.IsApproved = chkApproved.Checked;
                    user.Comment = txtReminder.Text;
                    Membership.UpdateUser(user);
                    Roles.AddUserToRole(user.UserName, "User");

                    account.ASPNETID = (Guid)user.ProviderUserKey;
                    account.FirstName = string.Empty;
                    account.LastName = string.Empty;
                    entities.Accounts.AddObject(account);
                    entities.SaveChanges();
                    
                    

                    if (StepCompleted != null)
                    {
                        NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.AccountSecurity);

                        Session["SetupASPNETID"] = args.ASPNETID = account.ASPNETID;
                        Session["SetupAcountId"] = args.CurrentAccountId = account.Id;

                        StepCompleted.Invoke(this, args);
                        //HideEditAddControls();
                    }
                }
                else //Edit Account
                {
                    MembershipUser user = Membership.GetUser(SetupASPNETID);
                    
                    user.ChangePasswordQuestionAndAnswer(txtPassword.Text, txtQuestion.Text, txtAnswer.Text);
                    user.Comment = txtReminder.Text;
                    user.IsApproved = chkApproved.Checked;
                    Membership.UpdateUser(user);
                    hdnEditAccountSecutiry.Value = null;

                    HideEditAddControls();

                    if (StepCompleted != null)
                    {
                        NewAccountStepCompletedEventArgs args = new NewAccountStepCompletedEventArgs(NewAccountWizardSteps.AccountSecurity);

                        args.ASPNETID = (Guid)user.ProviderUserKey;

                        StepCompleted.Invoke(this, args);
                    }
                }
            }
            else
            {
                lblError.Text = "Въведените отговори на тайният въпрос не съвпадат.";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// "Възникна проблем, моля опитайте по-късно.";
        }
    }

    #endregion

}