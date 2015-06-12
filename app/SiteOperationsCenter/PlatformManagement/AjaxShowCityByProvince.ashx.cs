using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：线路区域分类——处理选中省份列出相应城市列表
    /// 开发人：杜桂云      开发时间：2010-07-07
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxShowCityByProvince : IHttpHandler
    {

        #region 处理请求
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strTemp = "";
            int provinceID = 0;
            if (!string.IsNullOrEmpty(Utils.InputText(context.Request.QueryString["ProvinceId"])))
            {
                provinceID = int.Parse(Utils.InputText(context.Request.QueryString["ProvinceId"]));
            }
            Dictionary<string, string> SiteDic = new Dictionary<string, string>();
            //获取城市列表
            IList<EyouSoft.Model.SystemStructure.SysCity> cityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(provinceID, 0, null, null,null);
            if (cityList != null && cityList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysCity model in cityList)
                {
                    string CityId = model.CityId.ToString();
                    string CityName = model.CityName;
                    if (!SiteDic.ContainsKey(CityId))
                    {
                        SiteDic.Add(CityId, CityName);
                    }
                }
            }
            if (SiteDic.Count > 0)
            {
                strTemp += "<ul><li>请选择城市：</li>";
                //int index = 0;
                if (provinceID != 0)
                {
                    foreach (KeyValuePair<string, string> pair in SiteDic)
                    {
                        strTemp += string.Format("<li><a  id='li_{1}' href='javascript:void(0)' onclick='ChangeClass(this,\"1\");ShowAreaAndCompany({2},{1},1);' onfocus=\"blur()\" name='a_ProvinceName'><nobr>{0}</nobr></a></li>", pair.Value, pair.Key, provinceID);

                    }
                }
                strTemp += "</ul>";
            }
            context.Response.Write(strTemp);
            context.Response.End();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
