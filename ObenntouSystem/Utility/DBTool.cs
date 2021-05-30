using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ObenntouSystem.Utility
{

    public class DBTool
    {
        public string WithoutScript(string words)
        {
            return Regex.Replace(words, @"[\W_]+", "");
        }
        //資料庫連結字串
        private string connectionString =
              ConfigurationManager.ConnectionStrings["ContextModel"].ConnectionString;

        /// <summary>
        /// 讀取資料庫目標資料表的目標欄位資料
        /// SELECT 欄位名稱 FROM 資料表名稱 ORDER BY 排序目標
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <returns></returns>
        public DataTable readTable(string readtablename, string[] readcolname)
        {
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //SQL語法參數化"SELECT 欄位名稱 FROM 資料表名稱 ORDER BY 排序目標"
            string queryString =
                $@" SELECT {readcoladd} FROM {readtablename};";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                //拋錯誤訊息
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 讀取資料庫目標資料表的目標欄位中符合條件的資料
        /// 不需要的參數請傳入NULL
        /// SELECT 欄位名稱 FROM 資料表名稱 "SELECT 欄位名稱 FROM 資料表名稱 條件"
        /// 條件的帶@參數名及參數值順序必須相同
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <param name="Logic">條件</param>
        /// <param name="Pname">條件的帶@參數名陣列</param>
        /// <param name="P">條件的參數值陣列</param>
        /// <returns></returns>
        public DataTable readTable(string readtablename, string[] readcolname,
            string Logic, string[] Pname, string[] P)
        {
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //SQL語法參數化"SELECT 欄位名稱 FROM 資料表名稱 條件"
            string queryString =
                $@" SELECT {readcoladd} FROM {readtablename}
                    {Logic};";

            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //傳入參數至@目標欄位
                if (Pname != null && P != null
                    && Pname.Length != 0 && P.Length != 0)
                {
                    for (int i = 0; i < Pname.Length; i++)
                        command.Parameters.AddWithValue(Pname[i], P[i]);  //將command指令串內的@目標欄位以傳入參數取代
                }
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

        /// <summary>
        /// 往資料庫中插入新資料列
        /// INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)
        /// 傳入的@欄位和參數的順序必須相同
        /// </summary>
        /// <param name="inserttablename">目標資料表名稱</param>
        /// <param name="insertcolname">目標欄位名稱的陣列</param>
        /// <param name="insertcolname_P">目標欄位名稱帶有@的陣列</param>
        /// <param name="insert_P">需給予@欄位之參數值的集合</param>
        public void InsertTable(string inserttablename, string[] insertcolname,
            string[] insertcolname_P, List<string> insert_P)
        {
            //先宣告SQL語法字串為空字串
            string queryString = string.Empty;
            //將接過來的目標欄位名稱及目標欄位名稱帶有@的陣列各自用「,」連接成一個字串
            string insertcolum = string.Join(",", insertcolname);
            //將參數的集合轉為陣列
            string[] puserinsert = insert_P.ToArray();
            //宣告新的@陣列為集合
            List<string> Newinsertcolname_P = new List<string>();
            //判斷傳過來目標欄位名稱帶有@的陣列以及參數的集合大小，創建對應的SQL語法
            //若參數大於@陣列則為新增多值 反之則為新增單值
            if (insert_P.Count > insertcolname_P.Length)
            {
                //將接過來的目標欄位名稱帶有@的陣列宣告為空字串
                string insertparameter = string.Empty;
                //跑參數/@陣列次數的迴圈
                for (int i = 0; i < (insert_P.Count / insertcolname_P.Length); i++)
                {
                    //每一筆加入加了i的@陣列
                    if (i == 0)
                    {
                        //將新的@參數加入@集合
                        foreach (var item in insertcolname_P)
                        {
                            Newinsertcolname_P.Add($"{item}{i}");
                        }
                        //第一筆前不用逗點
                        insertparameter += $"({string.Join($"{i},", insertcolname_P)}{i})";
                    }
                    else
                    {
                        //將新的@參數加入@集合
                        foreach (var item in insertcolname_P)
                        {
                            Newinsertcolname_P.Add(item + i);
                        }
                        //第二筆之後前面加逗點
                        insertparameter += $",({string.Join($"{i},", insertcolname_P)}{i})";
                    }
                    //SQL語法參數化"INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)"
                }
                queryString =
                  $@" INSERT INTO {inserttablename}
                         ({insertcolum})
                   VALUES
                         {insertparameter}";
            }
            else
            {
                //將接過來的目標欄位名稱帶有@的陣列各自用「,」連接成一個字串
                string insertparameter = string.Join(",", insertcolname_P);
                //將新的@參數加入@集合
                foreach (var item in insertcolname_P)
                {
                    Newinsertcolname_P.Add(item);
                }
                //SQL語法參數化"INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)"
                queryString =
                    $@" INSERT INTO {inserttablename}
                         ({insertcolum})
                   VALUES
                         ({insertparameter})";
            }
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                command.Transaction = sqlTransaction;
                try
                {
                    //利用迴圈將參數一個一個放進@欄位
                    for (int i = 0; i < insert_P.Count; i++)
                    {
                        command.Parameters.AddWithValue($"{Newinsertcolname_P[i]}", puserinsert[i]);
                    }
                    command.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新資料庫中的資料列
        /// UPDATE 資料表名稱 SET 欄位名稱=@欄位名稱... WHERE Where條件(欄位名稱=@欄位名稱)
        /// 傳入的@欄位和參數的順序必須相同
        /// updatecolname_P最後一個值須為Where的目標@欄位名稱
        /// 只能下一種WHERE條件
        /// </summary>
        /// <param name="updatetablename">目標資料表名稱</param>
        /// <param name="updatecol_Logic">欲更新的"欄位名稱=@欄位名稱"之字串陣列</param>
        /// <param name="Where_Logic">Where條件</param>
        /// <param name="updatecolname_P">目標欄位名稱及Where條件欄位帶有@的陣列</param>
        /// <param name="update_P">需給予@欄位之參數值的集合</param>
        public void UpdateTable(string updatetablename, string[] updatecol_Logic, string Where_Logic, string[] updatecolname_P, List<string> update_P)
        {
            //將接過來的陣列用「,」連接成一個字串
            string updatecolum = string.Join(",", updatecol_Logic);
            //將user輸入的集合轉為陣列
            string[] puserupdate = update_P.ToArray();
            //SQL語法參數化"UPDATE 資料表名稱 SET 欄位名稱=@欄位名稱... WHERE Where條件(欄位名稱=@欄位名稱)"
            string queryString =
                $@"   UPDATE {updatetablename}
                        SET  {updatecolum}
                        WHERE {Where_Logic} ";

            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction(); //塞方法進去sqlTransaction 起始 Commit() 及Rollback() 開頭 
                command.Transaction = sqlTransaction; //開始交易 
                try
                {
                    //利用迴圈將參數一個一個放進@欄位
                    for (int i = 0; i < updatecolname_P.Length; i++)
                    {
                        command.Parameters.AddWithValue($"{updatecolname_P[i]}", puserupdate[i]);
                    }
                    command.ExecuteNonQuery();
                    sqlTransaction.Commit();  //command.ExecuteNonQuery(); 成功 進入sqlTransaction.Commit() 真正寫進資料庫
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback(); //失敗的話進入此sqlTransaction.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 刪除資料庫中的資料列
        /// DELETE FROM 資料表名稱 WHERE 欄位名稱=@欄位名稱
        /// </summary>
        /// <param name="deletetablename">目標資料表名稱</param>
        /// <param name="deletecolname">目標欄位名稱</param>
        /// <param name="deletecolname_P">目標欄位名稱帶有@的字串</param>
        /// <param name="delete_P">需給予@欄位之參數值</param>
        public void DeleteTable(string deletetablename, string deletecolname, string deletecolname_P, string delete_P)
        {
            //SQL語法參數化"DELETE FROM 資料表名稱 WHERE 欄位名稱=@欄位名稱"
            string queryString = $"DELETE FROM {deletetablename} WHERE {deletecolname} = {deletecolname_P}";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //傳入參數至Where條件的@目標欄位
                command.Parameters.AddWithValue(deletecolname_P, delete_P);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }




    }
}