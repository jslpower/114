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
    /// 页面功能：SMS短语编辑页
    /// 开发人：xuty 开发时间：2010-08-04
    /// </summary>
    public partial class PhraseEdit : BackPage
    {
        EyouSoft.IBLL.SMSStructure.ITemplate tempBll;
        protected string isAdd = "add";//添加或修改操作
        protected string phraseId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            tempBll = EyouSoft.BLL.SMSStructure.Template.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            if (method == "")
                method = Utils.GetFormValue("method");
            if (method == "addClass")
            {
                AddClass();//添加类
                return;
            }
            if (method == "update" || method == "add")
            {
                AddOrUpdatePhrase(method);//添加或修改客户
                return;
            }
            if (method == "delClass")
            {
                DeleteClass();//删除类
                return;
            }
            LoadData();//初始化数据
        }

        #region 删除短语类别
        protected void DeleteClass()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起，你尚未审核通过!");
                return;
            }
            int classId = Utils.GetInt(Utils.GetQueryStringValue("classid"), -1);
            if (tempBll.DeleteCategory(classId))
            {
                Utils.ResponseMegSuccess();
            }
            else
            {
                Utils.ResponseMeg(false, "删除失败!");
            }
        }
        #endregion

        #region 添加类别
        protected void AddClass()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起，你尚未审核通过!");
                return;
            }
            string className = Utils.InputText(Server.UrlDecode(Request.QueryString["classname"] ?? "")).Trim();
            EyouSoft.Model.SMSStructure.TemplateCategoryInfo cateModel = new EyouSoft.Model.SMSStructure.TemplateCategoryInfo();
            cateModel.CategoryName =className;
            cateModel.CompanyId = SiteUserInfo.CompanyID;
            cateModel.UserId = SiteUserInfo.ID;
            int cateId = tempBll.InsertCategory(cateModel);
            if (cateId > 0)
            {
                Utils.ResponseMeg(true, cateId.ToString());
            }
            else
            {
                Utils.ResponseMeg(false, "添加失败!");
            }
        }
        #endregion

        #region 添加或修改短语
        protected void AddOrUpdatePhrase(string method)
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string content = Utils.InputText(Server.UrlDecode(Request.Form["content"] ?? "")).Trim();
            string cateName = Utils.InputText(Server.UrlDecode(Request.Form["catename"] ?? "")).Trim();
            int cateId = Utils.GetInt(Request.Form["cateid"]);
            string phraseid = Utils.GetFormValue("phraseid");
            EyouSoft.Model.SMSStructure.TemplateInfo tempModel = new EyouSoft.Model.SMSStructure.TemplateInfo();
            if (method == "update")//修改短语时获取原短语实体
            {
                tempModel = tempBll.GetTemplateInfo(phraseid);
            }
            tempModel.CategoryId = cateId;
            tempModel.CategoryName = cateName;
            tempModel.CompanyId = SiteUserInfo.CompanyID;
            tempModel.Content =content;
            tempModel.UserId = SiteUserInfo.ID;
            bool isSuccess = true;
            if (method == "update")
            {
                isSuccess = tempBll.UpdateTemplate(tempModel);//更新到数据库
            }
            else if (method == "add")
            {
                isSuccess = tempBll.InsertTemplate(tempModel);//添加到数据库
            }
            if (isSuccess)
            {
                Utils.ResponseMegSuccess();
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion

        #region 初始化信息
        protected void LoadData()
        {  
            //绑定短语类型
            pe_selPhraseClass.DataTextField = "CategoryName";
            pe_selPhraseClass.DataValueField = "CategoryId";
            pe_selPhraseClass.DataSource = tempBll.GetCategorys(SiteUserInfo.CompanyID);
            pe_selPhraseClass.DataBind();
            pe_selPhraseClass.Items.Insert(0, new ListItem("选择类别", ""));
            string pId = Utils.GetQueryStringValue("phraseid");
            if (pId != "")
            {
                EyouSoft.Model.SMSStructure.TemplateInfo tempModel = tempBll.GetTemplateInfo(pId);
                if (tempModel != null)
                {
                    isAdd = "update";
                    phraseId = pId;
                    pe_selPhraseClass.Value = tempModel.CategoryId.ToString();
                    pe_txtContent.Value = tempModel.Content;
                }
            }
        }
        #endregion
    }
}
