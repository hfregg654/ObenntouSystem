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
    public partial class SettingDish : System.Web.UI.Page
    {
        ContextModel model = new ContextModel();
        DBTool dBTool = new DBTool();
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
                string dishid = Request.QueryString["id"];
                int dishidint;
                string omiseid = Request.QueryString["omise"];
                int omiseidint;
                if (dishid != null && int.TryParse(dishid, out dishidint))
                {
                    var thisdish = model.Dishes.Where(obj => obj.dish_id == dishidint && obj.dish_deldate == null).FirstOrDefault();
                    if (thisdish != null)
                    {
                        this.TBName.Text = thisdish.dish_name;
                        this.TBPrice.Text = thisdish.dish_price.ToString();
                    }
                    else
                    {
                        LoginHelper loginHelper = new LoginHelper();
                        loginHelper.Logout();
                        Response.Redirect("../Index.aspx");
                    }

                }
                else if (omiseid != null && int.TryParse(omiseid, out omiseidint))
                {
                    var thisomise = model.OmiseMasters.Where(obj => obj.omise_id == omiseidint && obj.omise_deldate == null).FirstOrDefault();
                    if (thisomise != null)
                    {
                        this.lblOmiseName.Text = thisomise.omise_name;
                    }
                    else
                    {
                        LoginHelper loginHelper = new LoginHelper();
                        loginHelper.Logout();
                        Response.Redirect("../Index.aspx");
                    }
                }
            }
        }

        protected void Clearbtn_Click(object sender, EventArgs e)
        {
            this.TBName.Text = "";
            this.TBPrice.Text = "";
            clearerror();
        }
        private void clearerror()
        {
            this.lblNameerror.Text = "";
            this.lblPriceerror.Text = "";
            this.lblerror.Text = "";
        }
        protected void Cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./BackCreateDishes.aspx");
        }
        protected void OKbtn_Click(object sender, EventArgs e)
        {
            clearerror();
            LogInfo info = Session["IsLogined"] as LogInfo;

            if (info == null || (info.user_pri != "Manager" && info.user_pri != "SuperManager"))
            {
                LoginHelper loginHelper = new LoginHelper();
                loginHelper.Logout();
                Response.Redirect("../Index.aspx");
            }
            string dishid = Request.QueryString["id"];
            int dishidint;
            if (dishid != null && int.TryParse(dishid, out dishidint))
            {
                var thisdish = model.Dishes.Where(obj => obj.dish_id == dishidint && obj.dish_deldate == null).FirstOrDefault();

                if (thisdish != null && ShowError(TBName, lblNameerror, 25) && ShowError(TBPrice, lblPriceerror, 18))
                {
                    int dishprice;
                    if (int.TryParse(this.TBPrice.Text, out dishprice) && dishprice > 0)
                    {
                        thisdish.dish_name = dBTool.WithoutScript(this.TBName.Text);
                        thisdish.dish_price = dishprice;
                        FileTool fileTool = new FileTool();
                        if (FUpDish.HasFile)
                        {
                            if (!thisdish.dish_pic.Contains("Group_"))
                            {
                                fileTool.DeleteFile(thisdish.dish_pic);
                            }
                            thisdish.dish_pic = fileTool.SaveFile(FUpDish);
                        }
                        thisdish.dish_upd = info.user_id;
                        thisdish.dish_upddate = DateTime.Now;
                        model.SaveChanges();
                        Response.Redirect("./BackCreateDishes.aspx");
                    }
                    else
                    {
                        lblPriceerror.Text = "價格必須為數字且大於0";
                    }

                }
                else
                {
                    if (!ShowError(TBName, lblNameerror, 25) || !ShowError(TBPrice, lblPriceerror, 18))
                    {
                        lblerror.Text = "請填寫正確";
                    }
                    else if (!ShowError(TBName, lblNameerror, 25) && !ShowError(TBPrice, lblPriceerror, 18))
                    {
                        lblerror.Text = "請填寫正確";
                    }
                    else
                    {
                        LoginHelper loginHelper = new LoginHelper();
                        loginHelper.Logout();
                        Response.Redirect("../Index.aspx");
                    }

                }
            }
            else
            {
                if (ShowError(TBName, lblNameerror, 25) && ShowError(TBPrice, lblPriceerror, 18))
                {
                    int dishprice;
                    if (int.TryParse(this.TBPrice.Text, out dishprice) && dishprice > 0)
                    {
                        string omiseid = Request.QueryString["omise"];
                        int omiseidint;
                        if (omiseid != null && int.TryParse(omiseid, out omiseidint))
                        {
                            var NewDish = new Dish();
                            NewDish.dish_omiseid = omiseidint;
                            NewDish.dish_name = dBTool.WithoutScript(this.TBName.Text);
                            NewDish.dish_price = dishprice;
                            FileTool fileTool = new FileTool();
                            if (FUpDish.HasFile)
                            {
                                NewDish.dish_pic = fileTool.SaveFile(FUpDish);
                            }
                            else
                            {
                                NewDish.dish_pic = "/Images/Group_Black.jpg";
                            }
                            NewDish.dish_cre = info.user_id;
                            NewDish.dish_credate = DateTime.Now;
                            model.Dishes.Add(NewDish);
                            model.SaveChanges();
                            Response.Redirect("./BackCreateDishes.aspx");
                        }
                        else
                        {
                            LoginHelper loginHelper = new LoginHelper();
                            loginHelper.Logout();
                            Response.Redirect("../Index.aspx");
                        }
                    }
                    else
                    {
                        lblPriceerror.Text = "價格必須為數字且大於0";
                    }
                }
                else
                {
                    if (!ShowError(TBName, lblNameerror, 25) || !ShowError(TBPrice, lblPriceerror, 18))
                    {
                        lblerror.Text = "請填寫正確";
                    }
                    else if (!ShowError(TBName, lblNameerror, 25) && !ShowError(TBPrice, lblPriceerror, 18))
                    {
                        lblerror.Text = "請填寫正確";
                    }
                    else
                    {
                        LoginHelper loginHelper = new LoginHelper();
                        loginHelper.Logout();
                        Response.Redirect("../Index.aspx");
                    }
                }


            }
        }
        private bool ShowError(TextBox textBox, Label label, int? maxlength)
        {
            bool success = true;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                label.Text = "不可為空";
                success = false;
            }
            if (textBox.Text.Length > maxlength)
            {
                label.Text = $"字數過長，需小於{maxlength}字";
                success = false;
            }

            return success;
        }
    }
}