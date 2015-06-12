using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 单位附件信息(宣传图片,企业LOGO,营业执照,经营许可证,税务登记证,承诺书,企业名片等) 数据访问接口
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class CompanyAttachInfo : EyouSoft.IBLL.CompanyStructure.ICompanyAttachInfo
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyAttachInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyAttachInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyAttachInfo CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyAttachInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyAttachInfo>();
            }
            return op;
        }
        /// <summary>
        /// 设置公司LOGO
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyLogo">公司logo实体</param>
        /// <returns></returns>
        public bool SetCompanyLogo(string companyId, EyouSoft.Model.CompanyStructure.CompanyLogo companyLogo)
        {
            return idal.SetCompanyLogo(companyId, companyLogo);
        }

        /// <summary>
        /// 设置公司宣传图片
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyImg">公司宣传图片实体</param>
        /// <returns></returns>
        public bool SetCompanyImage(string companyId, EyouSoft.Model.CompanyStructure.CompanyImg companyImg)
        {
            return idal.SetCompanyImage(companyId, companyImg);
        }

        /// <summary>
        /// 设置公司高级网店头部
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="ShopBanner">公司高级网店头部实体</param>
        /// <returns></returns>
        public bool SetCompanyShopBanner(string companyId, EyouSoft.Model.CompanyStructure.CompanyShopBanner ShopBanner)
        {
            return idal.SetCompanyShopBanner(companyId, ShopBanner);
        }

        /// <summary>
        /// 设置企业名片名片
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="card">企业名片实体类</param>
        /// <returns></returns>
        public bool SetCompanyCard(string companyId, EyouSoft.Model.CompanyStructure.CardInfo card)
        {
            return idal.SetCompanyCard(companyId, card);
        }

        /// <summary>
        /// 获得公司附件信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyAttachInfo GetModel(string companyId)
        {
            return idal.GetModel(companyId);
        }

        /// <summary>
        /// 设置公司MQ广告
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mqAdv">公司MQ广告实体</param>
        /// <returns></returns>
        public bool SetCompanyMQAdv(string companyId, EyouSoft.Model.CompanyStructure.CompanyMQAdv mqAdv)
        {
            return idal.SetCompanyMQAdv(companyId, mqAdv);
        }

        /// <summary>
        /// 获得公司附件信息集合
        /// </summary>
        /// <param name="companyId">公司ID集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyAttachInfo> GetList(params string[] companyId)
        {
            List<EyouSoft.Model.CompanyStructure.CompanyAttachInfo> list = new List<EyouSoft.Model.CompanyStructure.CompanyAttachInfo>();
            foreach (string _companyId in companyId)
            {
                EyouSoft.Model.CompanyStructure.CompanyAttachInfo model = GetModel(_companyId);
                if (model != null)
                    list.Add(model);
            }
            return list;
        }
    }
}
