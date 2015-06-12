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

namespace UserBackCenter
{
    /// <summary>
    /// 旅行社后台首页广告详细页
    /// 罗丽娥   2010-07-30
    /// </summary>
    public partial class AdvDetail : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string AfficheID = EyouSoft.Common.Utils.InputText(Request.QueryString["AfficheID"]);
                if (!String.IsNullOrEmpty(AfficheID) && EyouSoft.Common.Utils.GetInt(AfficheID) != 0)
                {
                    EyouSoft.Model.SystemStructure.Affiche model = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetModel(int.Parse(AfficheID));
                    if (model != null)
                    {
                        this.ltrAdvTitle.Text = model.AfficheTitle;
                        this.ltrAdvContent.Text = model.AfficheInfo;
                    }
                    model = null;
                }
                else {
                    this.ltrAdvContent.Text = "对不起，未找到您要查看的广告信息!";
                }
            }
        }
    }
}
