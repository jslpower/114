using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.DataProtection;
using System.Text;
using EyouSoft.ControlCommon;
using EyouSoft.ControlCommon.Control;
namespace IMFrame.WebControls
{
    /// <summary>
    /// 章已泉 2009-9-10
    /// 销售城市用户控件 C\S
    /// -----------------------------------------------
    /// 2009-12-07 汪奇志 GetSiteNameProvinceIdName()
    /// 修改功能：前台js，图片，样式引用新的方式，后台方法引用新的底层方法
    /// 修改人：杜桂云    修改时间：2010-08-14
    /// </summary>
    public partial class SiteSelect : System.Web.UI.UserControl
    {
        protected int ProvinceId = 0;
        protected string ProvinceName = "";
        protected string SiteName = "";
        protected string[] ProvinceArray = { "", "" };
        protected string strSiteName = "";
        protected string ImageServerUrl = "";
        private int _cityId;
        /// <summary>
        /// 销售城市Id
        /// </summary>
        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            MQPage page = this.Page as MQPage;
            ImageServerUrl = page.ImageServerUrl;
            if (!IsPostBack)
            {
                EyouSoft.Model.SystemStructure.SysCity model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
                if (model != null)
                {
                    ProvinceName = model.ProvinceName;
                    SiteName = model.CityName;
                    ProvinceId = model.ProvinceId;
                }
                model = null;
            }
            InitData();
        }

        /// <summary>
        /// 获得所有的省份,销售城市,初始化数据
        /// </summary>
        private void InitData()
        {
            string str1 = "";
            string str2 = "";
            int AllByteCount = 50;
            string strDefault = "";
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                EyouSoft.IBLL.SystemStructure.ISysCity Citybll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                foreach (EyouSoft.Model.SystemStructure.SysProvince Province in ProvinceList)
                {
                    IList<EyouSoft.Model.SystemStructure.SysCity> Citylist = Citybll.GetSaleCity(null, new int[] { Province.ProvinceId });

                    if (Citylist != null && Citylist.Count > 0)
                    {
                        if (Province.AreaId == EyouSoft.Model.SystemStructure.ProvinceAreaType.直辖市)//表明直辖市
                        {
                            str1 += string.Format("<a id='p_{1}' href='javascript:void(0)' onclick='ChangeProvinceName(this,{0})' {3}><strong>{2}</strong></a><span style='letter-spacing: -3px'> | </span>", Citylist[0].CityId, Province.ProvinceId, Province.ProvinceName, Province.ProvinceId == ProvinceId ? "style='color:Red'" : "");
                        }
                        else
                        {
                            string name = string.Format("{0}  |  ",Province.ProvinceName);
                            if (Utils.GetByteLength(strDefault + name) > AllByteCount)
                            {
                                str2 += "<br />";
                                strDefault = name;
                            }
                            else
                            {
                                strDefault += name;
                            }
                            str2 += string.Format("<a id='p_{1}' href='javascript:void(0)' onclick='ChangeProvinceName(this,{0})' {3}>{2}</a><span style='letter-spacing: -3px'>&nbsp;|&nbsp;</span>", Citylist[0].CityId, Province.ProvinceId, Province.ProvinceName, Province.ProvinceId == ProvinceId ? "style='color:Red'" : "");
                        }
                        foreach (EyouSoft.Model.SystemStructure.SysCity cityModel in Citylist)
                        {
                            //获得当前省份下的销售城市信息
                            strSiteName += string.Format("<label pid='{3}'><a id='c_{0}' href='javascript:void(0)' onclick='ChangeSiteName(this,{0})' {2}>{1}</a><span style='letter-spacing: -3px'>&nbsp;|&nbsp;</span></label>", cityModel.CityId, cityModel.CityName, cityModel.CityId == CityId ? "style='color:Red'" : "",cityModel.ProvinceId);
                        }
                        Citylist = null;
                    }
                }
                //strSiteName = strSiteName.Length > 0 ? strSiteName.Substring(0, strSiteName.LastIndexOf("|")) : "";
            }
            ProvinceArray = new string[] { str1.Length > 0 ? str1.Substring(0, str1.LastIndexOf("|")) : "", str2.Length > 0 ? str2.Substring(0, str2.LastIndexOf("|")) : "" };
        }
    }
}