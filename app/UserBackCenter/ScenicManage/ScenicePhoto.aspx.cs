using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 景区列表页
    /// 功能：显示查询以后获得景区的列表
    /// 创建人：方琪
    /// 创建时间： 2011-10-21  
    /// </summary>
    public partial class ScenicePhoto : EyouSoft.Common.Control.BackPage
    {
        protected string scenicid = string.Empty;
        protected string companyid = string.Empty;
        protected string ScenicName = string.Empty;
        //景区导游地图
        protected MScenicImg modelScenicImg1 = new MScenicImg();
        //景区形象
        protected MScenicImg modelScenicImg2 = new MScenicImg();
        IList<MScenicImg> listScenicImg = new List<MScenicImg>();
        protected void Page_Load(object sender, EventArgs e)
        {
            scenicid = Utils.GetQueryStringValue("EditId");
            companyid = this.SiteUserInfo.CompanyID;
            ScenicName=EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(scenicid).ScenicName;
            this.txtSceniceimage.Value =ScenicName + "_景区形象图";
            this.txtSceniceview.Value = ScenicName + "_景区导览图";
            if (!IsPostBack)
            {
                GetScenicOnCompany();
                if (string.IsNullOrEmpty(scenicid))
                {
                    MessageBox.Show(Page, "没有该景区的数据");
                }
                else
                {
                    GetScenicPhoto(scenicid);
                    listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(scenicid);
                }
            }
            #region 处理前台请求

            string type = Utils.GetQueryStringValue("type");
            string filevalue = Utils.GetQueryStringValue("filevalue");
            scenicid = Utils.GetQueryStringValue("EditId");
            string Description = Utils.GetQueryStringValue("Description");
            if (type != "" && filevalue != "" && scenicid != ""&&Description!="")
            {
                Response.Clear();
                Response.Write(Decide(type, filevalue, Description));
                Response.End();
            }
            #endregion
        }

        #region 处理判断
        /// <summary>
        /// 处理执行结果
        /// </summary>
        /// <param name="type">景区类型</param>
        /// <param name="filevalue">路径</param>
        /// <returns></returns>
        protected string Decide(string type, string filevalue, string Description)
        {
            int mark = 0;
            string result = "";
            switch (type)
            {
                case "Scenicimage":
                    mark = UpdateOrAddScenicimage(filevalue, scenicid);
                    switch (mark)
                    {
                        case 1:
                            result = "上传修改景区形象图片成功！";
                            break;
                        case 2:
                            result = "上传修改景区形象图片失败！";
                            break;
                        case 3:
                            result = "上传添加景区形象图片成功！";
                            break;
                        case 4:
                            result = "上传添加景区形象图片失败！";
                            break;

                    }
                    break;
                case "Scenicview":
                    mark = UpdateOrAddScenicview(filevalue, scenicid);
                    switch (mark)
                    {
                        case 1:
                            result = "上传修改景区导览图片成功！";
                            break;
                        case 2:
                            result = "上传修改景区导览图片失败！";
                            break;
                        case 3:
                            result = "上传添加景区导览图片成功！";
                            break;
                        case 4:
                            result = "上传添加景区导览图片失败！";
                            break;

                    }
                    break;
                case "Scenicimages":
                    mark = AddScenicimages(filevalue, scenicid, Description);
                    switch (mark)
                    {
                        case 1:
                            result = "上传添加景区其他图片成功！";
                            break;
                        case 2:
                            result = "上传添加景区其他图片失败！";
                            break;
                    }
                    break;
            }
            return result;
        }

        #endregion

        #region 获取公司的所有景区
        protected void GetScenicOnCompany()
        {
            IList<MScenicArea> listScenicA = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(companyid);
            foreach (var item in listScenicA)
            {
                ListItem li = new ListItem();
                li.Text =Utils.GetText2( item.ScenicName,15,false);
                li.Value = item.ScenicId;
                this.DpScenicArea.Items.Add(li);
                if (item.ScenicId == scenicid)
                    DpScenicArea.SelectedValue = scenicid;
            }
        }
        #endregion


        #region 获取景区照片实体
        /// <summary>
        /// 获取景区照片实体
        /// </summary>
        public void GetScenicPhoto(string scenicid)
        {
            IList<MScenicImg> nlistScenicImg = new List<MScenicImg>();
            IList<MScenicImg> listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(scenicid);
            if (listScenicImg.Count > 0)
            {
                foreach (EyouSoft.Model.ScenicStructure.MScenicImg item in listScenicImg)
                {
                    switch (item.ImgType)
                    {
                        case ScenicImgType.景区导游地图:
                            if (item != null)
                                modelScenicImg2 = item;
                            break;
                        case ScenicImgType.景区形象:
                            if (item != null)
                                modelScenicImg1 = item;
                            break;
                        case ScenicImgType.其他:
                            if (item != null)
                                nlistScenicImg.Add(item);
                            break;
                    }
                }
            }
            //this.rpt_ImgList.DataSource = nlistScenicImg;
            //this.rpt_ImgList.DataBind();
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
            IList<MScenicImg> listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(scenicid);
            if (listScenicImg != null)
            {
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
            }
            return listdefault;
        }
        #endregion


        #region 景区形象图片
        /// <summary>
        ///  景区形象图片
        /// </summary>
        /// <param name="filevalue">文件路径</param>
        /// <param name="scenicid">景区编号</param>
        /// <returns></returns>
        protected int UpdateOrAddScenicimage(string filevalue, string scenicid)
        {
            int biaoshi = 0;
            MScenicImg modeld = new MScenicImg();
            if (GetListByImgType(ScenicImgType.景区形象).Count > 0)
                modeld = GetListByImgType(ScenicImgType.景区形象)[0];
            string[] Array = filevalue.Split('|');
            modeld.Address = Array[0];
            modeld.ThumbAddress = Array[1];
            modeld.Description = this.txtSceniceimage.Value;
            modeld.ImgType = ScenicImgType.景区形象;
            modeld.ScenicId = scenicid;
            modeld.CompanyId = this.SiteUserInfo.CompanyID;
            if (!string.IsNullOrEmpty(modeld.ImgId))
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Update(modeld))
                    biaoshi = 1;//修改景区形象成功
                else
                    biaoshi = 2;//修改失败
            }
            else
            {
                modeld.ImgId = Guid.NewGuid().ToString();
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
                    biaoshi = 3;//添加成功
                else
                    biaoshi = 4;//添加失败
            }
            return biaoshi;
        }
        #endregion

        #region 景区导览图
        /// <summary>
        /// 景区导览图
        /// </summary>
        /// <param name="filevalue">文件路径</param>
        /// <param name="scenicid">景区编号</param>
        /// <returns></returns>
        protected int UpdateOrAddScenicview(string filevalue, string scenicid)
        {
            int biaoshi = 0;
            MScenicImg modeld = new MScenicImg();
            if (GetListByImgType(ScenicImgType.景区导游地图).Count > 0)
                modeld = GetListByImgType(ScenicImgType.景区导游地图)[0];
            string[] Arry = filevalue.Split('|');
            modeld.Address = Arry[0].ToString();//SfpScenicview.FindControl("hidFileName").UniqueID
            modeld.ThumbAddress = Arry[1].ToString();
            modeld.Description = this.txtSceniceview.Value;
            modeld.ImgType = ScenicImgType.景区导游地图;
            modeld.ScenicId = scenicid;
            modeld.CompanyId = this.SiteUserInfo.CompanyID;
            if (!string.IsNullOrEmpty(modeld.ImgId))
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Update(modeld))
                    biaoshi = 1;
                else
                    biaoshi = 2;
            }
            else
            {
                modeld.ImgId = Guid.NewGuid().ToString();
                if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
                    biaoshi = 3;
                else
                    biaoshi = 4;
            }
            return biaoshi;
        }
        #endregion


        #region 更多图片
        /// <summary>
        /// 更多图片
        /// </summary>
        /// <param name="filevalue">文件路径</param>
        /// <param name="scenicid">景区编号</param>
        /// <returns></returns>
        protected int AddScenicimages(string filevalue, string scenicid, string Description)
        {
            if(!string.IsNullOrEmpty(Description)){
            int biaoshi = 0;
            MScenicImg modeld = new MScenicImg();
            string[] Arry = filevalue.Split('|');//"ctl00$ContentPlaceHolder1$SfpScenicimages$hidFileName"
            modeld.Address = Arry[0].ToString();//SfpScenicimages.FindControl("hidFileName").UniqueID
            modeld.Description = Description;
            modeld.ThumbAddress = Arry[1].ToString();
            modeld.ImgType = ScenicImgType.其他;
            modeld.ScenicId = scenicid;
            modeld.ImgId = Guid.NewGuid().ToString();
            modeld.CompanyId = this.SiteUserInfo.CompanyID;
            if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Add(modeld))
                biaoshi = 1;
            else
                biaoshi = 2;

            //this.rpt_ImgList.DataSource = GetListByImgType(ScenicImgType.其他);
            //this.rpt_ImgList.DataBind();
            return biaoshi;
            }
            return 2;
        }

        #endregion


        //#region 切换景区图片
        //protected void DpScenicArea_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    scenicid = DpScenicArea.SelectedValue;
        //    GetScenicPhoto(scenicid);
        //}

        //#endregion
    }
}
