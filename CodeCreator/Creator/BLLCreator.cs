using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeCreator
{
    public class BLLCreator
    {
        private SQLHelper sqlHelper = null;
        private ProjectConfig config = null;
        public BLLCreator(SQLHelper sqlHelper, ProjectConfig config)
        {
            this.sqlHelper = sqlHelper;
            this.config = config;
        }

        public Dictionary<string, string> CreateBLL(List<string> tableNames,string database)
        {
            Dictionary<string, string> sqlDic = new Dictionary<string, string>();
            foreach (string name in tableNames)
            {
                sqlDic.Add(name, $@"use {database} select top 0 * from {name}");
            }
            DataSet ds = this.sqlHelper.GetDataSet(sqlDic);
            Dictionary<string, string> bllClassCode = new Dictionary<string, string>();
            foreach (string tableName in sqlDic.Keys)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(SQLHelper.CreateNote());
               
                builder.AppendLine("using System;");
                builder.AppendLine("using System.Configuration;");
                builder.AppendLine("using System.Collections.Generic;");
                builder.AppendLine("using System.Test;");
                builder.AppendLine("using System.Data;");
                builder.AppendLine($"using {config.ModelsNameSpace};");
                builder.AppendLine($"using {config.DALNameSpace};");
                builder.AppendLine(" ");
                builder.AppendLine($"namespace {config.ProjectName}.BLL ");
                builder.AppendLine("{ ");
                builder.AppendLine($"\t public class {tableName} ");
                builder.AppendLine("\t {");
                //内部方法
                builder.AppendLine($"\t\t private {tableName}Sql dal = new {tableName}Sql();");

                //添加方法：
                builder.AppendLine("\t\t\t " + CreateInsert(ds.Tables[tableName]));
                //修改方法：
                builder.AppendLine("\t\t\t " + CreateUpdate(ds.Tables[tableName]));
                //删除方法：
                builder.AppendLine("\t\t\t " + CreateDelete(ds.Tables[tableName]));
                //查询单一方法：
                builder.AppendLine("\t\t\t " + CreateGetModel(ds.Tables[tableName]));
                //查询List集合方法：
                builder.AppendLine("\t\t\t " + CreateGetList(ds.Tables[tableName]));


                builder.AppendLine("\t }");
                builder.AppendLine("} ");

                bllClassCode.Add(tableName, builder.ToString());
            }
            return bllClassCode;
        }

        #region 生成业务代码
        private string CreateInsert(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public int Insert({table.TableName} model)").AppendLine("{ ");
            sqlBuilder.AppendLine("return dal.Insert(model);").AppendLine("}");
            return sqlBuilder.ToString();

        }
        private string CreateUpdate(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public int Update({table.TableName} model)").AppendLine("{ ");
            sqlBuilder.AppendLine("return dal.Update(model);").AppendLine("}");
            return sqlBuilder.ToString();

        }
        private string CreateDelete(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public int Delete(int id)").AppendLine("{ ");
            sqlBuilder.AppendLine("return dal.Delete(id);").AppendLine("}");
            return sqlBuilder.ToString();

        }
        private string CreateGetModel(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public {table.TableName} GetModel(int id)").AppendLine("{ ");
            sqlBuilder.AppendLine("return dal.GetModel(id);").AppendLine("}");
            return sqlBuilder.ToString();

        }
        private string CreateGetList(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public List<{table.TableName}> GetList()").AppendLine("{ ");
            sqlBuilder.AppendLine("return dal.GetList();").AppendLine("}");
            return sqlBuilder.ToString();

        }
        #endregion

    }
}
