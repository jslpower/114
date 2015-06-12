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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Collections.Generic;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    /// 广告管理：文字广告列表
    /// 功能：查询，删除，排序，新增，修改
    /// 创建人：袁惠
    /// 创建时间： 2010-7-22  
    public partial class WritingAdList :EyouSoft.Common.Control.YunYingPage
    {
        protected int currentPage = 0;
        protected string PagePath = "";
        protected int pageSize = 15;
        protected bool IsUpdateGant = false;
        protected bool IsDeleteGant = false;
        protected bool IsAddGant = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                #region 权限判断
                //查询
                //判断是否有【广告——管理栏目】权限，如果没有改权限，则不能查看改页面
                if (!this.CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目, true);
                    return;
                }
                IsUpdateGant = CheckMasterGrant(YuYingPermission.同业114广告_修改);
                IsDeleteGant = CheckMasterGrant(YuYingPermission.同业114广告_删除);
                IsAddGant = CheckMasterGrant(YuYingPermission.同业114广告_新增);
                #endregion
                InitData();
            }
        }
        private void InitData()
        {
            string position=Request.QueryString["position"];
            if (!string.IsNullOrEmpty(position))
            {
                startPosition.Value = position;
                EyouSoft.Model.AdvStructure.AdvPositionInfo poinfo = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetPositionInfo((EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), position));

                if (poinfo != null)
                {
                    switch (poinfo.DisplayType)
                    {
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.单位图片广告:
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.单位图文广告:
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.单位文字广告:
                            PagePath = "AddUnitAd.aspx";
                            break;
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.图片广告:
                            PagePath = "AddPhotoAd.aspx";
                            break;
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.图文广告:
                            PagePath = "AddPhotoWritingAd.aspx";
                            break;
                        case EyouSoft.Model.AdvStructure.AdvDisplayType.文字广告:
                            PagePath = "AddWritingAd.aspx";
                            break;
                    };
                    poinfo = null;
                }
            }
            else
            {
                MessageBox.ResponseScript(this.Page, "window.location.href='/Default.aspx'");
                return;
            }
            DateTime? end = null;
            DateTime? start = null;
            string unitname=Server.UrlDecode(Request.QueryString["unit"])!=""?Server.UrlDecode(Request.QueryString["unit"]):null;
            string category = Utils.GetString(Request.QueryString["category"], "");
            string title = Utils.GetString(Server.UrlDecode(Request.QueryString["title"]), "");
            int province = Utils.GetInt(Utils.GetQueryStringValue("province"), 1);
            int city = Utils.GetInt(Utils.GetQueryStringValue("city"), 0);
            if(!string.IsNullOrEmpty(Request.QueryString["start"]))
            {
                start = Utils.GetDateTime(Request.QueryString["start"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["end"]))
            {
                end = Utils.GetDateTime(Request.QueryString["end"]);
            }
            bool IsContain = false;
            foreach (var item in MasterUserInfo.AreaId)
            {
                if (item == city)
                {
                    IsContain = true;
                    break;
                }
            }
            if (!IsContain)
                city = int.MaxValue;
            int relationId = city;
            //广告位置
            txtTitle.Value = title;
            txtUnitName.Value =unitname;
            DatePicker1.Value = start == null ? "" : start.Value.ToString("yyyy-MM-dd");
            DatePicker2.Value = end == null ? "" : end.Value.ToString("yyyy-MM-dd");
            ProvinceAndCityList1.SetCityId = city;
            ProvinceAndCityList1.SetProvinceId = province;
            ddlCategory.SelectedValue=category;
            ltr_Menu.Text = ((EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), position)).ToString() + "广告管理";
            //购买单位
            int intRecordCount = 0; //总记录数
            currentPage = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            IList<EyouSoft.Model.AdvStructure.AdvInfo> list = null;
            if (category != "")
            {
                list=EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(pageSize, currentPage, ref intRecordCount, (EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), position), relationId, unitname, (EyouSoft.Model.AdvStructure.AdvCategory)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCategory), category), start, end, title);
            }
            else
            {
     list = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(pageSize, currentPage, ref intRecordCount, (EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), position), relationId, unitname, null, start, end, title);
            }
            
            if (list != null && list.Count > 0)
            {
                crptPhotoList.DataSource = list;
                crptPhotoList.DataBind();
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.CurrencyPage = currentPage;
                this.ExportPageInfo1.intRecordCount = intRecordCount; 
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = "WritingAdList.aspx?";
                this.ExportPageInfo1.LinkType = 3;   
                list = null;
            }
            else
            {
                crptPhotoList.EmptyText = "<tr><td colspan=\"7\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无广告信息！</span></div></td></tr>";
                this.ExportPageInfo1.Visible = false;
            }

            this.ddlCategory.Items.Add(new ListItem("-请选择-",""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.AdvStructure.AdvCategory));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlCategory.Items.Add(new ListItem(str, ((int)(EyouSoft.Model.AdvStructure.AdvCategory)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCategory),str)).ToString()));
                }
            }
            ddlCategory.SelectedValue = category;
        }

        #region 获取投放城市
        protected string GetRelation(object Range,object relases)
        {
            string result = "";
            IList<int> relas = relases as IList<int>;
            switch ((EyouSoft.Model.AdvStructure.AdvRange)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvRange), Range.ToString()))
            {
                case EyouSoft.Model.AdvStructure.AdvRange.全国:
                    result = "全国";
                    break;
                case EyouSoft.Model.AdvStructure.AdvRange.全省: ;
                    if (relas != null)
                    {
                        int num = 0;
                        EyouSoft.Model.SystemStructure.SysProvince province = null;
                        foreach (int item in relas)
                        {
                            province = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(item);
                            if (province != null)
                            {
                                num++;
                                result += province.ProvinceName + ",";
                            }
                            if (num %4== 0)
                            {
                                result += "</br>";
                            }
                        }
                        province = null;
                    }
                    break;
                case EyouSoft.Model.AdvStructure.AdvRange.单位类型:
                    break;
                case EyouSoft.Model.AdvStructure.AdvRange.城市:
                    if (relas != null)
                    {
                        EyouSoft.Model.SystemStructure.SysCity city = null;
                        int num = 0;
                        foreach (int item in relas)
                        {
                            city = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(item);
                            if (city != null)
                            {
                                num++;
                                result += city.CityName + ",";
                            }
                            if (num %4== 0)
                            {
                                result += "</br>";
                            }
                        }
                        city = null;
                    }
                    break;
            }
            if (result != "全国" && result != "")
            {
                return "<a href=\"javascript:void(0)\" onmouseover='wsug(this,\"" + Range.ToString() + "：" + result.TrimEnd(',') + "\")' onmouseout='wsug(this, 0)'>" + Utils.GetText(result.TrimEnd(','), 5) + "</a>";
            }
            else
            {
                return result;

            }
        }
        #endregion
        #region 永久时间判断
        protected string GetDate(string start, string end)
        {
            if (Utils.GetDateTime(end, DateTime.MinValue).Date == DateTime.MaxValue.Date)
            {
                return "永久";
            }
            else
            {
                return Utils.GetDateTime(start).ToString("yyyy-MM-dd") + "至" + Utils.GetDateTime(end).ToString("yyyy-MM-dd");
            }
        }
        #endregion
    }
}
