using ObenntouSystem.Models;
using ObenntouSystem.Utility;
using ObenntouSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObenntouSystem.Backstage
{
    public partial class BackCreateUser : System.Web.UI.Page
    {
        ContextModel model = new ContextModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogInfo info = Session["IsLogined"] as LogInfo;
            if (!IsPostBack)
            {
                if (info == null || (info.user_pri != "Manager" && info.user_pri != "SuperManager"))
                {
                    LoginHelper loginHelper = new LoginHelper();
                    loginHelper.Logout();
                    Response.Redirect("../Index.aspx");
                }
                var list = model.Users.Where(obj => obj.user_deldate == null && obj.user_pri != "SuperManager").ToList();
                Rep_Users.DataSource = list;
                Rep_Users.DataBind();
            }
        }

        protected void CreateUserbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./SettingUser.aspx");
        }

        protected void UpdateUserbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Response.Redirect($"./SettingUser.aspx?id={button.CommandArgument}");
        }

        protected void DelUserbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            LogInfo info = Session["IsLogined"] as LogInfo;
            if (info == null || (info.user_pri != "Manager" && info.user_pri != "SuperManager"))
            {
                LoginHelper loginHelper = new LoginHelper();
                loginHelper.Logout();
                Response.Redirect("../Index.aspx");
            }
            int id = Convert.ToInt32(button.CommandArgument);
            var user = model.Users.Where(obj => obj.user_id == id && obj.user_deldate == null).FirstOrDefault();
            if (user != null)
            {
                user.user_del = info.user_id;
                user.user_deldate = DateTime.Now;
                model.SaveChanges();
            }
            Response.Redirect("./BackCreateUser.aspx");
        }
    }
}