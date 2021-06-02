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
    public partial class SettingGroupPic : System.Web.UI.Page
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
                var list = model.GroupImageMasters.Where(obj => obj.gpic_deldate == null).ToList();

                Rep_Gpic.DataSource = list;
                Rep_Gpic.DataBind();
            }
        }

        protected void Delbtn_Click(object sender, EventArgs e)
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
            var pic = model.GroupImageMasters.Where(obj => obj.gpic_id == id && obj.gpic_deldate == null).FirstOrDefault();
            if (pic != null)
            {
                if (!pic.gpic_path.Contains("Group_"))
                {
                    pic.gpic_del = info.user_id;
                    pic.gpic_deldate = DateTime.Now;
                    FileTool fileTool = new FileTool();
                    fileTool.DeleteFile(pic.gpic_path);
                    model.SaveChanges();
                    Response.Redirect("./SettingGroupPic.aspx");
                }
                else
                {
                    Response.Write("<script>alert('此圖片無法刪除!');window.location.href ='./SettingGroupPic.aspx'</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('已無此圖片!');window.location.href ='./SettingGroupPic.aspx'</script>");
            }

        }

        protected void Clearbtn_Click(object sender, EventArgs e)
        {
            this.ImgNGroup.ImageUrl = "~/Images/Group_Black.jpg";
            clearerror();
            this.FUpNGroup.Attributes.Clear();
        }
        private void clearerror()
        {
            this.lblerror.Text = "";
        }

        protected void Cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./BackIndex.aspx");
        }

        protected void OKbtn_Click(object sender, EventArgs e)
        {
            LogInfo info = Session["IsLogined"] as LogInfo;
            if (info == null || (info.user_pri != "Manager" && info.user_pri != "SuperManager"))
            {
                LoginHelper loginHelper = new LoginHelper();
                loginHelper.Logout();
                Response.Redirect("../Index.aspx");
            }
            FileTool fileTool = new FileTool();
            if (FUpNGroup.HasFile)
            {
                GroupImageMaster NgroupImageMaster = new GroupImageMaster();

                NgroupImageMaster.gpic_path = fileTool.SaveFile(FUpNGroup);
                NgroupImageMaster.gpic_cre = info.user_id;
                NgroupImageMaster.gpic_credate = DateTime.Now;

                model.GroupImageMasters.Add(NgroupImageMaster);
                model.SaveChanges();
                Response.Redirect("./SettingGroupPic.aspx");

            }
            else
            {
                lblerror.Text = "無選擇檔案";
            }
        }
    }
}