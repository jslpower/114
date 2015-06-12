using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;

namespace IMFrame.TourAgency.TourManger
{
    /// <summary>
    /// 设置关注批发商
    /// </summary>
    public partial class SetAttentionCompany : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int requestType = Utils.GetInt(Request.QueryString["RequestType"], 0);

            switch (requestType)
            {
                case 1:
                    this.SetFavors();
                    break;
                case 2:
                    this.InitCompanys();
                    break;
                default:
                    this.InitAreas();
                    break;
            }
        }

        #region Other Request
        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitAreas()
        {
            string output = "var cityAreas={0};";
            EyouSoft.Model.SystemStructure.SysCity cityInfo = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(SiteUserInfo.CityId);

            if (cityInfo != null && cityInfo.CityAreaControls != null)
            {
                var cityAreas = cityInfo.CityAreaControls.Select((tmp, index) => new { AreaId = tmp.AreaId, AreaName = tmp.AreaName, AreaType = (int)tmp.RouteType });

                if (cityAreas != null)
                {
                    output = string.Format(output, Newtonsoft.Json.JsonConvert.SerializeObject(cityAreas));
                }
            }

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), output, true);
        }
        #endregion

        #region RequestType=1 设置/取消收藏
        /// <summary>
        /// 设置/取消收藏
        /// </summary>
        private void SetFavors()
        {
            int response = 0;
            int type =Utils.GetInt(Request.QueryString["SetType"]);

            EyouSoft.Model.CompanyStructure.CompanyFavor favorInfo = new EyouSoft.Model.CompanyStructure.CompanyFavor
            {
                AreaId = 0,
                CompanyId = SiteUserInfo.CompanyID,
                FavorCompanyId = Request.QueryString["FavorCompanyId"]
            };

            if (string.IsNullOrEmpty(favorInfo.FavorCompanyId))
            {
                response = 2;
            }

            EyouSoft.IBLL.CompanyStructure.ICompanyFavor bll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();

            if (type == 1)
            {
                response = bll.SaveCompanyFavor(favorInfo) ? 1 : 0;
            }
            else if (type == 0)
            {
                response = bll.Delete(favorInfo.CompanyId, favorInfo.FavorCompanyId) ? 1 : 0;
            }
            else
            {
                response = 3;
            }

            Response.Clear();
            Response.Write(response);
            Response.End();
        }
        #endregion

        #region RequestType=2 批发商列表
        /// <summary>
        /// 已设置的关注批发商信息集合
        /// </summary>
        private IList<string> Favors;

        /// <summary>
        /// 初始化批发商列表
        /// </summary>
        private void InitCompanys()
        {
            this.InitFavors();

            int pageSize = 20;
            int pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            int recordCount=0;
            EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query = new EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase();

            query.AreaId = Utils.GetInt(Request.QueryString["AreaId"], 0);
            query.CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
            query.CityId = SiteUserInfo.CityId;

            EyouSoft.IBLL.CompanyStructure.ICompanyInfo bll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            var items = bll.GetListRouteAgency(query, pageSize, pageIndex, ref recordCount);
            var obj = new { html = string.Empty, pageSize = pageSize, pageIndex = pageIndex, recordCount = recordCount };

            if (items != null && items.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append(@"<table width=""210"" border=""0"" cellpadding=""0"" align=""center"" cellspacing=""1"" bgcolor=""#eeeeee"" > ");

                foreach (EyouSoft.Model.CompanyStructure.CompanyInfo tmp in items)
                {
                    html.Append("<tr>");
                    html.Append(this.GetstrCompany(tmp.ID,tmp.CompanyName,tmp.ContactInfo.ContactName,tmp.ContactInfo.Tel,tmp.ContactInfo.Mobile));
                    html.Append("</tr>");
                }

                html.Append("</table>");

                obj = new { html = html.ToString(), pageSize = pageSize, pageIndex = pageIndex, recordCount = recordCount };
            }

            bll = null;

            Response.Clear();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
            Response.End();

        }

        /// <summary>
        /// 拼接每一家公司的信息
        /// </summary>
        private string GetstrCompany(string Id, string CompanyName, string ContactName, string ContactTel, string ContactMobile)
        {
            StringBuilder s = new StringBuilder();
            string Name = ContactName;
            if (Name.Length > 12)
            {
                string tmp1 = Name.Substring(0, 12);
                string tmp2 = Name.Substring(12, Name.Length - 12);
                Name = tmp1 + "<br />" + tmp2;
            }
            string Tel = ContactTel;
            if (Tel.Length > 18)
            {
                string tmp1 = Tel.Substring(0, 18);
                string tmp2 = Tel.Substring(18, Tel.Length - 18);
                Tel = tmp1 + "<br />" + tmp2;
            }
            string Mobile = ContactMobile;
            if (Mobile.Length > 18)
            {
                string tmp1 = Mobile.Substring(0, 18);
                string tmp2 = Mobile.Substring(12, Mobile.Length - 18);
                Mobile = tmp1 + "<br />" + tmp2;
            }
            string strContact = "联系人:" + Name + "<br />电 话:" + Tel + "<br />手 机:" + Mobile + "<br />";


            string url = EyouSoft.Common.Utils.GetCompanyDomain(Id, EyouSoft.Model.CompanyStructure.CompanyType .专线);

            s.AppendFormat("<td width=\"13%\" align=\"center\" bgcolor=\"#FFFFFF\"><input type='checkbox' id='input_{0}' name='input_{0}' value='{0}' onclick='setFavors(this)' {1} /></td>", Id, this.Favors.Contains(Id) ? " checked='checked' " : "");
            s.AppendFormat("<td width=\"87%\" bgcolor=\"#FFFFFF\"><a target=\"_blank\"  href=\"{2}\" class=\"cliewh\" onmouseover=\"wsug(event, '{0}')\" onmouseout=\"wsug(event, 0)\">{1}</a></td>", strContact, CompanyName, url);

            return s.ToString();

        }

        /// <summary>
        /// 初始化公司已设置的采购目录信息
        /// </summary>
        private void InitFavors()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyFavor bll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
            this.Favors = bll.GetListByCompanyId(SiteUserInfo.CompanyID);

            if (this.Favors == null)
            {
                this.Favors = new List<string>();
            }

            bll = null;
        }
        #endregion
    }
}
