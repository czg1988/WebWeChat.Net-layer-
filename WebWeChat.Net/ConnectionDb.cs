using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace Implement.DAL
{
    public class ConnectionDb
    {
        private string strError = null;
        private int intCount = 0;
        private static string dbStr = "";
        public ConnectionDb()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 公开方法DBConn，返回数据库连接
        /// </summary>
        /// <returns></returns>
        public SqlConnection DBconn()
        {
            //string strConn = "Server=(local);Database=RxEducation2013;Uid=sa;pwd=123";
            ConnectionStringSettings connectionSetting = ConfigurationManager.ConnectionStrings["DB"];
            string strConn = connectionSetting.ConnectionString;
            dbStr=strConn;
            try
            {
                return new SqlConnection(strConn);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 公开方法DBConn，返回数据库连接
        /// </summary>
        /// <returns></returns>
        public SqlConnection DBconn(string db_name, string db_pass)
        {
            string strConn = "Server=(local);Database=" + db_name + ";Uid=sa;pwd=" + db_pass + "";
            dbStr = strConn;
            try
            {
                return new SqlConnection(strConn);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 公开属性ErrorMessage，返回错误信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return strError;
            }
        }

        /// <summary>
        /// 直接将同结构的datatable插入进数据库表中。
        /// </summary>
        /// create by zcy
        /// <param name="dt"></param>
        /// <param name="tablename"></param>
        public static void insertDataTable(DataTable dt, string tablename, string[] sList)
        {

            using (SqlConnection conn = new SqlConnection(dbStr))
            {
                SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(dbStr, SqlBulkCopyOptions.UseInternalTransaction);
                sqlbulkcopy.DestinationTableName = tablename;//数据库中的表名
                for (int a = 0; a < sList.Length; a++)
                {
                    string s = sList[a].Split('=')[0];
                    string x = sList[a].Split('=')[1];
                    sqlbulkcopy.ColumnMappings.Add(s, x);    
                }
                    
                sqlbulkcopy.WriteToServer(dt);
                sqlbulkcopy.Close();
                conn.Close();
            }
        }




        /// <summary>
        /// 公开属性ErrorMessage，返回错误信息
        /// </summary>
        public int EffectRows
        {
            get
            {
                return intCount;
            }
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="strSelect">查询语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>有数据则返回DataSet对象，否则返回null</returns>
        public DataSet Select(string SelectString, SqlConnection sqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                selectCommand.CommandType = CommandType.Text;
                mySqlDataAdapter.SelectCommand = selectCommand;
                DataSet myDS = new DataSet();
                mySqlDataAdapter.Fill(myDS);
                return myDS;
            }
            catch (Exception e)
            {
                strError = "数据检索失败：" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据,性能高于DataSet
        /// </summary>
        /// <param name="strSelect">查询语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>有数据则返回DataTable对象，否则返回null</returns>
        /// 在默认的情况下填充DataTable时并没有从数据库中取的主键的信息。如何获得主键呢？
        /// 在填充Dataset的时候使用DataAdapter的MissingSchemaAction属性可以解决这个问题
        public DataTable SelectDT(string SelectString, SqlConnection sqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
                mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                selectCommand.CommandType = CommandType.Text;
                mySqlDataAdapter.SelectCommand = selectCommand;
                DataTable myDT = new DataTable();
                mySqlDataAdapter.Fill(myDT);
                return myDT;
            }
            catch (Exception e)
            {
                strError = "数据检索失败：" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        public DataTable SelectDT(string SelectString, SqlConnection sqlConn, out string error)
        {
            DataTable myDT = new DataTable();
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                selectCommand.CommandType = CommandType.Text;
                SqlDataReader reader = selectCommand.ExecuteReader();
                myDT.Load(reader);
                error = "";
                return myDT;
            }
            catch (Exception e)
            {
                error = "数据检索失败：" + e.Message;
                return myDT;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// sqlCommand执行查询返回datatable
        /// </summary>
        /// create by zcy
        /// <param name="SelectString"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public DataTable SelectDTByPram(string SelectString, string[][] parameters, SqlConnection sqlConn)
        {
            DataTable myDT = new DataTable();
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
                //mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                if (parameters[0].Length > 0)
                {
                    for (int i = 0; i < parameters[0].Length; i++)
                    {
                        selectCommand.Parameters.AddWithValue(parameters[0][i], parameters[1][i]);
                    }
                }
                selectCommand.CommandType = CommandType.Text;
                // mySqlDataAdapter.SelectCommand = selectCommand;
                SqlDataReader reader = selectCommand.ExecuteReader();
                myDT.Load(reader);
                return myDT;
            }
            catch (Exception e)
            {
                strError = "数据检索失败：" + e.Message;
                return myDT;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

     
        /// <summary>    
        /// 执行多条SQL语句，实现数据库事务。
        /// create zcy
        /// </summary>    
        /// <param name="SQLStringList">多条SQL语句</param>         
        public static void ExecuteSql(ArrayList SQLStringList, SqlConnection sqlConn)
        {
            using (SqlConnection conn = sqlConn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                    //    ITNB.Base.Error.showError(E.Message.ToString());    
                }
            }
        }
        public static void ExecuteSqlByPara(ArrayList SQLStringList, SqlConnection sqlConn)
        {
            using (SqlConnection conn = sqlConn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                    //    ITNB.Base.Error.showError(E.Message.ToString());    
                }
            }
        }
        /// <summary>
        /// 更新，插入，删除记录。返回执行结果。sqlCommand执行
        /// </summary>
        /// create by zcy 
        /// <param name="SelectString"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public bool UpdateByPara(string SelectString, string[][] parameters, SqlConnection sqlConn)
        {
            DataTable myDT = new DataTable();
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                if (parameters[0].Length > 0)
                {
                    for (int i = 0; i < parameters[0].Length; i++)
                    {
                        selectCommand.Parameters.AddWithValue(parameters[0][i], parameters[1][i]);
                    }
                }
                selectCommand.CommandType = CommandType.Text;
                int reader = selectCommand.ExecuteNonQuery();
                if (reader == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                strError = "数据更新失败：" + e.Message;
                return false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public int UpdateByParas(string SelectString, string[][] parameters, SqlConnection sqlConn)
        {
            DataTable myDT = new DataTable();
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                if (parameters[0].Length > 0)
                {
                    for (int i = 0; i < parameters[0].Length; i++)
                    {
                        selectCommand.Parameters.AddWithValue(parameters[0][i], parameters[1][i]);
                    }
                }
                selectCommand.CommandType = CommandType.Text;
                int reader = selectCommand.ExecuteNonQuery();
                if (reader == -1)
                {
                    return -1;
                }
                return reader;
            }
            catch (Exception e)
            {
                strError = "数据更新失败：" + e.Message;
                return -1;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="UpdateString">Update Sql语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>更新成功返回true</returns>
        public bool Update(string UpdateString, SqlConnection SqlConn)
        {
            return udiDataBase(UpdateString, SqlConn);
        }

        public int UpdateCount(string UpdateString, SqlConnection SqlConn)
        {
            return udiDataBaseCount(UpdateString, SqlConn);
        }
        /// <summary>
        /// 从数据库中删除数据
        /// </summary>
        /// <param name="DeleteString">Delete Sql语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>删除成功返回true</returns>
        public bool Delete(string DeleteString, SqlConnection SqlConn)
        {
            return udiDataBase(DeleteString, SqlConn);
        }

        /// <summary>
        /// 把数据插入数据库
        /// </summary>
        /// <param name="InsertString">Insert Sql语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>插入成功返回true</returns>
        public bool Insert(string InsertString, SqlConnection SqlConn)
        {
            return udiDataBase(InsertString, SqlConn);
        }

        public int InsertCount(string InsertString, SqlConnection SqlConn)
        {
            return udiDataBaseCount(InsertString, SqlConn);
        }
        /// <summary>
        /// 根据Sql语句更新数据库
        /// </summary>
        /// <param name="UDIString">更新语句</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>更新成功则返回true</returns>
        public bool udiDataBase(string UDIString, SqlConnection SqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (SqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = SqlConn;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(UDIString, conn);
                cmd.CommandType = CommandType.Text;
                intCount = cmd.ExecuteNonQuery();
                return !(intCount < 1);
            }
            catch (Exception e)
            {
                strError = "更新数据库失败：" + e.Message;
                return false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public int udiDataBaseCount(string UDIString, SqlConnection SqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (SqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = SqlConn;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(UDIString, conn);
                cmd.CommandType = CommandType.Text;
                intCount = cmd.ExecuteNonQuery();
                return intCount;
            }
            catch (Exception e)
            {
                strError = "更新数据库失败：" + e.Message;
                return 0;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="StoreProcedure">存储过程名</param>
        /// <param name="sp">SqlParameter数组</param>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>存储过程执行成功则返回true</returns>
        public bool ExcuteStoreProcedure(string StoreProcedure, SqlParameter[] sp, SqlConnection SqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (SqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = SqlConn;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(StoreProcedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= sp.GetUpperBound(0); i++)
                {
                    cmd.Parameters.Add(sp[i]);
                }
                intCount = cmd.ExecuteNonQuery();
                return !(intCount < 1);
            }
            catch (Exception e)
            {
                strError = "更新数据库失败：" + e.Message + sp.GetUpperBound(0);
                return false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 获得当前连接数据库中表的相关信息
        /// </summary>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>返回DataView</returns>
        public DataView GetTables(SqlConnection sqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string tables = System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables;
                //string views = System.Data.SqlClient.SqlClientMetaDataCollectionNames.Views;
                DataTable dt = conn.GetSchema(tables);
                dt.DefaultView.Sort = "table_name";
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                strError = "获取数据库表信息失败，你是否有该权限" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 获得当前连接数据库中所有表的各列相关信息
        /// </summary>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>返回DataView</returns>
        public DataView GetColumns(SqlConnection sqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable dt = conn.GetSchema("Columns");
                DataColumn dc = new DataColumn();
                dc.ColumnName = "isPrimaryKey";
                dc.Caption = "PrimaryKey";
                dc.DataType = System.Type.GetType("System.String");
                dc.ReadOnly = false;
                dt.Columns.Add(dc);
                DataTable idt = conn.GetSchema("IndexColumns");
                ArrayList list = new ArrayList();
                foreach (DataRow row in idt.Rows)
                {
                    if (row["constraint_name"].ToString().StartsWith("pk") || row["constraint_name"].ToString().StartsWith("PK"))
                    {
                        //do nothing
                        list.Add(row["table_name"].ToString() + row["column_name"].ToString());
                    }
                }
                idt = null;
                foreach (DataRow row in dt.Rows)
                {
                    if (list.Contains(row["TABLE_NAME"].ToString() + row["column_name"].ToString()))
                        row["isPrimaryKey"] = "Yes";
                    else
                        row["isPrimaryKey"] = "No";
                }
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                strError = "获取数据库表信息失败，你是否有该权限" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 获得当前连接数据库中所有外键相关信息
        /// </summary>
        /// <param name="SqlConn">数据库连接</param>
        /// <returns>返回DataView</returns>
        public DataView GetForeignKeys(SqlConnection sqlConn)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string foreignkeys = System.Data.SqlClient.SqlClientMetaDataCollectionNames.ForeignKeys;
                DataTable dt = conn.GetSchema(foreignkeys);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                strError = "获取数据库表信息失败，你是否有该权限" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 获得一个表的所有列名和数据类型
        /// </summary>
        /// <param name="SqlConn">数据库连接、表名</param>
        /// <returns>返回Dictionary</returns>
        public Dictionary<String, String> GetFields(SqlConnection sqlConn, string TableName)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }

            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string Sql = "select top 1 * from " + TableName + " where 1=0";
                DataTable dt = this.SelectDT(Sql, conn);
                Dictionary<String, String> dic = new Dictionary<string, string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    //string liststr = string.Format("列名：{0} ,数据类型：{1}", dc.ColumnName, dc.DataType));
                    dic.Add(dc.ColumnName, dc.DataType.ToString());
                }
                return dic;
            }
            catch (Exception e)
            {
                strError = "获取数据库表字段信息失败，你是否有该权限" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 获得一个表的主键列名
        /// </summary>
        /// <param name="SqlConn">数据库连接、表名</param>
        /// <returns>返回String</returns>
        public DataColumn[] GetPrimaryKeys(SqlConnection sqlConn, string TableName)
        {
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }

            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string Sql = "select top 1 * from " + TableName + " where 1=0";
                DataTable dt = this.SelectDT(Sql, conn);
                DataColumn[] cols = dt.PrimaryKey;
                return cols;
            }
            catch (Exception e)
            {
                strError = "获取数据库表主键信息失败，你是否有该权限" + e.Message;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 格式化操作系统类型到数据库类型
        /// </summary>
        /// <param name="Type">操作系统数据类型</param>
        /// <returns>返回SqlDbType</returns>
        public SqlDbType ConvertSqlType(string type)
        {
            switch (type.ToLower())
            {
                case "system.int64":
                case "system.uint64":
                    return SqlDbType.BigInt;
                case "system.boolean":
                    return SqlDbType.Bit;
                case "system.datetime":
                    return SqlDbType.DateTime;
                case "system.decimal":
                    return SqlDbType.Decimal;
                case "system.double":
                    return SqlDbType.Float;
                case "system.int32":
                    return SqlDbType.Int;
                case "system.single":
                    return SqlDbType.Real;
                case "system.int16":
                    return SqlDbType.SmallInt;
                case "system.byte":
                    return SqlDbType.TinyInt;
                case "system.sbyte":
                    return SqlDbType.Bit;
                case "system.guid":
                    return SqlDbType.UniqueIdentifier;
                case "system.byte()":
                    return SqlDbType.VarBinary;
                case "system.string":
                case "system.text":
                    return SqlDbType.VarChar;
                case "system.char":
                    return SqlDbType.Char;
                case "system.object":
                    return SqlDbType.Variant;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="SelectString"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public bool AddByPara(string SelectString, SqlParameter[] parameters, SqlConnection sqlConn)
        {
            DataTable myDT = new DataTable();
            strError = "";
            SqlConnection conn;
            if (sqlConn == null)
            {
                conn = DBconn();
            }
            else
            {
                conn = sqlConn;
            }
            try
            {
                //若数据库连接的当前状态是关闭的，则打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand selectCommand = new SqlCommand(SelectString, conn);
                if (parameters != null)
                {
                    selectCommand.Parameters.AddRange(parameters);
                }
                selectCommand.CommandType = CommandType.Text;
                int reader = selectCommand.ExecuteNonQuery();
                if (reader == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                strError = "数据添加失败：" + e.Message;
                return false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 从数据字典zdb_q中找到：资产类别、专业部室、项目部、资产状态等内容
        /// </summary>
        /// <param name="txt_nr_s">DropDownList控件</param>
        /// <param name="c_nr">控件选择项</param>
        /// <param name="DDlb">下拉框类型，包括：资产类别、专业部室、项目部、资产状态</param>
        /// <returns></returns>
        public void setDD(DropDownList txt_nr_s, string c_nr, string DDlb)
        {
            int ddcountxmb = 0; // 项目部
            int ddcountzybs = 0; // 专业部室
            int ddcountzczt = 0; // 资产状态
            int ddcountzclb = 0; // 资产类别

            try
            {
                //--默认显示设备类别：固定资产、低值易耗品、耗材--
                string c_sqlzclb1 = "select * FROM zdb_q where zdm='lb' and xh>0";// where xh=0";//,表名=bm
                txt_nr_s.DataSource = SelectDT(c_sqlzclb1, null);
                txt_nr_s.DataTextField = "nr";
                txt_nr_s.DataValueField = "nr";
                txt_nr_s.DataBind();
                txt_nr_s.Items.Insert(0, new ListItem("请选择", "0"));

                switch (DDlb) //--- 判断下拉框类型 ---
                {
                    case "资产类别":
                        if (ddcountzclb < 1)
                        {
                            string c_sqlzclb = "select * FROM zdb_q where zdm='lb' and xh>0";// where xh=0;表名=bm
                            txt_nr_s.DataSource = SelectDT(c_sqlzclb, null);
                            txt_nr_s.DataTextField = "nr";
                            txt_nr_s.DataValueField = "nr";
                            txt_nr_s.DataBind();
                            txt_nr_s.Items.Insert(0, new ListItem("请选择", "0"));
                        }
                        break;
                    case "专业部室":
                        if (ddcountzybs < 1)
                        {
                            string c_sqlbs = "select * FROM zdb_q where zdm='zybs' and xh>0";
                            txt_nr_s.DataSource = SelectDT(c_sqlbs, null);
                            txt_nr_s.DataTextField = "nr";
                            txt_nr_s.DataValueField = "nr";
                            txt_nr_s.DataBind();
                            txt_nr_s.Items.Insert(0, new ListItem("请选择", "0"));
                        }
                        break;

                    case "项目部":

                        if (ddcountxmb < 1)
                        {
                            string c_sqlxmb = "select nr FROM zdb_q where zdm='xmb' and xh>0";
                            txt_nr_s.DataSource = SelectDT(c_sqlxmb, null);
                            txt_nr_s.DataTextField = "nr";
                            txt_nr_s.DataValueField = "nr";
                            txt_nr_s.DataBind();
                            txt_nr_s.Items.Insert(0, new ListItem("请选择", "0"));
                        }
                        break;

                    case "资产状态":

                        if (ddcountzczt < 1)
                        {
                            string c_sqlzczt = "select nr FROM zdb_q where zdm='zt' and xh>0";

                            txt_nr_s.DataSource = SelectDT(c_sqlzczt, null);
                            txt_nr_s.DataTextField = "nr";
                            txt_nr_s.DataValueField = "nr";
                            txt_nr_s.DataBind();
                            txt_nr_s.Items.Insert(0, new ListItem("请选择", "0"));
                        }
                        break;

                    default: // 文本框的值
                        break;
                }
                txt_nr_s.SelectedIndex = txt_nr_s.Items.IndexOf(txt_nr_s.Items.FindByValue(c_nr.ToString()));
                txt_nr_s.Width = 204;
                txt_nr_s.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

    }
}
