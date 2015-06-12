using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-13
    /// 描述：供求栏目固定文字实体类
    /// </summary>
    public class SiteTopic
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SiteTopic() { }

        #region 属性
        /// <summary>
        /// key====[访谈介绍,Interview],[同业交流专区,CommArea],[学堂介绍,School]
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }
        /// <summary>
        /// value
        /// </summary>
        public string FieldValue
        {
            get;
            set;
        }
        #endregion
    }
}
