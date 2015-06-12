using System;
using System.Web.UI.WebControls;

using EyouSoft.Common;

namespace SiteOperationsCenter.TongyeCenter
{
    /// <summary>
    /// 同业中心公告与广播编辑页
    /// 修改记录:
    /// 1. 2011-05-27 曹胡生 创建
    /// </summary>
    public partial class TongyeNoticeEdit : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //公告编号
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (id == 0)
            {
                YuYingPermission[] parms = { YuYingPermission.同业中心_公告广播新增, YuYingPermission.同业中心_公告广播新增 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业中心_公告广播新增, true);
                    return;
                }
            }
            else
            {
                YuYingPermission[] parms = { YuYingPermission.同业中心_公告广播修改, YuYingPermission.同业中心_公告广播修改 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业中心_公告广播修改, true);
                    return;
                }
            }
            if (!IsPostBack)
            {
                Bind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //公告编号
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            //类型(公告0,广播1)
            string NoticeClass = Utils.GetQueryStringValue("NoticeClass");
            //公告对象
            string TongyeCenterID = string.Empty;
            EyouSoft.Model.MQStructure.IMSuperClusterNews model = new EyouSoft.Model.MQStructure.IMSuperClusterNews();
            if (NoticeClass == "0")
            {
                model.Category = EyouSoft.Model.MQStructure.Type.公告;
            }
            else if (NoticeClass == "1")
            {
                model.Category = EyouSoft.Model.MQStructure.Type.广播;
            }
            foreach (ListItem item in chkNoticeList.Items)
            {
                if (item.Selected)
                {
                    TongyeCenterID += item.Value + ",";
                }
            }
            if (string.IsNullOrEmpty(TongyeCenterID))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请选择公告对象!')");
                return;
            }
            else
            {
                TongyeCenterID = TongyeCenterID.Trim(',');
            }
            if (string.IsNullOrEmpty(FCK_PlanTicketContent.Value))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请输入正文!')");
                return;
            }
            model.Num = Utils.GetInt(txtSort.Value);
            model.Centres = TongyeCenterID;
            model.NewsContent = FCK_PlanTicketContent.Value;
            model.Operater = MasterUserInfo.ContactName;
            model.Title = txtTitle.Text;
            model.OperateTime = Utils.GetDateTime(txtDate.Text, DateTime.Now);
            if (id == 0)
            {
                if (EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().Add(model))
                {
                    Utils.ShowAndRedirect("添加成功", "TongyeNotice.aspx?NoticeClass=" + NoticeClass);
                }
                else
                {
                    Utils.ShowAndRedirect("添加失败", "TongyeNotice.aspx?NoticeClass=" + NoticeClass);
                }
            }
            else
            {
                model.Id = id;
                if (EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().Upd(model))
                {
                    Utils.ShowAndRedirect("修改成功", "TongyeNotice.aspx?NoticeClass=" + NoticeClass);
                }
                else
                {
                    Utils.ShowAndRedirect("修改成功", "TongyeNotice.aspx?NoticeClass=" + NoticeClass);
                }
            }
        }

        //绑定公告对象
        private void BindNoticeTo()
        {
            chkNoticeList.DataSource = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetAllClusters();
            chkNoticeList.DataTextField = "Title";
            chkNoticeList.DataValueField = "Id";
            chkNoticeList.DataBind();
        }

        private void Bind()
        {
            BindNoticeTo();
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (id != 0)
            {
                EyouSoft.Model.MQStructure.IMSuperClusterNews model = EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().GetModel(id);
                if (model != null)
                {
                    //标题
                    txtTitle.Text = model.Title;
                    //公告对象
                    foreach (var item in model.Centres.Split(','))
                    {
                        if (item != "")
                        {
                            chkNoticeList.Items.FindByValue(item).Selected = true;
                        }
                    }
                    //序号
                    txtSort.Value = model.Num.ToString();
                    //公告正文
                    FCK_PlanTicketContent.Value = model.NewsContent;
                    //修改时间
                    txtDate.Text = model.OperateTime.ToString();
                    //发布人就是当前操作人???
                    txtOper.Text = MasterUserInfo.ContactName;
                }
            }
            else
            {
                //修改时间
                txtDate.Text = DateTime.Now.ToString();
                //发布人就是当前操作人???
                txtOper.Text = MasterUserInfo.ContactName;
            }
        }
    }
}
