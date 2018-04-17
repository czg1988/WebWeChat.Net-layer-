using Implement.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Web.common
{
    public class Tools
    {


        /// <summary>  
        /// 获取结果集  
        /// </summary>  
        /// <param name="rel">状态</param>  
        /// <param name="msg">提示信息</param>  
        /// <param name="data">数据集</param>         
        /// <returns></returns>  
        public static object GetResult(bool rel, string msg, object data)
        {
            return new Dictionary<string, object> { { "rel", rel }, { "msg", msg }, { "obj", data } };
        }


        /// <summary>
        /// Json转换成实体类，返回对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="jsonString">反序列化字符串</param>
        /// <returns>反序列化后的值</returns>
        public static T JsonToModel<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    T returnOjbect = (T)serializer.ReadObject(ms);
                    return returnOjbect;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        /// Json转换成List集合，返回对象List
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="jsonString">反序列化字符串</param>
        /// <returns>反序列化后的值</returns>
        public static List<T> JsonToList<T>(string jsonString)
        {
            return JsonToModel<List<T>>(jsonString);
        }


        internal static string GetSrq(string v1)
        {
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(v1) || (Convert.ToDateTime(v1, dtFormat) == Convert.ToDateTime("1900-01-01", dtFormat)))
            {
                v1 = new DateTime(now.Year, now.Month, 1).ToString("yyyy.MM.dd");
            }
            else
            {
                v1 = Convert.ToDateTime(v1, dtFormat).ToString("yyyy.MM.dd");
            }
            return v1;
        }


        internal static string GetErq(string v2)
        {
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(v2) || (Convert.ToDateTime(v2, dtFormat) == Convert.ToDateTime("1900-01-01", dtFormat)))
            {
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                v2 = d1.AddMonths(1).AddDays(-1).ToString("yyyy.MM.dd");
            }
            else
            {
                v2 = Convert.ToDateTime(v2, dtFormat).ToString("yyyy.MM.dd");
            }
            return v2;
        }

        //
        public static class ModelConvertHelper<T> where T : new()  // 此处一定要加上new()
        {

            public static IList<T> ConvertToModel(DataTable dt)
            {

                IList<T> ts = new List<T>();// 定义集合
                Type type = typeof(T); // 获得此模型的类型
                string tempName = "";
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;
                        if (dt.Columns.Contains(tempName))
                        {
                            if (!pi.CanWrite) continue;
                            object value = dr[tempName];
                            if (value != DBNull.Value)
                                pi.SetValue(t, value, null);
                        }
                    }
                    ts.Add(t);
                }
                return ts;
            }
        }

        //--------------设备分页搜索---------------
        //PageIndex 当前页码
        //PageSize  每页条数
        //Condition 表明+条件
        //RecordCount 总记录数
        //PageCount 总页数
        //AllFields查询字段
        //IndexField 排序字段
        public static DataTable GetPageList(int PageIndex, int PageSize, string AllFields, string IndexField, string Condition, string OrderFields, out int RecordCount, out int PageCount, SqlParameter[] param)
        {
            return DbHelper.ExecutePageNew(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }


        /// <summary>
        /// 为字符串中的非英文字符编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///指定 一个字符是否应该被编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 为非英文字符串编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }


    }
}