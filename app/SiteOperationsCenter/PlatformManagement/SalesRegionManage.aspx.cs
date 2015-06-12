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
using System.Text;


namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——销售区域维护
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class SalesRegionManage : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int AgencyCount = 0;
        private int CurrencyPage = 1;
        private int intPageSize = 10;
        protected bool EditFlag = false; //修改权限
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_城市管理 };
            EditFlag = CheckMasterGrant(parms);
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_城市管理, true);
                return;
            }
            else
            {
                //绑定列表数据
                InitListInfo();
                this.imb_Qurey.ImageUrl = ImageServerUrl + "/images/yunying/chaxun.gif";
            }
        }
        #endregion

        #region 绑定数据源
        /// <summary>
        /// 绑定数据源
        /// </summary>
        protected void InitListInfo()
        {
            int intRecordCount = 0;
            int ProvinceId = 0;
            int CityId = 0;
            bool IsEnable = false;
            IList<EyouSoft.Model.SystemStructure.SysCity> cityListInfo = null;
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["ProvinceID"])))
            {
                ProvinceId =Utils.GetInt(Request.QueryString["ProvinceID"]);
            }
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["CityId"])))
            {
                CityId = Utils.GetInt(Request.QueryString["CityID"]);
            }
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
             //当此变量为-1的时候，则isenable传null，不作为查询条件
            string flg = "-1";
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["IsEnable"])))
            {
                flg = Utils.InputText(Request.QueryString["IsEnable"]);
                if (flg == "1")
                { IsEnable = true; }
                else
                { IsEnable = false; }
            }

            #region 初始化查询条件
            this.ProvinceAndCityList1.SetProvinceId = ProvinceId;
            this.ProvinceAndCityList1.SetCityId = CityId;
            this.ddlIsEnable.SelectedValue = Utils.InputText(Request.QueryString["IsEnable"]);
            #endregion

            if (flg != "-1")
            {
                cityListInfo = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(intPageSize, CurrencyPage, ref intRecordCount, 0, ProvinceId, CityId, IsEnable);
            }
            else
            {
                cityListInfo = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(intPageSize, CurrencyPage, ref intRecordCount, 0, ProvinceId, CityId, null);
            }
            if (cityListInfo != null && cityListInfo.Count > 0)
            {

                //绑定数据源
                this.RepeaterList.DataSource = cityListInfo;
                this.RepeaterList.DataBind();
                //绑定分页控件
                this.ExportPageInfo2.intPageSize = intPageSize;
                this.ExportPageInfo2.intRecordCount = intRecordCount;
                this.ExportPageInfo2.CurrencyPage = CurrencyPage;
                this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo2.UrlParams = Request.QueryString;
                this.ExportPageInfo2.PageLinkURL = "SalesRegionManage.aspx?";
                this.ExportPageInfo2.LinkType = 3;

            }
            else
            {
                this.NoData.Visible = true;
            }
            //释放资源
            cityListInfo = null;
        }
        #endregion

        #region 查询
        protected void imb_Qurey_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Request.ServerVariables["SCRIPT_NAME"] + "?ProvinceID=" + Utils.GetFormValue("ProvinceAndCityList1$ddl_ProvinceList") + "&CityID=" + Utils.GetFormValue("ProvinceAndCityList1$ddl_CityList") + "&IsEnable=" + Utils.GetFormValue("ddlIsEnable"));
            return;
        }
        #endregion

        #region 每个城市下关联的批发商总数
        protected string GetRelationCompanyCount(object agencyList, int CityID,int ProvinceID)
        {
            int count = 0;
            if (agencyList != null)
            {
                Dictionary<int, IList<string>> dicData = (Dictionary<int, IList<string>>)agencyList;
                foreach (KeyValuePair<int, IList<string>> pair in dicData)
                {
                    count += pair.Value.Count;
                }
            }
            string returnVal = "";
            //实例化对象，获取批发商的数量，点击链接到设置区域批发商的页面
            returnVal = string.Format("<a  href='ChooseRouteAgency.aspx?CityID=" + CityID + "&ProvinceID=" + ProvinceID + "'>{0}家</a>", count);

            return returnVal;
        }
        #endregion

        #region 取分站内所有线路区域,并取每个线路区域包含的专线商数(都要属于该分站内)
        /// <summary>
        /// 获取国内长线下的线路区域所包含的专线商
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        protected string GetGNAreaName(object AreaList, object agencyList, int cityID,int TypeID)
        {
            string returnLongVal = "";
            string returnShortal = "";
            string returnOutVal = "";
            if (AreaList == null)
            {
                return "";
            }
            else
            {
                int index1 = 0, index2 = 0, index3 = 0;
                int num = 0;//单个线路区域下面所有的批发商数量
                //一个城市下所关联的所有批发商
                IList<EyouSoft.Model.SystemStructure.SysCityArea> areList = (IList<EyouSoft.Model.SystemStructure.SysCityArea>)AreaList;
                //循环取出线路区域
                foreach (EyouSoft.Model.SystemStructure.SysCityArea model in areList)
                {
                   
                    //键：线路区域id  值：线路区域对应的批发商ID集合 
                    Dictionary<int, IList<string>> dicData = (Dictionary<int, IList<string>>)agencyList;
                    num = 0;

                    if (dicData != null && dicData.ContainsKey(model.AreaId) && dicData[model.AreaId] != null)
                    {
                        num += dicData[model.AreaId].Count;
                    }
                    if (model.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)
                    {
                        index1++;
                        returnLongVal += string.Format("{0}<a class=\"fontcolor\" href='ShowCompany.aspx?EditSiteId={1}&AreaId={2}'  target=\"_blank\" style=\"cursor:pointer\" > (批发商:{3})</a>|", model.AreaName, cityID, model.AreaId, num);
                        if (index1 % 3 == 0)
                        {
                            returnLongVal += "<br />";
                        }
                    }
                    else if (model.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)
                    {
                        index2++;
                        returnShortal += string.Format("{0}<a class=\"fontcolor\" href='ShowCompany.aspx?EditSiteId={1}&AreaId={2}'  target=\"_blank\" style=\"cursor:pointer\" > (批发商:{3})</a>|", model.AreaName, cityID, model.AreaId, num);
                        if (index2 % 3 == 0)
                        {
                            returnShortal += "<br />";
                        }
                    }
                    else
                    {
                        index3++;
                        returnOutVal += string.Format("{0}<a class=\"fontcolor\" href='ShowCompany.aspx?EditSiteId={1}&AreaId={2}'  target=\"_blank\" style=\"cursor:pointer\" > (批发商:{3})</a>|", model.AreaName, cityID, model.AreaId, num);
                        if (index3 % 3 == 0)
                        {
                            returnOutVal += "<br />";
                        }
                    }

                }
                //释放资源
                areList = null;
                //typeID：0—长线；1—短线；2—出境线路
                if (TypeID == 0)
                {
                    return returnLongVal;
                }
                else if (TypeID == 1)
                {
                    return returnShortal;
                }
                else 
                {
                    return returnOutVal;
                }
            }
        }
        #endregion

        #region 修改线路区域下的专线商
        /// <summary>
        /// 修改线路区域下的专线商
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        protected string EditeAreaName(int CityId, int ProvinceID)
        {
            string str = "";
            str = string.Format("<a href=\"ChooseRouteArea.aspx?CityID=" + CityId + "&ProvinceID=" + ProvinceID + "\" >维护线路区域</a>");
            return str;
        }
        #endregion

        #region 项绑定事件
        protected void RepeaterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //操作状态
                LinkButton linkState = (LinkButton)e.Item.FindControl("linkState");
                Label lblIsEnable = (Label)e.Item.FindControl("lblIsEnable");
                if (lblIsEnable.Text == "False")
                {
                    linkState.Text = "启用";
                }
                else if (lblIsEnable.Text == "True")
                {
                    //启用状态
                    linkState.Text = "<span class='RedFnt'>停用</span>";
                }

                //设置或取消出港城市
                LinkButton lbt_SetOutCity = (LinkButton)e.Item.FindControl("lbt_SetOutCity");
                Label lbl_SetOutCity = (Label)e.Item.FindControl("lbl_SetOCity");
                if (lbl_SetOutCity.Text == "False")
                {
                    lbt_SetOutCity.Text = "设置出港";
                }
                else if (lbl_SetOutCity.Text == "True")
                {
                    lbt_SetOutCity.Text = "<span class='RedFnt'>取消出港</span>";
                }
            }
        }
        #endregion

        #region 处理项事件
        protected void RepeaterList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //实例化类的对象
            //设置或者取消出港城市
            if (EditFlag)
            {
                if (e.CommandName == "SetCity")
                {
                    int userId = Int32.Parse(e.CommandArgument.ToString());
                    int[] cityID = { userId };
                    LinkButton linkBtn = e.CommandSource as LinkButton;
                    if (linkBtn.Text == "设置出港")
                    {
                        bool flag = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().SetIsSite(cityID, true);
                        if (flag == true)
                            MessageBox.Show(this, "设置成功！");
                        else
                            MessageBox.Show(this, "设置失败！");

                    }
                    else if (linkBtn.Text == "<span class='RedFnt'>取消出港</span>")
                    {
                        //调用底层修改方法修改成启用
                        bool flag = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().SetIsSite(cityID, false);
                        if (flag == true)
                            MessageBox.Show(this, "取消设置成功！");
                        else
                            MessageBox.Show(this, "取消设置失败！");

                    }
                }
                //设置启用或停用状态
                if (e.CommandName == "Lock")
                {
                    int userId = Int32.Parse(e.CommandArgument.ToString());
                    LinkButton linkBtn = e.CommandSource as LinkButton;
                    if (linkBtn.Text == "启用")
                    {
                        //修改成启用
                        bool flag = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().SetIsEnabled(userId, true);
                        if (flag == true)
                            MessageBox.Show(this, "启用成功！");
                        else
                            MessageBox.Show(this, "启用失败！");
                    }
                    else if (linkBtn.Text == "<span class='RedFnt'>停用</span>")
                    {
                        //调用底层修改方法修改成停用
                        bool flag = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().SetIsEnabled(userId, false);
                        if (flag == true)
                            MessageBox.Show(this, "停用成功！");
                        else
                            MessageBox.Show(this, "停用失败！");
                    }
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "对不起，您没有修改权限！", Request.Url.ToString());
            }

            //绑定列表数据
            InitListInfo();
        }
        #endregion
    }
}
