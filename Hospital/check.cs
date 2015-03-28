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
    public partial class check : Form
    {
        public check()
        {
            InitializeComponent();
        }
        private void display(int n)
        {
            if (n < 0) return;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString().Trim();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString().Trim();
            textBox3.Text = dataGridView1.Rows[n].Cells[2].Value.ToString().Trim();
        }
        private void refreshigrid()
        {
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB.GetRecordset("SELECT * FROM viewc ORDER BY 项目码");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            string cno, cname, cfee;
            cno = textBox1.Text.Trim();
            cname = textBox2.Text.Trim();
            cfee = textBox3.Text.Trim();
            sql = "insert into c values('" + cno + "','" + cname + "','" + cfee + "')";
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DB.DBClose();
            refreshigrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql;
            string cno, cname, cfee;
            cno = textBox1.Text.Trim();
            cname = textBox2.Text.Trim();
            cfee = textBox3.Text.Trim();
            sql = "update c set cname='" + cname + "',cfee='" + cfee + "' where c#='" + cno + "'";
            if (MessageBox.Show("是否更新项目码为" + cno + "的项目？", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }

        private void check_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "状态：" + this.Text;
            toolStripStatusLabel2.Text = "当前日期：" + DateTime.Now.ToString();
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet DS = DB.GetRecordset("Select * from viewc");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
            display(0);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            display(n);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql, cno;
            cno = textBox1.Text.Trim();
            sql = "delete  from c where c#='" + cno + "'";
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (MessageBox.Show("您将删除项目码为" + cno + "的项目的信息，请确认是否继续？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


    }
}
