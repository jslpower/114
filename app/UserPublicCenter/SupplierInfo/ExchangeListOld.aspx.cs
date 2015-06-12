using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 供求信息列表页
    /// </summary>
    /// 周文超 2010-08-03
    public partial class ExchangeList : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExchangeLeft1.CurrCityId = CityId;
            ExchangeLeft1.ImageServerPath = ImageServerPath;
            ExchangeLeft1.IsLogin = IsLogin;
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 1;
            if (!IsPostBack)
            {
                InitSearchControl();
            }
            //设置Title.....
            this.Title = string.Format(EyouSoft.Common.PageTitle.SupplyList_Title, CityModel.CityName);
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.SupplyList_Des, CityModel.CityName));
            AddMetaTag("keywords", string.Format(EyouSoft.Common.PageTitle.SupplyList_Keywords, CityModel.CityName));
        }
        #region 生成信息检索块
        public void InitSearchControl()
        {
            #region 页面参数初始化
            int time = 0;
            int tag = -1;
            int type = -1;
            int pid = 0;
            if (Request.QueryString["time"] != null)
            {
                int.TryParse(Request.QueryString["time"], out time);
            }
            if (Request.QueryString["tag"] != null)
            {
                int.TryParse(Request.QueryString["tag"], out tag);
            }
            if (Request.QueryString["type"] != null)
            {
                int.TryParse(Request.QueryString["type"], out type);
            }
            if (Request.QueryString["pid"] != null)
            {
                int.TryParse(Request.QueryString["pid"], out pid);
            }
            if (Request.QueryString["kw"] != null)
            {
                txt_keyword.Value = Utils.InputText(Request.QueryString["kw"].ToString());
            }
            #endregion

            #region 初始化时间块
            StringBuilder StrControl = new StringBuilder();
            StrControl.Append("时间：");
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.SearchDateType)))
            {
                int strValue=(int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.SearchDateType),str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{2}\">{3}</a>|",
                    strValue == time ? "background:#CCCCCC;" : "", strValue, strValue == time ? 1 : 0, str);
            }
            ltDateTypes.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion

            #region 初始化标签块
            StrControl = new StringBuilder();
            StrControl.Append("标签：");
            StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|", tag == -1 ? "background:#CCCCCC;" : "",
                -1, "全部", tag == -1 ? 1 : 0);
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|",
                    strValue == tag ? "background:#CCCCCC;" : "", strValue, str, strValue == tag ? 1 : 0);
            }
            ltTags.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion

            #region 初始化类别块
            StrControl = new StringBuilder();
            StrControl.Append("类别：");
            StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|", type == -1 ? "background:#CCCCCC;" : "",
                -1, "全部", type == -1 ? 1 : 0);
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|",
                    strValue == type ? "background:#CCCCCC;" : "", strValue, str, strValue == type ? 1 : 0);
            }
            ltTypes.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion

            #region 初始化省份块
            StrControl = new StringBuilder();
            StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|", pid == 0 ? "background:#CCCCCC;" : "",
                0, "全部", pid == 0 ? 1 : 0);
            foreach (EyouSoft.Model.SystemStructure.SysProvince model in EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList())
            {
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\"><nobr>{2}</nobr></a>|",
                    pid == model.ProvinceId ? "background:#CCCCCC;" : "", model.ProvinceId, model.ProvinceName, pid == model.ProvinceId ? 1 : 0);
            }
            ltProvinces.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion
        }
        #endregion
    }
}
