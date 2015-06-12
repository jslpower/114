using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：首页轮换广告
    /// </summary>
    public class SysIndexAdv
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysIndexAdv() { }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 广告性质
        /// </summary>
        public int AdvType
        {
            get;
            set;
        }
        /// <summary>
        /// 分站
        /// </summary>
        public int SiteId
        {
            get;
            set;
        }
        /// <summary>
        /// 广告图片路径
        /// </summary>
        public string ImgPath
        {
            get;
            set;
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAdress
        {
            get;
            set;
        }
        /// <summary>
        /// 图片说明
        /// </summary>
        public string ImgRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }
}
