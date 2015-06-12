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
using System.Text;
using System.Collections.Generic;
namespace TourUnion.WEB.IM.TourAgency.ExchangeTopic
{
    /// <summary>
    /// 获的话题信息
    /// </summary>
    public partial class AjaxExchangeList : System.Web.UI.Page
    {
        /// <summary>
        /// 是否是查看热门话题
        /// </summary>
        protected int isHotTopic = 0;
        /// <summary>
        /// 话题类型ID
        /// </summary>
        protected int TopicClassId = -1;
        private int PageSize = 15;
        private int CurrencyPage = 1;
        private TourUnion.Account.Model.UserManage usermanage = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            usermanage = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.None, TourUnion.Account.Enum.SystemMedia.MQ);
            isHotTopic = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["isHotTopic"]);
            TopicClassId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["TopicClassId"]);
            if (!Page.IsPostBack)
            {
                BindTopicList();
            }

        }
        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        protected void BindTopicList()
        {
            int intRecordCount = 0;
            IList<TourUnion.Model.Interaction.TopicInfo> list = new List<TourUnion.Model.Interaction.TopicInfo>();
            int UserId = 0;
            if (usermanage.AccountUser != null)
            {
                UserId = usermanage.AccountUser.Id;
            }
           
            Adpost.Common.ExporPage.PageControlSelect pagecontrol = new Adpost.Common.ExporPage.PageControlSelect(PageSize);
            CurrencyPage = pagecontrol.CurrentPage;

            string Title = Server.UrlDecode(Request.QueryString["Title"]);
            TourUnion.BLL.Interaction.Interaction bll = new TourUnion.BLL.Interaction.Interaction();
 
            if(isHotTopic==1)
            {
                list = bll.GetHotTopics(PageSize, CurrencyPage, ref intRecordCount, Title);
            }
            else
            {
                list = bll.GetTopicsByCategoryId(PageSize, CurrencyPage, ref  intRecordCount, TopicClassId, Title);
            }
            bll = null;
            if (intRecordCount > 0)
            {
                this.NoDate.Visible = false;
                pagecontrol.SetPage(this.ExporPageInfoSelect1, intRecordCount);
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LoadData(this)", 1);
                this.repTopicList.DataSource = list;
                this.repTopicList.DataBind();
                pagecontrol = null;

            }
            else
            {
                this.NoDate.Visible = true;
            }
          
            list = null;
          
        }
        #endregion


        #region 前台方法
        /// <summary>
        ///  显示Title
        /// </summary>
        protected string GetTitle(string ClassId, string ClassName, string Id, string Title, string ToProvinceName, int ReplyCount, int ViewCount)
        {
           StringBuilder strValue = new StringBuilder();

           int MQId = 0;
           string Md5 = "";
           if (usermanage.AccountUser != null)
           {
               MQId = usermanage.AccountUser.ContactMQ;
               Md5 = usermanage.AccountUser.MD5Password;
           }

           TourUnion.Account.Model.Account account = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.LocalAgency, TourUnion.Account.Enum.SystemMedia.MQ).AccountUser;
           TourUnion.Account.Model.MQUrlPar mqurlpar = TourUnion.Account.Model.UserManage.MQUrlParModel;
           string UserNameParName = mqurlpar.IDMyUrlName;
           string Md5ParName = mqurlpar.PwMyUrlName;
           mqurlpar = null;

           string url = "/IM/LoginWeb.aspx?" + UserNameParName + "=" + MQId + "&" + Md5ParName + "=" + Md5 + "&CsToBsRedirectUrl=";
           string strUrl = string.Empty;
           string strTypeUrl = string.Empty;
           if (account.CompanyTypeId == 3)
           {
               strUrl = url + Server.UrlEncode("/LocalAgency/TopicManger/TopicDetails.aspx?TopicId=" + Id + "&TopicClassID=" + ClassId);
               strTypeUrl = url + Server.UrlEncode("/LocalAgency/TopicManger/TopicList.aspx?TopicClassID=" + ClassId);
           }
           else
           {
               strUrl = url + Server.UrlEncode("/TourAgency/ExchangeTopic/TopicDetails.aspx?TopicId=" + Id);
               strTypeUrl = url + Server.UrlEncode("/TourAgency/ExchangeTopic/Default.aspx?ClassId=" + ClassId);
           }
            string strClassName = "";
    
            if (TopicClassId > -1) //无需显示话题类型
            {
                if (ClassId == "4") //显示城市
                {
                    strValue.AppendFormat("<a href=\"javascript:void(0);\"  class=\"hds-3\" >[{0}]</a> <a href=\"{1}\"  title='" + Title + "' target=\"_blank\">{2}</a>", ToProvinceName, strUrl, Title);
                }
                else
                {
                    strValue.AppendFormat("<a href=\"{0}\"  title='" + Title + "' target=\"_blank\">{1}</a>", strUrl, Title);
                }
            }
            else  //显示话题类型
            {

                TourUnion.Cache.Interaction cache = new TourUnion.Cache.Interaction();
                IList<TourUnion.Model.Interaction.CategoryInfo> modelList = cache.GetCategorys();
                foreach (TourUnion.Model.Interaction.CategoryInfo model in modelList)
                {
                    if (model.CategoryId.ToString() == ClassId)
                    {
                        strClassName = model.CategoryStyle;
                        break;
                    }
                }
                modelList = null;

                //显示话题类别
                strValue.AppendFormat("<a href=\"javascript:void(0);\"  class=" + strClassName + " onclick=\"ChangeCss({1});GetTopicList(0,{1},false);\">[{0}]</a>", ClassName, ClassId);
                //显示话题标题
                strValue.AppendFormat("<a href=\"{0}\"  title='" + Title + "' target=\"_blank\">{1}</a>", strUrl, Title);
            }
            strValue.AppendFormat("<span>{0}/{1}</span>", ReplyCount, ViewCount);
            strValue.AppendFormat(" <a href=\"{0}\"  target=\"_blank\">[回复]</a>", strUrl + Server.UrlEncode("&reply=1#replytopic"));
            account = null;
            return strValue.ToString();
        }


        #endregion
    }
}
