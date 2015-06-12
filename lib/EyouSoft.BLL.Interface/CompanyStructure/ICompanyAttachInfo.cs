using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：单位附件信息(宣传图片,企业LOGO,营业执照,经营许可证,税务登记证,承诺书,企业名片等) 业务逻辑接口
    /// </summary>
    public interface ICompanyAttachInfo
    {
        /// <summary>
        /// 设置公司LOGO
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyLogo">公司logo实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYLOGO_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYLOGO_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYLOGO_UPDATE_CODE
            )]
        bool SetCompanyLogo(string companyId, EyouSoft.Model.CompanyStructure.CompanyLogo companyLogo);

        /// <summary>
        /// 设置公司宣传图片
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyImg">公司宣传图片实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYIMAGE_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYIMAGE_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYIMAGE_UPDATE_CODE
            )]
        bool SetCompanyImage(string companyId, EyouSoft.Model.CompanyStructure.CompanyImg companyImg);
         /// <summary>
        /// 设置公司高级网店头部
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="ShopBanner">公司高级网店头部实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
           LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSHOPBANNER_UPDATE_TITLE,
           LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSHOPBANNER_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
           LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
           EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSHOPBANNER_UPDATE_CODE
           )]
        bool SetCompanyShopBanner(string companyId, EyouSoft.Model.CompanyStructure.CompanyShopBanner ShopBanner);
        /// <summary>
        /// 设置企业名片名片
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="card">企业名片实体类</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYCARD_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYCARD_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYCARD_UPDATE_CODE
            )]
        bool SetCompanyCard(string companyId, EyouSoft.Model.CompanyStructure.CardInfo card);
        /// <summary>
        /// 设置公司MQ广告
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mqAdv">公司MQ广告实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYMQADV_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYMQADV_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYMQADV_UPDATE_CODE
            )]
        bool SetCompanyMQAdv(string companyId, EyouSoft.Model.CompanyStructure.CompanyMQAdv mqAdv);
        /// <summary>
        /// 获得公司附件信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyAttachInfo GetModel(string companyId);
        /// <summary>
        /// 获得公司附件信息集合
        /// </summary>
        /// <param name="companyId">公司ID集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyAttachInfo> GetList(params string[] companyId);
    }
}
