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
    public partial class BackCreateDishes : System.Web.UI.Page
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
                foreach (OmiseMaster item in list)
                {
                    DDL_Omise.Items.Add(new ListItem()
                    {
                        Text = item.omise_name,
                        Value = item.omise_id.ToString()
                    });
                }
                DDL_Omise.SelectedIndex = 0;
                int omiseid = Convert.ToInt32(DDL_Omise.SelectedValue);
                var listdishes = model.Dishes.Where(obj => obj.dish_deldate == null && obj.dish_omiseid == omiseid).ToList();
                Rep_Dishes.DataSource = listdishes;
                Rep_Dishes.DataBind();
            }
        }

        protected void CreateDishbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"./SettingDish.aspx?omise={DDL_Omise.SelectedValue}");
        }

        protected void UpdateDishbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Response.Redirect($"./SettingDish.aspx?id={button.CommandArgument}");
        }

        protected void DelDishbtn_Click(object sender, EventArgs e)
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
            var dish = model.Dishes.Where(obj => obj.dish_id == id && obj.dish_deldate == null).FirstOrDefault();
            if (dish != null)
            {
                dish.dish_del = info.user_id;
                dish.dish_deldate = DateTime.Now;
                model.SaveChanges();
            }
            Response.Redirect("./BackCreateDishes.aspx");
        }

        protected void DDL_Omise_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = sender as DropDownList;
            int omiseid = Convert.ToInt32(dropDownList.SelectedValue);
            var list = model.Dishes.Where(obj => obj.dish_deldate == null && obj.dish_omiseid == omiseid).ToList();
            Rep_Dishes.DataSource = list;
            Rep_Dishes.DataBind();
        }
    }
}