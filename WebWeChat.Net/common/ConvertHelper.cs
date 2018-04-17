using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Web.common
{
    public class ConvertHelper : IHttpModule
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
        }

        public void Init(HttpApplication context)
        {
            // 下面是如何处理 LogRequest 事件并为其 
            // 提供自定义日志记录实现的示例
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //可以在此处放置自定义日志记录逻辑
        }
        public static ArrayList GetJSonArrayList(DataTable _dt)
        {
            ArrayList m_R = new ArrayList();
            foreach (DataRow m_Row in _dt.Rows)
            {
                Dictionary<string, object> m_Di = new Dictionary<string, object>();
                foreach (DataColumn m_Col in _dt.Columns)
                    m_Di.Add(m_Col.ColumnName, m_Row[m_Col]);
                m_R.Add(m_Di);
            }
            return m_R;
        }
    }
}
