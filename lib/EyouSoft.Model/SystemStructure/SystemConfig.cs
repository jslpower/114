using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统设置
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SystemConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemConfig()
        { }

        #region Model

        private int _systemid;
        private int _tourviewtype;
        private int _visitviewtype;

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 团队默认查看方式
        /// </summary>
        public int TourViewType
        {
            set { _tourviewtype = value; }
            get { return _tourviewtype; }
        }
        /// <summary>
        /// 组团社（零售商）散客报名界面产品默认显示方式
        /// </summary>
        public int VisitViewType
        {
            set { _visitviewtype = value; }
            get { return _visitviewtype; }
        }

        #endregion Model
    }
}
