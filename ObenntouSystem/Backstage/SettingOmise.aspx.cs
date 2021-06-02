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
    public partial class SettingOmise : System.Web.UI.Page
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
                string omiseid = Request.QueryString["id"];
                int omiseidint;
                if (omiseid != null && int.TryParse(omiseid, out omiseidint))
                {
                    var thisomise = model.OmiseMasters.Where(obj => obj.omise_id == omiseidint && obj.omise_deldate == null).FirstOrDefault();
                    if (thisomise != null)
                    {
                        this.TBName.Text = thisomise.omise_name;
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
            clearerror();
        }
        private void clearerror()
        {
            this.lblNameerror.Text = "";
            this.lblerror.Text = "";
        }
        protected void Cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./BackCreateOmise.aspx");
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
            string omiseid = Request.QueryString["id"];
            int omiseidint;
            if (omiseid != null && int.TryParse(omiseid, out omiseidint))
            {
                var thisomise = model.OmiseMasters.Where(obj => obj.omise_id == omiseidint && obj.omise_deldate == null).FirstOrDefault();

                if (thisomise != null && ShowError(TBName, lblNameerror, 15))
                {
                    thisomise.omise_name = dBTool.WithoutScript(this.TBName.Text);
                    thisomise.omise_upd = info.user_id;
                    thisomise.omise_upddate = DateTime.Now;
                    model.SaveChanges();
                    Response.Redirect("./BackCreateOmise.aspx");

                }
                else
                {
                    if (!ShowError(TBName, lblNameerror, 15))
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
                if (ShowError(TBName, lblNameerror, 15))
                {
                    var NewOmise = new OmiseMaster();
                    NewOmise.omise_name = dBTool.WithoutScript(this.TBName.Text);
                    NewOmise.omise_cre = info.user_id;
                    NewOmise.omise_credate = DateTime.Now;
                    model.OmiseMasters.Add(NewOmise);
                    model.SaveChanges();
                    Response.Redirect("./BackCreateOmise.aspx");
                }
                else
                {
                    if (!ShowError(TBName, lblNameerror, 15))
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