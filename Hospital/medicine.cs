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
    public partial class medicine : Form
    {
        public medicine()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            string mno, mname, mfee,mps;
            mno = textBox1.Text.Trim();
            mname = textBox2.Text.Trim();
            mfee = textBox3.Text.Trim();
            mps = textBox4.Text.Trim();
            sql = "insert into m values('" + mno + "','" + mname + "','" + mfee + "','" +mps + "')";
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
            string mno, mname, mfee, mps;
            mno = textBox1.Text.Trim();
            mname = textBox2.Text.Trim();
            mfee = textBox3.Text.Trim();
            mps = textBox4.Text.Trim();
            sql = "update m set mname='" + mname + "',mfee='" + mfee + "',mps='" + mps + "' where m#='" + mno + "'";
            if (MessageBox.Show("是否更新药品码为" + mno + "的药品？", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }
        private void display(int n)
        {
            if (n < 0) return;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString().Trim();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString().Trim();
            textBox3.Text = dataGridView1.Rows[n].Cells[2].Value.ToString().Trim();
            textBox4.Text = dataGridView1.Rows[n].Cells[3].Value.ToString().Trim();
        }
        private void refreshigrid()
        {
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB.GetRecordset("SELECT * FROM viewm ORDER BY 药品码");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
        }
        private void medicine_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "状态：" + this.Text;
            toolStripStatusLabel2.Text = "当前日期：" + DateTime.Now.ToString();
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet DS = DB.GetRecordset("Select * from viewm");
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
            string sql, mno;
            mno = textBox1.Text.Trim();
            sql = "delete  from m where m#='" + mno + "'";
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (MessageBox.Show("您将删除药品码为" + mno + "的药品的信息，请确认是否继续？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }

    }
}
