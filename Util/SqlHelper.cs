
using System;
using System.Data;
using System.Data.SqlClient;
//引用数据库连接工具库

namespace OlymmpicManagementSystem.Util
{
    public class SqlHelper
    {
        public static string ConStr { get; set; }//数据库连接字符串

        public static DataTable ExecuteTable(string cmdStr)
        {
            //获取数据表的方法，返回一个DataTable
            using (SqlConnection con = new SqlConnection(ConStr))//using自动释放
            {
                con.Open();
                //建立连接
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }
        public static int ExecuteNonQuery(string cmdStr)
        {
            //修改数据表的方法，返回受影响行数
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                int rows = cmd.ExecuteNonQuery();
                if (rows <= 0)
                {
                    throw new Exception("Database operation failed");
                    //操作失败抛出异常
                }
                return rows;
            }
        }

    }
}
