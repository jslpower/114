using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.TicketStructure;

namespace UserBackCenter.TravelersManagement
{
    /// <summary>
    /// 说明：用户后台—营销工具—常旅客管理（列表）
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class TravelersList : BackPage
    {
        protected string tblID = string.Empty;
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_常旅客管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            string id = Utils.GetQueryStringValue("id");
            if (Utils.GetQueryStringValue("method") == "ajax")
            {
                this.ajaxMethod(Utils.GetQueryStringValue("type"), id);
                return;
            }
            if (!IsPostBack)
            {
                this.tblID = Guid.NewGuid().ToString();
                this.initData();
            }
        }
        protected void initData()
        {
            this.pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string kw =Server.UrlDecode(Utils.GetQueryStringValue("kw"));//姓名、手机号
            EyouSoft.BLL.TicketStructure.TicketVisitor BLL = new EyouSoft.BLL.TicketStructure.TicketVisitor();
            MVisitorSearchInfo Model = new MVisitorSearchInfo();
            Model.KeyWord = kw;//手机号？
            IList<TicketVistorInfo> lst = BLL.GetVisitors(this.pageSize, this.pageIndex, ref recordCount, this.SiteUserInfo.CompanyID, Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.BindPage(kw);
            }
            else
            {
                this.RepList.Controls.Add(new Literal() { Text = "<tr><td colspan='9' align='center'>暂无信息！</td></tr>" });
                this.ExportPageInfo1.Visible = false;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="kw">关键字（姓名、手机号）</param>
        protected void BindPage(string kw)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("kw", kw);
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax方法
        /// </summary>
        /// <param name="type">save:保存  del:删除</param>
        /// <param name="id">id</param>
        protected void ajaxMethod(string type, string id)
        {
            if (string.IsNullOrEmpty(type))
            {
                return;
            }
            switch (type)
            {
                case "del":
                    this.del(id);
                    break;
            }
        }
        /// <summary>
        /// ajax_删除
        /// </summary>
        protected void del(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            string str = string.Empty;
            EyouSoft.BLL.TicketStructure.TicketVisitor BLL = new EyouSoft.BLL.TicketStructure.TicketVisitor();
            if (BLL.DeleteTicketVisitorInfo(id.Split(',')))
            {
                str = "删除成功！";
            }
            else
            {
                str = "删除失败！";
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
        }
        /// <summary>
        /// 根据城市编号返回城市名  如果是中国，则返回“省名-城市名”，否则返回国家名
        /// </summary>
        protected string getCityName(object o, object countryId)
        {
            string str = string.Empty;
            int nationId = 0;
            Int32.TryParse(Convert.ToString(countryId), out nationId);
            if (nationId != 0)
            {
                if (nationId == 224)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(o)))
                    {
                        //列表MODEL中没有城市名
                        EyouSoft.BLL.SystemStructure.SysCity BLL = new EyouSoft.BLL.SystemStructure.SysCity();
                        EyouSoft.Model.SystemStructure.SysCity Model=BLL.GetSysCityModel(Utils.GetInt(o.ToString()));
                        if (null != Model)
                        {
                            str =string.IsNullOrEmpty(Model.ProvinceName)?"":Model.ProvinceName+"-"+Model.CityName;
                        }
                    }
                }
                else
                {
                    //返回国家名   
                    EyouSoft.BLL.SystemStructure.BSysCountry nationBLL = new EyouSoft.BLL.SystemStructure.BSysCountry();
                    EyouSoft.Model.SystemStructure.MSysCountry nationModel = nationBLL.GetCountry(nationId);
                    if (null != nationModel)
                    {
                        str =string.Format("{0}({1})",nationModel.CName,nationModel.EnName);
                    }
                }
            }
            return str;
        }
    }
}