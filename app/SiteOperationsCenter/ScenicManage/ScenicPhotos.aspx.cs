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
using System.Collections.Generic;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.ScenicManage
{
    public partial class ScenicPhotos : System.Web.UI.Page
    {
        protected string scenicid = string.Empty;
        protected string companyid = string.Empty;
        protected string scenicname = string.Empty;
        //景区形象
        protected MScenicImg modelScenicImg1 = null;
        //景区导游地图
        protected MScenicImg modelScenicImg2 = null;
        IList<MScenicImg> listScenicImg = new List<MScenicImg>();
        protected void Page_Load(object sender, EventArgs e)
        {
            scenicid = Utils.GetQueryStringValue("ScenicId");
            companyid = Utils.GetQueryStringValue("CompanyId");
            scenicname = Utils.GetQueryStringValue("ScenicName");



            if (!IsPostBack)
            {
                GetScenicOnCompany();
                if (string.IsNullOrEmpty(scenicid))
                {
                    MessageBox.Show(Page, "没有该景区的数据");
                }
                else
                {
                    GetScenicPhoto();
                    listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(DpScenicArea.SelectedValue);
                }
            }
        }


        #region 获取公司的所有景区
        protected void GetScenicOnCompany()
        {
            IList<MScenicArea> listScenicA = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(companyid);
            this.DpScenicArea.DataSource = listScenicA;
            this.DpScenicArea.DataTextField = "ScenicName";
            this.DpScenicArea.DataValueField = "ScenicId";
            this.DpScenicArea.DataBind();
            foreach (var item in listScenicA)
            {
                if (item.ScenicId == scenicid)
                    DpScenicArea.SelectedValue = scenicid;
            }
        }
        #endregion

        #region 获取景区照片实体
        /// <summary>
        /// 获取景区照片实体
        /// </summary>
        public void GetScenicPhoto()
        {
            modelScenicImg1 = new MScenicImg();
            modelScenicImg2 = new MScenicImg();
            IList<MScenicImg> nlistScenicImg = new List<MScenicImg>();
            IList<MScenicImg> listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(DpScenicArea.SelectedValue);
            if (listScenicImg.Count > 0)
            {
                foreach (var item in listScenicImg)
                {
                    switch (item.ImgType)
                    {
                        case ScenicImgType.景区导游地图:
                            if (item != null)
                            {
                                modelScenicImg2 = item;
                                Scenicviewdes.Value = scenicname+modelScenicImg2.Description;
                            } break;
                        case ScenicImgType.景区形象:
                            if (item != null)
                            {
                                modelScenicImg1 = item;
                                Scenicimagedes.Value = scenicname + modelScenicImg1.Description;
                            }
                                break;
                        case ScenicImgType.其他:
                            if (item != null)
                                nlistScenicImg.Add(item);
                            break;
                    }
                }
            }
            this.RephotoList.DataSource = nlistScenicImg;
            this.RephotoList.DataBind();
        }

        #endregion

        #region 景区形象照片
        /// <summary>
        /// 景区形象照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Scenicimage_Click(object sender, EventArgs e)
        {
            MScenicImg modeld = new MScenicImg();
            if (GetListByImgType(ScenicImgType.景区形象).Count > 0)
                modeld = GetListByImgType(ScenicImgType.景区形象)[0];
            modeld.Address = Utils.GetFormValue(SfpScenicimage.FindControl("hidFileName").UniqueID).Split('|')[0];
            modeld.ThumbAddress = Utils.GetFormValue(SfpScenicimage.FindControl("hidFileName").UniqueID).Split('|')[1];
            modeld.ImgType = ScenicImgType.景区形象;
            modeld.ScenicId = DpScenicArea.SelectedValue;
            modeld.CompanyId = companyid;
            modeld.Description = Scenicimagedes.Value;
            if (!string.IsNullOrEmpty(modeld.ImgId))
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Update(modeld))
                {
                    MessageBox.Show(Page, "修改景区形象成功");
                }
                else
                    MessageBox.Show(Page, "修改景区形象失败");
            }
            else
            {
                modeld.ImgId = Guid.NewGuid().ToString();
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
                    MessageBox.Show(Page, "上传景区形象成功");
                else
                    MessageBox.Show(Page, "上传景区形象失败");
            }
            GetScenicPhoto();
        }
        #endregion


        #region 根据图片类型获取图片
        /// <summary>
        /// 根据图片类型获取图片
        /// </summary>
        /// <param name="imgtype">图片类型</param>
        /// <returns></returns>
        protected IList<MScenicImg> GetListByImgType(ScenicImgType imgtype)
        {
            IList<MScenicImg> listdefault = new List<MScenicImg>();
            IList<MScenicImg> listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(DpScenicArea.SelectedValue);
            if (listScenicImg.Count > 0)
            {
                foreach (var item in listScenicImg)
                {
                    if (item.ImgType == imgtype)
                    {
                        if (item != null)
                            listdefault.Add(item);
                    }
                }
            }
            return listdefault;
        }
        #endregion


        #region 景区导游地图
        /// <summary>
        /// 景区导览图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Scenicview_Click(object sender, EventArgs e)
        {
            MScenicImg modeld = new MScenicImg();
            if (GetListByImgType(ScenicImgType.景区导游地图).Count > 0)
                modeld = GetListByImgType(ScenicImgType.景区导游地图)[0];
            modeld.Address = Utils.GetFormValue(SfpScenicview.FindControl("hidFileName").UniqueID).Split('|')[0];
            modeld.ThumbAddress = Utils.GetFormValue(SfpScenicview.FindControl("hidFileName").UniqueID).Split('|')[1];
            modeld.ImgType = ScenicImgType.景区导游地图;
            modeld.ScenicId = DpScenicArea.SelectedValue;
            modeld.CompanyId = companyid;
            modeld.Description = Scenicviewdes.Value;
            
            if (!string.IsNullOrEmpty(modeld.ImgId))
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Update(modeld))
                    MessageBox.Show(Page, "修改景区导游地图成功");
                else
                    MessageBox.Show(Page, "修改景区导游地图失败");
            }
            else
            {
                modeld.ImgId = Guid.NewGuid().ToString();
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
                    MessageBox.Show(Page, "上传景区导游地图成功");
                else
                    MessageBox.Show(Page, "上传景区导游地图失败");
            }
            scenicid = DpScenicArea.SelectedValue;
            GetScenicPhoto();
        }
        #endregion


        #region 更多图片
        /// <summary>
        ///  更多图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Scenicimages_Click(object sender, EventArgs e)
        {
            MScenicImg modeld = new MScenicImg();
            modeld.Address = Utils.GetFormValue(SfpScenicimages.FindControl("hidFileName").UniqueID).Split('|')[0];
            modeld.ThumbAddress = Utils.GetFormValue(SfpScenicimages.FindControl("hidFileName").UniqueID).Split('|')[1];
            modeld.Description = Utils.GetFormValue(txtDescription.UniqueID);
            modeld.ImgType = ScenicImgType.其他;
            modeld.ScenicId = DpScenicArea.SelectedValue;
            modeld.ImgId = Guid.NewGuid().ToString();
            modeld.CompanyId = companyid;
            if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
            {
                MessageBox.Show(Page, "上传成功");
                txtDescription.Value = "";
            }
            else
                MessageBox.Show(Page, "上传失败");
            GetScenicPhoto();
        }

        #endregion


        #region 切换景区图片
        protected void DpScenicArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetScenicPhoto();
        }

        #endregion
    }
}
