using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewsStructure;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    ///  获取资讯列表
    ///  创建时间：2011-12-14 方琪
    /// </summary>
    public partial class AjaxGetInformationList : EyouSoft.Common.Control.YunYingPage
    {
        #region 参数
        /// <summary>
        /// 同业MQ
        /// </summary>
        protected string Mq = string.Empty;

        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 20;

        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;

        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);//获取分页序号
            if (!Page.IsPostBack)
            {
                BindInformationList();
            }
        }

        #region 绑定资讯列表
        /// <summary>
        /// 绑定资讯列表
        /// </summary>
        protected void BindInformationList()
        {
            int recordCount = 0;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");// 关键字查询
            string InfoType = Utils.GetQueryStringValue("InfoType"); // 分类
            //实例化一个搜索实体，赋值
            EyouSoft.Model.NewsStructure.MQueryPeerNews QueryModel =
                new EyouSoft.Model.NewsStructure.MQueryPeerNews();
            QueryModel.KeyWord = KeyWord;
            if (InfoType != "-1"&& InfoType !="")
            {
                QueryModel.TypeId = (PeerNewType)Utils.GetInt(InfoType);
            }
            IList<EyouSoft.Model.NewsStructure.MPeerNews> PeerNewList = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetGetPeerNewsList(PageSize, PageIndex, ref recordCount, QueryModel);
            if (PeerNewList != null && PeerNewList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "Informationindustry.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "Informationindustry.LoadData(this);", 0);
                this.repInformationList.DataSource = PeerNewList;
                this.repInformationList.DataBind();
            }
            else
            {
                //如果查询结果为空，就显示暂无资讯信息
                StringBuilder sbtext = new StringBuilder();
                sbtext.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\" id=\"tbInformationList\">");
                sbtext.Append("<tr> <th nowrap=\"nowrap\"> 序号 </th><th nowrap=\"nowrap\"> 标题 </th> <th nowrap=\"nowrap\"> 操作单位</th><th nowrap=\"nowrap\">类别</th><th nowrap=\"nowrap\">发布时间</th><th nowrap=\"nowrap\">发布人</th><th nowrap=\"nowrap\">点击量</th><th nowrap=\"nowrap\">操作</th></tr>");
                sbtext.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无同业资讯信息</td></tr>");
                sbtext.Append("</table>");
                this.repInformationList.EmptyText = sbtext.ToString();
            }
        }
        #endregion

        #region 序号
        /// <summary>
        /// 序号（根据当前页码 和页大小计算出序号）
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
        #endregion

        #region 根据公司编号获取
        /// <summary>
        /// 根据公司编号获取MQ
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>MQ洽谈</returns>
        protected string GetMq(string CompanyId)
        {
            return Utils.GetMQ(EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId).ContactInfo.MQ);
        }
        #endregion

        #region 根据公司编号获取网店
        /// <summary>
        /// 根据公司编号获取网店RURL
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>网店RURL</returns>
        protected string GetShopURL(string CompanyId)
        {
            return Utils.GetShopUrl(CompanyId);
        }
        #endregion
    }
}
