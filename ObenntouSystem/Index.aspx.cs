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


            if (!IsPostBack)
            {
                string name = Request.QueryString["Name"];
                if (!string.IsNullOrEmpty(name))
                    this.txtName.Text = name;
                string type = Request.QueryString["Type"];
                if (!string.IsNullOrEmpty(type))
                    this.DDL_Search.SelectedIndex = Convert.ToInt32(type);
            }

            if (info != null)
            {
                if (info.user_id == 1)
                {
                    LinkBackstage.Visible = true;
                }
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
            string name = Request.QueryString["Name"];


            int totalSize;
            int _pageSize = 10;

            DataTable Comdata = readTableForPage(name, out totalSize, pIndex, _pageSize);
            List<IndexModel> lastresult = new List<IndexModel>();
            if (Comdata.Rows.Count > 0)
            {
                Nothingdiv.Visible = false;
                foreach (DataRow item in Comdata.Rows)
                {
                    IndexModel indexModel = new IndexModel();
                    indexModel.group_id = Convert.ToInt32(item["group_id"]);
                    indexModel.group_name = item["group_name"].ToString();
                    indexModel.group_pic = item["group_pic"].ToString();
                    indexModel.group_type = item["group_type"].ToString();
                    indexModel.user_name = item["user_name"].ToString();
                    indexModel.omise_name = item["omise_name"].ToString();
                    indexModel.omise_id = Convert.ToInt32(item["omise_id"]);

                    string[] uccolname = { "COUNT(order_userid) as peoplenum" };
                    string[] uccolnamep = { "@order_groupid" };
                    string[] ucp = { indexModel.group_id.ToString() };

                    string uclogic = @"
                           (SELECT order_userid
                            FROM Orders
                            JOIN Groups ON Orders.order_groupid=Groups.group_id
                            WHERE Orders.order_deldate IS NULL AND Orders.order_groupid=@order_groupid
							GROUP BY order_userid) as temp
                            ";
                    DataTable ucdata = dBTool.readTable(uclogic, uccolname, "", uccolnamep, ucp);
                    indexModel.peoplenum =Convert.ToInt32(ucdata.Rows[0]["peoplenum"]);

                    string[] dcolname = { "Dishes.dish_name" };
                    string[] dcolnamep = { "@omise_id" };
                    string[] dp = { indexModel.omise_id.ToString() };

                    string dlogic = @"
                            JOIN OmiseMaster ON Dishes.dish_omiseid=OmiseMaster.omise_id
                            WHERE Dishes.dish_deldate IS NULL AND Dishes.dish_omiseid=@omise_id
                            ";
                    DataTable ddata = dBTool.readTable("Dishes", dcolname, dlogic, dcolnamep, dp);
                    List<IndexDish> indexDishes = new List<IndexDish>();
                    if (ddata.Rows.Count > 0)
                    {

                        foreach (DataRow ditem in ddata.Rows)
                        {
                            IndexDish indexDish = new IndexDish();
                            indexDish.dish_name = ditem["dish_name"].ToString();
                            indexDishes.Add(indexDish);
                        }

                    }
                    indexModel.dishes = indexDishes;

                    lastresult.Add(indexModel);
                }
            }
            else
            {
                Nothingdiv.Visible = true;
            }

            int pages = CalculatePages(totalSize, _pageSize);
            List<PagingLink> pagingList = new List<PagingLink>();
            HLfirst.NavigateUrl = $"Index.aspx{this.GetQueryString(false, 1)}";
            HLlast.NavigateUrl = $"Index.aspx{this.GetQueryString(false, pages)}";
            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"Index.aspx{this.GetQueryString(false, i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });
            }

            this.repPaging.DataSource = pagingList;
            this.repPaging.DataBind();

            repGroup.DataSource = lastresult;
            repGroup.DataBind();

        }


        private string GetQueryString(bool includePage, int? pageIndex)
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];
            string name = Request.QueryString["Name"];
            //----- Get Query string parameters -----


            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(page) && includePage)
                conditions.Add("Page=" + page);

            conditions.Add("Type=" + DDL_Search.SelectedValue);

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
            string searchcol;
            switch (DDL_Search.SelectedIndex)
            {
                case 0:
                    searchcol = "Groups.group_name";
                    break;
                case 1:
                    searchcol = "OmiseMaster.omise_name";
                    break;
                default:
                    searchcol = "group_name";
                    break;
            }
            string queryString;
            if (DDL_Search.SelectedIndex != 2)
            {
                 queryString =
                              $@" 
                                  SELECT TOP {pageSize} * FROM
                                  (
                                      SELECT 
                                          ROW_NUMBER() OVER(ORDER BY group_id DESC) AS RowNumber,
                                           Groups.group_id,
                                           Groups.group_name, 
                                           Groups.group_pic,
                                           Groups.group_type,
                                           Users.user_name,
				                 	      OmiseMaster.omise_name,
				                 		  OmiseMaster.omise_id
                                      FROM Groups
                                      JOIN Users ON Groups.group_userid=Users.user_id
                                      JOIN OmiseMaster ON Groups.group_omiseid=OmiseMaster.omise_id
                                      WHERE Groups.group_deldate IS NULL AND {searchcol} LIKE '%' + @name + '%'
                                  ) AS TempT
                                  WHERE RowNumber > {pageSize * (currentPage - 1)}
                                  ORDER BY group_id DESC
                              ";
            }
            else
            {
                queryString =
                             $@" 
                                  SELECT TOP {pageSize} * FROM
                                  (
                                      SELECT 
                                         ROW_NUMBER() OVER(ORDER BY group_id DESC) AS RowNumber,
                                         Groups.group_id,
                                         Groups.group_name, 
                                         Groups.group_pic,
                                         Groups.group_type,
						                 Users.user_name,
						                 OmiseMaster.omise_name,
						                 OmiseMaster.omise_id,
						                 Dishes.dish_name
                                      FROM Groups
                                      JOIN Users ON Groups.group_userid=Users.user_id
                                      JOIN OmiseMaster ON Groups.group_omiseid=OmiseMaster.omise_id
                                      JOIN Dishes ON OmiseMaster.omise_id=Dishes.dish_omiseid
                                      WHERE group_deldate IS NULL AND dish_name=@name
                                   ) AS TempT
                                      WHERE RowNumber > {pageSize * (currentPage - 1)}
                                      ORDER BY group_id DESC
                              ";
            }


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
                    DataTable totalSize1 = readTablePageNum(name);
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



        public DataTable readTablePageNum(string name)
        {
            string searchcol;
            switch (DDL_Search.SelectedIndex)
            {
                case 0:
                    searchcol = "Groups.group_name";
                    break;
                case 1:
                    searchcol = "OmiseMaster.omise_name";
                    break;
                default:
                    searchcol = "group_name";
                    break;
            }
            string countQuery;
            if (DDL_Search.SelectedIndex != 2)
            {
                countQuery =
                     $@" SELECT 
                             COUNT(group_id) AS COUNT
                         FROM Groups
                         JOIN Users ON Groups.group_userid=Users.user_id
                         JOIN OmiseMaster ON Groups.group_omiseid=OmiseMaster.omise_id
                         WHERE Groups.group_deldate IS NULL AND {searchcol} LIKE '%' + @name + '%'
                     ";
            }
            else
            {
                countQuery =
                     $@" SELECT 
                             COUNT(group_id) AS COUNT
                         FROM Groups
                         JOIN Users ON Groups.group_userid=Users.user_id
                         JOIN OmiseMaster ON Groups.group_omiseid=OmiseMaster.omise_id
                         JOIN Dishes ON OmiseMaster.omise_id=Dishes.dish_omiseid
                         WHERE group_deldate IS NULL AND dish_name=@name
                     ";

            }
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(countQuery, connection);
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


        private class IndexModel
        {
            public int group_id { get; set; }
            public string group_name { get; set; }
            public string group_pic { get; set; }
            public string group_type { get; set; }
            public string user_name { get; set; }
            public string omise_name { get; set; }
            public int omise_id { get; set; }
            public int peoplenum { get; set; }
            public List<IndexDish> dishes { get; set; }
        }

        private class IndexDish
        {
            public string dish_name { get; set; }
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


            string template = $"?Page=1&Type={DDL_Search.SelectedValue}";

            if (!string.IsNullOrEmpty(name))
                template += "&Name=" + name;




            Response.Redirect("Index.aspx" + template);

        }

        protected void repGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("Rep_Indexdish") as Repeater;
                HiddenField hf = e.Item.FindControl("HFdishomise") as HiddenField;

                string[] dcolname = { "Dishes.dish_name" };
                string[] dcolnamep = { "@omise_id" };
                string[] dp = { hf.Value };

                string dlogic = @"
                            JOIN OmiseMaster ON Dishes.dish_omiseid=OmiseMaster.omise_id
                            WHERE Dishes.dish_deldate IS NULL AND Dishes.dish_omiseid=@omise_id
                            ";
                DataTable ddata = dBTool.readTable("Dishes", dcolname, dlogic, dcolnamep, dp);

                rep.DataSource = ddata;
                rep.DataBind();

            }
        }

        protected void LinkBackstage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Backstage/BackIndex.aspx");
        }
    }
}