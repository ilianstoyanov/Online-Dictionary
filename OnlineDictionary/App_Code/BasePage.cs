using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Xml.Linq;

public class BasePage : System.Web.UI.Page
{
    #region Variables

    /// <summary>
    /// Returns integer variable with ID of current Account 
    /// </summary>
    public int CurrentAccountId
    {
        get
        {
            return (int)Convert.ToInt32(Session["CurrentAccountId"]);
        }
        set
        {
            Session["CurrentAccountId"] = value.ToString();
        }
    }

    /// <summary>
    /// Returns a string with username of current Account
    /// </summary>
    public string CurrentAccountUsername
    {
        get
        {
            return Session["CurrentAccountUsername"] as string;
        }
        set
        {
            Session["CurrentAccountUsername"] = value;
        }
    }

    /// <summary>
    /// Returns string with company name of current company
    /// </summary>
    public string CurrentAccountName
    {
        get
        {
            return Session["CurrentAccountName"] as string;
        }
        set
        {
            Session["CurrentAccountName"] = value;
        }
    }

    /// <summary>
    /// Returns MembershipUser object of current Account
    /// </summary>
    public MembershipUser user
    {
        get
        {
            if (CurrentAccountUsername == null)
            {
                return null;
            }
            else
            {
                return Membership.GetUser(CurrentAccountUsername);
            }
        }
        set
        {
            CurrentAccountUsername = value.UserName;
        }
    }

    protected List<string> LastLoadedControls
    {
        get;
        set;
    }

    public Dictionary<string, string> MenuItems
    {
        get;
        set;
    }

    public string CurrentUrlHash
    {
        get
        {
            if (Session["hash"] == null)
            {
                if (Roles.IsUserInRole("Master"))
                {
                    Session["hash"] = "#Master";
                }
                else if (Roles.IsUserInRole("Admin"))
                {
                    Session["hash"] = "#Admin";
                }
                else
                {
                    Session["hash"] = "#Default";
                }
            }
            return Session["hash"].ToString();
        }
        set
        {
            Session["hash"] = value;
        }
    }

    public string CurrentUserNames
    {
        get
        {
            return Session["CurrentUserNames"] as string;
        }
        set
        {
            Session["CurrentUserNames"] = value;
        }
    }

    #endregion

    #region Methods

    protected void SetCurrentAccountVariables(MembershipUser user)
    {
        DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities(); // >>>>>IZVYN METODA

        Guid aspnetId = (Guid)user.ProviderUserKey;

        DictionaryModel.Account account = entities.Accounts.FirstOrDefault(acc => acc.ASPNETID == aspnetId);

        if (account != null) //The Manager is Admin
        {
            this.CurrentAccountUsername = user.UserName;
            this.CurrentAccountId = account.Id;
            this.CurrentUserNames = account.FirstName + " " + account.LastName;
        }
        else //The user is guest
        {
            this.CurrentAccountUsername = "Guest";//.CurrentManagerUsername = user.UserName;
            this.CurrentAccountId = 1; //.CurrentCompanyId = accountManager.CompanyId.Value;// accountManager.CompanyId;
        }

        HttpCookie UserName = new HttpCookie("UserName", user.UserName);
        Response.Cookies.Add(UserName);
    }

    protected void SetUser()
    {
        if (User.Identity.IsAuthenticated && user == null)
        {
            user = Membership.GetUser(Page.User.Identity.Name);
            SetCurrentAccountVariables(user);
        }


        DataBindMainMenu();

        if (!CheckIfUserHasPermissionsToSeeThisPage())
        {
            CurrentUrlHash = null;
            Response.Redirect("~/" + CurrentUrlHash.Replace("#", "").Replace(" ", "") + ".aspx");
        }
    }

