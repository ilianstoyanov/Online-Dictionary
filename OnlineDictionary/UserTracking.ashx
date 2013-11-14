<%@ WebHandler Language="C#" Class="UserTracking" %>

using System;
using System.Web;
using System.Web.Security;
using System.Data.Objects;
using System.Linq;
using System.Web.SessionState;


public class UserTracking : IHttpHandler, IRequiresSessionState
{
    DictionaryModel.DictionaryEntities entities = new DictionaryModel.DictionaryEntities();
    
    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request.QueryString["id"];
        int currentId = 0;
        if (Int32.TryParse(id, out currentId))
        {
            //if ( context.Session == null)
           if(context.Session["LoginTime"] == null)
            {
               HttpContext.Current.Session["LoginTime"] = DateTime.Now.ToString();
            }
            DateTime logOutTime = DateTime.Parse(context.Request.QueryString["timeNow"]);
            DateTime logInTime = DateTime.Parse(context.Session["LoginTime"].ToString());
            TimeSpan spentTime = logOutTime.Subtract(logInTime);

            DictionaryModel.TrackingSpendTime last = entities.TrackingSpendTimes.FirstOrDefault(user => user.UserId.Equals(currentId) && EntityFunctions.DiffDays(user.LoginDate, logOutTime) == 0);

            bool update = false;

            if (last != null)
            {
                spentTime = spentTime.Add(last.SpendTime);
                update = true;
                context.Session["LoginTime"] = logOutTime;
            }

            DictionaryModel.TrackingSpendTime track = new DictionaryModel.TrackingSpendTime();
            
            try
            {
                if (!update)
                {
                    track.UserId = currentId;
                    track.LoginDate = logOutTime;
                    track.SpendTime = spentTime;
                    entities.TrackingSpendTimes.AddObject(track);
                    entities.SaveChanges();
                }
                else
                {
                    last.SpendTime = spentTime;
                    entities.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}