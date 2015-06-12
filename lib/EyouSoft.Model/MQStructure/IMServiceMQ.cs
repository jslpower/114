using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// MQ客服实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMServiceMQ
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMServiceMQ()
		{}

		#region Model

		private int _id;
		private int _mqid;
		private bool _isdefault;
		private int _provinceid;

		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// MQ编号
		/// </summary>
		public int MQId
		{
			set{ _mqid=value;}
			get{return _mqid;}
		}
		/// <summary>
		/// 是否默认客服
		/// </summary>
		public bool IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 服务省份
		/// </summary>
		public int ProvinceId
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}

		#endregion Model
    }
}
