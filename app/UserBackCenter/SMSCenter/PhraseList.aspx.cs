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
using EyouSoft.Common.Control;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：SMS常用短语页
    /// 开发人：xuty 开发时间：2010-08-04
    /// </summary>
    public partial class PhraseList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPhraseCate();//初始化短信类型
        }
        #region 绑定短信类型
        protected void BindPhraseCate()
        {
            pl_selPhraseClass.DataTextField = "CategoryName";
            pl_selPhraseClass.DataValueField = "CategoryId";
            pl_selPhraseClass.DataSource = EyouSoft.BLL.SMSStructure.Template.CreateInstance().GetCategorys(SiteUserInfo.CompanyID);
            pl_selPhraseClass.DataBind();
            pl_selPhraseClass.Items.Insert(0, new ListItem("选择类型", "-1"));
        }
        #endregion
    }
}
