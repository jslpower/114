using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
namespace TourUnion.WEB.IM.TourAgency.ExchangeTopic
{
    /// <summary>
    /// IM端同业互动链接
    /// </summary>
    public partial class TopicMenu : System.Web.UI.Page
    {
        protected string strAllClass = "";
        protected string _pageNameLink = "";
        protected string mqidurlpar = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TourUnion.Account.Model.MQUrlPar mqurlpar = TourUnion.Account.Model.UserManage.MQUrlParModel;
                mqidurlpar = mqurlpar.IDMyUrlName;
                mqurlpar = null;
                BindAllTopicClass();
            }

        }

        #region 绑定话题类型
        protected void BindAllTopicClass()
        {
            TourUnion.BLL.Interaction.Interaction bll = new TourUnion.BLL.Interaction.Interaction();
            IList<TourUnion.Model.Interaction.CategoryInfo> list = bll.GetCategorys();
            int index = 0;
            //直接链到BS端
             foreach (TourUnion.Model.Interaction.CategoryInfo model in list)
            {
                 strAllClass += string.Format("<a href=\"javascript:void(0);\" onclick=\"ChangeCss(this);GetUrl({0})\" >{1}</a> | ", model.CategoryId, model.CategoryName);
                index++;
                if(index==2)
                {
                   strAllClass=strAllClass.Substring(0,strAllClass.LastIndexOf("|"));
                   strAllClass += "</br>";
                }
            }
            strAllClass = strAllClass.Substring(0, strAllClass.LastIndexOf("|"));
            list = null;
            bll = null;
        }
        #endregion 
    }
}
