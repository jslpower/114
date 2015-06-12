using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 查看公司经营的线路区域
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class CompanyTourArea : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetCompanyAreaList();
            }  
        }

        /// <summary>
        /// 获的公司经营的线路区域
        /// </summary>
        protected void GetCompanyAreaList()
        {
            string CompanyId = EyouSoft.Common.Utils.InputText(Request.QueryString["CompanyId"]);
            StringBuilder strAllAreaList = new StringBuilder();
            StringBuilder strAllArea1 = new StringBuilder();
            StringBuilder strAllArea2 = new StringBuilder();
            StringBuilder strAllArea3 = new StringBuilder();


            IList<EyouSoft.Model.SystemStructure.AreaBase> SaleList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(CompanyId);
            int Count1 = 0;
            int Count2 = 0;
            int Count3 = 0;
            if (SaleList != null && SaleList.Count > 0)
            {
                strAllArea1.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内长线</td><td width=\"96%\">");
                strAllArea1.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strAllArea1.Append("<tr >");

                strAllArea2.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国际线</td><td width=\"96%\">");
                strAllArea2.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strAllArea2.Append("<tr>");

                strAllArea3.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内短线</td><td width=\"96%\">");
                strAllArea3.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strAllArea3.Append("<tr >");

                foreach (EyouSoft.Model.SystemStructure.AreaBase item in SaleList)
                {
                    if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)
                    {
                        strAllArea1.AppendFormat("<td width=\"25%\" align=\"left\"><label for=\"{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                        Count1++;
                        if (Count1 != 0 && Count1 % 4 == 0)
                        {
                            strAllArea1.Append("</tr><tr>");
                        }
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                    {
                        strAllArea2.AppendFormat("<td width=\"25%\" align=\"left\"><label for=\"{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                        Count2++;
                        if (Count2 != 0 && Count2 % 4 == 0)
                        {
                            strAllArea2.Append("</tr><tr >");
                        }
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)
                    {
                        strAllArea3.AppendFormat("<td width=\"25%\" align=\"left\"><label for=\"{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                        Count3++;
                        if (Count3 != 0 && Count3 % 4 == 0)
                        {
                            strAllArea3.Append("</tr><tr >");
                        }
                    }
                }
                strAllArea1.Append("</tr>");
                strAllArea1.Append("</table>");
                strAllArea1.Append("</td></tr></table>");
                strAllArea2.Append("</tr>");
                strAllArea2.Append("</table>");
                strAllArea2.Append("</td></tr></table>");
                strAllArea3.Append("</tr>");
                strAllArea3.Append("</table>");
                strAllArea3.Append("</td></tr></table>");
            }
            if (Count1 != 0)
            {
                strAllAreaList.Append(strAllArea1);
            }
            if (Count2 != 0)
            {
                strAllAreaList.Append(strAllArea2);
            }
            if (Count3 != 0)
            {
                strAllAreaList.Append(strAllArea3);
            }
            SaleList = null;

            if (string.IsNullOrEmpty(strAllAreaList.ToString()))
            {
                strAllAreaList.Append("暂无经营线路区域");
            }

            this.labTourAreaList.Text = strAllAreaList.ToString();

        }
    }
}
