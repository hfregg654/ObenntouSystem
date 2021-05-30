using ObenntouSystem.Utility;
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

        }
    }
}