using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    ///页面功能：运营后台--广告管理---114平台管理--散拼中心广告普通版列表
    /// CreateTime:2011-05-12
    /// Author：刘咏梅
    /// </summary>
    public partial class SanPinCenterNormalList : EyouSoft.Common.Control.YunYingPage
    {
        protected string rpListStr = string.Empty;
        protected string rpCompanyListStr = string.Empty;
        protected string DeleteId = string.Empty;
        protected int RecordCount = 0;
        protected int pageSize = 20;
        protected int pageIndex = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目, true);
                return;
            }
            #region 异步删除数据
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString() == "del")
            {         
                bool result=EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().DelSiteExtend(Utils.GetInt(Request.QueryString["id"], 0));
                Response.Clear();
                if (result)
                    Response.Write("success");
                else
                    Response.Write("error");
                Response.End();
            }
            #endregion

            #region 异步添加数据
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString() == "add")
            {
                string strHtml = "error";
                Response.Clear();
                int CityId = Utils.GetInt(Request.QueryString["cityid"], 0);
                string companyId = Request.QueryString["cid"]==null?"":Request.QueryString["cid"].ToString();
                if (CityId > 0 && !string.IsNullOrEmpty(companyId))
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo= EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyId);
                    if(CompanyInfo!=null)
                    {
                        EyouSoft.Model.AdvStructure.SiteExtendInfo model = new EyouSoft.Model.AdvStructure.SiteExtendInfo();
                        model.CompanyID = CompanyInfo.ID;
                        model.CompanyName = CompanyInfo.CompanyName;
                        model.IsBold = false;
                        model.IsShow = true;
                        model.ShowCityID = CityId;
                        model.SortID = 0;
                        EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().AddSiteExtend(model);
                        model = null;
                        strHtml = JsonConvert.SerializeObject(EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().GetSiteExtendList(CityId, true));
                    }
                    CompanyInfo = null;
                }
                Response.Write(strHtml);
                Response.End();
            }
            #endregion

            if (Request.QueryString["mode"] != null && (Request.QueryString["mode"].ToString().ToLower() == "getlist"
                || Request.QueryString["mode"].ToString().ToLower() == "getalllist"))
            {
                BindList();
            }

        }

        #region 获取列表
        private void BindList()
        {
            #region 参数赋值
            int cityId = Utils.GetInt(Request.QueryString["cityId"], 0);//顶部列表城市编号
            int cid = Utils.GetInt(Request.QueryString["cid"], 0);
            int areaId = Utils.GetInt(Request.QueryString["aid"],0);
            bool IsPay=Utils.GetInt(Request.QueryString["ispay"],0)==0?false:true;
            pageIndex = Utils.GetInt(Request.QueryString["pageIndex"], 1);
            #endregion

            #region 读取数据
            rpListStr = JsonConvert.SerializeObject(EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().GetSiteExtendList(cityId, true));
            rpCompanyListStr = JsonConvert.SerializeObject(EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().GetALLVerifyCompany(pageSize, pageIndex, ref RecordCount, cid, IsPay), new JavaScriptDateTimeConverter());
            rpCompanyListStr = RecordCount.ToString() + "$$$$"+pageIndex.ToString()+"$$$$" + rpCompanyListStr;
            #endregion

            #region 验证是否异步请求
            if (Request.QueryString["mode"] != null)
            {
                switch (Request.QueryString["mode"].ToString())
                { 
                    case "getlist":
                        Response.Clear();
                        Response.Write(rpListStr);
                        Response.End();
                        break;
                    case "getalllist":
                        Response.Clear();
                        Response.Write(rpCompanyListStr);
                        Response.End();
                        break;
                }
            }
            #endregion

            
        }
        #endregion

        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> list = new List<EyouSoft.Model.AdvStructure.SiteExtendInfo>();
            int CurrCityId = Utils.GetInt(hCurrCityId.Value, 362);
            int CurrProvinceId = Utils.GetInt(hCurrProvinceId.Value, 33);
            string CurrCityName = hCurrCityName.Value;

            string[] Ids = Utils.GetFormValues("hKeyId");
            string[] CompanyIds = Utils.GetFormValues("hCId");
            string[] SotrNums = Utils.GetFormValues("txt_Sort");
            string[] CompanyNames = Utils.GetFormValues("hCompanyName");
            string[] Colors = Utils.GetFormValues("hColor");
            string[] IsBolds = Utils.GetFormValues("hCk_Blod");

            for (int i = 0; i < Ids.Length; i++)
            {
                EyouSoft.Model.AdvStructure.SiteExtendInfo model = new EyouSoft.Model.AdvStructure.SiteExtendInfo();
                model.Color = Colors[i];
                model.CompanyID = CompanyIds[i];
                model.CompanyName = CompanyNames[i];
                model.ID = int.Parse(Ids[i]);
                model.IsBold = IsBolds[i] == "1" ? true : false;
                model.IsShow = true;
                model.ShowCity = CurrCityName;
                model.ShowCityID = CurrCityId;
                model.ShowProID = CurrProvinceId;
                model.SortID = int.Parse(SotrNums[i]);
                list.Add(model);
                model = null;
            }
            if (list.Count > 0)
            {
                bool result = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().UpdateSiteExtend(list);
                MessageBox.ShowAndRedirect(this,result?"保存成功！":"保存失败！","/AdManagement/SanPinCenterNormalList.aspx");
            }
        }
        #endregion

    }
}
