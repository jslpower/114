using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using EyouSoft.BLL.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 团队状态修改
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TourOrderStatusChange : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            //团号
            string tourId = Utils.GetQueryStringValue("tourId");
            //状态
            int intStatus = Utils.GetInt(Utils.GetQueryStringValue("intStatus"));
            if (intStatus > 0 && tourId.Length > 0)
            {
                context.Response.Write(BTourList.CreateInstance().TourOrderStatusChange(
                    (EyouSoft.Model.NewTourStructure.TourOrderStatus)intStatus,
                    tourId));
            }
            else
            {
                context.Response.Write("false");
            }
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
