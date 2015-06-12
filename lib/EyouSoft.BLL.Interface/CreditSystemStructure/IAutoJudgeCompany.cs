using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-interface for the AutoJudgeCompany BLL
    /// </summary>
    public interface IAutoJudgeCompany
    {
        /// <summary>
        /// 获得所有自动好评的公司信息
        /// </summary>
        IList<EyouSoft.Model.CreditSystemStructure.AutoJudgeCompany> GetAutoJudgeCompany();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        void Delete();
    }
}
