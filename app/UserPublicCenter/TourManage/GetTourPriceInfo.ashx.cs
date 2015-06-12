using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.SSOComponent.Entity;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// (日历)获的团队的报价明细
    /// </summary>
    public class GetTourPriceInfo : EyouSoft.ControlCommon.Control.BaseAshx, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            base.BaseInit(context);
            context.Response.ContentType = "text/plain";
            string TourId = EyouSoft.Common.Utils.InputText(context.Request.QueryString["TourId"]);
            string RemnantNumber = EyouSoft.Common.Utils.InputText(context.Request.QueryString["RemnantNumber"]);
            string leaveDate = EyouSoft.Common.Utils.InputText(context.Request.QueryString["leaveDate"]);
            string callback = context.Request.QueryString["callback"];
            string type = context.Request.QueryString["onlyprice"];
            if (type == "yes")
            {
                string routeId = context.Request.QueryString["RouteId"];
                MPowderList PowderModel = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(TourId);
                string Price = string.Empty;//市场价
                if (PowderModel != null)
                {
                    Price = "成人价 " + EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(PowderModel.RetailAdultPrice) + "元/人起";
                }
                else
                {
                    Price = "电询";
                }
                context.Response.Write("[{ \"price\":\"" + Price + "\"}]");
            }
            else
            {
                context.Response.Write(";" + callback + "([{PriceInfo:'" + this.GetPriceInfo(TourId, RemnantNumber, leaveDate) + "'}])");
            }
            context.Response.End();
        }


        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// 转换价格等级数据，方便绑定浮动价格等级
        /// </summary>
        public class CompanyPriceDetail
        {
            /// <summary>
            /// 价格等级编号
            /// </summary>
            public string PriceStandId { get; set; }
            /// <summary>
            /// 价格等级名称
            /// </summary>
            public string PriceStandName { get; set; }
            /// <summary>
            /// 同行-成人价
            /// </summary>
            public decimal AdultPrice1 { get; set; }
            /// <summary>
            /// 同行-儿童价
            /// </summary>
            public decimal ChildrenPrice1 { get; set; }
            /// <summary>
            /// 同行-单房差[结算价]
            /// </summary>
            public decimal SingleRoom1 { get; set; }

            /// <summary>
            /// 门市-成人价
            /// </summary>
            public decimal AdultPrice2 { get; set; }
            /// <summary>
            /// 门市-儿童价
            /// </summary>
            public decimal ChildrenPrice2 { get; set; }
            /// <summary>
            /// 门市-单房差
            /// </summary>
            public decimal SingleRoom2 { get; set; }

        }
        /// <summary>
        /// 获的团队的价格明细
        /// </summary>
        /// <param name="TourId">团队ID</param>
        /// <param name="RemnantNumber">剩于人数</param>
        /// <param name="leaveDate">出团日期</param>
        protected string GetPriceInfo(string TourId, string RemnantNumber, string leaveDate)
        {
            StringBuilder strPrice = new StringBuilder();
            if (IsLogin)
            {
                strPrice.AppendFormat("<strong>{0}&nbsp;&nbsp;&nbsp;&nbsp;团队剩余{1}人</strong>", leaveDate.Split(",".ToCharArray())[0] + "月" + leaveDate.Split(",".ToCharArray())[1] + "日", RemnantNumber);
                if (!string.IsNullOrEmpty(TourId))
                {
                    IList<EyouSoft.Model.TourStructure.TourPriceDetail> PriceLists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail(TourId);
                    List<CompanyPriceDetail> newLists = new List<CompanyPriceDetail>();
                    CompanyPriceDetail cpModel = null;
                    if (PriceLists != null && PriceLists.Count > 0)
                    {
                        ((List<EyouSoft.Model.TourStructure.TourPriceDetail>)PriceLists).ForEach(item =>
                        {
                            cpModel = new CompanyPriceDetail();
                            if (!string.IsNullOrEmpty(item.PriceStandName))
                            {
                                cpModel.PriceStandName = item.PriceStandName;
                            }
                            else
                            {
                                cpModel.PriceStandName = "常规";
                            }
                            ((List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)item.PriceDetail).ForEach(childItem =>
                            {
                                switch (childItem.CustomerLevelType)
                                {
                                    case EyouSoft.Model.CompanyStructure.CustomerLevelType.同行:
                                        cpModel.AdultPrice1 = childItem.AdultPrice;
                                        cpModel.ChildrenPrice1 = childItem.ChildrenPrice;
                                        break;
                                    case EyouSoft.Model.CompanyStructure.CustomerLevelType.门市:
                                        cpModel.AdultPrice2 = childItem.AdultPrice;
                                        cpModel.ChildrenPrice2 = childItem.ChildrenPrice;
                                        break;
                                    case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                        cpModel.SingleRoom1 = childItem.AdultPrice;
                                        cpModel.SingleRoom2 = childItem.ChildrenPrice;
                                        break;
                                }
                            });

                            newLists.Add(cpModel);

                            cpModel = null;
                        });
                        if (newLists != null && newLists.Count > 0)
                        {
                            string isDisplay = "";
                            if (!IsCompanyCheck)
                            {
                                isDisplay = " style=\"display:none\"";
                            }
                            strPrice.AppendFormat("<table><tr><td align=\"center\">标准</td><td align=\"left\">门市价(成人/儿童)</td><td align=\"left\" {0} >同行价(成人/儿童)</td></tr>", isDisplay);
                            for (int i = 0; i < newLists.Count; i++)
                            {
                                strPrice.AppendFormat("<tr><td align=\"center\">{0}</td><td align=\"center\">{1}元</td><td align=\"center\" {3}>{2}元</td></tr>", newLists[i].PriceStandName, newLists[i].AdultPrice2.ToString("F0") + "/" + newLists[i].ChildrenPrice2.ToString("F0"), newLists[i].AdultPrice1.ToString("F0") + "/" + newLists[i].ChildrenPrice1.ToString("F0"), isDisplay);
                            }
                            strPrice.Append("</table>");
                        }
                        newLists = null;
                    }
                    PriceLists = null;
                    strPrice.Append("<div>请点剩于人数进行预定</div>");
                }
            }
            return strPrice.ToString();
        }
    }
}
