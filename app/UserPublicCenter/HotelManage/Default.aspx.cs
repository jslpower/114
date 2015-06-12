using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;


namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店首页
    /// 功能：显示酒店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {
        private EyouSoft.IBLL.HotelStructure.IHotelLocalInfo iBll = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置title
                this.Page.Title = EyouSoft.Common.PageTitle.Hotel_Title;
                // 设置关键字
                AddMetaTag("description", EyouSoft.Common.PageTitle.Hotel_Des);
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Hotel_Keywords);
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 4;
                //加载城市数据
                CityDataInit();
                #region 图片广告
                #endregion

                //设置用户控件图片路径
                this.StarHotelControl1.ImageServerPath = this.ImageServerPath;
                //设置广告图片的宽度
                this.ImgFristControl1.ImageWidth = "277px";
                this.ImgSecondControl1.ImageWidth = "277px";

                #region 设置用户控件城市ID
                //最新加入酒店
                this.NewJoinHotelControl1.CityId = this.CityId;
                //明星酒店
                this.StarHotelControl1.CityId = this.CityId;
                //促销酒店
                this.PromoHotelControl1.CityId = this.CityId;
                #endregion
            }
        }


        #region 查询数据初始化
        /// <summary>
        /// 城市数据初始化
        /// </summary>
        protected void CityDataInit()
        {


        }
        #endregion

        #region 广告图片
        /// <summary>
        /// banner广告图片
        /// </summary>
        //protected void BannerImg()
        //{
        //    if (IsLogin)
        //    {
        //        this.lclBannerImg.Text = "<a href=\"" + Domain.UserBackCenter + "/hotelcenter/hotelordermanage/teamonlinesubmit.aspx\"><img class=\"add01\" src=\"" + ImageServerPath + "/images/hotel/main4.jpg\" /></a>";
        //    }
        //    else
        //    {
        //        this.lclBannerImg.Text = "<a href=\"" + EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Domain.UserBackCenter + "/hotelcenter/hotelordermanage/teamonlinesubmit.aspx", "登录") + "\"><img class=\"add01\" src=\"" + ImageServerPath + "/images/hotel/main4.jpg\" /></a>";
        //    }
        //}
        #endregion

        #region 共用GetList 方法
        /// <summary>
        /// 获得LIST方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="cityCode">城市编号</param>
        /// <param name="topNum">条数</param>
        /// <returns></returns>
        protected IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(EyouSoft.Model.HotelStructure.HotelShowType type, string cityCode, int topNum)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> list = null;
            iBll = new EyouSoft.BLL.HotelStructure.HotelLocalInfo();
            list = iBll.GetList(type, cityCode, topNum);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;
        }
        #endregion

        #region 获得对象方法
        /// <summary>
        /// 获得对象方法
        /// </summary>
        protected EyouSoft.Model.AdvStructure.AdvInfo GetAdvsModel(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = null;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advInfoList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (advInfoList != null && advInfoList.Count > 0)
            {
                model = advInfoList[0];
                if (model != null)
                {
                    return model;
                }
                model = null;
            }
            return null;
        }
        #endregion

    }
}
