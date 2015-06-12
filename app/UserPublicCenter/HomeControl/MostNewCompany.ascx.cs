using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Control;
namespace UserPublicCenter.DatePicker
{
    /// <summary>
    /// 首页最活跃/最新加入的企业
    /// </summary>
    public partial class MostNewCompany : System.Web.UI.UserControl
    {
        private bool _isNew = false;

        /// <summary>
        /// 是否是最新加入的企业 默认为最活跃的企业
        /// </summary>
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }
        protected string strAllCompanyList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetCompanyList();
            }
        }

        /// <summary>
        /// 获的企业列表
        /// </summary>
        protected void GetCompanyList()
        {
             FrontPage page = this.Page as FrontPage;
            int CityId = page.CityId;
            //最活跃的企业
            if (!IsNew)
            {
                IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告最活跃的企业排名);
                if (AdvList != null && AdvList.Count > 0)
                {
                    int AdvCount = AdvList.Count;
                    StringBuilder strAdvList = new StringBuilder();

                    strAdvList.Append("<div class=\"q3div\">");
                    int Number = 1;
                    foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                    {
                        strAdvList.AppendFormat("<span {0}>{3}</span><a href='{1}' style='text-align:left;' target=\"_blank\" class=\"q3a\">&nbsp;{2}</a>", Number < 4 ? "class=\"q3\"" : "", EyouSoft.Common.Utils.SiteAdvUrl(EyouSoft.Common.Utils.GeneratePublicCenterUrl(item.RedirectURL, CityId)), EyouSoft.Common.Utils.GetText(item.Title, 14), Number);
                        Number++;
                    }
                    strAdvList.Append("</div><div class=\"clear\"></div>");
                    strAllCompanyList = strAdvList.ToString();
                }
                else
                {
                    strAllCompanyList = "<div class=\"q3div\"><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\">暂无公司列表</a><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\"></a><a class=\"q3a\"></div>";
                }
            }
            else  //最新加入的企业
            {
                IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告最新加入企业);
                if (AdvList != null && AdvList.Count > 0)
                {
                    int AdvCount = AdvList.Count;
                    StringBuilder strAdvList = new StringBuilder();

                    strAdvList.Append("<ul class=\"newcom\">");
                    foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                    {
                        strAdvList.AppendFormat("<li style='text-align:left;'><img src=\"{0}\" /><a href='{1}' target=\"_blank\" >{2}</a></li>", page.ImageServerUrl + "/Images/UserPublicCenter/newsdot.gif", EyouSoft.Common.Utils.SiteAdvUrl(EyouSoft.Common.Utils.GeneratePublicCenterUrl(item.RedirectURL, CityId)), EyouSoft.Common.Utils.GetText(item.Title, 15));
                    }
                    strAdvList.Append("</ul>");
                    strAllCompanyList = strAdvList.ToString();
                }
                else
                {
                    strAllCompanyList = "<ul class=\"newcom\"><li></li><li></li><li></li><li></li><li>暂无信息</li><li></li><li></li><li></li><li></li></ul>";
                }
            }
        }
    }
}