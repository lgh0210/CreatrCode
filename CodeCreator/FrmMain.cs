using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCreator
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            //初始化生成按钮状态为false
            this.btnCreator.Enabled = false;
        }
        private MainCreator mainCreator = null;
        //测试连接数据库
        private void btnLoginDatabase_Click(object sender, EventArgs e)
        {
            this.mainCreator = new MainCreator(this.txtServer.Text.Trim(),
                //this.txtDatabase.Text.Trim(),
                this.txtUserId.Text.Trim(),
                this.txtPwd.Text.Trim()
                //this.txtProject.Text.Trim()
                );
            //开启生成代码按钮
            this.btnCreator.Enabled = true;
            this.cobTableName.Items.Clear();//清空当前表名下拉列表

            List<string> dataBases = mainCreator.DataBases;
            if (dataBases != null) MessageBox.Show("连接成功", "提示信息");
            //添加表名下拉列表
            this.cobTableName.Items.AddRange(dataBases.ToArray());
            this.cobTableName.SelectedIndex = -1;//默认选中第一个数据表

        }
        //生成代码按钮
        private void btnCreator_Click(object sender, EventArgs e)
        {
            if (cobTableName.SelectedIndex == -1)
            {
                MessageBox.Show("请选择数据库", "提示信息");
                return;
            }
            if (txtProject.Text.Trim() == "" && txtProject.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填写项目名称", "提示信息");
                return;
            }


            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            string path = string.Empty;
            if (result == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            try
            {
                this.mainCreator.Database = this.cobTableName.SelectedItem.ToString();
                this.mainCreator.ProjectName = this.txtProject.Text.Trim();
                
                this.mainCreator.StartCreatorCode(path);
                MessageBox.Show("生成成功", "提示信息");
            }
            catch (Exception ex)
            {

                MessageBox.Show("生成失败："+ex.Message, "提示信息");
            }
        }
        
      
    }
}
