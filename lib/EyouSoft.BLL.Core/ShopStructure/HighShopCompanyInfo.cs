using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.ShopStructure;
using System.Transactions;

namespace EyouSoft.BLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：高级网店详细信息业务逻辑接口
    /// </summary>
    public class HighShopCompanyInfo : IHighShopCompanyInfo
    {
        private readonly IDAL.ShopStructure.IHighShopCompanyInfo dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopCompanyInfo>();

        #region CreateInstance
        /// <summary>
        /// 创建高级网店详细信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.ShopStructure.IHighShopCompanyInfo CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopCompanyInfo op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopCompanyInfo>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 设置关于我们
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="AboutText">关于我们内容</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetAboutUs(string CompanyID, string AboutText)
        {
            return dal.SetAboutUs(CompanyID, AboutText);
        }
        /// <summary>
        /// 获取高级网店的详细信息
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>高级网店的详细信息实体</returns>
        public EyouSoft.Model.ShopStructure.HighShopCompanyInfo GetModel(string CompanyID)
        {
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = new EyouSoft.Model.ShopStructure.HighShopCompanyInfo();
            model = dal.GetModel(CompanyID);
            if(model != null)
                model.PositionInfo = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance().GetCompanyPositionInfo(CompanyID);

            return model;
        }
        /// <summary>
        /// 设置版权
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CopyRightText">版权内容</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetCopyRight(string CompanyID, string CopyRightText)
        {
            return dal.SetCopyRight(CompanyID, CopyRightText);
        }
        /*
        /// <summary>
        /// 设置名片
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CardLink">名片链接地址</param>
        /// <returns>false:失败 1:成功</returns>
        public int SetCardLink(string CompanyID, string CardLink)
        {
            return dal.SetCardLink(CompanyID, CardLink);
        }
        /// <summary>
        /// 设置LOGO
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="Logo">LOGO地址</param>
        /// <returns>false:失败 1:成功</returns>
        public int SetLogo(string CompanyID, string Logo)
        {
            return dal.SetLogo(CompanyID, Logo);
        }
         */ 
        /// <summary>
        /// 设置自定义模板
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TemplateID">模板编号</param>
        /// <param name="gotoUrl">网店跳转到的url</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetTemplate(string CompanyID, int TemplateID,string gotoUrl)
        {
            bool result = true;

            using (TransactionScope AddTran = new TransactionScope())
            {
                result = dal.SetTemplate(CompanyID, TemplateID);

                if (!result) return false;

                result = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().UpdateGotoUrl(CompanyID, EyouSoft.Model.SystemStructure.DomainType.网店域名, gotoUrl);

                if (!result) return false;

                AddTran.Complete();
            }

            return true;
        }
        #endregion
    }
}
