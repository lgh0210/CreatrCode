using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeCreator
{
    /// <summary>
    /// 业务逻辑类生成代码
    /// </summary>
    public class DALCreator
    {
        private SQLHelper sqLHelper = null;
        private ProjectConfig config = null;

        public DALCreator(SQLHelper sqlHelper, ProjectConfig config, string database)
        {
            this.sqLHelper = sqlHelper;
            this.config = config;
        }
        public Dictionary<string, string> CreateDAL(List<string> tableNames, string database)
        {
            Dictionary<string, string> sqlDic = new Dictionary<string, string>();
            foreach (string name in tableNames)
            {
                sqlDic.Add(name, $@"use {database}  select top 0  * from {name}");
            }
            DataSet ds = sqLHelper.GetDataSet(sqlDic);

            Dictionary<string, string> dalClassCode = new Dictionary<string, string>();
            foreach (string tableName in sqlDic.Keys)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(SQLHelper.CreateNote());
                builder.AppendLine("using System;");
                builder.AppendLine("using System.Collections.Generic;");
                builder.AppendLine("using System.Text;");
                builder.AppendLine("using System.Data;");
                builder.AppendLine("using System.Data.sqlClient;");
                builder.AppendLine($"using {config.ModelsNameSpace};");
                builder.AppendLine(" ");
                builder.AppendLine($"namespace {config.DALNameSpace}");
                builder.AppendLine("{");
                builder.AppendLine(" ");
                builder.AppendLine(" /// <summary>");
                builder.AppendLine($" ///{tableName}业务访问类");
                builder.AppendLine(" /// <summary>");
                builder.AppendLine($" public class {tableName}Sql");
                builder.AppendLine(" {");

                //插入方法
                builder.AppendLine(CreateInsertMethod(ds.Tables[tableName], database));
                //修改方法
                builder.AppendLine(CreateUpdateMethod(ds.Tables[tableName], database));
                //删除方法
                builder.AppendLine(CreateDeleteMethod(ds.Tables[tableName], database));
                //查询单个实体的方法
                builder.AppendLine(CreateSigleModelMethod(ds.Tables[tableName], database));
                //查询列表的方法
                builder.AppendLine(CreateGetListMothod(ds.Tables[tableName]));
                //查询数据绑定
                builder.AppendLine(CreateGetDataInfo(ds.Tables[tableName]));




                builder.AppendLine("    }");
                builder.AppendLine("}");
                dalClassCode.Add(tableName, builder.ToString());
            }
            return dalClassCode;
        }

        #region 生成业务代码
        //生成添加方法
        private string CreateInsertMethod(DataTable table,string database)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine($"public int Insert({table.TableName} model)");
            sqlBuilder.AppendLine("{");
            sqlBuilder.Append($"    string sql=\"insert into {table.TableName}(");
            //获取非自增列的项
            string primaryKey = GetPrimarykey(table.TableName, database);
            List<string> colNames = this.GetColumnsNames(table, true);
            //遍历生成插入的值
            for (int i = 0; i < colNames.Count; i++)
            {
                if (colNames[i] != primaryKey)
                {
                    sqlBuilder.Append(colNames[i]);
                    if (i != colNames.Count - 1) sqlBuilder.Append(",");
                    else sqlBuilder.Append(") values(");
                }

            }
            //遍历生成参数值
            for (int j = 0; j < colNames.Count; j++)
            {
                sqlBuilder.Append($"@{colNames[j]}");
                if (j != colNames.Count - 1) sqlBuilder.Append(",");
                else sqlBuilder.Append(")\"; ");
            }

            //生成参数部分
            sqlBuilder.AppendLine(" ");
            sqlBuilder.AppendLine("  SqlParameter[] param = new SqlParameter[]");
            sqlBuilder.AppendLine(" {");
            for (int k = 0; k < colNames.Count; k++)
            {
                if (k < colNames.Count)
                    sqlBuilder.AppendLine($"\t\t\t new SqlParameter(\"@{colNames[k]}\",model.{colNames[k]}),");
                else
                    sqlBuilder.AppendLine($"\t\t\t new SqlParameter(\"@{colNames[k]}\",model.{colNames[k]})");
            }

            sqlBuilder.AppendLine(" };");
            sqlBuilder.AppendLine("     return SQLHelper.ExecuteNonQuery(sql, param);");
            sqlBuilder.AppendLine(" }");

            return sqlBuilder.ToString();
        }

        //生成修改方法
        private string CreateUpdateMethod(DataTable table, string database)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine($"public int Update({table.TableName} model)").AppendLine("{");
            sqlBuilder.Append($"string sql=\"update {table.TableName} set ");

            string primaryKey = GetPrimarykey(table.TableName, database);//获取表的主键名称
            List<string> colNames = this.GetColumnsNames(table, true);
            for (int i = 0; i < colNames.Count; i++)
            {
                if (colNames[i] != primaryKey)
                {
                    if (i != table.Columns.Count - 1)
                    {
                        sqlBuilder.Append($"{colNames[i] }=@{colNames[i]},");
                    }
                    else
                    {
                        sqlBuilder.Append($"{colNames[i] }=@{colNames[i]} ");
                    }
                }
            }
            sqlBuilder.Append($"where {primaryKey}=@{primaryKey}\";");
            //生成参数
            sqlBuilder.AppendLine(" ");
            sqlBuilder.AppendLine("  SqlParameter[] param = new SqlParameter[]");
            sqlBuilder.AppendLine(" {");
            for (int k = 0; k < colNames.Count; k++)
            {
                if (k != colNames.Count - 1)
                    sqlBuilder.AppendLine($"\t\t\t new SqlParameter(\"@{colNames[k]}\",model.{colNames[k]}),");
                else
                    sqlBuilder.AppendLine($"\t\t\t new SqlParameter(\"@{colNames[k]}\",model.{colNames[k]})");
            }

            sqlBuilder.AppendLine(" };");
            sqlBuilder.AppendLine("     return SQLHelper.ExecuteNonQuery(sql, param);");
            sqlBuilder.AppendLine(" }");

            return sqlBuilder.ToString();
        }

        //生成删除方法
        private string CreateDeleteMethod(DataTable table, string database)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine(" public int Delete(int id)").AppendLine("{");
            string primaryKey = this.GetPrimarykey(table.TableName, database);
            sqlBuilder.AppendLine($"string sql=\"delete from {table.TableName} where {primaryKey}=@{primaryKey}\";");
            sqlBuilder.AppendLine("SqlParameter[] param = new SqlParameter[]").AppendLine("{");
            sqlBuilder.AppendLine($"\t\t\t new SqlParameter(\"@{primaryKey}\",id)").AppendLine("};");
            sqlBuilder.Append("return SQLHelper.ExecuteNonQuery(sql, param);");
            sqlBuilder.AppendLine("}");
            return sqlBuilder.ToString();
        }

        //生成查询返回单一结果的查询
        private string CreateSigleModelMethod(DataTable table, string database)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public {table.TableName} GetModel(int id)").AppendLine("{");
            sqlBuilder.Append($"string sql = \"SELECT ");
            string primaryKey = this.GetPrimarykey(table.TableName, database);
            List<string> columnNames = this.GetColumnsNames(table, false);
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i != columnNames.Count - 1)
                {
                    sqlBuilder.Append($"{columnNames[i]},");
                }
                else
                {
                    sqlBuilder.Append($"{columnNames[i]} from {table.TableName} where {primaryKey}=@{primaryKey}\";");
                }

            }
            sqlBuilder.AppendLine(" ");
            sqlBuilder.AppendLine("SqlParameter[] param = new SqlParameter[]").AppendLine("{").AppendLine($"\t\t\t new SqlParameter(\"@{primaryKey}\",id)").AppendLine("};");
            sqlBuilder.AppendLine("SqlDataReader objReader = SQLHelper.ExecuteReader(sql, param);").AppendLine($" {table.TableName} model = null;").AppendLine("if (objReader.Read())").AppendLine("{").AppendLine("model =GerDataInfo(objReader);").AppendLine("}").AppendLine(" return model;").AppendLine("}");
            return sqlBuilder.ToString();
        }

        //生成获取实体列表的方法
        private string CreateGetListMothod(DataTable table)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine($"public {table.TableName} GetListAllModel()").AppendLine("{");
            sqlBuilder.Append($"string sql = \"SELECT ");
            //string primaryKey = this.GetPrimarykey(table.TableName);
            List<string> columnNames = this.GetColumnsNames(table, false);
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i != columnNames.Count - 1)
                {
                    sqlBuilder.Append($"{columnNames[i]},");
                }
                else
                {
                    sqlBuilder.Append($"{columnNames[i]} from {table.TableName}\";");
                }

            }
            sqlBuilder.AppendLine(" ");
            sqlBuilder.AppendLine("SqlDataReader objReader = SQLHelper.ExecuteReader(sql, param);").AppendLine($" List<{table.TableName}> list = new List<{table.TableName}>();").AppendLine("while (objReader.Read())").AppendLine("{").AppendLine("list.Add( GerDataInfo(objReader));").AppendLine("}").AppendLine(" return model;").AppendLine("}");
            return sqlBuilder.ToString();

        }

        private string CreateGetDataInfo(DataTable table)
        {
            StringBuilder builder = new StringBuilder();
            List<string> columnNames = this.GetColumnsNames(table, false);
            builder.AppendLine($"public {table.TableName} GetDataInfo(SqlDataReader reader)").AppendLine("{").AppendLine($"{table.TableName} model=null;");
            builder.AppendLine("if(reader.Read())").AppendLine("{").AppendLine($"model=new {table.TableName} ").AppendLine("{");
            //生成转义字符串
            builder.AppendLine(GetTableColumns(table));

            builder.AppendLine("};").AppendLine("}").AppendLine("return model;\r\n}");
            return builder.ToString();

        }

        #endregion


        #region 辅助方法
        private string GetTableColumns(DataTable table)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataColumn column in table.Columns)
            {

                if (column.DataType == typeof(System.Int32))
                {
                    builder.AppendLine($"\t\t {column}=(int)reader[\"{ column}\"],");
                }
                else if (column.DataType == typeof(System.Boolean))
                {
                    builder.AppendLine($"\t\t {column}=(bool)reader[\"{ column}\"],");
                }
                else if (column.DataType == typeof(System.DateTime))
                {
                    builder.AppendLine($"\t\t {column}=(DataTime)reader[\"{ column}\"],");
                }
                else if (column.DataType == typeof(System.String))
                {
                    builder.AppendLine($"\t\t {column}=(string)reader[\"{ column}\"],");
                }
                else
                {
                    builder.AppendLine($"\t\t {column}=({column.DataType.ToString().Split('.')[1].ToLower()})reader[\"{ column}\"],");
                }
            }
            return builder.ToString();
        }

        //返回集合形式的列名
        private List<string> GetColumnsNames(DataTable table, bool isAutoIncrement)
        {
            List<string> colNames = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                if (isAutoIncrement)
                {
                    if (!column.Unique)
                    {
                        colNames.Add(column.ColumnName);
                    }

                }
                else
                {
                    colNames.Add(column.ColumnName);
                }

            }
            return colNames;
        }

        //获取主键名称
        private string GetPrimarykey(string tableName, string database)
        {
            SqlDataReader objReader = this.sqLHelper.GetReader($"use {database} exec sp_pkeys @table_Name= N'{tableName}'");
            string primarykey = string.Empty;
            if (objReader.Read())
            {
                primarykey = objReader["COLUMN_NAME"].ToString();
            }
            objReader.Close();
            return primarykey;
        }
        #endregion

    }
}
