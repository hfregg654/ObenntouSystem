using ObenntouSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ObenntouSystem.Utility
{
    public class FileTool
    {
        //接受檔案類型
        private string[] _allowExts = { ".jpg", ".png", ".bmp", ".gif" };

        ContextModel model = new ContextModel();
        public string SaveFile(FileUpload fu)
        {
            //如無檔案則回傳空字串
            if (!fu.HasFile)
                return string.Empty;


            //取得檔案
            var uFile = fu.PostedFile;
            //取得檔案名稱
            var fileName = uFile.FileName;
            //取得副檔名(檔案類型)
            string fileExt = System.IO.Path.GetExtension(fileName);
            //判別檔案類型
            if (!_allowExts.Contains(fileExt.ToLower()))
                return string.Empty;


            //取名為GUID
            string newFileName = Guid.NewGuid().ToString() + fileExt;
            //存檔路徑
            string path = $"/Images/{newFileName}";
            string realypath = HttpContext.Current.Server.MapPath(path);
            //存檔
            uFile.SaveAs(realypath);
            return path;
        }
        public void DeleteFile(string fu)
        {
            if (fu.Contains("Images"))
            {
                string paths = HttpContext.Current.Server.MapPath(fu);
                File.Delete(paths);
            }
        }
    }
}