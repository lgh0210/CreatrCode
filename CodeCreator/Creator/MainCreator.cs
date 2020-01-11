using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeCreator
{
    /// <summary>
    /// 代码生成器程序类
    /// </summary>
    public class MainCreator
    {
        private SQLHelper sqlHelper = null;
        private ProjectConfig config = null;
        public string Database { get; set; }
        public string ProjectName { get; set; }

        //定义3个生成器对象
        private ModelsCreator modelCreator = null;
        private DALCreator dalCreator = null;
        private BLLCreator bllCreator = null;
        

        //定义3个集合用来保存生成的代码类（定义为属性：供界面使用）
        public Dictionary<string,string> ModelClassDic { get; set; }
        public Dictionary<string, string> DALClassDic { get; set; }
        public Dictionary<string, string> BLLClassDic { get; set; }

        public MainCreator(string server,string uid,string pwd)
        {
            //实例化通用类
            this.sqlHelper = new SQLHelper(server, "master", uid, pwd);
        }
        private List<string> tableNames = null;
        private List<string> dataBases = null;
        
        //public List<string> TableNames
        //{
        //    get {
        //        if (tableNames == null)//如果数据表名称集合数据为null，则需要从数据库中动态查询
        //        {
        //            tableNames = this.sqlHelper.GetAllTableNames(Database);
        //        }
        //        return tableNames;
        //    }
        //}
        public List<string> DataBases
        {
            get
            {
                if (dataBases == null)
                {
                    dataBases = this.sqlHelper.GetAllDatabases();
                }
                return dataBases;
            }
        }

        /// <summary>
        /// 代码生成的入口
        /// </summary>
        /// <param name="path">代码保存路径</param>
        public void StartCreatorCode(string path)
        {

            this.config = new ProjectConfig { ProjectName = ProjectName };

            //实例化实体类，数据访问类，业务逻辑类
            this.modelCreator = new ModelsCreator(sqlHelper, config);
            this.dalCreator = new DALCreator(sqlHelper, config, Database);
            this.bllCreator = new BLLCreator(sqlHelper, config);

          
            tableNames = this.sqlHelper.GetAllTableNames(Database);
            
          
            //生成实体类，数据访问类，业务逻辑类
            ModelClassDic =modelCreator.CreateModels(tableNames, Database);
            DALClassDic = dalCreator.CreateDAL(tableNames, Database);
            BLLClassDic = bllCreator.CreateBLL(tableNames, Database);

            //创建保存路径
            path += "\\" + config.ProjectName;
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "\\Models");
            Directory.CreateDirectory(path + "\\DAL");
            Directory.CreateDirectory(path + "\\BLL");

            SaveCodeToFile(path + "\\Models\\", ModelClassDic, "Model");
            SaveCodeToFile(path + "\\DAL\\", DALClassDic, "Sql");
            SaveCodeToFile(path + "\\BLL\\", BLLClassDic, null);



        }
        /// <summary>
        /// 内部方法：生成代码保存到文件
        /// </summary>
        /// <param name="path">对用路径</param>
        /// <param name="dic">代码字符串集合</param>
        /// <param name="suffix">对应后缀名</param>
        private void SaveCodeToFile(string path,Dictionary<string,string>dic,string suffix)
        {
            //保存类字符串到具体的文件中
            foreach (string className in dic.Keys)
            {
                FileStream fs = new FileStream($"{path}{className}{suffix}.cs", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(dic[className]);
                sw.Close();
                fs.Close();
            }
        }

    }
}
