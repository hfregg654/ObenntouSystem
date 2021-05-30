using ObenntouSystem.Utility;
using System;
using System.Text.RegularExpressions;

namespace ObenntouSystem
{
    public partial class LogIn : System.Web.UI.Page
    {
        //命名變數 登入後首頁
        string _goToUrl = "~/Index.aspx";
        LoginHelper helper = new LoginHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

            // 檢查使用者資訊，如果已經有session 且符合資料庫內使用者資訊則直接轉至首頁
            if (helper.HasLogIned())
                //導向至指定網址(首頁)
                Response.Redirect(this._goToUrl);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //擷取使用者的帳號與密碼 放入acc及pwd變數
            string acc = this.TextBoxacc.Text;
            string pwd = this.TextBoxpwd.Text;
            if (string.IsNullOrWhiteSpace(acc))
                this.ltlacc.Text = "帳號不可為空";
            else
                this.ltlacc.Text = "";
            if (string.IsNullOrWhiteSpace(acc))
                this.ltlpwd.Text = "密碼不可為空";
            else
                this.ltlpwd.Text = "";


            //判斷登入資訊是否為真 比較後如果為真 真為True
            bool isSuccess = helper.TryLogIn(acc, pwd);
            //如果是True 則跳轉到登入首頁
            if (isSuccess)
            {
                //跳轉到首頁
                Response.Redirect(this._goToUrl);
            }
            else
            {   //如果為0 則輸出帳號或密碼輸入錯誤，請重新輸入訊息
                this.ltMessage.Text = "帳號或密碼輸入錯誤，請重新輸入";
            }
        }
    }
}