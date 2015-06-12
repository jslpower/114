using System;
using System.Collections.Generic;

using EyouSoft.Common;

namespace SiteOperationsCenter.TongyeCenter
{
    /// <summary>
    /// 同业中心列表页
    /// 修改记录:
    /// 1. 2011-05-27 曹胡生 创建
    /// </summary>
    public partial class TongyeCenterManager : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        //总记录数
        private int RecordCount = 0;
        //当前页
        private int CurrencyPage = 1;
        //每页显示的记录数
        private int intPageSize = 20;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YuYingPermission[] parms = { YuYingPermission.同业中心_栏目管理, YuYingPermission.同业中心_栏目管理 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业中心_栏目管理, true);
                    return;
                }
                string state = Utils.GetQueryStringValue("state");
                if (state == "Del")
                {
                    YuYingPermission[] deleteParms = { YuYingPermission.同业中心_删除, YuYingPermission.同业中心_删除 };
                    if (!CheckMasterGrant(deleteParms))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.同业中心_删除, true);
                        return;
                    }
                    Del();
                }
                Bind();
            }
        }

        //绑定
        private void Bind()
        {
            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("page")) == 0 ? 1 : Utils.GetInt(Utils.GetQueryStringValue("page"));
            IList<EyouSoft.Model.MQStructure.IMSuperCluster> list = null;
            list = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetList(intPageSize, CurrencyPage, ref RecordCount);
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindFenYe();
            }
            //列表为空时
            else
            {
                this.ExportPageInfo1.Visible = false;
                this.repList.EmptyText = "<tr onmouseout=\"TongyeNoticeM.mouseouttr(this)\"  onmouseover=\"TongyeNoticeM.mouseovertr(this)\" class=\"baidi\"><td align=\"center\" colspan=\"8\" >暂时没有数据！</td></tr>";
            }
        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindFenYe()
        {
            //每页显示记录数
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.CurrencyPage = CurrencyPage;
            this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.LinkType = 3;
        }

        //删除同心中心
        private void Del()
        {
            //要删除的编号
            string ids = Utils.GetQueryStringValue("ids");
            int[] iids = { };
            if (!string.IsNullOrEmpty(ids))
            {
                iids = ConvertToIntArray(ids.Split(','));
            }
            if (EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().Del(iids))
            {
                Utils.ShowAndRedirect("删除成功", "TongyeCenterManager.aspx");
            }
        }

        //将字符串数组转化成整型数组
        private int[] ConvertToIntArray(string[] source)
        {
            int[] to = new int[source.Length];
            //将全部的数字存到数组里。
            for (int i = 0; i < source.Length; i++)
            {
                if (!string.IsNullOrEmpty(source[i].ToString()))
                {
                    to[i] = Utils.GetInt(source[i].ToString());
                }
            }
            if (to[0] == 0)
            {
                return null;
            }
            return to;
        }

        //批量设置序号
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.MQStructure.IMSuperCluster> list = new List<EyouSoft.Model.MQStructure.IMSuperCluster>();
            EyouSoft.Model.MQStructure.IMSuperCluster model = null;
            for (int i = 0; i < Utils.GetFormValues("chkId").Length; i++)
            {
                for (int j = 0; j < Utils.GetFormValues("sort").Length; j++)
                {
                    if (Utils.GetInt(Utils.GetFormValues("chkId")[i]) == Utils.GetInt(Utils.GetFormValues("hidid")[j]))
                    {
                        model = new EyouSoft.Model.MQStructure.IMSuperCluster()
                        {
                            Num = Utils.GetInt(Utils.GetFormValues("sort")[j]),
                            Id = Utils.GetInt(Utils.GetFormValues("chkId")[i])
                        };
                        list.Add(model);
                        break;
                    }
                }
            }
            if (list.Count > 0)
            {
                EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().SetNums(list);
            }
            Bind();
        }
    }
}
