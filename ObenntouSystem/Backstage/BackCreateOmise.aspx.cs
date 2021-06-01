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
    public partial class BackCreateOmise : System.Web.UI.Page
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
                var list = model.OmiseMasters.Where(obj => obj.omise_deldate == null).ToList();
                Rep_Omise.DataSource = list;
                Rep_Omise.DataBind();
            }
        }

        protected void DelOmisebtn_Click(object sender, EventArgs e)
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
            var omise = model.OmiseMasters.Where(obj => obj.omise_id == id && obj.omise_deldate == null).FirstOrDefault();
            if (omise != null)
            {
                omise.omise_del = info.user_id;
                omise.omise_deldate = DateTime.Now;
                model.SaveChanges();
            }
            Response.Redirect("./BackCreateOmise.aspx");
        }

        protected void UpdateOmisebtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Response.Redirect($"./SettingOmise.aspx?id={button.CommandArgument}");
        }

        protected void CreateOmisebtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./SettingOmise.aspx");
        }
    }
}