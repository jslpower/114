using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 团队信息 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ITour
    {
        #region 团队基本操作
        /*/// <summary>
        /// 新增团队信息(模板团标准发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int AddStandardTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model);*/

        /*/// <summary>
        /// 修改团队信息(模板团标准发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <param name="insertTours">要添加的子团信息集合</param>
        /// <param name="updateTours">要更新的子团信息集合</param>
        /// <param name="deleteTours">要删除的子团信息集合</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int UpdateStandardTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> insertTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> updateTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> deleteTours);*/

        /*/// <summary>
        /// 新增团队信息(模板团快速发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int AddQuickTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model);*/

        /*/// <summary>
        /// 修改团队信息(模板团快速发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <param name="insertTours">要添加的子团信息集合</param>
        /// <param name="updateTours">要更新的子团信息集合</param>
        /// <param name="deleteTours">要删除的子团信息集合</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int UpdateQuickTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> insertTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> updateTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> deleteTours);*/

        /// <summary>
        /// 新增模板团信息
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int InsertTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model);

        /// <summary>
        /// 修改模板团信息
        /// </summary>
        /// <param name="tourInfo">团队信息实体</param>
        /// <returns></returns>
        int UpdateTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo tourInfo);

        /// <summary>
        /// 追加模板团发团计划信息
        /// </summary>
        /// <param name="tourInfo">团队信息实体</param>
        /// <returns></returns>
        int AppendTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo tourInfo);

        /// <summary>
        /// 修改团队信息(单个子团)
        /// </summary>
        /// <param name="model">团队信息业务实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        int UpdateTourInfo(EyouSoft.Model.TourStructure.TourInfo model);

        /// <summary>
        /// 删除团队(虚拟删除)
        /// </summary>
        /// <param name="companyId">团队所在公司编号</param>
        /// <param name="tourId">团队ID</param>
        /// <param name="isDelete">删除状态</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool DeleteByVirtual(string companyId, string tourId, bool isDelete);

        /// <summary>
        /// 删除团队信息(物理删除)
        /// </summary>
        /// <param name="companyId">团队所在公司编号</param>
        /// <param name="tourId">团队ID</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool DeleteByActual(string companyId, string tourId);

        /// <summary>
        /// 获取标准发布计划行程信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourStandardPlan> GetTourInfoStandardPlans(string tourId);

        /// <summary>
        /// 获取快速发布计划行程信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        string GetTourInfoQuickPlan(string tourId);

        /// <summary>
        /// 获取标准发布计划服务标准信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.TourServiceStandard GetTourServiceStandard(string tourId);

        /// <summary>
        /// 获取标准发布计划地接社信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        IList<EyouSoft.Model.TourStructure.TourLocalityInfo> GetTourLocalAgencys(string tourId);

        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>返回团队信息业务实体</returns>
        EyouSoft.Model.TourStructure.TourInfo GetTourInfo(string tourId);

        /// <summary>
        /// 审核团队
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool SetChecked(string tourId, bool isChecked);

        /// <summary>
        /// 设置团队收客状态
        /// </summary>
        /// <param name="tourState">团队状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool SetTourState(EyouSoft.Model.TourStructure.TourState tourState, params string[] tourId);

        /// <summary>
        /// 设置团队推广状态
        /// </summary>        
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool SetTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, params string[] tourId);

        /// <summary>
        /// 设置模板团下所有子团推广状态
        /// </summary>
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        bool SetTemplateTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, string tourId);

        /// <summary>
        /// 获取团队报价信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">发布团队的公司编号</param>
        /// <param name="parentTourId">模板团编号</param>
        /// <returns>报价信息业务实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourPriceDetail> GetTourPriceDetail(string tourId, out string companyId, out string parentTourId);

        /// <summary>
        /// 设置团队剩余人数 当设置的剩余人数大于团队计划人数时将会把剩余人数设置成团队的计划人数
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="remnantNumber">剩余人数</param>
        /// <returns>System.Boolean true:success false:error</returns>
        bool SetTourRemnantNumber(string tourId, int remnantNumber);

        /// <summary>
        /// 批量生成团号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="leaveDate">出团日期</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AutoTourCodeInfo> CreateAutoTourCodes(string companyId, int areaId, params DateTime[] leaveDate);

        /*/// <summary>
        /// 获取团队第一个报价等级的报价信息
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.TourPriceDetail GetFirstTourPriceDetail(string tourId);*/

        /// <summary>
        /// 判断团队是否已删除
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        bool IsDeleted(string tourId);
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
        IList<EyouSoft.Model.TourStructure.TemplateTourInfo> GetTemplateTours(string companyId, string userId, string userAreas, int? leaveCityId);

        /// <summary>
        /// 获取未出发团队信息集合(模板团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount, string companyId, int? areaId);

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
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount
            , string templateTourId, string tourCode, EyouSoft.Model.TourStructure.SearchTourState searchTourState
            , DateTime? startLeaveDate, DateTime? finishLeaveDate);

        /// <summary>
        /// 获取已出发团队信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetStartingTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string userAreas, string tourCode, string routeName
            , int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate);

        /// <summary>
        /// 获取快到期产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="expireTime">到期时间</param>
        /// <param name="userAreas">当前用户负责的区域(城市)编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetComingExpireTours(int pageSize, int pageIndex, ref int recordCount, DateTime expireTime
            , string userAreas, int? areaId, int? cityId, string companyName, string routeName);

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
        IList<EyouSoft.Model.TourStructure.TourInfo> GetAttentionTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId
            , int? leaveCityId, int? areaId, string routeName
            , string attentionCompanyId, DateTime? startLeaveDate, DateTime? finishLeaveDate
            , EyouSoft.Model.SystemStructure.AreaType? areaType);

        /// <summary>
        /// 获取有订单的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isHistory">是否是历史团队</param>
        /// <param name="userId">要查询的用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="tourCode">团队编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> GetHavingOrderTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, bool isHistory, string userId, string userAreas
            , string tourCode, string routeName, int? tourDays
            , DateTime? startLeaveDate, DateTime? finishLeaveDate, params EyouSoft.Model.TourStructure.OrderState[] orderState);

        /// <summary>
        /// 获取MQ订单提醒的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="tourId">团队编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo> GetMQRemindHavingOrderTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string userId, string userAreas, string tourId, params EyouSoft.Model.TourStructure.OrderState[] orderState);

        /// <summary>
        /// 获取MQ订单提醒的历史团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        int GetMQRemindHistoryToursCount(string companyId, string userId, string userAreas);

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
        IList<EyouSoft.Model.TourStructure.TourInfo> GetRecommendTours(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.SystemStructure.AreaType areaType, int? areaId, int siteId,
            EyouSoft.Model.TourStructure.TourDisplayType displayType, string companyId, bool isUnit,
            string routeName, string companyName, DateTime? startLeaveDate, DateTime? finishLeaveDate);*/

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
        IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(int pageSize, int pageIndex, ref int recordCount,
            string companyId, int? cityId,
            string routeName, int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate, bool isDT
            , int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState);

        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourSpreadState">团队推广状态 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(string companyId, int? cityId, int expression, int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState);

        /// <summary>
        /// 获取所有子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetChildrenTours(string tourId);

        /// <summary>
        /// 获取未出发子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetNotStartingChildrenTours(string tourId);

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
        IList<EyouSoft.Model.TourStructure.TourInfo> GetNormalEshopTours(int pageSize, int pageIndex, ref int recordCount,
            int? cityId, int? areaId, string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType);

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
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTours(int pageSize, int pageIndex, ref int recordCount, bool isPopularize
            , EyouSoft.Model.TourStructure.TourSearchType searchType, EyouSoft.Model.TourStructure.TourSearchInfo searchInfo);
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
         IList<EyouSoft.Model.TourStructure.TourInfo> GetTours(int pageSize, int pageIndex, ref int recordCount,
            string CompanyId, string TourNo, string RouteName, int? areaId, DateTime? startLeaveDate, DateTime? finishLeaveDate);
        #endregion

        #region 统计相关
        /// <summary>
        /// 获取平台下批发商有效团队数量
        /// </summary>
        /// <returns></returns>
        int GetPlatformValidTourNumber();

        /// <summary>
        /// 获取关注批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="days">即将出团所定义的天数</param>
        /// <returns></returns>
        int GetAttentionComingLeaveTourNumber(string companyId, int days);

        /// <summary>
        /// 获取关注批发商有效产品按线路区域类型统计信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.AreaTypeStatInfo GetAttentionTourByAreaTypeStats(string companyId, string userId, int? leaveCityId);

        /// <summary>
        /// 获取关注批发商有效产品按线路区域统计信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetAttentionTourByAreaStats(string companyId, int? leaveCityId);

        /// <summary>
        /// 获取批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="days">即将出团所定义的天数</param>
        /// <returns></returns>
        int GetComingLeaveTourNumber(string companyId, string userAreas, int days);

        /// <summary>
        /// 获取批发商是否有发布过团队
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        bool IsReleaseTour(string companyId, string userAreas);

        /// <summary>
        /// 获取批发商有效团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="tourState">团队状态</param>
        /// <returns></returns>
        int GetTourNumber(string companyId, string userAreas, EyouSoft.Model.TourStructure.TourState? tourState);

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
        IList<EyouSoft.Model.TourStructure.TourStatByOrderInfo> GetTourStatsByOrder(int pageSize, int pageIndex, ref int recordCount, string tourCode
            , int? leaveCity, string routeName, DateTime? startLeaveDate, DateTime? finishLeaveDate);

        /// <summary>
        /// 获取指定城市的模板团队和子团数量
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysCity GetCityTourNumber(int cityId);

        /// <summary>
        /// 获取批发商有效产品按线路区域统计信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetTourByAreaStats(string companyId, string userAreas);

        /// <summary>
        /// 获取批发商快到期产品数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="expireTime">到期时间</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        int GetComingExpireTourNumber(string companyId, DateTime expireTime, string userAreas);

        /// <summary>
        /// 获取MQ订单提醒的团队信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourId">团队编号</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo GetMQRemindTourInfo(string companyId, string tourId, params EyouSoft.Model.TourStructure.OrderState[] orderState);

        /// <summary>
        /// 获取批发商有效团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <returns></returns>
        int GetTourNumber(string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType);
        #endregion

        #region 团队访问记录
        /// <summary>
        /// 添加团队访问记录,并更新团队的浏览次数(浏览次数++)
        /// </summary>
        /// <param name="visitInfo">团队浏览记录信息业务实体</param>
        /// <returns></returns>
        bool InsertTourVisitedInfo(EyouSoft.Model.TourStructure.TourVisitInfo visitInfo);

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)统计团队被访问的次数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        int GetTourVisitedNumberByUser(string companyId, string userAreas);

        /// <summary>
        /// 按团队分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorys(int pageSize, int pageIndex, ref int recordCount, string tourId);

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)、指定返回行数的数值表达式获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="compayId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(string compayId, string userAreas, int expression);

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string companyId, string userAreas);

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
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVistedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount,
            string companyId, string visitorCompanyName, string visitedRouteName
            , DateTime? startVisitedTime, DateTime? finishVisitedTime, int? areaId);

        /// <summary>
        /// 按访问团队的用户、指定返回行数的数值表达式获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(string userId, int expression);

        /// <summary>
        /// 按访问团队的用户分页获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号 为空时不做为查询条件</param>
        /// <param name="userId">用户编号 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId);

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
        IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount, string companyId,
            string visitedCompanyName, DateTime? startVisitedTime, DateTime? finishVisitedTime);

        /// <summary>
        /// 按公司统计访问批发商的数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetVisitedNumberByCompany(string companyId);

        /// <summary>
        /// 按公司分页获取访问批发商的统计信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.VisitedHistoryStatInfo> GetVisitedHistoryStats(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 设置团队浏览数，按expression指定的数值增加或减少
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="expression">增量数值表达式</param>
        /// <returns></returns>
        bool SetClicks(string tourId,int expression);
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
        IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByAreaId(int pageSize, int pageIndex, ref int recordCount, int AreaId,int LeaveCityId);
        #endregion
    }
}
