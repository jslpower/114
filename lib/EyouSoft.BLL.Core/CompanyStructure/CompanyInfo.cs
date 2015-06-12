using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 公司信息业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    /// -------------------------
    /// 修改人：鲁功源 2011-04-06
    /// 内容：新增方法GetTopNumNewCompanys(int TopNum,bool? IsHighShop)
    /// ------------------------
    /// 修改人：郑付杰 2011-12-23
    /// 内容：新增方法 GetNewComapnyScenic(int topNum,EyouSoft.Model.CompanyStructure.CompanyType companyType);
    public class CompanyInfo : EyouSoft.IBLL.CompanyStructure.ICompanyInfo
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyInfo>();
        private readonly EyouSoft.IDAL.TicketStructure.ITicketWholesalersInfo idalTicketProvider = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketWholesalersInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyInfo CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyInfo>();
            }
            return op;
        }

        #region 成员方法
        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="model">公司实体</param>
        /// <param name="otherCityText">其他销售城市</param>
        /// <returns></returns>
        public EyouSoft.Model.ResultStructure.UserResultInfo Add(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model, string otherCityText)
        {
            /*
             * 添加公司所做的操作:
             * 1.添加公司基本资料
             * 2.添加公司附件信息(不再写入)
             * 3.添加默认的常规团价格等级
             * 4.添加默认的团队状态
             * 5.创建管理员角色
             * 6.添加MQ用户
             * 7.添加平台用户
             * 8.公司设置(不再写入)
             * 9.公司、用户区域关系(不再写入)
             * 10.添加诚信体系基础数据
             * 11.写入公司身份信息
             * 12.写入公司常用出港城市
             * 13.写入MQ客服号
            */
            EyouSoft.Model.ResultStructure.UserResultInfo result = EyouSoft.Model.ResultStructure.UserResultInfo.Error;

            if (model == null || string.IsNullOrEmpty(model.AdminAccount.UserName) || string.IsNullOrEmpty(model.ContactInfo.Email))
            {
                return result;
            }

            model.AdminAccount.UserName = model.AdminAccount.UserName.Trim();  //去除用户名空格
            model.ContactInfo.Email = model.ContactInfo.Email.Trim();   //去除email空格
            //设置所有的密码
            model.AdminAccount.PassWordInfo = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(model.AdminAccount.PassWordInfo.NoEncryptPassword);

            #region 2011-12-20  线路改版新加

            //计算公司信息完整度
            model.InfoFull = ComputeCompanyInfoFull(model);

            #endregion

            result = idal.Add(ref model);

            if (result != EyouSoft.Model.ResultStructure.UserResultInfo.Succeed)
            {
                return result;
            }

            bool singleStepResult = true;

            //其它销售城市
            if (!string.IsNullOrEmpty(otherCityText) && !string.IsNullOrEmpty(otherCityText.Trim()))
            {
                singleStepResult = EyouSoft.BLL.CompanyStructure.CompanyUnCheckedCity.CreateInstance().AddSaleCity(model.ID, otherCityText);

                if (!singleStepResult)
                {
                    return EyouSoft.Model.ResultStructure.UserResultInfo.Error;
                }
            }

            //供应商扩展信息
            if (model.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商))
            {
                if (model.TicketSupplierInfo == null)
                {
                    return EyouSoft.Model.ResultStructure.UserResultInfo.Error;
                }

                model.TicketSupplierInfo.CompanyId = model.ID;
                model.TicketSupplierInfo.ContactName = model.ContactInfo.ContactName;
                model.TicketSupplierInfo.ContactTel = model.ContactInfo.Tel;

                singleStepResult = idalTicketProvider.Add(model.TicketSupplierInfo);

                if (!singleStepResult)
                {
                    return EyouSoft.Model.ResultStructure.UserResultInfo.Error;
                }
            }


            if (result == EyouSoft.Model.ResultStructure.UserResultInfo.Succeed)
            {
                //新增MQ的默认客服,不放入事务中
                try
                {
                    EyouSoft.BLL.MQStructure.IMServiceMQ.CreateInstance().InsertFriendServiceMQ(Convert.ToInt32(model.ContactInfo.MQ), model.ProvinceId);
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 运营后台修改公司资料,该方法会设置分站,线路区域信息(若不修改密码,请设置密码字段为空,若有修改密码,则已同步修改了mq,机票等所有的相关密码)
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            bool isTrue = false;
            if (model == null)
                return isTrue;

            if (string.IsNullOrEmpty(model.ContactInfo.Email)) model.ContactInfo.Email = string.Empty;
            model.ContactInfo.Email = model.ContactInfo.Email.Trim();  //去除email空格
            //设置所有的密码
            model.AdminAccount.PassWordInfo = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(model.AdminAccount.PassWordInfo.NoEncryptPassword);

            #region 2011-12-20  线路改版新加

            //计算公司信息完整度
            model.InfoFull = ComputeCompanyInfoFull(model);

            #endregion

            isTrue = idal.Update(ref model);

            if (isTrue)
            {
                //若密码不为空,则要修改机票的密码
                //if (!string.IsNullOrEmpty(model.AdminAccount.PassWordInfo.NoEncryptPassword))
                //    EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().UpdateTicketUserPwd(model.AdminAccount.ID, model.AdminAccount.PassWordInfo.MD5Password);
                this.RemoveCompanyCityCache(model.ID);
            }

            #region 同步修改其他平台公司信息  zwc  2011-04-07

            if (isTrue)
                this.SyncUpdateCompanyInfo(model.ID, model.AdminAccount.ID);

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 高级网店修改公司档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public bool UpdateArchive(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            if (model == null)
                return false;

            #region 2011-12-20  线路改版新加

            //计算公司信息完整度
            model.InfoFull = ComputeCompanyInfoFull(model);

            #endregion

            bool isTrue = idal.UpdateArchive(model);

            #region 同步修改其他平台公司信息  zwc  2011-04-07

            if (isTrue)
                this.SyncUpdateCompanyInfo(model.ID, null);

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 单位信息管理,修改自己公司的档案资料(不修改销售城市,线路区域)
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public bool UpdateSelf(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            if (model == null)
                return false;

            #region 2011-12-20  线路改版新加

            //计算公司信息完整度
            model.InfoFull = ComputeCompanyInfoFull(model);

            #endregion

            bool isTrue = idal.UpdateSelf(model);

            #region 同步修改其他平台公司信息  zwc  2011-04-07

            if (isTrue)
                this.SyncUpdateCompanyInfo(model.ID, null);

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 真实删除公司
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        public bool Delete(params string[] companyIdList)
        {
            bool isTrue = false;
            if (companyIdList == null || companyIdList.Length == 0)
                return isTrue;
            //isTrue = idal.Delete(companyIdList);
            //if (isTrue)
            //{
            //    this.RemoveCompanyCityCache(companyIdList);
            //    this.RemoveCompanyStateCache(companyIdList);
            //}
            isTrue = idal.Delete(companyIdList);

            #region 同步删除其他平台公司信息 zwc  2011-04-07

            //if (isTrue)
            //{
            //    BLL.SyncStructure.SyncCompany.SyncDeleteCompany(companyIdList);
            //}

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 移除公司(即虚拟删除公司)
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        public bool Remove(params string[] companyIdList)
        {
            bool isTrue = false;
            if (companyIdList == null || companyIdList.Length == 0)
                return isTrue;
            isTrue = idal.Remove(companyIdList);
            //if (isTrue)
            //{
            //    this.RemoveCompanyCityCache(companyIdList);
            //    this.RemoveCompanyStateCache(companyIdList);
            //}

            #region 同步删除其他平台公司信息 zwc  2011-04-07

            //if (isTrue)
            //{
            //    BLL.SyncStructure.SyncCompany.SyncDeleteCompany(companyIdList);
            //}

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 获取公司明细信息实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetModel(string companyid)
        {
            if (string.IsNullOrEmpty(companyid))
                return null;
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = idal.GetModel(companyid);
            if (model != null)
            {
                //获得公司的销售城市,线路区域
                model.SaleCity = (List<EyouSoft.Model.SystemStructure.CityBase>)EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanySaleCity(companyid);
                model.Area = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(companyid);

                //获得公司附件信息
                model.AttachInfo = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(companyid);
            }
            return model;
        }
        /// <summary>
        /// 根据oPUserID获得公司以及当前用户明细信息实体类
        /// </summary>
        /// <param name="oPUserID">用户OPUserID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyAndUserInfo GetModelByOPUserID(int oPUserID)
        {
            if (oPUserID <= 0)
                return null;
            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo modelAll = null;
            string companyid = "";
            EyouSoft.Model.CompanyStructure.CompanyUser user = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModelByOPUserID(oPUserID);
            if (user != null)
            {
                modelAll = new EyouSoft.Model.CompanyStructure.CompanyAndUserInfo();
                companyid = user.CompanyID;
                modelAll.User = user;
            }
            user = null;
            if (string.IsNullOrEmpty(companyid))
                return null;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo company = idal.GetModel(companyid);
            if (company != null)
            {
                ////获得公司附件信息
                company.AttachInfo = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(companyid);
                modelAll.Company = company;
            }
            company = null;

            return modelAll;
        }

        /// <summary>
        /// 根据用户MQID获得公司以及当前用户明细信息实体类
        /// </summary>
        /// <param name="userMqId">用户MQID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyAndUserInfo GetModel(int userMqId)
        {
            if (userMqId <= 0)
                return null;
            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo modelAll = null;
            string companyid = "";
            EyouSoft.Model.CompanyStructure.CompanyUser user = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(userMqId);
            if (user != null)
            {
                modelAll = new EyouSoft.Model.CompanyStructure.CompanyAndUserInfo();
                companyid = user.CompanyID;
                modelAll.User = user;
            }
            user = null;
            if (string.IsNullOrEmpty(companyid))
                return null;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo company = idal.GetModel(companyid);
            if (company != null)
            {
                ////获得公司的销售城市,线路区域
                //company.SaleCity = (List<EyouSoft.Model.SystemStructure.CityBase>)EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanySaleCity(companyid);
                //company.Area = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(companyid);

                ////获得公司附件信息
                company.AttachInfo = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(companyid);
                modelAll.Company = company;
            }
            company = null;

            return modelAll;
        }
        /// <summary>
        /// 获取公司状态信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyStateMore GetCompanyState(string companyId)
        {
            string cacheName = EyouSoft.CacheTag.Company.CompanyState + companyId;
            EyouSoft.Model.CompanyStructure.CompanyStateMore StateMore = (EyouSoft.Model.CompanyStructure.CompanyStateMore)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);
            if (StateMore == null)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = idal.GetModel(companyId);
                if (model != null && model.StateMore != null)  //插入缓存
                {
                    StateMore = model.StateMore;
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, model.StateMore, DateTime.Now.AddHours(1));
                }
            }
            return StateMore;
        }
        /// <summary>
        /// 运营后台获取未审核的单位[包括旅行社,景区,酒店...]明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetListNoChecked(EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            EyouSoft.Model.SystemStructure.QueryParamsSysUserArea areaQuery = this.GetQueryParamsSysUserArea(1);

            //若无任何查看区域,则表示无查看区域数据的权限,则直接返回null
            if (areaQuery.ManageCity == null || areaQuery.ManageCity.Length == 0)
                return null;

            return idal.GetList(EyouSoft.Model.ResultStructure.CheckState.未审核, query, areaQuery, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 运营后台获取已审核通过的单位[包括旅行社,景区,酒店...]明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetListChecked(EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            EyouSoft.Model.SystemStructure.QueryParamsSysUserArea areaQuery = new EyouSoft.Model.SystemStructure.QueryParamsSysUserArea();
            if (query.IsAuthentication)
            {

                areaQuery = this.GetQueryParamsSysUserArea(2);

                //若无任何查看数据的区域,则表示无查看区域数据的权限,则直接返回null
                if (areaQuery.ManageCity == null || areaQuery.ManageCity.Length == 0)
                    return null;
            }
            else
            {
                //查询所有
                areaQuery = new EyouSoft.Model.SystemStructure.QueryParamsSysUserArea();
                areaQuery.ManageCompanyType = new List<EyouSoft.Model.CompanyStructure.CompanyType>();
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.组团);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.地接);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.景区);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.酒店);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.车队);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.购物店);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商);
                areaQuery.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商);
            }

            return idal.GetList(EyouSoft.Model.ResultStructure.CheckState.已通过, query, areaQuery, pageSize, pageIndex, ref recordCount);
        }

        /// <summary>
        /// 外部客户获取已审核通过的所有旅行社单位[专线/批发商,组团/零售商,地接]列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTravelAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetListTravelAgency(query, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 外部客户获取已审核通过的批发商单位列表(批发商单位可按销售省份,销售城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListRouteAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetListAgency(query, EyouSoft.Model.CompanyStructure.CompanyType.专线, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 外部客户获取已审核通过的组团社单位列表(组团社单位可按注册所在地的省份,城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTourAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetListAgency(query, EyouSoft.Model.CompanyStructure.CompanyType.组团, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取指定销售城市,线路区域下的广告批发商单位名称
        /// </summary>
        /// <param name="query">查询条件实体[城市,线路区域为必填项]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListCityAreaAdvRouteAgencyName(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query)
        {
            var list = GetCityAreaAdvNum(query.CityId, query.AreaId);
            if (list != null && list.Any())
                return idal.GetList(list);

            return null;
        }
        /// <summary>
        /// 获取指定销售城市,线路区域下的广告批发商单位图文信息
        /// </summary>
        /// <param name="query">查询条件实体[城市,线路区域为必填项]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> GetListCityAreaAdvRouteAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query)
        {
            return idal.GetListPicTxt(this.GetCityAreaAdvNum(query.CityId, query.AreaId));
        }

        /// <summary>
        /// 外部客户获取已审核通过的地接社单位列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListLocalAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetListLocalAgency(query, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获得目的地地接社城市列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetListLocalCity()
        {
            string cacheName = EyouSoft.CacheTag.System.LocalCity;
            IList<EyouSoft.Model.SystemStructure.SysProvince> items = (IList<EyouSoft.Model.SystemStructure.SysProvince>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);
            if (items == null)
            {
                items = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList(idal.GetListLocalCity().ToArray());
                if (items == null)
                    items = new List<EyouSoft.Model.SystemStructure.SysProvince>();
                //插入缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, items, DateTime.Now.AddHours((double)1));

            }
            return items;
        }
        /// <summary>
        /// 设置公司状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isEnabled">true:启用  false:停用</param>
        /// <returns></returns>
        public bool SetState(string companyid, bool isEnabled)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(companyid))
                return isTrue;
            isTrue = idal.SetState(companyid, isEnabled);
            if (isTrue)
                this.RemoveCompanyStateCache(companyid);
            return isTrue;
        }
        /// <summary>
        /// 通过注册
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public bool PassRegister(string companyId)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(companyId))
                return isTrue;
            isTrue = idal.PassRegister(companyId);
            if (isTrue)
                this.RemoveCompanyStateCache(companyId);
            return isTrue;
        }
        /// <summary>
        /// 审核公司承诺书
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        public bool SetCertificateCheck(string companyid, bool isCheck)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(companyid))
                return isTrue;
            isTrue = idal.SetCertificateCheck(companyid, isCheck);
            if (isTrue)
            {
                this.RemoveCompanyStateCache(companyid);
                EyouSoft.BLL.CreditSystemStructure.RateScore.CreateInstance().RemoveCache(companyid);
            }
            return isTrue;
        }
        /// <summary>
        /// 审核诚信档案中的证书(营业执照,经营许可证,税务登记证)
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        public bool SetCreditCheck(string companyid, bool isCheck)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(companyid))
                return isTrue;
            isTrue = idal.SetCreditCheck(companyid, isCheck);
            if (isTrue)
            {
                this.RemoveCompanyStateCache(companyid);
                EyouSoft.BLL.CreditSystemStructure.RateScore.CreateInstance().RemoveCache(companyid);
            }
            return isTrue;
        }
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
        public IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetValidTourCompanys(int pageSize, int pageIndex, ref int recordCount
            , int? cityId, int? areaId, string companyName)
        {
            return idal.GetCompanysByValidTour(pageSize, pageIndex, ref recordCount, true, cityId, areaId, companyName, this.GetWebMasterArea());
        }

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
        public IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetInvalidTourCompanys(int pageSize, int pageIndex, ref int recordCount
            , int? cityId, int? areaId, string companyName)
        {
            return idal.GetCompanysByValidTour(pageSize, pageIndex, ref recordCount, false, cityId, areaId, companyName, this.GetWebMasterArea());
        }

        /// <summary>
        /// 根据公司编号获取当期公司用户的登录记录列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总的记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.LogUserLogin> GetLoginList(int pageSize, int pageIndex, ref int recordCount, string CompanyId)
        {
            return this.GetLoginList(pageSize, pageIndex, ref recordCount, CompanyId, null, null);
        }

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
        public IList<EyouSoft.Model.CompanyStructure.LogUserLogin> GetLoginList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, DateTime? startTime, DateTime? finishTime)
        {
            return idal.GetLoginList(pageSize, pageIndex, ref recordCount, CompanyId, startTime, finishTime);
        }

        /// <summary>
        /// 根据公司ID获取某段时间段内该公司的登录次数
        /// </summary>
        /// <param name="CompanyId">公司ID(必须传值)</param>
        /// <param name="StartTime">开始时间(为null不作条件)</param>
        /// <param name="EndTime">结束时间(为null不作条件)</param>
        /// <returns>登录次数</returns>
        public int GetLoginCountByTime(string CompanyId, DateTime? StartTime, DateTime? EndTime)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return 0;

            return idal.GetLoginCountByTime(CompanyId, StartTime, EndTime);
        }

        /// <summary>
        /// 设置公司付费项目状态
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="serviceItem">某项服务项目</param>
        /// <returns></returns>
        public bool SetCompanyPayService(string companyId, EyouSoft.Model.CompanyStructure.CompanyServiceItem serviceItem)
        {
            return idal.SetCompanyPayService(companyId, serviceItem);
        }

        /// <summary>
        /// 申请成为采购商/供应商
        /// </summary>
        /// <param name="companyInfo">公司信息业务实体</param>
        /// <param name="otherCityText">其他销售城市</param>
        /// <returns></returns>
        public bool ToCompany(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo companyInfo, string otherCityText)
        {
            if (companyInfo == null || string.IsNullOrEmpty(companyInfo.ID)) return false;

            bool singleStepResult = true;

            companyInfo.ContactInfo.Email = companyInfo.ContactInfo.Email.Trim();
            companyInfo.AdminAccount.PassWordInfo = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(companyInfo.AdminAccount.PassWordInfo.NoEncryptPassword);

            #region 2011-12-20  线路改版新加

            //计算公司信息完整度
            companyInfo.InfoFull = ComputeCompanyInfoFull(companyInfo);

            #endregion

            singleStepResult = idal.Update(ref companyInfo);

            if (!singleStepResult)
            {
                return false;
            }

            //其它销售城市
            if (!string.IsNullOrEmpty(otherCityText) && !string.IsNullOrEmpty(otherCityText.Trim()))
            {
                singleStepResult = EyouSoft.BLL.CompanyStructure.CompanyUnCheckedCity.CreateInstance().AddSaleCity(companyInfo.ID, otherCityText);

                if (!singleStepResult)
                {
                    return false;
                }
            }

            //供应商扩展信息
            if (companyInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商))
            {
                if (companyInfo.TicketSupplierInfo == null)
                {
                    return false;
                }

                companyInfo.TicketSupplierInfo.CompanyId = companyInfo.ID;
                companyInfo.TicketSupplierInfo.ContactName = companyInfo.ContactInfo.ContactName;
                companyInfo.TicketSupplierInfo.ContactTel = companyInfo.ContactInfo.Tel;

                singleStepResult = idalTicketProvider.Add(companyInfo.TicketSupplierInfo);

                if (!singleStepResult)
                {
                    return false;
                }
            }


            return true;
        }
        /// <summary>
        /// 获取最新加盟的公司列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="IsHighShop">是否高级网店 =null返回所有</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetTopNumNewCompanys(int TopNum, bool? IsHighShop)
        {
            if (TopNum <= 0)
                return null;
            return idal.GetTopNumNewCompanys(TopNum, IsHighShop);
        }

        /// <summary>
        /// 分页获取同行列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总数</param>
        /// <param name="queryEntity">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyTongHang> GetTongHangList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.QueryNewCompany queryEntity)
        {
            return idal.GetTongHangList(pageSize, pageIndex, ref recordCount, queryEntity);
        }

        /// <summary>
        /// 最新加盟公司
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <param name="businessProperties">企业性质</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetNewComapnyScenic(int topNum
            , EyouSoft.Model.CompanyStructure.BusinessProperties businessProperties)
        {
            topNum = topNum < 1 ? 10 : topNum;
            return idal.GetNewComapnyScenic(topNum, businessProperties);
        }

        /// <summary>
        /// 根据公司自增编号获取公司明细信息实体
        /// </summary>
        /// <param name="opCompanyId">公司自增编号</param>
        /// <returns></returns>
        public Model.CompanyStructure.CompanyDetailInfo GetModelByOpCompanyId(int opCompanyId)
        {
            if (opCompanyId <= 0)
                return null;
            Model.CompanyStructure.CompanyDetailInfo model = idal.GetModel(opCompanyId);
            if (model != null)
            {
                //获得公司的销售城市,线路区域
                model.SaleCity = (List<Model.SystemStructure.CityBase>)CompanyCity.CreateInstance().GetCompanySaleCity(model.ID);
                model.Area = (List<Model.SystemStructure.AreaBase>)CompanyArea.CreateInstance().GetCompanyArea(model.ID);

                //获得公司附件信息
                model.AttachInfo = CompanyAttachInfo.CreateInstance().GetModel(model.ID);
            }
            return model;
        }

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="topNum">top数量(小于等于0取所有)</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        public IList<Model.CompanyStructure.CompanyDetailInfo> GetAllCompany(int topNum,
                                                                      Model.CompanyStructure.QueryNewCompany queryModel)
        {
            return idal.GetAllCompany(topNum, queryModel);
        }

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        public IList<Model.CompanyStructure.CompanyAndUserInfo> GetAllCompany(int pageSize, int pageIndex, ref int recordCount,
                                                                       Model.CompanyStructure.QueryNewCompany queryModel)
        {
            return idal.GetAllCompany(pageSize, pageIndex, ref recordCount, queryModel);
        }

        /// <summary>
        /// 获取订单短信接收者信息业务实体
        /// </summary>
        /// <param name="dstCompanyId">接收者公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo GetOrderSmsDstInfo(string dstCompanyId)
        {
            if (string.IsNullOrEmpty(dstCompanyId)) return null;

            return idal.GetOrderSmsDstInfo(dstCompanyId);
        }

        /// <summary>
        /// 根据传入的参数实体计算公司的资料完整度
        /// </summary>
        /// <param name="model">公司信息实体</param>
        /// <returns>返回资料完整度</returns>
        public decimal ComputeCompanyInfoFull(Model.CompanyStructure.CompanyArchiveInfo model)
        {
            decimal infoFull = 0;
            if (model == null)
                return infoFull;

            #region 基本属性   55

            //公司简称   3
            if (!string.IsNullOrEmpty(model.Introduction))
                infoFull += 0.03M;
            //公司品牌名   3
            if (!string.IsNullOrEmpty(model.CompanyBrand))
                infoFull += 0.03M;
            //公司网站   2
            if (!string.IsNullOrEmpty(model.WebSite))
                infoFull += 0.02M;
            //地区(到县一级才有)   2
            if (model.CountyId > 0)
                infoFull += 0.02M;
            //办公地址   3
            if (!string.IsNullOrEmpty(model.CompanyAddress))
                infoFull += 0.03M;
            //地图标注   5
            if (model.Latitude > 0 && model.Longitude > 0)
                infoFull += 0.05M;
            //公司介绍(50字以上)   16
            if (!string.IsNullOrEmpty(model.Remark) && model.Remark.Length >= 50)
                infoFull += 0.16M;
            //公司规模   3
            if ((int)model.Scale > 0)
                infoFull += 0.03M;
            //许可证号   5
            if (!string.IsNullOrEmpty(model.License))
                infoFull += 0.05M;
            //同业联系方式   5
            if (!string.IsNullOrEmpty(model.PeerContact))
                infoFull += 0.05M;
            //支付宝   5
            if (!string.IsNullOrEmpty(model.AlipayAccount))
                infoFull += 0.05M;
            //银行账户(有1个就3)     3  
            if (model.BankAccounts != null && model.BankAccounts.Any())
                infoFull += 0.03M;

            #endregion

            #region 联系人属性    16

            if (model.ContactInfo != null)
            {
                //客服电话   5
                if (!string.IsNullOrEmpty(model.ContactInfo.Tel))
                    infoFull += 0.05M;
                //客服传真   3
                if (!string.IsNullOrEmpty(model.ContactInfo.Fax))
                    infoFull += 0.03M;
                //客服邮箱Email   3
                if (!string.IsNullOrEmpty(model.ContactInfo.Email))
                    infoFull += 0.03M;
                //联系手机   5
                if (!string.IsNullOrEmpty(model.ContactInfo.Mobile))
                    infoFull += 0.05M;
            }

            #endregion

            #region 附件 图片属性   29

            if (model.AttachInfo != null)
            {
                //logo照片   5
                if (model.AttachInfo.CompanyLogo != null &&
                    (!string.IsNullOrEmpty(model.AttachInfo.CompanyLogo.ImagePath)
                        || !string.IsNullOrEmpty(model.AttachInfo.CompanyLogo.ImagePath)))
                    infoFull += 0.05M;

                //证书管理   3*5 = 15
                if (model.AttachInfo.BusinessCertif != null)
                {
                    //经营许可证   3
                    if (!string.IsNullOrEmpty(model.AttachInfo.BusinessCertif.BusinessCertImg))
                        infoFull += 0.03M;
                    //营业执照   3
                    if (!string.IsNullOrEmpty(model.AttachInfo.BusinessCertif.LicenceImg))
                        infoFull += 0.03M;
                    //负责人身份证   3
                    if (!string.IsNullOrEmpty(model.AttachInfo.BusinessCertif.PersonCardImg))
                        infoFull += 0.03M;
                    //税务登记证   3
                    if (!string.IsNullOrEmpty(model.AttachInfo.BusinessCertif.TaxRegImg))
                        infoFull += 0.03M;
                    //授权证书   3
                    if (!string.IsNullOrEmpty(model.AttachInfo.BusinessCertif.WarrantImg))
                        infoFull += 0.03M;
                }

                //公司图片     3*3 = 9
                if (model.AttachInfo.CompanyPublicityPhoto != null)
                {
                    infoFull += (model.AttachInfo.CompanyPublicityPhoto.Count * 0.03M);
                }
            }

            #endregion

            model.InfoFull = infoFull;

            return infoFull;
        }

        /// <summary>
        /// 设置公司网店的点击量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool SetShopClickNum(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return false;

            return idal.SetShopClickNum(companyId, 1);
        }

        #endregion 成员方法

        #region 私有方法
        /// <summary>
        /// 移除单位状态信息缓存
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        private void RemoveCompanyStateCache(params string[] companyIdList)
        {
            if (companyIdList == null || companyIdList.Length == 0)
                return;
            foreach (string id in companyIdList)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.CompanyState + id);
        }
        /// <summary>
        /// 移除公司销售城市缓存
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        private void RemoveCompanyCityCache(params string[] companyIdList)
        {
            if (companyIdList == null || companyIdList.Length == 0)
                return;
            foreach (string id in companyIdList)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.CompanyCity + id);
        }
        /// <summary>
        /// 移除目的地地接社城市列表缓存
        /// </summary>
        private void RemoveLocalCityCache()
        {
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.LocalCity);
        }
        /// <summary>
        /// 获取当前用户(运营后台)用户分管的区域范围 多个用","间隔
        /// </summary>
        /// <returns></returns>
        private string GetWebMasterArea()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentWebMasterArea();
        }
        /// <summary>
        /// 获得指定城市,指定线路区域下显示的广告批发商id集合
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        private IList<string> GetCityAreaAdvNum(int cityId, int areaId)
        {
            if (cityId == 0 || areaId == 0)
                return null;
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityId);
            if (cityModel == null || cityModel.AreaAdvNum == null || cityModel.AreaAdvNum.Count == 0 || !cityModel.AreaAdvNum.ContainsKey(areaId))
                return null;
            IList<string> list = cityModel.AreaAdvNum[areaId];
            cityModel = null;

            return list;
        }
        /// <summary>
        /// 获得当前所登录系统的用户所管理的区域查询条件实体类
        /// </summary>
        /// <param name="typeId">类型1:会员审核2:会员管理</param>
        /// <returns></returns>
        private EyouSoft.Model.SystemStructure.QueryParamsSysUserArea GetQueryParamsSysUserArea(int typeId)
        {
            EyouSoft.Model.SystemStructure.QueryParamsSysUserArea model = new EyouSoft.Model.SystemStructure.QueryParamsSysUserArea();
            EyouSoft.SSOComponent.Entity.MasterUserInfo masterUser = new EyouSoft.Security.Membership.UserProvider().GetMaster();
            if (masterUser != null)
            {
                model.ManageCity = masterUser.AreaId;
                /*
                 * 判断审核的权限
                 * 组团社审核=26
                 * 专线商审核=27
                 * 地接社审核=28
                 * 景区审核=29
                 * 酒店审核=30
                 * 车队审核=31
                 * 旅游用品店审核=32
                 * 购物店审核=33
                 * 机票供应商=108
                */

                if (masterUser.PermissionList != null && masterUser.PermissionList.Length > 0)
                {
                    model.ManageCompanyType = new List<EyouSoft.Model.CompanyStructure.CompanyType>();
                    if (typeId == 1)
                    {
                        if (masterUser.PermissionList.Contains(26))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.组团);
                        if (masterUser.PermissionList.Contains(27))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                        if (masterUser.PermissionList.Contains(28))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.地接);
                        if (masterUser.PermissionList.Contains(29))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.景区);
                        if (masterUser.PermissionList.Contains(30))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.酒店);
                        if (masterUser.PermissionList.Contains(31))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.车队);
                        if (masterUser.PermissionList.Contains(32))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店);
                        if (masterUser.PermissionList.Contains(33))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.购物店);
                        if (masterUser.PermissionList.Contains(108))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商);
                        if (masterUser.PermissionList.Contains(116))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商);
                        if (masterUser.PermissionList.Contains(135))
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛);
                    }
                    else if (typeId == 2)
                    {
                        if (masterUser.PermissionList.Contains(119))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.组团);
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.地接);
                        }
                        if (masterUser.PermissionList.Contains(120))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.景区);
                        }
                        if (masterUser.PermissionList.Contains(121))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.酒店);
                        }
                        if (masterUser.PermissionList.Contains(122))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.车队);
                        }
                        if (masterUser.PermissionList.Contains(123))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店);
                        }
                        if (masterUser.PermissionList.Contains(124))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.购物店);
                        }
                        if (masterUser.PermissionList.Contains(125))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商);
                        }
                        if (masterUser.PermissionList.Contains(126))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商);
                        }
                        if (masterUser.PermissionList.Contains(138))
                        {
                            model.ManageCompanyType.Add(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛);
                        }
                    }
                }
            }
            masterUser = null;
            return model;
        }

        /// <summary>
        /// 同步修改其他平台公司信息  zwc  2011-04-07
        /// </summary>
        /// <param name="PlamCompanyId">公司Id</param>
        /// <param name="PlamUserId">用户Id，不传值不修改</param>
        private void SyncUpdateCompanyInfo(string PlamCompanyId, string PlamUserId)
        {
            BLL.SyncStructure.SyncCompany.SyncUpdateCompany(PlamCompanyId, PlamUserId);
        }

        #endregion 私有方法

        #region 不要的代码
        ///// <summary>
        ///// 获取审核通过的批发商单位和经营单位明细信息列表(仅不包含银行和附件信息)
        ///// </summary>
        ///// <param name="ProvinceId">省份ID,若=0,则不做为查询条件</param>
        ///// <param name="SiteId">分站ID,若=0,则不做为查询条件</param>
        ///// <param name="companyName">公司名称,若为null或空,则不做为查询条件</param>
        ///// <param name="contactName">公司负责人,若为null或空,则不做为查询条件</param>
        ///// <param name="companyBrand">公司品牌名称,若为null或空,则不做为查询条件</param>
        ///// <param name="pageSize">分页大小</param>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.CompanyStructure.CompanyBasicInfo> GetListCompanyAndUnit(int ProvinceId, int SiteId, string companyName, string contactName, string companyBrand, int pageSize, int pageIndex, ref int recordCount)
        //{
        //    return idal.GetListCompanyAndUnit(ProvinceId, SiteId, companyName, contactName, companyBrand, pageSize, pageIndex, ref recordCount);
        //}
        ///// <summary>
        ///// 获取公司所拥有的所有分站,线路区域列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> GetCompanySiteAreaList(string companyId)
        //{
        //    //首先获取缓存信息
        //    IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> items = (IList<EyouSoft.Model.SystemStructure.SiteAreaInfo>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.Company.CompanyInfo + companyId);
        //    if (items == null || items.Count == 0)
        //    {
        //        items = idal.GetCompanySiteAreaList(companyId);
        //        if (items != null && items.Count > 0)
        //            EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.Company.CompanyInfo + companyId, items, DateTime.Now.AddDays(1));
        //    }
        //    return items;
        //}
        ///// <summary>
        ///// 获取公司所拥有的分站列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanySiteList(string companyId)
        //{
        //    IList<EyouSoft.Model.SystemStructure.CityBase> siteItems = new List<EyouSoft.Model.SystemStructure.CityBase>();
        //    //获得该公司所有的分站,线路区域列表信息
        //    IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> siteAreaItems = this.GetCompanySiteAreaList(companyId);
        //    if (siteAreaItems != null && siteAreaItems.Count > 0)
        //    {
        //        System.Collections.Generic.IEnumerator<EyouSoft.Model.SystemStructure.SiteAreaInfo> ienumerator = siteAreaItems.GetEnumerator();
        //        while (ienumerator.MoveNext())
        //        {
        //            if (ienumerator != null && ienumerator.Current != null)
        //            {
        //                siteItems.Add(ienumerator.Current);
        //            }
        //        }
        //    }
        //    return siteItems;
        //}
        ///// <summary>
        ///// 获取当前省份,分站下公司所拥有的省份,分站,线路区域列表信息
        ///// </summary>
        ///// <param name="companyId">公司ID</param>
        ///// <param name="currentProvinceId">当前省份ID,若=0,则不做为查询条件</param>
        ///// <param name="currentSiteId">当前分站ID,若=0,则不做为查询条件</param>
        ///// <returns></returns>
        //public EyouSoft.Model.SystemStructure.ProvinceSiteAreaList GetCompanyProvinceSiteAreaList(string companyId, int currentProvinceId, int currentSiteId)
        //{
        //    EyouSoft.Model.SystemStructure.ProvinceSiteAreaList list = new EyouSoft.Model.SystemStructure.ProvinceSiteAreaList();
        //    list.CurrentProvinceId = currentProvinceId;
        //    list.CurrentSiteId = currentSiteId;
        //    //获得该公司所有的分站,线路区域列表信息
        //    IList<EyouSoft.Model.SystemStructure.SiteAreaInfo> siteAreaItems = this.GetCompanySiteAreaList(companyId);
        //    if (siteAreaItems != null && siteAreaItems.Count > 0)
        //    {
        //        if (list.ProvinceList == null)
        //            list.ProvinceList = new List<EyouSoft.Model.SystemStructure.ProvinceBase>();
        //        if (list.SiteList == null)
        //            list.SiteList = new List<EyouSoft.Model.SystemStructure.CityBase>();                
        //        System.Collections.Generic.IEnumerator<EyouSoft.Model.SystemStructure.SiteAreaInfo> ienumerator = siteAreaItems.GetEnumerator();
        //        while (ienumerator.MoveNext())
        //        {
        //            if (ienumerator != null && ienumerator.Current != null)
        //            {
        //                //获得省份信息                        
        //                EyouSoft.Model.SystemStructure.ProvinceBase province = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceBaseModel(ienumerator.Current.ProvinceId);
        //                //判断省份是否已在列表中,若不存在,则进行添加
        //                if (list.ProvinceList != null && list.ProvinceList.Count > 0 && !list.ProvinceList.Contains(province))
        //                    list.ProvinceList.Add(province);
        //                //添加当前省份对应的分站,以及对应的线路区域
        //                if (ienumerator.Current.ProvinceId == list.CurrentProvinceId)  //当前省份
        //                {
        //                    EyouSoft.Model.SystemStructure.CityBase site = new EyouSoft.Model.SystemStructure.CityBase();
        //                    site.CityId = ienumerator.Current.CityId;
        //                    site.ProvinceId = ienumerator.Current.ProvinceId;
        //                    site.CityName = ienumerator.Current.CityName;
        //                    list.SiteList.Add(site);
        //                    site = null;

        //                    //是否为当前分站,若为当前分站,则直接将当前分站的线路区域赋值给列表
        //                    if (ienumerator.Current.CityId == list.CurrentSiteId)
        //                    {
        //                        list.AreaList = ienumerator.Current.AreaList;
        //                    }
        //                }                        
        //            }
        //        }
        //    }
        //    return list;
        //}
        #endregion 不要的代码
    }
}
