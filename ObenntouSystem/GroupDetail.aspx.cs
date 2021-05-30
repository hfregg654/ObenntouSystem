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
    public partial class GroupDetail : System.Web.UI.Page
    {
        DBTool dBTool = new DBTool();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogInfo info = Session["IsLogined"] as LogInfo;

                string groupid = Request.QueryString["id"];
                if (!string.IsNullOrWhiteSpace(groupid))
                {
                    string[] groupcolname = { "group_omiseid", "group_pic", "group_type", "group_userid", "group_name" };
                    string[] groupcolnamep = { "@group_id" };
                    string[] groupp = { groupid };

                    string grouplogic = @"
                            WHERE group_deldate IS NULL AND group_id=@group_id
                            ";
                    DataTable groupdata = dBTool.readTable("Groups", groupcolname, grouplogic, groupcolnamep, groupp);
                    if (groupdata.Rows.Count > 0)
                    {
                        Image_G.ImageUrl = groupdata.Rows[0]["group_pic"].ToString();

                        if (info != null)
                        {
                            if (info.user_id == Convert.ToInt32(groupdata.Rows[0]["group_userid"]))
                            {
                                DDL_type.SelectedValue = groupdata.Rows[0]["group_type"].ToString();
                                IsUser.Visible = false;
                                IsConvener.Visible = true;
                                LogIndiv.Visible = true;
                            }
                            else
                            {
                                ltlgrouptype.Text = groupdata.Rows[0]["group_type"].ToString();
                                IsUser.Visible = true;
                                IsConvener.Visible = false;
                                LogIndiv.Visible = true;
                            }
                        }
                        else
                        {
                            ltlgrouptype.Text = groupdata.Rows[0]["group_type"].ToString();
                            IsUser.Visible = true;
                            IsConvener.Visible = false;
                            LogIndiv.Visible = false;
                        }

                        string[] Concolname = { "user_name" };
                        string[] Concolnamep = { "@user_id" };
                        string[] Conp = { groupdata.Rows[0]["group_userid"].ToString() };

                        string Conlogic = @"
                            WHERE user_deldate IS NULL AND user_id=@user_id
                            ";
                        DataTable Condata = dBTool.readTable("Users", Concolname, Conlogic, Concolnamep, Conp);
                        if (Condata.Rows.Count > 0)
                        {
                            ltlConName.Text = Condata.Rows[0]["user_name"].ToString();
                        }


                        string[] omisecolname = { "omise_name" };
                        string[] omisecolnamep = { "@omise_id" };
                        string[] omisep = { groupdata.Rows[0]["group_omiseid"].ToString() };

                        string omiselogic = @"
                            WHERE omise_deldate IS NULL AND omise_id=@omise_id
                            ";
                        DataTable omisedata = dBTool.readTable("OmiseMaster", omisecolname, omiselogic, omisecolnamep, omisep);
                        if (omisedata.Rows.Count > 0)
                        {
                            ltl_omise.Text = omisedata.Rows[0]["omise_name"].ToString();
                        }

                        string[] dishcolname = { "dish_id", "dish_name", "dish_price" };
                        string[] dishcolnamep = { "@dish_omiseid" };
                        string[] dishp = { groupdata.Rows[0]["group_omiseid"].ToString() };

                        string dishlogic = @"
                            WHERE dish_omiseid=@dish_omiseid
                            ";
                        DataTable dishdata = dBTool.readTable("Dishes", dishcolname, dishlogic, dishcolnamep, dishp);
                        Rep_Dish.DataSource = dishdata;
                        Rep_Dish.DataBind();




                        string[] orderusercolname = { "Orders.order_userid", "Users.User_name" };
                        string[] orderusercolnamep = { "@order_groupid" };
                        string[] orderuserp = { groupid };

                        string orderuserlogic = @"
                            JOIN Users ON Orders.order_userid=Users.User_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_groupid=@order_groupid
                            GROUP BY Orders.order_userid, Users.User_name
                            ";
                        DataTable orderuserdata = dBTool.readTable("Orders", orderusercolname, orderuserlogic, orderusercolnamep, orderuserp);
                        Rep_Order.DataSource = orderuserdata;
                        Rep_Order.DataBind();
                        

                    }

                }


            }
        }

        protected void Rep_Order_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LogInfo info = Session["IsLogined"] as LogInfo;
                Repeater rel = e.Item.FindControl("Rep_Orderdish") as Repeater;
                Button btn = e.Item.FindControl("kickbtn") as Button;

                string groupid = Request.QueryString["id"];
                string[] groupcolname = { "group_omiseid", "group_pic", "group_type", "group_userid", "group_name" };
                string[] groupcolnamep = { "@group_id" };
                string[] groupp = { groupid };

                string grouplogic = @"
                            WHERE group_deldate IS NULL AND group_id=@group_id
                            ";
                DataTable groupdata = dBTool.readTable("Groups", groupcolname, grouplogic, groupcolnamep, groupp);
                if (info != null)
                {
                    if (info.user_id == Convert.ToInt32(groupdata.Rows[0]["group_userid"]))
                    {
                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
                else
                {
                    btn.Visible = false;
                }

                string userid = btn.CommandArgument.ToString();
                if (!string.IsNullOrWhiteSpace(userid))
                {
                    string[] orderusercolname = { "Orders.order_dishesid", "Dishes.dish_name", "Orders.order_num" };
                    string[] orderusercolnamep = { "@order_userid" };
                    string[] orderuserp = { userid };

                    string orderuserlogic = @"
                            JOIN Dishes ON Orders.order_dishesid=Dishes.dish_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_userid=@order_userid
                            ";
                    DataTable orderuserdata = dBTool.readTable("Orders", orderusercolname, orderuserlogic, orderusercolnamep, orderuserp);
                    rel.DataSource = orderuserdata;
                    rel.DataBind();
                }

            }
        }

        protected void BackListbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}