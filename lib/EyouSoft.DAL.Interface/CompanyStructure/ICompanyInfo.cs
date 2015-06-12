using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 公司信息数据访问接口层
    /// </summary>
    /// 创建人：张志瑜 2010-05-31
    /// -------------------------
    /// 修改人：鲁功源 2011-04-06
    /// 内容：新增方法GetTopNumNewCompanys(int TopNum,bool? IsHighShop)
    /// ------------------------
    /// 修改人：郑付杰 2011-12-23
    /// 内容：新增方法 GetNewComapnyScenic(int topNum,EyouSoft.Model.CompanyStructure.CompanyType companyType);
    public interface ICompanyInfo
    {
        /// <summary>
        /// 添加公司(添加公司后将自动生成公司基础信息,公司总帐号,总帐号的MQ号)
        /// </summary>
        /// <param name="model">公司实体</param>
        /// <returns></returns>
        EyouSoft.Model.ResultStructure.UserResultInfo Add(ref EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        bool AddTest();
        /// <summary>
        /// 运营后台修改公司资料,该方法会设置分站,线路区域信息(若不修改密码,请设置密码字段为空)
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        bool Update(ref EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// 高级网店修改公司档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        bool UpdateArchive(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// 单位信息管理,修改自己公司的档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        bool UpdateSelf(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model);
        /// <summary>
        /// <summary>
        /// 真实删除公司
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        bool Delete(params string[] companyIdList);
        /// <summary>
        /// 移除公司(即虚拟删除公司)
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        bool Remove(params string[] companyIdList);
        /// <summary>
        /// 获取公司明细信息实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetModel(string companyid);
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
        /// 外部客户,指定单位类型,获取已审核通过的单位列表(批发商单位可按销售省份,销售城市进行查询/组团社,地接社...单位可按注册所在地的省份,城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="companyType">公司类型</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, EyouSoft.Model.CompanyStructure.CompanyType companyType, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 运营后台获取未审核以及审核通过的单位明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="checkState">审核状态</param>
        /// <param name="query">查询条件实体</param>
        /// <param name="sysUserManageArea">运营后台系统用户所能管理的区域实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetList(EyouSoft.Model.ResultStructure.CheckState checkState, EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, EyouSoft.Model.SystemStructure.QueryParamsSysUserArea sysUserManageArea, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取批发商单位列表基本信息
        /// </summary>
        /// <param name="companyIdList">公司ID集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetList(IList<string> companyIdList);
        /// <summary>
        /// 获取批发商单位列表图文信息
        /// </summary>
        /// <param name="companyIdList">公司ID集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> GetListPicTxt(IList<string> companyIdList);
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
        /// 获得目的地地接社城市列表[只返回省份ID]
        /// </summary>
        /// <returns></returns>
        IList<int> GetListLocalCity();
        /// <summary>
        /// 设置公司状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isEnabled">true:启用  false:停用</param>
        /// <returns></returns>
        bool SetState(string companyid, bool isEnabled);
        /// <summary>
        /// 通过注册
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        bool PassRegister(string companyId);
        /// <summary>
        /// 审核公司承诺书
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        bool SetCertificateCheck(string companyid, bool isCheck);
        /// <summary>
        /// 审核诚信档案中的证书(营业执照,经营许可证,税务登记证)
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        bool SetCreditCheck(string companyid, bool isCheck);
        ///// <summary>
        ///// 获取公司所拥有的所有分站,线路区域列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <returns></returns>
        //IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> GetCompanySiteAreaList(string companyId);

        /// <summary>
        /// 按有无有效产品获取批发商信息集合(统计分析)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="isValid">true:有有效产品 false:无有效产品</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetCompanysByValidTour(int pageSize, int pageIndex, ref int recordCount
            , bool isValid, int? cityId, int? areaId, string companyName, string userAreas);
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
        Model.CompanyStructure.CompanyDetailInfo GetModel(int opCompanyId);

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="topNum">top数量</param>
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
        /// 设置公司网店的点击量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="number">累计的数量</param>
        /// <returns></returns>
        bool SetShopClickNum(string companyId, int number);
    }
}
