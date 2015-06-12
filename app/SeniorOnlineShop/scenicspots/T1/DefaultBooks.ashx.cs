/*Author:汪奇志 2010-12-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using System.Text;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景点高级网店模板1首页攻略
    /// </summary>
    public class DefaultBooks : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string cid = Utils.GetString(context.Request.QueryString["cid"], "");
            EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)Utils.GetInt(context.Request.QueryString["bookType"]);

            if (string.IsNullOrEmpty(cid)) return;

            string s = string.Empty;

            if (type == EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿)
            {
                s = this.GetGuideTypeStay(cid);
            }
            else
            {
                s = this.GetGuideType(cid, type);
            }

            context.Response.Write(s);
        }

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region private members
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetGuideTypeName(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type)
        {
            string s = string.Empty;

            switch (type)
            {
                case EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略: s = "攻略"; break;
                case EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物: s = "购物"; break;
                case EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通: s = "交通"; break;
                case EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食: s = "美食"; break;
                case EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿: s = "住宿"; break;
            }

            return s;
        }

        /// <summary>
        /// 获取攻略、美食、交通、购物内容
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetGuideType(string cid, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type)
        {
            StringBuilder l = new StringBuilder();
            StringBuilder r = new StringBuilder();
            var items = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(10, cid, (int)type, null);

            l.Append("<ul class=\"sidebar02_2Content_L\">");
            r.Append("<ul class=\"sidebar02_2Content_R\">");

            if (items != null && items.Count > 0)
            {
                int i = 0;

                foreach (var item in items)
                {
                    if (i < 5)
                    {
                        l.AppendFormat("<li><a href=\"{0}\" title=\"{4}\"><font class=\"C_blue\">[{1}]</font> {2}</a> <em>{3}</em></li>", Utils.GenerateShopPageUrl2("/scenicinfodetail_" + item.ID + "_5", cid)
                            , this.GetGuideTypeName(type)
                            , Utils.GetText2(item.Title, 13, true)
                            , item.IssueTime.ToString("yyyy-MM-dd")
                            , item.Title);
                    }
                    else
                    {
                        r.AppendFormat("<li><a href=\"{0}\" title=\"{4}\"><font class=\"C_blue\">[{1}]</font> {2}</a> <em>{3}</em></li>", Utils.GenerateShopPageUrl2("/scenicinfodetail_" + item.ID + "_5", cid)
                            , this.GetGuideTypeName(type)
                            , Utils.GetText2(item.Title, 13, true)
                            , item.IssueTime.ToString("yyyy-MM-dd")
                            , item.Title);
                    }

                    i++;
                }
            }
            else
            {
                return this.GetEmptyResponse("暂无相关内容");
            }


            l.Append("</ul>");
            r.Append("</ul>");

            return l.ToString() + r.ToString() + "<div class=\"clearboth\"></div>";
        }

        /// <summary>
        /// 获取住宿内容
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private string GetGuideTypeStay(string cid)
        {
            StringBuilder s = new StringBuilder();
            var items = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(4, cid, (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿, null);
            s.Append("<ol>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<li><div><a href=\"{0}\" title=\"{3}\"><img src=\"{1}\" /><br>{2}</a></div></li>", Utils.GenerateShopPageUrl2("/scenicinfodetail_" + item.ID + "_5", cid)
                        , Domain.FileSystem + item.ImagePath
                        , Utils.GetText2(item.Title, 8, true)
                        , item.Title);
                }
            }
            else
            {
                return this.GetEmptyResponse("暂无相关内容");
            }
            s.Append("<div class=\"clearboth\"></div>");
            s.Append("</ol>");
            s.Append("<div class=\"clearboth\"></div>");

            return s.ToString();
        }

        /// <summary>
        /// 无内容时输出
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        private string GetEmptyResponse(string msg)
        {
            return string.Format("<ul class=\"clearboth\"><li style=\"text-align:left;\" class=\"c999\">{0}</li></ul>", msg);
        }
        #endregion
    }
}
