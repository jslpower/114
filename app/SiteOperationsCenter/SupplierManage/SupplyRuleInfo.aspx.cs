using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.SupplierManage
{
    /// <summary>
    /// 页面功能：运营后台供求新栏目---供求规则管理
    /// CreateTime：2011-05-11
    /// </summary>
    /// Author:liuym
    public partial class SupplyRuleInfo : EyouSoft.Common.Control.YunYingPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.供求信息_供求规则))
                {
                    MessageBox.Show(this.Page, "对不起，你没有该权限");
                    return;
                }
                #region 初始化表单数据
                //获取供求信息集合列表
                IList<EyouSoft.Model.CommunityStructure.MSupplyDemandRule> list = EyouSoft.BLL.CommunityStructure.BSupplyDemandRule.CreateInstance().GetList();
                if (list != null && list.Count > 0)
                {
                    //供求规则头条
                    txtsupTopTitle.Value = list[0].NewsTitle;
                    txtsupTopHref.Value = list[0].LinkAddress;
                    txtsupRuleDescription.Value = list[0].NewsContent;
                    //供求规则一
                    txtRuleTitle1.Value = list[1].NewsTitle;
                    txtRuleHref1.Value = list[1].LinkAddress;
                    //供求规则二
                    txtRuleTitle2.Value = list[2].NewsTitle;
                    txtRuleHref2.Value = list[2].LinkAddress;
                    //供求规则三
                    txtRuleTitle3.Value = list[3].NewsTitle;
                    txtRuleHref3.Value = list[3].LinkAddress;
                    //供求规则四
                    txtRuleTitle4.Value = list[4].NewsTitle;
                    txtRuleHref4.Value = list[4].LinkAddress;
                }
                list = null;
                #endregion
            }
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //供求规则头条
            string supTopTitle = Utils.GetText(txtsupTopTitle.Value, 255);//供求规则头条新闻标题
            string supTopHref = Utils.GetText(txtsupTopHref.Value, 255);//供求规则头条链接地址
            string supRuleDescription = Utils.GetText(txtsupRuleDescription.Value, 1000);//供求规则头条概要
            EyouSoft.Model.CommunityStructure.MSupplyDemandRule model = new EyouSoft.Model.CommunityStructure.MSupplyDemandRule();
            model.NewsTitle = supTopTitle;
            model.LinkAddress = getRuleUrl(supTopHref);
            model.NewsContent = supRuleDescription;
            //供求规则一
            string supRule1 = Utils.GetText(txtRuleTitle1.Value, 255);
            string supHref1 = Utils.GetText(txtRuleHref1.Value, 255);
            EyouSoft.Model.CommunityStructure.MSupplyDemandRule model1 = new EyouSoft.Model.CommunityStructure.MSupplyDemandRule();
            model1.NewsTitle = supRule1;
            model1.LinkAddress = getRuleUrl(supHref1);
            //供求规则二
            string supRule2 = Utils.GetText(txtRuleTitle2.Value, 255);
            string supHref2 = Utils.GetText(txtRuleHref2.Value, 255);
            EyouSoft.Model.CommunityStructure.MSupplyDemandRule model2 = new EyouSoft.Model.CommunityStructure.MSupplyDemandRule();
            model2.NewsTitle = supRule2;
            model2.LinkAddress = getRuleUrl(supHref2);
            //供求规则三
            string supRule3 = Utils.GetText(txtRuleTitle3.Value, 255);
            string supHref3 = Utils.GetText(txtRuleHref3.Value, 255);
            EyouSoft.Model.CommunityStructure.MSupplyDemandRule model3 = new EyouSoft.Model.CommunityStructure.MSupplyDemandRule();            //供求规则四
            model3.NewsTitle = supRule3;
            model3.LinkAddress = getRuleUrl(supHref3);

            //供求规则四
            string supRule4 = Utils.GetText(txtRuleTitle4.Value, 255);
            string supHref4 = Utils.GetText(txtRuleHref4.Value, 255);
            EyouSoft.Model.CommunityStructure.MSupplyDemandRule model4 = new EyouSoft.Model.CommunityStructure.MSupplyDemandRule();            //供求规则四
            model4.NewsTitle = supRule4;
            model4.LinkAddress = getRuleUrl(supHref4);
            List<EyouSoft.Model.CommunityStructure.MSupplyDemandRule> SupDemandList = new List<EyouSoft.Model.CommunityStructure.MSupplyDemandRule>();
            SupDemandList.Add(model);
            SupDemandList.Add(model1);
            SupDemandList.Add(model2);
            SupDemandList.Add(model3);
            SupDemandList.Add(model4);


            //调用保存供求规则信息
            bool result = false;
            result = EyouSoft.BLL.CommunityStructure.BSupplyDemandRule.CreateInstance().Add(SupDemandList);

            if (result)
                MessageBox.ShowAndRedirect(this, "保存成功！", "/SupplierManage/SupplyRuleInfo.aspx");
            else
                MessageBox.ShowAndRedirect(this, "保存失败！", "/SupplierManage/SupplyRuleInfo.aspx");

            #region 释放资源
            model = null;
            model1 = null;
            model2 = null;
            model3 = null;
            model4 = null;
            SupDemandList = null;
            #endregion
        }
        #endregion

        #region 链接Url处理，有【Http;//】就不加，没有就加上
        private string getRuleUrl(string linkUrl)
        {
            string strUrl = string.Empty;
            if (!string.IsNullOrEmpty(linkUrl))
            {
                strUrl = linkUrl.Contains("http://") ? linkUrl : "http://" + linkUrl;
            }
            return strUrl;
        }
        #endregion
    }
}
