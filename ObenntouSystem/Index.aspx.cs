using ObenntouSystem.Models;
using ObenntouSystem.Utility;
using ObenntouSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObenntouSystem
{
    public partial class Index : System.Web.UI.Page
    {
        DBTool  dBTool = new DBTool();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogInfo info = Session["IsLogined"] as LogInfo;

            if (info != null)
            {
                Linklogout.Visible = true;
                Linklogin.Visible = false;
                Linkcreate.Visible = true;
            }
            else
            {
                Linklogout.Visible = false;
                Linklogin.Visible = true;
                Linkcreate.Visible = false;
            }

            string[] colname = { "group_id", "group_name", "group_pic", "group_type" };
            string[] colnamep = { "" };
            string[] p = { "" };

            string logic = @"
                            WHERE group_deldate IS NULL
                            ";
            DataTable data = dBTool.readTable("Groups", colname, logic, colnamep, p);

           
            repGroup.DataSource = data;
            repGroup.DataBind();
        }

        protected void Linklogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LogIn.aspx");
        }

        protected void Linklogout_Click(object sender, EventArgs e)
        {
            LoginHelper helper = new LoginHelper();
            helper.Logout();
            Response.Redirect("~/Index.aspx");
        }

        protected void Linkcreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateGroup.aspx");
        }
    }
}