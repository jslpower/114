using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    public class Infomation
    {
        /// <summary>
        /// 获取资讯首页URL
        /// </summary>
        /// <returns></returns>
        public static string InfoDefaultUrlWrite()
        {
            return string.Format("{0}/newsDefault", Domain.UserPublicCenter);
        }
        /// <summary>
        /// 获得新闻详细的URL
        /// </summary>
        /// <param name="typeId">新闻类型编号</param>
        /// <param name="newsId">新闻编号</param>
        /// <returns></returns>
        public static string GetNewsDetailUrl(int typeId, int newsId)
        {
            return string.Format("{0}/newsDetail_{1}_{2}", Domain.UserPublicCenter, typeId, newsId);
        }

        /// <summary>
        /// 获得新闻列表
        /// </summary>
        /// <param name="typeId">新闻类型编号</param>
        /// <returns></returns>
        public static string GetNewsListUrl(int typeId)
        {
            return string.Format("{0}/newsList_{1}", Domain.UserPublicCenter, typeId);
        }

        /// <summary>
        /// 获取其他列表
        /// </summary>
        /// <param name="tagId">tagId</param>
        /// <returns></returns>
        public static string GetOtherListUrl(int tagId)
        {
            return string.Format("{0}/otherList_{1}", Domain.UserPublicCenter, tagId);
        }

        /// <summary>
        /// 获得新闻列表（专题）
        /// </summary>
        /// <param name="typeId">新闻类型编号</param>
        /// <returns></returns>
        public static string GetNewsListZhuanTiUrl(int typeId)
        {
            return string.Format("{0}/newsListZhuanTi_{1}", Domain.UserPublicCenter, typeId);
        }

        /// <summary>
        /// 获得新闻列表（区域）
        /// </summary>
        /// <param name="typeId">线路区域编号</param>
        /// <returns></returns>
        public static string GetNewsListAreaUrl(int tourAreaId)
        {
            return string.Format("{0}/newsListQuYu_{1}", Domain.UserPublicCenter, tourAreaId);
        }

        /// <summary>
        /// 获得新闻线路
        /// </summary>
        /// <param name="typeId">线路区域编号</param>
        /// <returns></returns>
        public static string GetNewsListRouteUrl(int tourAreaId)
        {
            return string.Format("{0}/newsXianLu_{1}", Domain.UserPublicCenter, tourAreaId);
        }

        /// <summary>
        /// 获得新闻线路（分页）
        /// </summary>
        /// <param name="typeId">线路区域编号</param>
        /// <returns></returns>
        public static string GetNewsListRoutePageUrl(int tourAreaId, int pageId)
        {
            return string.Format("{0}/newsXianLu_{1}_{2}", Domain.UserPublicCenter, tourAreaId, pageId);
        }

        /// <summary>
        /// 获取tag列表页URL
        /// </summary>
        /// <returns></returns>
        public static string GetTagListUrl()
        {
            return string.Format("{0}/newsTagList", Domain.UserPublicCenter);
        }
    }
}
