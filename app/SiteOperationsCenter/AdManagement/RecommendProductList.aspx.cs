using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using EyouSoft.Common;
using Newtonsoft.Json.Converters;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    ///页面功能：运营后台--广告管理---114平台管理--推荐产品列表
    /// CreateTime:2011-05-16
    /// Author：刘咏梅
    /// </summary>
    public partial class RecommendProductList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected string rpListStr = string.Empty;
        protected string rpCompanyListStr = string.Empty;
        protected string DeleteId = string.Empty;
        protected int RecordCount = 0;
        protected int pageSize = 20;
        protected int pageIndex = 1;
        #endregion


        #region 页面加载
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
                bool result = EyouSoft.BLL.AdvStructure.ExtendProduct.CreateInstance().DelExtendProduct(Utils.GetInt(Request.QueryString["id"], 0));
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
                //产品ID 
                string productId = Request.QueryString["ProductID"];
                //产品名称
                string productName = Request.QueryString["ProductName"];
                ////产品价格
                //decimal productPrice = Utils.GetDecimal(Request.QueryString["ProductPrice"], 0);
                int CityId = Utils.GetInt(Request.QueryString["cityid"], 0);
                string companyId = Request.QueryString["cid"] == null ? "" : Request.QueryString["cid"].ToString();
                if (CityId > 0 && !string.IsNullOrEmpty(companyId))
                {
                        EyouSoft.Model.AdvStructure.ExtendProductInfo model = new EyouSoft.Model.AdvStructure.ExtendProductInfo();
                        model.CompanyID = companyId;
                        model.ShowCityId = CityId;
                        //model.Price = productPrice;
                        model.ProductName = productName;
                        model.ProductID = productId;
                        model.SortID = 0;
                        EyouSoft.BLL.AdvStructure.ExtendProduct.CreateInstance().AddExtendProduct(model);
                        strHtml = JsonConvert.SerializeObject(EyouSoft.BLL.AdvStructure.ExtendProduct.CreateInstance().GetExtendProductList(CityId));
                        model = null;
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
        #endregion

        #region 绑定列表
        private void BindList()
        {
            #region 参数赋值
            int cityId = Utils.GetInt(Request.QueryString["cityId"], 0);//顶部列表城市编号
            int cid = Utils.GetInt(Request.QueryString["cid"], 0);
            int areaId = Utils.GetInt(Request.QueryString["aid"], 0);
            pageIndex = Utils.GetInt(Request.QueryString["pageIndex"], 1);
            int leaveCityId = Utils.GetInt(Request.QueryString["cid"], 0);
            #endregion

            #region 读取数据
            rpListStr = JsonConvert.SerializeObject(EyouSoft.BLL.AdvStructure.ExtendProduct.CreateInstance().GetExtendProductList(cityId));
            rpCompanyListStr = JsonConvert.SerializeObject((EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByAreaId(pageSize, pageIndex, ref RecordCount,areaId, leaveCityId)), new JavaScriptDateTimeConverter());
            rpCompanyListStr = RecordCount.ToString() + "$$$$" + pageIndex.ToString() + "$$$$" + rpCompanyListStr;
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


        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //定义推荐产品集合列表
            IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> list = new List<EyouSoft.Model.AdvStructure.ExtendProductInfo>();
            //城市编号
            int CurrCityId = Utils.GetInt(hCurrCityId.Value, 362);
            //省份编号
            int CurrProvinceId = Utils.GetInt(hCurrProvinceId.Value, 33);
            //省份名称
            string CurrCityName = hCurrCityName.Value;
            //主键ID
            string[] Ids = Utils.GetFormValues("hKeyId");
            //公司Id
            string[] CompanyIds = Utils.GetFormValues("hCId");
            //排序号
            string[] SotrNums = Utils.GetFormValues("txt_Sort");
            //产品价格
            string[] ProductPrices = Utils.GetFormValues("hPrice");
            //产品名称
            string[] ProductNames = Utils.GetFormValues("hProductName");  
            //产品编号
            string[] ProductIDS = Utils.GetFormValues("hProductId");
            //公司名称
            string[] CompanyNames = Utils.GetFormValues("hfCompanyName");
            //联系MQ
            string[] ContactMQS = Utils.GetFormValues("hfMQ");
            for (int i = 0; i < Ids.Length; i++)
            {
                //定义推荐产品实体对象
                EyouSoft.Model.AdvStructure.ExtendProductInfo model = new EyouSoft.Model.AdvStructure.ExtendProductInfo();
               
                model.CompanyID = CompanyIds[i];
                model.CompanyName = CompanyNames[i];
                model.ProductName = ProductNames[i];
                model.ContactMQ = ContactMQS[i];
                model.Price =Utils.GetDecimal(ProductPrices[i],0);
                model.ProductID = ProductIDS[i];
                model.ID = int.Parse(Ids[i]);
                model.ShowCity = CurrCityName;
                model.ShowCityId = CurrCityId;
                model.ShowProId = CurrProvinceId;
                model.SortID = int.Parse(SotrNums[i]);
                list.Add(model);
                model = null;
            }
            if (list.Count > 0)
            {
                bool result = EyouSoft.BLL.AdvStructure.ExtendProduct.CreateInstance().UpdateExtendProduct(list);
                MessageBox.ShowAndRedirect(this, result ? "保存成功！" : "保存失败！", "/AdManagement/RecommendProductList.aspx");
            }
        }
        #endregion
    }
}
