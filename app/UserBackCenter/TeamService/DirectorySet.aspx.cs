using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.BLL.SystemStructure;
using EyouSoft.Model.SystemStructure;
namespace UserBackCenter.TeamService
{   
    /// <summary>
    /// 页面功能：组团服务设置目录
    /// 开发人：xuty 开发时间：2010-6-24
    /// ------------------------------------
    /// 修改人：张新兵，修改时间：20110413
    /// 修改内容：因为系统整合的数据，会出现有些用户的省份城市数据为空的情况，所以加入了 省份城市不为空的判断
    /// </summary>
    public partial class DirectorySet : BackPage
    {  
        protected int totalCompany;//加盟批发商总数
        protected int setCompanyNum;//已设置的批发商数
        protected int nowItemIndex=0;//当前绑定到第几条区域
        protected int recordCount = 0;//当前区域类型下的区域数
        protected string shortArea = "";
        protected string longArea = "";
        protected string exitArea = "";
        protected bool haveUpdate = true;
        protected EyouSoft.IBLL.SystemStructure.ISysCity sysCityBll;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyFavor favorBll;//公司收藏
        protected IList<EyouSoft.Model.SystemStructure.SysCityArea> sysAreaList;//线路区域集合
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.组团_我的收藏管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_我的收藏管理, TravelPermission.组团_我的收藏管理))
            {
                haveUpdate = false;
            }

            favorBll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            if (method == "sel")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有改权限！");
                    return;
                }
                SelCompany();//设置收藏
                return;
            }
            else
            {
                if (method == "nosel")
                {
                    if (!haveUpdate)
                    {
                        Utils.ResponseMeg(false, "对不起，你没有改权限！");
                        return;
                    }
                    CancelComapny();//取消收藏
                    return;
                }
            }
            BindArea();//绑定线路区域
            sysAreaList = null;
        }

        #region 设为我的收藏
        protected void SelCompany()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            int areaId = Utils.GetInt(Request.QueryString["areaid"],0);
            string companyId=Utils.GetQueryStringValue("companyid");
            if (areaId!=0 && companyId != "")
            {
                EyouSoft.Model.CompanyStructure.CompanyFavor favorModle = new EyouSoft.Model.CompanyStructure.CompanyFavor();
                favorModle.AreaId = areaId;
                favorModle.CompanyId = SiteUserInfo.CompanyID;
                favorModle.FavorCompanyId = companyId;
                if (EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance().SaveCompanyFavor(favorModle))
                {
                    Utils.ResponseMeg(true, "收藏成功!");
                }
                else
                {
                    Utils.ResponseMegError();
                }
                //获取收藏的公司数
                setCompanyNum = favorBll.GetAllFavorCount(SiteUserInfo.CompanyID);
            }
        }
        #endregion

        #region 取消收藏
        protected void CancelComapny()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string companyId=Utils.GetQueryStringValue("companyid");
            if (companyId != "")
            {
                favorBll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
                if (favorBll.Delete(SiteUserInfo.CompanyID,companyId))
                {
                    Utils.ResponseMeg(true, "取消成功!");
                }
                else
                {
                    Utils.ResponseMegError();
                }
            }
            //获取收藏的公司数
            setCompanyNum = favorBll.GetAllFavorCount(SiteUserInfo.CompanyID);
        }
        #endregion 

        #region 绑定线路区域
        protected void BindArea()
        {
            sysCityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            EyouSoft.Model.SystemStructure.SysCity city = sysCityBll.GetSysCityModel(SiteUserInfo.CityId);
            if (city!=null&&city.CityAreaControls != null)
            {
                //获取国内长线区域
                sysAreaList = city.CityAreaControls.Where(i => i.RouteType == AreaType.国内长线).ToList();
                recordCount = sysAreaList.Count;
                if (sysAreaList.Count > 0)
                {
                    ds_rpt_home1.DataSource = sysAreaList;
                    ds_rpt_home1.DataBind();
                }
                else
                {
                    longArea = "<tr><td class='noarea'>暂无线路区域信息！</td><tr>";
                }
                //获取国内短线区域
                sysAreaList = city.CityAreaControls.Where(i => i.RouteType == AreaType.国内短线).ToList();
                recordCount = sysAreaList.Count;
                if (sysAreaList.Count > 0)
                {
                    ds_rpt_home2.DataSource = sysAreaList;
                    ds_rpt_home2.DataBind();
                }
                else
                {
                    shortArea = "<tr><td class='noarea'>暂无线路区域信息！</td><tr>";
                }
                //获取国际线区域
                sysAreaList = city.CityAreaControls.Where(i => i.RouteType == AreaType.国际线).ToList();
                recordCount = sysAreaList.Count;
                if (sysAreaList.Count > 0)
                {
                    ds_rpt_abroad1.DataSource = sysAreaList;
                    ds_rpt_abroad1.DataBind();
                }
                else
                {
                    exitArea = "<tr><td class='noarea'>暂无线路区域信息！</td><tr>";
                }
            }

            //初始化大平台公司总数，以及当前用户收藏的公司数量
            EyouSoft.IBLL.SystemStructure.ISummaryCount summaryBll=EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance();
            totalCompany = summaryBll.GetSummary().TravelAgency + summaryBll.GetSummary().TravelAgencyVirtual;//获取平台公司总数
            setCompanyNum = favorBll.GetAllFavorCount(SiteUserInfo.CompanyID); //获取收藏的公司数
        }
        #endregion

        #region 绑定线路区域时项是否换行
        protected string GetItem()
        {
            StringBuilder strBuilder = new StringBuilder();
            if (nowItemIndex % 2 == 0)//奇数项(因为第一项nowItemIndex为0)
            {
                strBuilder.Append("<td width='5'></td>");
                nowItemIndex++;
            }
            else//偶数项换行
            {
              strBuilder.Append("</tr><tr><td colspan='8' height='5'  width='95'></td></tr><tr>");
              nowItemIndex++;
            }
            if (nowItemIndex == recordCount)
            {
                if (recordCount % 2 == 1)//如果是最后一条数据如果总数是奇数则再添加一列
                {
                    strBuilder.Append("<td colspan='8' height='5'  width='95'></td>");
                }
                strBuilder.Append("</tr>");
                nowItemIndex = 0;
            }
            return strBuilder.ToString();
        }
        #endregion

        #region 返回线路区域最后一行时
        protected string GetFooterItem()
        {
            if (recordCount == 0)
            {
                return "</tr>";
            }
            else
                return "";
        }
        #endregion

    }
  
}
