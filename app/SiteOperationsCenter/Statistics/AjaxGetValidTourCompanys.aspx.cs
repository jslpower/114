using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 有有效产品批发商列表
    /// 罗丽娥    2010-09-19
    /// </summary>
    public partial class AjaxGetValidTourCompanys : EyouSoft.Common.Control.YunYingPage
    {
        private int intPageSize = 20, CurrencyPage = 1, count = 0;
        protected bool Invalid = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Invalid = bool.Parse(Utils.GetQueryStringValue("Invalid"));
            if (!Page.IsPostBack)
            {
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_管理该栏目))
                {
                    Utils.ResponseNoPermit("对不起，您没有统计分析_管理该栏目权限!");
                    return;
                }
                else
                {
                    if (!Invalid)
                    {
                        if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_无有效产品专线商))
                        {
                            Utils.ResponseNoPermit("对不起，您没有统计分析_无有效产品专线商权限!");
                            return;
                        }
                    }
                    else
                    {
                        if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_有有效产品专线商))
                        {
                            Utils.ResponseNoPermit("对不起，您没有统计分析_有有效产品专线商权限!");
                            return;
                        }
                    }
                }

                InitValidTourCompanys();
            }
        }

        #region 初始化有有效产品批发商列表
        private void InitValidTourCompanys()
        {
            int intRecordCount = 0;
            int? ProvinceID = Utils.GetIntNull(Utils.GetQueryStringValue("ProvinceID"), null);
            if (ProvinceID == 0)
                ProvinceID = null;
            int? CityID = Utils.GetIntNull(Utils.GetQueryStringValue("CityID"),null);
            if (CityID == 0)
                CityID = null;
            int? AreaID = Utils.GetIntNull(Utils.GetQueryStringValue("AreaID"),null);
            if (AreaID == 0)
                AreaID = null;
            string CompanyName = Utils.GetQueryStringValue("CompanyName");

            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> list = null;

            if (Invalid)
            {
                list = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetValidTourCompanys(intPageSize, CurrencyPage, ref intRecordCount, CityID, AreaID, CompanyName);
            }
            else {
                list = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetInvalidTourCompanys(intPageSize, CurrencyPage, ref intRecordCount, CityID, AreaID, CompanyName);
            }

            if (intRecordCount > 0)
            {
                this.tr_NoData.Visible = false;

                this.rptValidTourCompanyList.DataSource = list;
                this.rptValidTourCompanyList.DataBind();

                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "ValidTourCompanys.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ValidTourCompanys.LoadData(this);", 0);
            }
            else {
                this.tr_NoData.Visible = true;
            }
        }
        #endregion

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (CurrencyPage - 1) * intPageSize;
        }
    }
}
