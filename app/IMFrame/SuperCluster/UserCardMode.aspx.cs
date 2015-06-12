using System;
using System.Collections;
using EyouSoft.BLL.MQStructure;
using EyouSoft.ControlCommon.Control;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace IMFrame.SuperCluster
{
    public partial class UserCardMode : EyouSoft.ControlCommon.Control.MQPage
    {
        private int pageSize = 12;//页大小
        private int pageIndex = 1;//当前页索引
        private int recordCount;//记录总数
        /// <summary>
        ///  卡片URL
        /// </summary>
        protected string ModeUrl
        {
            get
            {
                string srcUrl = Request.Url.ToString();
                if (srcUrl.Contains("&Page"))
                {
                    int index = srcUrl.IndexOf("&Page");
                    return srcUrl.Substring(0, index);
                }
                return srcUrl;
            }
        }

        /// <summary>
        ///  列表URL
        /// </summary>
        protected string ListUrl
        {
            get
            {
                return ModeUrl.Replace("Mode", "List");
            }
        }

        protected EyouSoft.Model.MQStructure.IMClusterUserCard SelfModel = new EyouSoft.Model.MQStructure.IMClusterUserCard();
        protected IList<EyouSoft.Model.MQStructure.IMClusterUserCard> listUserCard = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("opType").Length <= 0)//如果请求中包含添加操作
            {
                if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
                {
                    pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
                    listUserCard = GetData();//获取数据
                    OtherBind();
                }
                else
                {
                    listUserCard = GetData();//获取数据
                    if (listUserCard != null)
                    {
                        if (listUserCard.Count > 0)
                        {
                            if (MQLoginId != listUserCard[0].MQ)//若是游客查看也是进行OtherBind();
                            {
                                OtherBind();
                            }
                            else
                            {
                                FirstBind();
                            }
                        }
                        else
                        {
                            this.ExportPageInfo1.Visible = false;
                            this.place_holder.Visible = false;
                        }
                    }
                    else
                    {
                        this.place_holder.Visible = false;
                        this.ExportPageInfo1.Visible = false;
                    }
                }
            }
            else
            {
                UploadLogo();
            }
        }

        /// <summary>
        /// 第一次请求页面时绑定数据（主要是绑定本人名片和剩下的9个MQ名片）
        /// </summary>
        private void FirstBind()
        {
            if (listUserCard != null)
            {
                if (listUserCard.Count > 0)
                {
                    SelfModel = listUserCard[0];
                    if (SelfModel != null)
                    {
                        this.companyId_hidden.Value = SelfModel.CompanyId;
                        listUserCard.RemoveAt(0);//请求第一页时删除出了本人以外的数据
                        Repeater1.DataSource = listUserCard;
                        Repeater1.DataBind();
                        BindPage();
                    }
                    else
                    {
                        this.ExportPageInfo1.Visible = false;
                        this.place_holder.Visible = false;
                    }
                }
                else
                {
                    this.ExportPageInfo1.Visible = false;
                    this.place_holder.Visible = false;
                }
            }
            else
            {
                this.ExportPageInfo1.Visible = false;
                this.place_holder.Visible = false;
            }
        }

        /// <summary>
        /// 不是第一次请求时的数据绑定
        /// </summary>
        private void OtherBind()
        {
            this.place_holder.Visible = false; //隐藏首页的第一个元素
            if (listUserCard != null)
            {
                if (listUserCard.Count > 0)
                {
                    Repeater1.DataSource = listUserCard;
                    Repeater1.DataBind();
                    BindPage();
                }
                else
                {
                    this.ExportPageInfo1.Visible = false;
                }
            }
            else
            {
                this.ExportPageInfo1.Visible = false;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private IList<EyouSoft.Model.MQStructure.IMClusterUserCard> GetData()
        {
            //同中中心编号
            int superID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            int mqId = MQLoginId;
            //int superID = 20009;
            //int mqId = 15183;
            return IMSuperCluster.CreateInstance().GetUserCardListByClusterId(pageSize, pageIndex, ref recordCount, superID, mqId);
        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 更新Logo
        /// </summary>
        private void UploadLogo()
        {
            string logoPath = Utils.GetFormValue("path", 250);
            string companyId = Utils.GetFormValue("companyId");
            EyouSoft.Model.CompanyStructure.CompanyMQAdv logoModel = new EyouSoft.Model.CompanyStructure.CompanyMQAdv()
            {
                ImagePath = logoPath
            };
            bool result = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyMQAdv(companyId, logoModel);
            Response.Clear();
            Response.Write(string.Format("{{\"res\":{0}}}", true ? 1 : -1));
            Response.End();
        }


        #region 前台调用

        /// <summary>
        /// 是否显示网店地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string IsShowWebStore(string url)
        {
            string temp = string.Format("<a href=\"{1}\" target=\"_blank\" style=\" padding-left:15px; background:url({0}/IM/images/mp_08.jpg) no-repeat left center;text-decoration:none;_padding-top:3px;_padding-bottom:2px; _background:url(images/mp_08.jpg) no-repeat left 2px;\">点击查看网店</a>", ImageServerUrl, url);
            if (!string.IsNullOrEmpty(url))
            {
                if (url.Length > 0)
                {
                    return temp;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取公司logo的显示路径
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        protected string GetCompanyLogoSrc(object src)
        {
            string emptySrc = string.Format("{0}/IM/images/mp_03.jpg", ImageServerUrl);
            if (src != null)
            {
                if (string.IsNullOrEmpty(src.ToString()))
                {
                    return emptySrc;
                }
                else
                {
                    if (emptySrc.Length <= 0)
                    {
                        return emptySrc;
                    }
                    else
                    {
                        return string.Format("{0}{1}", EyouSoft.Common.Domain.FileSystem, src.ToString());
                    }
                }
            }
            return emptySrc;
        }

        /// <summary>
        /// 是否显示文件上传按钮
        /// </summary>
        /// <param name="index">行号</param>
        /// <returns></returns>
        protected string IsShowFileButton(int index)
        {
            //string temp = string.Format("<input type=\"button\" style=\"width:25px; height:20px; border:0; background:url({0}/IM/images/mp_11.jpg) no-repeat right bottom;position:absolute; bottom:14px;\"/>",ImageServerUrl);
            string temp = "<uc1:MQCardFileUpload ID=\"MQCardFileUpload1\" runat=\"server\" />";
            if (index == 0)
                return temp;
            return "";
        }
        #endregion


    }
}
