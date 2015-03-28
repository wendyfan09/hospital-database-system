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
    public partial class patientdoctor : Form
    {
        public patientdoctor()
        {
            InitializeComponent();
        }
        private void display(int n)
        {
            if (n < 0) return;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString().Trim();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString().Trim();
            textBox3.Text = dataGridView1.Rows[n].Cells[2].Value.ToString().Trim();
            textBox4.Text = dataGridView1.Rows[n].Cells[3].Value.ToString().Trim();
            textBox5.Text = dataGridView1.Rows[n].Cells[4].Value.ToString().Trim();
            textBox6.Text = dataGridView1.Rows[n].Cells[5].Value.ToString().Trim();
            textBox7.Text = dataGridView1.Rows[n].Cells[6].Value.ToString().Trim();
        }
        private void refreshigrid()
        {
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB.GetRecordset("SELECT * FROM viewdp ORDER BY 病人码");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
        }
        private void patientdoctor_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "状态：" + this.Text;
            toolStripStatusLabel2.Text = "当前日期：" + DateTime.Now.ToString();
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet DS = DB.GetRecordset("Select * from viewdp");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
            display(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            string pno, pname, dname,dno,ditleof,dresection,ddate;
            pno= textBox1.Text.Trim();
            pname = textBox2.Text.Trim();
            dname= textBox3.Text.Trim();
            dno = textBox4.Text.Trim();
            ditleof = textBox5.Text.Trim();
            dresection = textBox6.Text.Trim();
            ddate = textBox7.Text.Trim();
            sql = "insert into dp values('" + pno + "','" + pname + "','" + dname + "','" + dno + "','" + ditleof + "','" + dresection + "','" + ddate + "')";
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
            
        }

    }
}
