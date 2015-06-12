using System;
using System.Collections.Generic;
using System.Text;

/*
 * 描述：日志记录类
 * 开发人：蒋胜蓝   开发时间：2010-05-25
 */

#region 日志记录业务层

namespace EyouSoft.BusinessLogWriter
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public interface IBusinessLog
    {
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model);
    }

    /// <summary>
    /// 日志记录基类
    /// </summary>
    public abstract class BusinessLog : IBusinessLog
    {
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public virtual void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        { ;}
    }

    #region 成员类

    /// <summary>
    /// 团队日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从10000-19999
    /// 当前最大号:10007
    /// </remarks>
    public class TourLog : BusinessLog
    {
        #region IBusinessLog 成员
        public const string LOG_TOUR_ADD = "[人员]在[时间]添加团队：{0}，编号为{1}";
        public const string LOG_TOUR_ADD_TITLE = "添加团队";
        public const int LOG_TOUR_ADD_CODE = 10001;

        public const string LOG_TOUR_EDIT = "[人员]在[时间]修改团队：{0}，编号为{1}";
        public const string LOG_TOUR_EDIT_TITLE = "修改团队";
        public const int LOG_TOUR_EDIT_CODE = 10002;

        public const string LOG_TOUR_DELETE = "[人员]在[时间]删除团队，编号为{0}";
        public const string LOG_TOUR_DELETE_TITLE = "删除团队";
        public const int LOG_TOUR_DELETE_CODE = 10003;

        public const string LOG_TOUR_APPEND = "[人员]在[时间]追加发团计划，编号为{0}";
        public const string LOG_TOUR_APPEND_TITLE = "追加发团计划";
        public const int LOG_TOUR_APPEND_CODE = 10004;

        public const string LOG_TOUR_SetTourState = "[人员]在[时间]设置团队收客状态，状态为{0}，编号为{1}";
        public const string LOG_TOUR_SetTourState_TITLE = "设置团队收客状态";
        public const int LOG_TOUR_SetTourState_CODE = 10005;

        public const string LOG_TOUR_SetTourSpreadState = "[人员]在[时间]设置团队推广状态，状态为{0}，编号为{1}";
        public const string LOG_TOUR_SetTourSpreadState_TITLE = "设置团队推广状态";
        public const int LOG_TOUR_SetTourSpreadState_CODE = 10006;

        public const string LOG_TOUR_SetTourRemnantNumber = "[人员]在[时间]设置团队虚拟剩余人数，编号为{0}";
        public const string LOG_TOUR_SetTourRemnantNumber_TITLE = "设置团队虚拟剩余人数";
        public const int LOG_TOUR_SetTourRemnantNumber_CODE = 10007;


        IDAL.ITourLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.ITourLog>();
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }

    /// <summary>
    /// 线路日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从20000-29999
    /// 当前最大号:20003
    /// </remarks>
    public class RouteLog : BusinessLog
    {
        #region IBusinessLog 成员
        public const string LOG_ROUTE_ADD = "[人员]在[时间]添加线路：{0}，编号为{1}";
        public const string LOG_ROUTE_ADD_TITLE = "添加线路";
        public const int LOG_ROUTE_ADD_CODE = 20001;

        public const string LOG_ROUTE_EDIT = "[人员]在[时间]修改线路：{0}，编号为{1}";
        public const string LOG_ROUTE_EDIT_TITLE = "修改线路";
        public const int LOG_ROUTE_EDIT_CODE = 20002;

        public const string LOG_ROUTE_DELETE = "[人员]在[时间]删除线路，编号为{0}";
        public const string LOG_ROUTE_DELETE_TITLE = "删除线路";
        public const int LOG_ROUTE_DELETE_CODE = 20003;
        IDAL.IRouteLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.IRouteLog>();
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }
    /// <summary>
    /// 订单日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从30000-39999
    /// 当前最大号:30006
    /// </remarks>
    public class OrderLog : BusinessLog
    {
        #region IBusinessLog 成员

        public const string LOG_ORDER_ADD = "[人员]在[时间]添加订单：编号为{0}";
        public const string LOG_ORDER_ADD_TITLE = "添加订单";
        public const int LOG_ORDER_ADD_CODE = 30001;

        public const string LOG_ORDER_EDIT = "[人员]在[时间]修改订单：编号为{0}";
        public const string LOG_ORDER_EDIT_TITLE = "修改订单";
        public const int LOG_ORDER_EDIT_CODE = 30002;

        public const string LOG_ORDER_DELETE = "[人员]在[时间]删除订单：编号为{0}";
        public const string LOG_ORDER_DELETE_TITLE = "删除订单";
        public const int LOG_ORDER_DELETE_CODE = 30003;

        public const string LOG_ORDER_SetOrderState = "[人员]在[时间]设置订单状态：状态为{0}，编号为{1}";
        public const string LOG_ORDER_SetOrderState_TITLE = "设置订单状态";
        public const int LOG_ORDER_SetOrderState_CODE = 30004;

        public const string LOG_RATEORDER_ADD = "[人员]在[时间]新增订单评价：订单编号为{0}";
        public const string LOG_RATEORDER_ADD_TITLE = "新增订单评价";
        public const int LOG_RATEORDER_ADD_CODE = 30005;

        public const string LOG_RATEORDER_EDIT = "[人员]在[时间]修改订单评价：订单编号为{0}，评价编号为{1}";
        public const string LOG_RATEORDER_EDIT_TITLE = "修改订单评价";
        public const int LOG_RATEORDER_EDIT_CODE = 30006;

        IDAL.IOrderLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.IOrderLog>();
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }
    /// <summary>
    /// 公司操作日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从40000-49999
    /// 当前最大号:40039
    /// </remarks>
    public class CompanyLog : BusinessLog
    {
        #region IBusinessLog 成员
        public const string LOG_COMPANY_UPDATE = "[人员]在[时间]修改公司，公司编号为{0}";
        public const string LOG_COMPANY_UPDATE_TITLE = "修改公司";
        public const int LOG_COMPANY_UPDATE_CODE = 40001;

        public const string LOG_COMPANYUSERPERSON_UPDATE = "[人员]在[时间]修改个人设置，用户编号为{0}";
        public const string LOG_COMPANYUSERPERSON_UPDATE_TITLE = "修改个人设置";
        public const int LOG_COMPANYUSERPERSON_UPDATE_CODE = 40002;

        public const string LOG_COMPANYUSER_ADD_TITLE = "添加公司子帐号";
        public const string LOG_COMPANYUSER_ADD = "[人员]在[时间]添加公司子帐号,用户名:{1}，用户编号为{0}";
        public const int LOG_COMPANYUSER_ADD_CODE = 40003;

        public const string LOG_COMPANYUSERCHILD_UPDATE = "[人员]在[时间]修改子帐号，用户编号为{0}";
        public const string LOG_COMPANYUSERCHILD_UPDATE_TITLE = "修改子帐号";
        public const int LOG_COMPANYUSERCHILD_UPDATE_CODE = 40004;

        public const string LOG_COMPANYUSERCHILD_DELETE = "[人员]在[时间]真实删除子帐号，用户编号为{0}";
        public const string LOG_COMPANYUSERCHILD_DELETE_TITLE = "真实删除子帐号";
        public const int LOG_COMPANYUSERCHILD_DELETE_CODE = 40005;

        public const string LOG_COMPANYUSERCHILD_REOMVE = "[人员]在[时间]虚拟删除子帐号，用户编号为{0}";
        public const string LOG_COMPANYUSERCHILD_REOMVE_TITLE = "虚拟删除子帐号";
        public const int LOG_COMPANYUSERCHILD_REOMVE_CODE = 40006;

        public const string LOG_COMPANYUSERPERSON_UPDATE_PASSWORD = "[人员]在[时间]修改个人密码，用户编号为{0}";
        public const string LOG_COMPANYUSERPERSON_UPDATE_PASSWORD_TITLE = "修改个人密码";
        public const int LOG_COMPANYUSERPERSON_UPDATE_PASSWORD_CODE = 40007;

        public const string LOG_MyCustomer_SetMyCustomer = "[人员]在[时间]设置我的客户，要设置的公司编号为{0}";
        public const string LOG_MyCustomer_SetMyCustomer_Title = "设置我的客户";
        public const int LOG_MyCustomer_SetMyCustomer_CODE = 40008;

        public const string LOG_MyCustomer_CancelMyCustomer = "[人员]在[时间]取消我的客户，要取消设置的公司编号为{0}";
        public const string LOG_MyCustomer_CancelMyCustomer_Title = "取消我的客户";
        public const int LOG_MyCustomer_CancelMyCustomer_CODE = 40009;

        public const string LOG_CompanyFavor_SetFavor = "[人员]在[时间]设置采购目录，关注公司编号为{0}";
        public const string LOG_CompanyFavor_SetFavor_Title = "设置采购目录";
        public const int LOG_CompanyFavor_SetFavor_CODE = 40010;

        public const string LOG_CompanyFavor_CancelFavor = "[人员]在[时间]取消设置采购目录，取消关注的公司编号为{0}";
        public const string LOG_CompanyFavor_CancelFavor_Title = "取消设置采购目录";
        public const int LOG_CompanyFavor_CancelFavor_CODE = 40011;

        public const string LOG_COMPANYUSER_SETENABLE_TITLE = "设置用户状态";
        public const string LOG_COMPANYUSER_SETENABLE = "[人员]在[时间]设置用户状态，状态为{1}，用户编号为{0}";
        public const int LOG_COMPANYUSER_SETENABLE_CODE = 40012;

        public const string LOG_COMPANYAREASETTING_UPDATE_TITLE = "修改单位区域设置";
        public const string LOG_COMPANYAREASETTING_UPDATE = "[人员]在[时间]修改单位区域设置";
        public const int LOG_COMPANYAREASETTING_UPDATE_CODE = 40013;

        public const string LOG_COMPANYLOGO_UPDATE_TITLE = "修改公司LOGO";
        public const string LOG_COMPANYLOGO_UPDATE = "[人员]在[时间]修改公司LOGO，公司编号为{0}";
        public const int LOG_COMPANYLOGO_UPDATE_CODE = 40014;

        public const string LOG_COMPANYIMAGE_UPDATE_TITLE = "修改公司宣传图片";
        public const string LOG_COMPANYIMAGE_UPDATE = "[人员]在[时间]修改公司宣传图片，公司编号为{0}";
        public const int LOG_COMPANYIMAGE_UPDATE_CODE = 40015;

        public const string LOG_COMPANYCARD_UPDATE_TITLE = "修改公司企业名片";
        public const string LOG_COMPANYCARD_UPDATE = "[人员]在[时间]修改公司企业名片，公司编号为{0}";
        public const int LOG_COMPANYCARD_UPDATE_CODE = 40016;

        public const string LOG_COMPANYPORTCITY_UPDATE_TITLE = "设置公司常用的出港城市";
        public const string LOG_COMPANYPORTCITY_UPDATE = "[人员]在[时间]设置公司常用的出港城市，公司编号为{0}";
        public const int LOG_COMPANYPORTCITY_UPDATE_CODE = 40017;

        public const string LOG_COMPANYDEPARTMENT_ADD_TITLE = "添加部门";
        public const string LOG_COMPANYDEPARTMENT_ADD = "[人员]在[时间]添加部门，部门名称为:{0},公司编号为{1}";
        public const int LOG_COMPANYDEPARTMENT_ADD_CODE = 40018;

        public const string LOG_COMPANYDEPARTMENT_UPDATE_TITLE = "修改部门";
        public const string LOG_COMPANYDEPARTMENT_UPDATE = "[人员]在[时间]修改部门，部门编号为{0},部门名称为:{1},公司编号为{2}";
        public const int LOG_COMPANYDEPARTMENT_UPDATE_CODE = 40019;

        public const string LOG_COMPANYDEPARTMENT_DELETE_TITLE = "删除部门";
        public const string LOG_COMPANYDEPARTMENT_DELETE = "[人员]在[时间]删除部门，部门编号为{0}";
        public const int LOG_COMPANYDEPARTMENT_DELETE_CODE = 40020;

        public const string LOG_COMPANYPRICESTAND_UPDATE_TITLE = "修改公司价格等级";
        public const string LOG_COMPANYPRICESTAND_UPDATE = "[人员]在[时间]修改公司价格等级，公司编号为{0}";
        public const int LOG_COMPANYPRICESTAND_UPDATE_CODE = 40021;

        public const string LOG_COMPANYROLES_ADD_TITLE = "添加公司角色";
        public const string LOG_COMPANYROLES_ADD = "[人员]在[时间]添加公司角色，角色名称为:{0},公司编号为{1}";
        public const int LOG_COMPANYROLES_ADD_CODE = 40022;

        public const string LOG_COMPANYROLES_UPDATE_TITLE = "修改公司角色";
        public const string LOG_COMPANYROLES_UPDATE = "[人员]在[时间]修改公司角色，角色编号为:{0},角色名称为:{1},公司编号为{2}";
        public const int LOG_COMPANYROLES_UPDATE_CODE = 40023;

        public const string LOG_COMPANYROLES_DELETE_TITLE = "删除公司角色";
        public const string LOG_COMPANYROLES_DELETE = "[人员]在[时间]公司角色，角色编号为{0}";
        public const int LOG_COMPANYROLES_DELETE_CODE = 40024;

        public const string LOG_COMPANYSETTING_UPDATE_TITLE = "修改单位设置所有信息";
        public const string LOG_COMPANYSETTING_UPDATE = "[人员]在[时间]修改单位设置所有信息,公司编号为:{0}";
        public const int LOG_COMPANYSETTING_UPDATE_CODE = 40025;

        public const string LOG_COMPANYSETTING_FIRSTMENU_UPDATE_TITLE = "修改单位设置--优先展示栏目位置";
        public const string LOG_COMPANYSETTING_FIRSTMENU_UPDATE = "[人员]在[时间]修改单位设置--优先展示栏目位置,公司编号为:{0}";
        public const int LOG_COMPANYSETTING_FIRSTMENU_UPDATE_CODE = 40026;

        public const string LOG_COMPANYSETTING_ORDERREFRESH_UPDATE_TITLE = "修改单位设置--订单刷新时间";
        public const string LOG_COMPANYSETTING_ORDERREFRESH_UPDATE = "[人员]在[时间]修改单位设置--订单刷新时间,公司编号为:{0}";
        public const int LOG_COMPANYSETTING_ORDERREFRESH_UPDATE_CODE = 40027;

        public const string LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE_TITLE = "修改单位设置--团队自动停收时间";
        public const string LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE = "[人员]在[时间]修改单位设置--团队自动停收时间,公司编号为:{0}";
        public const int LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE_CODE = 40028;

        public const string LOG_SERVICESTANDARD_ADD_TITLE = "添加包含项目";
        public const string LOG_SERVICESTANDARD_ADD = "[人员]在[时间]添加包含项目,公司编号为:{0},包含项目类型为{1}";
        public const int LOG_SERVICESTANDARD_ADD_CODE = 40029;

        public const string LOG_SERVICESTANDARD_UPDATE_TITLE = "修改包含项目";
        public const string LOG_SERVICESTANDARD_UPDATE = "[人员]在[时间]修改包含项目,公司编号为:{0},包含项目类型为{1}";
        public const int LOG_SERVICESTANDARD_UPDATE_CODE = 40030;

        public const string LOG_SERVICESTANDARD_DELETE_TITLE = "删除包含项目";
        public const string LOG_SERVICESTANDARD_DELETE = "[人员]在[时间]添加包含项目,公司编号为:{0},包含项目主键为{1}";
        public const int LOG_SERVICESTANDARD_DELETE_CODE = 40031;

        public const string LOG_COMPANYMQADV_UPDATE_TITLE = "修改公司MQ广告";
        public const string LOG_COMPANYMQADV_UPDATE = "[人员]在[时间]修改公司MQ广告，公司编号为{0}";
        public const int LOG_COMPANYMQADV_UPDATE_CODE = 40032;

        public const string LOG_COMPANYSHOPBANNER_UPDATE_TITLE = "修改公司高级网店顶部";
        public const string LOG_COMPANYSHOPBANNER_UPDATE = "[人员]在[时间]修改公司高级网店顶部，公司编号为{0}";
        public const int LOG_COMPANYSHOPBANNER_UPDATE_CODE = 40033;

        public const string LOG_RECEIVABLES_ADD_TITLE = "添加应收账款信息";
        public const string LOG_RECEIVABLES_ADD = "[人员]在[时间]添加应收账款,公司编号为:{0},应收账款信息编号为{1}";
        public const int LOG_RECEIVABLES_ADD_CODE = 40034;

        public const string LOG_FundRegister_Add_TITLE = "添加收款或付款登记";
        public const string LOG_FundRegister_Add = "[人员]在[时间]添加收款或付款登记,登记编号:{0}";
        public const int LOG_FundRegister_Add_CODE = 40035;

        public const string LOG_FundRegister_Delete_TITLE = "删除收款或付款登记";
        public const string LOG_FundRegister_Delete = "[人员]在[时间]删除收款或付款登记,登记编号:{0}";
        public const int LOG_FundRegister_Delete_CODE = 40036;

        public const string LOG_FundRegister_Update_TITLE = "更新收款或付款登记";
        public const string LOG_FundRegister_Update = "[人员]在[时间]更新收款或付款登记,登记编号:{0}";
        public const int LOG_FundRegister_Update_CODE = 40037;

        public const string LOG_FundRegister_Check_TITLE = "收款或付款登记审核";
        public const string LOG_FundRegister_Check = "[人员]在[时间]审核收款或付款登记,登记编号:{0}";
        public const int LOG_FundRegister_Check_CODE = 40038;

        public const string LOG_PAYMENTS_ADD_TITLE = "添加应付账款信息";
        public const string LOG_PAYMENTS_ADD = "[人员]在[时间]添加应付账款,公司编号为:{0},应付账款信息编号为{1}";
        public const int LOG_PAYMENTS_ADD_CODE = 40039;

        IDAL.ICompanyLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.ICompanyLog>();
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }
    /// <summary>
    /// 平台服务日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从50000-59999
    /// 当前最大号:50023
    /// </remarks>
    public class ServiceLog : BusinessLog
    {
        #region IBusinessLog 成员

        #region 高级网店相关日志标签
        public const string LOG_HighShopAdv_INSERT = "[人员]在[时间]设置高级网店广告，公司编号为{0}";
        public const string LOG_HighShopAdv_INSERT_TITLE = "设置高级网店广告";
        public const int LOG_HighShopAdv_INSERT_CODE = 50001;

        public const string LOG_HighShopFriendLink_INSERT = "[人员]在[时间]添加高级网店友情链接，编号为{0}";
        public const string LOG_HighShopFriendLink_INSERT_TITLE = "添加高级网店友情链接";
        public const int LOG_HighShopFriendLink_INSERT_CODE = 50002;

        public const string LOG_HighShopFriendLink_UPDATE = "[人员]在[时间]修改高级网店友情链接，编号为{0}";
        public const string LOG_HighShopFriendLink_UPDATE_TITLE = "修改高级网店友情链接";
        public const int LOG_HighShopFriendLink_UPDATE_CODE = 50003;

        public const string LOG_HighShopFriendLink_DELETE = "[人员]在[时间]删除高级网店友情链接，编号为{0}";
        public const string LOG_HighShopFriendLink_DELETE_TITLE = "删除高级网店友情链接";
        public const int LOG_HighShopFriendLink_DELETE_CODE = 50004;

        public const string LOG_HighShopNews_INSERT = "[人员]在[时间]添加高级网店旅游动态，编号为{0}";
        public const string LOG_HighShopNews_INSERT_TITLE = "添加高级网店旅游动态";
        public const int LOG_HighShopNews_INSERT_CODE = 50005;

        public const string LOG_HighShopNews_UPDATE = "[人员]在[时间]修改高级网店旅游动态，编号为{0}";
        public const string LOG_HighShopNews_UPDATE_TITLE = "修改高级网店旅游动态";
        public const int LOG_HighShopNews_UPDATE_CODE = 50006;

        public const string LOG_HighShopNews_DELETE = "[人员]在[时间]删除高级网店旅游动态，编号为{0}";
        public const string LOG_HighShopNews_DELETE_TITLE = "删除高级网店旅游动态";
        public const int LOG_HighShopNews_DELETE_CODE = 50007;

        public const string LOG_HighShopNews_SETTOP = "[人员]在[时间]置顶高级网店旅游动态，编号为{0},置顶状态为{1}";
        public const string LOG_HighShopNews_SETTOP_TITLE = "置顶高级网店旅游动态";
        public const int LOG_HighShopNews_SETTOP_CODE = 50008;

        public const string LOG_HighShopResource_INSERT = "[人员]在[时间]添加高级网店资源推荐，编号为{0}";
        public const string LOG_HighShopResource_INSERT_TITLE = "添加高级网店资源推荐";
        public const int LOG_HighShopResource_INSERT_CODE = 50009;

        public const string LOG_HighShopResource_UPDATE = "[人员]在[时间]修改高级网店资源推荐，编号为{0}";
        public const string LOG_HighShopResource_UPDATE_TITLE = "修改高级网店资源推荐";
        public const int LOG_HighShopResource_UPDATE_CODE = 50010;

        public const string LOG_HighShopResource_DELETE = "[人员]在[时间]删除高级网店资源推荐，编号为{0}";
        public const string LOG_HighShopResource_DELETE_TITLE = "删除高级网店资源推荐";
        public const int LOG_HighShopResource_DELETE_CODE = 50011;

        public const string LOG_HighShopResource_SETTOP = "[人员]在[时间]置顶高级网店资源推荐，编号为{0}，置顶状态为{1}";
        public const string LOG_HighShopResource_SETTOP_TITLE = "置顶高级网店资源推荐";
        public const int LOG_HighShopResource_SETTOP_CODE = 50012;

        public const string LOG_HighShopTripGuide_INSERT = "[人员]在[时间]添加高级网店出游指南，编号为{0}";
        public const string LOG_HighShopTripGuide_INSERT_TITLE = "添加高级网店出游指南";
        public const int LOG_HighShopTripGuide_INSERT_CODE = 50013;

        public const string LOG_HighShopTripGuide_UPDATE = "[人员]在[时间]修改高级网店出游指南，编号为{0}";
        public const string LOG_HighShopTripGuide_UPDATE_TITLE = "修改高级网店出游指南";
        public const int LOG_HighShopTripGuide_UPDATE_CODE = 50014;

        public const string LOG_HighShopTripGuide_DELETE = "[人员]在[时间]删除高级网店出游指南，编号为{0}";
        public const string LOG_HighShopTripGuide_DELETE_TITLE = "删除高级网店出游指南";
        public const int LOG_HighShopTripGuide_DELETE_CODE = 50015;

        public const string LOG_HighShopTripGuide_SETTOP = "[人员]在[时间]置顶高级网店出游指南，编号为{0}";
        public const string LOG_HighShopTripGuide_SETTOP_TITLE = "置顶高级网店出游指南";
        public const int LOG_HighShopTripGuide_SETTOP_CODE = 50016;

        public const string LOG_HighShopCompanyInfo_SetAboutUs = "[人员]在[时间]设置关于我们，公司编号为{0}";
        public const string LOG_HighShopCompanyInfo_SetAboutUs_TITLE = "设置关于我们";
        public const int LOG_HighShopCompanyInfo_SetAboutUs_CODE = 50017;

        public const string LOG_HighShopCompanyInfo_SetCopyRight = "[人员]在[时间]设置版权，公司编号为{0}";
        public const string LOG_HighShopCompanyInfo_SetCopyRight_TITLE = "设置版权";
        public const int LOG_HighShopCompanyInfo_SetCopyRight_CODE = 50018;

        public const string LOG_HighShopCompanyInfo_SetTemplate = "[人员]在[时间]设置自定义模板，公司编号为{0},模板编号为{1}";
        public const string LOG_HighShopCompanyInfo_SetTemplate_TITLE = "设置自定义模板";
        public const int LOG_HighShopCompanyInfo_SetTemplate_CODE = 50019;

        #endregion

        #region 供求信息收藏夹日志标签
        public const string LOG_ExchangeFavor_INSERT = "[人员]在[时间]收藏供求信息{0}";
        public const string LOG_ExchangeFavor_INSERT_TITLE = "收藏供求信息";
        public const int LOG_ExchangeFavor_INSERT_CODE = 50020;

        public const string LOG_ExchangeFavor_DELETE = "[人员]在[时间]删除收藏供求{0}";
        public const string LOG_ExchangeFavor_DELETE_TITLE = "删除收藏供求";
        public const int LOG_ExchangeFavor_DELETE_CODE = 50021;
        #endregion

        #region 供求信息日志标签
        public const string LOG_ExchangeList_INSERT = "[人员]在[时间]添加供求信息，编号为{0}";
        public const string LOG_ExchangeList_INSERT_TITLE = "添加供求信息";
        public const int LOG_ExchangeList_INSERT_CODE = 50022;

        public const string LOG_ExchangeList_UPDATE = "[人员]在[时间]修改供求信息，编号为{0}";
        public const string LOG_ExchangeList_UPDATE_TITLE = "修改供求信息";
        public const int LOG_ExchangeList_UPDATE_CODE = 50023;

        public const string LOG_ExchangeList_DELETE = "[人员]在[时间]删除供求信息编号为{0}";
        public const string LOG_ExchangeList_DELETE_TITLE = "删除供求信息";
        public const int LOG_ExchangeList_DELETE_CODE = 60019;
        #endregion

        #region 短信中心日志标签
        public const string LOG_SMS_INSERTCUSTOMERCATEGORY = "[人员]在[时间]添加客户类型信息，类型为{0}";
        public const string LOG_SMS_INSERTCUSTOMERCATEGORY_TITLE = "添加客户类型信息";
        public const int LOG_SMS_INSERTCUSTOMERCATEGORY_CODE = 50024;

        public const string LOG_SMS_DELETECUSTOMERCATEGORY = "[人员]在[时间]删除客户类型信息，编号为{0}";
        public const string LOG_SMS_DELETECUSTOMERCATEGORY_TITLE = "删除客户类型信息";
        public const int LOG_SMS_DELETETCUSTOMERCATEGORY_CODE = 50025;

        public const string LOG_SMS_INSERTCUSTOMER = "[人员]在[时间]添加客户信息";
        public const string LOG_SMS_INSERTCUSTOMER_TITLE = "添加客户信息";
        public const int LOG_SMS_INSERTCUSTOMER_CODE = 50026;

        public const string LOG_SMS_DELETECUSTOMER = "[人员]在[时间]删除客户信息，编号为{0}";
        public const string LOG_SMS_DELETECUSTOMER_TITLE = "删除客户信息";
        public const int LOG_SMS_DELETECUSTOMER_CODE = 50027;

        public const string LOG_SMS_UPDATECUSTOMER = "[人员]在[时间]修改客户信息，编号为{0}";
        public const string LOG_SMS_UPDATECUSTOMER_TITLE = "修改客户信息";
        public const int LOG_SMS_UPDATECUSTOMER_CODE = 50028;

        public const string LOG_SMS_INSERTTEMPLATECATEGORY = "[人员]在[时间]添加常用短语类型信息，类型为{0}";
        public const string LOG_SMS_INSERTTEMPLATERCATEGORY_TITLE = "添加常用短语类型信息";
        public const int LOG_SMS_INSERTTEMPLATECATEGORY_CODE = 50029;

        public const string LOG_SMS_DELETETEMPLATECATEGORY = "[人员]在[时间]删除常用短语类型信息，编号为{0}";
        public const string LOG_SMS_DELETETEMPLATECATEGORY_TITLE = "删除常用短语类型信息";
        public const int LOG_SMS_DELETETEMPLATECATEGORY_CODE = 50030;

        public const string LOG_SMS_INSERTTEMPLATE = "[人员]在[时间]添加常用短语信息";
        public const string LOG_SMS_INSERTTEMPLATE_TITLE = "添加常用短语信息";
        public const int LOG_SMS_INSERTTEMPLATE_CODE = 50031;

        public const string LOG_SMS_DELETETEMPLATE = "[人员]在[时间]删除常用短语信息，编号为{0}";
        public const string LOG_SMS_DELETETEMPLATE_TITLE = "删除常用短语信息";
        public const int LOG_SMS_DELETETEMPLATE_CODE = 50032;

        public const string LOG_SMS_UPDATETEMPLATE = "[人员]在[时间]修改常用短语信息，编号为{0}";
        public const string LOG_SMS_UPDATETEMPLATE_TITLE = "修改常用短语信息";
        public const int LOG_SMS_UPDATETEMPLATE_CODE = 50033;
        #endregion


        IDAL.IServiceLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.IServiceLog>();
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }

    /// <summary>
    /// 运营后台日志
    /// </summary>
    /// <remarks>
    /// 事件类型号从60000-69999
    /// 当前最大号:60058
    /// </remarks>
    public class WebMasterLog : BusinessLog
    {
        #region 公司日志标签

        public const string LOG_COMPANY_UPDATE = "[人员]在[时间]修改公司：公司名称{0}，公司编号为{1}";
        public const string LOG_COMPANY_UPDATE_TITLE = "修改公司";
        public const int LOG_COMPANY_UPDATE_CODE = 60001;

        public const string LOG_COMPANY_DELETE = "[人员]在[时间]真实删除公司，公司编号为{0}";
        public const string LOG_COMPANY_DELETE_TITLE = "真实删除公司";
        public const int LOG_COMPANY_DELETE_CODE = 60002;

        public const string LOG_COMPANY_REMOVE = "[人员]在[时间]虚拟删除公司，公司编号为{0}";
        public const string LOG_COMPANY_REMOVE_TITLE = "虚拟删除公司";
        public const int LOG_COMPANY_REMOVE_CODE = 60003;

        public const string LOG_COMPANY_STATE_TITLE = "设置公司状态";
        public const string LOG_COMPANY_STATE = "[人员]在[时间]设置公司状态，状态为{1}，公司编号为{0}";
        public const int LOG_COMPANY_STATE_CODE = 60004;

        public const string LOG_COMPANY_PASSREGISTER_TITLE = "审核公司";
        public const string LOG_COMPANY_PASSREGISTER = "[人员]在[时间]审核公司，通过审核，公司编号为{0}";
        public const int LOG_COMPANY_PASSREGISTER_CODE = 60005;

        public const string LOG_COMPANY_SETCERTIFICATE_TITLE = "审核公司承诺书";
        public const string LOG_COMPANY_SETCERTIFICATE = "[人员]在[时间]审核公司承诺书，状态为{1}，公司编号为{0}";
        public const int LOG_COMPANY_SETCERTIFICATE_CODE = 60006;

        public const string LOG_COMPANY_SETCREATE_TITLE = "审核诚信档案中的证书(营业执照,经营许可证,税务登记证)";
        public const string LOG_COMPANY_SETCREATE = "[人员]在[时间]审核诚信档案中的证书，状态为{1}，公司编号为{0}";
        public const int LOG_COMPANY_SETCREATE_CODE = 60007;
        #endregion

        #region 广告日志标签

        public const string LOG_Adv_InsertAdv = "[人员]在[时间]发布广告，编号为{0}";
        public const string LOG_Adv_InsertAdv_Title = "发布广告";
        public const int LOG_Adv_InsertAdv_CODE = 60008;

        public const string LOG_Adv_UpdateAdv = "[人员]在[时间]更新广告，编号为{0}";
        public const string LOG_Adv_UpdateAdv_Title = "更新广告";
        public const int LOG_Adv_UpdateAdv_CODE = 60009;

        public const string LOG_Adv_DeleteAdv = "[人员]在[时间]删除广告，编号为{0}";
        public const string LOG_Adv_DeleteAdv_Title = "删除广告";
        public const int LOG_Adv_DeleteAdv_CODE = 60010;

        public const string LOG_Adv_SetAdvSort = "[人员]在[时间]设置广告排序，编号为{0}";
        public const string LOG_Adv_SetAdvSort_Title = "设置广告排序";
        public const int LOG_Adv_SetAdvSort_CODE = 60011;

        #endregion

        #region 顾问团队日志标签
        public const string LOG_Advisor_INSERT = "[人员]在[时间]添加顾问团队：{0}，编号为{1}";
        public const string LOG_Advisor_INSERT_TITLE = "添加顾问团队";
        public const int LOG_Advisor_INSERT_CODE = 60012;

        public const string LOG_Advisor_UPDATE = "[人员]在[时间]修改顾问团队：{0}，编号为{1}";
        public const string LOG_Advisor_UPDATE_TITLE = "修改顾问团队";
        public const int LOG_Advisor_UPDATE_CODE = 60013;

        public const string LOG_Advisor_DELETE = "[人员]在[时间]删除顾问团队编号为{0}";
        public const string LOG_Advisor_DELETE_TITLE = "删除顾问团队";
        public const int LOG_Advisor_DELETE_CODE = 60014;

        public const string LOG_Advisor_CHECK = "[人员]在[时间]审核顾问团队编号为{0}，是否审核为{1}";
        public const string LOG_Advisor_CHECK_TITLE = "审核顾问团队";
        public const int LOG_Advisor_CHECK_CODE = 60015;

        public const string LOG_Advisor_Show = "[人员]在[时间]设置顾问团队前台显示编号为{0}，前台是否显示为{1}";
        public const string LOG_Advisor_Show_TITLE = "设置顾问团队前台显示";
        public const int LOG_Advisor_Show_CODE = 60054;

        #endregion

        #region 嘉宾访谈日志标签
        public const string LOG_HonoredGuest_INSERT = "[人员]在[时间]添加嘉宾访谈：{0}，编号为{1}";
        public const string LOG_HonoredGuest_INSERT_TITLE = "添加嘉宾访谈";
        public const int LOG_HonoredGuest_INSERT_CODE = 60016;

        public const string LOG_HonoredGuest_UPDATE = "[人员]在[时间]修改嘉宾访谈：{0}，编号为{1}";
        public const string LOG_HonoredGuest_UPDATE_TITLE = "修改嘉宾访谈";
        public const int LOG_HonoredGuest_UPDATE_CODE = 60017;

        public const string LOG_HonoredGuest_DELETE = "[人员]在[时间]删除嘉宾访谈编号为{0}";
        public const string LOG_HonoredGuest_DELETE_TITLE = "删除嘉宾访谈";
        public const int LOG_HonoredGuest_DELETE_CODE = 60018;
        #endregion

        #region 供求信息日志标签
        public const string LOG_ExchangeList_DELETE = "[人员]在[时间]删除供求信息编号为{0}";
        public const string LOG_ExchangeList_DELETE_TITLE = "删除供求信息";
        public const int LOG_ExchangeList_DELETE_CODE = 60019;

        public const string LOG_ExchangeList_SETTOP = "[人员]在[时间]置顶供求信息编号为{0},置顶状态为{1}";
        public const string LOG_ExchangeList_SETTOP_TITLE = "置顶供求信息";
        public const int LOG_ExchangeList_SETTOP_CODE = 60020;

        public const string LOG_ExchangeList_SETISCHECK = "[人员]在[时间]审核供求信息编号为{0},审核状态为{1}";
        public const string LOG_ExchangeList_SETISCHECK_TITLE = "审核供求信息";
        public const int LOG_ExchangeList_SETISCHECK_CODE = 60055;
        #endregion

        #region 行业资讯
        public const string LOG_InfoArticle_INSERT = "[人员]在[时间]添加行业资讯编号为{0}";
        public const string LOG_InfoArticle_INSERT_TITLE = "添加行业资讯";
        public const int LOG_InfoArticle_INSERT_CODE = 60021;

        public const string LOG_InfoArticle_UPDATE = "[人员]在[时间]修改行业资讯编号为{0}";
        public const string LOG_InfoArticle_UPDATE_TITLE = "修改行业资讯";
        public const int LOG_InfoArticle_UPDATE_CODE = 60022;

        public const string LOG_InfoArticle_DELETE = "[人员]在[时间]删除行业资讯编号为{0}";
        public const string LOG_InfoArticle_DELETE_TITLE = "删除行业资讯";
        public const int LOG_InfoArticle_DELETE_CODE = 60023;

        public const string LOG_InfoArticle_SETTOP = "[人员]在[时间]置顶行业资讯编号为{0}，置顶状态为{1}";
        public const string LOG_InfoArticle_SETTOP_TITLE = "置顶行业资讯";
        public const int LOG_InfoArticle_SETTOP_CODE = 60053;

        public const string LOG_InfoArticle_SETFRONTPAGE = "[人员]在[时间]设置首页显示行业资讯编号为{0}，首页显示状态为{1}";
        public const string LOG_InfoArticle_SETFRONTPAGE_TITLE = "设置首页显示行业资讯";
        public const int LOG_InfoArticle_SETFRONTPAGE_CODE = 60056;
        #endregion

        #region 新闻中心日志标签

        public const string LOG_Affiche_Add = "[人员]在[时间]添加新闻信息，类别为{0}，编号为{1}";
        public const string LOG_Affiche_Add_Title = "添加新闻信息";
        public const int LOG_Affiche_Add_CODE = 60024;

        public const string LOG_Affiche_Edit = "[人员]在[时间]修改新闻信息，类别为{0}，编号为{1}";
        public const string LOG_Affiche_Edit_Title = "修改新闻信息";
        public const int LOG_Affiche_Edit_CODE = 60025;

        public const string LOG_Affiche_Del = "[人员]在[时间]删除新闻信息，编号为{0}";
        public const string LOG_Affiche_Del_Title = "删除新闻信息";
        public const int LOG_Affiche_Del_CODE = 60026;

        public const string LOG_Affiche_SetIsTop = "[人员]在[时间]置顶新闻信息，编号为{0}";
        public const string LOG_Affiche_SetIsTop_Title = "置顶新闻信息";
        public const int LOG_Affiche_SetIsTop_CODE = 60027;

        #endregion

        #region 平台管理日志标签

        #region 系统信息标签

        public const string LOG_SystemInfo_Edit = "[人员]在[时间]修改系统信息，编号为{0}";
        public const string LOG_SystemInfo_Edit_Title = "修改系统信息";
        public const int LOG_SystemInfo_Edit_CODE = 60028;

        #endregion

        #region 分站管理日志标签

        public const string LOG_SysCity_SetIsEnabled = "[人员]在[时间]设置城市是否启用，编号为{0}，启用状态为{1}";
        public const string LOG_SysCity_SetIsEnabled_Title = "设置城市是否启用";
        public const int LOG_SysCity_SetIsEnabled_CODE = 60029;

        public const string LOG_SysCity_SetIsSite = "[人员]在[时间]设置是否出港城市，编号为{0}，是否出港城市{1}";
        public const string LOG_SysCity_SetIsSite_Title = "设置是否出港城市";
        public const int LOG_SysCity_SetIsSite_CODE = 60030;

        public const string LOG_CityArea_ADD = "[人员]在[时间]添加城市线路区域关系， 城市编号为{0}";
        public const string LOG_CityArea_ADD_TITLE = "添加城市线路区域关系";
        public const int LOG_CityArea_ADD_CODE = 60031;

        public const string LOG_CityArea_EDIT = "[人员]在[时间]修改城市线路区域关系， 城市编号为{0}";
        public const string LOG_CityArea_EDIT_TITLE = "修改城市线路区域关系";
        public const int LOG_CityArea_EDIT_CODE = 60032;

        public const string LOG_CityArea_DelByCity = "[人员]在[时间]根据城市ID删除城市线路区域关系， 城市编号为{0}";
        public const string LOG_CityArea_DelByCity_TITLE = "根据城市ID删除城市线路区域关系";
        public const int LOG_CityArea_DelByCity_CODE = 60033;

        public const string LOG_CityArea_Delete = "[人员]在[时间]删除城市线路区域关系， 城市编号为{0}，线路区域编号为{1}";
        public const string LOG_CityArea_Delete_TITLE = "删除城市线路区域关系";
        public const int LOG_CityArea_Delete_CODE = 60034;

        #endregion

        #region 线路区域分类日志标签

        public const string LOG_AreaSite_Add = "[人员]在[时间]添加长短线区域城市关系";
        public const string LOG_AreaSite_AddTitle = "添加长短线区域城市关系";
        public const int LOG_AreaSite_Add_CODE = 60035;

        public const string LOG_AreaSite_DelByPId = "[人员]在[时间]根据省份ID删除长短线区域城市关系，省份编号为{0}";
        public const string LOG_AreaSite_DelByPId_Title = "根据省份ID删除长短线区域城市关系";
        public const int LOG_AreaSite_DelByPId_CODE = 60036;

        public const string LOG_AreaSite_DelByPIdAndAId = "[人员]在[时间]根据省份ID和线路区域ID删除长短线区域城市关系，省份编号为{0}，线路区域编号为{1}";
        public const string LOG_AreaSite_DelByPIdAndAId_Title = "根据省份ID和线路区域ID删除长短线区域城市关系";
        public const int LOG_AreaSite_DelByPIdAndAId_CODE = 60037;

        public const string LOG_AreaSite_DelByCId = "[人员]在[时间]根据城市ID删除长短线区域城市关系，城市编号为{0}";
        public const string LOG_AreaSite_DelByCId_Title = "根据城市ID删除长短线区域城市关系";
        public const int LOG_AreaSite_DelByCId_CODE = 60038;

        public const string LOG_AreaSite_DelByCIdAndAId = "[人员]在[时间]根据城市ID和线路区域ID删除长短线区域城市关系，城市编号为{0}，线路区域编号为{1}";
        public const string LOG_AreaSite_DelByCIdAndAId_Title = "根据城市ID和线路区域ID删除长短线区域城市关系";
        public const int LOG_AreaSite_DelByCIdAndAId_CODE = 60039;

        #endregion

        #region 通用线路区域维护日志标签

        public const string LOG_SysArea_Add = "[人员]在[时间]添加通用专线区域，编号为{0}";
        public const string LOG_SysArea_Add_Title = "添加通用专线区域";
        public const int LOG_SysArea_Add_CODE = 60040;

        public const string LOG_SysArea_Edit = "[人员]在[时间]修改通用专线区域，编号为{0}";
        public const string LOG_SysArea_Edit_Title = "修改通用专线区域";
        public const int LOG_SysArea_Edit_CODE = 60041;

        public const string LOG_SysArea_Del = "[人员]在[时间]删除通用专线区域，编号为{0}";
        public const string LOG_SysArea_Del_Title = "删除通用专线区域";
        public const int LOG_SysArea_Del_CODE = 60042;
        #endregion

        #region 基础数据维护日志标签

        public const string LOG_SysField_Add = "[人员]在[时间]添加基础数据，类型为{0}，名称为{1}";
        public const string LOG_SysField_Add_Title = "添加基础数据";
        public const int LOG_SysField_Add_CODE = 60043;

        public const string LOG_SysField_Edit = "[人员]在[时间]修改基础数据，类型为{0}，名称为{1}，编号为{2}";
        public const string LOG_SysField_Edit_Title = "修改基础数据";
        public const int LOG_SysField_Edit_CODE = 60044;

        public const string LOG_SysField_Del = "[人员]在[时间]删除基础数据，类型为{0}，编号为{1}";
        public const string LOG_SysField_Del_Title = "删除基础数据";
        public const int LOG_SysField_Del_CODE = 60045;

        public const string LOG_SysField_SetIsEnabled = "[人员]在[时间]设置基础数据是否启用，类型为{0}，编号为{1}，是否启用为{2}";
        public const string LOG_SysField_SetIsEnabled_Title = "设置基础数据是否启用";
        public const int LOG_SysField_SetIsEnabled_CODE = 60046;
        #endregion

        #region 统计数据维护日志标签

        public const string LOG_SummaryCount_Edit = "[人员]在[时间]修改系统数据统计";
        public const string LOG_SummaryCount_Edit_Title = "修改系统数据统计";
        public const int LOG_SummaryCount_Edit_CODE = 60047;

        #endregion

        #region 友情链接和战略合作伙伴日志标签

        public const string LOG_SysFriendLink_Add = "[人员]在[时间]添加友情链接，类型为{0}，编号为{1}";
        public const string LOG_SysFriendLink_Add_Title = "添加友情链接";
        public const int LOG_SysFriendLink_Add_CODE = 60048;

        public const string LOG_SysFriendLink_Edit = "[人员]在[时间]修改友情链接，类型为{0}，编号为{1}";
        public const string LOG_SysFriendLink_Edit_Title = "修改友情链接";
        public const int LOG_SysFriendLink_Edit_CODE = 60049;

        public const string LOG_SysFriendLink_Del = "[人员]在[时间]删除友情链接或者战略合作伙伴，编号为{0}";
        public const string LOG_SysFriendLink_Del_Title = "删除友情链接或者战略合作伙伴";
        public const int LOG_SysFriendLink_Del_CODE = 60050;

        #endregion

        #endregion

        #region 短信充值管理
        public const string LOG_SMS_CheckPayMoney_Title = "短信充值审核";
        public const string LOG_SMS_CheckPayMoney = "[人员]在[时间]短信充值审核，充值支付编号为{0}";
        public const int LOG_SMS_CheckPayMoney_CODE = 60051;

        public const string LOG_SMS_DeleteNoPassCheckPayMoney_Title = "删除未通过审核的充值记录";
        public const string LOG_SMS_DeleteNoPassCheckPayMoney = "[人员]在[时间]删除未通过审核的充值记录，充值记录编号为{0}";
        public const int LOG_SMS_DeleteNoPassCheckPayMoney_CODE = 60052;
        #endregion

        #region 服务审核标签
        public const string LOG_HighShop_CHECK = "[人员]在[时间]高级网店审核，高级网店编号为{0}";
        public const string LOG_HighShop_CHECK_TITLE = "高级网店审核";
        public const int LOG_HighShop_CHECK_CODE = 60057;
        #endregion

        #region MQ
        public const string LOG_MQ_SetGroupMember = "[人员]在[时间]设置MQ群人数，群号：{0}，人数：{1}";
        public const string LOG_MQ_SetGroupMember_Title = "设置MQ群人数";
        public const int LOG_MQ_SetGroupMember_CODE = 60058;
        #endregion

        #region IBusinessLog 成员
        IDAL.IWebMasterLog DAL = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.IWebMasterLog>();
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public override void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DAL.WriteLog(model);
        }
        #endregion
    }

    #endregion
}
#endregion

