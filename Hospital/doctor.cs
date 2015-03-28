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
    public partial class doctor : Form
    {
        public doctor()
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            display(n);
        }
        private void refreshigrid()
        {
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB.GetRecordset("SELECT * FROM viewd ORDER BY 医生码");
            dataGridView1.DataSource = DS.Tables[0];
            DB.DBClose();
        }
        private void doctor_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "状态：医生管理";
            toolStripStatusLabel2.Text = "当前日期：" + DateTime.Now.ToString();
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                MessageBox.Show(DB.ErrMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet DS = DB.GetRecordset("Select * from viewd");
            DataSet DS2 = DB.GetRecordset("Select * from viewdf");
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView2.DataSource = DS2.Tables[0];
            DB.DBClose();
            display(0);
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
            DB = new MyDBase("u-PC", "hospital");
            DB.DBClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql, dno;
            dno = textBox1.Text.Trim();
            sql = "delete  from d where d#='" + dno + "'";
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (MessageBox.Show("您将删除医生码为" + dno + "的医生的信息，请确认是否继续？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
            //删除的同时刷新dataGridView2 
            MyDBase DB1 = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DataSet DS = DB1.GetRecordset("SELECT * FROM viewdf ORDER BY 医生码");
            dataGridView2.DataSource = DS.Tables[0];
            DB.DBClose();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sql;
            string dno, dname, dsex, dbrithin, ditleof, dresection, dtel;
            dno = textBox1.Text.Trim();
            dname = textBox2.Text.Trim();
            dsex = comboBox1.Text.Trim();
            dbrithin = textBox3.Text.Trim();
            ditleof = textBox4.Text.Trim();
            dresection = textBox5.Text.Trim();
            dtel = textBox6.Text.Trim();
            sql = "update d set dname='" + dname + "',dsex='" + dsex + "',dbrithin='" + dbrithin + "',ditleof='" + ditleof + "',drsection='" + dresection + "',dtel='" + dtel + "' where d#='" + dno + "'";
            if (MessageBox.Show("是否更新医生码为" + dno + "的医生？", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            DB.ExecuteSQL(sql);
            if (DB.ErrorCode())
                MessageBox.Show(DB.ErrMessage(), "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DB.DBClose();
            refreshigrid();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string sql;
            string dno, dname, dsex, dbrithin, ditleof, dresection, dtel;
            dno= textBox1.Text.Trim();
            dname = textBox2.Text.Trim();
            dsex = comboBox1.Text.Trim();
            dbrithin = textBox3.Text.Trim();
            ditleof= textBox4.Text.Trim();
            dresection = textBox5.Text.Trim();
            dtel= textBox6.Text.Trim();
            sql = "insert into d values('" + dno + "','" + dname + "','" + dsex + "','" + dbrithin + "','" + ditleof + "','" + dresection + "','" + dtel + "')";
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==0)
                return;
            int rowIndex = dataGridView1.SelectedRows[0].Index; //得到当前选中行的索引
            if(rowIndex == 0)
            {
                MessageBox.Show("已经是第一行了!","移动提示",MessageBoxButtons.OK,MessageBoxIcon.Warning );
                return;
            }
            dataGridView1.Rows[rowIndex - 1].Selected = true ;
            dataGridView1.Rows[rowIndex].Selected = false ;
            int a;
            a = dataGridView1.SelectedRows[0].Index;
            display(a);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int rowIndex = dataGridView1.SelectedRows[0].Index; //得到当前选中行的索引
            if (rowIndex == dataGridView1.Rows.Count  -2)
            {
                MessageBox.Show("已经是最后一行了!", "移动提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridView1.Rows[rowIndex+1].Selected = true  ;
            dataGridView1.Rows[rowIndex].Selected = false  ;
            int a;
            a = dataGridView1.SelectedRows[0].Index;
            display(a);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int rowIndex = dataGridView1.SelectedRows[0].Index; //得到当前选中行的索引
            dataGridView1.Rows[rowIndex].Selected = false;
            dataGridView1.Rows[0].Selected = true;
            int a;
            a = dataGridView1.SelectedRows[0].Index;
            display(a);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int rowIndex = dataGridView1.SelectedRows[0].Index; //得到当前选中行的索引
            dataGridView1.Rows[rowIndex].Selected = false;
            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Selected = true;
            int a;
            a = dataGridView1.SelectedRows[0].Index;
            display(a);
        }


        
    }
}
