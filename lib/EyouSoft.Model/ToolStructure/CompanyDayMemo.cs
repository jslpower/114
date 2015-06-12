using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure.MemoDetailType
{
    /// <summary>
    /// 公司备忘录紧急度
    /// </summary>
    public enum UrgentType
    {
        /// <summary>
        /// 一般
        /// </summary>
        Normal,
        /// <summary>
        /// 紧急
        /// </summary>
        Urgent
    }

    /// <summary>
    /// 公司备忘录完成状态
    /// </summary>
    public enum MemoState
    {
        /// <summary>
        /// 未完成
        /// </summary>
        Incomplete,
        /// <summary>
        /// 已完成
        /// </summary>
        Complete,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancel,
    }

}

namespace EyouSoft.Model.ToolStructure
{
    using EyouSoft.Model.ToolStructure.MemoDetailType;

    /// <summary>
    /// 创建人：蒋胜蓝 2010-05-12
    /// 描述：公司备忘录
    /// </summary>
    public class CompanyDayMemo
    {
        #region 属性

        /// <summary>
        /// 编号  
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string MemoTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string MemoText
        {
            get;
            set;
        }
        /// <summary>
        /// 紧急度
        /// </summary>
        public UrgentType UrgentType
        {
            get;
            set;
        }
        /// <summary>
        /// 完成状态
        /// </summary>
        public MemoState MemoState
        {
            get;
            set;
        }
        /// <summary>
        /// 备忘录时间
        /// </summary>
        public DateTime MemoTime
        {
            get;
            set;
        }  
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }       
        #endregion      
    }    
}

