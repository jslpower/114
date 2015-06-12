using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 公司信息业务逻辑接口层
    /// </summary>
    /// 创建人：张志瑜 2010-06-02
    /// -------------------------
    /// 修改人：鲁功源 2011-04-06
    /// 内容：新增方法GetTopNumNewCompanys(int TopNum,bool? IsHighShop)
    /// -------------------------
    /// 修改人：郑付杰 2011-12-23
    /// 内容：新增方法 GetNewComapnyScenic(int topNum,EyouSoft.Model.CompanyStructure.CompanyType companyType);
    public interface ICompanyInfo
    {
        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="model">公司实体</param>
        /// <param name="otherCityText">其他销售城市</param>
        /// <returns></returns>
        EyouSoft.Model.ResultStructure.UserResultInfo Add(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model, string otherCityText);

        /// <summary>
        /// 运营后台修改公司资料,该方法会设置销售城市,线路区域信息(若不修改密码,请设置密码字段为空)
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CompanyName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_UPDATE_CODE
            )]    //说明:Index表示为Update方法中的参数索引值(model参数的索引值为0),Attribute表示为要取得值的字段名称,AttributeType表示参数的类型(类:class,具体值:val,数组:array)
        bool Update(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// 高级网店修改公司档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE_CODE
            )]
        bool UpdateArchive(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// 单位信息管理,修改自己公司的档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANY_UPDATE_CODE
            )]
        bool UpdateSelf(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// 真实删除公司
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_DELETE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyIdList"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_DELETE_CODE
            )]
        bool Delete(params string[] companyIdList);
        /// <summary>
        /// 移除公司(即虚拟删除公司)
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_REMOVE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_REMOVE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyIdList"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_REMOVE_CODE
            )]
        bool Remove(params string[] companyIdList);
        /// <summary>
        /// 获取公司状态信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyStateMore GetCompanyState(string companyId);
        /// <summary>
        /// 获取公司明细信息实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetModel(string companyid);
        /// <summary>
        /// 根据用户MQID获得公司以及当前用户明细信息实体类
        /// </summary>
        /// <param name="userMqId">用户MQID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyAndUserInfo GetModel(int userMqId);
        /// <summary>
        /// 根据oPUserID获得公司以及当前用户明细信息实体类
        /// </summary>
        /// <param name="oPUserID">用户OPUserID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyAndUserInfo GetModelByOPUserID(int oPUserID);
        /// <summary>
        /// 运营后台获取未审核的单位[包括旅行社,景区,酒店...]明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetListNoChecked(EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 运营后台获取已审核通过的单位[包括旅行社,景区,酒店...]明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetListChecked(EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 外部客户获取已审核通过的所有旅行社单位[专线/批发商,组团/零售商,地接]列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTravelAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 外部客户获取已审核通过的批发商单位列表(批发商单位可按销售省份,销售城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListRouteAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 外部客户获取已审核通过的组团社单位列表(组团社单位可按注册所在地的省份,城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTourAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取指定销售城市,线路区域下的广告批发商单位名称
        /// </summary>
        /// <param name="query">查询条件实体[城市,线路区域为必填项]</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListCityAreaAdvRouteAgencyName(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query);
        /// <summary>
        /// 获取指定销售城市,线路区域下的广告批发商单位图文信息
        /// </summary>
        /// <param name="query">查询条件实体[城市,线路区域为必填项]</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> GetListCityAreaAdvRouteAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query);
        /// <summary>
        /// 外部客户获取已审核通过的地接社单位列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListLocalAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获得目的地地接社城市列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetListLocalCity();
        /// <summary>
        /// 设置公司状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isEnabled">true:启用  false:停用</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_STATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_STATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}, {""Index"":1,""Attribute"":""isEnabled"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_STATE_CODE
            )]
        bool SetState(string companyId, bool isEnabled);
        /// <summary>
        /// 通过注册审核
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_PASSREGISTER_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_PASSREGISTER, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_PASSREGISTER_CODE
            )]
        bool PassRegister(string companyId);
        /// <summary>
        /// 审核公司承诺书
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCERTIFICATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCERTIFICATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}, {""Index"":1,""Attribute"":""isCheck"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCERTIFICATE_CODE
            )]
        bool SetCertificateCheck(string companyId, bool isCheck);
        /// <summary>
        /// 审核诚信档案中的证书(营业执照,经营许可证,税务登记证)
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCREATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCREATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}, {""Index"":1,""Attribute"":""isCheck"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_COMPANY_SETCREATE_CODE
            )]
        bool SetCreditCheck(string companyId, bool isCheck);
        /// <summary>
        /// 获取当前登录管理员分管区域范围内的有效产品批发商信息集合(统计分析)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetValidTourCompanys(int pageSize, int pageIndex, ref int recordCount
            , int? cityId, int? areaId, string companyName);

        /// <summary>
        /// 获取当前登录管理员分管区域范围内的无效产品批发商信息集合(统计分析)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetInvalidTourCompanys(int pageSize, int pageIndex, ref int recordCount
            , int? cityId, int? areaId, string companyName);
        /// <summary>
        /// 根据公司编号获取当期公司用户的登录记录列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总的记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.LogUserLogin> GetLoginList(int pageSize, int pageIndex, ref int recordCount, string CompanyId);

        /// <summary>
        /// 根据公司编号获取当期公司用户的登录记录列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总的记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <param name="startTime">起始时间 为null时不做为查询条件</param>
        /// <param name="finishTime">截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.LogUserLogin> GetLoginList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, DateTime? startTime, DateTime? finishTime);

        /// <summary>
        /// 根据公司ID获取某段时间段内该公司的登录次数
        /// </summary>
        /// <param name="CompanyId">公司ID(必须传值)</param>
        /// <param name="StartTime">开始时间(为null不作条件)</param>
        /// <param name="EndTime">结束时间(为null不作条件)</param>
        /// <returns>登录次数</returns>
        int GetLoginCountByTime(string CompanyId, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 设置公司付费项目状态
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="serviceItem">某项服务项目</param>
        /// <returns></returns>
        bool SetCompanyPayService(string companyId, EyouSoft.Model.CompanyStructure.CompanyServiceItem serviceItem);
        #region 不要的代码
        ///// <summary>
        ///// 获取已审核通过的批发商单位和经营单位明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        ///// </summary>
        ///// <param name="companyType">公司类型</param>
        ///// <param name="ProvinceId">省份ID,若=0,则不做为查询条件</param>
        ///// <param name="SiteId">分站ID,若=0,则不做为查询条件</param>
        ///// <param name="CityId">城市ID,若=0,则不做为查询条件</param>
        ///// <param name="companyName">公司名称,若为null或空,则不做为查询条件</param>
        ///// <param name="contactName">公司负责人,若为null或空,则不做为查询条件</param>
        ///// <param name="companyBrand">公司品牌名称,若为null或空,则不做为查询条件</param>
        ///// <param name="pageSize">分页大小</param>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <returns></returns>
        //IList<EyouSoft.Model.CompanyStructure.CompanyBasicInfo> GetListCompanyAndUnit(int ProvinceId, int SiteId, string companyName, string contactName, string companyBrand, int pageSize, int pageIndex, ref int recordCount);
        ///// <summary>
        ///// 获取公司所拥有的所有分站,线路区域列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <returns></returns>
        //IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> GetCompanySiteAreaList(string companyId);
        ///// <summary>
        ///// 获取公司所拥有的分站列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <returns></returns>
        //IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanySiteList(string companyId);
        ///// <summary>
        ///// 获取当前省份,分站下公司所拥有的省份,分站,线路区域列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <param name="currentProvinceId">当前省份ID,若=0,则不做为查询条件</param>
        ///// <param name="currentSiteId">当前分站ID,若=0,则不做为查询条件</param>
        ///// <returns></returns>
        //EyouSoft.Model.SystemStructure.ProvinceSiteAreaList GetCompanyProvinceSiteAreaList(string companyId, int currentProvinceId, int currentSiteId); 
        #endregion 不要的代码

        /// <summary>
        /// 申请成为采购商/供应商
        /// </summary>
        /// <param name="companyInfo">公司信息业务实体</param>
        /// <param name="otherCityText">其他销售城市</param>
        /// <returns></returns>
        bool ToCompany(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo companyInfo, string otherCityText);
        /// <summary>
        /// 获取最新加盟的公司列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="IsHighShop">是否高级网店 =null返回所有</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetTopNumNewCompanys(int TopNum, bool? IsHighShop);

        /// <summary>
        /// 分页获取同行列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总数</param>
        /// <param name="queryEntity">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyTongHang> GetTongHangList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.QueryNewCompany queryEntity);

        /// <summary>
        /// 最新加盟公司
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <param name="businessProperties">企业性质</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetNewComapnyScenic(int topNum
            , EyouSoft.Model.CompanyStructure.BusinessProperties businessProperties);

        /// <summary>
        /// 根据公司自增编号获取公司明细信息实体
        /// </summary>
        /// <param name="opCompanyId">公司自增编号</param>
        /// <returns></returns>
        Model.CompanyStructure.CompanyDetailInfo GetModelByOpCompanyId(int opCompanyId);

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="topNum">top数量(小于等于0取所有)</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        IList<Model.CompanyStructure.CompanyDetailInfo> GetAllCompany(int topNum,
                                                                      Model.CompanyStructure.QueryNewCompany queryModel);
        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        IList<Model.CompanyStructure.CompanyAndUserInfo> GetAllCompany(int pageSize, int pageIndex, ref int recordCount,
                                                                       Model.CompanyStructure.QueryNewCompany queryModel);
        /// <summary>
        /// 获取订单短信接收者信息业务实体
        /// </summary>
        /// <param name="dstCompanyId">接收者公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo GetOrderSmsDstInfo(string dstCompanyId);

        /// <summary>
        /// 根据传入的参数实体计算公司的资料完整度
        /// </summary>
        /// <param name="model">公司信息实体</param>
        /// <returns>返回资料完整度</returns>
        decimal ComputeCompanyInfoFull(Model.CompanyStructure.CompanyArchiveInfo model);

        /// <summary>
        /// 设置公司网店的点击量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool SetShopClickNum(string companyId);
    }
}
