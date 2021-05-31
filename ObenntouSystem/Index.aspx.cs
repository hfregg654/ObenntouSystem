using ObenntouSystem.Models;
using ObenntouSystem.Utility;
using ObenntouSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObenntouSystem
{
    public partial class Index : System.Web.UI.Page
    {
        DBTool dBTool = new DBTool();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogInfo info = Session["IsLogined"] as LogInfo;

            if (info != null)
            {
                ltlWelcome.Text = $"歡迎  {info.user_name}";
                Linklogout.Visible = true;
                Linklogin.Visible = false;
                Linkcreate.Visible = true;
            }
            else
            {
                ltlWelcome.Text = "";
                Linklogout.Visible = false;
                Linklogin.Visible = true;
                Linkcreate.Visible = false;
            }



            BuildDataTableCommit();
        }


        public void BuildDataTableCommit()
        {
            string page = Request.QueryString["Page"];
            int pIndex;
            if (string.IsNullOrEmpty(page))
                pIndex = 1;
            else
            {
                int.TryParse(page, out pIndex);

                if (pIndex <= 0)
                    pIndex = 1;
            }
            string name = Request.QueryString["name"];

            int totalSize;
            int _pageSize = 9;

            DataTable Comdata = readTableForPage(name, out totalSize, pIndex, _pageSize);

            if (Comdata.Rows.Count > 0)
            {
                Nothingdiv.Visible = false;
            }
            else
            {
                Nothingdiv.Visible = true;
            }

            int pages = CalculatePages(totalSize, _pageSize);
            List<PagingLink> pagingList = new List<PagingLink>();
            HyperLink1.NavigateUrl = $"Index.aspx?Page={pages}";
            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"Index.aspx?Page={this.GetQueryString(false, i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });
            }

            this.repPaging.DataSource = pagingList;
            this.repPaging.DataBind();


            repGroup.DataSource = Comdata;
            repGroup.DataBind();
        }


        private string GetQueryString(bool includePage, int? pageIndex)
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];
            string name = Request.QueryString["name"];
            //----- Get Query string parameters -----


            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(page) && includePage)
                conditions.Add("Page=" + page);

            if (!string.IsNullOrEmpty(name))
                conditions.Add("Name=" + name);


            if (pageIndex.HasValue)
                conditions.Add("Page=" + pageIndex.Value);

            string retText =
                (conditions.Count > 0)
                    ? "?" + string.Join("&", conditions)
                    : string.Empty;

            return retText;
        }

        private string connectionString = ConfigurationManager.ConnectionStrings["ContextModel"].ConnectionString;
        public DataTable readTableForPage(string name, out int totalSize, int currentPage = 1, int pageSize = 9)
        {


            string queryString =
                $@" 
                    SELECT TOP {pageSize} * FROM
                    (
                        SELECT 
                            ROW_NUMBER() OVER(ORDER BY group_id DESC) AS RowNumber,
                             group_id,
                             group_name, 
                             group_pic,
                             group_type
                        FROM Groups
                        WHERE group_deldate IS NULL AND group_name LIKE '%' + @name + '%'
                    ) AS TempT
                    WHERE RowNumber > {pageSize * (currentPage - 1)}
                    ORDER BY group_id DESC
                ";

            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                if (!string.IsNullOrEmpty(name))
                    command.Parameters.AddWithValue("@name", name);
                else
                    command.Parameters.AddWithValue("@name", "");
            try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(); //執行指令串
                    DataTable dt = new DataTable();
                    dt.Load(reader); // 將reader放入dt表
                    reader.Close();
                    connection.Close();
                    DataTable totalSize1 = readTablePageNum();
                    int? totalSize2 = totalSize1.Rows[0]["COUNT"] as int?;
                    totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        public DataTable readTablePageNum()
        {
            string countQuery =
                $@" SELECT 
                        COUNT(group_id) AS COUNT
                    FROM Groups
                ";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(countQuery, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(); //執行指令串
                    DataTable dt = new DataTable();
                    dt.Load(reader); // 將reader放入dt表
                    reader.Close();
                    connection.Close();
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int CalculatePages(int totalSize, int pageSize)
        {
            int pages = totalSize / pageSize;

            if (totalSize % pageSize != 0)
                pages += 1;

            return pages;
        }


        internal class PagingLink
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
        }





        protected void Linklogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LogIn.aspx");
        }

        protected void Linklogout_Click(object sender, EventArgs e)
        {
            LoginHelper helper = new LoginHelper();
            helper.Logout();
            Response.Redirect("~/Index.aspx");
        }

        protected void Linkcreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateGroup.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text;


            string template = "?Page=1";

            if (!string.IsNullOrEmpty(name))
                template += "&name=" + name;


            Response.Redirect("Index.aspx" + template);
        }
    }
}