using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using EyouSoft.Common;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 描述：新闻列表页面
    /// 修改记录:
    /// 1. 2011-04-02 AM 曹胡生 创建
    /// </summary>
    public partial class NewsList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private int RecordCount = 0;
        //当前页
        private int CurrencyPage = 1;
        //每页显示的记录数
        private int intPageSize = 20;
        protected bool EditFlag = false;
        protected bool DeleteFlag = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //权限验证
            YuYingPermission[] editParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_修改 };
            EditFlag = CheckMasterGrant(editParms);
            YuYingPermission[] deleteParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_删除 };
            DeleteFlag = CheckMasterGrant(deleteParms);
            if (!EditFlag && !DeleteFlag)
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!IsPostBack)
            {
                
                if (DeleteFlag)
                {
                    if (Utils.GetQueryStringValue("State") == "Del")
                    {
                        DelNew(Utils.GetInt(Utils.GetQueryStringValue("Id")));
                    }
                }
                else
                {
                   
                   
            
                    Utils.ResponseNoPermit(YuYingPermission.新闻中心_删除.ToString());
                    return;
                }
                BindAfficheSource();
                BindNewsClass();
                DataInit();
            }
        }

        /// <summary>
        /// 新闻列表初始化
        /// </summary>
        private void DataInit()
        {
        
            EyouSoft.Model.NewsStructure.SearchOrderInfo SearchOrderInfo = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
            //标题
            string Title = Utils.GetQueryStringValue("Title");
            //页数
            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //类别
            int ClassId = Utils.GetInt(Utils.GetQueryStringValue("ClassId"));
            //省份
            int ProId = Utils.GetInt(Utils.GetQueryStringValue("ProId"));
            //城市
            int CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            //排序
            int Zhidin = Utils.GetInt(Utils.GetQueryStringValue("selZhidin"));
            //推荐
            string TueiJian = Utils.GetQueryStringValue("chkTueiJian").ToLower();
            SearchOrderInfo.Title = Title;
            if (ClassId != 0)
            {
                SearchOrderInfo.Type = ClassId;
            }
            if (ProId != 0)
            {
                SearchOrderInfo.ProvinceId = ProId;
            }
            if (CityId != 0)
            {
                SearchOrderInfo.City = CityId;
            }
            if (TueiJian == "true")
            {
                SearchOrderInfo.IsRecPositionId = true;
            }
            if (Zhidin != 0)
            {
                SearchOrderInfo.Source = (EyouSoft.Model.NewsStructure.AfficheSource)Zhidin;
            }

            if (ProId != 0)
            {
                ProvinceAndCityList1.SetProvinceId = ProId;
            }
            if (CityId != 0)
            {
                ProvinceAndCityList1.SetCityId = CityId;
            }
            if (Zhidin != 0)
            {
                selZhidin.Items.FindByValue(Zhidin.ToString()).Selected = true;
            }
            if (ClassId != 0)
            {
                selNewClass.Items.FindByValue(ClassId.ToString()).Selected = true;
            }
            chkTueiJian.Checked = TueiJian == "true" ? true : false;
            txtNewTitle.Value = Title;
            EyouSoft.BLL.NewsStructure.NewsBll NewsBll = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.NewsModel> list = NewsBll.GetList(intPageSize, CurrencyPage, ref RecordCount, SearchOrderInfo);
           
            this.repList.DataSource = list;
            this.repList.DataBind();
            BindFenYe();

        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindFenYe()
        {
            //绑定分页控件
            this.ExportPageInfo1.intPageSize = intPageSize;//每页显示记录数
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.CurrencyPage = CurrencyPage;
            this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.LinkType = 3;
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="Id"></param>
        private void DelNew(int Id)
        {
            EyouSoft.BLL.NewsStructure.NewsBll NewsBll = new EyouSoft.BLL.NewsStructure.NewsBll();
            //标题
            string Title = Utils.GetQueryStringValue("Title");
            //页数
            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //类别
            int ClassId = Utils.GetInt(Utils.GetQueryStringValue("ClassId"));
            //省份
            int ProId = Utils.GetInt(Utils.GetQueryStringValue("ProId"));
            //城市
            int CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            //排序
            int Zhidin = Utils.GetInt(Utils.GetQueryStringValue("selZhidin"));
            //推荐
            string TueiJian = Utils.GetQueryStringValue("chkTueiJian").ToLower();
            if (NewsBll.DelNews(Id))
            {
                Utils.ShowAndRedirect("删除成功", string.Format("NewsList.aspx?Title={0}&ClassId={1}&page={2}&ProId={3}&CityId={4}&selZhidin={5}&chkTueiJian={6}", Title, ClassId, CurrencyPage, ProId, CityId, Zhidin, TueiJian));
            }
        }

        /// <summary>
        /// 绑定新闻类别
        /// </summary>
        private void BindNewsClass()
        {
            EyouSoft.BLL.NewsStructure.NewsType NewsType = new EyouSoft.BLL.NewsStructure.NewsType();
            IList<EyouSoft.Model.NewsStructure.NewsType> list = NewsType.GetAllType();
            if (list != null && list.Count > 0)
            {
                selNewClass.DataTextField = "ClassName";
                selNewClass.DataValueField = "Id";
                selNewClass.DataSource = list;
                selNewClass.DataBind();
                this.selNewClass.Items.Insert(0, new ListItem("请选择", ""));
            }
            NewsType = null;
            list = null;
        }

        /// <summary>
        /// 返回一个新闻浏览地址
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="NewId"></param>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public string ReWriteNews(object typeId, object NewId)
        {
            int type = Utils.GetInt(typeId == null ? "0" : typeId.ToString());
            int id = Utils.GetInt(NewId == null ? "0" : NewId.ToString());
            return EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(type, id);
        }

        /// <summary>
        /// 绑定文章排序
        /// </summary>
        private void BindAfficheSource()
        {
            System.Collections.Generic.List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.NewsStructure.AfficheSource));
            if (list != null && list.Count > 0)
            {
                this.selZhidin.DataTextField = "Text";
                this.selZhidin.DataValueField = "Value";
                this.selZhidin.DataSource = list;
                this.selZhidin.DataBind();
                this.selZhidin.Items.Insert(0, new ListItem("请选择", ""));
            }
            list = null;
        }

        /// <summary>
        /// 转化推荐
        /// </summary>
        public string RecPositionList(object o)
        {
            string temp = string.Empty;
            if (o != null)
            {
                IList<EyouSoft.Model.NewsStructure.RecPosition> RecPositionList = o as IList<EyouSoft.Model.NewsStructure.RecPosition>;
                foreach (var item in RecPositionList)
                {
                    temp += item.ToString() + ',';
                }
                if (temp != string.Empty)
                {
                    temp = temp.TrimEnd(',');
                }
            }
            return temp;
        }

        /// <summary>
        /// 新闻浏览地址重写，当为URL跳转时，跳到跳转的URL
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ReWrite(params object[] list)
        {

            if (list != null)
            {
                if (list[0] != null)
                {
                    IList<EyouSoft.Model.NewsStructure.RecPosition> RecPositionList = list[0] as IList<EyouSoft.Model.NewsStructure.RecPosition>;
                    foreach (var item in RecPositionList)
                    {
                        if (item == EyouSoft.Model.NewsStructure.RecPosition.URL跳转)
                        {
                            return list[4].ToString();
                        }
                    }
                }
                return ReWriteNews(list[1], list[2]);
            }
            return "";
        }
    }
}
