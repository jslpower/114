using System;
using System.Linq;

namespace IMFrame.SupplierInfo
{
    /// <summary>
    /// MQ发布求购页
    /// 创建人：曹胡生
    /// </summary>
    public partial class PublishQiuGou : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Post();
            }
        }

        private void Post()
        {
            //同业中心编号
            int SuperID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            //同心中心所选省份
            EyouSoft.Model.MQStructure.IMSuperCluster IMSuperCluster = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetSuperClusterByID(SuperID);
            System.Collections.Generic.IList<int> ProvinceIds = null;
            if (IMSuperCluster != null)
            {
                if (IMSuperCluster.SelectType == EyouSoft.Model.MQStructure.SelectType.选择省市)
                {
                    string[] strProvinceIds = IMSuperCluster.SelectValue.Split(',');

                    if (strProvinceIds != null && strProvinceIds.Length > 0)
                    {
                        ProvinceIds = new System.Collections.Generic.List<int>();
                        for (int i = 0; i < strProvinceIds.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(strProvinceIds[i]) && EyouSoft.Common.Function.StringValidate.IsInteger(strProvinceIds[i]))
                                ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                        }
                    }
                }
            }
            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            if (content.Value.Length == 0)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "document.getElementById(\"showMsg\").innerHTML=\"请输入求购内容\";");
                return;
            }
            if (content.Value.Length > 500)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "document.getElementById(\"showMsg\").innerHTML=\"求购内容最多500字\";");
                return;
            }
            if (content.Value.Length > 15)
            {
                model.ExchangeTitle = content.Value.Substring(0, 15);
            }
            else
            {
                model.ExchangeTitle = content.Value;
            }
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = SiteUserInfo.ContactInfo.ContactName;
            model.ContactTel = SiteUserInfo.ContactInfo.Tel;
            model.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.求;
            model.ExchangeTag = EyouSoft.Model.CommunityStructure.ExchangeTag.无;
            model.ExchangeText = content.Value;
            model.IsCheck = IsCompanyCheck;
            model.IssueTime = DateTime.Now;
            model.IsTop = false;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = SiteUserInfo.ContactInfo.MQ;
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            model.AttatchPath = "";
            model.TopicClassID = EyouSoft.Model.CommunityStructure.ExchangeType.同业MQ;
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            if (Result)
            {
                content.Value = "";
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "document.getElementById(\"showMsg\").innerHTML=\"发布成功\";");
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "document.getElementById(\"showMsg\").innerHTML=\"发布失败\";");
            }
        }
    }
}
