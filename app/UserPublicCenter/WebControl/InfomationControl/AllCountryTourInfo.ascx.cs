using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;

namespace UserPublicCenter.WebControl.InfomationControl
{
    /// <summary>
    /// 全国旅游资讯
    /// mk 2010-4-1
    /// </summary>
    public partial class AllCountryTourInfo : UserControl
    {
        protected string AllCounryInfoList = "";//全国旅游资讯html
        protected string ShowQuanGuoHtml = ""; //显示‘全国’
        protected string EmptyContent = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllCounryInfoList();
                ShowQuanGuoNews();
                if (AllCounryInfoList.Length == 0 && ShowQuanGuoHtml.Length == 0)
                    EmptyContent = "  暂无最新全国旅游资讯信息 ";
            }
        }

        /// <summary>
        /// 获得全国旅游资讯信息列表
        /// </summary>
        private void GetAllCounryInfoList()
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> provinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetExistsNewsProvinceList();
            if (provinceList != null && provinceList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                foreach (EyouSoft.Model.SystemStructure.SysProvince item in provinceList)
                {
                    strAdvList.AppendFormat("<a href='{0}' target=\"_blank\">{1}</a>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListAreaUrl(item.ProvinceId), EyouSoft.Common.Utils.GetText(item.ProvinceName, 30));
                }
                AllCounryInfoList = strAdvList.ToString();
            }
        }

        /// <summary>
        /// 显示全国信息
        /// </summary>
        /// <returns></returns>
        private void ShowQuanGuoNews()
        {
            int pageSize = 1;
            int pageIndex = 1;
            int recordCount = 0;
            EyouSoft.Model.NewsStructure.SearchOrderInfo queryModel = null;
            EyouSoft.BLL.NewsStructure.NewsBll bll = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.NewsModel> lsNews = new List<EyouSoft.Model.NewsStructure.NewsModel>();

            queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
            queryModel.ProvinceId = 35;
            lsNews = bll.GetList(pageSize, pageIndex, ref recordCount, queryModel);
            if (lsNews != null)
            {
                if (lsNews.Count > 0)
                {
                    string url = EyouSoft.Common.URLREWRITE.Infomation.GetNewsListAreaUrl(35);
                    ShowQuanGuoHtml = string.Format("<a href='{0}' target=\"_blank\">全国</a> ", url);
                }
            }
        }
    }
}