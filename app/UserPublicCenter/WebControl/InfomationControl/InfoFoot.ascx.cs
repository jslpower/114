using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;

namespace UserPublicCenter.WebControl.InfomationControl
{
    /// <summary>
    /// 四种新闻类型列表显示控件
    /// mk 2010-4-1
    /// </summary>
    public partial class InfoFoot : UserControl
    {
        protected string TourTraffic = "";//旅游交通
        protected string HotelLive = "";//酒店住宿
        protected string TourGonglv = "";//旅游攻略
        protected string TourZhanHui = "";//旅游展会
        protected string MoreUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TourTraffic = GetList(4);//获取旅游交通
                HotelLive = GetList(3);//获取酒店住宿
                TourGonglv = GetList(10);//获取旅游攻略
                TourZhanHui = GetList(7);//获取旅游展会
            }
        }

        /// <summary>
        /// 获取点击更多时的url链接
        /// </summary>
        /// <param name="typeId">类型编号</param>
        /// <returns></returns>
        protected string GetMoreUrl(int typeId)
        {
            return EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(typeId);
        }

        /// <summary>
        /// 获得信息列表
        /// </summary>
        private string GetList(int typeId)
        {
            string result = "";
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> newsList = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance().GetListByNewType(5, typeId);
            if (newsList != null && newsList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in newsList)
                {
                    if (item.GotoUrl.Length > 0)
                    {
                        strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\">{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 14));
                    }
                    else
                    {
                        strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\">{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(typeId, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 14));
                    }
                }
                result = strAdvList.ToString();
            }
            else
            {
                result = string.Format("<li>暂无{0}信息</li>", GetTypeName(typeId));
            }
            return result;
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="typeId">类型编号</param>
        /// <returns></returns>
        private string GetTypeName(int typeId)
        {
            string typeName = "";
            switch (typeId)
            {
                case 4:
                    typeName = "旅游交通";
                    break;
                case 3:
                    typeName = "酒店住宿";
                    break;
                case 10:
                    typeName = "旅游攻略";
                    break;
                case 7:
                    typeName = "旅游展会";
                    break;
                default:
                    break;
            }
            return typeName;
        }
    }
}
