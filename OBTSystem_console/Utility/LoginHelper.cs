using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObenntouSystem.Models;
using ObenntouSystem.ViewModels;

namespace OBTSystem_console.Utility
{

    public class LoginHelper
    {
        //定義常數儲存登入者資訊的Session名稱
        private const string _sessionKey = "IsLogined";
        /// <summary>
        /// 確認登入狀態,回傳值為true or false
        /// </summary>
        /// <returns></returns>
        public bool HasLogIned()
        {
            //定義變數取得登入狀態的Session，後面接as LogInfo 前面var判斷型別才不會出錯。 
            var val = HttpContext.Current.Session[_sessionKey] as LogInfo;
            //檢查變數內有值且為true回傳true,否則回傳false
            if (val != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 登入的帳號密碼驗證,回傳值為true or false
        /// </summary>
        /// <param name="Account">使用者帳號</param>
        /// <param name="Password">使用者密碼</param>
        /// <returns></returns>
        public bool TryLogIn(string Account, string Password)
        {
            //若已是登入狀態則回傳true
            if (HasLogIned())
                return true;
            //從資料庫裡撈出符合帳號的資料,若沒有則回傳FALSE
            ContextModel getaccount = new ContextModel();
            User dtuserAccount = getaccount.Users.Where(obj => obj.user_acc == Account).FirstOrDefault();
            if (dtuserAccount == null)
                return false;
            if (dtuserAccount == null || string.Compare(Password, dtuserAccount.user_pwd, false) != 0)
                return false;

            HttpContext.Current.Session[_sessionKey] = new LogInfo()
            {
                user_id = dtuserAccount.user_id,
                user_name = dtuserAccount.user_name,
                user_pri = dtuserAccount.user_pri,
                user_phone = dtuserAccount.user_phone,
                user_mail = dtuserAccount.user_mail
            };
            return true;


        }
        /// <summary>
        /// 將使用者登出並刪除其Session
        /// </summary>
        public void Logout()
        {
            //若為非登入狀態直接回傳
            if (!HasLogIned())
                return;
            HttpContext.Current.Session.Remove(_sessionKey);
        }

        /// <summary>
        /// 取得已登入者的資訊，若沒登入傳空字串
        /// </summary>
        /// <returns></returns>
        public LogInfo GetCurrentUserInfo()
        {
            if (!HasLogIned())
                return null;
            return HttpContext.Current.Session[_sessionKey] as LogInfo;

        }



    }
}