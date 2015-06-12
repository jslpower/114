using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——基础数据维护新增处理程序页
    /// 开发人：杜桂云      开发时间：2010-07-1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AddBasicDataByAjax : IHttpHandler
    {

        #region 处理请求
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string AreaName = "";//用户输入的主题名称
            int OrderID = 0;
            int typeID = 0;  //线路区域类型：0：线路主题； 1：景点主题； 2：报价等级；3：客户等级
            //获取请求页面传递过来的参数
            if (Utils.InputText(HttpContext.Current.Server.UrlDecode(context.Request.QueryString["AreaName"])) != null)
            {
                AreaName = Utils.InputText(HttpContext.Current.Server.UrlDecode(context.Request.QueryString["AreaName"]));
            }

            if (context.Request.QueryString["typeID"] != null)
            {
                typeID = int.Parse(context.Request.QueryString["typeID"]);
            }
            switch (typeID)
            {
                case 0:
                    //线路主题
                    break;
                case 1:
                    //景点主题
                    break;
                case 2:
                    //报价等级
                    break;
                default:
                    //客户等级
                    break;
            }
            //EyouSoft.Bll.SystemStructure.SysArea sBll = new EyouSoft.Bll.SystemStructure.SysArea();
            //EyouSoft.Model.SystemStructure.SysArea model = new  EyouSoft.Model.SystemStructure.SysArea model();
            int reInt = 1;
            //插入信息，失败返回0，成功返回1
            // reInt = sBll.Insert(model);
            //model = null;
            //sBll = null;
            context.Response.Write(reInt.ToString());
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
