using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace EyouSoft.Common.Function
{
    /// <summary>
    /// 枚举处理类
    /// </summary>
    /// 周文超 2010-07-26
    public class EnumHandle
    {
        /// <summary>
        /// 获取枚举的键值对
        /// </summary>
        /// <param name="EnumType">枚举类型（为typeof参数，即typeof(需要读取的枚举)）</param>
        /// <returns></returns>
        public static NameValueCollection GetNVCFromEnumValue(Type EnumType)
        {
            if (EnumType.Equals(null) || EnumType.IsEnum == false)
                return null;

            NameValueCollection nvc = new NameValueCollection();
            foreach (int i in Enum.GetValues(EnumType))
            {
                nvc.Add(Enum.GetName(EnumType, i), i.ToString());
            }
            return nvc;
        }

        /// <summary>
        /// 将枚举转换为System.Web.UI.WebControls.ListItem
        /// </summary>
        /// <param name="EnumType">枚举类型（为typeof参数，即typeof(需要读取的枚举)）</param>
        /// <returns></returns>
        public static IList<System.Web.UI.WebControls.ListItem> GetListEnumValue(Type EnumType)
        {
            if (EnumType.Equals(null) || EnumType.IsEnum == false)
                return null;

            IList<System.Web.UI.WebControls.ListItem> List = new List<System.Web.UI.WebControls.ListItem>();
            foreach (int i in Enum.GetValues(EnumType))
            {
                System.Web.UI.WebControls.ListItem listitem = new System.Web.UI.WebControls.ListItem(Enum.GetName(EnumType, i), i.ToString());
                List.Add(listitem);
            }
            return List;
        }
    }
}
