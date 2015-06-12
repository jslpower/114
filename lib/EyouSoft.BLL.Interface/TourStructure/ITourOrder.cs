using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;


namespace EyouSoft.IBLL.TourStructure
{
    /// <summary>
    /// 订单信息 业务接口
    /// </summary>
    /// 周文超 2010-05-25
    public interface ITourOrder
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="OrderModel">订单业务实体(游客信息实体集合为null或者count小于等于0不新增游客信息))</param>
        /// <returns>-1:写入订单信息失败;0:订单实体为空;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_ADD,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_ADD_CODE)]
        int AddTourOrder(EyouSoft.Model.TourStructure.TourOrder OrderModel);

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="OrderModel">订单业务实体(游客信息实体集合为null或者count小于等于0不修改游客信息))</param>
        /// <returns>-1:写入订单信息失败;0:订单实体为空;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_EDIT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_EDIT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_EDIT_CODE)]
        int UpdateTourOrder(EyouSoft.Model.TourStructure.TourOrder OrderModel);

        /// <summary>
        /// 删除订单信息(假删除)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回操作是否成功，true操作成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_DELETE,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""OrderId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_DELETE_CODE)]
        bool UpdateIsDelete(string OrderId);

        ///// <summary>
        ///// 删除订单信息(物理删除)
        ///// </summary>
        ///// <param name="OrderId">订单ID</param>
        ///// <returns>返回是否删除成功，true删除成功</returns>
        //bool DeleteRealityTourOrder(string OrderId);

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="OrderState">订单状态</param>
        /// <param name="SaveSeatDate">留位过期时间（改为已留位状态时必须传值）</param>
        /// <returns>返回操作是否成功，true操作成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_SetOrderState_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_SetOrderState,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":1,""Attribute"":""OrderState"",""AttributeType"":""val""},{""Index"":0,""Attribute"":""OrderId"",""AttributeType"":""val""}]", 
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_ORDER_SetOrderState_CODE)]
        bool SetTourOrderState(string OrderId, Model.TourStructure.OrderState OrderState, DateTime? SaveSeatDate);

        /// <summary>
        ///  根据订单ID获取订单实体(同时获取游客信息)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回订单实体</returns>
        EyouSoft.Model.TourStructure.TourOrder GetOrderModel(string OrderId);

        /// <summary>
        /// 根据团队ID获取未处理订单(专线未处理订单使用)
        /// </summary>
        /// <param name="SellCompanyId">批发商公司ID(必须传值)</param>
        /// <param name="UserId">用户ID</param>
        /// <param name="TourId">团队ID</param>
        /// <param name="TourNo">团号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="LeaveDateStart">出团开始时间</param>
        /// <param name="LeaveDateEnd">出团结束时间</param>
        /// <param name="OrderState">订单状态</param>
        /// <returns>返回订单实体集合</returns>
        IList<Model.TourStructure.TourOrder> GetUndisposedOrder(string SellCompanyId, string UserId, string TourId
            , string TourNo, string RouteName, DateTime? LeaveDateStart, DateTime? LeaveDateEnd
            , params Model.TourStructure.OrderState[] OrderState);

        /// <summary>
        /// 根据团队ID获取已处理订单(专线已处理订单使用)
        /// </summary>
        /// <param name="SellCompanyId">批发商公司ID(必须传值)</param>
        /// <param name="UserId">用户ID</param>
        /// <param name="TourId">团队ID</param>
        /// <param name="TourNo">团号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="LeaveDateStart">出团开始时间</param>
        /// <param name="LeaveDateEnd">出团结束时间</param>
        /// <param name="OrderState">订单状态</param>
        /// <returns>返回订单实体集合</returns>
        IList<Model.TourStructure.TourOrder> GetHandledOrder(string SellCompanyId, string UserId, string TourId
            , string TourNo, string RouteName, DateTime? LeaveDateStart, DateTime? LeaveDateEnd
            , params Model.TourStructure.OrderState[] OrderState);

        /// <summary>
        /// 根据团队ID获取订单(专线历史订单使用)
        /// </summary>
        /// <param name="SellCompanyId">批发商公司ID(必须传值)</param>
        /// <param name="UserId">用户ID</param>
        /// <param name="TourId">团队ID</param>
        /// <param name="TourNo">团号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="LeaveDateStart">出团开始时间</param>
        /// <param name="LeaveDateEnd">出团结束时间</param>
        /// <param name="OrderState">订单状态数组</param>
        /// <returns>返回订单实体集合</returns>
        IList<Model.TourStructure.TourOrder> GetOrderList(string SellCompanyId, string UserId, string TourId, string TourNo, string RouteName
            , DateTime? LeaveDateStart, DateTime? LeaveDateEnd, params Model.TourStructure.OrderState[] OrderState);

        /// <summary>
        /// 已成交客户统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:买家公司升/降序;2/3:预定成功次数升/降序;4/5:预定人数升/降序;6/7:金额升/降序;8/9:留位过期次数升/降序;10/11:不受理次数升/降序;)</param>
        /// <param name="SellCompanyId">卖家公司ID(必须传值)</param>
        /// <param name="UserId">用户ID(为null不作条件)</param>
        /// <param name="BuyCompanyName">买家公司名称(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string SellCompanyId, string UserId, string BuyCompanyName, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取历史订单及其人数
        /// </summary>
        /// <param name="CompanyId">批发商ID(为null不作条件)</param>
        /// <param name="AreaIds">线路区域ID集合(为null不作条件)</param>
        /// <param name="EndTime">日期截止时间(出团日期小于等于此时间的为历史订单，为null取当前时间)</param>
        /// <returns>索引0：历史订单数；索引1：历史订单成人数之和；索引2： 历史订单儿童数之和；</returns>
        IList<int> GetHistoryOrderNum(string CompanyId, int[] AreaIds, DateTime? EndTime);

        /// <summary>
        /// 获取某零售商在当前公司所交易的所有订单
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="SellCompanyId">当前公司ID(批发商、卖家)(必须传值)</param>
        /// <param name="BuyCompanyId">零售商ID(买家)(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <returns>返回订单实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string SellCompanyId, string BuyCompanyId, Model.TourStructure.OrderState? OrderState);

        /// <summary>
        /// 获取当前用户的未处理的订单数
        /// </summary>
        /// <param name="AreaIds">用户线路区域ID集合(为null不作条件)</param>
        /// <param name="CompanyId">用户所在公司ID(必须传值)</param>
        /// <returns>订单数</returns>
        int GetUntreatedOrderNum(int[] AreaIds, string CompanyId);

        /// <summary>
        /// 获取Top零售商预定
        /// </summary>
        /// <param name="TopNum">Top数量</param>
        /// <param name="BuyCompanyName">零售商名称(为null或者空不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="OrderIndex">排序索引(0/1:预定次数升/降序;2/3:预定人数升/降序;)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int TopNum, string BuyCompanyName
            , int[] ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime, Model.TourStructure.OrderState? OrderState, int OrderIndex);

        /// <summary>
        /// 零售商预定情况统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:买家公司升/降序;2/3:预定成功次数升/降序;4/5:预定人数升/降序;)</param>
        /// <param name="BuyCompanyName">零售商公司名称(为null或者空不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string BuyCompanyName, int[] ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime
            , Model.TourStructure.OrderState? OrderState);

        /// <summary>
        /// 获取某零售商在某批发商公司下的所有订单信息(不含游客信息)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="BuyCompanyId">买家公司ID(零售商,必须传值)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <returns>返回订单实体</returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string BuyCompanyId, string RouteName, DateTime? StartTime, DateTime? EndTime, Model.TourStructure.OrderState? OrderState);

        /// <summary>
        /// 零售商登录情况统计
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="CompanyName">零售商公司名称</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="OrderIndex">排序索引(0/1:登录次数升/降序;2/3:访问数量升/降序;)</param>
        /// <returns>返回公司登录统计实体集合</returns>
        IList<Model.TourStructure.RetailLoginStatistics> GetRetailLoginStatistics(int TopNum, string CompanyName
            , int[] ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime, int OrderIndex);

        /// <summary>
        /// 零售商登录情况统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录次数升/降序;2/3:访问数量升/降序;)</param>
        /// <param name="CompanyName">零售商公司名称</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns>返回公司登录统计实体集合</returns>
        IList<Model.TourStructure.RetailLoginStatistics> GetRetailLoginStatistics(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string CompanyName, int[] ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 返回公司登录明细(登录时间、登录IP数组)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录时间升/降序;)</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="StartTime">登录开始时间</param>
        /// <param name="EndTime">登录结束时间</param>
        /// <param name="LoginTime">登录时间数组</param>
        /// <param name="LoginIP">登录IP数组</param>
        void GetRetailLoginList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string CompanyId, DateTime? StartTime
            , DateTime? EndTime, out DateTime?[] LoginTime, out string[] LoginIP);

        /// <summary>
        /// 根据订单状态获取订单数
        /// </summary>
        /// <param name="StartTime">下单开始时间(订单IssueTime为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单IssueTime为null不作条件)</param>
        /// <returns>返回订单数集合(索引对应订单状态枚举)</returns>
        IList<int> GetOrderNumByState(DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 根据订单状态获取订单数
        /// </summary>
        /// <param name="AreaIds">区域集合(为null不作条件)</param>
        /// <param name="CompanyId">公司ID(专线，为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单IssueTime为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单IssueTime为null不作条件)</param>
        /// <returns>返回订单数集合(索引对应订单状态枚举)</returns>
        IList<int> GetOrderNumByState(int[] AreaIds, string CompanyId, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 根据订单状态获取订单数
        /// </summary>
        /// <param name="AreaIds">区域集合(为null不作条件，逗号分割模式用户直接in)</param>
        /// <param name="CompanyId">公司ID(专线，为null不作条件)</param>
        /// <param name="UserId">当前用户ID</param>
        /// <param name="StartTime">下单开始时间(订单IssueTime为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单IssueTime为null不作条件)</param>
        /// <returns>返回订单数集合(索引对应订单状态枚举)</returns>
        IList<int> GetOrderNumByState(string AreaIds, string CompanyId, string UserId, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取批发商行为分析列表
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录时间升/降序;2/3:成交订单升/降序;4/5:留位订单升/降序;6/7:留位过期订单升/降序;8/9:不受理订单升/降序;10/11:登录次数升/降序;12/13:被查看次数升/降序;)</param>
        /// <param name="SellCompanyName">批发商名称(为null不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="SellCityId">销售城市ID(小于等于0不作条件，且售城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="AreaId">区域ID(小于等于0不作条件，且售城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <returns>返回批发商行为分析实体集合</returns>
        IList<Model.TourStructure.WholesalersStatistics> GetWholesalersStatistics(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, string SellCompanyName, int[] ManagerUserCityIds, int SellCityId, int AreaId
            , DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取某一批发商下所有的订单信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="SellCompanyId">批发商ID(卖家，必须传值)</param>
        /// <param name="BuyCompanyName">零售商名称(买家，为null不作条件)</param>
        /// <param name="TourNo">团号(为null不作条件)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="CityId">城市ID(小于等于0不作条件，且城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="AreaId">区域ID(小于等于0不作条件，且城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <returns>返回订单实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string SellCompanyId, string BuyCompanyName, string TourNo, string RouteName, int CityId, int AreaId
            , Model.TourStructure.OrderState? OrderState, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取批发商产品(所有的没有被删除且审核通过的)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:成交订单升/降序;2/3:留位订单升/降序;4/5:留位过期订单升/降序;6/7:不受理订单升/降序;8/9:被查看次数升/降序;10/11:产品创建时间升/降序)</param>
        /// <param name="TourDisplayType">团队展示类型(必须传值)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="StartTime">创建开始时间(为null不作条件)</param>
        /// <param name="EndTime">创建结束时间(为null不作条件)</param>
        /// <returns></returns>
        IList<Model.TourStructure.TourStatByOrderInfo> GetTourStatByOrderInfo(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, EyouSoft.Model.TourStructure.TourDisplayType TourDisplayType, string SellCompanyId, int AreaId, string RouteName
            , DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取某个组团社的订单信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="BuyCompanyId">组团社ID(必须传值)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="LeaveDateStart">出团开始日期(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团结束日期(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="StartTime">开始出团时间(为null不作条件)</param>
        /// <param name="EndTime">结束出团时间(为null不作条件)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string SellCompanyId, string BuyCompanyId, string RouteName, int AreaId, DateTime? LeaveDateStart, DateTime? LeaveDateEnd
            , Model.TourStructure.OrderState? OrderState, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取某个组团社的订单信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="BuyCompanyId">组团社ID(必须传值)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="LeaveDateStart">出团开始日期(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团结束日期(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <param name="OrderSource">订单来源(0 组团下单;1:专线代客预订)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetRetailersOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string SellCompanyId, string BuyCompanyId, string RouteName, int AreaId, DateTime? LeaveDateStart, DateTime? LeaveDateEnd
            , Model.TourStructure.OrderState? OrderState, DateTime? StartTime, DateTime? EndTime, int OrderSource);

        /// <summary>
        /// 某个组团社的订单数统计
        /// </summary>
        /// <param name="BuyCompanyId">组团社ID(必须传值)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="RouteName">线路区域(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="LeaveDateStart">出团开始时间(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团介绍时间(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单介绍时间(为null不作条件)</param>
        /// <returns>组团订单统计实体集合</returns>
        IList<Model.TourStructure.AreaAndOrderNum> GetAreaAndOrderNum(string BuyCompanyId, string SellCompanyId
            , string RouteName, int AreaId, DateTime? LeaveDateStart, DateTime? LeaveDateEnd, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 根据公司ID统计订单数
        /// </summary>
        /// <param name="CurrentCompanyId">当前公司ID(必须传值)</param>
        /// <param name="CustomerCompanyId">客户公司ID(必须传值)</param>
        /// <param name="CompanyType">当前公司类型(组团统计买家，专线统计卖家，为null统计所有)</param>
        /// <param name="OrderState">订单状态(为null统计所有)</param>
        /// <returns>订单数</returns>
        int GetRetailersOrderNumByState(string CurrentCompanyId, string CustomerCompanyId, Model.CompanyStructure.CompanyType? CompanyType, Model.TourStructure.OrderState? OrderState);

        /// <summary>
        /// 获取所有订单信息(不含游客信息)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="CurrentCompanyId">当前公司ID(必须传值)</param>
        /// <param name="CustomerCompanyId">客户公司ID(必须传值)</param>
        /// <param name="CompanyType">当前公司类型(组团统计买家，专线统计卖家，为null统计所有)</param>
        /// <param name="OrderState">订单状态(为null统计所有)</param>
        /// <returns>订单信息集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrder> GetRetailersOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string CurrentCompanyId, string CustomerCompanyId, Model.CompanyStructure.CompanyType? CompanyType
            , Model.TourStructure.OrderState? OrderState);

        /// <summary>
        /// 统计整个下过单的组团社数量
        /// </summary>
        /// <param name="SellCompanyId">批发商ID（为null统计整个平台下）</param>
        /// <returns>组团社数量</returns>
        int GetRetailersNumByOrder(string SellCompanyId);

        /// <summary>
        /// 统计每月每个订单状态的订单数量的实体(flash图表统计用)
        /// </summary>
        /// <param name="StartTime">开始时间(必须传值)</param>
        /// <param name="EndTime">结束时间(必须传值)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth> GetTourOrderStatisticsByMonth(DateTime StartTime
            , DateTime EndTime);
    }
}
