using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EyouSoft.Common.Function;
using EyouSoft.Common;
namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 描述：运营后台图片保存页面
    /// 添加人：HL
    /// 添加时间：2010-04-2
    /// </summary>
    public partial class PictureUpdate : EyouSoft.Common.Control.YunYingPage
    {
        #region 定义
        /// <summary>
        /// 定义图片路径
        /// </summary>
        protected string img_Pathone = string.Empty, img_Pathtwo = string.Empty, img_PathShree = string.Empty,
            img_PathFour = string.Empty, img_PathFive = string.Empty, img_PathNews = string.Empty,
            img_PathTour = string.Empty, img_PathSame = string.Empty, img_PathSameRight = string.Empty,
            img_PathCommunity = string.Empty, img_PathMiddleLeft = string.Empty, img_PathMiddleRight = string.Empty;

        #endregion

        #region 事件

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //判断焦点图的权限功能
                YuYingPermission[] parms = { YuYingPermission.焦点图片管理_管理该栏目, YuYingPermission.焦点图片管理_管理该栏目 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.焦点图片管理_管理该栏目, true);
                    return;
                }
                GetPageData();
            }
        }
        #endregion

        #region 数据保存
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSave();
        }
        #endregion

        #endregion

        #region 方法

        #region 读取页面基本信息
        /// <summary>
        /// 读取页面基本信息
        /// </summary>
        private void GetPageData()
        {
            EyouSoft.Model.NewsStructure.NewsPicInfo model_NewsPicInfo = new EyouSoft.Model.NewsStructure.NewsPicInfo();
            EyouSoft.BLL.NewsStructure.NewsPicInfo bll_NewsPicInfo = new EyouSoft.BLL.NewsStructure.NewsPicInfo();

            //获取焦点图信息
            if (bll_NewsPicInfo.GetModel() != null)
            {
                IList<EyouSoft.Model.NewsStructure.BasicNewsPic> HomeList = bll_NewsPicInfo.GetModel().FocusPic;

                //对列表进行遍历,取出值
                if (HomeList != null && HomeList.Count > 0)
                {
                        //1.第一张焦点图
                        if (HomeList[0].PicPath.Length > 0)
                        {
                            img_Pathone = GetImgName(HomeList[0].PicPath);
                            hdfFocusOne.Value = HomeList[0].PicPath;
                        }
                        InpFocusHrefOne.Value = HomeList[0].PicUrl;
                        InpFocusTitleOne.Value = HomeList[0].PicTitle;
                        //2.第二张焦点图
                        if (HomeList[1].PicPath.Length > 0)
                        {
                            img_Pathtwo = GetImgName(HomeList[1].PicPath);
                            hdfFocusTwo.Value = HomeList[1].PicPath;
                        }
                        inpFocusHrefTwo.Value = HomeList[1].PicUrl;
                        inpFocusTitleTwo.Value = HomeList[1].PicTitle;
                        //3.第三张焦点图
                        if (HomeList[2].PicPath.Length > 0)
                        {
                            img_PathShree = GetImgName(HomeList[2].PicPath);
                            hdfFocusShree.Value = HomeList[2].PicPath;
                        }
                        inpFocusHrefShree.Value = HomeList[2].PicUrl;
                        inpFocusTitleShree.Value = HomeList[2].PicTitle;
                        //4.第四张焦点图
                        if (HomeList[3].PicPath.Length > 0)
                        {
                            img_PathFour = GetImgName(HomeList[3].PicPath);
                            hdfFocusFour.Value = HomeList[3].PicPath;
                        }
                        inpFocusHrefFour.Value = HomeList[3].PicUrl;
                        inpFocusTitleFour.Value = HomeList[3].PicTitle;
                        //5.第五张焦点图
                        if (HomeList[4].PicPath.Length > 0)
                        {
                            img_PathFive = GetImgName(HomeList[4].PicPath);
                            hdfFocusFive.Value = HomeList[4].PicPath;
                        }
                        inpFocusHrefFive.Value = HomeList[4].PicUrl;
                        inpFocusTitleFive.Value = HomeList[4].PicTitle;
                    }
                    //清空数据
                    HomeList = null;

                    //6.获取焦点新闻右侧公告图片     
                    if (!string.IsNullOrEmpty(bll_NewsPicInfo.GetModel().FocusRightPic.PicPath))
                    {
                        img_PathNews = GetImgName(bll_NewsPicInfo.GetModel().FocusRightPic.PicPath);
                        hdfNews.Value = bll_NewsPicInfo.GetModel().FocusRightPic.PicPath;
                    }
                    inpNewsHref.Value = bll_NewsPicInfo.GetModel().FocusRightPic.PicUrl;

                    //7.旅游资讯焦点图片
                    if (!string.IsNullOrEmpty(bll_NewsPicInfo.GetModel().TravelFocusPic.PicPath))
                    {
                        //获取相关信息
                        img_PathTour = GetImgName(bll_NewsPicInfo.GetModel().TravelFocusPic.PicPath);
                        hdfTour.Value = bll_NewsPicInfo.GetModel().TravelFocusPic.PicPath;
                    }
                    inpTourHref.Value = bll_NewsPicInfo.GetModel().TravelFocusPic.PicUrl;
                    inpTourTitle.Value = bll_NewsPicInfo.GetModel().TravelFocusPic.PicTitle;
                    //8.同业学堂焦点图片
                    if (!string.IsNullOrEmpty(bll_NewsPicInfo.GetModel().SchoolFocusPic.PicPath))
                    {
                        img_PathSame = GetImgName(bll_NewsPicInfo.GetModel().SchoolFocusPic.PicPath);
                        hdfSame.Value = bll_NewsPicInfo.GetModel().SchoolFocusPic.PicPath;
                    }
                    inpSameHref.Value = bll_NewsPicInfo.GetModel().SchoolFocusPic.PicUrl;
                    inpSameTitle.Value = bll_NewsPicInfo.GetModel().SchoolFocusPic.PicTitle;
                    //9.同业学堂右侧广告
                    if (!string.IsNullOrEmpty(bll_NewsPicInfo.GetModel().SchoolRightPic.PicPath))
                    {
                        img_PathSameRight = GetImgName(bll_NewsPicInfo.GetModel().SchoolRightPic.PicPath);
                        hdfSameRight.Value = bll_NewsPicInfo.GetModel().SchoolRightPic.PicPath;
                    }
                    inpSameHrefRight.Value = bll_NewsPicInfo.GetModel().SchoolRightPic.PicUrl;
                    //10.同行社区
                    if (!string.IsNullOrEmpty(bll_NewsPicInfo.GetModel().CommunityPic.PicPath))
                    {
                        img_PathCommunity = GetImgName(bll_NewsPicInfo.GetModel().CommunityPic.PicPath);
                        hdfCommunity.Value = bll_NewsPicInfo.GetModel().CommunityPic.PicPath;
                    }
                    inpCommunityHref.Value = bll_NewsPicInfo.GetModel().CommunityPic.PicUrl;

                    IList<EyouSoft.Model.NewsStructure.BasicNewsPic> MiddleList = bll_NewsPicInfo.GetModel().CommunityMiddlePic;

                    if (MiddleList != null && MiddleList.Count > 0)
                    {
                        //同行社区[左]
                        if (MiddleList[0].PicPath.Length > 0)
                        {
                            img_PathMiddleLeft = GetImgName(MiddleList[0].PicPath);
                            hidMiddleLeft.Value = MiddleList[0].PicPath;
                        }
                        inpHrefMiddleLeft.Value = MiddleList[0].PicUrl;
                        intTitleMiddleLeft.Value = MiddleList[0].PicTitle;
                        //同行社区[右]
                        if (MiddleList[1].PicPath.Length > 0)
                        {
                            img_PathMiddleRight = GetImgName(MiddleList[1].PicPath);
                            HidMiddleRight.Value = MiddleList[1].PicPath;
                        }
                        inphrefMiddleRight.Value = MiddleList[1].PicUrl;
                        inpTitleMiddleRight.Value = MiddleList[1].PicTitle;
                    }
                    
                
            }
        }
        #endregion

        #region 数据保存
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <returns></returns>
        private void DataSave()
        {
            EyouSoft.Model.NewsStructure.NewsPicInfo model_NewsPicInfo = new EyouSoft.Model.NewsStructure.NewsPicInfo();
            EyouSoft.BLL.NewsStructure.NewsPicInfo bll_NewsPicInfo = new EyouSoft.BLL.NewsStructure.NewsPicInfo();

            #region 图片定义
            //焦点图1
            string FileFocusOne = Request.Form["SFFocusOne$hidFileName"];           
            if (!string.IsNullOrEmpty(FileFocusOne))
            {
                FileFocusOne = Request.Form["SFFocusOne$hidFileName"];
            }
            else
            {
                FileFocusOne = hdfFocusOne.Value;
            }
            //焦点图2
            string FileFocusTwo = Request.Form["SFFocusTwo$hidFileName"];
            if (!string.IsNullOrEmpty(FileFocusTwo))
            {
                FileFocusTwo = Request.Form["SFFocusTwo$hidFileName"];
            }
            else
            {
                FileFocusTwo = hdfFocusTwo.Value;
            }
            //焦点图3
            string FileFocusShree = Request.Form["SFFocusShree$hidFileName"];
            if (!string.IsNullOrEmpty(FileFocusShree))
            {
                FileFocusShree = Request.Form["SFFocusShree$hidFileName"];
            }
            else
            {
                FileFocusShree = hdfFocusShree.Value;
            }
            //焦点图4
            string FileFocusFour = Request.Form["SFFocusFour$hidFileName"];
            if (!string.IsNullOrEmpty(FileFocusFour))
            {
                FileFocusFour = Request.Form["SFFocusFour$hidFileName"];
            }
            else
            {
                FileFocusFour = hdfFocusFour.Value;
            }
            //焦点图5
            string FileFocusFive = Request.Form["SFFocusFive$hidFileName"];
            if (!string.IsNullOrEmpty(FileFocusFive))
            {
                FileFocusFive = Request.Form["SFFocusFive$hidFileName"];
            }
            else
            {
                FileFocusFive = hdfFocusFive.Value;
            }
            //焦点新闻右侧公告图片
            string FileNews = Request.Form["SFNews$hidFileName"];
            if (!string.IsNullOrEmpty(FileNews))
            {
                FileNews = Request.Form["SFNews$hidFileName"];
            }
            else
            {
                FileNews = hdfNews.Value;
            }
            //旅游资讯焦点图片
            string FileTour = Request.Form["SFTour$hidFileName"];
            if (!string.IsNullOrEmpty(FileTour))
            {
                FileTour = Request.Form["SFTour$hidFileName"];
            }
            else
            {
                FileTour = hdfTour.Value;
            }
            //同业学堂焦点图片
            string FileSame = Request.Form["SFSame$hidFileName"];
            if (!string.IsNullOrEmpty(FileSame))
            {
                FileSame = Request.Form["SFSame$hidFileName"];
            }
            else
            {
                FileSame = hdfSame.Value;
            }
            //同业学堂右侧广告
            string FileSameRight = Request.Form["SFSameRight$hidFileName"];           
            if (!string.IsNullOrEmpty(FileSameRight))
            {
                FileSameRight = Request.Form["SFSameRight$hidFileName"];
            }
            else
            {
                FileSameRight = hdfSameRight.Value;
            }
            //同行社区
            string FileCommunity = Request.Form["SFCommunity$hidFileName"];
            if (!string.IsNullOrEmpty(FileCommunity))
            {
                FileCommunity = Request.Form["SFCommunity$hidFileName"];
            }
            else
            {
                FileCommunity = hdfCommunity.Value;
            }
            //同业社区中[左]
            string FileMiddleLeft = Request.Form["SFMiddleLeft$hidFileName"];
            if (!string.IsNullOrEmpty(FileMiddleLeft))
            {
                FileMiddleLeft = Request.Form["SFMiddleLeft$hidFileName"];
            }
            else
            {
                FileMiddleLeft = hidMiddleLeft.Value;
            }
            //同业社区中[右]
            string FileMiddleRight = Request.Form["SFMiddleRight$hidFileName"];
            if (!string.IsNullOrEmpty(FileMiddleRight))
            {
                FileMiddleRight = Request.Form["SFMiddleRight$hidFileName"];
            }
            else
            {
                FileMiddleRight = HidMiddleRight.Value;
            }
            #endregion           

            #region 连地地址定义
            //焦点图1链接路径
            string FocusHrefOne = Utils.GetFormValue("InpFocusHrefOne", 250);
            //焦点图2链接路径
            string FocusHrefTwo = Utils.GetFormValue("inpFocusHrefTwo", 250);
            //焦点图3链接路径
            string FocusHrefShree = Utils.GetFormValue("inpFocusHrefShree", 250);
            //焦点图4链接路径
            string FocusHrefFour = Utils.GetFormValue("inpFocusHrefFour", 250);
            //焦点图5链接路径
            string FocusHrefFive = Utils.GetFormValue("inpFocusHrefFive", 250);
            //焦点新闻右侧公告图片链接路径
            string NewsHref = Utils.GetFormValue("inpNewsHref", 250);
            //旅游资讯焦点图片链接路径
            string TourHref = Utils.GetFormValue("inpTourHref", 250);
            //同业学堂焦点图片链接路径
            string SameHref = Utils.GetFormValue("inpSameHref", 250);
            //同业学堂右侧广告链接路径
            string SameHrefRight = Utils.GetFormValue("inpSameHrefRight", 250);
            //同行社区链接路径
            string CommunityHref = Utils.GetFormValue("inpCommunityHref", 250);
            //同行社区左
            string MiddleLeftHref = Utils.GetFormValue("inpHrefMiddleLeft", 250);
            //同行社区右
            string MiddleRightHref = Utils.GetFormValue("inphrefMiddleRight", 250);
            #endregion

            #region 标题定义

            //焦点图1标题
            string FocusTitleOne = Utils.GetFormValue(InpFocusTitleOne.UniqueID, 250);
            //焦点图2标题
            string FocusTitleTwo = Utils.GetFormValue(inpFocusTitleTwo.UniqueID, 250);
            //焦点图3标题
            string FocusTitleShree = Utils.GetFormValue(inpFocusTitleShree.UniqueID, 250);
            //焦点图4标题
            string FocusTitleFour = Utils.GetFormValue(inpFocusTitleFour.UniqueID, 250);
            //焦点图5标题
            string FocusTitleFive = Utils.GetFormValue(inpFocusTitleFive.UniqueID, 250);
            //旅游资讯焦点图片标题
            string TourTitle = Utils.GetFormValue(inpTourTitle.UniqueID, 250);
            //同业学堂焦点图片标题
            string SameTitle = Utils.GetFormValue(inpSameTitle.UniqueID, 250);
            //同行社区左
            string MiddleLeftTitle = Utils.GetFormValue(intTitleMiddleLeft.UniqueID, 250);
            //同行社区右
            string MiddleRightTitle = Utils.GetFormValue(inpTitleMiddleRight.UniqueID, 250);
            #endregion

            #region 数据验证

            //System.Text.StringBuilder strBuilderErr = new System.Text.StringBuilder();           
            //if (FileFocusOne == "")
            //{                
            //   // strErr += "请选择焦点图1要上传的图片！\\n";
            //    strBuilderErr.Append("请选择焦点图1要上传的图片！\\n");
            //}
            //if (FileFocusTwo == "")
            //{
            //    //strErr += "请选择焦点图2要上传的图片！\\n";
            //    strBuilderErr.Append("请选择焦点图2要上传的图片！\\n");
            //}
            //if (FileFocusShree == "")
            //{
            //    //strErr += "请选择焦点图3要上传的图片！\\n";
            //    strBuilderErr.Append("请选择焦点图3要上传的图片！\\n");
            //}
            //if (FileFocusFour == "")
            //{
            //    //strErr += "请选择焦点图4要上传的图片！\\n";
            //    strBuilderErr.Append("请选择焦点图4要上传的图片！\\n");
            //}
            //if (FileFocusFive == "")
            //{
            //    //strErr += "请选择焦点图5要上传的图片！\\n";
            //    strBuilderErr.Append("请选择焦点图5要上传的图片！\\n");
            //}
            //if (FileNews == "")
            //{
            //    //strErr += "请选择新闻右侧公告图片要上传的图片！\\n";
            //    strBuilderErr.Append("请选择新闻右侧公告图片要上传的图片！\\n");
            //}
            //if (FileTour == "")
            //{
            //    //strErr += "请选择资讯焦点图片要上传的图片！\\n";
            //    strBuilderErr.Append("请选择资讯焦点图片要上传的图片！\\n");
            //}
            //if (FileSame == "")
            //{
            //    //strErr += "请选择同业学堂焦点图片要上传的图片！\\n";
            //    strBuilderErr.Append("请选择同业学堂焦点图片要上传的图片！\\n");
            //}
            //if (FileSameRight == "")
            //{
            //   // strErr += "请选择同业学堂右侧广告要上传的图片！\\n";
            //    strBuilderErr.Append("请选择同业学堂右侧广告要上传的图片！\\n");
            //}
            //if (FileCommunity == "")
            //{
            //    //strErr += "请选择同行社区要上传的图片！\\n";
            //    strBuilderErr.Append("请选择同行社区要上传的图片！\\n");
            //}
            //if (FileMiddleLeft == "")
            //{
            //    //strErr += "请选择同行社区中间[左]要上传的图片！\\n";
            //    strBuilderErr.Append("请选择同行社区中间[左]要上传的图片！\\n");
            //}
            //if (FileMiddleRight == "")
            //{
            //    //strErr += "请选择同行社区中间[右]要上传的图片！\\n";
            //    strBuilderErr.Append("请选择同行社区中间[右]要上传的图片！\\n");
            //}
            //if (FocusHrefOne == "")
            //{
            //    //strErr += "请输入焦点图1连接路径！\\n";
            //    strBuilderErr.Append("请输入焦点图1连接路径！\\n");
            //}
            //if (FocusHrefTwo == "")
            //{
            //    //strErr += "请输入焦点图2连接路径！\\n";
            //    strBuilderErr.Append("请输入焦点图2连接路径！\\n");
            //}
            //if (FocusHrefShree == "")
            //{
            //    //strErr += "请输入焦点图3连接路径！\\n";
            //    strBuilderErr.Append("请输入焦点图3连接路径！\\n");
            //}
            //if (FocusHrefFour == "")
            //{
            //    //strErr += "请输入焦点图4连接路径！\\n";
            //    strBuilderErr.Append("请输入焦点图4连接路径！\\n");
            //}
            //if (FocusHrefFive == "")
            //{
            //    //strErr += "请输入焦点图5连接路径！\\n";
            //    strBuilderErr.Append("请输入焦点图5连接路径！\\n");
            //}
            //if (NewsHref == "")
            //{
            //    //strErr += "请输入焦点新闻右侧公告图片链接路径！\\n";
            //    strBuilderErr.Append("请输入焦点新闻右侧公告图片链接路径！\\n");
            //}
            //if (TourHref == "")
            //{
            //    //strErr += "请输入旅游资讯焦点图片链接路径！\\n";
            //    strBuilderErr.Append("请输入旅游资讯焦点图片链接路径！\\n");
            //}
            //if (SameHref == "")
            //{
            //    //strErr += "请输入同业学堂焦点图片链接路径！\\n";
            //    strBuilderErr.Append("请输入同业学堂焦点图片链接路径！\\n");
            //}
            //if (SameHrefRight == "")
            //{
            //    //strErr += "请输入同业学堂右侧广告链接路径！\\n";
            //    strBuilderErr.Append("请输入同业学堂右侧广告链接路径！\\n");
            //}
            //if (CommunityHref == "")
            //{
            //    //strErr += "请输入同行社区链接路径！\\n";
            //    strBuilderErr.Append("请输入同行社区链接路径！\\n");
            //}
            //if (MiddleLeftHref == "")
            //{
            //    //strErr += "请输入同行社区中间[左]链接路径！\\n";
            //    strBuilderErr.Append("请输入同行社区中间[左]链接路径！\\n");
            //}
            //if (MiddleRightHref == "")
            //{
            //    //strErr += "请输入同行社区中间[右]链接路径！\\n";
            //    strBuilderErr.Append("请输入同行社区中间[右]链接路径！\\n");
            //}
            //if (FocusTitleOne == "")
            //{
            //   // strErr += "请输入焦点图1标题！\\n";
            //    strBuilderErr.Append("请输入焦点图1标题！\\n");
            //}
            //if (FocusTitleTwo == "")
            //{
            //    //strErr += "请输入焦点图2标题！\\n";
            //    strBuilderErr.Append("请输入焦点图2标题！\\n");
            //}
            //if (FocusTitleShree == "")
            //{
            //    //strErr += "请输入焦点图3标题！\\n";
            //    strBuilderErr.Append("请输入焦点图3标题！\\n");
            //}
            //if (FocusTitleFour == "")
            //{
            //    //strErr += "请输入焦点图4标题！\\n";
            //    strBuilderErr.Append("请输入焦点图4标题！\\n");
            //}
            //if (FocusTitleFive == "")
            //{
            //    //strErr += "请输入焦点图5标题！\\n";
            //    strBuilderErr.Append("请输入焦点图5标题！\\n");
            //}
            //if (TourTitle == "")
            //{
            //    //strErr += "请输入旅游资讯焦点图片标题！\\n";
            //    strBuilderErr.Append("请输入旅游资讯焦点图片标题！\\n");
            //}
            //if (SameTitle == "")
            //{
            //    //strErr += "请输入同业学堂焦点图片标题！\\n";
            //    strBuilderErr.Append("请输入同业学堂焦点图片标题！\\n");
            //}
            //if (MiddleLeftTitle == "")
            //{
            //    //strErr += "请输入同业社区中间[左]图片标题！\\n";
            //    strBuilderErr.Append("请输入同业社区中间[左]图片标题！\\n");
            //}
            //if (MiddleRightTitle == "")
            //{
            //    //strErr += "请输入同业社区中间[右]图片标题！\\n";
            //    strBuilderErr.Append("请输入同业社区中间[右]图片标题！\\n");
            //}
            //if (strBuilderErr.ToString() != "")
            //{
            //    MessageBox.Show(this, strBuilderErr.ToString());
            //    return;
            //}

            #endregion            

            #region 添加数据

            List<EyouSoft.Model.NewsStructure.BasicNewsPic> homeList = new List<EyouSoft.Model.NewsStructure.BasicNewsPic>();

            EyouSoft.Model.NewsStructure.BasicNewsPic Model_BasicNewsPic = null;
           

            //1.第一张焦点图赋值
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileFocusOne != "")
            {
                Model_BasicNewsPic.PicPath = FileFocusOne;
            }
            Model_BasicNewsPic.PicTitle = FocusTitleOne;
            Model_BasicNewsPic.PicUrl = FocusHrefOne;
            //添加进去
            homeList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;
            
            //2.第二张焦点图
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileFocusTwo != "")
            {
                Model_BasicNewsPic.PicPath = FileFocusTwo;
            }
            Model_BasicNewsPic.PicTitle = FocusTitleTwo;
            Model_BasicNewsPic.PicUrl = FocusHrefTwo;
            //添加
            homeList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;

            //3.第三张焦点图
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileFocusShree != "")
            {
                Model_BasicNewsPic.PicPath = FileFocusShree;
            }
            Model_BasicNewsPic.PicTitle = FocusTitleShree;
            Model_BasicNewsPic.PicUrl = FocusHrefShree;
            //添加
            homeList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;


            //4.第四张焦点图
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileFocusFour != "")
            {
                Model_BasicNewsPic.PicPath = FileFocusFour;
            }
            Model_BasicNewsPic.PicTitle = FocusTitleFour;
            Model_BasicNewsPic.PicUrl = FocusHrefFour;
            //添加
            homeList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;

            //5.第五张焦点图
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileFocusFive != "")
            {
                Model_BasicNewsPic.PicPath = FileFocusFive;
            }
            Model_BasicNewsPic.PicTitle = FocusTitleFive;
            Model_BasicNewsPic.PicUrl = FocusHrefFive;
            //添加
            homeList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;
            //添加
            model_NewsPicInfo.FocusPic = homeList;

            //6.添加焦点新闻右侧公告图片
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileNews != "")
            {
                Model_BasicNewsPic.PicPath = FileNews;
            }
            Model_BasicNewsPic.PicTitle = "";
            Model_BasicNewsPic.PicUrl = NewsHref;
            model_NewsPicInfo.FocusRightPic = Model_BasicNewsPic;
            Model_BasicNewsPic = null;
            //7.旅游资讯焦点图片
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileTour != "")
            {
                Model_BasicNewsPic.PicPath = FileTour;
            }
            Model_BasicNewsPic.PicTitle = TourTitle;
            Model_BasicNewsPic.PicUrl = TourHref;
            model_NewsPicInfo.TravelFocusPic = Model_BasicNewsPic;
            Model_BasicNewsPic = null;
            //8.同业学堂焦点图片
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileSame != "")
            {
                Model_BasicNewsPic.PicPath = FileSame;
            }
            Model_BasicNewsPic.PicTitle = SameTitle;
            Model_BasicNewsPic.PicUrl = SameHref;
            model_NewsPicInfo.SchoolFocusPic = Model_BasicNewsPic;
            Model_BasicNewsPic = null;
            //9.同业学堂右侧广告
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileSameRight != "")
            {
                Model_BasicNewsPic.PicPath = FileSameRight;
            }
            Model_BasicNewsPic.PicTitle = "";
            Model_BasicNewsPic.PicUrl = SameHrefRight;
            model_NewsPicInfo.SchoolRightPic = Model_BasicNewsPic;
            Model_BasicNewsPic = null;

            //10.同行社区
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileCommunity != "")
            {
                Model_BasicNewsPic.PicPath = FileCommunity;
            }
            Model_BasicNewsPic.PicTitle = "";
            Model_BasicNewsPic.PicUrl = CommunityHref;
            model_NewsPicInfo.CommunityPic = Model_BasicNewsPic;
            Model_BasicNewsPic = null;

            List<EyouSoft.Model.NewsStructure.BasicNewsPic> MiddleList = new List<EyouSoft.Model.NewsStructure.BasicNewsPic>();
            //同上社区左
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileMiddleLeft != "")
            {
                Model_BasicNewsPic.PicPath = FileMiddleLeft;
            }
            Model_BasicNewsPic.PicTitle = MiddleLeftTitle;
            Model_BasicNewsPic.PicUrl = MiddleLeftHref;
            MiddleList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;

            //同行社区右
            Model_BasicNewsPic = new EyouSoft.Model.NewsStructure.BasicNewsPic();
            if (FileMiddleRight != "")
            {
                Model_BasicNewsPic.PicPath = FileMiddleRight;
            }
            Model_BasicNewsPic.PicTitle = MiddleRightTitle;
            Model_BasicNewsPic.PicUrl = MiddleRightHref;
            MiddleList.Add(Model_BasicNewsPic);
            Model_BasicNewsPic = null;
            //添加
            model_NewsPicInfo.CommunityMiddlePic = MiddleList;         

            //添加操作
            if (bll_NewsPicInfo.Add(model_NewsPicInfo) == true)
            {
                MessageBox.Show(this, "数据保存成功！");
            }
            else
            {
                MessageBox.Show(this, "数据保存失败！");
            }
            //释放资源
            bll_NewsPicInfo = null;
            model_NewsPicInfo = null;
            //加载
            GetPageData();

            #endregion

        }
        #endregion       

        #region 查看图片
        /// <summary>
        /// 查看图片
        /// </summary>
        /// <param name="ImgPath"></param>
        /// <returns></returns>
        public string GetImgName(string ImgPath)
        {
            string s = string.Empty;
            if (!string.IsNullOrEmpty(ImgPath))
                s = string.Format("<a href='{0}' target='_blank'>查看</a>", Domain.FileSystem + ImgPath);
            return s;
        }
        #endregion

        #endregion
    }
}