#region 日志记录数据层接口

namespace EyouSoft.BusinessLogWriter.IDAL
{
    /// <summary>
    /// 日志写入
    /// </summary>
    /// <param name="model"></param>
    public interface ILogWriter
    {
        void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model);
    }
    /// <summary>
    /// 订单日志
    /// </summary>
    public interface ITourLog : ILogWriter { }
    public interface IRouteLog : ILogWriter { }
    public interface ICompanyLog : ILogWriter { }
    public interface IWebMasterLog : ILogWriter { }
    public interface IOrderLog : ILogWriter { }
    public interface IServiceLog : ILogWriter { }
    public interface IExceptionLog : ILogWriter { }
}
#endregion

#region 日志记录数据层

namespace EyouSoft.BusinessLogWriter.DAL
{
    using System.Data;
    using System.Data.Common;
    using EyouSoft.Common.DAL;

    #region 成员类

    /// <summary>
    /// 团队日志
    /// </summary>
    public class TourLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.ITourLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogTour([EventID],[CompanyId],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                     "@EventID,@CompanyId,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        #endregion
    }
    /// <summary>
    /// 运营后台日志
    /// </summary>
    public class WebMasterLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.IWebMasterLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogSystem([EventID],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                     "@EventID,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiString, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        private void Log(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            System.IO.StreamWriter ad = new System.IO.StreamWriter(@"C:\companylog.txt", true);
            model.EventTime = DateTime.Now;
            ad.WriteLine(EyouSoft.Common.SerializationHelper.ConvertJSON<EyouSoft.BusinessLogWriter.Model.LogWriter>(model));
            ad.Close();
        }
        #endregion
    }
    /// <summary>
    /// 公司日志
    /// </summary>
    public class CompanyLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.ICompanyLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogCompany([EventID],[CompanyId],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                     "@EventID,@CompanyId,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        #endregion
    }
    /// <summary>
    /// 订单日志
    /// </summary>
    public class OrderLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.IOrderLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogOrder([EventID],[CompanyId],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                     "@EventID,@CompanyId,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        #endregion
    }
    /// <summary>
    /// 线路日志
    /// </summary>
    public class RouteLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.IRouteLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogRoute([EventID],[CompanyId],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                    "@EventID,@CompanyId,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        #endregion
    }
    /// <summary>
    /// 平台服务日志
    /// </summary>
    public class ServiceLog : EyouSoft.Common.DAL.DALBase, EyouSoft.BusinessLogWriter.IDAL.IServiceLog
    {
        #region ILog 成员
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogService([EventID],[CompanyId],[OperatorId],[OperatorName],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP],[EventTime])VALUES(" +
                                    "@EventID,@CompanyId,@OperatorId,@OperatorName,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP,@EventTime)";
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        public void WriteLog(EyouSoft.BusinessLogWriter.Model.LogWriter model)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.AnsiStringFixedLength, model.EventID);
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, model.EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, model.EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, model.EventCode);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, model.EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, model.EventIP);
            this.LogStore.AddInParameter(dc, "EventTime", DbType.String, model.EventTime);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        #endregion
    }
    #endregion
}
#endregion

#region 日志记录实体
namespace EyouSoft.BusinessLogWriter.Model
{
    /// <summary>
    /// 日志记录实体
    /// </summary>
    public class LogWriter
    {
        public string EventID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string EventTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string EventMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 日志类型号
        /// </summary>
        public int EventCode
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生地址
        /// </summary>
        public string EventUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生IP
        /// </summary>
        public string EventIP
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生时间
        /// </summary>
        public DateTime EventTime
        {
            get;
            set;
        }
    }
}
#endregion