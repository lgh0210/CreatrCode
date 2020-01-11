namespace CodeCreator
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.connString = new System.Windows.Forms.GroupBox();
            this.btnLoginDatabase = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameSpace = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobTableName = new System.Windows.Forms.ComboBox();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.tableName = new System.Windows.Forms.GroupBox();
            this.btnCreator = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.connString.SuspendLayout();
            this.nameSpace.SuspendLayout();
            this.tableName.SuspendLayout();
            this.SuspendLayout();
            // 
            // connString
            // 
            this.connString.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.connString.Controls.Add(this.btnLoginDatabase);
            this.connString.Controls.Add(this.txtPwd);
            this.connString.Controls.Add(this.label4);
            this.connString.Controls.Add(this.txtUserId);
            this.connString.Controls.Add(this.label3);
            this.connString.Controls.Add(this.txtServer);
            this.connString.Controls.Add(this.label1);
            this.connString.Location = new System.Drawing.Point(32, 26);
            this.connString.Name = "connString";
            this.connString.Size = new System.Drawing.Size(980, 111);
            this.connString.TabIndex = 0;
            this.connString.TabStop = false;
            this.connString.Text = "连接数据库";
            // 
            // btnLoginDatabase
            // 
            this.btnLoginDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLoginDatabase.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLoginDatabase.Location = new System.Drawing.Point(833, 42);
            this.btnLoginDatabase.Name = "btnLoginDatabase";
            this.btnLoginDatabase.Size = new System.Drawing.Size(114, 36);
            this.btnLoginDatabase.TabIndex = 2;
            this.btnLoginDatabase.Text = "测试连接";
            this.btnLoginDatabase.UseVisualStyleBackColor = false;
            this.btnLoginDatabase.Click += new System.EventHandler(this.btnLoginDatabase_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtPwd.Location = new System.Drawing.Point(642, 46);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(159, 26);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(543, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "管理员密码：";
            // 
            // txtUserId
            // 
            this.txtUserId.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtUserId.Location = new System.Drawing.Point(378, 47);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(159, 26);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "管理员名称：";
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtServer.Location = new System.Drawing.Point(119, 46);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(159, 26);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "mypc\\sql08R2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库连接名：";
            // 
            // nameSpace
            // 
            this.nameSpace.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.nameSpace.Controls.Add(this.label5);
            this.nameSpace.Controls.Add(this.cobTableName);
            this.nameSpace.Location = new System.Drawing.Point(32, 158);
            this.nameSpace.Name = "nameSpace";
            this.nameSpace.Size = new System.Drawing.Size(400, 88);
            this.nameSpace.TabIndex = 1;
            this.nameSpace.TabStop = false;
            this.nameSpace.Text = "数据库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "数据库名称:";
            // 
            // cobTableName
            // 
            this.cobTableName.FormattingEnabled = true;
            this.cobTableName.Location = new System.Drawing.Point(108, 31);
            this.cobTableName.Name = "cobTableName";
            this.cobTableName.Size = new System.Drawing.Size(159, 24);
            this.cobTableName.TabIndex = 1;
            // 
            // txtProject
            // 
            this.txtProject.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtProject.Location = new System.Drawing.Point(132, 34);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(159, 26);
            this.txtProject.TabIndex = 1;
            this.txtProject.Text = "ProjectName";
            // 
            // tableName
            // 
            this.tableName.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tableName.Controls.Add(this.txtProject);
            this.tableName.Controls.Add(this.btnCreator);
            this.tableName.Controls.Add(this.label6);
            this.tableName.Location = new System.Drawing.Point(498, 158);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(514, 88);
            this.tableName.TabIndex = 1;
            this.tableName.TabStop = false;
            this.tableName.Text = "命名空间";
            // 
            // btnCreator
            // 
            this.btnCreator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCreator.Location = new System.Drawing.Point(367, 29);
            this.btnCreator.Name = "btnCreator";
            this.btnCreator.Size = new System.Drawing.Size(114, 38);
            this.btnCreator.TabIndex = 2;
            this.btnCreator.Text = "生成代码";
            this.btnCreator.UseVisualStyleBackColor = false;
            this.btnCreator.Click += new System.EventHandler(this.btnCreator_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "命名空间名称：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1040, 297);
            this.Controls.Add(this.tableName);
            this.Controls.Add(this.nameSpace);
            this.Controls.Add(this.connString);
            this.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三层代码生成器";
            this.connString.ResumeLayout(false);
            this.connString.PerformLayout();
            this.nameSpace.ResumeLayout(false);
            this.nameSpace.PerformLayout();
            this.tableName.ResumeLayout(false);
            this.tableName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connString;
        private System.Windows.Forms.GroupBox nameSpace;
        private System.Windows.Forms.GroupBox tableName;
        private System.Windows.Forms.Button btnLoginDatabase;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCreator;
        private System.Windows.Forms.ComboBox cobTableName;
        private System.Windows.Forms.Label label6;
    }
}

