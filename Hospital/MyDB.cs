using System;
using System.Data;
using System.Data.SqlClient;  

public class MyDBase
{
        bool ECode=false;
        string ES;
		SqlConnection  cn=new System.Data.SqlClient.SqlConnection();
		DataSet Rs;
		public MyDBase(string MyDBServerName,string MyDataBaseName)
		{
            ECode = false;
            cn.ConnectionString="workstation id="+MyDBServerName+";packet size=4096;integrated security=SSPI;data source="+MyDBServerName+";persist security info=False;initial catalog="+MyDataBaseName;
            try
            {
                cn.Open();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true; 
            }
		}
        public MyDBase(string MyDBServerName, string MyDataBaseName, string sUerName, string sPasswd)
        {
            ECode = false;
            string sConn = "workstation id=" + MyDBServerName + ";packet size=4096;user id=" + sUerName + ";pwd=" + sPasswd + ";data source=" + MyDBServerName + ";persist security info=False;initial catalog=" + MyDataBaseName;
            cn.ConnectionString = sConn;
            try
            {
                cn.Open();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
            }
        }

		public DataSet GetRecordset(string Sqls)
		{
			SqlCommand sqlCmd= new SqlCommand();
			sqlCmd.Connection = cn;
			sqlCmd.CommandText = Sqls;
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                Rs = new DataSet();
                adp.Fill(Rs);
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
                return null;
            }
			return (Rs);
		}
		public int ExecuteSQLScalar(string Sqls)
		{
            string s;
			SqlCommand sqlCmd= new SqlCommand();
			sqlCmd.Connection = cn;
			sqlCmd.CommandText = Sqls;
			sqlCmd.CommandType = CommandType.Text;
            try
            {
               s = sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
                return -1;
            }
			return(int.Parse(s)); 
		}
		public string ExecuteSQLWithTrans(string Sqls)
		{
            string s;
			SqlTransaction myTrans;
			myTrans=cn.BeginTransaction(); 
			SqlCommand sqlCmd= new SqlCommand();
			sqlCmd.Connection = cn;
			sqlCmd.CommandText = Sqls;
			sqlCmd.CommandType = CommandType.Text;
			sqlCmd.Transaction =myTrans;
			sqlCmd.ExecuteNonQuery(); 
			//Sqls="SELECT @@IDENTITY AS ID";
			sqlCmd.CommandText =Sqls;
            try
            {
                s = sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
                myTrans.Commit(); 
                return "";
            }
			myTrans.Commit();
			return(s);
		}
		public void ExecuteSQL(string Sqls)
		{
			SqlCommand sqlCmd= new SqlCommand();
			sqlCmd.Connection = cn;
			sqlCmd.CommandText = Sqls;
			sqlCmd.CommandType = CommandType.Text;
            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
            }
		}
		public SqlDataReader DBDataReader(string Sqls)
		{
			SqlCommand sqlCmd= new SqlCommand();
			sqlCmd.Connection = cn;
			sqlCmd.CommandText = Sqls;
			sqlCmd.CommandType = CommandType.Text;
            try
            {
                return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
                return null;
            }
		}
		public void DBClose()
		{
            try
            {
                cn.Close();
            }
            catch (Exception e)
            {
                ES = e.Message;
                ECode = true;
            }
		}
        public bool ErrorCode()
        {
            return ECode;
        }
        public string ErrMessage()
        {
            return ES;
        }
		~MyDBase()
		{
			//if (cn.State==ConnectionState.Open ) cn.Close();
		}
}
