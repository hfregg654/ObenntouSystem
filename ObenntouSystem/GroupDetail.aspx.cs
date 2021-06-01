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
    public partial class GroupDetail : System.Web.UI.Page
    {
        static List<OrderingDish> dishdiction = new List<OrderingDish>();
        DBTool dBTool = new DBTool();
        ContextModel model = new ContextModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable groupdata = new DataTable();
            if (!IsPostBack)
            {
                LogInfo info = Session["IsLogined"] as LogInfo;

                if (DDL_type.SelectedValue == "已到")
                {
                    Response.Redirect($"~/Index.aspx");
                }

                string groupid = Request.QueryString["id"];
                if (!string.IsNullOrWhiteSpace(groupid))
                {
                    string[] groupcolname = { "group_omiseid", "group_pic", "group_type", "group_userid", "group_name" };
                    string[] groupcolnamep = { "@group_id" };
                    string[] groupp = { groupid };

                    string grouplogic = @"
                            WHERE group_deldate IS NULL AND group_id=@group_id
                            ";
                    groupdata = dBTool.readTable("Groups", groupcolname, grouplogic, groupcolnamep, groupp);
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
                                Rep_OrderCount.Visible = true;
                                ImageUser.ImageUrl = info.user_pic;
                            }
                            else
                            {
                                ltlgrouptype.Text = groupdata.Rows[0]["group_type"].ToString();
                                IsUser.Visible = true;
                                IsConvener.Visible = false;
                                LogIndiv.Visible = true;
                                Rep_OrderCount.Visible = false;
                                ImageUser.ImageUrl = info.user_pic;

                            }
                        }
                        else
                        {
                            ltlgrouptype.Text = groupdata.Rows[0]["group_type"].ToString();
                            IsUser.Visible = true;
                            IsConvener.Visible = false;
                            LogIndiv.Visible = false;
                            Rep_OrderCount.Visible = false;
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

                        string[] dishcolname = { "dish_id", "dish_name", "dish_price", "dish_pic" };
                        string[] dishcolnamep = { "@dish_omiseid" };
                        string[] dishp = { groupdata.Rows[0]["group_omiseid"].ToString() };

                        string dishlogic = @"
                            WHERE dish_omiseid=@dish_omiseid
                            ";
                        DataTable dishdata = dBTool.readTable("Dishes", dishcolname, dishlogic, dishcolnamep, dishp);
                        Rep_Dish.DataSource = dishdata;
                        Rep_Dish.DataBind();




                        string[] orderusercolname = { "Orders.order_userid", "Users.User_name", "Users.user_pic" };
                        string[] orderusercolnamep = { "@order_groupid" };
                        string[] orderuserp = { groupid };

                        string orderuserlogic = @"
                            JOIN Users ON Orders.order_userid=Users.User_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_groupid=@order_groupid
                            GROUP BY Orders.order_userid, Users.User_name, Users.user_pic
                            ";
                        DataTable orderuserdata = dBTool.readTable("Orders", orderusercolname, orderuserlogic, orderusercolnamep, orderuserp);
                        Rep_Order.DataSource = orderuserdata;
                        Rep_Order.DataBind();




                        string[] ordercolname = { "Orders.order_dishesid", "Dishes.dish_name", "Orders.order_num", "Dishes.dish_price" };
                        string[] ordercolnamep = { "@order_groupid" };
                        string[] orderp = { groupid };

                        string orderlogic = @"
                            JOIN Dishes ON Orders.order_dishesid=Dishes.dish_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_groupid=@order_groupid
                            ORDER BY Orders.order_dishesid
                            ";
                        DataTable orderdata = dBTool.readTable("Orders", ordercolname, orderlogic, ordercolnamep, orderp);
                        List<OrderingDish> allorder = new List<OrderingDish>();
                        decimal totalprice = 0;
                        foreach (DataRow item in orderdata.Rows)
                        {
                            if (allorder.FindIndex(obj => obj.DishID == Convert.ToInt32(item["order_dishesid"])) == -1)
                            {
                                OrderingDish order = new OrderingDish();
                                order.DishID = Convert.ToInt32(item["order_dishesid"]);
                                order.DishName = item["dish_name"].ToString();
                                order.DishNum = Convert.ToInt32(item["order_num"]);
                                allorder.Add(order);
                                totalprice += Convert.ToInt32(item["dish_price"]) * Convert.ToInt32(item["order_num"]);
                            }
                            else
                            {
                                allorder[allorder.FindIndex(obj => obj.DishID == Convert.ToInt32(item["order_dishesid"]))].DishNum += Convert.ToInt32(item["order_num"]);
                                totalprice += Convert.ToInt32(item["dish_price"]) * Convert.ToInt32(item["order_num"]);
                            }
                        }

                        HFtotalprice.Value = totalprice.ToString();
                        Rep_OrderCount.DataSource = allorder;
                        Rep_OrderCount.DataBind();

                    }


                }


            }
            if (DDL_type.SelectedValue == "結團")
            {
                DDL_type.Items[0].Enabled = false;
                LogIndiv.Visible = false;
            }
            else if (DDL_type.SelectedValue == "已到")
            {
                DDL_type.Enabled = false;
            }
            ltlnoorder.Text = "";

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
                    if (info.user_id == Convert.ToInt32(groupdata.Rows[0]["group_userid"]) && DDL_type.SelectedValue == "未結團")
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
                    string[] orderusercolnamep = { "@order_userid", "@order_groupid" };
                    string[] orderuserp = { userid, groupid };

                    string orderuserlogic = @"
                            JOIN Dishes ON Orders.order_dishesid=Dishes.dish_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_userid=@order_userid AND Orders.order_groupid=@order_groupid
                            ";
                    DataTable orderuserdata = dBTool.readTable("Orders", orderusercolname, orderuserlogic, orderusercolnamep, orderuserp);

                    List<OrderingDish> allorder = new List<OrderingDish>();

                    foreach (DataRow item in orderuserdata.Rows)
                    {
                        if (allorder.FindIndex(obj => obj.DishID == Convert.ToInt32(item["order_dishesid"])) == -1)
                        {
                            OrderingDish order = new OrderingDish();
                            order.DishID = Convert.ToInt32(item["order_dishesid"]);
                            order.DishName = item["dish_name"].ToString();
                            order.DishNum = Convert.ToInt32(item["order_num"]);
                            allorder.Add(order);
                        }
                        else
                        {
                            allorder[allorder.FindIndex(obj => obj.DishID == Convert.ToInt32(item["order_dishesid"]))].DishNum += Convert.ToInt32(item["order_num"]);
                        }
                    }

                    rel.DataSource = allorder;
                    rel.DataBind();
                }

            }
        }

        protected void BackListbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }

        protected void DDL_dishnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dishchoose = sender as DropDownList;
            string[] dishidandname = dishchoose.ToolTip.Split(',');

            if (dishdiction.FindIndex(obj => obj.DishName == dishidandname[0]) == -1)
            {
                OrderingDish orderingDish = new OrderingDish();
                orderingDish.DishID = Convert.ToInt32(dishidandname[1]);
                orderingDish.DishName = dishidandname[0];
                orderingDish.DishNum = Convert.ToInt32(dishchoose.SelectedValue);
                dishdiction.Add(orderingDish);
            }
            else
            {
                dishdiction[dishdiction.FindIndex(obj => obj.DishName == dishidandname[0])].DishNum += Convert.ToInt32(dishchoose.SelectedValue);
            }
            Rep_Ordering.DataSource = dishdiction;
            Rep_Ordering.DataBind();
            dishchoose.SelectedIndex = 0;
        }

        protected void Rep_Dish_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PlaceHolder dishchoose = e.Item.FindControl("PH_dishnum") as PlaceHolder;
                LogInfo info = Session["IsLogined"] as LogInfo;
                if (info != null && DDL_type.SelectedValue == "未結團")
                {
                    dishchoose.Visible = true;

                }
                else
                {
                    dishchoose.Visible = false;
                }

            }
        }


        private class OrderingDish
        {
            public int DishID { get; set; }
            public string DishName { get; set; }
            public int DishNum { get; set; }
        }

        protected void OrderResetbtn_Click(object sender, EventArgs e)
        {
            string groupid = Request.QueryString["id"];

            ltlnoorder.Text = "";
            dishdiction.Clear();
            Response.Redirect($"~/GroupDetail.aspx?id={groupid}");
        }

        protected void OrderOKbtn_Click(object sender, EventArgs e)
        {
            if (dishdiction.Count == 0)
            {
                ltlnoorder.Text = "請選擇至少一項";
            }
            else
            {
                LogInfo info = Session["IsLogined"] as LogInfo;
                string groupid = Request.QueryString["id"];
                int groupidint;
                if (groupid != null && int.TryParse(groupid, out groupidint))
                {
                    var typecheck = model.Groups.Where(obj => obj.group_id == groupidint).FirstOrDefault();
                    if (typecheck != null)
                    {
                        if (typecheck.group_type != "結團" && typecheck.group_type != "已到")
                        {
                            string[] colname = { "order_groupid", "order_userid", "order_dishesid", "order_num", "order_cre", "order_credate" };
                            string[] colnamep = { "@order_groupid", "@order_userid", "@order_dishesid", "@order_num", "@order_cre", "@order_credate" };
                            List<string> p = new List<string>();
                            foreach (OrderingDish item in dishdiction)
                            {
                                p.Add(groupid);
                                p.Add(info.user_id.ToString());
                                p.Add(item.DishID.ToString());
                                p.Add(item.DishNum.ToString());
                                p.Add(info.user_id.ToString());
                                p.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                            dBTool.InsertTable("Orders", colname, colnamep, p);
                            dishdiction.Clear();

                            Response.Redirect($"~/GroupDetail.aspx?id={groupid}");
                        }
                        else
                        {
                            Response.Write("<script>alert('此團已結團!');window.location.href ='./Index.aspx'</script>");
                        }

                    }
                }




            }
        }

        protected void kickbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            LogInfo info = Session["IsLogined"] as LogInfo;
            string groupid = Request.QueryString["id"];


            string[] updatecol_Logic = { "order_del=@order_del", "order_deldate=@order_deldate" }; /*  要更新的欄位*/
            string Where_Logic = "order_userid=@order_userid AND order_groupid=@order_groupid";
            string[] updatecolname_P = { "@order_del", "@order_deldate", "@order_userid", "@order_groupid" }; /*要帶入的參數格子*/
            List<string> update_P = new List<string>();
            update_P.Add(info.user_id.ToString());
            update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            update_P.Add(button.CommandArgument);
            update_P.Add(groupid);
            dBTool.UpdateTable("Orders", updatecol_Logic, Where_Logic, updatecolname_P, update_P);

            Response.Redirect($"~/GroupDetail.aspx?id={groupid}");
        }

        protected void Rep_OrderCount_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Literal dishchoose = e.Item.FindControl("totalprice") as Literal;
                if (dishchoose != null)
                {
                    dishchoose.Text = HFtotalprice.Value;
                }

            }
        }

        protected void ChangeTypebtn_Click(object sender, EventArgs e)
        {
            LogInfo info = Session["IsLogined"] as LogInfo;
            string groupid = Request.QueryString["id"];

            if (DDL_type.SelectedValue == "已到")
            {
                string[] updatecol_Logic = { "group_type=@group_type", "group_del=@group_del", "group_deldate=@group_deldate" }; /*  要更新的欄位*/
                string Where_Logic = "group_id=@group_id AND group_deldate IS NULL";
                string[] updatecolname_P = { "@group_type", "@group_del", "@group_deldate", "@group_id" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(DDL_type.SelectedValue);
                update_P.Add(info.user_id.ToString());
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add(groupid);
                dBTool.UpdateTable("Groups", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                Response.Redirect($"~/Index.aspx");

            }
            else
            {
                string[] updatecol_Logic = { "group_type=@group_type", "group_upd=@group_upd", "group_upddate=@group_upddate" }; /*  要更新的欄位*/
                string Where_Logic = "group_id=@group_id AND group_deldate IS NULL";
                string[] updatecolname_P = { "@group_type", "@group_upd", "@group_upddate", "@group_id" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(DDL_type.SelectedValue);
                update_P.Add(info.user_id.ToString());
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add(groupid);
                dBTool.UpdateTable("Groups", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                Response.Redirect($"~/GroupDetail.aspx?id={groupid}");
            }



        }
    }
}