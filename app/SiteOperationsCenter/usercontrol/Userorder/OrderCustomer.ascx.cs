using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteOperationsCenter.usercontrol.UserOrder
{
    public partial class OrderCustomer : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> _tourOrderCustomer;

        /// <summary>
        /// 旅客数据集合
        /// </summary>
        public IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> TourOrderCustomer
        {
            get { return _tourOrderCustomer; }
            set { _tourOrderCustomer = value; }
        }

        private string _txtAudltID;

        /// <summary>
        /// 成人数文本框ID
        /// </summary>
        public string TxtAudltID
        {
            get { return _txtAudltID; }
            set { _txtAudltID = value; }
        }

        private string _txtChildID;

        /// <summary>
        /// 儿童数文本框ID
        /// </summary>
        public string TxtChildID
        {
            get { return _txtChildID; }
            set { _txtChildID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.rptList.DataSource = TourOrderCustomer;
                this.rptList.DataBind();
            }
        }

        protected void GetData ()
        {

            
            #region 处理旅客
            //string[] txtName = Utils.GetFormValues("txtName");
            //string[] txtTel = Utils.GetFormValues("txtTel");
            //string[] txtCard = Utils.GetFormValues("txtCard");
            //string[] txtCardS = Utils.GetFormValues("txtCardS");
            //string[] txtCardT = Utils.GetFormValues("txtCardT");
            //string[] sltSex = Utils.GetFormValues("sltSex");
            //string[] sltChild = Utils.GetFormValues("sltChild");
            //string[] txtNumber = Utils.GetFormValues("txtNumber");
            //string[] txtRemarks = Utils.GetFormValues("txtRemarks");
            //string[] cbxVisitor = Utils.GetFormValues("cbxVisitor");


            //IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> customerList = new List<EyouSoft.Model.NewTourStructure.MTourOrderCustomer>();

            //if (txtName.Length > 0)
            //{
            //    for (int i = 0; i < txtName.Length; i++)
            //    {
            //        EyouSoft.Model.NewTourStructure.MTourOrderCustomer model = new EyouSoft.Model.NewTourStructure.MTourOrderCustomer();
            //        model.CertificatesType = EyouSoft.Model.TicketStructure.TicketCardType.None;
            //        model.CompanyId = SiteUserInfo.CompanyID;
            //        model.IsSaveToTicketVistorInfo = cbxVisitor[i] == "1" ? true : false;
            //        model.ContactTel = txtTel[i];
            //        model.CradType = sltChild[i] == "1" ? EyouSoft.Model.TicketStructure.TicketVistorType.成人 : EyouSoft.Model.TicketStructure.TicketVistorType.儿童;
            //        model.IdentityCard = txtCard[i];
            //        model.IssueTime = DateTime.Now;
            //        model.Notes = txtRemarks[i];
            //        model.OtherCard = txtCardT[i];
            //        model.Passport = txtCardS[i];
            //        model.Sex = sltSex[i] == "0" ? EyouSoft.Model.CompanyStructure.Sex.男 : EyouSoft.Model.CompanyStructure.Sex.女;
            //        model.SiteNo = txtNumber[i];
            //        model.VisitorName = txtName[i];

            //        customerList.Add(model);
            //    }
            //}
            #endregion
        }
    }
}