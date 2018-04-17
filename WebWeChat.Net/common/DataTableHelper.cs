using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebWeChat.Net.common
{
    public class DataTableHelper
    {
        /// <summary>
        /// DataRow 转换为对应的类
        /// </summary>
        /// <param name="adaptedRow"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static object ToEntity(DataRow adaptedRow, Type entityType)
        {
            if (entityType == null || adaptedRow == null)
            {
                return null;
            }

            object entity = Activator.CreateInstance(entityType);
            CopyToEntity(entity, adaptedRow);

            return entity;
        }
        /// <summary>
        /// DataRow 转换为对应的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="adaptedRow"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEntity<T>(DataRow adaptedRow, T value) where T : new()
        {
            T item = new T();
            if (value == null || adaptedRow == null)
            {
                return item;
            }

            item = Activator.CreateInstance<T>();
            CopyToEntity(item, adaptedRow);

            return item;
        }

        private static void CopyToEntity(object entity, DataRow adaptedRow)
        {
            if (entity == null || adaptedRow == null)
            {
                return;
            }
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (!CanSetPropertyValue(propertyInfo, adaptedRow))
                {
                    continue;
                }

                try
                {
                    if (adaptedRow[propertyInfo.Name

] is DBNull)
                    {
                        propertyInfo.SetValue(entity, null, null);
                        continue;
                    }
                    SetPropertyValue(entity, adaptedRow, propertyInfo);
                }
                finally
                {

                }
            }
        }

        private static bool CanSetPropertyValue(PropertyInfo propertyInfo, DataRow adaptedRow)
        {
            if (!propertyInfo.CanWrite)
            {
                return false;
            }
            Boolean isContinue = false;
            foreach (DataColumn column in adaptedRow.Table.Columns)
            {
                if (column.ColumnName.ToUpper().Equals(propertyInfo.Name

.ToUpper()))
                {
                    isContinue = true;
                    break;
                }
            }
            if (!isContinue)
            {
                return false;
            }
            return true;
        }

        private static void SetPropertyValue(object entity, DataRow adaptedRow, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(DateTime?) ||
                propertyInfo.PropertyType == typeof(DateTime))
            {
                DateTime date = DateTime.MaxValue;
                DateTime.TryParse(adaptedRow[propertyInfo.Name

.ToLower()].ToString(),
                    CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

                propertyInfo.SetValue(entity, date, null);
            }
            else if (propertyInfo.PropertyType == typeof(Int32?) ||
              propertyInfo.PropertyType == typeof(Int64?) ||
              propertyInfo.PropertyType == typeof(Int16?) ||
              propertyInfo.PropertyType == typeof(Int32) ||
              propertyInfo.PropertyType == typeof(Int64) ||
              propertyInfo.PropertyType == typeof(Int16))
            {
                int value = Convert.ToInt32(adaptedRow[propertyInfo.Name

.ToLower()]);
                propertyInfo.SetValue(entity, value, null);
            }
            else
            {
                propertyInfo.SetValue(entity, adaptedRow[propertyInfo.Name

.ToLower()], null);
            }
        }
    }
}
