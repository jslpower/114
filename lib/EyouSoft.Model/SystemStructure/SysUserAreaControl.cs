using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统管理员区域
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysUserAreaControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysUserAreaControl()
		{}

		#region Model

		private int _id;
		private int _areaid;
		private int _userid;
		/// <summary>
		/// 编号
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 区域编号
		/// </summary>
		public int AreaId
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}

		#endregion Model
    }
}
