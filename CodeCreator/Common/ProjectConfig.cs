using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreator
{
    public class ProjectConfig
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string MyDatabase { get; set; }
        /// <summary>
        /// 项目名称，用来定义项目的命名空间
        /// </summary>
        public string ProjectName { get; set; }

        public string ModelsNameSpace { get { return $"{ProjectName}.Models"; } }
        public string DALNameSpace { get { return $"{ProjectName}.DAL"; } }
        public string BLLlNameSpace { get { return $"{ProjectName}.BLL"; } }
    }
}
