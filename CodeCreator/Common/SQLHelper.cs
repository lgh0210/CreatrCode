using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeCreator
{
    public class SQLHelper
    {
        //数据库连接字符串
        private string ConnString = string.Empty;
        //使用SQLHelper构造函数初始化数据库连接字符串
        public SQLHelper(string server, string database, string uid, string pwd)
        {
            this.ConnString = $"Server={server};DataBase={database};Uid={uid};pwd={pwd}";
        }


        public void ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 返回只读数据集的查询方法
        /// </summary>
        /// <param name="sql">执行SQL语句</param>
        /// <returns>DataReader对象</returns>
        public SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 使用存储过程获取只读数据集的查询方法
        /// </summary>
        /// <param name="sqlName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>DataReader对象</returns>
        public SqlDataReader GetReaderByProceduce(string sqlName, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = sqlName
            };
            if (param != null) cmd.Parameters.AddRange(param);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 返回单个数据表结构的方法
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            DataSet ds = new DataSet();
            //在代码生成器中，需要使用数据表的结构，所以有一下设置
            da.FillSchema(ds, SchemaType.Source);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        /// <summary>
        /// 返回多个dataTable表结构的方法
        /// </summary>
        /// <param name="sqlDic"></param>
        /// <returns></returns>
        public DataSet GetDataSet(Dictionary<string, string> sqlDic)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            DataSet ds = new DataSet();
            //循环查询将表的结构添加到ds中
            int i = 0;
            foreach (string tableName in sqlDic.Keys)
            {
                cmd.CommandText = sqlDic[tableName];//要执行的SQL语句
                //da.FillSchema(ds, SchemaType.Source, tableName);
                da.Fill(ds, tableName);
            }

            conn.Close();
            return ds;
        }
        /// <summary>
        /// 获取当前数据库所有数据表的名称
        /// </summary>
        /// <returns>数据表List集合</returns>
        public List<string> GetAllTableNames(string database)
        {
            string sqlSelect = $"use { database} select name from sysobjects where Xtype='u' order by name";
            SqlDataReader dataReader = GetReader(sqlSelect);
            List<string> tableNames = new List<string>();
            while (dataReader.Read())
            {
                tableNames.Add(dataReader["name"].ToString());
            }
            dataReader.Close();
            return tableNames;

        }

        public List<string> GetAllDatabases()
        {
            string sql = "select name from sysdatabases order by name";
            SqlDataReader dataReader = GetReader(sql);
            List<string> databases = new List<string>();
            while (dataReader.Read())
            {
                databases.Add(dataReader["name"].ToString());
            }
            dataReader.Close();
            return databases;
        }

        /// <summary>
        /// 生成文件注释部分
        /// </summary>
        /// <returns></returns>
        public static string CreateNote()
        {
            StringBuilder note = new StringBuilder();
            note.AppendLine("/*");
            note.AppendLine(" * CreateTime:" + DateTime.Now);
            note.AppendLine(" * Developer : ligaohui");
            note.AppendLine(" * Url : none");
            note.AppendLine(" * Email : 642709100@qq.com");
            note.AppendLine(" */");
            return note.ToString();
        }
    }
}
