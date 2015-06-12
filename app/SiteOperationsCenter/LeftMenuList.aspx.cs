using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using EyouSoft.Common;
namespace SiteOperationsCenter.PlatformManagement
{
    public partial class LeftMenuList : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                #region 会员管理
              
                //会员管理栏目
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目))
                {
                    this.divCompanyManage.Visible = false;
                }
                else
                {
                    trRegisterCompany.Visible = CheckMasterGrant(YuYingPermission.注册审核_组团社审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_专线商审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_旅游用品店审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_酒店审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_景区审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_购物店审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_地接社审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_车队审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_机票供应商审核)
                        || CheckMasterGrant(YuYingPermission.注册审核_随便逛逛);

                    trDefaultCompanyManage.Visible = CheckMasterGrant(YuYingPermission.旅行社汇总管理_管理该栏目);
                    trOther1Company.Visible = CheckMasterGrant(YuYingPermission.景区汇总管理_管理该栏目);
                    trOther2Company.Visible = CheckMasterGrant(YuYingPermission.酒店汇总管理_管理该栏目);
                    trOther3Company.Visible = CheckMasterGrant(YuYingPermission.车队汇总管理_管理该栏目);
                    trOther4Company.Visible = CheckMasterGrant(YuYingPermission.旅游用品店汇总管理_管理该栏目);
                    trOther5Company.Visible = CheckMasterGrant(YuYingPermission.机票供应商管理_管理该栏目);
                    trOther6Company.Visible = CheckMasterGrant(YuYingPermission.其他采购商管理_管理该栏目);
                    trOther7Company.Visible = CheckMasterGrant(YuYingPermission.随便逛逛_管理该栏目);
                    if (!trRegisterCompany.Visible && !trDefaultCompanyManage.Visible
                        && !trOther1Company.Visible && !trOther2Company.Visible
                        && !trOther3Company.Visible && !trOther4Company.Visible
                        && !trOther5Company.Visible && !trOther6Company.Visible && !trOther7Company.Visible)
                    {
                        divCompanyManage.Visible = false;
                    }
                }
                #endregion

                #region 新闻中心
                //新闻中心管理栏目
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.新闻中心_管理该栏目))
                {
                    this.divNewsCenter.Visible = false;
                }
                else
                {
                    //添加新闻
                    trOperatorNewsPage.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.新闻中心_新增);
                    trNewsInfoList.Visible = CheckMasterGrant(YuYingPermission.新闻中心_修改)
                        || CheckMasterGrant(YuYingPermission.新闻中心_删除);

                    //新闻中心 栏目 ，当没有添加、修改、删除新闻的权限时则隐藏新闻中心 栏目.
                    if (!trOperatorNewsPage.Visible && !trNewsInfoList.Visible)
                    {
                        divNewsCenter.Visible = false;
                    }
                }
                #endregion

                #region 广告管理
                //同业114广告
                this.trTonye114Adv.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.同业114广告_管理该栏目);
                //MQ广告
                this.trMQAdv.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.MQ广告_管理该栏目);
                //广告管理,没有 上面两个 栏目权限时，则隐藏广告管理栏目
                if (!trTonye114Adv.Visible && !trMQAdv.Visible)
                {
                    this.divAdvMange.Visible = false;
                }
                #endregion

                #region 统计分析
                //统计分析
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_管理该栏目))
                {
                    this.divAnalysisManage.Visible = false;
                }
                else
                {
                    this.trStaMQ.Visible = CheckMasterGrant(YuYingPermission.统计分析_收费MQ到期统计);
                    this.trStaHighShop.Visible = CheckMasterGrant(YuYingPermission.统计分析_高级网店到期统计);
                    this.tr_SMSRemnantStatistics.Visible = CheckMasterGrant(YuYingPermission.统计分析_短信余额统计);
                    this.tr_AgencyActionAnalysis.Visible = CheckMasterGrant(YuYingPermission.统计分析_组团社行为分析);
                    this.trZXAgencyActionAnalysis.Visible = CheckMasterGrant(YuYingPermission.统计分析_专线商行为分析);
                    this.trNoValidZXAgency.Visible = CheckMasterGrant(YuYingPermission.统计分析_无有效产品专线商);
                    this.trValidZXAgency.Visible = CheckMasterGrant(YuYingPermission.统计分析_有有效产品专线商);
                    this.trOnLineAgency.Visible = CheckMasterGrant(YuYingPermission.统计分析_在线组团社);
                    this.trHistoryAgencyLogin.Visible = CheckMasterGrant(YuYingPermission.统计分析_组团社历史登录记录);

                    //机票采购商查询链接
                    this.trPlanerBuyerQuery.Visible = true;

                    if (trStaMQ.Visible == false
                        && trStaHighShop.Visible == false
                        && tr_SMSRemnantStatistics.Visible == false
                        && tr_AgencyActionAnalysis.Visible == false
                        && trZXAgencyActionAnalysis.Visible == false
                        && trNoValidZXAgency.Visible == false
                        && trValidZXAgency.Visible == false
                        && trOnLineAgency.Visible == false
                        && trHistoryAgencyLogin.Visible == false)
                    {
                        divAnalysisManage.Visible = false;
                    }
                }
                #endregion

                #region 同业114产品管理
                bool isHaveMQApplication = true;
                bool isHaveHightShop = true;
                bool ioHaveSMSManage = true;

                //企业MQ申请审核 
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.企业MQ申请审核_管理该栏目))
                {
                    this.trMQApplication.Visible = false;
                    isHaveMQApplication = false;
                }

                //高级网店申请审核 
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.高级网店申请审核_管理该栏目))
                {
                    this.trHightShop.Visible = false;
                    isHaveHightShop = false;
                }
                //短信充值审核_管理 
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.短信充值审核_管理该栏目))
                {
                    this.trSMSManage.Visible = false;
                    ioHaveSMSManage = false;
                }
                ////提醒_登录下面的提醒
                //if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.提醒_登录下面的提醒))
                //{
                //    this.trLoginedRemind.Visible = false;
                //}

                if (CheckMasterGrant(EyouSoft.Common.YuYingPermission.给客户发送MQ消息))
                {
                    this.trSendIMMessage.Visible = true;
                }

                //都没有【企业MQ申请审核】,【高级网店申请审核】,【短信充值审核】,【机票采购商查询】,【给客户发送MQ消息】的权限时，
                //隐藏 同业114产品管理 栏目.
                if (!isHaveMQApplication && !isHaveHightShop && !ioHaveSMSManage && trPlanerBuyerQuery.Visible == false && !this.trSendIMMessage.Visible)
                {
                    this.divtongye114Manage.Visible = false;
                }

                #endregion

                #region 供求管理
                //供求信息
                trSupplyManage.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.供求信息_管理该栏目);
                //嘉宾访谈
                trGuestManage.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.嘉宾访谈_管理该栏目);
                //同业学堂
                trTongyeSchool.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.同业学堂_管理该栏目);
                ////供求首页焦点图
                trSupDemandBannerImg.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.供求信息_供求首页焦点图);
                //供求规则
               this.trSupRule.Visible = CheckMasterGrant(EyouSoft.Common.YuYingPermission.供求信息_供求规则);
                //Q群信息管理
                //this.trQInformationManager.Visible=CheckMasterGrant(EyouSoft.Common.YuYingPermission.

                //供求管理栏目,没有上面三个任何一个栏目的权限时，隐藏 供求管理 栏目
               if (!trSupplyManage.Visible && !trGuestManage.Visible && !trTongyeSchool.Visible && !trSupRule.Visible && !trSupDemandBannerImg.Visible)
               {
                   this.divSupplyManage.Visible = false;
               }
                #endregion

                #region 平台管理
                //平台管理
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.平台管理_管理该栏目))
                {
                    this.divTerraceManage.Visible = false;
                }
                else
                {
                    trBasicInfoManagement.Visible = CheckMasterGrant(YuYingPermission.平台管理_基本信息);
                    trSalesRegionManage.Visible = CheckMasterGrant(YuYingPermission.平台管理_城市管理);
                    trBasicDataMange.Visible = CheckMasterGrant(YuYingPermission.平台管理_基础数据维护);
                    trRouteAreaType.Visible = CheckMasterGrant(YuYingPermission.平台管理_线路区域分类);
                    trUniversalLineManage.Visible = CheckMasterGrant(YuYingPermission.平台管理_通用专线区域);
                    trDataCount.Visible = CheckMasterGrant(YuYingPermission.平台管理_统计数据维护);
                    trPartnersInfo.Visible = CheckMasterGrant(YuYingPermission.平台管理_战略合作伙伴);
                    trLinksInfo.Visible = CheckMasterGrant(YuYingPermission.平台管理_友情链接);

                    //没有平台管理中任何一个栏目权限时，隐藏平台管理栏目
                    if (!trBasicInfoManagement.Visible && !trSalesRegionManage.Visible
                        && !trBasicDataMange.Visible && !trRouteAreaType.Visible
                        && !trUniversalLineManage.Visible && !trDataCount.Visible
                        && !trPartnersInfo.Visible && !trLinksInfo.Visible)
                    {
                        divTerraceManage.Visible = false;
                    }
                }
                #endregion

                #region 用户反馈
                bool isHaveFeedNetWorkMarketing = true;
                bool isHaveFeedHighShop = true;
                bool isHaveFeedMQ = true;
                bool isHaveFeedTonye114 = true;
                bool isHaveFeedCompany = true;
                bool isHaveFeedGuest = true;
                //高级网店反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.高级网店反馈_管理该栏目))
                {
                    this.trFeedHighShop.Visible = false;
                    isHaveFeedHighShop = false;
                }
                //MQ反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.MQ反馈_管理该栏目))
                {
                    this.trFeedMQ.Visible = false;
                    isHaveFeedMQ = false;
                }
                //同业114平台反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.同业114平台反馈_管理该栏目))
                {
                    this.trFeedTonye114.Visible = false;
                    isHaveFeedTonye114 = false;
                }
                //网络营销反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.网络营销反馈_管理栏目))
                {
                    this.trNetWorkMarketing.Visible = false;
                    isHaveFeedNetWorkMarketing = false;
                }
                //旅行社后台反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社后台反馈_管理该栏目))
                {
                    this.trFeedCompany.Visible = false;
                    isHaveFeedCompany = false;
                }
                //嘉宾申请反馈
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.嘉宾申请反馈_管理该栏目))
                {
                    this.trFeedGuest.Visible = false;
                    isHaveFeedGuest = false;
                }
                //没有用户反馈中任何一个栏目权限时，隐藏用户反馈栏目
                if (!isHaveFeedHighShop && !isHaveFeedMQ && !isHaveFeedTonye114 && !isHaveFeedCompany && !isHaveFeedGuest)
                {
                    this.divFeedManage.Visible = false;
                }

                #endregion

                #region 帐户管理
                //帐户管理
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.帐户管理_管理该栏目))
                {
                    this.divAccountManage.Visible = false;
                }
                else
                {
                    trAccountManage.Visible = CheckMasterGrant(YuYingPermission.帐户管理_子帐号管理);
                    trPwdModi.Visible = CheckMasterGrant(YuYingPermission.帐户管理_修改密码);

                    //如果没有账户管理栏目任何一个子栏目权限的，就隐藏帐户管理栏目
                    if (!trAccountManage.Visible && !trPwdModi.Visible)
                    {
                        divAccountManage.Visible = false;
                    }
                }
                #endregion

                #region 广告机票
                trAdvertiseTicket.Visible = CheckMasterGrant(YuYingPermission.广告机票_机票查询_管理该栏目);
                if (!trAdvertiseTicket.Visible)
                {
                    divAdvPlant.Visible = false;
                }
                #endregion

                #region 客户资料
                trCustomerManage.Visible = CheckMasterGrant(YuYingPermission.客户资料_管理该栏目);
                trSysMaintenance.Visible = CheckMasterGrant(YuYingPermission.客户资料_系统维护_管理该栏目);

                if (trCustomerManage.Visible == false && trSysMaintenance.Visible == false)
                {
                    divCustomerManage.Visible = false;
                }
                #endregion

                #region 酒店后台管理
                trHotelOrderSear.Visible = CheckMasterGrant(YuYingPermission.酒店后台管理_订单管理);
                trGroupOrder.Visible = CheckMasterGrant(YuYingPermission.酒店后台管理_团队订单管理);
                trHotelFirstAdd.Visible = CheckMasterGrant(YuYingPermission.酒店后台管理_首页板块数据管理);

                if (!trHotelOrderSear.Visible && !trGroupOrder.Visible && !trHotelFirstAdd.Visible)
                {
                    divHotelManage.Visible = false;
                }
                #endregion

                #region 酒店首页前台管理
                trHotelList.Visible = CheckMasterGrant(YuYingPermission.酒店首页管理_特价酒店);
                if (!trHotelList.Visible)
                {
                    divHotelManage.Visible = false;
                }
                #endregion

                #region 机票管理
                trAirItem.Visible = CheckMasterGrant(YuYingPermission.机票首页管理_特价机票管理);
                trTourTeamApply.Visible = CheckMasterGrant(YuYingPermission.机票首页管理_团队票申请管理);
                if (!trAirItem.Visible && !trTourTeamApply.Visible )
                {
                    this.divAirTickets.Visible = false;
                }
                #endregion
            }

        }
    }
}
