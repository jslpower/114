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
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;


namespace UserBackCenter.EShop
{
    /// <summary>
    /// 上传轮换图片
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// 
    public partial class RotationPicManage :EyouSoft.Common.Control.BasePage
    {
        protected string CompanyId = string.Empty;
        protected string OpeaId = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            CompanyId = this.SiteUserInfo.CompanyID;
            
            if(!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> list = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5,CompanyId);
            if (list != null && list.Count>0)
            {
                string ShowImgPath = "<a href='{0}'  target='_blank' id=\"{1}\"  title=\"点击查看\">查看原图</a>";
                string id = "";
                for (int i = 1; i <= list.Count; i++)
                {
                    ((TextBox)Page.FindControl("txtLinkAddress" + i.ToString())).Text = list[i - 1].LinkAddress;
                    ((TextBox)Page.FindControl("txtSort" + i.ToString())).Text = list[i - 1].SortID.ToString();
                    ((HtmlInputHidden)(Page.FindControl("hdfAgoImgPath" + i.ToString()))).Value = list[i - 1].ImagePath;
                    if(!string.IsNullOrEmpty(list[i-1].ImagePath))
                    {
                    ((Literal)Page.FindControl("ltrUpImagePath" + i.ToString())).Text = string.Format(ShowImgPath,Domain.FileSystem+ list[i - 1].ImagePath,"ago"+i.ToString());                       
                    }
                    id += list[i-1].ID + ",";
                }
                hdfids.Value = id.TrimEnd(',');
            }
            list = null;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> list = new List<EyouSoft.Model.ShopStructure.HighShopAdv>();
            EyouSoft.Model.ShopStructure.HighShopAdv model = null;
            string[] ids = null;
            if (!string.IsNullOrEmpty(hdfids.Value))
            {
                ids = hdfids.Value.Split(',');
            }
            for (int i = 1; i <= 5; i++)
            {
                model = new EyouSoft.Model.ShopStructure.HighShopAdv();
                model.CompanyID = CompanyId;
                model.IssueTime = System.DateTime.Now;
                model.SortID = Utils.GetInt(Utils.InputText(((TextBox)Page.FindControl("txtSort" + i.ToString())).Text));
               string url = url = Utils.InputText(((TextBox)Page.FindControl("txtLinkAddress" + i.ToString())).Text);
                model.LinkAddress = url;
                if (url != "")
                {
                    if (url.IndexOf("http://") < 0)
                    {
                        model.LinkAddress = Utils.InputText("http://" + url);
                    }
                }
                model.OperatorID = this.SiteUserInfo.ID;
                
                list.Add(model);
            }

            string newImgPath1 = Utils.GetFormValue("sfupic1$hidFileName");
            string newImgPath2 = Utils.GetFormValue("sfupic2$hidFileName");
            string newImgPath3 = Utils.GetFormValue("sfupic3$hidFileName");
            string newImgPath4 = Utils.GetFormValue("sfupic4$hidFileName");
            string newImgPath5 = Utils.GetFormValue("sfupic5$hidFileName");
            if (newImgPath1.Length>0)
            {
                list[0].ImagePath = newImgPath1;
            }
            else
            {
                list[0].ImagePath = Utils.GetFormValue(hdfAgoImgPath1.UniqueID);
            }
            if (newImgPath2.Length>0)
            {
                list[1].ImagePath = newImgPath2;
            }
            else
            {
                list[1].ImagePath = Utils.GetFormValue(hdfAgoImgPath2.UniqueID);
            }
            if (newImgPath3.Length>0)
            {
                list[2].ImagePath = newImgPath3;
            }
            else
            {
                list[2].ImagePath = Utils.GetFormValue(hdfAgoImgPath3.UniqueID);
            }
            if (newImgPath4.Length>0)
            {
                list[3].ImagePath = newImgPath4;
            }
            else
            {
                list[3].ImagePath = Utils.GetFormValue(hdfAgoImgPath4.UniqueID);
            }
            if (newImgPath5.Length>0)
            {
                list[4].ImagePath = newImgPath5;
            }
            else
            {
                list[4].ImagePath = Utils.GetFormValue(hdfAgoImgPath5.UniqueID);
            }
            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    list[i].ID = ids[i];
                }
            }
            if (EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().Update(CompanyId, list))
            {
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('操作失败！');");
            }
            list = null;
            model = null;

        }
    }
}
