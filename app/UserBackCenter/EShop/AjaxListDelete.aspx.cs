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

namespace UserBackCenter.EShop
{
    public partial class AjaxListDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = HttpContext.Current.Request["DataType"]; //News,Resources,Guide
            string Id = HttpContext.Current.Request["Id"];
            string result = null;

            if (!string.IsNullOrEmpty(Id))
            {
                if (!string.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "News":
                            if (EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().Delete(Id))
                            {
                                result = "操作成功！";

                            }
                            else
                            {
                                result = "操作失败！";
                            }
                            break;
                        case "Resources":
                            if (EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().Delete(Id))
                                result = "操作成功！";
                            else
                                result = "操作失败！";                           
                            break;
                        case "Guide":
                            if (EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().Delete(Id))
                                result = "操作成功！";
                            else
                                result = "操作失败！";
                            break;
                        default:
                            result = "操作错误！";
                            break;
                    }
                }
                else
                {
                    result = "操作错误！";
                }
            }
            else
            {
                result = "操作错误！";
            }
            HttpContext.Current.Response.Write(result);
            HttpContext.Current.Response.End();
        }
    }
}
