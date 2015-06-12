using System;
using System.Collections.Generic;
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

namespace UserPublicCenter.SupplierInfo.UserControl
{
    public partial class NewsControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 资讯标题
        /// </summary>
        protected string _contentcss = "xtneibiao";
        protected string ToppicTag = string.Empty;
        protected bool _ismargintop = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (TopicClass.HasValue && TopicArea.HasValue)
            {
                ToppicTag = TopicArea.Value != EyouSoft.Model.CommunityStructure.TopicAreas.旅行社 ? Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicAreas), (int)TopicArea) : "旅行社新闻";
            }
            else if (TopicClass.HasValue)
            {
                ToppicTag = Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicClass), (int)TopicClass);
            }
            if (IsShowMore)
            {
                lbMore.Text = string.Format("<a href=\"/SupplierInfo/{0}?TypeId={1}&AreaId={2}\">更多&gt&gt</a>", (int)TopicClass == 1 ? "ArticleList.aspx" : "SchoolIntroductionList.aspx", TopicClass.HasValue ? (int)TopicClass.Value : -1, TopicArea.HasValue ? (int)TopicArea.Value : -1);
            }
            EyouSoft.IBLL.CommunityStructure.IInfoArticle IBll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = IBll.GetTopNumList(TopNumber, TopicClass, TopicArea, null, string.Empty);
            if (list != null && list.Count > 0)
            {
                rpMain.DataSource = list;
                rpMain.DataBind();
            }
            if (IsMarginTop)
            {
                MainTB.Style.Add("margin-top", "10px");
            }
        }

        /// <summary>
        /// 行业资讯大类别
        /// </summary>
        public EyouSoft.Model.CommunityStructure.TopicClass? TopicClass
        {
            get;
            set;
        }

        /// <summary>
        /// 行业资讯小类别
        /// </summary>
        public EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示更多
        /// </summary>
        public bool IsShowMore
        {
            get;
            set;
        }
        /// <summary>
        /// 需要返回的记录数
        /// </summary>
        public int TopNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 当前栏目是否设置style:margin-top:10px;(默认值为true)
        /// </summary>
        public bool IsMarginTop
        {
            get
            {
                return _ismargintop;
            }
            set
            {
                _ismargintop = value;
            }
        }
        /// <summary>
        /// 外层Div样式
        /// </summary>
        public string ContentCss
        {
            get {
                return _contentcss;
            }
            set {
                _contentcss = value;
            }
        }
    }
}