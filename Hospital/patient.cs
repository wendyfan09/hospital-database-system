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
    public partial class patient : Form
    {
        public patient()
        {
            InitializeComponent();
        }
        private void display(int n)
        {
            if (n < 0) return;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString().Trim();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString().Trim();
            comboBox1.Text = dataGridView1.Rows[n].Cells[2].Value.ToString().Trim();
            textBox3.Text = dataGridView1.Rows[n].Cells[3].Value.ToString().Trim();
            textBox4.Text = dataGridView1.Rows[n].Cells[4].Value.ToString().Trim();
            textBox5.Text = dataGridView1.Rows[n].Cells[5].Value.ToString().Trim();
            textBox6.Text = dataGridView1.Rows[n].Cells[6].Value.ToString().Trim();
        }
        private void refreshigrid()
        {
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB.GetRecordset("SELECT * FROM viewp ORDER BY 病历码");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            string pno, pname, psex, pdate, pcode, padress, ptel;
            pno = textBox1.Text.Trim();
            pname = textBox2.Text.Trim();
            psex = comboBox1.Text.Trim();
            pdate = textBox3.Text.Trim();
            pcode = textBox4.Text.Trim();
            padress = textBox5.Text.Trim();
            ptel = textBox6.Text.Trim();
            sql = "insert into p values('" + pno + "','" + pname + "','" + psex + "','" + pdate + "','" + padress + "','" + ptel + "')";
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

        private void patient_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "状态：" + this.Text;
            toolStripStatusLabel2.Text = "当前日期：" + DateTime.Now.ToString();
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet DS = DB.GetRecordset("Select * from viewp");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
            display(0);
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pno, sql;
            pno= textBox1.Text.Trim();
            sql = "delete  from p where p# ='" + pno + "'";
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (MessageBox.Show("请确认是否删除病历码为" + pno + "的病人信息？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sql;
            string pno, pname, psex, pdate, pcode, padress, ptel;
            pno = textBox1.Text.Trim();
            pname = textBox2.Text.Trim();
            psex = comboBox1.Text.Trim();
            pdate = textBox3.Text.Trim();
            pcode = textBox4.Text.Trim();
            padress = textBox5.Text.Trim();
            ptel = textBox6.Text.Trim();
            sql = "update p set pname='" + pname + "',psex='" + psex + "',pdate='" + pdate + "',pid='" + pcode + "',paddress='" + padress + "',ptel='" + ptel + "' where p#='" + pno + "'";
            if (MessageBox.Show("是否更新病历码为" + pno + "的病人？", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            display(n);
        }
    }
}
