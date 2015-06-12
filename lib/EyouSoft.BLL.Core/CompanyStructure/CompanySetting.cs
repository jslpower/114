using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 单位设置业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class CompanySetting : EyouSoft.IBLL.CompanyStructure.ICompanySetting
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanySetting idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanySetting>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanySetting CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanySetting op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanySetting>();
            }
            return op;
        }
        /// <summary>
        /// 修改单位设置[暂不开放该方法]
        /// </summary>
        /// <param name="model">单位设置实体</param>
        /// <returns></returns>
        private bool Update(EyouSoft.Model.CompanyStructure.CompanySetting model)
        {
            return idal.Update(model);
        }
        /// <summary>
        /// 获得单位设置实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanySetting GetModel(string companyid)
        {
            return idal.GetModel(companyid);
        }
        /// <summary>
        /// 更新单位设置--优先展示栏目位置
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="firstMenu">要优先显示的栏目</param>
        /// <returns></returns>
        public bool UpdateFirstMenu(string companyId, EyouSoft.Model.CompanyStructure.MenuSection firstMenu)
        {
            return idal.UpdateFirstMenu(companyId, firstMenu);
        }
        /// <summary>
        /// 更新单位设置--订单刷新时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="minute">刷新时间(单位分钟)</param>
        /// <returns></returns>
        public bool UpdateOrderRefresh(string companyId, int minute)
        {
            return idal.UpdateOrderRefresh(companyId, minute);
        }
        /// <summary>
        /// 更新单位设置--团队自动停收时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="day">团队自动停收时间(单位天)</param>
        /// <returns></returns>
        public bool UpdateTourStopTime(string companyId, int day)
        {
            return idal.UpdateTourStopTime(companyId, day);
        }

        /// <summary>
        /// 更新公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="PositionInfo">地理位置信息实体</param>
        /// <returns></returns>
        public bool UpdateCompanyPositionInfo(string companyId, EyouSoft.Model.ShopStructure.PositionInfo PositionInfo)
        {
            if (string.IsNullOrEmpty(companyId) || PositionInfo == null)
                return false;

            return idal.UpdateCompanyPositionInfo(companyId, PositionInfo);
        }

        /// <summary>
        /// 获取公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>公司地理位置信息实体</returns>
        public EyouSoft.Model.ShopStructure.PositionInfo GetCompanyPositionInfo(string companyId)
        {
            EyouSoft.Model.ShopStructure.PositionInfo PositionInfo = new EyouSoft.Model.ShopStructure.PositionInfo();
            if (string.IsNullOrEmpty(companyId))
                return PositionInfo;

            string strTmp = idal.GetFieldValueByCompanyId(companyId, "PositionInfo");
            if (string.IsNullOrEmpty(strTmp))
                return PositionInfo;

            string[] strTmpArr = strTmp.Split(',');
            if (strTmpArr == null || strTmpArr.Length < 2)
                return PositionInfo;

            PositionInfo.Longitude = double.Parse(strTmpArr[0]);
            PositionInfo.Latitude = double.Parse(strTmpArr[1]);
            if(strTmpArr.Length==3)
                PositionInfo.ZoomLevel = int.Parse(strTmpArr[2]);
            return PositionInfo;
        }
    }
}
