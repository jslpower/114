using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 供求列表页
    /// author: dj
    /// date: 2011-05-12
    /// </summary>
    public partial class SupplierList : EyouSoft.Common.Control.FrontPage
    {
        protected int pagesize = 40;
        protected int pageindex;
        protected int totalcount;

        protected int sTime = 0;
        protected int sTage = 0;
        protected int sType = 0;
        protected int sCat = 0;
        protected int sProvid = 0;
        protected string keyword = string.Empty;
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> exList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Bind();
            }

            //设置Title.....
            this.Title = string.Format(EyouSoft.Common.PageTitle.SupplyList_Title, GetProName(sProvid), sType==0?"":((EyouSoft.Model.CommunityStructure.ExchangeType)sType).ToString());
            AddMetaTag("description", EyouSoft.Common.PageTitle.SupplyList_Des);
            AddMetaTag("keywords", EyouSoft.Common.PageTitle.SupplyList_Keywords);

        }

        private void Bind()
        {

            sTime = EyouSoft.Common.Utils.GetInt(Request.QueryString["stime"],0);
            sTage = EyouSoft.Common.Utils.GetInt(Request.QueryString["stag"],0);
            sType = EyouSoft.Common.Utils.GetInt(Request.QueryString["stype"],0);
            sCat = EyouSoft.Common.Utils.GetInt(Request.QueryString["sCat"],0);
            sProvid = EyouSoft.Common.Utils.GetInt(Request.QueryString["sProvid"],-1);
            if (sProvid < 0)
            {
                sProvid = 0;
            }
            keyword = EyouSoft.Common.Utils.GetQueryStringValue("keyword");
            if (keyword == "请输入关键字")
            {
                keyword = "";
            }

            EyouSoft.Model.CommunityStructure.SearchInfo searchinfo = new EyouSoft.Model.CommunityStructure.SearchInfo();
            //searchinfo.City = CityId;
            searchinfo.ExchangeCategory = null;
            if (sCat > 0 && !IsLogin)
            {
                string strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(EyouSoft.Common.Domain.UserPublicCenter + Request.RawUrl, string.Empty);
                Response.Redirect(strLoginUrl);
            }
            else if(sCat>0)
            {
                searchinfo.ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)sCat;
            }
            searchinfo.ExchangeTag = null;
            if(sTage>0)
            {
                searchinfo.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)sTage;
            }
            searchinfo.ExchangeTitle = keyword;
            searchinfo.ExchangeType = null;
            if(sType>0)
            {
                searchinfo.ExchangeType = (EyouSoft.Model.CommunityStructure.ExchangeType)sType;
            }
            searchinfo.Province = null;
            if(sProvid>0)
            {
                searchinfo.Province = sProvid;
            }
            searchinfo.SearchDateTimeType = (EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe)sTime;

            pageindex = EyouSoft.Common.Utils.GetInt(Request.QueryString["page"], 1);
            exList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetList(pagesize, pageindex, ref totalcount, searchinfo);
            PageBind();


            
        }

        private void PageBind()
        {
            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExporPageInfoSelect2.Placeholder = "#PageIndex#";
                this.ExporPageInfoSelect2.IsUrlRewrite = true;
                string strTemp = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request);
                if (strTemp.Split('_').Count() == 3)
                {
                    this.ExporPageInfoSelect2.PageLinkURL = strTemp.Split('_')[0] + "_" + strTemp.Split('_')[1] + "_0" + "_" + CityId + "_#PageIndex#";
                }
                else
                {
                    this.ExporPageInfoSelect2.PageLinkURL = strTemp + "_#PageIndex#";
                }
                //this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            }

            this.ExporPageInfoSelect2.intPageSize = pagesize;
            this.ExporPageInfoSelect2.CurrencyPage = pageindex;
            this.ExporPageInfoSelect2.intRecordCount = totalcount;
        }

        /// <summary>
        /// 得到导航
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stime"></param>
        /// <param name="stag"></param>
        /// <param name="stype"></param>
        /// <param name="proid"></param>
        /// <param name="cat"></param>
        /// <param name="cityid"></param>
        /// <param name="timename"></param>
        /// <param name="index">1时间,2标签,3类别 4 供求</param>
        /// <returns></returns>
        protected string getTimeNavHtml(string key, int stime, int stag, int stype, int proid, int cat, int cityid, string[] timename,int index)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0;i<timename.Length;i++)
            {
                //if (index == 3 && stype==8)

                if ((i == stime && index == 1) || (i == stag && index == 2) || (i == stype && index == 3) || (i == cat && index == 4))
                {
                    if (!(index == 3 && i == 8))
                        sb.AppendFormat("<span>{0}</span>&nbsp;", timename[i]);
                }
                else
                {
                    switch (index)
                    {
                        case 1:
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, i, stag, stype, proid, cat, cityid), timename[i]);
                            break;
                        case 2:
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, i, stype, proid, cat, cityid), timename[i]);
                            break;
                        case 3:
                            if (i != 8) //让其它移动到最后
                                sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, stag, i, proid, cat, cityid), timename[i]);
                            break;
                        case 4:
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, stag, stype, proid, i, cityid), timename[i]);
                            break;
                    }

                }
            }

            if (index == 3 && stype != 8) //让其它移动到最后  有链接
            {
                sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, stag, 8, proid, cat, cityid), "其它");
            }

            if (index == 3 && stype == 8)//让其它移动到最后 无链接
            {
                sb.AppendFormat("<span>{0}</span>&nbsp;", timename[stype]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到省份导航
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stime"></param>
        /// <param name="stag"></param>
        /// <param name="stype"></param>
        /// <param name="proid"></param>
        /// <param name="cat"></param>
        /// <param name="cityid"></param>
        /// <returns></returns>
        protected string GetProNavHtml(string key, int stime, int stag, int stype, int proid, int cat, int cityid)
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> plist = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            StringBuilder sb = new StringBuilder();
            if (proid == 0)
            {
                sb.Append("<span>全部</span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, stag, stype, 0, cat, cityid), "全部");
            }
            if (plist != null && plist.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysProvince p in plist)
                {
                    if (p.ProvinceId == proid)
                    {
                        sb.AppendFormat("<span>{0}</span>", p.ProvinceName);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl(key, stime, stag, stype, p.ProvinceId, cat, cityid), p.ProvinceName);

                    }
                }
            }
            return sb.ToString();
        }


        protected string GetProName(int proid)
        {
            if (proid == 0)
            {
                return "全国";
            }
            else
            {
                EyouSoft.Model.SystemStructure.SysProvince p = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(proid);
                if (p != null)
                {
                    return p.ProvinceName;
                }
                else
                {
                    return "全国";
                }
            }
        }


        /// <summary>
        /// 获取只看供应或只看求购连接地址
        /// </summary>
        public string GetReturnUrl(int cat)
        {
            string m = this.Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl( EyouSoft.Common.Domain.UserPublicCenter + EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,CityModel.ProvinceId,cat,CityId), "_top", "");
        }

    }
}
