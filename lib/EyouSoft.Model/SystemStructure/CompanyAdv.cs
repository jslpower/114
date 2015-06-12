using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：后台登陆页广告
    /// </summary>
    public class CompanyAdv
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyAdv() { }

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
        /// 公司性质
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyType AdvCompanyType
        {
            get;
            set;
        }
        /// <summary>
        /// 广告类型(0:文字 1:图片)
        /// </summary>
        public int AdvType
        {
            get;
            set;
        }
        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 广告内容
        /// </summary>
        public string AdvContent
        {
            get;
            set;
        }
        /// <summary>
        /// 广告链接
        /// </summary>
        public string AdvLink
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为新窗口打开
        /// </summary>
        public bool IsNewOpen
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
        /// 广告图片路径
        /// </summary>
        public string ImgPath
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        ///  创建时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 内部公告链接页面格式如：aaa.aspx?id=
        /// </summary>
        public string InternalLink
        {
            get;
            set;
        }
        #endregion
    }
}
