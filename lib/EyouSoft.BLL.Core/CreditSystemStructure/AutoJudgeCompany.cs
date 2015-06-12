using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;


namespace EyouSoft.BLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-订单自动好评的公司信息业务逻辑类
    /// </summary>
    public class AutoJudgeCompany : EyouSoft.IBLL.CreditSystemStructure.IAutoJudgeCompany
    {
        private readonly EyouSoft.IDAL.CreditSystemStructure.IAutoJudgeCompany idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CreditSystemStructure.IAutoJudgeCompany>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CreditSystemStructure.IAutoJudgeCompany CreateInstance()
        {
            EyouSoft.IBLL.CreditSystemStructure.IAutoJudgeCompany op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CreditSystemStructure.IAutoJudgeCompany>();
            }
            return op;
        }

        /// <summary>
        /// 获得所有自动好评的公司信息
        /// </summary>
        public IList<EyouSoft.Model.CreditSystemStructure.AutoJudgeCompany> GetAutoJudgeCompany()
        {
            return idal.GetAutoJudgeCompany();
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void Delete()
        {
            idal.Delete();
        }
    }
}
