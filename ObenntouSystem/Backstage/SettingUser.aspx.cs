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

namespace ObenntouSystem.Backstage
{
    public partial class SettingUser : System.Web.UI.Page
    {
        ContextModel model = new ContextModel();
        DBTool dBTool = new DBTool();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userid = Request.QueryString["id"];
                int useridint;
                if (userid != null && int.TryParse(userid, out useridint))
                {
                    var thispeople = model.Users.Where(obj => obj.user_id == useridint && obj.user_deldate == null).FirstOrDefault();
                    if (thispeople != null)
                    {
                        this.TBName.Text = thispeople.user_name;
                        this.TBPhone.Text = thispeople.user_phone;
                        this.TBMail.Text = thispeople.user_mail;
                        this.DDL_Pri.SelectedValue = thispeople.user_pri;
                        this.ImgUser.ImageUrl = thispeople.user_pic;
                        this.accforcreate.Visible = false;
                        this.pwdforcreate.Visible = true;
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
            this.TBPhone.Text = "";
            this.TBMail.Text = "";
            this.DDL_Pri.SelectedValue = "User";
            this.ImgUser.ImageUrl = "~/Images/Group_Black.jpg";
            this.TBAcc.Text = "";
            this.TBPwd.Text = "";
            this.TBNPwd.Text = "";
            this.TBPwdC.Text = "";
            this.lblAccerror.Text = "";
            this.lblNameerror.Text = "";
            this.lblPhoneerror.Text = "";
            this.lblMailerror.Text = "";
            this.lblPwdCerror.Text = "";
            this.lblPwderror.Text = "";
            this.lblNPwderror.Text = "";
            this.lblerror.Text = "";
            this.FUpUser.Attributes.Clear();
        }
        private void clearerror()
        {
            this.lblAccerror.Text = "";
            this.lblNameerror.Text = "";
            this.lblPhoneerror.Text = "";
            this.lblMailerror.Text = "";
            this.lblPwdCerror.Text = "";
            this.lblPwderror.Text = "";
            this.lblNPwderror.Text = "";
            this.lblerror.Text = "";
        }

        protected void Cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./BackCreateUser.aspx");
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
            string userid = Request.QueryString["id"];
            int useridint;
            if (userid != null && int.TryParse(userid, out useridint))
            {
                var thispeople = model.Users.Where(obj => obj.user_id == useridint && obj.user_deldate == null).FirstOrDefault();

                if (thispeople != null && string.IsNullOrWhiteSpace(this.TBPwd.Text) && string.IsNullOrWhiteSpace(this.TBNPwd.Text) && string.IsNullOrWhiteSpace(this.TBPwdC.Text))
                {
                    if (ShowError(TBName, lblNameerror, 15) && ShowError(TBPhone, lblPhoneerror, 10) && ShowError(TBMail, lblMailerror, 100))
                    {
                        thispeople.user_name = dBTool.WithoutScript(this.TBName.Text);
                        thispeople.user_phone = dBTool.WithoutScript(this.TBPhone.Text);
                        thispeople.user_mail = dBTool.WithoutScript(this.TBMail.Text);
                        thispeople.user_pri = this.DDL_Pri.SelectedValue;
                        FileTool fileTool = new FileTool();
                        if (FUpUser.HasFile)
                        {
                            if (!thispeople.user_pic.Contains("Group_"))
                            {
                                fileTool.DeleteFile(thispeople.user_pic);
                            }
                            thispeople.user_pic = fileTool.SaveFile(FUpUser);
                        }
                        thispeople.user_upd = info.user_id;
                        thispeople.user_upddate = DateTime.Now;
                        model.SaveChanges();
                        Response.Redirect("./BackCreateUser.aspx");
                    }

                }
                else if (thispeople != null && !string.IsNullOrWhiteSpace(this.TBPwd.Text) && !string.IsNullOrWhiteSpace(this.TBNPwd.Text) && !string.IsNullOrWhiteSpace(this.TBPwdC.Text))
                {
                    if (ShowError(TBName, lblNameerror, 15) && ShowError(TBPhone, lblPhoneerror, 10) && ShowError(TBMail, lblMailerror, 100))
                    {
                        var usercheck = model.Users.Where(obj => obj.user_id == useridint && obj.user_pwd == TBPwd.Text && obj.user_deldate == null).FirstOrDefault();
                        if (usercheck != null)
                        {
                            if (TBNPwd.Text == TBPwdC.Text)
                            {
                                if (ShowError(TBNPwd, lblNPwderror, 50))
                                {
                                    usercheck.user_name = dBTool.WithoutScript(this.TBName.Text);
                                    usercheck.user_phone = dBTool.WithoutScript(this.TBPhone.Text);
                                    usercheck.user_mail = dBTool.WithoutScript(this.TBMail.Text);
                                    usercheck.user_pwd = dBTool.WithoutScript(this.TBNPwd.Text);
                                    usercheck.user_pri = this.DDL_Pri.SelectedValue;
                                    FileTool fileTool = new FileTool();
                                    if (FUpUser.HasFile)
                                    {
                                        if (!thispeople.user_pic.Contains("Group_"))
                                        {
                                            fileTool.DeleteFile(usercheck.user_pic);
                                        }
                                        usercheck.user_pic = fileTool.SaveFile(FUpUser);
                                    }
                                    usercheck.user_upd = info.user_id;
                                    usercheck.user_upddate = DateTime.Now;
                                    model.SaveChanges();
                                    Response.Redirect("./BackCreateUser.aspx");
                                }
                            }
                            else
                            {
                                lblPwdCerror.Text = "新密碼與密碼確認不符";
                            }
                        }
                        else
                        {
                            lblPwderror.Text = "舊密碼錯誤";
                        }

                    }
                }
                else
                {
                    if (ShowError(TBPwd, lblPwderror, 50) || ShowError(TBNPwd, lblNPwderror, 50) || ShowError(TBPwdC, lblPwdCerror, 50))
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
                if (ShowError(TBName, lblNameerror, 15) && ShowError(TBPhone, lblPhoneerror, 10) && ShowError(TBMail, lblMailerror, 100)
                    && ShowError(TBAcc, lblAccerror, 50) && ShowError(TBPwd, lblPwderror, 50) && ShowError(TBPwdC, lblPwdCerror, 50))
                {
                    if (TBPwd.Text == TBPwdC.Text)
                    {
                        var acc_check = model.Users.Where(obj => obj.user_acc == TBAcc.Text).FirstOrDefault();
                        if (acc_check == null)
                        {
                            var Newuser = new User();
                            Newuser.user_name = dBTool.WithoutScript(this.TBName.Text);
                            Newuser.user_phone = dBTool.WithoutScript(this.TBPhone.Text);
                            Newuser.user_mail = dBTool.WithoutScript(this.TBMail.Text);
                            Newuser.user_acc = dBTool.WithoutScript(this.TBAcc.Text);
                            Newuser.user_pwd = dBTool.WithoutScript(this.TBPwd.Text);
                            Newuser.user_pri = this.DDL_Pri.SelectedValue;
                            FileTool fileTool = new FileTool();
                            if (FUpUser.HasFile)
                            {
                                Newuser.user_pic = fileTool.SaveFile(FUpUser);
                            }
                            else
                            {
                                Newuser.user_pic = "/Images/Group_Black.jpg";
                            }
                            Newuser.user_cre = info.user_id;
                            Newuser.user_credate = DateTime.Now;
                            model.Users.Add(Newuser);
                            model.SaveChanges();
                            Response.Redirect("./BackCreateUser.aspx");
                        }
                        else
                        {
                            lblAccerror.Text = "帳號重複，請使用其他帳號";
                        }

                    }
                    else
                    {
                        lblPwdCerror.Text = "密碼與密碼確認不符";
                    }

                }
                else
                {
                    if (ShowError(TBName, lblNameerror, 15) || ShowError(TBPhone, lblPhoneerror, 10) || ShowError(TBMail, lblMailerror, 100)
                    || ShowError(TBAcc, lblAccerror, 50) || ShowError(TBPwd, lblPwderror, 50) || ShowError(TBPwdC, lblPwdCerror, 50))
                    {
                        lblerror.Text = "請填寫正確";
                    }
                    else if (!ShowError(TBName, lblNameerror, 15) && !ShowError(TBPhone, lblPhoneerror, 10) && !ShowError(TBMail, lblMailerror, 100)
                     && !ShowError(TBAcc, lblAccerror, 50) && !ShowError(TBPwd, lblPwderror, 50) && !ShowError(TBPwdC, lblPwdCerror, 50))
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