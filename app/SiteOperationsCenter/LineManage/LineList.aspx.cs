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
using EyouSoft.Model.SystemStructure;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using System.Collections.Generic;
using EyouSoft.IBLL.SystemStructure;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 线路区域列表查询
    /// 蔡永辉 2011-12-19
    /// </summary>
    public partial class LineList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 分页
        /// </summary>
        protected int currentPage = 0;
        /// <summary>
        /// 改模块权限 预留 后期添加
        /// </summary>
        protected bool IsGrantUpdate = true;
        /// <summary>
        /// 线路区域id
        /// </summary>
        protected string lineId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            string action = Utils.GetQueryStringValue("action");//ajax请求类型
            lineId = Utils.GetQueryStringValue("lineId");
            if (!IsPostBack)
            {
                BindDropDownList();
            }
            #region ajax请求
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "setTourMarkerNote"://设置类型
                        SetTourMarkerNote();
                        break;
                    case "setRouteStatus"://设置线路状态
                        SetRouteStatus();
                        break;
                    case "GetLineByType"://获取线路根据类型
                        GetLineByType();
                        break;
                    case "GetCompanyByLine"://获取专线商根据线路
                        GetCompanyByLine();
                        break;
                    case "DeleteLine"://删除线路
                        DeleteLine();
                        break;
                }
            }
            #endregion
        }

        #region 初始化类型
        protected string GetTourMarkerNote()
        {
            StringBuilder str = new StringBuilder();
            List<EnumObj> listenum = EnumObj.GetList(typeof(RecommendType));
            foreach (EnumObj model in listenum)
            {
                if (model.Value == "1")

                    str.Append("<a class=\"nostate\" href=\"javascript:void(0)\" onclick=\"LineManage.setTourMarkerNote(1);return false;\">取消设置</a>&nbsp;");
                else
                    str.AppendFormat("<a href=\"javascript:void(0)\" class=\"state{0}\" onclick=\"LineManage.setTourMarkerNote({1});return false;\">{2}</a>&nbsp;", Convert.ToInt32(model.Value) - 1, Convert.ToInt32(model.Value), model.Text);
            }
            return str.ToString();
        }
        #endregion

        #region 删除线路
        protected void DeleteLine()
        {
            Response.Clear();
            if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().DeleteSiteRoute(lineId))
            {
                Response.Write("1");
            }
            else
                Response.Write("0");
            Response.End();
        }
        #endregion

        #region 设置山下架
        /// <summary>
        /// 设置山下架
        /// </summary>
        protected void SetRouteStatus()
        {
            #region 判断权限
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            #endregion

            string arg = Utils.GetQueryStringValue("arg").TrimEnd('$');
            string[] arglist = { "" };
            if (arg.Contains('$'))
                arglist = arg.Split('$');
            else
                arglist[0] = arg;
            string status = Utils.GetQueryStringValue("RouteStatus");
            Response.Clear();
            if (arglist.Length > 0)
            {
                switch (status)
                {
                    case "1":
                        if (BRoute.CreateInstance().UpdateRouteStatus(RouteStatus.上架, arglist))
                            Response.Write("[{isSuccess:true,ErrorMessage:'设置成功'}]");
                        else
                            Response.Write("[{isSuccess:false,ErrorMessage:'设置失败'}]");
                        break;
                    case "2":
                        if (BRoute.CreateInstance().UpdateRouteStatus(RouteStatus.下架, arglist))
                            Response.Write("[{isSuccess:true,ErrorMessage:'设置成功'}]");
                        else
                            Response.Write("[{isSuccess:false,ErrorMessage:'设置失败'}]");
                        break;
                }
            }
            else
                Response.Write("[{isSuccess:false,ErrorMessage:'设置失败'}]");
            Response.End();
        }
        #endregion

        #region 根据专线类型获取专线
        protected void GetLineByType()
        {
            string argument = Utils.GetQueryStringValue("argument");
            ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            int type = Utils.GetInt(argument, 0);
            IList<SysArea> list = bll.GetSysAreaList((AreaType)type);
            StringBuilder strb1 = new StringBuilder("{tolist:[");
            foreach (SysArea item in list)
            {
                strb1.Append("{\"AreaId\":\"" + item.AreaId + "\",\"AreaName\":\"" + item.AreaName + "\"},");
            }
            Response.Clear();
            Response.Write(strb1.ToString().TrimEnd(',') + "]}");
            Response.End();
        }

        #endregion

        #region 绑定下来列表
        protected void BindDropDownList()
        {

            //类型（推荐，特价）
            this.dropRecommendType.DataSource = EnumObj.GetList(typeof(RecommendType));
            this.dropRecommendType.DataTextField = "Text";
            this.dropRecommendType.DataValueField = "Value";
            this.dropRecommendType.DataBind();
            this.dropRecommendType.Items.Insert(0, new ListItem("请选择", "-1"));

            //国内国际周边
            foreach (EnumObj item in EnumObj.GetList(typeof(AreaType)))
            {
                if (Utils.GetInt(item.Value) < 3)
                {
                    this.dropLine.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
            this.dropLine.Items.Insert(0, new ListItem("请选择", "-1"));

            //专线
            this.dropLine1.Items.Insert(0, new ListItem("请选择", "0"));


            //线路类型
            IList<EnumObj> searchLineList = EnumObj.GetList(typeof(AreaType)).Where(p => p.Value != "3").ToList();
            this.DropSearchLine.DataSource = searchLineList;
            this.DropSearchLine.DataTextField = "Text";
            this.DropSearchLine.DataValueField = "Value";
            this.DropSearchLine.DataBind();
            this.DropSearchLine.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        #endregion

        #region 设置类型

        /// <summary>
        /// 设置线路类型,即推广状态
        /// </summary>
        private void SetTourMarkerNote()
        {
            //权限判断
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }

            #region 获取推广状态 且转换成枚举
            RecommendType SpreadState = new RecommendType();
            List<RecommendType> listPowder = new List<RecommendType>();
            foreach (EnumObj item in EnumObj.GetList(typeof(RecommendType)))
            {
                if (item.Value == Utils.GetQueryStringValue("TourMarkerNote"))
                    SpreadState = ((RecommendType)Utils.GetInt(item.Value));
            }
            #endregion

            #region 获取线路ID列表
            string TemplateTourID = Utils.GetQueryStringValue("TemplateTourID");
            string[] LineIdList = { "" };
            if (!string.IsNullOrEmpty(TemplateTourID))
            {
                TemplateTourID = TemplateTourID.TrimEnd('$');
                if (TemplateTourID.Contains('$'))
                    LineIdList = TemplateTourID.Split('$');
                else
                    LineIdList[0] = TemplateTourID;
            }
            #endregion

            EyouSoft.IBLL.NewTourStructure.IRoute Ibll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            bool isTrue = Ibll.UpdateRouteRecommend(SpreadState, LineIdList);
            Ibll = null;
            Response.Clear();
            if (isTrue)
            {
                Response.Write("1");
            }
            else
            {
                Response.Write("error");
            }
            Response.End();
        }

        #endregion

        #region 获取专线商根据线路
        protected void GetCompanyByLine()
        {
            int areaid = Utils.GetInt(Utils.GetQueryStringValue("argument"));
            QueryNewCompany Searchmodel = new QueryNewCompany();
            if (areaid != 0)
                Searchmodel.AreaId = areaid;
            IList<CompanyDetailInfo> listCompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(0, Searchmodel);
            StringBuilder strb = new StringBuilder("{tolist:[");
            foreach (Company item in listCompany)
            {
                strb.Append("{\"ID\":\"" + item.ID + "\",\"CompanyName\":\"" + item.CompanyName + "\"},");
            }
            Response.Clear();
            Response.Write(strb.ToString().TrimEnd(',') + "]}");
            Response.End();
        }

        #endregion
    }
}
