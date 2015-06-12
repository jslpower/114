using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewsStructure;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 同业资讯列表页
    /// 开发人：方琪 
    /// 时间：2011-12-14
    /// </summary>
    public partial class InformationIndustry : EyouSoft.Common.Control.YunYingPage
    {
        protected int intPage = 0;
        protected int isReturn = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //修改完成返回时要用到的页码
            intPage = Utils.GetInt(Utils.GetQueryStringValue("Page"));
            if (!IsPostBack)
            {
                //类别绑定
                this.DdlType.DataSource = EnumObj.GetList(typeof(PeerNewType));
                this.DdlType.DataTextField = "Text";
                this.DdlType.DataValueField = "Value";
                this.DdlType.DataBind();
                this.DdlType.Items.Insert(0,new ListItem("请选择","-1"));
            }
            #region 处理前台请求
            string Type = Utils.GetQueryStringValue("Type");
            if (Type.Equals("Delete", StringComparison.OrdinalIgnoreCase))
            {
                Response.Clear();
                Response.Write(this.DeletInformation());
                Response.End();
            }
            #endregion
        }

        protected bool DeletInformation()
        {
            bool Result = false;
            string[] strinformationId = Utils.GetFormValues("ckInformationId"); //获取勾选项
            if (strinformationId != null)
            {
                Result = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().DelPeerNews(strinformationId) == 1 ? true : false;
            }
            return Result;
        }

        protected void Setzero()
        {
            intPage = 0;
        }
    }
}
