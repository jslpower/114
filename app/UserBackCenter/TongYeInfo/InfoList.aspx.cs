using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.NewsStructure;
using EyouSoft.BLL.NewsStructure;
using System.Text;
using EyouSoft.IBLL.NewsStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.TongYeInfo
{
    /// <summary>
    /// 说明：用户后台—营销工具—同业资讯（列表）
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class InfoList : BackPage
    {
        protected string tblID = string.Empty;
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        private bool isZT = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_同业资讯))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!IsPostBack)
            {
                isZT = SiteUserInfo.CompanyRole.HasRole(CompanyType.组团);
                this.tblID = Guid.NewGuid().ToString();
                this.InitData();
            }
        }
        protected void InitData()
        {
            this.pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //是否显示”添加“按钮 //说明：添加按钮只对“专线商（地接）、景区、酒店“显示
            bool myShopFlag = !this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);//this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线)||this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店);
            this.btnAddInfo.Visible = myShopFlag;
            string kw = Server.UrlDecode(Utils.GetQueryStringValue("kw"));//关键字（标题、发布单位）
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"), -1);//分类
            MQueryPeerNews Model = new MQueryPeerNews();
            Model.KeyWord = kw.Trim();
            if (type != -1)
            {
                Model.TypeId = (PeerNewType)type;
            }
            IPeerNews BLL = BPeerNews.CreateInstance();
            IList<MPeerNews> lst = BLL.GetGetPeerNewsList(this.pageSize, this.pageIndex, ref this.recordCount, Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.BindPage(kw, type.ToString());
            }
            else
            {
                this.RepList.Controls.Add(new Literal() { Text = "<tr><td colspan='4' align='center'>暂无信息！</td></tr>" });
                this.ExportPageInfo1.Visible = false;
            }

        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="kw">关键字（标题、发布单位）</param>
        /// <param name="type">分类</param>
        protected void BindPage(string kw, string type)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("kw", kw);
            this.ExportPageInfo1.UrlParams.Add("type", type);
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 页面初始化值绑定
        /// </summary>
        /// <param name="type">"type":分类</param>
        /// <returns></returns>
        protected string getInfo(string type)
        {
            StringBuilder str = new StringBuilder();
            switch (type)
            {
                case "type":
                    //分类
                    List<EnumObj> lstType = EnumObj.GetList(typeof(EyouSoft.Model.NewsStructure.PeerNewType));
                    if (null != lstType && lstType.Count > 0)
                    {
                        for (int i = 0; i < lstType.Count; i++)
                        {
                            str.AppendFormat("<option value=\"{0}\">{1}</option>", lstType[i].Value, lstType[i].Text);
                        }
                    }
                    break;
            }
            return str.ToString();
        }
        /// <summary>
        /// 返回公司网店URL链接
        /// </summary>
        /// <param name="cmpName">公司名</param>
        /// <param name="cmpID">公司ID</param>
        /// <returns></returns>
        protected string getShopUrl(object cmpName, object cmpID)
        {
            string strUrl = string.Empty;
            string strCmpName = Convert.ToString(cmpName);
            string strCmpID = Convert.ToString(cmpID);
            if (!string.IsNullOrEmpty(strCmpName) && !string.IsNullOrEmpty(strCmpID))
            {
                //判断是否开通了高级网店
                if (Utils.IsOpenHighShop(strCmpID))//开通了高级网店
                {
                    strUrl = Domain.SeniorOnlineShop + "/seniorshop/default.aspx?cid=" + strCmpID;
                }
                else//没有开通
                {
                    strUrl = Domain.UserPublicCenter + "/_" + strCmpID;
                }
            }
            return string.Format("<a href=\"{0}\" target='_blank'>{1}</a>", strUrl, strCmpName);
        }
        /// <summary>
        /// 返回资讯相关链接
        /// </summary>
        /// <param name="AreaName">区域名</param>
        /// <param name="AreaId">区域ID</param>
        /// <param name="ScenicId">景区ID</param>
        /// <returns></returns>
        protected string getInfoAboutHref(object AreaName, object AreaId, object ScenicId, object CompanyId)
        {
            string str = string.Empty;
            EyouSoft.BLL.CompanyStructure.CompanyInfo companyBLL = new EyouSoft.BLL.CompanyStructure.CompanyInfo();
            CompanyDetailInfo companyModel = companyBLL.GetModel(Convert.ToString(CompanyId));
            if (null != companyModel)
            {
                if (companyModel.CompanyRole.HasRole(CompanyType.地接) || companyModel.CompanyRole.HasRole(CompanyType.专线))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(AreaName)))
                    {
                        //2012-02-10 14:10信息来源：楼 链接到组团菜单中的"旅游线路库"
                        str = string.Format(@"<a href='javascript:void(0)'" + (isZT ? @" onclick=""topTab.open('{0}','资讯相关',{{}})""" : "") + @" class='font12_grean' title='{1}'>【{1}】</a>", isZT ? EyouSoft.Common.Domain.UserBackCenter + "/teamservice/linelibrarylist.aspx?lineId=" + AreaId : "", Utils.GetText2(Eval("AreaName").ToString(), 6, true));
                    }
                }
                else if (companyModel.CompanyRole.HasRole(CompanyType.景区))
                {
                    EyouSoft.Model.ScenicStructure.MScenicArea Area = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetModel(Convert.ToString(ScenicId));
                    if (null != Area)
                    {
                        str = string.Format(@"<a href=""{0}"" target=""_blank"" class='font12_grean' title='{1}'>【{1}】</a>", EyouSoft.Common.Domain.UserPublicCenter + "/ScenicManage/NewScenicDetails.aspx?ScenicId=" + Area.ScenicId, Utils.GetText2(Area.ScenicName, 6, true));
                    }
                }
            }
            return str;
        }
    }
}