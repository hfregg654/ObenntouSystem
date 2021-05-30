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
    public partial class CreateGroupaspx : System.Web.UI.Page
    {
        DBTool dBTool = new DBTool();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                img_pic.ImageUrl = DDL_Pic.SelectedValue;
                string[] colname = { "omise_id", "omise_name" };
                string[] colnamep = { "" };
                string[] p = { "" };

                string logic = @"
                            WHERE omise_deldate IS NULL
                            ";
                DataTable data = dBTool.readTable("OmiseMaster", colname, logic, colnamep, p);
                foreach (DataRow item in data.Rows)
                {
                    DDL_Omise.Items.Add(new ListItem()
                    {
                        Text = item["omise_name"].ToString(),
                        Value = item["omise_id"].ToString()
                    });
                }

            };

        }

        protected void Pic_SelectedIndexChanged(object sender, EventArgs e)
        {
            img_pic.ImageUrl = DDL_Pic.SelectedValue;
        }

        protected void Resetbtn_Click(object sender, EventArgs e)
        {
            DDL_Omise.SelectedIndex = 0;
            DDL_Pic.SelectedIndex = 0;
            ltlgname.Text = "";
            ltlMessage.Text = "";
            Groupname.Text = "";
            img_pic.ImageUrl = DDL_Pic.SelectedValue;
        }

        protected void OKbtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Groupname.Text))
            {
                string ClearName = dBTool.WithoutScript(Groupname.Text);

                LogInfo info = Session["IsLogined"] as LogInfo;
                string[] colname = { "group_userid", "group_omiseid", "group_name", "group_cre", "group_credate", "group_pic", "group_type" };
                string[] colnamep = { "@group_userid", "@group_omiseid", "@group_name", "@group_cre", "@group_credate", "@group_pic", "@group_type" };
                List<string> p = new List<string>();
                p.Add(info.user_id.ToString());
                p.Add(DDL_Omise.SelectedValue);
                p.Add(ClearName);
                p.Add(info.user_id.ToString());
                p.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                p.Add(DDL_Pic.SelectedValue);
                p.Add("未結團");
                dBTool.InsertTable("Groups", colname, colnamep, p);

                Response.Redirect("~/Index.aspx");
            }
            else
            {
                ltlgname.Text = "不可空白";
            }
        }
    }
}