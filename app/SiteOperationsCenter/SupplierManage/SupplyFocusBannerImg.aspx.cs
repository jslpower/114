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
    ///页面功能：运营后台- 供求首页（五张）焦点图片管理
    /// CreateDate:2011-05-11
    /// </summary>
    /// Author:liuym
    public partial class SupplyFocusBannerImg : EyouSoft.Common.Control.YunYingPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                //权限验证
                if (!CheckMasterGrant(YuYingPermission.供求信息_供求首页焦点图))
                {
                    MessageBox.Show(this.Page, "对不起，你没有该权限");
                    return;
                }

                #region 初始化表单数据
                IList<EyouSoft.Model.CommunityStructure.MSupplyDemandPic> list = EyouSoft.BLL.CommunityStructure.BSupplyDemandPic.CreateInstance().GetList();
                if (list != null && list.Count > 0)
                {
                    txtFocusImgHref1.Value = string.IsNullOrEmpty(list[0].LinkAddress) ? "" : list[0].LinkAddress;
                    if (!string.IsNullOrEmpty(list[0].PicPath))
                    {
                        lblImg1.Text = string.Format("<a href='{0}' title='点击查看原图' target='_blank'>{1}</a>", Domain.FileSystem + list[0].PicPath, "查看");
                        hdfoldimgpath1.Value = list[0].PicPath;
                    }

                    txtFocusImgHref2.Value = string.IsNullOrEmpty(list[1].LinkAddress) ? "" : list[1].LinkAddress;
                    if (!string.IsNullOrEmpty(list[1].PicPath))
                    {
                        lblImg2.Text = string.Format("<a href='{0}' title='点击查看原图' target='_blank'>{1}</a>", Domain.FileSystem + list[1].PicPath, "查看");
                        hdfoldimgpath2.Value = list[1].PicPath;
                    }

                    txtFocusImgHref3.Value = string.IsNullOrEmpty(list[2].LinkAddress) ? "" : list[2].LinkAddress;
                    if (!string.IsNullOrEmpty(list[2].PicPath))
                    {
                        lblImg3.Text = string.Format("<a href='{0}' title='点击查看原图' target='_blank'>{1}</a>", Domain.FileSystem + list[2].PicPath, "查看");
                        hdfoldimgpath3.Value = list[2].PicPath;
                    }

                    txtFocusImgHref4.Value = string.IsNullOrEmpty(list[3].LinkAddress) ? "" : list[3].LinkAddress;
                    if (!string.IsNullOrEmpty(list[3].PicPath))
                    {
                        lblImg4.Text = string.Format("<a href='{0}' title='点击查看原图' target='_blank'>{1}</a>", Domain.FileSystem + list[3].PicPath, "查看");
                        hdfoldimgpath4.Value = list[3].PicPath;
                    }

                    txtFocusImgHref5.Value = string.IsNullOrEmpty(list[4].LinkAddress) ? "" : list[4].LinkAddress;
                    if (!string.IsNullOrEmpty(list[4].PicPath))
                    {
                        lblImg5.Text = string.Format("<a href='{0}' title='点击查看原图' target='_blank'>{1}</a>", Domain.FileSystem + list[4].PicPath, "查看");
                        hdfoldimgpath5.Value = list[4].PicPath;
                    }
                }
                list = null;
                #endregion
            }
        }
        #endregion

        #region 保存规则焦点图片
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 获取参数
            string imgHref1 = Utils.InputText(getRuleUrl(Utils.GetText(txtFocusImgHref1.Value.Trim(), 255)));
            string imgPath1 = Request.Form["sufLoadImg1$hidFileName"];
            string imgHref2 = Utils.InputText(getRuleUrl(Utils.GetText(txtFocusImgHref2.Value.Trim(), 255)));
            string imgPath2 = Request.Form["sufLoadImg2$hidFileName"];
            string imgHref3 = Utils.InputText(getRuleUrl(Utils.GetText(txtFocusImgHref3.Value.Trim(), 255)));
            string imgPath3 = Request.Form["sufLoadImg3$hidFileName"];
            string imgHref4 = Utils.InputText(getRuleUrl(Utils.GetText(txtFocusImgHref4.Value.Trim(), 255)));
            string imgPath4 = Request.Form["sufLoadImg4$hidFileName"];
            string imgHref5 = Utils.InputText(getRuleUrl(Utils.GetText(txtFocusImgHref5.Value.Trim(), 255)));
            string imgPath5 = Request.Form["sufLoadImg5$hidFileName"];
            #endregion

            #region 声明5个对象 并添加到集合列表
            EyouSoft.Model.CommunityStructure.MSupplyDemandPic Model1 = new EyouSoft.Model.CommunityStructure.MSupplyDemandPic();
            Model1.LinkAddress = imgHref1;
            Model1.OperateTime = DateTime.Now;
            Model1.PicPath =string.IsNullOrEmpty(imgPath1)?hdfoldimgpath1.Value:imgPath1;
           
            EyouSoft.Model.CommunityStructure.MSupplyDemandPic Model2 = new EyouSoft.Model.CommunityStructure.MSupplyDemandPic();
            Model2.LinkAddress = imgHref2;
            Model2.OperateTime = DateTime.Now;
            Model2.PicPath =string.IsNullOrEmpty(imgPath2)?hdfoldimgpath2.Value:imgPath2;
           
            EyouSoft.Model.CommunityStructure.MSupplyDemandPic Model3 = new EyouSoft.Model.CommunityStructure.MSupplyDemandPic();
            Model3.LinkAddress = imgHref3;
            Model3.OperateTime = DateTime.Now;
            Model3.PicPath =string.IsNullOrEmpty(imgPath3)?hdfoldimgpath3.Value:imgPath3;
           
            EyouSoft.Model.CommunityStructure.MSupplyDemandPic Model4 = new EyouSoft.Model.CommunityStructure.MSupplyDemandPic();
            Model4.LinkAddress = imgHref4;
            Model4.OperateTime = DateTime.Now;
            Model4.PicPath =string.IsNullOrEmpty(imgPath4)?hdfoldimgpath4.Value:imgPath4;
  
            EyouSoft.Model.CommunityStructure.MSupplyDemandPic Model5 = new EyouSoft.Model.CommunityStructure.MSupplyDemandPic();
            Model5.LinkAddress = imgHref5;
            Model5.OperateTime = DateTime.Now;
            Model5.PicPath = string.IsNullOrEmpty(imgPath5)?hdfoldimgpath5.Value:imgPath5;

            //5个对象添加到集合列表
            List<EyouSoft.Model.CommunityStructure.MSupplyDemandPic> list = new List<EyouSoft.Model.CommunityStructure.MSupplyDemandPic>();
            list.Add(Model1);
            list.Add(Model2);
            list.Add(Model3);
            list.Add(Model4);
            list.Add(Model5);
            #endregion
            
            #region 执行保存操作 返回相应的结果
            bool result = EyouSoft.BLL.CommunityStructure.BSupplyDemandPic.CreateInstance().Add(list);
            if (result)
                MessageBox.ShowAndRedirect(this, "保存成功！", "/SupplierManage/SupplyFocusBannerImg.aspx");
            else
                MessageBox.ShowAndRedirect(this, "保存失败！", "/SupplierManage/SupplyFocusBannerImg.aspx");
            #endregion

            #region 释放资源
            Model1 = null;
            Model2 = null;
            Model3 = null;
            Model4 = null;
            Model5 = null;
            list = null;
            #endregion
        }
        #endregion

        #region 链接Url处理，有【http://】就不加，没有就加上
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