    /// <summary>
    /// check if user has authorization to open this page
    /// </summary>
    /// <returns>BOOLEAN</returns>
    private bool CheckIfUserHasPermissionsToSeeThisPage()
    {
        foreach (KeyValuePair<string, string> item in MenuItems)
        {
            if (Request.RawUrl.ToLower().Contains("/" + item.Value.ToLower().Replace(" ", "")))
            {
                return true;
            }
        }

        return false;
    }

    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    protected bool CheckIfXMLDataSourceExists(IXMLDataSource control)
    {
        bool isExists = true;
        foreach (string filename in control.XmlDataFileNames)
        {
            if (!System.IO.File.Exists(Server.MapPath(@"App_Data\" + filename)))
            {
                isExists = false;
            }
        }
        return isExists;
    }

    /// <summary>
    /// Loading all available pages for current user
    /// </summary>
    protected void DataBindMainMenu()
    {
        XDocument menu = XDocument.Load(Server.MapPath("~/App_Data/MainMenu.xml"));
        MenuItems = new Dictionary<string, string>();

        foreach (XElement menuItem in menu.Root.Elements().Where(el => el.Attribute("visible").Value == "true")) //get all visible menu items
        {
            string menuName = menuItem.Attribute("name").Value;
            string menuPath = menuItem.Attribute("path").Value;

            if (menuItem.Attribute("role").Value.Contains("Guest") && !User.Identity.IsAuthenticated)
            {
                MenuItems.Add(menuItem.Attribute("name").Value, menuItem.Attribute("path").Value);
            }
            else if (menuItem.Attribute("role").Value.Contains("Admin") && Roles.IsUserInRole("Admin"))
            {
                MenuItems.Add(menuItem.Attribute("name").Value, menuItem.Attribute("path").Value);
            }
            else if (menuItem.Attribute("role").Value.Contains("Master") && Roles.IsUserInRole("Master"))
            {
                MenuItems.Add(menuName, menuPath);
            }
            else if (user != null && (menuItem.Attribute("role").Value.Contains("User")))
            {
                int totalControls = menuItem.Elements().Count();

                foreach (XElement control in menuItem.Elements())
                {
                    UserControl u = (UserControl)LoadControl("Modules/" + control.Attribute("name").Value);
                    IXMLDataSource xmlSourceControl = u as IXMLDataSource;
                    if (xmlSourceControl != null) //check if the control uses xml file as data source
                    {
                        if (!CheckIfXMLDataSourceExists(xmlSourceControl)) //check if the xml data source file exists
                        {
                            totalControls--;
                        }
                    }
                }

                if (totalControls > 0)
                {
                    MenuItems.Add(menuName, menuPath);
                }
            }
        }
    }

    /// <summary>
    /// Loadig all controls for current page
    /// </summary>
    /// <param name="mustDataBind"></param>
    protected void LoadControls(bool mustDataBind)
    {
        LastLoadedControls = new List<string>();

        CurrentUrlHash = Request.Url.PathAndQuery.Replace("/", "#").Replace(".aspx", "");
        string chekCurrentUrlHash = CurrentUrlHash;

        if (CurrentUrlHash.Contains("?"))
        {
            string[] splitCurrentUrlHash = CurrentUrlHash.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            chekCurrentUrlHash = splitCurrentUrlHash[0];
        }

        XElement currentMenuItem = XDocument.Load(Server.MapPath("~/App_Data/MainMenu.xml")).Root.Elements().First(el => el.Attribute("path").Value.ToLower().Replace(".aspx", "") == chekCurrentUrlHash.Replace("#", "").ToLower());

        foreach (XElement control in currentMenuItem.Elements()) //load all controns that need to be inside this control
        {
            UserControl u = (UserControl)LoadControl("Modules/" + control.Attribute("name").Value);
            IXMLDataSource xmlSourceControl = u as IXMLDataSource;



            if (xmlSourceControl != null) //check if the control uses xml file as data source
            {
                if (CheckIfXMLDataSourceExists(xmlSourceControl)) //check if the xml data source file exists
                {
                    LastLoadedControls.Add("Modules/" + control.Attribute("name").Value);
                }
            }
            else
            {
                LastLoadedControls.Add("Modules/" + control.Attribute("name").Value);
            }

        }

        LoadUserControls(mustDataBind);
    }

    /// <summary>
    /// Finding place for current control and showing it 
    /// </summary>
    /// <param name="MustDataBind"></param>
    public void LoadUserControls(bool MustDataBind)
    {
        this.Master.FindControl("PlaceHolderControls").Controls.Clear();

        int index = 0;

        foreach (string controlPath in LastLoadedControls)
        {
            if (!string.IsNullOrEmpty(controlPath))
            {
                UserControl uc = (UserControl)LoadControl(controlPath.Substring(0, controlPath.IndexOf(".ascx") + ".ascx".Length));
                uc.ID = controlPath.Replace("/", "_").Replace(".ascx", index.ToString());

                //Check if the control implement ILocation
                //ILocation location = uc as ILocation;
                //if (location != null)
                //{
                //    location.SelectedLocationIndex = Convert.ToInt32(controlPath.Substring(controlPath.IndexOf(".ascx") + ".ascx".Length));
                //}

                this.Master.FindControl("PlaceHolderControls").Controls.Add(uc);

                index++;
            }
        }
    }


    public string SetUserTracking(out bool startTimer)
    {
        string url = string.Empty;
        startTimer = false;
        if (user != null && Roles.IsUserInRole("User"))
        {
            DateTime timeNow = DateTime.Now;
            url = "~/UserTracking.ashx?id=" + CurrentAccountId + "&timeNow=" + timeNow;
            startTimer = true;
        }
        return url;
    }

    #endregion
}