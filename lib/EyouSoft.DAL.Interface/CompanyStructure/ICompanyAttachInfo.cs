using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-01
    /// 描述：单位附件信息(宣传图片,企业LOGO,营业执照,经营许可证,税务登记证,承诺书,企业名片等) 数据访问接口
    /// </summary>
    public interface ICompanyAttachInfo
    {
        /// <summary>
        /// 设置公司LOGO
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyLogo">公司logo实体</param>
        /// <returns></returns>
        bool SetCompanyLogo(string companyId, EyouSoft.Model.CompanyStructure.CompanyLogo companyLogo);
        /// <summary>
        /// 设置公司宣传图片
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyImg">公司宣传图片实体</param>
        /// <returns></returns>
        bool SetCompanyImage(string companyId, EyouSoft.Model.CompanyStructure.CompanyImg companyImg);
        /// <summary>
        /// 设置公司高级网店头部
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="ShopBanner">公司高级网店头部实体</param>
        /// <returns></returns>
        bool SetCompanyShopBanner(string companyId, EyouSoft.Model.CompanyStructure.CompanyShopBanner ShopBanner);
        /// <summary>
        /// 设置企业名片名片
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="card">企业名片实体类</param>
        /// <returns></returns>
        bool SetCompanyCard(string companyId, EyouSoft.Model.CompanyStructure.CardInfo card);
        /// <summary>
        /// 设置公司MQ广告
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mqAdv">公司MQ广告实体</param>
        /// <returns></returns>
        bool SetCompanyMQAdv(string companyId, EyouSoft.Model.CompanyStructure.CompanyMQAdv mqAdv);
        /// <summary>
        /// 获得公司附件信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyAttachInfo GetModel(string companyId);
        /// <summary>
        /// 设置公司所有的附件信息(必须保证每个不进行修改的字段,都需要保持原来本身的值)
        /// </summary>
        /// <param name="model">公司所有的附件信息(必须保证每个不进行修改的字段,都需要保持原来本身的指)</param>
        /// <returns></returns>
        bool SetCompanyAttachInfo(EyouSoft.Model.CompanyStructure.CompanyAttachInfo model);
    }
}
