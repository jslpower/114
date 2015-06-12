using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using EyouSoft.Common;
using Newtonsoft.Json.Converters;
namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// 团队列表页日历获的子团信息
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetTourChildrens : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/javascript";
            string RouteId = EyouSoft.Common.Utils.InputText(context.Request.QueryString["RouteId"]);
            string callback = Utils.InputText(context.Request.QueryString["callback"]);
            if (!string.IsNullOrEmpty(RouteId))
            {
                context.Response.Clear();
                IsoDateTimeConverter isoDate = new IsoDateTimeConverter();
                isoDate.DateTimeFormat = "yyyy-MM-dd";
                context.Response.Write(";" + callback + "(" + JsonConvert.SerializeObject(this.GetChildrens(RouteId), isoDate) + ")");

                context.Response.End();
            }

        }
        /// <summary>
        /// 获的子团信息 
        /// </summary>
        /// <param name="TourId">模板团ID</param>
        protected IList<EyouSoft.Model.NewTourStructure.MPowderList> GetChildrens(string RouteId)
        {
            IList<EyouSoft.Model.NewTourStructure.MPowderList> Powderlist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteId).OrderBy(m => m.LeaveDate).ToList();
            IList<EyouSoft.Model.NewTourStructure.MPowderList> CalendarChild = this.filterChildren(Powderlist);
            return CalendarChild;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 日历子团信息
        /// 取有用到的数据，没登录时，过滤掉同行价
        /// </summary>
        protected IList<EyouSoft.Model.NewTourStructure.MPowderList> filterChildren(IList<EyouSoft.Model.NewTourStructure.MPowderList> OldChildrens)
        {
            IList<EyouSoft.Model.NewTourStructure.MPowderList> NewChildrens = new List<EyouSoft.Model.NewTourStructure.MPowderList>();
            foreach (EyouSoft.Model.NewTourStructure.MPowderList Model in OldChildrens)
            {
                EyouSoft.Model.NewTourStructure.MPowderList item = new EyouSoft.Model.NewTourStructure.MPowderList();
                item.TourNo = Model.TourNo;
                item.TourId = Model.TourId;
                item.LeaveDate = Model.LeaveDate;
                item.RetailAdultPrice = Model.RetailAdultPrice;
                item.SaveNum = Model.SaveNum;
                item.MoreThan = Model.MoreThan;
                item.IsLimit = Model.IsLimit;
                item.PowderTourStatus = Model.PowderTourStatus;
                item.TourNum = Model.TourNum;
                NewChildrens.Add(item);
                item = null;
            }
            return NewChildrens;
        }

    }
}
