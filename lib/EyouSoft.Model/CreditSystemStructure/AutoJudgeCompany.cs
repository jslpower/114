using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-自动好评数据实体类
    /// </summary>
    public class AutoJudgeCompany
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoJudgeCompany()
		{}
		#region Model
		/// <summary>
		/// 被处理为默认好评的卖家公司ID,默认为0
		/// </summary>
        public int CompanyId { get; set; }
		/// <summary>
		/// 自动好评获得的分值  默认为0
		/// </summary>
        public double RateScore { get; set; }
		#endregion Model
    }
}
