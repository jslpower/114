using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 专线类型
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class RouteType
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteType()
        { }

        #region Model

        private int _id;
        private string _typename;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }

        #endregion Model
    }
}
