using System;
using System.Collections;
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
using EyouSoft.Common;
using EyouSoft.Common.DataProtection;
using System.Text;
using System.Collections.Generic;

namespace UserPublicCenter.PlaneInfo
{
    /// <summary>
    /// 页面功能：机票——机票更多连接列表页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class PlaneNewsList : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        protected string titleName="";   //页面头标题前缀
        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 18;   //每页显示的记录数
        protected int TypeID = 0;//0-运价参考；1-帮助信息；2-合作供应商
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            string titleInfo = "";
            string url = "";
            string url2 = "";
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["TypeID"])))
            {
                TypeID = Utils.GetInt(Request.QueryString["TypeID"]);
                url = Utils.GeneratePublicCenterUrl("PlaneListPage.aspx", CityId);
                switch (TypeID)
                {
                    case 0:
                        titleName = "运价参考";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=0", CityId);
                        titleInfo = string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >运价参考</a> ", url, url2);
                        break;
                    case 1:
                        titleName = "帮助信息";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=1", CityId);
                        titleInfo = string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >帮助信息</a> ", url, url2);
                        break;
                    default:
                        titleName = "合作供应商";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=2", CityId);
                        titleInfo = string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >合作供应商</a> ", url, url2);
                        break;
                }
                //绑定数据列表
                BindListInfo(TypeID);
            }
            this.Page.Title = titleName ;
            this.div_Title.InnerHtml = titleInfo;
            this.CityAndMenu1.HeadMenuIndex = 3;
        }
        #endregion

        #region 列表绑定
        protected void BindListInfo(int TypeID)
        {
            int intRecordCount = 0;
            //获取数据集
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = null;
            switch (TypeID)
            {
                case 0://0-运价参考
                    ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, EyouSoft.Model.SystemStructure.AfficheType.运价参考);
                    break;
                case 1://1-帮助信息
                    ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, EyouSoft.Model.SystemStructure.AfficheType.帮助信息);
                    break;
                default://2-合作供应商
                    ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, EyouSoft.Model.SystemStructure.AfficheType.合作供应商);
                    break;
            }
           if (ModelList != null && ModelList.Count > 0)
            {
                //绑定数据源
                this.rpt_NewsListInfo.DataSource = ModelList;
                this.rpt_NewsListInfo.DataBind();
                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;//每页显示记录数
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;// Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.PageLinkURL = "PlaneNewsList.aspx?";
                this.ExporPageInfoSelect1.LinkType = 3;
            }
            //释放资源
           ModelList = null;
        }
        #endregion

        #region 转换时间显示格式
        protected string GetTime(DateTime times)
        {
            return times.ToString("MM/dd hh:mm");
        }
        #endregion

        #region 获取列表标题信息
        protected string ShowTicketInfo(int AffiID, string AffiName)
        {
            string linkUrl = Utils.GetWordAdvLinkUrl(0, AffiID, TypeID, CityId);
            return string.Format("<a title=\"{2}\" href=\"{0}\" target='_blank'>{1}</a>", linkUrl, Utils.GetText(AffiName, 30,true) ,AffiName);
        }
        #endregion
    }
}
