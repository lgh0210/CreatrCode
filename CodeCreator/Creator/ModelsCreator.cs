using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreator
{
    /// <summary>
    /// 实体层代码生成类
    /// </summary>
    public class ModelsCreator
    {
        private SQLHelper sqlHelper = null;
        private ProjectConfig config = null;


        public ModelsCreator(SQLHelper sqlHelper, ProjectConfig config)
        {
            this.sqlHelper = sqlHelper;
            this.config = config;
        }
        //根据一组表的名称生成一组实体类
        public Dictionary<string, string> CreateModels(List<string> tableNames,string database)
        {
            //用来保存sql语句
            Dictionary<string, string> sqlDic = new Dictionary<string, string>();
            foreach (string name in tableNames)
            {
                sqlDic.Add(name, $@"use {database}  select top 0 * from {name}");
            }
            //获取多张表的结构
            DataSet ds = this.sqlHelper.GetDataSet(sqlDic);
            //用来保存实体代码的集合
            Dictionary<string, string> modelClassCode = new Dictionary<string, string>();

            //循环生成实体类代码
            foreach (string tableName in sqlDic.Keys)
            {
                //获取指定表的数据结构信息
                DataTable table = ds.Tables[tableName];
                //生成实体类的代码字符串
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(SQLHelper.CreateNote());

                builder.AppendLine("using System;");
                builder.AppendLine("using System.Collections.Generic;");
                builder.AppendLine("using System.Linq;");
                builder.AppendLine("using System.Text;");
                builder.AppendLine(" ");
                builder.AppendLine($"namespace {config.ModelsNameSpace}");
                builder.AppendLine("{ ");
                builder.AppendLine(" /// <summary>");
                builder.AppendLine($" ///{tableName}实体类");
                builder.AppendLine(" /// <summary>");
                builder.AppendLine(" [Serializable]");
                builder.AppendLine($" public class {tableName}Model");
                builder.AppendLine(" {");
                //调用私有方法GetTableColumns添加到builder里
                builder.AppendLine( GetTableColumns(table));

                builder.AppendLine(" }");
                builder.AppendLine("} ");


                //添加到集合
                modelClassCode.Add(tableName, builder.ToString());
            }
            return modelClassCode;
        }
        /// <summary>
        /// 遍历表的列，并添加到内容部分
        /// </summary>
        /// <param name="table">遍历的当前列</param>
        /// <returns></returns>
        private string GetTableColumns(DataTable table)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataColumn column in table.Columns)
            {
                if (column.DataType == typeof(System.Int32))
                {
                    builder.AppendLine($"   public int {column.ColumnName} {{get;set;}}");
                }
                else if (column.DataType == typeof(System.Boolean))
                {
                    builder.AppendLine($"   public bool {column.ColumnName} {{get;set;}}");
                }
                else if (column.DataType == typeof(System.DateTime))
                {
                    builder.AppendLine($"   public DateTime {column.ColumnName} {{get;set;}}");
                }
                else
                {
                    builder.AppendLine($"   public {column.DataType.Name.ToLower()} {column.ColumnName} {{get;set;}}");
                }
            }
            return builder.ToString();
        }
    }
}
