using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
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
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：ajax获取SMS常用短语列表
    /// 开发人：xuty 开发时间：2010-08-04
    /// </summary>
    public partial class AjaxPhraseList : EyouSoft.Common.Control.BasePage
    {
        protected int pageIndex;
        protected int pageSize = 10;
        protected int recordCount;
        EyouSoft.IBLL.SMSStructure.ITemplate tempBll;
        protected void Page_Load(object sender, EventArgs e)
        {   
            tempBll= EyouSoft.BLL.SMSStructure.Template.CreateInstance();
            string method = Utils.GetFormValue("method");
            if (method == "delete")
            {
                if (!IsCompanyCheck)//是否审核通过
                {
                    Utils.ResponseMeg(false, "");
                    return;
                }
                DelPhrase();//删除短语
            }
            GetPhraseList();//获取短语
        }

        #region 删除短语
        protected void DelPhrase()
        {
            string pid = Utils.GetFormValue("pids");//获取要删除的短语Id集合
            pid = pid.TrimEnd(',');
            string[] pidArray=pid.Split(',');
            foreach(string id in pidArray)
            {
                tempBll.DeleteTemplate(id);//执行短语删除
            }
        }
        #endregion

        #region 获取常用短语列表
        protected void GetPhraseList()
        {
            pageIndex = Utils.GetInt(Request.Form["Page"], 1);
            string keyword = Utils.InputText(Server.UrlDecode(Request.Form["keyword"] ?? "")).Trim(); ;//查询关键字
            int phraseClass = Utils.GetInt(Server.UrlDecode(Request.Form["phraseclass"]??""),-1);//查询短语类别
            IList<EyouSoft.Model.SMSStructure.TemplateInfo> templateList = tempBll.GetTemplates(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyID,keyword, phraseClass);
            if (templateList != null && templateList.Count > 0)
            {
                apl_rptPhraseList.DataSource = templateList;
                apl_rptPhraseList.DataBind();
                BindPage();
            }
            else
            {
                apl_rptPhraseList.EmptyText = "暂无短语信息";
                this.ExportPageInfo1.Visible = false;
            }
            templateList = null;
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion
    }
}
