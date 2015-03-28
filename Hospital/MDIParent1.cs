using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hospital2
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            DBuser.hserver = "";
            DBuser.huser = "";
            DBuser.hpassward = "";
            DBuser.flag = -1;
            login  f = new login();
            f.MdiParent = this;
            f.Show();
        }

        private void 医生管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBuser.flag == -1)
            {
                MessageBox.Show("请正确注册", "用户注册错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            doctor f = new doctor();
            f.MdiParent = this;
            f.Show();
        }

        private void 病人管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (DBuser.flag == -1)
            {
                MessageBox.Show("请正确注册", "用户注册错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            patient p = new patient();
            p.MdiParent = this;
            p.Show();
        }

        private void 诊断项目管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBuser.flag == -1)
            {
                MessageBox.Show("请正确注册", "用户注册错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           check c = new check();
            c.MdiParent = this;
            c.Show();

        }

        private void 药品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBuser.flag == -1)
            {
                MessageBox.Show("请正确注册", "用户注册错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            medicine m = new medicine();
            m.MdiParent = this;
            m.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void 病人归属查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBuser.flag == -1)
            {
                MessageBox.Show("请正确注册", "用户注册错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           patientdoctor pd= new patientdoctor();
            pd.MdiParent = this;
            pd.Show();


        }




    }
}
