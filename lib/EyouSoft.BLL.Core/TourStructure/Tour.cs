using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 团队业务逻辑层
    /// </summary>
    public class Tour : EyouSoft.IBLL.TourStructure.ITour
    {
        private readonly IDAL.TourStructure.ITour dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITour>();

        #region CreateInstance
        /// <summary>
        /// 创建团队信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TourStructure.ITour CreateInstance()
        {
            EyouSoft.IBLL.TourStructure.ITour op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.TourStructure.ITour>();
            }
            return op1;
        }
        #endregion

        #region private member
        /// <summary>
        /// 获取当前用户分管的线路区域 多个线路区域用","间隔 管理员用户返回null
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserArea()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentUserArea();
        }

        /// <summary>
        /// 获取用户分管的线路区域字符串 多个线路区域用","间隔
        /// </summary>
        /// <param name="userAreas">线路区域集合</param>
        /// <returns></returns>
        private string GetCurrentUserArea(int[] userAreas)
        {
            if (userAreas == null || userAreas.Length < 1) { return string.Empty; }

            StringBuilder areas = new StringBuilder();

            areas.Append(userAreas[0].ToString());

            for (int i = 1; i < userAreas.Length; i++)
            {
                areas.AppendFormat(",{0}", userAreas[i].ToString());
            }

            return areas.ToString();
        }

        /// <summary>
        /// 获取当前登录用户的公司编号 未登录返回null
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserCompanyId()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentUserCompanyId();
        }

        /// <summary>
        /// 获取当前用户(运营后台)用户分管的线路区域 多个线路区域用","间隔
        /// </summary>
        /// <returns></returns>
        private string GetWebMasterArea()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentWebMasterArea();
        }

        /// <summary>
        /// 获取团队推广状态名称(含CSS样式)
        /// </summary>
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <returns></returns>
        private string GetTourSpreadStateNameHavingStyle(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription)
        {
            string s = string.Empty;
            
            if (tourSpreadState != EyouSoft.Model.TourStructure.TourSpreadState.None)
            {
                //s = string.Format("<a href=\"javascript:void(0)\" class=\"state{0}\"><nobr>{1}</nobr></a>", (int)tourSpreadState, tourSpreadState.ToString());
                s = string.Format("<span onmouseout=\"tooltip.hide();\" onmouseover=\"tooltip.show('<strong>推广名称理由：</strong>{0}');\" class=\"state{1}\">{2}</span> ", tourSpreadDescription, (int)tourSpreadState, tourSpreadState.ToString());
            }
            
            return s;
        }
        #endregion

        #region 团队基本操作
        /// <summary>
        /// 新增团队信息(模板团)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public int InsertTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            return dal.InsertTemplateTourInfo(model);
        }

        /*/// <summary>
        /// 修改团队信息(模板团)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <param name="insertTours">要添加的子团信息集合</param>
        /// <param name="updateTours">要更新的子团信息集合</param>
        /// <param name="deleteTours">要删除的子团信息集合</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public int UpdateTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> insertTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> updateTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> deleteTours)
        {
            int updateResult = 0;

            if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
            {
                updateResult = dal.UpdateQuickTemplateTourInfo(model, insertTours, updateTours, deleteTours);
            }
            else
            {
                updateResult = dal.UpdateStandardTemplateTourInfo(model, insertTours, updateTours, deleteTours);
            }

            return updateResult;
        }*/

        /// <summary>
        /// 修改团队信息(模板团)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public int UpdateTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            return dal.UpdateTemplateTourInfo(model);
        }

        /// <summary>
        /// 追加模板团发团计划信息
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns></returns>
        public int AppendTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            return dal.AppendTemplateTourInfo(model);
        }

        /// <summary>
        /// 修改团队信息(单个子团)
        /// </summary>
        /// <param name="model">团队信息业务实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public int UpdateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            if (string.IsNullOrEmpty(model.ID)) { return 0; }

            return dal.UpdateTourInfo(model);
        }

        /// <summary>
        /// 删除团队(虚拟删除)
        /// </summary>
        /// <param name="tourId">团队ID</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public bool DeleteByVirtual(string tourId)
        {
            bool tmp = false;

            if (string.IsNullOrEmpty(tourId)) { return tmp; }

            EyouSoft.SSOComponent.Entity.UserInfo userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();

            if (userInfo == null) { return tmp; }

            tmp = dal.DeleteByVirtual(userInfo.CompanyID, tourId, true);

            userInfo = null;

            return tmp;
        }

        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>返回团队信息业务实体</returns>
        public EyouSoft.Model.TourStructure.TourInfo GetTourInfo(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) { return null; }

            EyouSoft.Model.TourStructure.TourInfo tourInfo = dal.GetTourInfo(tourId);

            if (tourInfo != null)
            {
                tourInfo.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tourInfo.TourSpreadState, tourInfo.TourSpreadDescription);
                tourInfo.TourPriceDetail = this.GetTourPriceDetail(tourId);

                if (string.IsNullOrEmpty(tourInfo.ParentTourID)) return tourInfo;

                #region 行程、服务标准、地接社信息处理
                //快速发布行程为空时取模板团行程信息
                if (tourInfo.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick
                    && string.IsNullOrEmpty(tourInfo.QuickPlan))
                {
                    tourInfo.QuickPlan = dal.GetTourInfoQuickPlan(tourInfo.ParentTourID);
                }

                //标准发布计划行程、服务标准、地接社信息
                if (tourInfo.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)
                {
                    if (tourInfo.StandardPlan == null || tourInfo.StandardPlan.Count == 0)
                    {
                        tourInfo.StandardPlan = dal.GetTourInfoStandardPlans(tourInfo.ParentTourID);
                    }

                    if (tourInfo.ServiceStandard == null)
                    {
                        tourInfo.ServiceStandard = dal.GetTourServiceStandard(tourInfo.ParentTourID);
                    }

                    if (tourInfo.LocalTravelAgency == null || tourInfo.LocalTravelAgency.Count == 0)
                    {
                        tourInfo.LocalTravelAgency = dal.GetTourLocalAgencys(tourInfo.ParentTourID);
                    }
                }
                #endregion
            }

            return tourInfo;
        }

        /// <summary>
        /// 审核团队
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public bool SetChecked(string tourId, bool isChecked)
        {
            if (string.IsNullOrEmpty(tourId)) { return false; }

            return dal.SetChecked(tourId, isChecked);
        }

        /// <summary>
        /// 设置团队收客状态
        /// </summary>
        /// <param name="tourState">团队状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public bool SetTourState(EyouSoft.Model.TourStructure.TourState tourState, params string[] tourId)
        {
            if (tourId.Length < 1 || tourState == EyouSoft.Model.TourStructure.TourState.自动客满 || tourState == EyouSoft.Model.TourStructure.TourState.自动停收) { return false; }

            return dal.SetTourState(tourState, tourId);
        }

        /// <summary>
        /// 设置团队推广状态
        /// </summary>        
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public bool SetTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, params string[] tourId)
        {
            if (tourId.Length < 1) { return false; }

            return dal.SetTourSpreadState(tourSpreadState, tourSpreadDescription, tourId);
        }

        /// <summary>
        /// 按模板团设置团队推广状态
        /// </summary>
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool SetTemplateTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) { return false; }

            return dal.SetTemplateTourSpreadState(tourSpreadState, tourSpreadDescription, tourId);
        }

        /// <summary>
        /// 获取团队报价信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>报价信息业务实体集合</returns>
        public IList<EyouSoft.Model.TourStructure.TourPriceDetail> GetTourPriceDetail(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) { return null; }
            string companyId, parentTourId;

            IList<EyouSoft.Model.TourStructure.TourPriceDetail> details = dal.GetTourPriceDetail(tourId, out companyId, out parentTourId);

            //价格信息没有获取到取模板团价格信息
            if ((details == null || details.Count == 0) && !string.IsNullOrEmpty(parentTourId))
            {
                details = dal.GetTourPriceDetail(parentTourId, out companyId, out parentTourId);
            }

            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> stands = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance().GetList(companyId);

            if (stands != null && stands.Count > 0 && details != null && details.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourPriceDetail tmpPrice in details)
                {
                    foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand tmpStand in stands)
                    {
                        if (tmpPrice.PriceStandId == tmpStand.ID)
                        {
                            tmpPrice.PriceStandName = tmpStand.PriceStandName;
                            break;
                        }
                    }
                }
            }

            return details;
        }

        /// <summary>
        /// 设置团队剩余人数 当设置的剩余人数大于团队计划人数时将会把剩余人数设置成团队的计划人数
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="remnantNumber">剩余人数</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public bool SetTourRemnantNumber(string tourId, int remnantNumber)
        {
            //if (string.IsNullOrEmpty(tourId) || remnantNumber < 1) { return false; }
            if (string.IsNullOrEmpty(tourId)) { return false; }

            return dal.SetTourRemnantNumber(tourId, remnantNumber);
        }

        /// <summary>
        /// 批量生成团号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="leaveDate">出团日期</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.AutoTourCodeInfo> CreateAutoTourCodes(string companyId, int areaId, params DateTime[] leaveDate)
        {
            if (string.IsNullOrEmpty(companyId) || areaId < 1 || leaveDate.Length < 1) { return null; }

            return dal.CreateAutoTourCodes(companyId, areaId, leaveDate);
        }

        /// <summary>
        /// 判断团队是否已删除
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool IsDeleted(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? true : dal.IsDeleted(tourId);
        }
        #endregion

        #region 列表相关

        /// <summary>
        /// 按线路区域分组获取模板团列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="leaveCityId">出港城市编号 为null时所有出港城市</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TemplateTourInfo> GetTemplateTours(string companyId, string userId, string userAreas, int? leaveCityId)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TemplateTourInfo> tours = dal.GetTemplateTours(companyId, userId, userAreas, leaveCityId);

            if (tours != null && tours.Count > 0)
            {
                EyouSoft.IBLL.SystemStructure.ISysArea sysAreaBLL = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();

                foreach (EyouSoft.Model.TourStructure.TemplateTourInfo tmp in tours)
                {
                    if (tmp.TemplateTour != null)
                        tmp.TourNumber = tmp.TemplateTour.Count;

                    EyouSoft.Model.SystemStructure.SysArea sysAreaInfo = sysAreaBLL.GetSysAreaModel(tmp.AreaId);

                    if (sysAreaInfo != null)
                    {
                        tmp.AreaName = sysAreaInfo.AreaName;
                    }

                    sysAreaInfo = null;
                }

                sysAreaBLL = null;
            }

            return tours;
        }

        /// <summary>
        /// 获取未出发团队信息集合(模板团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount, string companyId, int? areaId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = dal.GetNotStartingTours(pageSize, pageIndex, ref recordCount, companyId, areaId);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourBasicInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取未出发团队信息集合(子团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="templateTourId">模板团编号</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="searchTourState">团队状态</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount
            , string templateTourId, string tourCode, EyouSoft.Model.TourStructure.SearchTourState searchTourState
            , DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            if (string.IsNullOrEmpty(templateTourId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = dal.GetNotStartingTours(pageSize, pageIndex, ref recordCount,
                templateTourId, tourCode, searchTourState, startLeaveDate, finishLeaveDate);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourBasicInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }


            return tours;
        }

        /// <summary>
        /// 获取已出发团队信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetStartingTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string tourCode, string routeName
            , int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = dal.GetStartingTours(pageSize, pageIndex, ref recordCount, companyId,
                this.GetCurrentUserArea(), tourCode, routeName, tourDays, startLeaveDate, finishLeaveDate);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourBasicInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取快到期产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetComingExpireTours(int pageSize, int pageIndex, ref int recordCount
            , int? areaId, int? cityId, string companyName, string routeName)
        {
            DateTime expireTime = DateTime.Today.AddMonths(1);

            return dal.GetComingExpireTours(pageSize, pageIndex, ref recordCount, expireTime, this.GetWebMasterArea()
                , areaId, cityId, companyName, routeName);
        }

        /// <summary>
        /// 获取关注批发商产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>        
        /// <param name="attentionCompanyId">关注的批发商编号 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <param name="areaType">线路区域类型 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetAttentionTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId
            , int? leaveCityId, int? areaId, string routeName
            , string attentionCompanyId, DateTime? startLeaveDate, DateTime? finishLeaveDate
            , EyouSoft.Model.SystemStructure.AreaType? areaType)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TourInfo> tours = dal.GetAttentionTours(pageSize, pageIndex, ref recordCount
                , companyId, leaveCityId, areaId
                , routeName, attentionCompanyId, startLeaveDate, finishLeaveDate, areaType);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取未处理订单的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">要查询的用户编号 为空时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> GetUndisposedOrderTours(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId)
        {
            EyouSoft.Model.TourStructure.OrderState[] defaulUndisposedOrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.未处理
                , EyouSoft.Model.TourStructure.OrderState.处理中 };

            return dal.GetHavingOrderTours(pageSize, pageIndex, ref recordCount
                , companyId, false, userId, this.GetCurrentUserArea()
                , null, null, null
                , null, null, defaulUndisposedOrderState);
        }

        /// <summary>
        /// 获取已处理订单的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">要查询的用户编号 为null时不做为查询条件</param>
        /// <param name="orderState">订单状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> GetHandledOrderTours(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId
            , EyouSoft.Model.TourStructure.OrderState? orderState)
        {
            EyouSoft.Model.TourStructure.OrderState[] defaultHandledOrderState = new EyouSoft.Model.TourStructure.OrderState[]{EyouSoft.Model.TourStructure.OrderState.不受理,
                EyouSoft.Model.TourStructure.OrderState.处理中,
                EyouSoft.Model.TourStructure.OrderState.留位过期,
                EyouSoft.Model.TourStructure.OrderState.已成交,
                EyouSoft.Model.TourStructure.OrderState.已留位};

            if (orderState.HasValue)
            {
                defaultHandledOrderState = new EyouSoft.Model.TourStructure.OrderState[] { orderState.Value };
            }

            return dal.GetHavingOrderTours(pageSize, pageIndex, ref recordCount,
                companyId, false, userId, this.GetCurrentUserArea(),
                null, null, null,
                null, null, defaultHandledOrderState);
        }

        /// <summary>
        /// 获取历史订单的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">要查询的用户编号 为null时不做为查询条件</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> GetHistorOrderTours(int pageSize, int pageIndex, ref int recordCount, string companyId
            , string userId, string tourCode, string routeName
            , int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            return dal.GetHavingOrderTours(pageSize, pageIndex, ref recordCount,
                companyId, true, userId, this.GetCurrentUserArea(),
                tourCode, routeName, tourDays,
                startLeaveDate, finishLeaveDate);
        }

        /// <summary>
        /// 获取当前登录MQ用户订单提醒的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo> GetMQRemindHavingOrderTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string userId, int[] userAreas, params EyouSoft.Model.TourStructure.OrderState[] orderState)
        {
            return dal.GetMQRemindHavingOrderTours(pageSize, pageIndex, ref recordCount, companyId, userId, this.GetCurrentUserArea(userAreas), null, orderState);
        }

        /// <summary>
        /// 获取当前登录MQ用户订单提醒的历史团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户分管的区域范围</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public int GetMQRemindHistoryToursCount(string companyId, string userId, int[] userAreas)
        {
            return dal.GetMQRemindHistoryToursCount(companyId, userId, this.GetCurrentUserArea(userAreas));
        }

        /*/// <summary>
        /// 获取平台推荐产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="areaType">线路区域类型</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="siteId">分站编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <param name="companyId">公司(或经营单位)编号 为null时不做为查询条件</param>
        /// <param name="isUnit">是否是经营单位</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetRecommendTours(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.SystemStructure.AreaType areaType, int? areaId, int siteId,
            EyouSoft.Model.TourStructure.TourDisplayType displayType, string companyId, bool isUnit,
            string routeName, string companyName, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {

            IList<EyouSoft.Model.TourStructure.TourInfo> tours = dal.GetRecommendTours(pageSize, pageIndex, ref recordCount,
                areaType, areaId, siteId, displayType,
                companyId, isUnit, routeName,
                companyName, startLeaveDate, finishLeaveDate);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.CompanyID);
                }
            }

            return tours;
        }*/

        /*/// <summary>
        /// 获取高级网店团队信息集合(按模板团)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(int pageSize, int pageIndex, ref int recordCount,
            string companyId, int? cityId,
            string routeName, int? tourDays)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TourInfo> tours = dal.GetEshopTours(pageSize, pageIndex, ref recordCount,
                companyId, cityId, routeName, tourDays, null, null, true);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState);
                }
            }

            return tours;
        }*/

        /// <summary>
        /// 获取高级网店团队信息集合,当按日期搜索时返回子团信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(int pageSize, int pageIndex, ref int recordCount,
            string companyId, int? cityId,
            string routeName, int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            bool isDT = true;

            if (startLeaveDate.HasValue || finishLeaveDate.HasValue)//按时间搜索时显示子团
            {
                isDT = false;
            }

            return this.GetEshopTours(pageSize, pageIndex, ref recordCount, companyId, cityId, routeName, tourDays, startLeaveDate, finishLeaveDate, isDT, null, null);
        }

        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <param name="isDT">是否按模板团显示</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourSpreadState">团队推广状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(int pageSize, int pageIndex, ref int recordCount,
            string companyId, int? cityId,
            string routeName, int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate
            , bool isDT, int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.TourInfo> tours = dal.GetEshopTours(pageSize, pageIndex, ref recordCount,
                companyId, cityId, routeName, tourDays, startLeaveDate, finishLeaveDate, isDT, areaId, tourSpreadState);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(string companyId, int? cityId, int expression)
        {
            return this.GetEshopTours(companyId, cityId, expression, null, null);
        }

        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourSpreadState">团队推广状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(string companyId, int? cityId, int expression, int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState)
        {
            if (string.IsNullOrEmpty(companyId) || expression < 1) { return null; }

            IList<EyouSoft.Model.TourStructure.TourInfo> tours = dal.GetEshopTours(companyId, cityId, expression, areaId, tourSpreadState);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取所有子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetChildrenTours(string tourId)
        {
            return dal.GetChildrenTours(tourId);
        }

        /// <summary>
        /// 获取未出发子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetNotStartingChildrenTours(string tourId)
        {
            return dal.GetNotStartingChildrenTours(tourId);
        }

        /// <summary>
        /// 获取普通网店团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetNormalEshopTours(int pageSize, int pageIndex, ref int recordCount,
            int? cityId, int? areaId, string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType)
        {
            return dal.GetNormalEshopTours(pageSize, pageIndex, ref recordCount, cityId, areaId, companyId, displayType);
        }


        /// <summary>
        /// 按照指定条件获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="isPopularize">是否是推广的</param>
        /// <param name="searchType">搜索类型</param>
        /// <param name="searchInfo">搜索条件</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTours(int pageSize, int pageIndex, ref int recordCount, bool isPopularize
            , EyouSoft.Model.TourStructure.TourSearchType searchType, EyouSoft.Model.TourStructure.TourSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = dal.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);

            if (tours != null && tours.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourBasicInfo tmp in tours)
                {
                    tmp.TourSpreadStateName = this.GetTourSpreadStateNameHavingStyle(tmp.TourSpreadState, tmp.TourSpreadDescription);
                }
            }

            return tours;
        }

        /// <summary>
        /// 按照指定条件获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTours(int pageSize, int pageIndex, ref int recordCount, int cityId, int? areaId, bool isPopularize, bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.None;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.IsAllHistory = isHistory;

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        }

        /// <summary>
        /// 获取团队信息集合（未出发，已出发都包含）
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="TourNo">团号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourInfo> GetTours(int pageSize, int pageIndex, ref int recordCount,
           string CompanyId, string TourNo, string RouteName, int? areaId, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            return dal.GetTours(pageSize, pageIndex, ref recordCount, CompanyId, TourNo, RouteName, areaId, startLeaveDate, finishLeaveDate);
        }

        /// <summary>
        /// 按照指定条件获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null是不做为查询条件</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTours(int pageSize, int pageIndex, ref int recordCount, int cityId
            , int? areaId, string routeName, int? tourDays, string companyName, DateTime? startLeaveDate, DateTime? finishLeaveDate, bool isPopularize
            , bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.Other;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.RouteName = routeName;
            searchInfo.TourDays = tourDays;
            //searchInfo.CompanyId = companyId;
            searchInfo.CompanyName = companyName;
            searchInfo.StartLeaveDate = startLeaveDate;
            searchInfo.FinishLeaveDate = finishLeaveDate;
            searchInfo.IsAllHistory = isHistory;

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        }

        /// <summary>
        /// 按线路主题获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="themeId">线路主题编号</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByRouteTheme(int pageSize, int pageIndex, ref int recordCount, int cityId
            , int? areaId, int themeId, bool isPopularize, bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.Theme;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.ThemeId = themeId;
            searchInfo.IsAllHistory = isHistory;

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        }

        /// <summary>
        /// 按价格区间获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="minPrice">价格区间起始值 null→-∞</param>
        /// <param name="maxPrice">价格区间截止值 null→+∞</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByPriceRange(int pageSize, int pageIndex, ref int recordCount, int cityId
            , int? areaId, int? minPrice, int? maxPrice, bool isPopularize, bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.Price;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.MinPrice = minPrice;
            searchInfo.MaxPrice = maxPrice;
            searchInfo.IsAllHistory = isHistory;

            EyouSoft.SSOComponent.Entity.UserInfo tmp;
            searchInfo.IsLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out tmp);

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        }

        /// <summary>
        /// 按行程天数获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourDaysType">行程天数搜索类型 小于0:≤指定天数  等于0：=指定天数 大于0:≥指定天数</param>
        /// <param name="tourDays">行程天数</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByTourDays(int pageSize, int pageIndex, ref int recordCount, int cityId
            , int? areaId, int tourDaysType, int tourDays, bool isPopularize, bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.TourDay;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.TourDaysType = tourDaysType;
            searchInfo.TourDays = tourDays;
            searchInfo.IsAllHistory = isHistory;

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        }

        /// <summary>
        /// 按公司编号获取团队信息业务实体
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isPopularize">是否仅是推广的</param>
        /// <param name="isHistory">团队是否已出团</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByCompanyId(int pageSize, int pageIndex, ref int recordCount, int cityId, int? areaId, string companyId, bool isPopularize, bool isHistory)
        {
            EyouSoft.Model.TourStructure.TourSearchType searchType = EyouSoft.Model.TourStructure.TourSearchType.Company;
            EyouSoft.Model.TourStructure.TourSearchInfo searchInfo = new EyouSoft.Model.TourStructure.TourSearchInfo(cityId);
            searchInfo.AreaId = areaId;
            searchInfo.CompanyId = companyId;
            searchInfo.IsAllHistory = isHistory;

            return this.GetTours(pageSize, pageIndex, ref recordCount, isPopularize, searchType, searchInfo);
        } 
        #endregion

        #region 统计相关
        /*/// <summary>
        /// 获取平台下批发商有效团队数量
        /// </summary>
        /// <returns></returns>
        public int GetPlatformValidTourNumber()
        {
            return dal.GetPlatformValidTourNumber();
        }*/

        /// <summary>
        /// 获取关注批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetAttentionComingLeaveTourNumber(string companyId)
        {
            return dal.GetAttentionComingLeaveTourNumber(companyId, 7);
        }

        /// <summary>
        /// 获取关注批发商有效产品按线路区域类型统计信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.AreaTypeStatInfo GetAttentionTourByAreaTypeStats(string companyId)
        {
            return dal.GetAttentionTourByAreaTypeStats(companyId, null, null);
        }

        /// <summary>
        /// 获取关注批发商有效产品按线路区域统计信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetAttentionTourByAreaStats(string companyId, int? leaveCityId)
        {
            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = dal.GetAttentionTourByAreaStats(companyId, leaveCityId);

            if (stats != null && stats.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.AreaStatInfo tmp in stats)
                {
                    EyouSoft.Model.SystemStructure.SysArea areaInfo = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(tmp.AreaId);

                    if (areaInfo != null)
                    {
                        tmp.AreaName = areaInfo.AreaName;
                    }
                }
            }

            return stats;
        }

        /// <summary>
        /// 获取批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetComingLeaveTourNumber(string companyId)
        {
            return dal.GetComingLeaveTourNumber(companyId, this.GetCurrentUserArea(), 15);
        }

        /// <summary>
        /// 获取批发商是否有发布过团队
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool IsReleaseTour(string companyId)
        {
            return dal.IsReleaseTour(companyId, this.GetCurrentUserArea());
        }

        /// <summary>
        /// 获取批发商正在收客的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetNormalTourNumber(string companyId)
        {
            return dal.GetTourNumber(companyId, this.GetCurrentUserArea(), EyouSoft.Model.TourStructure.TourState.收客);
        }

        /// <summary>
        /// 获取团队按订单统计信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="leaveCity">出港城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourStatByOrderInfo> GetTourStatsByOrder(int pageSize, int pageIndex, ref int recordCount, string tourCode
            , int? leaveCity, string routeName, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            return dal.GetTourStatsByOrder(pageSize, pageIndex, ref recordCount,
                tourCode, leaveCity, routeName,
                startLeaveDate, finishLeaveDate);
        }

        /*/// <summary>
        /// 获取指定城市的模板团队和子团数量
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SysCity GetCityTourNumber(int cityId)
        {
            return dal.GetCityTourNumber(cityId);
        }*/

        /// <summary>
        /// 获取当前批发商用户有效产品按线路区域统计信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetCurrentUserTourByAreaStats()
        {
            string companyId = this.GetCurrentUserCompanyId();

            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = dal.GetTourByAreaStats(companyId, this.GetCurrentUserArea());

            if (stats != null && stats.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.AreaStatInfo tmp in stats)
                {
                    EyouSoft.Model.SystemStructure.SysArea areaInfo = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(tmp.AreaId);

                    if (areaInfo != null)
                    {
                        tmp.AreaName = areaInfo.AreaName;
                    }
                }
            }

            return stats;
        }

        /// <summary>
        /// 获取当前批发商用户一周内快到期产品数量
        /// </summary>
        /// <returns></returns>
        public int GetComingExpireTourNumber()
        {
            string companyId = this.GetCurrentUserCompanyId();

            if (string.IsNullOrEmpty(companyId)) { return 0; }

            return dal.GetComingExpireTourNumber(companyId, DateTime.Today.AddDays(7), this.GetCurrentUserArea());
        }

        /// <summary>
        /// 获取MQ订单提醒的团队信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourId">团队编号</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo GetMQRemindTourInfo(string companyId, string tourId, params EyouSoft.Model.TourStructure.OrderState[] orderState)
        {
            return (string.IsNullOrEmpty(tourId) || string.IsNullOrEmpty(companyId)) ? null : dal.GetMQRemindTourInfo(companyId, tourId, orderState);
        }

        /// <summary>
        /// 获取批发商有效团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <returns></returns>
        public int GetTourNumber(string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType)
        {
            return string.IsNullOrEmpty(companyId) ? 0 : dal.GetTourNumber(companyId, displayType);
        }
        #endregion

        #region 团队访问记录
        /// <summary>
        /// 添加团队访问记录,并更新团队的浏览次数(浏览次数++)
        /// </summary>
        /// <param name="visitInfo">团队浏览记录信息业务实体</param>
        /// <returns></returns>
        public bool InsertTourVisitedInfo(EyouSoft.Model.TourStructure.TourVisitInfo visitInfo)
        {
            visitInfo.Id = Guid.NewGuid().ToString();
            return dal.InsertTourVisitedInfo(visitInfo);
        }

        /// <summary>
        /// 按当前登录的批发商用户(用户分管的线路区域)统计团队被访问的次数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetTourVisitedNumberByUser(string companyId)
        {
            return dal.GetTourVisitedNumberByUser(companyId, this.GetCurrentUserArea());
        }

        /// <summary>
        /// 按团队分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorys(int pageSize, int pageIndex, ref int recordCount, string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) { return null; }

            return dal.GetTourVisitedHistorys(pageSize, pageIndex, ref recordCount, tourId);
        }

        /// <summary>
        /// 按当前登录的批发商用户(用户分管的线路区域)、指定返回行数的数值表达式获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="compayId">公司编号</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(string compayId, int expression)
        {
            return dal.GetTourVisitedHistorysByUser(compayId, this.GetCurrentUserArea(), expression);
        }

        /// <summary>
        /// 按当前登录的批发商用户(用户分管的线路区域)分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            return dal.GetTourVisitedHistorysByUser(pageSize, pageIndex, ref recordCount, companyId, this.GetCurrentUserArea());
        }

        /// <summary>
        /// 按批发商分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="visitorCompanyName">访问者公司名称 为null时不做为查询条件</param>
        /// <param name="visitedRouteName">访问的线路名称 为null时不做为查询条件</param>
        /// <param name="startVisitedTime">访问开始时间 为null时不做为查询条件</param>
        /// <param name="finishVisitedTime">访问截止时间 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVistedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount,
            string companyId, string visitorCompanyName, string visitedRouteName
            , DateTime? startVisitedTime, DateTime? finishVisitedTime, int? areaId)
        {
            if (finishVisitedTime.HasValue)
            {
                finishVisitedTime = finishVisitedTime.Value.AddDays(1);
            }

            return dal.GetTourVistedHistorysByCompany(pageSize, pageIndex, ref recordCount,
                companyId, visitorCompanyName, visitedRouteName
                , startVisitedTime, finishVisitedTime, areaId);
        }

        /// <summary>
        /// 获取用户访问批发商团队历史记录信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        /// <remarks>
        /// 按访问团队的用户、指定返回行数的数值表达式获取访问团队的历史记录
        /// </remarks>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(string userId, int expression)
        {
            return dal.GetVisitedHistorysByUser(userId, expression);
        }

        /// <summary>
        /// 获取用户访问批发商团队历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        /// <remarks>
        /// 按访问团队的用户分页获取访问团队的历史记录
        /// </remarks>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string userId)
        {
            return dal.GetVisitedHistorysByUser(pageSize, pageIndex, ref recordCount, null, userId);
        }

        /// <summary>
        /// 按访问团队的公司分页获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="visitedCompanyName">被访问的批发商名称 为null时不做为查询条件</param>
        /// <param name="startVisitedTime">访问开始时间 为null时不做为查询条件</param>
        /// <param name="finishVisitedTime">访问截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount, string companyId,
            string visitedCompanyName, DateTime? startVisitedTime, DateTime? finishVisitedTime)
        {
            if (finishVisitedTime.HasValue)
            {
                finishVisitedTime = finishVisitedTime.Value.AddDays(1);
            }

            return dal.GetVisitedHistorysByCompany(pageSize, pageIndex, ref recordCount, companyId,
                visitedCompanyName, startVisitedTime, finishVisitedTime);
        }

        /// <summary>
        /// 按公司统计访问批发商的数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetVisitedNumberByCompany(string companyId)
        {
            return dal.GetVisitedNumberByCompany(companyId);
        }

        /// <summary>
        /// 按公司分页获取访问批发商的统计信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.VisitedHistoryStatInfo> GetVisitedHistoryStats(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            return dal.GetVisitedHistoryStats(pageSize, pageIndex, ref recordCount, companyId);
        }

        /// <summary>
        /// 增加团队浏览数，增量为1
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool IncreaseClicks(string tourId)
        {
            return dal.SetClicks(tourId, 1);
        }
        #endregion

        #region 新版资讯页面相关
        /// <summary>
        /// 根据线路区域获取相关的团队列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="AreaId">线路区域编号 =0时不做条件</param>
        /// <param name="LeaveCityId">出港城市编号 =0时不做条件</param>
        /// <returns></returns>
        public  IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByAreaId(int pageSize, int pageIndex, ref int recordCount, int AreaId,int LeaveCityId)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> list = dal.GetToursByAreaId(pageSize, pageIndex, ref recordCount, AreaId, LeaveCityId);
            if (list != null && list.Count > 0)
            {
                SystemStructure.SysCity cityBll = new EyouSoft.BLL.SystemStructure.SysCity();
                foreach (var item in list)
                {
                    if (item.LeaveCityId > 0)
                    {
                        EyouSoft.Model.SystemStructure.CityBase CityInfo = cityBll.GetSysCityModel(item.LeaveCityId);
                        if(CityInfo!=null)
                            item.LeaveCityName = CityInfo.CityName;
                        CityInfo = null;
                    }
                } 
                cityBll = null;
            }
            return list;
        }
        /// <summary>
        /// 获取全国范围下指定条数的最新团队信息[模版团]
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTopNumNewTours(int TopNum)
        {
            int recordCount = 0;
            return this.GetToursByAreaId(TopNum, 1,ref recordCount,0,0);
        }
        /// <summary>
        /// 获取指定城市指定条数的最新团队信息[模版团]
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="CityId">城市编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTopNumNewTours(int TopNum,int CityId)
        {
            int recordCount = 0;
            return this.GetToursByAreaId(TopNum, 1, ref recordCount, 0,CityId);
        }
        #endregion
    }
}
