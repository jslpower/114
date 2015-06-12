using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-28
    /// 描述：单位设置接口
    /// </summary>
    public interface ICompanySetting
    {
        /// <summary>
        /// 修改单位设置
        /// </summary>
        /// <param name="model">单位设置实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.CompanyStructure.CompanySetting model);
        /// <summary>
        /// 获得单位设置实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanySetting GetModel(string companyid);
        /// <summary>
        /// 更新单位设置--优先展示栏目位置
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="firstMenu">要优先显示的栏目</param>
        /// <returns></returns>
        bool UpdateFirstMenu(string companyId, EyouSoft.Model.CompanyStructure.MenuSection firstMenu);
        /// <summary>
        /// 更新单位设置--订单刷新时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="minute">刷新时间(单位分钟)</param>
        /// <returns></returns>
        bool UpdateOrderRefresh(string companyId, int minute);
        /// <summary>
        /// 更新单位设置--团队自动停收时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="day">团队自动停收时间(单位天)</param>
        /// <returns></returns>
        bool UpdateTourStopTime(string companyId, int day);

        /// <summary>
        /// 更新公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="PositionInfo">地理位置信息实体</param>
        /// <returns></returns>
        bool UpdateCompanyPositionInfo(string companyId, EyouSoft.Model.ShopStructure.PositionInfo PositionInfo);

        /// <summary>
        /// 根据公司ID和字段名称获取字段值
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="FieldName">字段名称</param>
        /// <returns>字段值</returns>
        string GetFieldValueByCompanyId(string companyId, string FieldName);
    }
}
