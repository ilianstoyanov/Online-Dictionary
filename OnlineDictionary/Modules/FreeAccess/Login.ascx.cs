using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Modules_FreeAccess_Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.ToString();
        string password = txtPassword.Text.ToString();
        string username = Membership.GetUserNameByEmail(email);

        if (Membership.ValidateUser(username, password))
        {
            MembershipUser user = Membership.GetUser(username);
            if (user.IsApproved)
            {
                if (user != null && !user.IsApproved && Roles.IsUserInRole(user.UserName, "Client"))
                {
                    lblFailure.Text = "Your account have been locked. Please contact your account manager for further access";

                    //FailureText.Text += GetSupportManagersText(user);
                    return;
                }


                HttpCookie landing = new HttpCookie("usernamepermanent");
                landing.Value = email;
                landing.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(landing);
                FormsAuthentication.SetAuthCookie(username, false);
                Session["LoginTime"] = DateTime.Now;

                //if (cwcNewWidnow.Checked)
                //{
                //    string popupId = "cp" + DateTime.Now.ToString("yyyyMMddHHmm");
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "openAdministrationPopup", "setTimeout(function(){openAdmin('" + popupId + "');}, 100);", true);
                //}
                //else
                //{
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                }
                else
                {
                    Response.Redirect("~/");
                }
            }
            else if (user != null && user.IsApproved)
            {
                lblFailure.Text = "Вашият профил е блокиран.";
            }
        }
        else
        {
            lblFailure.Text = "Невалиден E-mail или парола.";
        }
    }
}