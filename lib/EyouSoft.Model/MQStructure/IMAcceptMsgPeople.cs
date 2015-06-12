using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// MQ订单提醒设置实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMAcceptMsgPeople
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMAcceptMsgPeople()
		{}

		#region Model

		private string _id;
		private string _companyid;
		private string _operatorid;
		private int _tourareaid;

		/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司编号
		/// </summary>
		public string CompanyId
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 操作人编号
		/// </summary>
		public string OperatorId
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}

		/// <summary>
		/// 线路区域编号
		/// </summary>
		public int TourAreaId
		{
			set{ _tourareaid=value;}
			get{return _tourareaid;}
		}

		#endregion Model
    }
}
