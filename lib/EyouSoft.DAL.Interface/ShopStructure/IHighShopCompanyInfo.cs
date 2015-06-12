using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：高级网店详细信息接口
    /// </summary>
    public interface IHighShopCompanyInfo
    {
        /// <summary>
        /// 设置关于我们
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="AboutText">关于我们内容</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetAboutUs(string CompanyID, string AboutText);
        /// <summary>
        /// 获取高级网店的详细信息
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>高级网店的详细信息实体</returns>
        EyouSoft.Model.ShopStructure.HighShopCompanyInfo GetModel(string CompanyID);
        /// <summary>
        /// 设置版权
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CopyRightText">版权内容</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetCopyRight(string CompanyID, string CopyRightText);
        /*
        /// <summary>
        /// 设置名片
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CardLink">名片链接地址</param>
        /// <returns>0:失败 1:成功</returns>
        int SetCardLink(string CompanyID, string CardLink);
        /// <summary>
        /// 设置LOGO
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="Logo">LOGO地址</param>
        /// <returns>0:失败 1:成功</returns>
        int SetLogo(string CompanyID, string Logo);
        */
        /// <summary>
        /// 设置自定义模板
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TemplateID">模板编号</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetTemplate(string CompanyID, int TemplateID);
    }
}
