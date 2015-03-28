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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }


        private void login_Load_1(object sender, EventArgs e)
        {
            textBox1.Text = "u-PC";
            textBox2.Text = "sa";
            textBox3.Text = "111";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBuser.hserver = textBox1.Text;
            DBuser.huser = textBox2.Text;
            DBuser.hpassward = textBox3.Text;
            MyDBase DB = new MyDBase(DBuser.hserver, "hospital", DBuser.huser, DBuser.hpassward);
            if (DB.ErrorCode())
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                DBuser.flag = -1;
                MessageBox.Show(DB.ErrMessage(), "数据库连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DB.DBClose();
                return;
            }
            DB.DBClose();
            DBuser.flag = 0;
            this.Close();
        }

    }
}
