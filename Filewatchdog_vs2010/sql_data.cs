using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;
//using Oracle.DataAccess.Client;
//using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace Filewatchdog_vs2010
{
    class sql_data
    {

        #region  全局变量
        public static SqlConnection conn1;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn2;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn3;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn4;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn5;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn6;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn7;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static SqlConnection conn8;//定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功


        public SqlDataReader odr1;
        public SqlDataReader odr2;
        public SqlDataReader odr3;
        public SqlDataReader odr4;
        public SqlDataReader odr5;
        public SqlDataReader odr6;
        public SqlDataReader odr7;
        public SqlDataReader odr8;

        public bool m_connect1;
        public bool m_connect2;
        public bool m_connect3;
        public bool m_connect4;
        public bool m_connect5;
        public bool m_connect6;
        public bool m_connect7;
        public bool m_connect8;


        // public string ConnectionString = "user id=rh;data source=ccm_client;password=lev2"; //服务器写连接串 
        //public static string ConnectionString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.11.24)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = bofdb))); Persist Security Info=True;User ID = rh; Password=lev2";
        public static string ConnectionString = "Data Source=10.3.86.126;Initial Catalog=level2_data;Persist Security Info=True;User ID=sa;Password=sa123456";

        public string Error_Mes;
        public SqlCommand cmd;
        //public static string dir = Global_Var_Function.dir;
        //public static string spacename = "Sql_DB";
        public int row = 0;


        #endregion


        #region  建立数据库连接1
        public bool OpenMyConnection1()
        {
            m_connect1 = false;
            try
            {
                if (conn1 == null || conn1.State == ConnectionState.Closed)
                {
                    conn1 = new SqlConnection(ConnectionString);
                    conn1.Open();
                    m_connect1 = true;
                }
                else
                {
                    m_connect1 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn1 = null;
                m_connect1 = false;

            }

            return m_connect1;
        }


        #endregion


        #region  建立数据库连接2

        public bool OpenMyConnection2()
        {
            m_connect2 = false;
            try
            {
                if (conn2 == null || conn2.State == ConnectionState.Closed)
                {
                    conn2 = new SqlConnection(ConnectionString);
                    conn2.Open();
                    m_connect2 = true;
                }
                else
                {
                    m_connect2 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn2 = null;
                m_connect2 = false;

            }

            return m_connect2;
        }

        #endregion


        #region  建立数据库连接3

        public bool OpenMyConnection3()
        {
            m_connect3 = false;
            try
            {
                if (conn3 == null || conn3.State == ConnectionState.Closed)
                {
                    conn3 = new SqlConnection(ConnectionString);
                    conn3.Open();
                    m_connect3 = true;
                }
                else
                {
                    m_connect3 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                // log.logtxt(dir, spacename, Error_Mes);
                conn3 = null;
                m_connect3 = false;

            }

            return m_connect3;
        }

        #endregion


        #region  建立数据库连接4

        public bool OpenMyConnection4()
        {
            m_connect4 = false;
            try
            {
                if (conn4 == null || conn4.State == ConnectionState.Closed)
                {
                    conn4 = new SqlConnection(ConnectionString);
                    conn4.Open();
                    m_connect4 = true;
                }
                else
                {
                    m_connect4 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                ///log.logtxt(dir, spacename, Error_Mes);
                conn4 = null;
                m_connect4 = false;

            }

            return m_connect4;
        }

        #endregion


        #region  建立数据库连接5

        public bool OpenMyConnection5()
        {
            m_connect5 = false;
            try
            {
                if (conn5 == null || conn5.State == ConnectionState.Closed)
                {
                    conn5 = new SqlConnection(ConnectionString);
                    conn5.Open();
                    m_connect5 = true;
                }
                else
                {
                    m_connect5 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn5 = null;
                m_connect5 = false;

            }

            return m_connect5;
        }

        #endregion


        #region  建立数据库连接6

        public bool OpenMyConnection6()
        {
            m_connect6 = false;
            try
            {
                if (conn6 == null || conn6.State == ConnectionState.Closed)
                {
                    conn6 = new SqlConnection(ConnectionString);
                    conn6.Open();
                    m_connect6 = true;
                }
                else
                {
                    m_connect6 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn6 = null;
                m_connect6 = false;

            }

            return m_connect6;
        }

        #endregion


        #region  建立数据库连接7

        public bool OpenMyConnection7()
        {
            m_connect7 = false;
            try
            {
                if (conn7 == null || conn7.State == ConnectionState.Closed)
                {
                    conn7 = new SqlConnection(ConnectionString);
                    conn7.Open();
                    m_connect7 = true;
                }
                else
                {
                    m_connect7 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn7 = null;
                m_connect7 = false;

            }

            return m_connect7;
        }

        #endregion


        #region  建立数据库连接8

        public bool OpenMyConnection8()
        {
            m_connect8 = false;
            try
            {
                if (conn8 == null || conn8.State == ConnectionState.Closed)
                {
                    conn8 = new SqlConnection(ConnectionString);
                    conn8.Open();
                    m_connect8 = true;
                }
                else
                {
                    m_connect8 = true;
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------";
                //log.logtxt(dir, spacename, Error_Mes);
                conn8 = null;
                m_connect8 = false;

            }

            return m_connect8;
        }

        #endregion


        #region  关闭数据库连接1

        public void CloseMyConnection1()
        {

            if (conn1.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn1.Close();   //关闭数据库的连接
                conn1.Dispose();   //释放My_con变量的所有空间
                conn1 = null;
            }
        }
        #endregion


        #region  关闭数据库连接2

        public void CloseMyConnection2()
        {
            if (conn2.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn2.Close();   //关闭数据库的连接
                conn2.Dispose();   //释放My_con变量的所有空间
                conn2 = null;
            }
        }
        #endregion


        #region  关闭数据库连接3

        public void CloseMyConnection3()
        {
            if (conn3.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn3.Close();   //关闭数据库的连接
                conn3.Dispose();   //释放My_con变量的所有空间
                conn3 = null;
            }
        }
        #endregion


        #region  关闭数据库连接4

        public void CloseMyConnection4()
        {
            if (conn4.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn4.Close();   //关闭数据库的连接
                conn4.Dispose();   //释放My_con变量的所有空间
                conn4 = null;
            }
        }
        #endregion


        #region  关闭数据库连接5

        public void CloseMyConnection5()
        {
            if (conn5.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn5.Close();   //关闭数据库的连接
                conn5.Dispose();   //释放My_con变量的所有空间
                conn5 = null;
            }
        }
        #endregion


        #region  关闭数据库连接6

        public void CloseMyConnection6()
        {
            if (conn6.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn6.Close();   //关闭数据库的连接
                conn6.Dispose();   //释放My_con变量的所有空间
                conn6 = null;
            }
        }
        #endregion


        #region  关闭数据库连接7

        public void CloseMyConnection7()
        {
            if (conn7.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn7.Close();   //关闭数据库的连接
                conn7.Dispose();   //释放My_con变量的所有空间
                conn7 = null;
            }
        }
        #endregion


        #region  关闭数据库连接8

        public void CloseMyConnection8()
        {
            if (conn8.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn8.Close();   //关闭数据库的连接
                conn8.Dispose();   //释放My_con变量的所有空间
                conn8 = null;
            }
        }
        #endregion


        #region 读取表中信息1

        public bool OpenMyRecordset1(string SQLstr)
        {

            try
            {
                //                OpenMyConnection1();  
                if (conn1.State == ConnectionState.Closed)
                {
                    OpenMyConnection1();
                }

                SqlCommand cmd = conn1.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr1 = cmd.ExecuteReader();//创建一个SqlDateReader对象 

                return true;

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                return false;
                //CloseMyConnection1();

            }

        }

        #endregion


        #region 读取表中信息2

        public bool OpenMyRecordset2(string SQLstr)
        {

            try
            {
                //                 OpenMyConnection2();
                if (conn2.State == ConnectionState.Closed)
                {
                    OpenMyConnection2();
                }
                SqlCommand cmd = conn2.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr2 = cmd.ExecuteReader();//创建一个SqlDateReader对象  
                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                return false;
                //CloseMyConnection2();
            }

        }

        #endregion


        #region 读取表中信息3

        public bool OpenMyRecordset3(string SQLstr)
        {

            try
            {
                //                OpenMyConnection3();
                if (conn3.State == ConnectionState.Closed)
                {
                    OpenMyConnection3();
                }
                SqlCommand cmd = conn3.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr3 = cmd.ExecuteReader();//创建一个OracleDateReader对象   

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                // log.logtxt(dir, spacename, Error_Mes);
                return false;
                //CloseMyConnection3();
            }

        }

        #endregion


        #region 读取表中信息4

        public bool OpenMyRecordset4(string SQLstr)
        {

            try
            {
                //                OpenMyConnection4();
                if (conn4.State == ConnectionState.Closed)
                {
                    OpenMyConnection4();
                }
                SqlCommand cmd = conn4.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr4 = cmd.ExecuteReader();//创建一个OracleDateReader对象

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);

                return false;
                //CloseMyConnection4();
            }

        }

        #endregion


        #region 读取表中信息5

        public bool OpenMyRecordset5(string SQLstr)
        {

            try
            {
                //                OpenMyConnection4();
                if (conn5.State == ConnectionState.Closed)
                {
                    OpenMyConnection5();
                }
                SqlCommand cmd = conn5.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr5 = cmd.ExecuteReader();//创建一个OracleDateReader对象 

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                // log.logtxt(dir, spacename, Error_Mes);

                return false;
                // CloseMyConnection5();
            }

        }

        #endregion


        #region 读取表中信息6

        public bool OpenMyRecordset6(string SQLstr)
        {

            try
            {
                //                OpenMyConnection4();
                if (conn6.State == ConnectionState.Closed)
                {
                    OpenMyConnection6();
                }
                SqlCommand cmd = conn6.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr6 = cmd.ExecuteReader();//创建一个OracleDateReader对象 

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);

                return false;
                // CloseMyConnection6();
            }

        }

        #endregion


        #region 读取表中信息7

        public bool OpenMyRecordset7(string SQLstr)
        {

            try
            {
                //                OpenMyConnection4();
                if (conn7.State == ConnectionState.Closed)
                {
                    OpenMyConnection7();
                }
                SqlCommand cmd = conn7.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr7 = cmd.ExecuteReader();//创建一个OracleDateReader对象 

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);

                return false;
                // CloseMyConnection6();
            }

        }

        #endregion


        #region 读取表中信息8

        public bool OpenMyRecordset8(string SQLstr)
        {

            try
            {
                //                OpenMyConnection4();
                if (conn8.State == ConnectionState.Closed)
                {
                    OpenMyConnection8();
                }
                SqlCommand cmd = conn8.CreateCommand();
                cmd.CommandText = SQLstr;//sql语句  
                odr8 = cmd.ExecuteReader();//创建一个OracleDateReader对象 

                return true;
            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);

                return false;
                // CloseMyConnection6();
            }

        }

        #endregion


        #region  关闭记录集1

        public void CloseMyRecordset1()
        {
            try
            {
                if (conn1.State == ConnectionState.Open)
                {
                    if (odr1 != null)
                    {
                        odr1.Close();
                        odr1.Dispose();

                        odr1 = null;

                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr1 != null)
                {
                    odr1.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集2

        public void CloseMyRecordset2()
        {
            try
            {
                if (conn2.State == ConnectionState.Open)
                {
                    if (odr2 != null)
                    {
                        odr2.Close();
                        odr2.Dispose();
                        odr2 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr2 != null)
                {
                    odr2.Dispose();
                }

                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集3

        public void CloseMyRecordset3()
        {
            try
            {
                if (conn3.State == ConnectionState.Open)
                {
                    if (odr3 != null)
                    {
                        odr3.Close();
                        odr3.Dispose();
                        odr3 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr3 != null)
                {
                    odr3.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集4

        public void CloseMyRecordset4()
        {
            try
            {
                if (conn4.State == ConnectionState.Open)
                {
                    if (odr4 != null)
                    {
                        odr4.Close();
                        odr4.Dispose();
                        odr4 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr4 != null)
                {
                    odr4.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集5

        public void CloseMyRecordset5()
        {
            try
            {
                if (conn5.State == ConnectionState.Open)
                {
                    if (odr5 != null)
                    {
                        odr5.Close();
                        odr5.Dispose();
                        odr5 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr5 != null)
                {
                    odr5.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集6

        public void CloseMyRecordset6()
        {
            try
            {
                if (conn6.State == ConnectionState.Open)
                {
                    if (odr6 != null)
                    {
                        odr6.Close();
                        odr6.Dispose();
                        odr6 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr6 != null)
                {
                    odr6.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集7

        public void CloseMyRecordset7()
        {
            try
            {
                if (conn7.State == ConnectionState.Open)
                {
                    if (odr7 != null)
                    {
                        odr7.Close();
                        odr7.Dispose();
                        odr7 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr7 != null)
                {
                    odr7.Dispose();
                }
                Error_Mes = ee.Message;
                // log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region  关闭记录集8

        public void CloseMyRecordset8()
        {
            try
            {
                if (conn8.State == ConnectionState.Open)
                {
                    if (odr8 != null)
                    {
                        odr8.Close();
                        odr8.Dispose();
                        odr8 = null;
                    }

                }
            }
            catch (System.Exception ee)
            {
                if (odr8 != null)
                {
                    odr8.Dispose();
                }
                Error_Mes = ee.Message;
                //log.logtxt(dir, spacename, Error_Mes);
            }


        }
        #endregion


        #region 查询获取表中行数1

        public int Recordcount1(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn1.State == ConnectionState.Open)
                {

                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn1);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection1();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数2

        public int Recordcount2(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn2.State == ConnectionState.Open)
                {

                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn2);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                ////log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection2();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数3

        public int Recordcount3(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn3.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn3);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;

                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection3();
                return 0;

            }
        }


        #endregion


        #region 查询获取表中行数4

        public int Recordcount4(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn4.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn4);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection4();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数5

        public int Recordcount5(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn5.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn5);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection5();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数6

        public int Recordcount6(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn6.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn6);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection6();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数7

        public int Recordcount7(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn7.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn7);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection6();
                return 0;


            }
        }


        #endregion


        #region 查询获取表中行数8

        public int Recordcount8(string SQLstr)
        {
            int record = 0;

            try
            {
                if (conn8.State == ConnectionState.Open)
                {
                    SqlDataAdapter myDa = new SqlDataAdapter();
                    myDa.SelectCommand = new SqlCommand(SQLstr, conn8);
                    DataTable myDt = new DataTable();
                    myDa.Fill(myDt);
                    record = myDt.Rows.Count;
                    return record;
                }
                else
                    return 0;

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                //CloseMyConnection6();
                return 0;


            }
        }


        #endregion


        #region Operate表1  包括UPDATE/INSERT/DELETE

        public void ExecuteSQL1(string SQLstr)
        {

            try
            {
                if (OpenMyConnection1())
                {
                    SqlCommand cmd = conn1.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection1();

            }

        }


        #endregion


        #region Operate表2   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL2(string SQLstr)
        {

            try
            {
                if (OpenMyConnection2())
                {
                    SqlCommand cmd = conn2.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection2();

            }

        }

        #endregion


        #region Operate表3   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL3(string SQLstr)
        {

            try
            {
                if (OpenMyConnection3())
                {
                    SqlCommand cmd = conn3.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {

                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection3();

            }

        }

        #endregion


        #region Operate表4   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL4(string SQLstr)
        {

            try
            {
                if (OpenMyConnection4())
                {
                    SqlCommand cmd = conn4.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection4();

            }

        }


        #endregion


        #region Operate表5   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL5(string SQLstr)
        {

            try
            {
                if (OpenMyConnection5())
                {
                    SqlCommand cmd = conn5.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection5();

            }

        }


        #endregion


        #region Operate表6   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL6(string SQLstr)
        {

            try
            {
                if (OpenMyConnection6())
                {
                    SqlCommand cmd = conn6.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection6();

            }

        }


        #endregion


        #region Operate表7   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL7(string SQLstr)
        {

            try
            {
                if (OpenMyConnection7())
                {
                    SqlCommand cmd = conn7.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection7();

            }

        }


        #endregion


        #region Operate表8   包括UPDATE/INSERT/DELETE

        public void ExecuteSQL8(string SQLstr)
        {

            try
            {
                if (OpenMyConnection8())
                {
                    SqlCommand cmd = conn8.CreateCommand();
                    cmd.CommandText = SQLstr;//sql语句 
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ee)
            {
                Error_Mes = ee.Message + "------" + SQLstr;
                //log.logtxt(dir, spacename, Error_Mes);
                CloseMyConnection8();

            }

        }


        #endregion

        public DataTable GetDataTable1(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn1);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable GetDataTable2(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn2);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable GetDataTable3(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn3);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable GetDataTable4(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn4);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetDataTable5(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn5);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public DataTable GetDataTable6(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn6);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable GetDataTable7(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn7);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable GetDataTable8(string CmdString)
        {
            try
            {

                SqlDataAdapter myDa = new SqlDataAdapter();
                myDa.SelectCommand = new SqlCommand(CmdString, conn8);
                DataTable myDt = new DataTable();
                myDa.Fill(myDt);

                return myDt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int ExecuteSql_Delete(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }



    }


}




