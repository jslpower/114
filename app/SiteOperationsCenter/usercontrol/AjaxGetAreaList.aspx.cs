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
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.usercontrol
{
    public partial class AjaxGetAreaList : System.Web.UI.Page
    {
        protected string strTourAreaList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string CityID = Utils.GetQueryStringValue("CityID");
                string ProvinceId = Utils.GetQueryStringValue("ProvinceId");
                StringBuilder strAreaList = new StringBuilder();
                strAreaList.Append("<option value='0'>请选择线路区域</option>");
                if (!string.IsNullOrEmpty(ProvinceId) && ProvinceId.ToLower() != "undefined")
                {
                    //国内长线
                    IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList1 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(int.Parse(ProvinceId));
                    if (AreaList1 != null && AreaList1.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.SysAreaSiteControl model in AreaList1)
                        {
                            strAreaList.AppendFormat("<option value='{0}'>{1}</option>", model.AreaId.ToString(), model.AreaName);
                        }
                    }
                    AreaList1 = null;
                }
               
                //国际线
                IList<EyouSoft.Model.SystemStructure.SysArea> AreaList2 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
                if (AreaList2 != null && AreaList2.Count > 0)
                {
                    foreach (EyouSoft.Model.SystemStructure.SysArea model2 in AreaList2)
                    {
                        strAreaList.AppendFormat("<option value='{0}'>{1}</option>", model2.AreaId.ToString(), model2.AreaName);
                    }
                }
                //国内短线
                IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList3 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(int.Parse(CityID));
                if (AreaList3 != null && AreaList3.Count > 0)
                {
                    foreach (EyouSoft.Model.SystemStructure.SysAreaSiteControl model in AreaList3)
                    {
                        strAreaList.AppendFormat("<option value='{0}'>{1}</option>", model.AreaId.ToString(), model.AreaName);
                    }
                }

                AreaList2 = null;
                AreaList3 = null;
                strTourAreaList = strAreaList.ToString();
                strAreaList = null;
            }
        }
    }
}
